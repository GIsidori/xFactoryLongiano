using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;

namespace XFactoryNET.Module.BusinessObjects
{
    [NonPersistent]
    [DefaultProperty("Codice")]
    public abstract class LookupTable : BaseXPCustomObject
    {
        string fCodice;

        string fDescrizione;
        public LookupTable(Session session) : base(session) { }
        public LookupTable() : base(Session.DefaultSession) { }
        
        [Key]
        public string Codice
        {
            get { return fCodice; }
            set { SetPropertyValue<string>("Codice", ref fCodice, value); }
        }

        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }



    }
}
