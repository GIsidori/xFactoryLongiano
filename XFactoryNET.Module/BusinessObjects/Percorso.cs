using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using System.Linq;
using DevExpress.Data.Filtering;
using System.Collections.Generic;

namespace XFactoryNET.Module.BusinessObjects
{
    public class Percorso : BaseXPObject
    {
        Modalità fModalità;
        [Size(12)]
        public Modalità Modalità
        {
            get { return fModalità; }
            set { SetPropertyValue<Modalità>("Modalità", ref fModalità, value); }
        }
        Apparato fApparatoFrom;
        [Association("Destinazioni_Apparato"), Browsable(true)]
        public Apparato ApparatoFrom
        {
            get { return fApparatoFrom; }
            set { SetPropertyValue<Apparato>("ApparatoFrom", ref fApparatoFrom, value); }
        }

        Apparato fApparatoTo;
        [Association("Prelievi_Apparato"), Browsable(true)]
        public Apparato ApparatoTo
        {
            get { return fApparatoTo; }
            set { SetPropertyValue<Apparato>("ApparatoTo", ref fApparatoTo, value); }
        }

        Trasporto fTrasporto;
        public Trasporto Trasporto
        {
            get { return fTrasporto; }
            set { SetPropertyValue<Trasporto>("Trasporto", ref fTrasporto, value); }
        }
        bool fAbilitato;
        public bool Abilitato
        {
            get { return fAbilitato; }
            set { SetPropertyValue<bool>("Valido", ref fAbilitato, value); }
        }
        public Percorso(Session session) : base(session) { }
        public Percorso() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
