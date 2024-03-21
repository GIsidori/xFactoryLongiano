using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Collections;
using System.IO.Ports;
using DevExpress.Xpo;
using System.Configuration;
using XFactoryNET.Module.BusinessObjects;

namespace XFactoryNET.Custom.MM1.WcfService
{
    public class ThreadPesa
    {

        static ThreadPesa threadPesa;
        static Thread thread;
        
        // Terminal interface port.
        private static System.IO.Ports.SerialPort Port = new SerialPort();
        

        private string settings;
        private string sPortName;
        private int iBaudRate, iDataBit;
        private float fPeso;

        Parity mParity;
        StopBits mStopBit;

        private bool bError = false;

        private static System.Collections.Generic.Dictionary<string, ThreadPesa> threads = new Dictionary<string, ThreadPesa>();

        public static void StartThread()
        {
            string connectionString = null;
            if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }

            using (Session session = new Session() { ConnectionString = connectionString })
            {

                XPCollection<Pesa> pese = new XPCollection<Pesa>(session);

                foreach (var pesa in pese)
                {
                    threadPesa = new ThreadPesa(pesa.Settings);
                    ThreadStart start = new ThreadStart(threadPesa.Run);
                    thread = new Thread(start) { Name = pesa.Codice };
                    thread.Start();
                    threads.Add(pesa.Codice, threadPesa);
                }
            }
        }

        public static void StopThread()
        {
            if (thread != null)
            {
                thread.Abort();
                thread.Join();
            }
            thread = null;
        }

        public static float GetPeso(string codicePesa)
        {
            if (threads.ContainsKey(codicePesa))
                return threads[codicePesa].fPeso;
            return 0F;
        }

        public ThreadPesa(string settings)
        {
            this.settings = settings;
        }

        public void Run()
        {
            try
            {
                //OpenComm();
                while (true)
                {
                    Thread.Sleep(2000);
                    fPeso = 1234F; // LeggiPeso();
                }
            }
            catch (ThreadAbortException)
            {
                CloseComm();
            }
            
        }

        private float LeggiPeso()
        {
            const int BLEN = 46;
            byte[] buffer = new Byte[BLEN];
            char[] chars = new Char[5];
            int iRead;
            float pPeso = 0;
            try
            {
                iRead = Port.Read(buffer,0,BLEN);
                if (iRead == BLEN)
                {
                    //buffer = Port.InputStream;
                    pPeso = Decode(buffer);
                }
                if (bError)
                {
                    //("Acquisizione peso bilico: comunicazione ripristinata");
                }
                bError = false;
            }
            catch (Exception ex)
            {
                if (!bError)
                {
                    //string.Format("Acquisizione peso bilico: errore: {0}", ex.Message);
                    bError = true;
                }
            }
            Port.DiscardInBuffer();
            return pPeso;

        }

        public void OpenComm()
        {

            if (!Port.IsOpen)
            {

                sPortName = "COM1";
                iBaudRate = 9600;
                iDataBit = 7;
                mParity = Parity.Even;
                mStopBit = StopBits.One;
                string[] portParams = settings.Split(',');       //COM1,9600,7,Even,One
                if (portParams.Length > 0)
                {
                    sPortName = portParams[0];
                    if (portParams.Length > 1)
                    {
                        int.TryParse(portParams[1], out iBaudRate);
                        if (portParams.Length > 2)
                        {
                            int.TryParse(portParams[2], out iDataBit);
                            if (portParams.Length > 3)
                            {
                                Enum.TryParse<Parity>(portParams[3], out mParity);
                                if (portParams.Length > 4)
                                {
                                    Enum.TryParse<StopBits>(portParams[4], out mStopBit);
                                }
                            }
                        }
                    }
                }

                Port.PortName = sPortName;
                Port.BaudRate = iBaudRate;
                Port.DataBits = iDataBit;
                Port.Parity = mParity;
                Port.StopBits = mStopBit;

                try
                {
                    //AlertProvider.AddAlert(string.Format("Apertura porta com{0}: {1},{2},{3},{4}", iPort, iBaudRate, mParity, iDataBit, mStopBit), null, AlertSeverity.Error);
                    Port.Open();
                }
                catch (Exception e)
                {
                    //ISIFactory.Log.LogEvent(string.Format("Errore apertura porta com{0}: {1}", iPort, e.Message), ISIFactory.Log.LogEventType.Error);
                    return;
                }
            }

        }

        public void CloseComm()
        {
            if (Port.IsOpen)
                Port.Close();
        }

        private static float Decode(byte[] buffer)
        {
            string s;
            const int BLEN = 46;
            char[] chars = new Char[5];
            float fPeso = 0;

            for (int i = 0; i < BLEN; i++)
            {
                if (buffer[i] == 2)
                {
                    for (int j = 0; j < 5; j++)
                        chars[j] = (char)buffer[i + j + 5];
                    break;
                }
            }

            s = new string(chars);
            if (s.Length != 0)
            {
                try
                {
                    fPeso = System.Convert.ToSingle(s);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }

            return fPeso;
        }

    }
}