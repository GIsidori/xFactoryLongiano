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
    [NavigationItem("Apparati")]
    [MapInheritance(MapInheritanceType.OwnTable)]
    public class Bilancia : Apparato
    {

        decimal fFondoScala;
        [ModelDefault("DisplayFormat", "n3")][ModelDefault("EditMask","n3")]
        public decimal FondoScala
        {
            get { return fFondoScala; }
            set { SetPropertyValue<decimal>("FondoScala", ref fFondoScala, value); }
        }

        decimal fTaraMax;
        [ModelDefault("DisplayFormat", "n3")][ModelDefault("EditMask","n3")]
        public decimal TaraMax
        {
            get { return fTaraMax; }
            set { SetPropertyValue<decimal>("TaraMax", ref fTaraMax, value); }
        }

        decimal fkMult;
        [ModelDefault("DisplayFormat", "n0")][ModelDefault("EditMask","n0")]
        public decimal kMult
        {
            get { return fkMult; }
            set { SetPropertyValue<decimal>("kMult", ref fkMult, value); }
        }
        int ftMin;
        public int tMin
        {
            get { return ftMin; }
            set { SetPropertyValue<int>("tMin", ref ftMin, value); }
        }
        int ftMax;
        public int tMax
        {
            get { return ftMax; }
            set { SetPropertyValue<int>("tMax", ref ftMax, value); }
        }
        decimal fPrecisione;
        [ModelDefault("DisplayFormat", "n3")][ModelDefault("EditMask","n3")]
        public decimal Precisione
        {
            get { return fPrecisione; }
            set { SetPropertyValue<decimal>("Precisione", ref fPrecisione, value); }
        }
        decimal fMaxVar;
        [ModelDefault("DisplayFormat", "n3")][ModelDefault("EditMask","n3")]
        public decimal MaxVar
        {
            get { return fMaxVar; }
            set { SetPropertyValue<decimal>("MaxVar", ref fMaxVar, value); }
        }

        decimal fMinVar;
        [ModelDefault("DisplayFormat", "n3")][ModelDefault("EditMask","n3")]
        public decimal MinVar
        {
            get { return fMinVar; }
            set { SetPropertyValue<decimal>("MinVar", ref fMinVar, value); }
        }

        decimal fVoloMassimo;
        [ModelDefault("DisplayFormat", "n3")][ModelDefault("EditMask","n3")]
        public decimal VoloMassimo
        {
            get { return fVoloMassimo; }
            set { SetPropertyValue<decimal>("VoloMassimo", ref fVoloMassimo, value); }
        }
        bool fVolumetrica;
        public bool Volumetrica
        {
            get { return fVolumetrica; }
            set { SetPropertyValue<bool>("Volumetrica", ref fVolumetrica, value); }
        }

        public Bilancia(Session session) : base(session) { }
        public Bilancia() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
