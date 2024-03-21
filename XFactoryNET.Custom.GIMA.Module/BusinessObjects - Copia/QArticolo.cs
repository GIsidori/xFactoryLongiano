using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Collections;

namespace XFactoryNET.Custom.GIMA.Module.BusinessObjects
{
    [DefaultProperty("Codice")]
    public class QArticolo : XPLiteObject
    {
        public QArticolo()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public QArticolo(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        [Key, Persistent("Codice"), Browsable(false)]
        public string codice;

        public string Codice
        {
            get { return codice; }
        }

        [Persistent("Descrizione"), Browsable(false)]
        public string descrizione;

        public string Descrizione
        {
            get { return descrizione; }
        }

        //[Association, Browsable(false)]
        //public XPCollection<QFormuleArticoli> Formule
        //{
        //    get { 
        //        return GetCollection<QFormuleArticoli>("Formule"); }
        //}

        //[Association("ArticoloComponente"), Aggregated, Browsable(false)]
        //public XPCollection<QComponente> Componenti
        //{
        //    get { return this.GetCollection<QComponente>("Componenti"); }
        //}

        //[Association("ArticoloAllegato"), Aggregated, Browsable(false)]
        //public XPCollection<QDettaglioAllegato> DettagliAllegato
        //{
        //    get { return this.GetCollection<QDettaglioAllegato>("DettagliAllegato"); }
        //}

    }

}
