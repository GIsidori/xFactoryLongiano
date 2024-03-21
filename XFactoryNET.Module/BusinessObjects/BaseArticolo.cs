using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;

namespace XFactoryNET.Module.BusinessObjects
{
    
    [DefaultProperty("Codice")]
    public abstract class BaseArticolo:BaseXPCustomObject
    {
        string fCodice;
        string fDescrizione;

        public BaseArticolo(Session session) : base(session) { }
        public BaseArticolo() : base(Session.DefaultSession) { }

        [Size(50)]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }

        [Key]
        [Size(12)]
        public string Codice
        {
            get { return fCodice; }
            set { SetPropertyValue<string>("Codice", ref fCodice, value); }
        }


        [Association]
        public XPCollection<Classe> Classi
        {
            get { return GetCollection<Classe>("Classi"); }
        }

        [Association, Aggregated]
        public XPCollection<Vincolo> Vincoli
        {
            get { return GetCollection<Vincolo>("Vincoli"); }
        }





    }
}
