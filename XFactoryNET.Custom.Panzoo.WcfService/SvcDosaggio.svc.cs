using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using XFactoryNET.Custom.Panzoo.ServiceLibrary;

namespace XFactoryNET.Custom.Panzoo.WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SvcDosaggio" in code, svc and config file together.
    public class SvcDosaggio : ISvcDosaggio
    {

        public void Avvia(int oid)
        {
            ThreadDosaggio.AvviaDosaggio(oid);
        }

        public void Abort()
        {
            ThreadDosaggio.Abort();
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

        public string Silos(int iBil)
        {
            return ThreadDosaggio.Silos(iBil);
        }

        public float QtàEstratta(int iBil)
        {
            return ThreadDosaggio.QtàEstratta(iBil);
        }

        public int NrIngrediente(int iBil)
        {
            return ThreadDosaggio.NrIngrediente(iBil);
        }

        public float QtàTeorica(int iBil)
        {
            return ThreadDosaggio.QtàTeorica(iBil);
        }

        public bool StatoBilancia(int iBil)
        {
            return ThreadDosaggio.StatoBilancia(iBil);
        }


    }
}
