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
    [MapInheritance(MapInheritanceType.OwnTable)]
    public class TipoConfezione : LookupTable
    {
        public TipoConfezione(Session session) : base(session) { }
        public TipoConfezione() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
    
    //public enum TipoConfezione
    //{
    //    Rinfusa = 'R',
    //    Sacchi = 'S',
    //    BigBag = 'B'
    //}
}
