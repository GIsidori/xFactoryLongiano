using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;

namespace XFactoryNET.Custom.MM1.Module.BusinessObjects
{
    public struct QComponenteRow
    {
        [Persistent("Formula"), Browsable(false)]
        [Association]
        public QFormula Formula;
        [Persistent("Articolo"), Browsable(false)]
        [Association("ArticoloComponente")]
        public QArticolo Articolo;
    }

}
