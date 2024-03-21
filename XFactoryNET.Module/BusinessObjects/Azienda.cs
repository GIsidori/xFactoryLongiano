using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpo;

namespace XFactoryNET.Module.BusinessObjects
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    public class Azienda:LookupTable
    {
        public Azienda(Session session) : base(session) { }
        public Azienda() : base(Session.DefaultSession) { }


    }
}

