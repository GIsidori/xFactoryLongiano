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

    public class Ingrediente : BaseXPObject
    {


        Lotto fLotto;
        [Association("Ingredienti")]
        public Lotto Lotto
        {
            get { return fLotto; }
            set { SetPropertyValue<Lotto>("Lotto", ref fLotto, value); }
        }
        Lotto fLottoIngr;
        [Association("Prodotti")]
        public Lotto LottoIngr
        {
            get { return fLottoIngr; }
            set { SetPropertyValue<Lotto>("LottoIngr", ref fLottoIngr, value); }
        }
        float fQt‡;
        [ModelDefault("DisplayFormat", "n4")]
        [ModelDefault("EditFormat", "n4")]
        public float Qt‡
        {
            get { return fQt‡; }
            set { SetPropertyValue<float>("Qt‡", ref fQt‡, value); }
        }
        float fQt‡Teo;
        [ModelDefault("DisplayFormat", "n4")]
        [ModelDefault("EditFormat", "n4")]
        public float Qt‡Teo
        {
            get { return fQt‡Teo; }
            set { SetPropertyValue<float>("Qt‡Teo", ref fQt‡Teo, value); }
        }
        float fTolleranza;
        [ModelDefault("DisplayFormat", "n4")]
        [ModelDefault("EditFormat", "n4")]
        public float Tolleranza
        {
            get { return fTolleranza; }
            set { SetPropertyValue<float>("Tolleranza", ref fTolleranza, value); }
        }
        float fPerc;
        [ModelDefault("DisplayFormat", "p4")]
        [ModelDefault("EditFormat", "p4")]
        public float Perc
        {
            get { return fPerc; }
            set { SetPropertyValue<float>("Perc", ref fPerc, value); }
        }
        int fNrIngr;
        public int NrIngr
        {
            get { return fNrIngr; }
            set { SetPropertyValue<int>("NrIngr", ref fNrIngr, value); }
        }
        Modalit‡ fModalit‡;
        [Size(12)]
        public Modalit‡ Modalit‡
        {
            get { return fModalit‡; }
            set { SetPropertyValue<Modalit‡>("Modalit‡", ref fModalit‡, value); }
        }

        AreaStoccaggio fPrelievo;
        public AreaStoccaggio Prelievo
        {
            get { return fPrelievo; }
            set { SetPropertyValue<AreaStoccaggio>("Prelievo", ref fPrelievo, value); }
        }


        AreaStoccaggio fDestinazione;
        public AreaStoccaggio Destinazione
        {
            get { return fDestinazione; }
            set { SetPropertyValue<AreaStoccaggio>("Destinazione", ref fDestinazione, value); }
        }

        StatoIngrediente fStato;
        public StatoIngrediente Stato
        {
            get { return fStato; }
            set { SetPropertyValue<StatoIngrediente>("Stato", ref fStato, value); }
        }
        Componente fComponente;
        public Componente Componente
        {
            get { return fComponente; }
            set { SetPropertyValue<Componente>("Componente", ref fComponente, value); }
        }
        public Ingrediente(Session session) : base(session) { }
        public Ingrediente() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
