using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace XFactoryNET.Module.BusinessObjects
{
    /// <summary>
    /// Ciclo di lavorazione per la produzione dell'articolo
    /// </summary>
    /// <remarks>Solo per Prodotti Finiti e Semilavorati</remarks>
    [MapInheritance(MapInheritanceType.OwnTable)]
    public class Lavorazione : LookupTable
    {
        public Lavorazione(Session session) : base(session) { }
        public Lavorazione() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        [Association]
        public XPCollection<Apparato> Apparato
        {
            get { return GetCollection<Apparato>("Apparato"); }
        }

        private int turnoCorrente;
        public int TurnoCorrente
        {
            get { return turnoCorrente; }
            set { SetPropertyValue<int>("TurnoCorrente", ref turnoCorrente, value); }
        }


        //private int currentOdl;
        //public int CurrentOdl
        //{
        //    get { return currentOdl; }
        //    set { SetPropertyValue<int>("CurrentOdl", ref currentOdl, value); }
        //}
    }
}
