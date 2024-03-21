using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;


namespace XFactoryNET.Module.BusinessObjects
{
    [NavigationItem("Lavorazioni")]
    [MapInheritance(MapInheritanceType.ParentTable)]
    [CreatableItem]
    public class OdlUscitaSacchi : Odl
    {
        public const string IDLavorazione = "SpedizSac";
        protected override string idLavorazione
        {
            get { return IDLavorazione; }
        }

        public OdlUscitaSacchi(Session session) : base(session) { }
        public OdlUscitaSacchi() : base(Session.DefaultSession) { }

        public override void AfterConstruction()
        {
            this.NumeroMiscelate = 1;
            base.AfterConstruction();
        }

        public override TipoLavorazione TipoLavorazione
        {
            get
            {
                return TipoLavorazione.UscitaMerci;
            }
        }


    }

}
