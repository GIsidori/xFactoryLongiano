using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using XFactoryNET.Module.BusinessObjects;

namespace XFactoryNET.Custom.GIMA.Module.BusinessObjects
{
    [DeferredDeletion(false)]
    [OptimisticLocking(false)]
    public class TBOrdini : XPObject
    {
        public TBOrdini(Session session):base(session)
        {

        }

        private int numeroOrdine;
        public int NumeroOrdine
        {
            get { return numeroOrdine; }
            set { SetPropertyValue<int>("NumeroOrdine", ref numeroOrdine, value); }
        }

        private string formula;
        public string Formula
        {
            get { return formula; }
            set { SetPropertyValue<string>("Formula", ref formula, value); }
        }

        private float qtà;
        public float Quantità
        {
            get { return qtà; }
            set { SetPropertyValue<float>("Quantità", ref qtà, value); }
        }



        private DateTime dataOrdine;
        public DateTime DataOrdine
        {
            get { return dataOrdine; }
            set { SetPropertyValue<DateTime>("DataOrdine", ref dataOrdine, value); }
        }

        private string cliente;
        public string Cliente
        {
            get { return cliente; }
            set { SetPropertyValue<string>("Cliente", ref cliente, value); }
        }

        private string descrizioneCliente;
        public string DescrizioneCliente
        {
            get { return descrizioneCliente; }
            set { SetPropertyValue<string>("DescerizioneCliente", ref descrizioneCliente, value); }
        }




    }
}
