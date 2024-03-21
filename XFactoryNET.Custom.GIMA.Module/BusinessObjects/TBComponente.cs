using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;

namespace XFactoryNET.Custom.GIMA.Module.BusinessObjects
{
    [DeferredDeletion(false)]
    [OptimisticLocking(false)]
    public class TBComponente : XPObject
    {
        public TBComponente(Session session)
            : base(session)
        {

        }

        public string Formula { get; set; }

        public decimal Versione { get; set; }

        public string Articolo {get;set;}

        public decimal Percentuale { get; set; }

        public decimal Tolleranza { get; set; }

        public string Modalità { get; set; }

        //public static string FieldList = "Formula, Versione, Articolo, Percentuale, Tolleranza, Modalità";
        //public static string ViewName = "QComponente";

        //public static ICollection<QComponente> GetObjects(Session session)
        //{
        //    return session.GetObjectsFromQuery<QComponente>(string.Format("SELECT {0} FROM {1}", FieldList, ViewName));
        //}

    }

}
