using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;

namespace XFactoryNET.Custom.GIMA.Module.BusinessObjects
{
    public enum TipoSegno
    {
        Set,
        Incr,
        Dec
    }

    public class QDettaglioAllegato:XPLiteObject
    {

        public QDettaglioAllegato()
        {

        }

        public QDettaglioAllegato(Session session):base(session)
        {

        }


        [Key, Persistent, Browsable(false)]
        public QAllegatoRow Row;

        public QAllegato Allegato
        {
            get { return Row.Allegato; }
        }

        public QArticolo Articolo
        {
            get { return Row.Articolo; }
        }

        [Persistent("Segno"), Browsable(false)]
        public string segno;
        public TipoSegno Segno
        {
            get
            {
                switch (segno)
                {
                    case "+":
                        return TipoSegno.Incr;
                    case "-":
                        return TipoSegno.Dec;
                    default:
                        return TipoSegno.Set;
                }
            }
        }

        [Persistent("TipoVariazione"),Browsable(false)]
        public string tipoVariazione;
        public string TipoVariazione
        {
            get { return tipoVariazione; }
        }


        [Persistent("Percentuale"), Browsable(false)]
        public decimal percentuale;
        public decimal Percentuale
        {
            get { return percentuale; }
        }

        

    }
}
