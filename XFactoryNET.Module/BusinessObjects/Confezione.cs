using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using System.Linq;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;

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

        decimal fPesoSacco;
        [ModelDefault("DisplayFormat", "n0")][ModelDefault("EditMask","n0")]
        public decimal PesoSacco
        {
            get { return fPesoSacco; }
            set { SetPropertyValue<decimal>("PesoSacco", ref fPesoSacco, value); }
        }

        decimal fCapacità;
        [ModelDefault("DisplayFormat", "n0")][ModelDefault("EditMask","n0")]
        public decimal Capacità
        {
            get { return fCapacità; }
            set { SetPropertyValue<decimal>("Capacità", ref fCapacità, value); }
        }

        public Confezione(Session session) : base(session) { }
        public Confezione() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
