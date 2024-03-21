using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Collections;
using DotNetSiemensPLCToolBoxLibrary.Communication;

namespace XFactoryNET.Custom.MM1.ServiceLibrary
{
    public class ThreadTrasporti
    {
        public static ThreadTrasporti[] threadTrasps;
        private static Thread[] threads;

        public static void StartThread()
        {
            threads = new Thread[2];
            threadTrasps = new ThreadTrasporti[2];


            threadTrasps[0] = new ThreadTrasporti("CPUTrasp1",8,9);
            ThreadStart myThreadStart = new ThreadStart(threadTrasps[0].Run);
            threads[0] = new Thread(myThreadStart);
            threads[0].Start();


            threadTrasps[1] = new ThreadTrasporti("CPUTrasp2", 28, 29);
            myThreadStart = new ThreadStart(threadTrasps[1].Run);
            threads[1] = new Thread(myThreadStart);
            threads[1].Start();

        }

        public static void StopThread()
        {
            if (threads[0] != null)
            {
                threads[0].Abort();
                threads[0].Join();
            }

            if (threads[1] != null)
            {
                threads[1].Abort();
                threads[1].Join();
            }

        }

        private string device;
        private int sendDB;
        private int recDB;
        const int DBSIZE = 20;
        PCMessage pcMessage = new PCMessage();
        PLCMessage plcMessage = new PLCMessage();

        struct PCMessage
        {
            public PLCTag tagPCReq;
            public PLCTag tagPCAck;
            public PLCTag tagCodiceComando;
            public PLCTag tagCodiceTrasporto;
            public PLCTag tagSilosPrelievo;
            public PLCTag tagSilosDestinazione;
            public PLCTag tagParam1;
            public PLCTag tagParam2;
            public PLCTag tagParam3;
            public PLCTag tagParam4;
            public PLCTag tagParam5;
            public PLCTag tagParam6;
            public PLCTag tagParam7;
            public PLCTag tagParam8;
        }

        struct PLCMessage
        {
            public PLCTag tagPLCReq;
            public PLCTag tagPLCAck;
            public PLCTag tagCodiceMessaggio;
            public PLCTag tagCodiceTrasporto;
            public PLCTag tagNrSilos;
            public PLCTag tagCodiceStato;
            public PLCTag tagReport1;
            public PLCTag tagReport2;
        }

        enum StatusEnum
        {
            Unknown,
            Idle,
            Busy,
            Waiting,
            Error
        }

        StatusEnum receiveStatus = StatusEnum.Idle;
        StatusEnum sendStatus = StatusEnum.Idle;


        public Queue MsgQueue = new Queue();

        public ThreadTrasporti(string device,int SendDB,int RecDB)
        {
            this.device = device;
            this.sendDB = SendDB;
            this.recDB = RecDB;
        }

        private PLCConnection myConn = new PLCConnection("PLCConfig");
        List<PLCTag> pollingList = new List<PLCTag>();
        List<PLCTag> readList = new List<PLCTag>();
        List<PLCTag> writeList = new List<PLCTag>();

        public delegate void stateConnectedDelegate();
        public stateConnectedDelegate myDelegate;
        private void stateConnected()
        {
            ;//Connected
        }

        public void Run()
        {

            myConn.Connect();
            myDelegate += new stateConnectedDelegate(stateConnected);


            pcMessage.tagPCReq = new PLCTag(string.Format("DB{0}.DBX1.14", sendDB));
            pcMessage.tagPCReq.ValueChanged += new PLCTag.ValueChangedEventHandler(tagPCReq_ValueChanged);

            pcMessage.tagPCAck = new PLCTag(string.Format("DB{0}.DBX1.15", sendDB));
            pcMessage.tagPCAck.ValueChanged += new PLCTag.ValueChangedEventHandler(tagPCAck_ValueChanged);

            writeList.Add(pcMessage.tagCodiceComando = new PLCTag(string.Format("DB{0}.DBB1", recDB)));
            writeList.Add(pcMessage.tagCodiceTrasporto = new PLCTag(string.Format("DB{0}.DBW2", recDB)));
            writeList.Add(pcMessage.tagSilosPrelievo = new PLCTag(string.Format("DB{0}.DBW2", recDB)));
            writeList.Add(pcMessage.tagSilosDestinazione = new PLCTag(string.Format("DB{0}.DBW7", recDB)));
            writeList.Add(pcMessage.tagParam1 = new PLCTag(string.Format("DB{0}.DBW11", recDB)));
            writeList.Add(pcMessage.tagParam2 = new PLCTag(string.Format("DB{0}.DBW12", recDB)));
            writeList.Add(pcMessage.tagParam3 = new PLCTag(string.Format("DB{0}.DBW13", recDB)));
            writeList.Add(pcMessage.tagParam4 = new PLCTag(string.Format("DB{0}.DBW14", recDB)));
            writeList.Add(pcMessage.tagParam5 = new PLCTag(string.Format("DB{0}.DBW15", recDB)));
            writeList.Add(pcMessage.tagParam6 = new PLCTag(string.Format("DB{0}.DBW16", recDB)));
            writeList.Add(pcMessage.tagParam7 = new PLCTag(string.Format("DB{0}.DBW17", recDB)));
            writeList.Add(pcMessage.tagParam8 = new PLCTag(string.Format("DB{0}.DBW18", recDB)));
            

            plcMessage.tagPLCReq = new PLCTag(string.Format("DB{0}.DBX1.14", recDB));
            plcMessage.tagPLCReq.ValueChanged += new PLCTag.ValueChangedEventHandler(tagPLCReq_ValueChanged);
            plcMessage.tagPLCAck = new PLCTag(string.Format("DB{0}.DBX1.15", recDB));
            plcMessage.tagPLCAck.ValueChanged += new PLCTag.ValueChangedEventHandler(tagPLCAck_ValueChanged);

            readList.Add(plcMessage.tagCodiceMessaggio = new PLCTag(string.Format("DB{0}.DBB1", recDB)));
            readList.Add(plcMessage.tagCodiceTrasporto = new PLCTag(string.Format("DB{0}.DBW2", recDB)));
            readList.Add(plcMessage.tagNrSilos = new PLCTag(string.Format("DB{0}.DBW3", recDB)));
            readList.Add(plcMessage.tagCodiceStato = new PLCTag(string.Format("DB{0}.DBW4", recDB)));
            readList.Add(plcMessage.tagReport1 = new PLCTag(string.Format("DB{0}.DBW12", recDB)));
            readList.Add(plcMessage.tagReport2 = new PLCTag(string.Format("DB{0}.DBW13", recDB)));

            pollingList.Add(pcMessage.tagPCReq);
            pollingList.Add(pcMessage.tagPCAck);
            pollingList.Add(plcMessage.tagPLCReq);
            pollingList.Add(plcMessage.tagPLCAck);

            myConn.PLCStart();

            try
            {
                lock (this)
                {
                    while (true)
                    {
                        myConn.ReadValues(pollingList);
                        Monitor.Wait(this, 500);
                    }
                }
            }
            catch (ThreadAbortException)
            {
                Thread.ResetAbort();
            }
            finally
            {
                myConn.Disconnect();
                myConn.Dispose();
                myConn = null;
            }

        }

        bool plcRequest
        {
            get
            {
                myConn.ReadValue(plcMessage.tagPLCReq);
                return (bool)plcMessage.tagPLCReq.Value;
            }
        }

        bool pcRequest
        {
            set 
            { 
                pcMessage.tagPCReq.Value = value;
                myConn.WriteValue(pcMessage.tagPCReq);
            }
        }

        bool plcAck
        {
            get {
                myConn.ReadValue(plcMessage.tagPLCAck);
                return (bool)plcMessage.tagPLCAck.Value; 
            }
        }

        bool pcAck
        {
            set 
            { 
                pcMessage.tagPCAck.Value = value;
                myConn.WriteValue(pcMessage.tagPCAck);
            }
            get {
                myConn.ReadValue(pcMessage.tagPCAck);
                return (bool)pcMessage.tagPCAck.Value; 
            }
        }


        void tagPLCAck_ValueChanged(PLCTag Sender, PLCTag.ValueChangedEventArgs e)
        {
            HandleCommunication();
        }

        void tagPLCReq_ValueChanged(PLCTag Sender, PLCTag.ValueChangedEventArgs e)
        {
            HandleCommunication();
        }

        void tagPCAck_ValueChanged(PLCTag Sender, PLCTag.ValueChangedEventArgs e)
        {
            HandleCommunication();
        }

        void tagPCReq_ValueChanged(PLCTag Sender, PLCTag.ValueChangedEventArgs e)
        {
            HandleCommunication();
        }

        private void HandleCommunication()
        {

            switch (receiveStatus)
            {
                case StatusEnum.Unknown:
                    if (pcAck)
                        receiveStatus = StatusEnum.Busy;
                    else
                        receiveStatus = StatusEnum.Idle;
                    break;
                case StatusEnum.Idle:
                    if (plcRequest)
                    {
                        receiveStatus = StatusEnum.Busy;
                        Analize_PLC_message();
                        pcAck = true;
                    }
                    else if (MsgQueue.Count > 0)
                    {
                        Analize_PC_message();
                        pcRequest = true;
                        sendStatus = StatusEnum.Busy;
                    }
                    break;
                case StatusEnum.Busy:
                    if (plcRequest == false)
                    {
                        receiveStatus = StatusEnum.Idle;
                        pcAck = false;
                    }
                    break;
                default:
                    break;
            }

            switch (sendStatus)
            {
                case StatusEnum.Unknown:

                    break;
                case StatusEnum.Idle:
                    break;
                case StatusEnum.Busy:
                    if (plcAck == true)
                    {
                        pcRequest = false;
                        sendStatus = StatusEnum.Waiting;
                    }
                    break;
                case StatusEnum.Waiting:
                    if (plcAck == false)
                    {
                        sendStatus = StatusEnum.Idle;
                    }
                    break;
                case StatusEnum.Error:
                    break;
                default:
                    break;
            }
        }

        private void Analize_PC_message()
        {
            if (MsgQueue.Count == 0)
                return;

            TransportMsg sMsg = (TransportMsg)MsgQueue.Dequeue();


            //
            //...
            //


            myConn.WriteValues(writeList);

        }

        private void Analize_PLC_message()
        {
            myConn.ReadValues(readList);

            //
            //...
            //
        }

    }

    public enum TransportCode
    {
        UNKNOWN = -1,
        PRESSA1 = 2111,
        PRESSA2A = 2112,
        PRESSA3A = 2113,
        PRESSA2B = 2114,
        PRESSA3B = 2115,
        SACCHI1 = 3101,
        SACCHI2 = 3102,
        SACCHI3 = 3103,
        RINFUSA1 = 3104,
        SACCONI = 3109,
        PREMIX = 1008,
        LIQ1 = 1121,
        LIQ2 = 1122,
        PNEUM1 = 1131,
        PNEUM2 = 1132,
        RINFUSA2 = 3106,
        BUCA1 = 1111,
        BUCA2 = 1112,
        //		T1113=1113,
        RICICLO = 1141,
        MACINAZIONE = 1150
    }

    public enum TransportMsgType
    {
        START_TRANSPORT = 0x0001,
        STOP_TRANSPORT = 0x0002,
        CHANGE_SOURCE = 0x0004,
        CHANGE_DEST = 0x0008,
        CHANGE_PARAM = 0x0010
    }

    public enum TransportResponseType
    {
        START = 0x0001,
        STOP = 0x0002,
        CHANGE = 0x0004,
        REPORT = 0x0008
    }

    public enum TransportStatus
    {
        OK = 0,
        UNKNOWN_SOURCE = 1,
        EMPTY = 3,
        FULL = 4,
        MANUAL = 6,
        BUSY = 7,
        ALREADY_STOPPED = 8,
        STOPPED = 10,
        NOT_STARTED = 11,
    }

    public struct TransportMsg
    {
        public TransportMsgType Type;
        public TransportCode Code;
        public int Source;
        public int Dest;
        public int IDLotto;
        public int IDTrasporto;
    }

    internal struct TransportResponse
    {
        public int IDTrasporto;
        public TransportResponseType Response;
        public TransportCode Code;
        public int Silos;
        public TransportStatus Status;
        public int ReportWord1;
        public int ReportWord2;
        public int ReportWord3;
    }

}