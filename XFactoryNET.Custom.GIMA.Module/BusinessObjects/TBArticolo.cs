using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Collections;
using DevExpress.Data.Filtering;

namespace XFactoryNET.Custom.GIMA.Module.BusinessObjects
{
    [DefaultProperty("Codice")]
    [DeferredDeletion(false)]
    [OptimisticLocking(false)]
    public class TBArticolo : XPCustomObject
    {
        public TBArticolo(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        [Key]
        public string Codice { get; set; }

        public string Descrizione { get; set; }

        public string Note { get; set; }

        public XPCollection<TBFormuleArticoli> FormuleArticoli
        {
            get
            {
                return new XPCollection<TBFormuleArticoli>(PersistentCriteriaEvaluationBehavior.InTransaction, this.Session, new BinaryOperator("Articolo", this.Codice));
            }
        }

        //public static string FieldList = "Codice, Descrizione";
        //public static string ViewName = "QArticolo";

        //public static ICollection<QArticolo> GetObjects(Session session)
        //{
        //    return session.GetObjectsFromQuery<QArticolo>(string.Format("SELECT {0} FROM {1}", FieldList, ViewName));
        //}

    }

}
