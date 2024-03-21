using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.Persistent.Validation;


namespace XFactoryNET.Module.BusinessObjects
{
    [NavigationItem("Lavorazioni")]
    [MapInheritance(MapInheritanceType.ParentTable)]
    [CreatableItem]
    public class OdlDosaggio : Odl
    {
        public const string IDLavorazione = "Dosaggio";

        protected override string idLavorazione
        {
            get { return IDLavorazione; }
        }

        public OdlDosaggio(Session session) : base(session) { }
        public OdlDosaggio() : base(Session.DefaultSession) { }

        public override void AfterConstruction()
        {
            //if (this.Lavorazione == null)
            //{
            //    Lavorazione = Session.GetObjectByKey<Lavorazione>(OdlDosaggio.IDLavorazione);
            //    if (Lavorazione == null)
            //        Lavorazione = new Lavorazione(Session) { Codice = OdlDosaggio.IDLavorazione };
            //    Lavorazione.Descrizione = "Dosaggio e miscelazione";
            //}
            base.AfterConstruction();
        }

        public override TipoLavorazione TipoLavorazione
        {
            get { return TipoLavorazione.Trasformazione; }
        }




        //[ Browsable(false)]
        //public new IList<Apparato> ApparatiDestinazione
        //{
        //    get
        //    {
        //        return base.ApparatiDestinazione.Cast<Apparato>().ToList<Apparato>();
        //    }
        //}

        //[ Browsable(false)]
        //public new IList<Apparato> ApparatiPrelievo
        //{
        //    get
        //    {

        //        return base.ApparatiPrelievo.Cast<Apparato>().ToList<Apparato>();
        //    }
        //}

    }

}
