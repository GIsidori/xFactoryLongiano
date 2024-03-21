using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XFactoryNET.Module.BusinessObjects;
using DevExpress.Xpo;
using System.ComponentModel;

namespace XFactoryNET.Custom.MM1.Module.BusinessObjects
{
    public class ParametriCaricoRinfusa:BaseXPObject
    {
        public ParametriCaricoRinfusa():base()
        {

        }
        public ParametriCaricoRinfusa(Session session):base(session)
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

        private int _LiqA;
        public int LiqA
        {
            get { return _LiqA; }
            set { SetPropertyValue<int>("LiqA", ref _LiqA, value); }
        }

        private int _LiqB;
        public int LiqB
        {
            get { return _LiqB; }
            set { SetPropertyValue<int>("LiqB", ref _LiqB, value); }
        }

        private byte _CodiceEH;
        public byte CodiceEH
        {
            get { return _CodiceEH; }
            set { SetPropertyValue<byte>("CodiceEH", ref _CodiceEH, value); }
        }

        private bool _Prepulitore;
        public bool Prepulitore
        {
            get { return _Prepulitore; }
            set { SetPropertyValue<bool>("Prepulitore", ref _Prepulitore, value); }
        }

        private bool _Talco;
        public bool Talco
        {
            get { return _Talco; }
            set { SetPropertyValue<bool>("Talco", ref _Talco, value); }
        }

    }
}
