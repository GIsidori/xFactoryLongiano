using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace XFactoryNET.Custom.Panzoo.ServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SvcDosaggio" in both code and config file together.
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class SvcDosaggio : ISvcDosaggio
    {
        public void Start()
        {
            ThreadDosaggio.StartThread();
        }

        public void Stop()
        {
            ThreadDosaggio.StopThread();
        }


        public void Avvia(int oid)
        {
            ThreadDosaggio.AvviaDosaggio(oid);
        }

        public void Arresta()
        {
            ThreadDosaggio.Arresta();
        }

        public void StopMisc(int nrMisc)
        {
            ThreadDosaggio.StopMisc(nrMisc);
        }

        public int IdOdL()
        {
            return ThreadDosaggio.IdOdl();
        }

        public int CurrMisc()
        {
            return ThreadDosaggio.CurrMisc();
        }

        public int NrMisc()
        {
            return ThreadDosaggio.NrMisc();
        }

        public bool Running()
        {
            return ThreadDosaggio.Running();
        }

        public float GetPeso(int iBil)
        {
            return ThreadDosaggio.GetPeso(iBil);
        }

        public float QtàEstratta(int iBil)
        {
            return ThreadDosaggio.QtàEstratta(iBil);
        }

        public int NrIngrediente(int iBil)
        {
            return ThreadDosaggio.NrIngrediente(iBil);
        }

        public bool StatoBilancia(int iBil)
        {
            return ThreadDosaggio.StatoBilancia(iBil);
        }


    }

}
