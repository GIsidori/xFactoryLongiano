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

    [DeferredDeletion(false)]
    [OptimisticLocking(false)]
    public class TBDettaglioAllegato : XPObject
    {

        public TBDettaglioAllegato(Session session)
            : base(session)
        {

        }


        public string Allegato {get;set;}

        public string Articolo {get;set;}

        [Browsable(false)]
        public string Segno { get; set; }

        public TipoSegno TipoSegno
        {
            get
            {
                switch (Segno)
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

        public string TipoVariazione
        {
            get;
            set;
        }


        public decimal Percentuale
        {
            set;
            get;
        }

        

    }
}
