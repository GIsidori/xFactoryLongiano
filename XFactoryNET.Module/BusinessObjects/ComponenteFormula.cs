using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;

namespace XFactoryNET.Module.BusinessObjects
{
    [MapInheritance(MapInheritanceType.ParentTable)]
    public class ComponenteFormula : Componente
    {
        public ComponenteFormula()
        {

        }

        public ComponenteFormula(Session session):base(session)
        {

        }
        Formula fFormula;
        [Association,Browsable(false)]
        public Formula Formula
        {
            get { return fFormula; }
            set { SetPropertyValue<Formula>("Formula", ref fFormula, value); }
        }
    }
}
