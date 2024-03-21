using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

using XFactoryNET.Module.BusinessObjects;
using XFactoryNET.Custom.MM1.ServiceLibrary;

namespace XFactoryNET.Custom.MM1.WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Trasporto" in code, svc and config file together.
    public class SvcTrasporto : XFactoryNET.Custom.MM1.ServiceLibrary.ISvcTrasporto
    {

        public void Avvia(int trasporto, int source, int dest, int lotto)
        {
            int codice = GetCodiceTrasporto(trasporto);
            if (codice != 0)
                SendAvviaTrasporto(codice, source, dest, lotto, trasporto);
        }


        public void Arresta(int trasporto, int source, int dest, int lotto)
        {
            int codice = GetCodiceTrasporto(trasporto);
            if (codice != 0)
                SendArrestaTrasporto(codice, source, lotto, trasporto);
        }

        public void CambiaSource(int trasporto, int source, int dest, int lotto)
        {
            int codice = GetCodiceTrasporto(trasporto);
            if (codice != 0)
                SendCambiaSource(codice, source, trasporto);
        }

        public void CambiaDest(int trasporto, int source, int dest, int lotto)
        {
            int codice = GetCodiceTrasporto(trasporto);
            if (codice != 0)
                SendCambiaDest(codice, dest, trasporto);
        }

        private int GetCodiceTrasporto(int oidTrasporto)
        {
            int codice = 0;
            using (DevExpress.Xpo.Session session = new DevExpress.Xpo.Session())
            {
                Trasporto trasp = session.GetObjectByKey<Trasporto>(oidTrasporto);
                codice = int.Parse(trasp.Settings);
            }
            return codice;
        }

        private ThreadTrasporti GetThread(TransportCode Code)
        {
            ThreadTrasporti wt;
            switch (Code)
            {
                case TransportCode.BUCA1:
                case TransportCode.BUCA2:
                case TransportCode.RICICLO:
                case TransportCode.MACINAZIONE:
                    {
                        wt = ThreadTrasporti.threadTrasps[0];
                        break;
                    }
                default:
                    {
                        wt = ThreadTrasporti.threadTrasps[1];
                        break;
                    }
            }
            return wt;
        }

        private void SendMessage(TransportMsg msg)
        {
            ThreadTrasporti wt = GetThread(msg.Code);

            lock (wt)
            {
                wt.MsgQueue.Enqueue(msg);
                Monitor.Pulse(wt);
            }

        }

        private void SendAvviaTrasporto(int CodiceTrasp, int Source, int Dest, int IDLotto, int trasporto)
        {
            TransportMsg StartMsg = new TransportMsg();
            StartMsg.Type = TransportMsgType.START_TRANSPORT;
            StartMsg.Code = (TransportCode)CodiceTrasp;
            StartMsg.Source = Source;
            StartMsg.Dest = Dest;
            StartMsg.IDLotto = IDLotto;
            StartMsg.IDTrasporto = trasporto;
            SendMessage(StartMsg);
        }

        private void SendArrestaTrasporto(int CodiceTrasporto, int source, int IDLotto, int IDTrasporto)
        {
            TransportMsg StopMsg = new TransportMsg();
            StopMsg.Type = TransportMsgType.STOP_TRANSPORT;
            StopMsg.Code = (TransportCode)CodiceTrasporto;
            StopMsg.Source = source;
            StopMsg.Dest = 0;
            StopMsg.IDLotto = IDLotto;
            StopMsg.IDTrasporto = IDTrasporto;
            SendMessage(StopMsg);
        }


        private void SendCambiaSource(int CodiceTrasporto, int source, int IDTrasporto)
        {
            TransportMsg ChMsg = new TransportMsg();
            ChMsg.Type = TransportMsgType.CHANGE_SOURCE;
            ChMsg.Code = (TransportCode)CodiceTrasporto;
            ChMsg.Source = source;
            ChMsg.Dest = 0;
            ChMsg.IDTrasporto = IDTrasporto;
            SendMessage(ChMsg);
        }


        private void SendCambiaDest(int CodiceTrasporto, int dest, int IDTrasporto)
        {
            TransportMsg ChMsg = new TransportMsg();
            ChMsg.Type = TransportMsgType.CHANGE_DEST;
            ChMsg.Code = (TransportCode)CodiceTrasporto;
            ChMsg.Source = 0;
            ChMsg.Dest = dest;
            ChMsg.IDTrasporto = IDTrasporto;
            SendMessage(ChMsg);
        }

    }
}
