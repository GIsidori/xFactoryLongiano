using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Data.Filtering;

namespace XFactoryNET.Custom.GIMA.Module.BusinessObjects
{
    [DefaultProperty("Codice")]
    [DeferredDeletion(false)]
    [OptimisticLocking(false)]
    public class TBFormula : XPCustomObject
    {
        public TBFormula(Session session):base(session)
        {

        }

        [Key]
        public string Codice { get; set; }

        public string TipoFormula { get; set; }

        public string Classe { get; set; }

        public XPCollection<TBFormuleArticoli> FormuleArticoli
        {
            get
            {
                return new XPCollection<TBFormuleArticoli>(PersistentCriteriaEvaluationBehavior.InTransaction, this.Session, new BinaryOperator("Formula", this.Codice));
            }
        }

        public XPCollection<TBComponente> Componenti
        {
            get
            {
                return new XPCollection<TBComponente>(PersistentCriteriaEvaluationBehavior.InTransaction, this.Session, new BinaryOperator("Formula", this.Codice));
            }
        }
    }

}
