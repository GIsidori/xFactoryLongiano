using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;

namespace XFactoryNET.Module.BusinessObjects
{
    [MapInheritance(MapInheritanceType.ParentTable)]
    public class ComponenteOdl : Componente
    {

        public ComponenteOdl(Session session) : base(session) { }
        public ComponenteOdl() : base(Session.DefaultSession) { }

        private Odl fOdl;
        [Association,Browsable(false)]
        public Odl Odl
        {
            get { return fOdl;}
            set { SetPropertyValue<Odl>("Odl", ref fOdl, value); }
        }

        private Formula fSostituzione;
        [NonPersistent]
        [DataSourceProperty("Articolo.Formule")]
        public Formula Sostituzione
        {
            get { return fSostituzione; }
            set
            {
                if (fSostituzione != value)
                {
                    fSostituzione = value;
                    this.Odl.SostituisciComponente(this, fSostituzione);
                }
            }
        }

        private Lotto fLotto;
        public Lotto Lotto
        {
            get
            {
                return fLotto;
            }
            set
            {
                SetPropertyValue<Lotto>("Lotto", ref fLotto, value);
            }
        }


    }
}
