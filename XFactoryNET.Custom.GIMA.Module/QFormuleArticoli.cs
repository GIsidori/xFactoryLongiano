using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;

namespace XFactoryNET.Custom.GIMA.Module.BusinessObjects
{

    public struct QFormuleArticoliRow
    {
        [Persistent("Articolo"),Browsable(false)]
        [Association]
        public QArticolo Articolo;

        [Persistent("Formula"),Browsable(false)]
        [Association]
        public QFormula Formula;


    }
    public class QFormuleArticoli:XPLiteObject
    {
        public QFormuleArticoli()
        {

        }

        public QFormuleArticoli(Session session):base(session)
        {

        }

        [Key,Persistent,Browsable(false)]
        public QFormuleArticoliRow Row;


        [Browsable(false)]
        public QFormula Formula
        {
            get { return Row.Formula; }
        }

        [Browsable(false)]
        public QArticolo Articolo
        {
            get { return Row.Articolo; }
        }


    }
}
