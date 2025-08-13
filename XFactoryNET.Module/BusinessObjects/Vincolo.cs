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

        public string Descrizione
        {
            get
            {
                if (this.Incompatibile)
                    return string.Format("Articolo {0} incompatibile con articolo {1}", Articolo1.Descrizione, Articolo2.Descrizione);
                else
                    return string.Format("Impossibile produrre {0} dopo {1} prima di {2} produzion{3} di pulizia", Articolo1.Descrizione, Articolo2.Descrizione, this.NProduzioni,NProduzioni==1?"e":"i");
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
