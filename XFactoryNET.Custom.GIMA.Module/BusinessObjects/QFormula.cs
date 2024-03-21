using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;

namespace XFactoryNET.Custom.GIMA.Module.BusinessObjects
{
    [DefaultProperty("Codice")]
    [NonPersistent]
    public class QFormula
    {
        public QFormula()
        {

        }
        public string Codice { get; set; }

        public string Descrizione
        {
            get;
            set;
        }

        public string Note
        {
            get;
            set;
        }

        public string TipoFormula { get; set; }

        public string Classe { get; set; }

        public static string FieldList = "Codice, Descrizione, Note, TipoFormula, Classe";
        public static string ViewName = "QFormula";

        public static ICollection<QFormula> GetObjects(Session session)
        {
            return session.GetObjectsFromQuery<QFormula>(string.Format("SELECT {0} FROM {1}", FieldList, ViewName));
        }

    }

}
