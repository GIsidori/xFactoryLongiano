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
    [DefaultProperty("Descrizione")]
    public class Gruppo : BaseXPObject
    {
        string fDescrizione;
        [Size(50)]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }
        public Gruppo(Session session) : base(session) { }
        public Gruppo() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        [Association]
        public XPCollection<Silos> Silos
        {
            get
            {
                return GetCollection<Silos>("Silos");
            }
        }
    }
}
