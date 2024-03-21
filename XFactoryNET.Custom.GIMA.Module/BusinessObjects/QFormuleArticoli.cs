using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;

namespace XFactoryNET.Custom.GIMA.Module.BusinessObjects
{

    //public struct QFormuleArticoliRow
    //{
    //    [Persistent("Articolo"),Browsable(false)]
    //    //[Association]
    //    public QArticolo Articolo;

    //    [Persistent("Formula"),Browsable(false)]
    //    //[Association]
    //    public QFormula Formula;


    //}
    
    [NonPersistent]
    public class QFormuleArticoli
    {
        public QFormuleArticoli()
        {

        }

        public string Articolo { get; set; }

        public string Formula { get; set; }

        public static string FieldList = "Articolo, Formula";
        public static string ViewName = "QFormuleArticoli";

        public static ICollection<QFormuleArticoli> GetObjects(Session session)
        {
            return session.GetObjectsFromQuery<QFormuleArticoli>(string.Format("SELECT {0} FROM {1}", FieldList, ViewName));
        }


    }
}
