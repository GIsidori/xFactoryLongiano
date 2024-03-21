using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using System.Linq;
using DevExpress.Data.Filtering;
using System.Collections.Generic;

namespace XFactoryNET.Module.BusinessObjects
{
    [DefaultProperty("Codice")]
    public class Magazzino : BaseXPCustomObject
    {
        public Magazzino(Session session) : base(session) { }
        public Magazzino() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

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

        [Association,Browsable(false)]
        public XPCollection<Lotto> Lotti
        {
            get { return GetCollection<Lotto>("Lotti"); }
        }

        public XPCollection<Lotto> Giacenze
        {
            get { return new XPCollection<Lotto>(Lotti, CriteriaOperator.Parse("TipoMovimento>=0 AND DataFine IS NULL")); }
        }


        public void RegistraMovimento(Lotto lotto)
        {
            if (lotto != null)
            {
                this.Reload();
                lotto.Magazzino = this;
                if (lotto.DataInizio == System.DateTime.MinValue)
                    lotto.DataInizio = System.DateTime.Now;
                if (lotto.TipoMovimento < 0)
                {
                    XPCollection<Lotto> lotti = new XPCollection<Lotto>(this.Lotti, CriteriaOperator.Parse("(Confezione=? OR ? OR Confezione IS NULL) AND Articolo=?", lotto.Confezione,lotto.Confezione == null, lotto.Articolo));
                    Lotto.UtilizzaLotti(lotti, lotto);
                }
            }
        }

    }
}
