using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpo;

namespace XFactoryNET.Module.BusinessObjects
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    public class Fornitore:LookupTable
    {
        public Fornitore(Session session) : base(session) { }
        public Fornitore() : base(Session.DefaultSession) { }


    }
}

