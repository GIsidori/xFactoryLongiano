using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace XFactoryNET.Module.BusinessObjects
{
    [MapInheritance(MapInheritanceType.ParentTable)]
    public class OdlTrasferimento:Odl
    {
        public OdlTrasferimento(Session s):base(s)
        {

        }

        public OdlTrasferimento()
        {

        }


        public const string IDLavorazione = "Trasferimento";
        protected override string idLavorazione
        {
            get { return IDLavorazione; }
        }

        public override void AfterConstruction()
        {
            this.NumeroMiscelate = 1;
            base.AfterConstruction();
        }

        public override TipoLavorazione TipoLavorazione
        {
            get
            {
                return TipoLavorazione.Trasformazione;
            }
        }

    }
}
