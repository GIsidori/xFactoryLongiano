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
    public class DettaglioAllegato : BaseXPObject
    {
        Allegato fAllegato;
        [Association,Browsable(false)]
        public Allegato Allegato
        {
            get { return fAllegato; }
            set { SetPropertyValue<Allegato>("Allegato", ref fAllegato, value); }
        }

        Articolo fArticolo;
        [Association,Browsable(false)]
        public Articolo Articolo
        {
            get { return fArticolo; }
            set { SetPropertyValue<Articolo>("Articolo", ref fArticolo, value); }
        }

        Articolo fArticoloIngrediente;
        public Articolo ArticoloIngrediente
        {
            get { return fArticoloIngrediente; }
            set { SetPropertyValue<Articolo>("ArticoloIngrediente", ref fArticoloIngrediente, value); }
        }
        bool fAssoluta;
        public bool Assoluta
        {
            get { return fAssoluta; }
            set { SetPropertyValue<bool>("Assoluta", ref fAssoluta, value); }
        }
        float fValore;
        [ModelDefault("DisplayFormat", "n4")]
        [ModelDefault("EditFormat", "n4")]
        public float Valore
        {
            get { return fValore; }
            set { SetPropertyValue<float>("Valore", ref fValore, value); }
        }

        public DettaglioAllegato(Session session) : base(session) { }
        public DettaglioAllegato() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
