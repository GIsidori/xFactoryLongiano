using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;

namespace XFactoryNET.Module.BusinessObjects
{
    [DefaultProperty("NumeroOrdine")]
    [NavigationItem("Ordini")]
    public class OrdineCliente : BaseXPCustomObject
    {

        public OrdineCliente()
            : base(Session.DefaultSession)
        {

        }

        public OrdineCliente(Session session)
            : base(session)
        {

        }


        private DateTime dataOrdine;
        public DateTime DataOrdine
        {
            get { return dataOrdine; }
            set { SetPropertyValue<DateTime>("DataOrdine", ref dataOrdine, value); }
        }

        private int numeroOrdine;
        [Key]
        [ModelDefault("Format", "D")]
        public int NumeroOrdine
        {
            get { return numeroOrdine; }
            set { SetPropertyValue<int>("NumeroOrdine", ref numeroOrdine, value); }
        }

        private Azienda cliente;
        public Azienda Cliente
        {
            get
            {
                return cliente;
            }
            set
            {
                SetPropertyValue<Azienda>("Cliente", ref cliente, value);
            }
        }

        [Association, Aggregated]
        public XPCollection<OrdineProduzione> OrdiniProduzione
        {
            get
            {
                return GetCollection<OrdineProduzione>("OrdiniProduzione");
            }
        }

    }
}
