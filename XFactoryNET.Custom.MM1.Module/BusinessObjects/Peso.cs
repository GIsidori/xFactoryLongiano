using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace XFactoryNET.Custom.MM1.Module.BusinessObjects
{
    [NonPersistent]
    class Peso:XPLiteObject
    {
        public Peso(Session session):base(session)
        {

        }

        public float PesoLetto { get; set; }
        public bool PesoStabile { get; set; }

    }
}
