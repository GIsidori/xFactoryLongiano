using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;

namespace XFactoryNET.Module.BusinessObjects
{
    [NavigationItem("Ordini")]
    [DefaultProperty("NumeroCarico")]
    public class OrdineCarico:BaseXPCustomObject
    {
        public OrdineCarico():base(Session.DefaultSession)
        {

        }

        public OrdineCarico(Session session):base(session)
        {

        }

        private Azienda vettore;
        public Azienda Vettore
        {
            get { return vettore; }
            set { SetPropertyValue<Azienda>("Vettore", ref vettore, value); }
        }

        private DateTime dataCarico;
        
        [ModelDefault("DisplayFormat","g")]
        public DateTime DataCarico
        {
            get { return dataCarico; }
            set { SetPropertyValue<DateTime>("DataCarico", ref dataCarico, value); }
        }


        private int numeroCarico;
        [Key]
        [ModelDefault("Format","D")]
        public int NumeroCarico
        {
            get { return numeroCarico; }
            set { SetPropertyValue<int>("NumeroCarico", ref numeroCarico, value); }
        }

        [Association]
        public XPCollection<OrdineProduzione> OrdiniProduzione
        {
            get
            {
                return GetCollection<OrdineProduzione>("OrdiniProduzione");
            }
        }

    }
}
