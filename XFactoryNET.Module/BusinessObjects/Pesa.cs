using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using System.Linq;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using System.Collections;

namespace XFactoryNET.Module.BusinessObjects
{
    [NavigationItem("Apparati")]
    [MapInheritance(MapInheritanceType.OwnTable)]
    public class Pesa : Apparato
    {
        string fSettings;
        [Size(50)]
        public string Settings
        {
            get { return fSettings; }
            set { SetPropertyValue<string>("Settings", ref fSettings, value); }
        }

        decimal fPesoAttuale;
        [NonPersistent]
        public decimal PesoAttuale
        {
            get { return fPesoAttuale; }
            set { fPesoAttuale = value; }
        }

        bool fPesoStabile;
        [NonPersistent]
        public bool PesoStabile
        {
            get { return fPesoStabile; }
            set { fPesoStabile = value; }
        }
        public Pesa(Session session) : base(session) { }
        public Pesa() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
