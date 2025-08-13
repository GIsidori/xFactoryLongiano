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
    [DefaultProperty("Numero")]
    public class Silos : Apparato
    {

        //private Articolo articolo;
        //public Articolo Articolo
        //{
        //    get { return articolo; }
        //    set { SetPropertyValue<Articolo>("Articolo", ref articolo, value); }
        //}

        public Articolo Articolo
        {
            get { return Lotto == null ? null : Lotto.Articolo; }
        }

        //private Odl odl;
        //public Odl Odl
        //{
        //    get { return odl; }
        //    set { SetPropertyValue<Odl>("Odl", ref odl, value); }
        //}

        private Lotto lotto;
        [Browsable(true)]
        [ModelDefault("AllowEdit","False")]
        public Lotto Lotto
        {
            get { return lotto; }
            set { SetPropertyValue<Lotto>("Lotto", ref lotto, value); }
        }

        int fTipoSilos;
        public int TipoSilos
        {
            get { return fTipoSilos; }
            set { SetPropertyValue<int>("TipoSilos", ref fTipoSilos, value); }
        }
        decimal fCapacit‡;
        [ModelDefault("DisplayFormat", "n0")][ModelDefault("EditMask","n0")]
        public decimal Capacit‡
        {
            get { return fCapacit‡; }
            set { SetPropertyValue<decimal>("Capacit‡", ref fCapacit‡, value); }
        }
        int fVelocit‡Grosso;
        public int Velocit‡Grosso
        {
            get { return fVelocit‡Grosso; }
            set { SetPropertyValue<int>("Velocit‡Grosso", ref fVelocit‡Grosso, value); }
        }
        int fVelocit‡Media;
        public int Velocit‡Media
        {
            get { return fVelocit‡Media; }
            set { SetPropertyValue<int>("Velocit‡Media", ref fVelocit‡Media, value); }
        }
        int fVelocit‡Fine;
        public int Velocit‡Fine
        {
            get { return fVelocit‡Fine; }
            set { SetPropertyValue<int>("Velocit‡Fine", ref fVelocit‡Fine, value); }
        }
        decimal fSogliaGrosso;
        [ModelDefault("DisplayFormat", "n0")][ModelDefault("EditMask","n0")]
        public decimal SogliaGrosso
        {
            get { return fSogliaGrosso; }
            set { SetPropertyValue<decimal>("SogliaGrosso", ref fSogliaGrosso, value); }
        }
        decimal fSogliaFine;
        [ModelDefault("DisplayFormat", "n0")][ModelDefault("EditMask","n0")]
        public decimal SogliaFine
        {
            get { return fSogliaFine; }
            set { SetPropertyValue<decimal>("SogliaFine", ref fSogliaFine, value); }
        }
        decimal fVolo;
        [ModelDefault("DisplayFormat", "n0")][ModelDefault("EditMask","n0")]
        public decimal Volo
        {
            get { return fVolo; }
            set { SetPropertyValue<decimal>("Volo", ref fVolo, value); }
        }
        
        decimal fTolleranza;
        [ModelDefault("DisplayFormat", "n0")][ModelDefault("EditMask","n0")]
        public decimal Tolleranza
        {
            get { return fTolleranza; }
            set { SetPropertyValue<decimal>("Tolleranza", ref fTolleranza, value); }
        }
        DateTime fDataManut;
        public DateTime DataManut
        {
            get { return fDataManut; }
            set { SetPropertyValue<DateTime>("DataManut", ref fDataManut, value); }
        }
        string fSettings;
        [Size(50)]
        public string Settings
        {
            get { return fSettings; }
            set { SetPropertyValue<string>("Settings", ref fSettings, value); }
        }

        [Association("LottiInSilos"),Browsable(false)]
        public XPCollection<Lotto> Lotti
        {
            get
            {
                return GetCollection<Lotto>("Lotti");
            }
        }

        private IList<Lotto> giacenza;
        [Delayed]
        public IList<Lotto> Giacenza
        {
            get
            {
                CriteriaOperator crit = CriteriaOperator.Parse("Silos = ? AND DataFine IS NULL AND TipoMovimento >= 0",this.Codice);
                giacenza = new XPCollection<Lotto>(Session, crit);

                //var query = from l in Lotti where (l.DataFine == null && l.TipoMovimento >= 0) select l;
                //giacenza = query.ToList<Lotto>();
                return giacenza;
            }
        }

        private XPCollection<Odl> odl;
        [Delayed]
        public IList<Odl> Odl
        {
            get 
            {
                CriteriaOperator crit = CriteriaOperator.Parse("[<Lotto>][^.Oid = Odl AND Silos = ? AND TipoMovimento >= 0].Count()>0", this.Codice);
                odl =new XPCollection<Odl>(Session, crit);
                return odl;
            }
        }

        private string elencoAllegati;
        [Delayed]
        public string ElencoAllegati
        {
            get
            {
                if (IsLoading || elencoAllegati == null)
                    elencoAllegati = this.Lotto == null ? string.Empty : Allegato.GetElencoAllegati(this.Lotto.AllegatiLotto.Select<AllegatoLotto,Allegato>(a => a.Allegato));
                return elencoAllegati;
            }
        }


        public void RegistraMovimento(Lotto lotto)
        {
            RegistraMovimento(lotto, lotto.TipoMovimento,lotto.Quantit‡);
        }

        public void RegistraMovimento(Lotto lotto, decimal qt‡)
        {
            RegistraMovimento(lotto, lotto.TipoMovimento,qt‡);
        }

        public void RegistraMovimento(Lotto lotto, TipoMovimento tipoMovimento)
        {
            RegistraMovimento(lotto, tipoMovimento, lotto.Quantit‡);
        }


        public void RegistraMovimento(Lotto lotto,TipoMovimento tipoMovimento,decimal qt‡)
        {
            if (lotto != null)
            {
                lotto.Silos = this;
                if (lotto.DataInizio == System.DateTime.MinValue)
                    lotto.DataInizio = System.DateTime.Now;
                lotto.TipoMovimento = tipoMovimento;
                if (tipoMovimento >=0)
                {
                    this.Lotto = lotto;
                }
                else
                {
                    this.Lotti.Reload();
                    Lotto.UtilizzaLotti(this.Giacenza, lotto);
                    if (this.Quantit‡ < 1)     // svuota se inferiore a 1 kg.
                        this.Svuota();
                }
            }
        }

        public void Svuota()
        {
            foreach (var lot in Giacenza)
            {
                lot.DataFine = System.DateTime.Now;
            }
            this.Lotto = null;
        }


        public Silos(Session session) : base(session) { }
        public Silos() : base(Session.DefaultSession) { }
        public override void AfterConstruction() 
        {
            base.AfterConstruction(); 
        }


        //private decimal quantit‡;
        //[ModelDefault("AllowEdit", "False")]
        //[ModelDefault("DisplayFormat", "n0")]
        //[ModelDefault("EditMask", "n0")]
        //public decimal Quantit‡
        //{
        //    get
        //    {
        //        return quantit‡;
        //    }
        //    set
        //    {
        //        SetPropertyValue<decimal>("Quantit‡", ref quantit‡, value);
        //    }
        //}


        /*
         * UPDATE Silos SET Quantit‡ = 
(SELECT SUM(Lotto.Quantit‡-Lotto.Quantit‡Prelevata) 
FROM            Lotto
WHERE        (Lotto.TipoMovimento >= 0) AND (Lotto.DataFine IS NULL) AND (Lotto.GCRecord IS NULL) AND Lotto.Silos = Silos.Codice)
         * */

        decimal? quantit‡ = null;
        [PersistentAlias("Lotti[TipoMovimento>=0 AND DataFine IS NULL].SUM(Quantit‡-Quantit‡Prelevata)")]
        [ModelDefault("DisplayFormat", "n0")]
        [ModelDefault("AllowEdit","False")]
        public decimal Quantit‡
        {
            get
            {
                quantit‡ = this.Giacenza.Sum(l => l.Quantit‡Residua);
                return quantit‡.Value;
                //var obj = EvaluateAlias("Quantit‡");
                //if (obj != null)
                //    return (decimal)obj;
                //return decimal.Zero;
            }
        }


        //private decimal? quantit‡Disponibile = null;
        //[Browsable(true)]
        //[ModelDefault("DisplayFormat", "n0")]
        //public decimal Quantit‡Disponibile
        //{
        //    get
        //    {
        //        if (quantit‡Disponibile.HasValue == false)
        //            quantit‡Disponibile = this.Giacenza.Sum(l => l.Quantit‡Disponibile);
        //        return quantit‡Disponibile.Value;
        //    }
        //}

        [Association]
        public XPCollection<Gruppo> Gruppi
        {
            get
            {
                return GetCollection<Gruppo>("Gruppi");
            }
        }


    }
}
