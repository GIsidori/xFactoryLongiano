using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace ISISoft.Siemens.Library
{
    public class DataEventArgs : EventArgs
    {
        public string Data;
        public DataEventArgs(string s)
        {
            Data = s;
        }
    }

    public class RK512 : IDisposable
    {

        public event EventHandler<DataEventArgs> DataSend;
        public event EventHandler<DataEventArgs> DataReceive;

        public enum rkEnum
        {
            Data,
            Input,
            Output,
            Merker,
            Counter,
            Timer
        }

        private System.IO.Ports.SerialPort port = new System.IO.Ports.SerialPort();

        private const char NUL = (char)0x00;
        private const char STX = (char)0x02;
        private const char ETX = (char)0x03;
        private const char EOT = (char)0x04;
        private const char ACK = (char)0x06;
        private const char DLE = (char)0x10;
        private const char NAK = (char)0x15;
        private const char FF = (char)0xFF;

        private string doubleDLE = string.Concat(DLE, DLE);

        private int tQVZ = 500;
        private const int tZVZ = 220;
        private const int tBLOCCO = 4000;
        private const int tREA = 5000;

        int x;
        int w;

        bool bNAK;
        bool bConflict;

        public RK512()
        {
            UseBCC = true;
        }

        private bool mHighPriority;
        public bool HighPriority
        {
            get { return mHighPriority; }
            set { mHighPriority = value; }
        }


        private bool useBCC = false;
        public bool UseBCC {
            get { return useBCC; }
            set { useBCC = value; tQVZ = useBCC ? 2000 : 500; }
        }

        public bool IsOpen
        {
            get { return port.IsOpen; }
        }

        public bool OpenPort(string settings)
        {
            if (port.IsOpen)
                return false;

            string sPortName = "COM1";
            int iBaudRate = 9600;
            int iDataBit = 7;
            Parity mParity = Parity.Even;
            StopBits mStopBit = StopBits.One;

            string[] portParams = settings.Split(',');       //COM1,9600,[None|Even|Odd|Mark|Space],[7|8],[None|One|Two|OnePointFive]
            if (portParams.Length > 0)
            {
                sPortName = portParams[0];
                if (portParams.Length > 1)
                {
                    int.TryParse(portParams[1], out iBaudRate);
                    if (portParams.Length > 2)
                    {
                        mParity = (Parity) Enum.Parse(typeof(Parity),portParams[2]);
                        if (portParams.Length > 3)
                        {
                            int.TryParse(portParams[3], out iDataBit);
                            if (portParams.Length > 4)
                            {
                               mStopBit = (StopBits) Enum.Parse(typeof(StopBits), portParams[4]);
                            }
                        }
                    }
                }
            }

            port.PortName = sPortName;
            port.BaudRate = iBaudRate;
            port.DataBits = iDataBit;
            port.Parity = mParity;
            port.StopBits = mStopBit;
            try
            {
                port.Open();
                Send(NAK);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public void ClosePort()
        {
            if (port.IsOpen)
                port.Close();
        }


        public void Dispose()
        {
            if (port != null)
            {
                port.Dispose();
                port = null;
            }
        }

        private int SendDatagram(string buffer)
        {
            string rxBuff = null;
            string sDati = buffer + DLE + ETX;

            if (useBCC)
                sDati = sDati + Bcc(sDati);

            w = 1;
retry: 
            x = 1;
sndSTX: 
            Send(STX);
wDLE:
            if (ReceiveNBytes(ref rxBuff, 1, tQVZ) == 0) goto t1;

            switch (rxBuff[0])
            {
                case DLE:
                    if (Send(sDati) != 0) goto w1;
                    if (ReceiveNBytes(ref rxBuff, 1, tQVZ) == 0) goto w1;
                    if (rxBuff[0] == DLE)
                    {
                        return 0;
                    }
                w1:
                    w++;
                    if (w <= 6) goto retry;
                    Send(NAK);
                    return 1;
               case STX:
                    if (mHighPriority) goto wDLE;
                    bConflict = true;
                    ReceiveDatagram(rxBuff, 0);
                    bConflict = false;
                    goto retry;
            }


t1:
            x++;
            if (x <= 6) goto sndSTX;

            Send(NAK);
            return 2;

        }

        private int ReceiveDatagram(string buffer, int timeout)
        {
            string rxBuff = null;
            string sDati = null;
            char mBCC = (char)0x00;

            if (!bConflict)
            {
                if (ReceiveNBytes(ref rxBuff, 1, timeout) == 0)
                    return 1;
                if (rxBuff[0] != NAK && rxBuff[0] != STX)
                {
                    bNAK = true;
                    goto quattro;
                }
            }

 uno:
            Send(DLE);

quattro:
            string DleEtx = new string(new char[] { DLE, ETX });    // string.Empty + (char)DLE + (char)ETX;
            if (ReceiveUntil(ref sDati, DleEtx, tZVZ) == false) { bNAK = true; goto sndNAK; };
            if (useBCC) mBCC = Bcc(sDati);
            sDati = RemoveDLE(sDati);
            if (useBCC)
            {
                if (ReceiveNBytes(ref rxBuff, 1, tZVZ) == 0) { bNAK = true; goto sndNAK; }
                if (mBCC != rxBuff[0]) { bNAK = true; goto sndNAK; };
                buffer = sDati.Substring(0, sDati.Length - 2);
            }
            else
                buffer = sDati.Substring(0, sDati.Length - 1);

        sndNAK:
            if (bNAK)
            {
                bNAK = false;
                Send(NAK);
                if (w <= 5)
                {
                    if (ReceiveNBytes(ref rxBuff, 1, tBLOCCO) != 0)
                    {
                        if (rxBuff[0] == STX)
                        {
                            w++;
                            goto uno;
                        }
                    }
                }
            }
            else
            {
                Send(DLE);
            }

            return 0;

        }

        private char Bcc(string buffer)
        {
            char bcc = (char)0x00;
            for (int i = 0; i < buffer.Length; i++)
            {
                bcc = (char) ((byte)bcc ^ (byte)buffer[i]);
            }
            return bcc;
        }

        private string AddDLE(string buffer)
        {
            string s;
            int j;

            s = buffer;
            j = -1;

            do
            {
                if (j == -1)
                    j = s.IndexOf((char)DLE);
                else
                    j = s.IndexOf((char)DLE, j + 1);
                if (j != -1)
                {
                    s = s.Substring(0, j+1) + (char)DLE + s.Substring(j+1,s.Length - j-1);
                    j++;
                }
            } while (j!=-1);

            return s;
        }

        private string RemoveDLE(string buffer)
        {
            string s;
            int j;

            s = buffer;
            j = -1;

            do
            {
                j = s.IndexOf(string.Empty + DLE + DLE,j+2);
                if (j != -1)
                {
                    s = s.Substring(0, j+1) + s.Substring(j + 1, s.Length - j - 2);
                }
            } while (j != -1);

            return s;
        }

        private int Send(char b)
        {
            try
            {
                port.Write(b.ToString());
                if (DataSend != null)
                    DataSend(this, new DataEventArgs(b.ToString()));
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        private int Send(string buffer)
        {
            try
            {
                port.Write(buffer);
                if (DataSend != null)
                    DataSend(this,new DataEventArgs(buffer));
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        private int ReceiveNBytes(ref string buffer, int iLen, int iTimeout)
        {
            char[] rxBuff= new char[iLen];

            try
            {
                port.ReadTimeout = iTimeout;
                int rc= port.Read(rxBuff, 0, iLen);
                buffer = new string(rxBuff);
                if (DataReceive != null)
                    DataReceive(this,new DataEventArgs(buffer));
                return rc;
            }
            catch
            {
                return 0;
            }
        }

        private bool ReceiveUntil(ref string buffer, string waitFor, int iTimeout)
        {
            try
            {
                port.ReadTimeout = iTimeout;
                buffer = port.ReadTo(waitFor);
                if (DataReceive != null)
                    DataReceive(this,new DataEventArgs(buffer));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int WriteDW(int db, int dw, int data)
        {
            int[] aData = new int[] { data };
            int rc = RK512Put(rkEnum.Data, db, dw, 1, aData);
            return rc;
        }

        public int RK512Put(rkEnum rkType, int iDB, int iDW, int iLen, int[] txData)
        {
            string txBuffer = string.Empty;
            string sDati;
            char cType = 'D';
            bool bNext = false;
            string sHead;
            string rxBuffer = string.Empty;

            foreach (var i in txData)
            {
                int j = i;
                if ((j & 0x8000) != 0)
                {
                    j = j + 0x10000;
                }
                txBuffer = txBuffer + (char)(j/256) + (char)(j % 256);
            }

            cType = DecodeType(rkType);

            do
            {
                sDati = txBuffer;
                if (txBuffer.Length > 128)
                {
                    sDati = txBuffer.Substring(0, 128);
                    txBuffer = txBuffer.Substring(128, txBuffer.Length - 128);
                }
                else
                    txBuffer = string.Empty;

                sDati = AddDLE(sDati);

                if (sDati.Length > 128)
                {
                    txBuffer = sDati.Substring(128, sDati.Length - 128) + txBuffer;
                    if (sDati.Substring(128, 2) == doubleDLE)
                        sDati = sDati.Substring(0, 127);
                    else
                        sDati = sDati.Substring(0, 128);
                }

                if (bNext)
                    sHead = string.Concat(FF,NUL,'A',cType);
                else
                    sHead = string.Concat(NUL, NUL,'A', cType, (char)iDB, (char)iDW, (char)(iLen / 256), (char)(iLen % 256), FF, FF);

                sHead = AddDLE(sHead);

                if (SendDatagram(sHead + sDati) == 0)
                {
                    if (ReceiveDatagram(rxBuffer, tREA) != 0)
                    {
                        return -2;
                    }
                    if (rxBuffer[3] != 0x00)
                    {
                        return rxBuffer[3];
                    }
                    if (bNext)
                    {
                        if (rxBuffer[0] == 0x00)
                            return -3;
                    }
                    else
                    {
                        if (rxBuffer[0] != 0x00)
                            return -4;
                        bNext = true;
                    }
                }
                else
                    return -1;
            } while (txBuffer != string.Empty);

            return 0;

        }

        public int ReadDW(int db, int dw)
        {
            int[] data = new int[1];
            int rc = RK512Get(rkEnum.Data, db, dw, 1,ref data);
            if (rc == 0)
                return data[0];

            throw new ApplicationException(string.Format("Errore nr {0}",rc));

        }

        public int RK512Get(rkEnum rkType, int iDB, int iDW, int iLen,ref int[] rxData)
        {
            bool bNext = false;
            string sHead;
            string rxBuffer = string.Empty;
            int k = 0;
            int lWord;

            //la word da leggere deve essere pari.
            if ((iDW % 2) != 0)
                return -5;

            iDW = iDW / 2;

            char cType = DecodeType(rkType);

            do
            {
                char[] buff;
                if (bNext)
                {
                    buff = new char[4];
                    buff[0] = FF;
                    buff[1] = NUL;
                    buff[2] = 'E';
                    buff[3] = cType;
                    sHead = new string(buff);
                    //sHead = string.Concat(FF, NUL, 'E', (char)cType);// string.Format("{0}{1}E{2}", 0xff, NUL, cType);
                }
                else
                {
                    buff = new char[10];
                    buff[0] = NUL;
                    buff[1] = NUL;
                    buff[2] = 'E';
                    buff[3] = cType;
                    buff[4] = (char)iDB;
                    buff[5] = (char)iDW;
                    buff[6] = (char)(iLen / 256);
                    buff[7] = (char)(iLen % 256);
                    buff[8] = FF;
                    buff[9] = FF;
                    sHead = new string(buff);
                    //sHead = string.Concat(NUL, NUL, 'E', (char)cType, (byte)iDB, (byte)iDW, (byte)(iLen / 256), (byte)(iLen % 256), FF, FF);
                }
                sHead = AddDLE(sHead);
                if (SendDatagram(sHead) == 0)
                {
                    if (ReceiveDatagram(rxBuffer, tREA) != 0)
                        return -2;
                    if (rxBuffer.Length>3 && rxBuffer[3] != 0)
                        return rxBuffer[3];
                    if (bNext)
                    {
                        if (rxBuffer[0] == 0x00)
                            return -3;
                    }
                    else
                    {
                        if (rxBuffer[0] != 0x00)
                            return -4;
                        bNext = true;
                    }
                    for (int i = 4; i < rxBuffer.Length; i += 2)
                    {
                        lWord = rxBuffer[i] * 256 + rxBuffer[i + 1];
                        if ((lWord & 0x8000) != 0)
                            lWord = lWord - 0x10000;
                        rxData[k] = lWord;
                        k++;
                    }
                }
                else
                    return -1;

            } while (k < iLen);

            return 0;

        }

        private char DecodeType(rkEnum rkType)
        {
            char cType = 'D';
            switch (rkType)
            {
                case rkEnum.Data:
                    cType = 'D';
                    break;
                case rkEnum.Input:
                    cType = 'E';
                    break;
                case rkEnum.Output:
                    cType = 'A';
                    break;
                case rkEnum.Merker:
                    cType = 'M';
                    break;
                case rkEnum.Timer:
                    cType = 'T';
                    break;
                case rkEnum.Counter:
                    cType = 'Z';
                    break;
            }

            return cType;
        }

    }
}