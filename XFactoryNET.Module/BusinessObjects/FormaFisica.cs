using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;

namespace XFactoryNET.Module.BusinessObjects
{
    /// <summary>
    /// Forma fisica dell'articolo (farina, pellet, sbriciolato,...)
    /// </summary>
    [MapInheritance(MapInheritanceType.OwnTable)]
    public class FormaFisica : LookupTable
    {
        public FormaFisica(Session session) : base(session) { }
        public FormaFisica() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
