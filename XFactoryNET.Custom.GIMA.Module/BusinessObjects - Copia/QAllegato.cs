using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;

namespace XFactoryNET.Custom.GIMA.Module.BusinessObjects
{
    public class QAllegato:XPLiteObject
    {

        public QAllegato()
        {

        }

        public QAllegato(Session session):base(session)
        {

        }


        [Key, Persistent("Codice"), Browsable(false)]
        public string codice;
        public string Codice { get { return codice; } }

        //[Association, Aggregated]
        //public XPCollection<QDettaglioAllegato> DettagliAllegato
        //{
        //    get { return GetCollection<QDettaglioAllegato>("DettagliAllegato"); }
        //}

    }
}
