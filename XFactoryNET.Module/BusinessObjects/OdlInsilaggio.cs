using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

namespace XFactoryNET.Module.BusinessObjects
{
    [NavigationItem("Lavorazioni")]
    [MapInheritance(MapInheritanceType.ParentTable)]
    [CreatableItem]
    public class OdlInsilaggio : Odl
    {
        public OdlInsilaggio()
        {

        }

        public OdlInsilaggio(Session session):base(session)
        {

        }

        public const string IDLavorazione = "Insilaggio";

        protected override string idLavorazione
        {
            get { return OdlInsacco.IDLavorazione; }
        }

        public override TipoLavorazione TipoLavorazione
        {
            get { return TipoLavorazione.Trasformazione; }
        }


    }
}
