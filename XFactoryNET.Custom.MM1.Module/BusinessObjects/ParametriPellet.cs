using System.Linq;
using System.Text;
using XFactoryNET.Module.BusinessObjects;
using DevExpress.Xpo;
using System.ComponentModel;

namespace XFactoryNET.Custom.MM1.Module.BusinessObjects
{
    public class ParametriPellet:BaseXPObject
    {
        public ParametriPellet(Session s):base(s)
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

        private bool _Sbriciolatore;
        public bool Sbriciolatore
        {
            get { return _Sbriciolatore; }
            set { SetPropertyValue<bool>("Sbriciolatore", ref _Sbriciolatore, value); }
        }

        private bool _SetaccioPC2;
        public bool SetaccioPC2
        {
            get { return _SetaccioPC2; }
            set { SetPropertyValue<bool>("SetaccioPC2", ref _SetaccioPC2, value); }
        }

        private bool _Talco;
        public bool Talco
        {
            get { return _Talco; }
            set { SetPropertyValue<bool>("Talco", ref _Talco, value); }
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

        private int _P1;
        public int P1
        {
            get { return _P1; }
            set { SetPropertyValue<int>("P1", ref _P1, value); }
        }



    }
}
