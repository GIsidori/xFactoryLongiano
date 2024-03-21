using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DevExpress.Xpo;
using XFactoryNET.Module.BusinessObjects;
using System.Threading;
using System.ComponentModel;

namespace XFactoryNET.Custom.MM1.Module.BusinessObjects
{
    public enum TipoSetaccio
    {
        Setaccio250,
        Setaccio250D,
        NoSetaccio
    }

    public enum TipoRete
    {
        Fine,
        Grossa
    }

    public enum TipoMolino
    {
        Molino178,
        Molino179,
        MolinoRulli
    }

    public class ParametriDosaggio:BaseXPObject
    {
        public ParametriDosaggio():base()
        {

        }

        public ParametriDosaggio(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private Odl _Odl;

        [Indexed(Unique=true)]
        [Browsable(false)]
        public Odl Odl
        {
            get { return _Odl; }
            set { SetPropertyValue<Odl>("Odl", ref _Odl, value); }
        }

        private int _TempoMiscelazione227;
        public int TempoMiscelazione227
        {
            get { return _TempoMiscelazione227; }
            set { SetPropertyValue<int>("TempoMiscelazione227", ref _TempoMiscelazione227, value); }
        }

        private int _TempoMiscelazione223;
        public int TempoMiscelazione223
        {
            get {return _TempoMiscelazione223;}
            set { SetPropertyValue<int>("TempoMiscelazione223", ref _TempoMiscelazione223, value); }
        }

        private int _PesoSupportoMCD;
        public int PesoSupportoMCD
        {
            get { return _PesoSupportoMCD; }
            set { SetPropertyValue<int>("PesoSupportoMCD", ref _PesoSupportoMCD, value); }
        }

        private int _VelocitaCoclea;
        public int VelocitàCoclea
        {
            get { return _VelocitaCoclea; }
            set { SetPropertyValue<int>("VelocitaCoclea", ref _VelocitaCoclea, value); }
        }

        private TipoSetaccio _TipoSetaccio;
        public TipoSetaccio TipoSetaccio
        {
            get { return _TipoSetaccio; }
            set { SetPropertyValue<TipoSetaccio>("TipoSetaccio", ref _TipoSetaccio, value); }
        }

        private TipoRete _TipoRete;
        public TipoRete TipoRete
        {
            get { return _TipoRete; }
            set { SetPropertyValue<TipoRete>("TipoRete", ref _TipoRete, value); }
        }

        private TipoMolino _TipoMolino;
        public TipoMolino TipoMolino
        {
            get { return _TipoMolino;}
            set { SetPropertyValue<TipoMolino>("TipoMolino", ref _TipoMolino, value); }
        }

        private int _AmpereMolino;
        public int AmpereMolino
        {
            get { return _AmpereMolino; }
            set { SetPropertyValue<int>("AmpereMolino", ref _AmpereMolino, value); }
        }

        private int _PercInverter;
        public int PercInverter
        {
            get { return _PercInverter; }
            set { SetPropertyValue<int>("PercInverter", ref _PercInverter, value); }
        }

        private int _Rotazione175;
        public int Rotazione175
        {
            get {return _Rotazione175;}
            set { SetPropertyValue<int>("Rotazione175", ref _Rotazione175, value); }
        }

        private bool _Setaccio106;
        public bool Setaccio106
        {
            get { return _Setaccio106; }
            set { SetPropertyValue<bool>("Setaccio106", ref _Setaccio106, value); }
        }
        
    }
}
