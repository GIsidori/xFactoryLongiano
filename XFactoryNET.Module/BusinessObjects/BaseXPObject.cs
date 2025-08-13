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
    public abstract class BaseXPObject : XPCustomObject
    {
        public BaseXPObject(Session session) : base(session) { }
        public BaseXPObject() : base(Session.DefaultSession) { }
        //
        // Summary:
        //     Gets or set a value which identifies the persistent object.
        [Key(AutoGenerate = true)]
        [Persistent("OID")]
        [System.ComponentModel.Browsable(false)]
        public int Oid { get; set; }
    }
}
