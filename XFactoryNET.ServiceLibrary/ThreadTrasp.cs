using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Threading;
using System.Runtime.InteropServices;
using System.Linq;

using OPC.Common;
using OPC.Data.Interface;
using OPC.Data;
using DevExpress.Xpo;
using XFactoryNET.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using XFactoryNET.Custom.MM1.Module.BusinessObjects;
using System.Collections.Generic;

namespace XFactoryNET.Custom.MM1.ServiceLibrary
{
    internal enum TransportCode
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

    internal enum TransportMsgType
    {
        START_TRANSPORT = 0x0001,
        STOP_TRANSPORT = 0x0002,
        CHANGE_SOURCE = 0x0004,
        CHANGE_DEST = 0x0008,
        CHANGE_PARAM = 0x0010
    }

    internal enum TransportResponseType
    {
        START = 0x0001,
        STOP = 0x0002,
        CHANGE = 0x0004,
        REPORT = 0x0008
    }

    internal enum TransportStatus
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

    internal struct TransportMsg
    {
        public TransportMsgType Type;
        public TransportCode Code;
        public int Source;
        public int Dest;
        public int IDLotto;
    }

    internal struct TransportResponse
    {
        public TransportResponseType Response;
        public TransportCode Code;
        public int Silos;
        public TransportStatus Status;
        public int ReportWord1;
        public int ReportWord2;
        public int ReportWord3;
    }

    internal class ThreadTrasp
    {
        internal static Thread t1;
        internal static Thread t2;

        internal static ThreadTrasp ThreadTrasp1;
        internal static ThreadTrasp ThreadTrasp2;

        public static void StartThread()
        {

            ThreadTrasp1 = new ThreadTrasp("CPU1", 8, 9);
            ThreadStart threadStart = new ThreadStart(ThreadTrasp1.Run);
            t1 = new Thread(threadStart);
            t1.Name = "Trasp_CPU1";
            t1.Start();

            ThreadTrasp2 = new ThreadTrasp("CPU2", 28, 29);
            threadStart = new ThreadStart(ThreadTrasp2.Run);
            t2 = new Thread(threadStart);
            t2.Name = "Trasp_CPU2";
            t2.Start();


        }

        public static void StopThread()
        {
            if (t1 != null)
            {
                t1.Abort();
                t1.Join();
            }
            if (t2 != null)
            {
                t2.Abort();
                t2.Join();
            }

        }

        string OPCDevice;
        string OPCServerProgID;
        string OPCItemStringFormat;							//Formato OPCItem

        const int UNKNOWN = 4;
        const int SEND_IDLE = 0;
        const int SEND_BUSY = 1;
        const int SEND_WAITING = 2;
        const int SEND_ERROR = 3;

        const int RECEIVE_IDLE = 0;
        const int RECEIVE_BUSY = 1;
        const int RECEIVE_ERROR = 3;

        const int RICHIESTA_DA_PC = 0x4000;
        const int ACK_DA_PC = 0x8000;

        const int RESET_TRASPORTO = 0x2000;
        const int RICHIESTA_DA_PLC = 0x4000;
        const int ACK_DA_PLC = 0x8000;

        int SDB, RDB;
        int _receive_status = UNKNOWN;
        int _send_status = UNKNOWN;

        const int DBSIZE = 20;

        int[] RDBW = new int[DBSIZE + 1];					//RDBW[0] non è utilizzato
        int[] SDBW = new int[DBSIZE + 1];					//SDBW[0] non è utilizzato

        private OpcServer theSrv;
        private OpcGroup theGrp;
        private OpcGroup thePollingGrp;

        private const int millisecondsTimeout = 2000;		//Timeout.Infinite;				//Heartbeat time

        private OPCItemDef[] SXitemDefs = new OPCItemDef[DBSIZE];
        private OPCItemDef[] RXitemDefs = new OPCItemDef[DBSIZE];

        private OPCItemDef[] PollItemDefs = new OPCItemDef[1];			//RDB.DW1

        private int[] RXhandlesSrv = new int[DBSIZE];
        private int[] SXhandlesSrv = new int[DBSIZE];
        private int[] PollHandles = new int[1];									//Handle di RDB.DW1

        internal Queue MsgQueue = new Queue();
        UnitOfWork uow;

		public ThreadTrasp(string opcDevice,int SendDB,int RecDB)
		{
			OPCDevice = opcDevice;
			SDB = SendDB;
			RDB = RecDB;
		}

        public void Run()
        {

            string connectionString = null;
            if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }


            OPCServerProgID = "ProScada.SiemensIE";
            OPCItemStringFormat = "{0}:DB{1},{2}";

            theSrv = new OpcServer();
            theSrv.Connect(OPCServerProgID);

            Thread.Sleep(500);				// we are faster then some servers!

            theGrp = theSrv.AddGroup("OPCGroup:" + OPCDevice, false, 900);

            // add items and save server handles
            for (int i = 0; i < DBSIZE; i++)
            {
                RXitemDefs[i] = new OPCItemDef(string.Format(OPCItemStringFormat, OPCDevice, RDB, i + 1), true, i, VarEnum.VT_EMPTY);
                SXitemDefs[i] = new OPCItemDef(string.Format(OPCItemStringFormat, OPCDevice, SDB, i + 1), true, i + DBSIZE, VarEnum.VT_EMPTY);
            }

            OPCItemResult[] rItm;
            theGrp.AddItems(SXitemDefs, out rItm);
            if (rItm == null)
                return;

            for (int i = 0; i < DBSIZE; i++)
            {
                if (HRESULTS.Failed(rItm[i].Error))
                {
                    AlertProvider.AddAlert(string.Format("OPCAddItems error OPCGroup: {0}, OPCItem: DB{1}:DW{2}", theGrp.Name, SDB, i + 1), null, AlertSeverity.Error);
                    theGrp.Remove(true);
                    theSrv.Disconnect();
                    return;
                }
                else
                    SXhandlesSrv[i] = rItm[i].HandleServer;			//Send Items
            }


            theGrp.AddItems(RXitemDefs, out rItm);
            if (rItm == null)
                return;

            for (int i = 0; i < DBSIZE; i++)
            {
                if (HRESULTS.Failed(rItm[i].Error))
                {
                    AlertProvider.AddAlert(string.Format("OPCAddItems error OPCGroup: {0}, OPCItem: DB{1}:DW{2}", theGrp.Name, RDB, i + 1), null, AlertSeverity.Error);
                    theGrp.Remove(true);
                    theSrv.Disconnect();
                    return;
                }
                else
                    RXhandlesSrv[i] = rItm[i].HandleServer;			//Receive Items
            }

            theGrp.SetEnable(true);
            theGrp.Active = true;

            thePollingGrp = theSrv.AddGroup("OPCPollingGroup:" + OPCDevice, false, 900);

            // add items and save server handles
            PollItemDefs[0] = new OPCItemDef(string.Format(OPCItemStringFormat, OPCDevice, RDB, 1), true, 999, VarEnum.VT_EMPTY);
            thePollingGrp.AddItems(PollItemDefs, out rItm);

            if (rItm == null)
                return;

            PollHandles[0] = rItm[0].HandleServer;						//Handle di RDB.DW1

            thePollingGrp.SetEnable(true);
            thePollingGrp.Active = true;

            thePollingGrp.DataChanged += new DataChangeEventHandler(this.thePollingGrp_DataChange);
            //				thePollingGrp.ReadCompleted+=new ReadCompleteEventHandler(this.thePollingGrp_ReadComplete);

            theGrp.ReadCompleted += new ReadCompleteEventHandler(this.theGrp_ReadComplete);
            theGrp.WriteCompleted += new WriteCompleteEventHandler(this.theGrp_WriteComplete);

            this.receive_status = RECEIVE_IDLE;
            this.send_status = SEND_IDLE;

            this.ReceiveDB();

            try
            {
                lock (this)
                {
                    while (true)
                    {
                        try
                        {
                            uow = new UnitOfWork() { ConnectionString = connectionString };
                            Monitor.Wait(this, millisecondsTimeout);
                            this.Handle_Communication();
                            uow.CommitChanges();
                        }
                        catch
                        {
                            uow.DropChanges();
                        }
                        finally
                        {
                            uow.Dispose();
                        }
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                }
            }
            catch (ThreadAbortException)
            {
                Thread.ResetAbort();
            }
            catch (Exception e)
            {
                AlertProvider.AddAlert(e.Message, null, AlertSeverity.Panic);
            }
            finally
            {
                theGrp.Remove(true);
                thePollingGrp.Remove(true);
                theSrv.Disconnect();
            }

        }

        private int send_status
        {
            get
            {
                return _send_status;
            }
            set
            {
                if (_send_status != value)
                {
                    _send_status = value;
                }
            }
        }

        private int receive_status
        {
            get
            {
                return _receive_status;
            }
            set
            {
                if (_receive_status != value)
                {
                    _receive_status = value;
                }
            }
        }

        public void thePollingGrp_DataChange(object sender, DataChangeEventArgs e)
        {
            //			ISIFactory.Log.LogEvent("DataChange event: gh={0} id={1} me={2} mq={3}", e.groupHandleClient, e.transactionID, e.masterError, e.masterQuality);
            foreach (OPCItemState s in e.sts)
            {
                if (HRESULTS.Succeeded(s.Error))
                {
                    if ((OPC_QUALITY_STATUS)(s.Quality & (short)OPC_QUALITY_MASKS.STATUS_MASK) != OPC_QUALITY_STATUS.OK)
                    {
                        AlertProvider.AddAlert(string.Format("DB{0}:DW1={1} Quality:{2}", RDB, RDBW[1], OpcGroup.QualityToString(s.Quality)), null, AlertSeverity.Warning);
                    }
                    RDBW[1] = System.Convert.ToInt16(s.DataValue);
                }
                else
                {
                    AlertProvider.AddAlert(string.Format("DB{0}:DW1={1} Error:{2}", RDB, RDBW[1], s.Error), null, AlertSeverity.Error);
                }
            }
            lock (this)
            {
                Handle_Communication();
            }
        }

        public void thePollingGrp_ReadComplete(object sender, ReadCompleteEventArgs e)
        {
            //			ISIFactory.Log.LogEvent("ReadComplete event: gh={0} id={1} me={2} mq={3}", e.groupHandleClient, e.transactionID, e.masterError,e.masterQuality);
            foreach (OPCItemState s in e.sts)
            {
                if (HRESULTS.Succeeded(s.Error))
                {
                    if ((OPC_QUALITY_STATUS)(s.Quality & (short)OPC_QUALITY_MASKS.STATUS_MASK) != OPC_QUALITY_STATUS.OK)
                    {
                        AlertProvider.AddAlert(string.Format("DB{0}:DW1={1} Quality:{2}", RDB, RDBW[1], OpcGroup.QualityToString(s.Quality)), null, AlertSeverity.Warning);
                    }
                    RDBW[1] = System.Convert.ToInt16(s.DataValue);
                }
                else
                {
                    AlertProvider.AddAlert(string.Format("DB{0}:DW1={1} Error:{2}", RDB, RDBW[1], s.Error), null, AlertSeverity.Error);
                }
            }
            lock (this)
            {
                Handle_Communication();
            }
        }

        public void theGrp_ReadComplete(object sender, ReadCompleteEventArgs e)
        {
            //			ISIFactory.Log.LogEvent("ReadComplete event: gh={0} id={1} me={2} mq={3}", e.groupHandleClient, e.transactionID, e.masterError, e.masterQuality);
            foreach (OPCItemState s in e.sts)
            {
                if (HRESULTS.Succeeded(s.Error))
                {
                    if (s.HandleClient < DBSIZE)
                    {
                        if ((OPC_QUALITY_STATUS)(s.Quality & (short)OPC_QUALITY_MASKS.STATUS_MASK) != OPC_QUALITY_STATUS.OK)
                        {
                            AlertProvider.AddAlert(string.Format("DB{0}:DW{1}={2} Quality:{3}", RDB, s.HandleClient + 1, s.DataValue, OpcGroup.QualityToString(s.Quality)), null, AlertSeverity.Warning);
                        }
                        RDBW[s.HandleClient + 1] = System.Convert.ToInt16(s.DataValue);
                    }
                }
                else
                {
                    AlertProvider.AddAlert(string.Format("DB{0}:DW{1}={2} Error:{3}", RDB, s.HandleClient + 1, s.DataValue, s.Error), null, AlertSeverity.Error);
                }
            }

            if (receive_status == RECEIVE_BUSY)
            {
                lock (this)
                {
                    Analyze_PLC_message();
                    SDBW[1] = SDBW[1] | ACK_DA_PC;
                    SendDB();
                }
            }

        }

        public void theGrp_WriteComplete(object sender, WriteCompleteEventArgs e)
        {
            //			ISIFactory.Log.LogEvent("WriteComplete event: gh={0} id={1} me={2}", e.groupHandleClient, e.transactionID, e.masterError );
            foreach (OPCWriteResult r in e.res)
            {
                if (HRESULTS.Succeeded(r.Error))
                {
                    //						ISIFactory.Log.LogEvent(" ih={0} e={1}", r.HandleClient, r.Error );
                }
                else
                {
                    AlertProvider.AddAlert(string.Format("DB{0}:DW{1} WriteError:{3}", SDB, r.HandleClient + 1, r.Error), null, AlertSeverity.Error);
                }
            }
        }

        private void SendDB()
        {
            int CancelID;
            int[] aE;

            object[] itemValues = new Object[DBSIZE];

            for (int i = 0; i < DBSIZE; i++)
            {
                itemValues[i] = SDBW[i + 1];
            }

            //			ISIFactory.Log.LogEvent("SendDB DB={0} - DW1= {1:x}",SDB,SDBW[1]);
            try
            {
                theGrp.Write(SXhandlesSrv, itemValues, 99887766, out CancelID, out aE);
            }
            catch (Exception e)
            {
                AlertProvider.AddAlert("Errore in scrittura PLC:" + e.ToString(), null, AlertSeverity.Error);
            }
        }

        private void ReceiveDB()
        {
            int CancelID;
            int[] aE;
            //			ISIFactory.Log.LogEvent("ReceiveDB DB={0}",RDB);
            theGrp.Read(RXhandlesSrv, 1, out CancelID, out aE);
            // some delay for asynch read-complete callback (simplification)
            //Thread.Sleep( 500 );
        }

        private void ReceiveDW1()
        {
            int CancelID;
            int[] aE;
            //				ISIFactory.Log.LogEvent("ReceiveDW1 DB={0}",RDB);
            this.thePollingGrp.Read(this.PollHandles, 2, out CancelID, out aE);
            // some delay for asynch read-complete callback (simplification)
            //Thread.Sleep( 500 );
        }


        private void Analyze_PLC_message()
        {

            TransportResponse tResp = new TransportResponse();
            tResp.Code = (TransportCode)RDBW[2];
            tResp.Response = (TransportResponseType)(0x00ff & RDBW[1]);
            tResp.Status = (TransportStatus)RDBW[4];
            tResp.Silos = (int)RDBW[3];
            tResp.ReportWord1 = (int)RDBW[12];
            tResp.ReportWord2 = (int)RDBW[13];
            tResp.ReportWord3 = (int)RDBW[14];

            OnChanged(tResp);

        }


        private void Analyze_PC_message()
        {

            TransportMsg sMsg = (TransportMsg)MsgQueue.Dequeue();

            for (int i = 0; i < SDBW.Length; SDBW[i++] = 0) ;			//Azzero DB

            SDBW[1] = (int)sMsg.Type;
            SDBW[2] = (int)sMsg.Code;

            //La linea premix vuole il silo di prelievo del supporto
            if (sMsg.Code == TransportCode.PREMIX)
                SDBW[3] = 6;
            else
                SDBW[3] = sMsg.Source;

            SDBW[4] = 0;
            SDBW[5] = 0;
            SDBW[6] = 0;

            SDBW[7] = sMsg.Dest;
            SDBW[8] = 0;
            SDBW[9] = 0;
            SDBW[10] = 0;

            Lotto lRow = uow.GetObjectByKey<Lotto>(sMsg.IDLotto);

            if (lRow != null && lRow.Odl != null)
            {
                Odl odlRow = lRow.Odl;
                CriteriaOperator crit = new BinaryOperator("Odl",odlRow);
                switch (sMsg.Code)
                {
                    case TransportCode.MACINAZIONE:
                        ParametriDosaggioMM1 pDos = uow.FindObject<ParametriDosaggioMM1>(crit);
                        //							if (!drParam.IsMol179Null() && drParam.Mol179)
                        //								SDBW[14] = 0x01;
                        SDBW[14] = pDos.TipoMolino == TipoMolino.MolinoRulli ? 0x02 : 0x01;
                        SDBW[15] = System.Convert.ToInt16(odlRow.Quantità / 1000);			//NrPesate da fare
                        if (SDBW[15] == 0)
                            SDBW[15] = 1;
                        break;
                    case TransportCode.PRESSA2A:
                        ParametriPellet dPC2 = uow.FindObject<ParametriPellet>(crit);
                        if (dPC2 != null)
                        {
                            SDBW[16] = System.Convert.ToInt16(dPC2.Sbriciolatore);
                            SDBW[17] = System.Convert.ToInt16(dPC2.SetaccioPC2);
                            SDBW[18] = System.Convert.ToInt16(dPC2.Talco);
                        }
                        break;
                    case TransportCode.PRESSA1:
                    case TransportCode.PRESSA3A:
                        ParametriPellet dPellet = uow.FindObject<ParametriPellet>(crit);
                        if (dPellet != null)
                        {
                            SDBW[12] = System.Convert.ToInt16(dPellet.LiqA);
                            SDBW[13] = System.Convert.ToInt16(dPellet.LiqB);
                            SDBW[18] = System.Convert.ToInt16(dPellet.Talco);
                            SDBW[14] = System.Convert.ToInt16(dPellet.P1);		//Percentuale miscelazione fiocchi
                        }
                        break;
                    case TransportCode.RINFUSA1:		//Linea rinfusa con setaccio
                    case TransportCode.SACCHI1:		//Non mi sembra che il PLC usi questi parametri per questa linea, però....
                    case TransportCode.SACCHI2:
                    case TransportCode.SACCHI3:
                    case TransportCode.SACCONI:
                        ParametriInsacco pIns = uow.FindObject<ParametriInsacco>(crit);
                        if (pIns != null)
                        {
                            SDBW[12] = System.Convert.ToInt16(pIns.P1);
                            SDBW[13] = System.Convert.ToInt16(pIns.P2);
                            SDBW[14] = System.Convert.ToInt16(pIns.P3);
                            SDBW[15] = System.Convert.ToInt16(pIns.P4);
                            SDBW[16] = System.Convert.ToInt16(pIns.Setaccio);
                            SDBW[17] = System.Convert.ToInt16(pIns.P5);
                            SDBW[18] = System.Convert.ToInt16(pIns.P6);
                        }
                        break;
                    //					case TransportCode.PREMIX:
                    case TransportCode.BUCA1:
                    case TransportCode.BUCA2:
                    case TransportCode.LIQ1:
                    case TransportCode.LIQ2:
                    case TransportCode.PNEUM1:
                    case TransportCode.PNEUM2:
                        ParametriCaricoRinfusa pRinf = uow.FindObject<ParametriCaricoRinfusa>(crit);
                        SDBW[11] = System.Convert.ToInt16(pRinf.CodiceEH);
                        SDBW[12] = System.Convert.ToInt16(pRinf.LiqA);
                        SDBW[13] = System.Convert.ToInt16(pRinf.LiqB);
                        SDBW[16] = System.Convert.ToInt16(pRinf.Prepulitore);
                        SDBW[17] = System.Convert.ToInt16(pRinf.Talco);
                        //							SDBW[18] = System.Convert.ToInt16( drParam.Molino);
                        break;
                }
            }
        }


        private void Handle_Communication()
        {
            switch (receive_status)
            {
                case (RECEIVE_IDLE):
                    if ((RDBW[1] & RICHIESTA_DA_PLC) != 0)	//04/10/04 || (RDBW[1] & RESET_TRASPORTO)!=0)
                    {
                        receive_status = RECEIVE_BUSY;
                        ReceiveDB();		//Read Asincrona!
                        //							Analyze_PLC_message();
                        //							SDBW[1] = SDBW[1] | ACK_DA_PC;
                        //							SendDB();
                    }
                    break;
                case (RECEIVE_BUSY):
                    if ((RDBW[1] & RICHIESTA_DA_PLC) == 0)
                    {
                        receive_status = RECEIVE_IDLE;
                        SDBW[1] = SDBW[1] & ~ACK_DA_PC;
                        SendDB();
                    }
                    break;
                default:
                    if ((RDBW[1] & RICHIESTA_DA_PLC) != 0)
                    {
                        if ((SDBW[1] & ACK_DA_PC) != 0)
                        {
                            receive_status = RECEIVE_BUSY;
                            SDBW[1] = SDBW[1] & ~ACK_DA_PC;
                            SendDB();
                        }
                        else
                        {
                            receive_status = RECEIVE_BUSY;
                            ReceiveDB();		//Read Asincrona!
                        }
                    }
                    else
                    {
                        if ((SDBW[1] & ACK_DA_PC) != 0)
                        {
                            SDBW[1] = SDBW[1] & ~ACK_DA_PC;
                            receive_status = RECEIVE_IDLE;
                            SendDB();
                        }
                        else
                        {
                            receive_status = RECEIVE_IDLE;
                        }
                    }
                    break;
            } /* Switch receive status */

            switch (send_status)
            {
                case (SEND_IDLE):
                    if (MsgQueue.Count > 0)
                    {
                        Analyze_PC_message();
                        SDBW[1] = SDBW[1] | RICHIESTA_DA_PC;
                        send_status = SEND_BUSY;
                        SendDB();
                    } /* Richiesta da PC*/
                    break;
                case (SEND_BUSY):
                    if ((RDBW[1] & ACK_DA_PLC) != 0)
                    {
                        SDBW[1] = SDBW[1] & ~RICHIESTA_DA_PC;
                        send_status = SEND_WAITING;
                        SendDB();
                    } /* Richiesta da  PLC */
                    break;
                case (SEND_WAITING):
                    if ((RDBW[1] & ACK_DA_PLC) == 0)
                    {
                        send_status = SEND_IDLE;
                    } /* send waiting */
                    break;
                default:
                    if ((RDBW[1] & RICHIESTA_DA_PLC) != 0)
                    {
                        if ((SDBW[1] & ACK_DA_PC) != 0)
                        {
                            SDBW[1] = SDBW[1] & ~RICHIESTA_DA_PC;
                            send_status = SEND_WAITING;
                            SendDB();
                        }
                        else
                        {
                            send_status = SEND_BUSY;
                        }
                    }
                    else
                    {
                        if ((SDBW[1] & ACK_DA_PC) != 0)
                        {
                            send_status = SEND_WAITING;
                        }
                        else
                        {
                            send_status = SEND_IDLE;
                        }
                    }
                    break;
            } /* Switch send status */
        }


        private void OnChanged(TransportResponse e)
        {
            throw new NotImplementedException();
        }
    }
}
