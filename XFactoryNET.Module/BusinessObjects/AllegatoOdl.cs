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
    public class AllegatoOdl:BaseXPObject
    {
        public AllegatoOdl(Session session):base(session)
        {

        }


        private Allegato allegato;
        [Association]
        public Allegato Allegato
        {
            get { return allegato; }
            set { SetPropertyValue<Allegato>("Allegato", ref allegato, value); }
        }

        private Odl odl;
        [Association]
        public Odl Odl
        {
            get { return odl; }
            set { SetPropertyValue<Odl>("Odl", ref odl, value); }
        }
    }
}
