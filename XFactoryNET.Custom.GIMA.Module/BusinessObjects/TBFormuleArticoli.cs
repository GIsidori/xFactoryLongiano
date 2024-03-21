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
    public class TBFormuleArticoli : XPObject
    {
        public TBFormuleArticoli(Session session)
            : base(session)
        {

        }

        public string Articolo { get; set; }

        public string Formula { get; set; }


    }
}
