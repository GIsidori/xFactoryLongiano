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
    public class Volo : XPLiteObject
    {
        int fIDVolo;
        [Key(true)]
        public int IDVolo
        {
            get { return fIDVolo; }
            set { SetPropertyValue<int>("IDVolo", ref fIDVolo, value); }
        }
        Articolo fIDArticolo;
        public Articolo IDArticolo
        {
            get { return fIDArticolo; }
            set { SetPropertyValue<Articolo>("Articolo", ref fIDArticolo, value); }
        }
        Silos fIDSilos;
        [Size(12)]
        public Silos IDSilos
        {
            get { return fIDSilos; }
            set { SetPropertyValue<Silos>("Silos", ref fIDSilos, value); }
        }
        float fVolo_;
        [Persistent(@"Volo")]
        public float Volo_
        {
            get { return fVolo_; }
            set { SetPropertyValue<float>("Volo_", ref fVolo_, value); }
        }
        public Volo(Session session) : base(session) { }
        public Volo() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
