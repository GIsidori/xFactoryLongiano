using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;

namespace XFactoryNET.Custom.GIMA.Module.BusinessObjects
{
    [DeferredDeletion(false)]
    [OptimisticLocking(false)]
    public class TBAllegato:XPCustomObject
    {

        public TBAllegato(Session session)
            : base(session)
        {

        }

        [Key]
        public string Codice { get; set; }

        public string TipoVariazione { get; set; }

    }
}
