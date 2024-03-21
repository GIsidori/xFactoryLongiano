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
        float fCapacit‡;
        public float Capacit‡
        {
            get { return fCapacit‡; }
            set { SetPropertyValue<float>("Capacit‡", ref fCapacit‡, value); }
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
        float fSogliaGrosso;
        public float SogliaGrosso
        {
            get { return fSogliaGrosso; }
            set { SetPropertyValue<float>("SogliaGrosso", ref fSogliaGrosso, value); }
        }
        float fSogliaFine;
        public float SogliaFine
        {
            get { return fSogliaFine; }
            set { SetPropertyValue<float>("SogliaFine", ref fSogliaFine, value); }
        }
        float fVolo;
        public float Volo
        {
            get { return fVolo; }
            set { SetPropertyValue<float>("Volo", ref fVolo, value); }
        }
        float fTolleranza;
        public float Tolleranza
        {
            get { return fTolleranza; }
            set { SetPropertyValue<float>("Tolleranza", ref fTolleranza, value); }
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

        [Association("LottiInSilos")]
        public XPCollection<Lotto> Lotti
        {
            get
            {
                return GetCollection<Lotto>("Lotti");
            }
        }

        public XPCollection<Lotto> Giacenza
        {
            get
            { return new XPCollection<Lotto>(Lotti, CriteriaOperator.Parse("DataFine IS NULL AND TipoMovimento>=0")); }
        }

        public string ElencoAllegati
        {
            get
            {
                return this.Lotto == null ? string.Empty : Allegato.GetElencoAllegati(this.Lotto.AllegatiLotto.Select<AllegatoLotto,Allegato>(a => a.Allegato));
            }
        }


        public void RegistraMovimento(Lotto lotto)
        {
            RegistraMovimento(lotto, lotto.TipoMovimento,lotto.Quantit‡);
        }

        public void RegistraMovimento(Lotto lotto, float qt‡)
        {
            RegistraMovimento(lotto, lotto.TipoMovimento,qt‡);
        }

        public void RegistraMovimento(Lotto lotto, TipoMovimento tipoMovimento)
        {
            RegistraMovimento(lotto, tipoMovimento, lotto.Quantit‡);
        }


        public void RegistraMovimento(Lotto lotto,TipoMovimento tipoMovimento,float qt‡)
        {
            if (lotto != null)
            {
                this.Reload();
                lotto.Silos = this;
                if (lotto.DataInizio == System.DateTime.MinValue)
                    lotto.DataInizio = System.DateTime.Now;
                lotto.TipoMovimento = tipoMovimento;
                if (tipoMovimento >=0)
                {
                    this.Quantit‡ += qt‡;
                    this.Lotto = lotto;
                }
                else
                {
                    Lotto.UtilizzaLotti(this.Lotti,lotto);
                    this.Quantit‡ -= qt‡;
                    if (this.Quantit‡ <= 0)
                        this.Svuota();
                }
            }

        }

        public void Svuota()
        {
            foreach (var lot in Lotti)
            {
                lot.DataFine = System.DateTime.Now;
            }
            this.Quantit‡ = 0;
            this.Lotto = null;
        }


        public Silos(Session session) : base(session) { }
        public Silos() : base(Session.DefaultSession) { }
        public override void AfterConstruction() 
        {
            base.AfterConstruction(); 
        }


        private float quantit‡;
        [ModelDefault("AllowEdit", "False")]
        public float Quantit‡
        {
            get
            {
                return quantit‡;
            }
            set { SetPropertyValue<float>("Quantit‡", ref quantit‡, value); }
        }


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
