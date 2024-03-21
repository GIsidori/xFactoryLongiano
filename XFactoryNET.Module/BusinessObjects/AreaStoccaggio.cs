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
    public abstract class AreaStoccaggio : BaseXPCustomObject
    {
        public AreaStoccaggio(Session session) : base(session) { }
        public AreaStoccaggio() : base(Session.DefaultSession) { }
        public override void AfterConstruction()
        {
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

        bool fCaricoAbilitato;
        public bool CaricoAbilitato
        {
            get { return fCaricoAbilitato; }
            set { SetPropertyValue<bool>("CaricoAbilitato", ref fCaricoAbilitato, value); }
        }

        bool fScaricoAbilitato;
        public bool ScaricoAbilitato
        {
            get { return fScaricoAbilitato; }
            set { SetPropertyValue<bool>("ScaricoAbilitato", ref fScaricoAbilitato, value); }
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
                var query = from Percorso p in this.Destinazioni select p.ApparatoTo;
                return query.ToList<Apparato>();
            }
        }

        [NonPersistent, Browsable(false)]
        public virtual IList<Apparato> ApparatiPrelievo
        {
            get
            {
                var query = from Percorso p in this.Prelievi select p.ApparatoFrom;
                return query.ToList<Apparato>();
            }
        }


        public abstract void CaricaLotto(Lotto lotto);


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

    }
}
