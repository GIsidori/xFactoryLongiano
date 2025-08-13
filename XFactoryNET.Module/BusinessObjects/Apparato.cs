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
    public class Apparato : BaseXPCustomObject
    {
        public Apparato(Session session) : base(session) { }
        public Apparato() : base(Session.DefaultSession) { }
        public override void AfterConstruction() 
        {
            this.CaricoAbilitato = this.ScaricoAbilitato = true;
            base.AfterConstruction();
        }

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

        bool fScaricoAbilitato;

        public bool ScaricoAbilitato
        {
            get { return fScaricoAbilitato; }
            set { SetPropertyValue<bool>("ScaricoAbilitato", ref fScaricoAbilitato, value); }
        }

        bool fCaricoAbilitato;
        public bool CaricoAbilitato
        {
            get { return fCaricoAbilitato; }
            set { SetPropertyValue<bool>("CaricoAbilitato", ref fCaricoAbilitato, value); }
        }


        int fNumero;
        public int Numero
        {
            get { return fNumero; }
            set { SetPropertyValue<int>("Numero", ref fNumero, value); }
        }

        public bool IsValidSource(string lavorazione)
        {
            return this.ScaricoAbilitato && this.Destinazioni.Any<Percorso>(p => p.ApparatoTo != null && p.ApparatoTo.Lavorazione != null && p.ApparatoTo.Lavorazione.Codice == lavorazione);
        }

        public bool IsValidDestination(string lavorazione)
        {
            return this.CaricoAbilitato && this.Prelievi.Any<Percorso>(p => p.ApparatoFrom != null && p.ApparatoFrom.Lavorazione != null && p.ApparatoFrom.Lavorazione.Codice == lavorazione);
        }


        [Association("Destinazioni_Apparato"), Aggregated]
        public XPCollection<Percorso> Destinazioni
        {
            get { return GetCollection<Percorso>("Destinazioni"); }
        }

        [Association("Prelievi_Apparato"), Aggregated]
        public XPCollection<Percorso> Prelievi
        {
            get { return GetCollection<Percorso>("Prelievi"); }
        }

        [NonPersistent, Browsable(false)]
        public virtual IList<Apparato> ApparatiDestinazione
        {
            get
            {
                var query = from Percorso p in this.Destinazioni where p.Abilitato && p.ApparatoTo.CaricoAbilitato select p.ApparatoTo;
                IList<Apparato> list = query.ToList<Apparato>();
                if (list.Count == 0)
                    list = new XPCollection<Silos>(Session, new BinaryOperator("CaricoAbilitato", true)).ToList<Apparato>();
                return list;
            }
        }

        [NonPersistent, Browsable(false)]
        public virtual IList<Apparato> ApparatiPrelievo
        {
            get
            {
                var query = from Percorso p in this.Prelievi where p.Abilitato && p.ApparatoFrom.ScaricoAbilitato select p.ApparatoFrom;
                IList<Apparato> list = query.ToList<Apparato>();
                if (list.Count == 0)
                    list = new XPCollection<Silos>(Session, new BinaryOperator("ScaricoAbilitato", true)).ToList<Apparato>();
                return list;
            }
        }

        private Lavorazione fLavorazione;
        [Association]
        public Lavorazione Lavorazione
        {
            get
            {
                return fLavorazione;
            }
            set
            {
                SetPropertyValue<Lavorazione>("Lavorazione", ref fLavorazione, value);
            }
        }

        private decimal quantit‡Max;
        [ModelDefault("DisplayFormat", "n0")]
        [ModelDefault("EditMask", "n0")]
        public decimal Quantit‡Max
        {
            get { return quantit‡Max; }
            set { SetPropertyValue<decimal>("Quantit‡Max", ref quantit‡Max, value); }
        }


        //[PersistentAlias("Lotti[NOT DataCarico IS NULL AND  DataScarico IS NULL].SUM(Quantit‡) - Lotti[DataCarico IS NULL AND NOT DataScarico IS NULL].SUM(Quantit‡)")]





    }
}
