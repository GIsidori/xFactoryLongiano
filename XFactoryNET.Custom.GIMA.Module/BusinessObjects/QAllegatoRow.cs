using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;

namespace XFactoryNET.Custom.GIMA.Module.BusinessObjects
{
    public struct QAllegatoRow
    {
        [Persistent("Allegato"), Browsable(false)]
        //[Association]
        public QAllegato Allegato;
        [Persistent("Articolo"), Browsable(false)]
        //[Association("ArticoloAllegato")]
        public QArticolo Articolo;

    }
}
