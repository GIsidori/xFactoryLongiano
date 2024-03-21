using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;

namespace XFactoryNET.Custom.GIMA.Module.BusinessObjects
{
    [DefaultProperty("Codice")]
    public class QFormula : XPLiteObject
    {
        public QFormula()
        {

        }

        public QFormula(Session session)
            : base(session)
        {

        }

        [Key, Persistent("Codice"), Browsable(false)]
        public string codice;
        public string Codice { get { return codice; } }

        [Persistent("Descrizione"), Browsable(false)]
        public string descrizione;

        public string Descrizione
        {
            get { return descrizione; }
        }

        [Persistent("Note"), Browsable(false)]
        public string note;
        public string Note
        {
            get { return note; }
        }

        //[Persistent("Versione"), Browsable(false)]
        //public string versione;
        //public string Versione { get { return versione; } }

        //[Association]
        //public XPCollection<QFormuleArticoli> Articoli
        //{
        //    get {
        //        return GetCollection<QFormuleArticoli>("Articoli"); }
        //}

        //[Association, Aggregated]
        //public XPCollection<QComponente> Componenti
        //{
        //    get { return this.GetCollection<QComponente>("Componenti"); }
        //}


    }

}
