using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using System.Linq;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.Threading;


namespace XFactoryNET.Module.BusinessObjects
{
    public class AllegatoLotto : BaseXPObject
    {
        public AllegatoLotto(Session session)
            : base(session)
        {

        }


        private Allegato allegato;
        [Association]
        public Allegato Allegato
        {
            get { return allegato; }
            set { SetPropertyValue<Allegato>("Allegato", ref allegato, value); }
        }

        private Lotto lotto;
        [Association]
        public Lotto Lotto
        {
            get { return lotto; }
            set { SetPropertyValue<Lotto>("Lotto", ref lotto, value); }
        }
    }
}
