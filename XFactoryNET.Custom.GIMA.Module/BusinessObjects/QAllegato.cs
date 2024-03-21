using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;

namespace XFactoryNET.Custom.GIMA.Module.BusinessObjects
{
    [NonPersistent]
    public class QAllegato
    {

        public QAllegato()
        {

        }


        public string Codice { get; set; }

        public static string FieldList = "Codice";
        public static string ViewName = "QAllegato";

        public static ICollection<QAllegato> GetObjects(Session session)
        {
            return session.GetObjectsFromQuery<QAllegato>(string.Format("SELECT {0} FROM {1}", FieldList, ViewName));
        }


    }
}
