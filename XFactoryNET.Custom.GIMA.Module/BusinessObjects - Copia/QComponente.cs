using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;

namespace XFactoryNET.Custom.GIMA.Module.BusinessObjects
{
    public class QComponente : XPLiteObject
    {
        public QComponente()
        {

        }

        public QComponente(Session session)
            : base(session)
        {

        }

        [Key, Persistent, Browsable(false)]
        public QComponenteRow Row;

        //[Persistent("Formula"), Browsable(false)]
        //[Association]
        //public QFormula formula;

        //[Persistent("Articolo"), Browsable(false)]
        //[Association("ArticoloComponente")]
        //public QArticolo articolo;

        [Browsable(false)]
        public QFormula Formula
        {
            get { return Row.Formula; }
        }

        public QArticolo Articolo
        {
            get { return Row.Articolo; }
        }

        [Persistent("Percentuale"), Browsable(false)]
        public float percentuale;
        [Persistent("Tolleranza"), Browsable(false)]
        public float tolleranza;
        [Persistent("Modalità"), Browsable(false)]
        public string modalità;

        [Custom("DisplayFormat", "n4")]
        public float Percentuale { get { return percentuale; } }

        [Custom("DisplayFormat", "n2")]
        public float Tolleranza { get { return tolleranza; } }

        public string Modalità { get { return modalità; } }

        [Persistent("Versione"), Browsable(false)]
        public string versione;
        public string Versione { get { return versione; } }

    }

}
