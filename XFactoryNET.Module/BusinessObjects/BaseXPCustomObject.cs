using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace XFactoryNET.Module.BusinessObjects
{
    [NonPersistent]
    [OptimisticLocking(Enabled = false)]
    public abstract class BaseXPCustomObject : XPCustomObject
    {
        public BaseXPCustomObject(Session session) : base(session) { }
        public BaseXPCustomObject() : base(Session.DefaultSession) { }
            

    }
}
