using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using System.Linq;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;

namespace XFactoryNET.Module.BusinessObjects
{
    [DefaultProperty("Codice")]
    [NavigationItem("Apparati")]
    public class Trasporto : BaseXPCustomObject
    {
        string fCodice;
        [Key]
        [Size(12)]
        public string Codice
        {
            get { return fCodice; }
            set { SetPropertyValue<string>("Codice", ref fCodice, value); }
        }


        string fDescrizione;
        [Size(50)]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }

        string fSettings;
        [Size(50)]
        public string Settings
        {
            get { return fSettings; }
            set { SetPropertyValue<string>("Settings", ref fSettings, value); }
        }
        StatoTrasporto fStatoTrasporto;
        public StatoTrasporto StatoTrasporto
        {
            get { return fStatoTrasporto; }
            set { SetPropertyValue<StatoTrasporto>("Stato", ref fStatoTrasporto, value); }
        }

        [Association]
        public XPCollection<Percorso> Percorsi
        {
            get { return GetCollection<Percorso>("Percorsi"); }
        }


        public Trasporto(Session session) : base(session) { }
        public Trasporto() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
