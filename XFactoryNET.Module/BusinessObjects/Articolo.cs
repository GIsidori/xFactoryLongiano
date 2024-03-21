using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;

namespace XFactoryNET.Module.BusinessObjects
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [NavigationItem("Formule e Materiali")]
    public class Articolo : BaseArticolo
    {
        public Articolo(Session session) : base(session) { }
        public Articolo() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }


        TipoMateriale fTipoMateriale;
        public TipoMateriale TipoMateriale
        {
            get { return fTipoMateriale; }
            set { SetPropertyValue<TipoMateriale>("TipoMateriale", ref fTipoMateriale, value); }
        }
        float fTolleranza;
        [ModelDefault("EditMask","n4")]
        [ModelDefault("DisplayFormat","n4")]
        public float Tolleranza
        {
            get { return fTolleranza; }
            set { SetPropertyValue<float>("Tolleranza", ref fTolleranza, value); }
        }

        private float fPesoSpecifico = 1;
        [ModelDefault("EditMask", "n4")]
        [ModelDefault("DisplayFormat", "n4")]
        public float PesoSpecifico
        {
            get
            {
                return fPesoSpecifico;
            }
            set
            {
                SetPropertyValue<float>("PesoSpecifico", ref fPesoSpecifico, value);
            }
        }


        Modalità fModalità;
        public Modalità Modalità
        {
            get { return fModalità; }
            set { SetPropertyValue<Modalità>("Modalità", ref fModalità, value); }
        }
        FormaFisica fFormaFisica;
        public FormaFisica FormaFisica
        {
            get { return fFormaFisica; }
            set { SetPropertyValue<FormaFisica>("FormaFisica", ref fFormaFisica, value); }
        }

        [Association,Browsable(false)]
        public XPCollection<DettaglioAllegato> DettaglioAllegato
        {
            get { return GetCollection<DettaglioAllegato>("DettaglioAllegato"); }
        }

        [Association,Browsable(false)]
        public XPCollection<Lotto> Lotti
        {
            get { return GetCollection<Lotto>("Lotti"); }
        }

        public XPCollection<Lotto> Giacenza
        {
            get
            { return new XPCollection<Lotto>(Lotti, CriteriaOperator.Parse("DataFine IS NULL AND TipoMovimento>=0")); }
        }

        string fNote;
        public string Note
        {
            get { return fNote; }
            set { SetPropertyValue<string>("Note", ref fNote, value); }
        }

        float qtà;
        public float QuantitàPerMiscelata
        {
            get { return qtà; }
            set { SetPropertyValue<float>("QuantitàPerMiscelata", ref qtà, value); }
        }


        [Association,Aggregated]
        public XPCollection<AllegatoArticolo> AllegatiArticolo
        {
            get
            {
                return GetCollection<AllegatoArticolo>("AllegatiArticolo");
            }
        }

        [Association]
        public XPCollection<Formula> Formule
        {
            get { return GetCollection<Formula>("Formule"); }
        }

        private TipoSostituzione tipoSostituzione;
        public TipoSostituzione TipoSostituzione
        {
            get { return tipoSostituzione; }
            set { SetPropertyValue<TipoSostituzione>("TipoSostituzione", ref tipoSostituzione, value); }
        }


    }

    public class AllegatoArticolo : BaseXPObject
    {
        public AllegatoArticolo(Session session)
            : base(session)
        {

        }

        public override void AfterConstruction()
        {
            //this.Count = 1;
            base.AfterConstruction();
        }

        private Allegato allegato;
        [Association]
        public Allegato Allegato
        {
            get { return allegato; }
            set { SetPropertyValue<Allegato>("Allegato", ref allegato, value); }
        }

        private Articolo articolo;
        [Association]
        public Articolo Articolo
        {
            get { return articolo; }
            set { SetPropertyValue<Articolo>("Articolo", ref articolo, value); }
        }

        //private int count;
        //public int Count
        //{
        //    get { return count; }
        //    set { SetPropertyValue<int>("Count", ref count, value); }
        //}

    }

}
