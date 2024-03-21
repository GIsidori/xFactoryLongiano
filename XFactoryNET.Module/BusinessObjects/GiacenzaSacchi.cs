using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace XFactoryNET.Module.BusinessObjects
{
    public class GiacenzaSacchi:BaseXPObject
    {
        public GiacenzaSacchi()
        {

        }
        public GiacenzaSacchi(Session session):base(session)
        {

        }

        private Magazzino magazzino;
        [Association]
        public Magazzino Magazzino
        {
            get { return magazzino; }
            set { SetPropertyValue<Magazzino>("Magazzino", ref magazzino, value); }
        }

        private Articolo articolo;
        [Association]
        public Articolo Articolo
        {
            get { return articolo; }
            set { SetPropertyValue<Articolo>("Articolo", ref articolo, value); }
        }

        private Confezione confezione;
        [Association]
        public Confezione Confezione
        {
            get { return confezione; }
            set { SetPropertyValue<Confezione>("Confezione", ref confezione, value); }
        }

        public IList<Lotto> Lotti
        {
            get
            {
                return new XPCollection<Lotto>(this.Session, CriteriaOperator.Parse("Magazzino = ? AND (Confezione = ? OR (Confezione IS NULL AND ?=1)) AND Articolo = ?", Magazzino, Confezione,Confezione == null ? 1 : 0, Articolo));
            }
        }


        private float quantità;
        public float Quantità
        {
            get { return quantità; }
            set { SetPropertyValue<float>("Quantità", ref quantità, value); }
        }

        public int NumeroSacchi
        {
            get
            {
                if (Confezione != null)
                {
                    float pesoSacco = 0;      
                    if (Confezione.TipoConfezione == TipoConfezione.Sacchi && Confezione.PesoSacco != 0)
                        pesoSacco = Confezione.PesoSacco;
                    if (Confezione.TipoConfezione == TipoConfezione.Sacconi && Confezione.Capacità != 0 && Articolo != null)
                    {
                        if (Articolo.PesoSpecifico != 0)        //Capacità in mc (metri cubi)
                            pesoSacco = 1000* Confezione.Capacità / Articolo.PesoSpecifico;
                        else
                            pesoSacco = 1000 * Confezione.Capacità;        //Se non è impostato il peso specifico del materiale, allora prendere pesoSpecifico=1
                    }
                    if (pesoSacco != 0)
                        return (int)Math.Floor(Quantità / pesoSacco);
                }
                return 0;
            }
        }
    
    }
}
