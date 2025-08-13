using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace XFactoryNET.Custom.MM1.ServiceLibrary
{
    public class SvcTrasporto:ISvcTrasporto
    {


        #region ISvcTrasporto

        public void Avvia(int codice, int source, int dest, int lotto)
        {
            SendAvviaTrasporto(codice, source, dest, lotto);
        }

        public void Arresta(int codice, int source, int dest, int lotto)
        {
            SendArrestaTrasporto(codice, source, lotto);
        }

        public void CambiaSource(int codice, int source, int dest, int lotto)
        {
            SendCambiaSource(codice, source);
        }

        public void CambiaDest(int codice, int source, int dest, int lotto)
        {
            SendCambiaDest(codice, dest);
        }

#endregion

        private ThreadTrasp GetThread(TransportCode Code)
        {
            ThreadTrasp wt;
            switch (Code)
            {
                case TransportCode.BUCA1:
                case TransportCode.BUCA2:
                case TransportCode.RICICLO:
                case TransportCode.MACINAZIONE:
                    {
                        wt = ThreadTrasp.ThreadTrasp1;
                        break;
                    }
                default:
                    {
                        wt = ThreadTrasp.ThreadTrasp2;
                        break;
                    }
            }
            return wt;
        }

        private void SendMessage(TransportMsg msg)
        {
            ThreadTrasp wt = GetThread(msg.Code);

            lock (wt)
            {
                wt.MsgQueue.Enqueue(msg);
                Monitor.Pulse(wt);
            }

        }

        private void SendAvviaTrasporto(int CodiceTrasp, int Source, int Dest, int IDLotto)
        {
            TransportMsg StartMsg = new TransportMsg();
            StartMsg.Type = TransportMsgType.START_TRANSPORT;
            StartMsg.Code = (TransportCode)CodiceTrasp;
            StartMsg.Source = Source;
            StartMsg.Dest = Dest;
            StartMsg.IDLotto = IDLotto;
            SendMessage(StartMsg);
        }

        private void SendArrestaTrasporto(int CodiceTrasporto, int source, int IDLotto)
        {
            TransportMsg StopMsg = new TransportMsg();
            StopMsg.Type = TransportMsgType.STOP_TRANSPORT;
            StopMsg.Code = (TransportCode)CodiceTrasporto;
            StopMsg.Source = source;
            StopMsg.Dest = 0;
            StopMsg.IDLotto = IDLotto;
            SendMessage(StopMsg);
        }

        private void SendCambiaSource(int CodiceTrasporto, int source)
        {
            TransportMsg ChMsg = new TransportMsg();
            ChMsg.Type = TransportMsgType.CHANGE_SOURCE;
            ChMsg.Code = (TransportCode)CodiceTrasporto;
            ChMsg.Source = source;
            ChMsg.Dest = 0;
            SendMessage(ChMsg);
        }


        private void SendCambiaDest(int CodiceTrasporto, int dest)
        {
            TransportMsg ChMsg = new TransportMsg();
            ChMsg.Type = TransportMsgType.CHANGE_DEST;
            ChMsg.Code = (TransportCode)CodiceTrasporto;
            ChMsg.Source = 0;
            ChMsg.Dest = dest;
            SendMessage(ChMsg);
        }


    }
}
