using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;

namespace XFactoryNET.Custom.MM1.Module.BusinessObjects
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

        [Persistent("Note"), Browsable(false)]
        public string note;
        public string Note
        {
            get { return note; }
        }

        [Persistent("Versione"), Browsable(false)]
        public string versione;
        public string Versione { get { return versione; } }

        [Association("FormulaArticolo")]
        public XPCollection<QArticolo> Articoli
        {
            get { return GetCollection<QArticolo>("Articoli"); }
        }

        [Association, Aggregated]
        public XPCollection<QComponente> Componenti
        {
            get { return this.GetCollection<QComponente>("Componenti"); }
        }




    }

}
