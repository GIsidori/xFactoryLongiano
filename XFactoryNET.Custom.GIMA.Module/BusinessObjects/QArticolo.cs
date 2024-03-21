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
    [NonPersistent]
    public class QArticolo 
    {
        public QArticolo()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }


        public string Codice { get; set; }

        public string Descrizione { get; set; }

        public static string FieldList = "Codice, Descrizione";
        public static string ViewName = "QArticolo";

        public static ICollection<QArticolo> GetObjects(Session session)
        {
            return session.GetObjectsFromQuery<QArticolo>(string.Format("SELECT {0} FROM {1}",FieldList,ViewName));
        }

    }

}
