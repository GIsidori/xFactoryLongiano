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

        float fPesoMax;
        public float PesoMax
        {
            get { return fPesoMax; }
            set { SetPropertyValue<float>("PesoMax", ref fPesoMax, value); }
        }
        float fFondoScala;
        public float FondoScala
        {
            get { return fFondoScala; }
            set { SetPropertyValue<float>("FondoScala", ref fFondoScala, value); }
        }
        float fTaraMax;
        public float TaraMax
        {
            get { return fTaraMax; }
            set { SetPropertyValue<float>("TaraMax", ref fTaraMax, value); }
        }
        float fkMult;
        public float kMult
        {
            get { return fkMult; }
            set { SetPropertyValue<float>("kMult", ref fkMult, value); }
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
        float fPrecisione;
        public float Precisione
        {
            get { return fPrecisione; }
            set { SetPropertyValue<float>("Precisione", ref fPrecisione, value); }
        }
        float fMaxVar;
        public float MaxVar
        {
            get { return fMaxVar; }
            set { SetPropertyValue<float>("MaxVar", ref fMaxVar, value); }
        }
        float fMinVar;
        public float MinVar
        {
            get { return fMinVar; }
            set { SetPropertyValue<float>("MinVar", ref fMinVar, value); }
        }
        float fVoloMassimo;
        public float VoloMassimo
        {
            get { return fVoloMassimo; }
            set { SetPropertyValue<float>("VoloMassimo", ref fVoloMassimo, value); }
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
