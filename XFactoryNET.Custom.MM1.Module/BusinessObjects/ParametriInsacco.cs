using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XFactoryNET.Module.BusinessObjects;
using DevExpress.Xpo;
using System.ComponentModel;

namespace XFactoryNET.Custom.MM1.Module.BusinessObjects
{
    public class ParametriInsacco:BaseXPObject
    {
        public ParametriInsacco(Session s):base(s)
        {

        }

        private Odl _Odl;

        [Indexed(Unique = true)]
        [Browsable(false)]
        public Odl Odl
        {
            get { return _Odl; }
            set { SetPropertyValue<Odl>("Odl", ref _Odl, value); }
        }

        private int _P1;
        public int P1
        {
            get { return _P1; }
            set { SetPropertyValue<int>("P1", ref _P1, value); }
        }

        private int _P2;
        public int P2
        {
            get { return _P2; }
            set { SetPropertyValue<int>("P2", ref _P2, value); }
        }

        private int _P3;
        public int P3
        {
            get { return _P3; }
            set { SetPropertyValue<int>("P3", ref _P3, value); }
        }

        private int _P4;
        public int P4
        {
            get { return _P4; }
            set { SetPropertyValue<int>("P4", ref _P4, value); }
        }

        private int _P5;
        public int P5
        {
            get { return _P5; }
            set { SetPropertyValue<int>("P5", ref _P5, value); }
        }

        private int _P6;
        public int P6
        {
            get { return _P6; }
            set { SetPropertyValue<int>("P6", ref _P6, value); }
        }

        private bool _Setaccio;
        public bool Setaccio
        {
            get { return _Setaccio; }
            set { SetPropertyValue<bool>("Setaccio", ref _Setaccio, value); }
        }



    }
}
