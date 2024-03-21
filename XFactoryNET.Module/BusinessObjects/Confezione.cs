using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using System.Linq;
using DevExpress.Data.Filtering;
using System.Collections.Generic;

namespace XFactoryNET.Module.BusinessObjects
{

    public enum TipoConfezione
    {
        Sacchi,         //Sacchi
        Sacconi          //Sacconi, Big bag
    }

    [MapInheritance(MapInheritanceType.OwnTable)]
    public class Confezione : LookupTable
    {
        TipoConfezione fTipoConfezione;
        public TipoConfezione TipoConfezione
        {
            get { return fTipoConfezione; }
            set { SetPropertyValue<TipoConfezione>("TipoConfezione", ref fTipoConfezione, value); }
        }

        float fPesoSacco;
        public float PesoSacco
        {
            get { return fPesoSacco; }
            set { SetPropertyValue<float>("PesoSacco", ref fPesoSacco, value); }
        }

        float fCapacità;
        public float Capacità
        {
            get { return fCapacità; }
            set { SetPropertyValue<float>("Capacità", ref fCapacità, value); }
        }

        public Confezione(Session session) : base(session) { }
        public Confezione() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
