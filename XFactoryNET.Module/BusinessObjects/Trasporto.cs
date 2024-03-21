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
    public class Trasporto : Apparato
    {
        string fSettings;
        [Size(50)]
        public string Settings
        {
            get { return fSettings; }
            set { SetPropertyValue<string>("Settings", ref fSettings, value); }
        }
        StatoTrasporto fStatoTrasporto;
        public StatoTrasporto StatoTrasporto
        {
            get { return fStatoTrasporto; }
            set { SetPropertyValue<StatoTrasporto>("Stato", ref fStatoTrasporto, value); }
        }
        public Trasporto(Session session) : base(session) { }
        public Trasporto() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
