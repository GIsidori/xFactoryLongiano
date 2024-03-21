using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpo;
using XFactoryNET.Module.BusinessObjects;
using System.ComponentModel;

namespace XFactoryNET.Custom.Panzoo.Module.BusinessObjects
{

    public class ParametriDosaggio:BaseXPObject
    {
        public ParametriDosaggio(Session session):base(session)
        {

        }

        private Odl _Odl;

        [Indexed]
        public Odl Odl
        {
            get { return _Odl; }
            set { 
                SetPropertyValue<Odl>("Odl", ref _Odl, value);
            }
        }

        private BaseArticolo _Articolo;
        [Indexed]
        [Browsable(false)]
        public BaseArticolo Articolo
        {
            get { return _Articolo; }
            set { SetPropertyValue<BaseArticolo>("Articolo", ref _Articolo, value); }
        }

        public bool DS1
        {
            get { return GetPropertyValue<bool>("DS1"); }
            set { SetPropertyValue<bool>("DS1", value); }
        }

        private bool granMenù = false;
        public bool GranMenù
        {
            get { return granMenù; }
            set { SetPropertyValue<bool>("GranMenù", ref granMenù, value); }
        }


    }
}
