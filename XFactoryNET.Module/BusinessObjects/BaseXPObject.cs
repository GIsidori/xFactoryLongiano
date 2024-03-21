using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;

namespace XFactoryNET.Module.BusinessObjects
{
//    [DeferredDeletion(false)]
    [NonPersistent]
    [OptimisticLocking(Enabled=false)]
    public abstract class BaseXPObject : XPObject
    {
        public BaseXPObject(Session session) : base(session) { }
        public BaseXPObject() : base(Session.DefaultSession) { }

        [System.ComponentModel.Browsable(false)]
        public new int Oid
        {
            get { return base.Oid; }
            set { base.Oid = value; }
        }

        //protected override void OnDeleted()
        //{
        //    //Emula il comportamento di DELETE RULE: CASCADE 
        //    var list = Session.CollectReferencingObjects(this);
        //    foreach (var item in list)
        //    {
        //        DevExpress.Xpo.XPBaseObject obj = (DevExpress.Xpo.XPBaseObject)item;
        //        if (obj.IsDeleted == false)
        //            obj.Delete();
        //    }
        //    base.OnDeleted();
        //}

    }
}
