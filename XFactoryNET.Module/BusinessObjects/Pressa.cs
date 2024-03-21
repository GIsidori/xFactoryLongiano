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
    public class Pressa : Apparato
    {
        public Pressa(Session session) : base(session) { }
        public Pressa() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
