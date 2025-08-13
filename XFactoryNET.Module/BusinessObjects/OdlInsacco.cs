using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;


namespace XFactoryNET.Module.BusinessObjects
{
    [NavigationItem("Lavorazioni")]
    [MapInheritance(MapInheritanceType.ParentTable)]
    [CreatableItem]
    public class OdlInsacco : Odl
    {
        public const string IDLavorazione = "Insacco";

        protected override string idLavorazione
        {
            get { return OdlInsacco.IDLavorazione; }
        }


        public OdlInsacco(Session session) : base(session) { }
        public OdlInsacco() : base(Session.DefaultSession) { }

        public override TipoLavorazione TipoLavorazione
        {
            get { return TipoLavorazione.Trasformazione; }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


    }
}
