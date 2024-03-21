using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;

namespace XFactoryNET.Module
{
    [NonPersistent]
    [DomainComponent]
    public class MessageBoxClass
    {
        [ModelDefault("AllowEdit","False")]
        public string Text { get; set; }
    }
}
