using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;


namespace XFactoryNET.Module.BusinessObjects
{
    public class Vincolo:BaseXPObject
    {
        public Vincolo()
        {

        }

        public Vincolo(Session session):base(session)
        {

        }

        private BaseArticolo articolo1;
        [Association]
        public BaseArticolo Articolo1
        {
            get
            {
                return articolo1;
            }
            set
            {
                SetPropertyValue<BaseArticolo>("Articolo1",ref articolo1, value);
            }
        }

        private BaseArticolo articolo2;
        public BaseArticolo Articolo2
        {
            get
            {
                return articolo2;
            }
            set
            {
                SetPropertyValue<BaseArticolo>("Articolo2", ref articolo2, value);
            }
        }

        private int nProduzioni;
        public int NProduzioni
        {
            get { return nProduzioni; }
            set { SetPropertyValue<int>("NProduzioni", ref nProduzioni, value); }
        }

        private bool incompatibile;
        public bool Incompatibile
        {
            get { return incompatibile; }
            set { SetPropertyValue<bool>("Incompatibile", ref incompatibile, value); }
        }


    }
}
