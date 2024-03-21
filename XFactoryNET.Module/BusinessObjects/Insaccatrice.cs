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
    public class Insaccatrice : Apparato
    {
        public Insaccatrice(Session session) : base(session) { }
        public Insaccatrice() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
