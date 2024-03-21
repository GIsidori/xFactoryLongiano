using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace XFactoryNET.Module.BusinessObjects
{
    /// <summary>
    /// Modalità di utilizzo in produzione
    /// </summary>
    [MapInheritance(MapInheritanceType.OwnTable)]
    public class Modalità : LookupTable
    {
        bool fSacchi;
        /// <summary>
        /// Modalità di prelievo a sacchi
        /// </summary>
        public bool Sacchi
        {
            get { return fSacchi; }
            set { SetPropertyValue<bool>("Sacchi", ref fSacchi, value); }
        }
        bool fTeorico;
        /// <summary>
        /// </summary>
        public bool Teorico
        {
            get { return fTeorico; }
            set { SetPropertyValue<bool>("Teorico", ref fTeorico, value); }
        }
        public Modalità(Session session) : base(session) { }
        public Modalità() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
