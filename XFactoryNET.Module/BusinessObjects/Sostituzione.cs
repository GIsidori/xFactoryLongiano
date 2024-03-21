using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;

namespace XFactoryNET.Module.BusinessObjects
{
    public class Sostituzione : BaseXPObject
    {
        BaseArticolo fClasse;
        [Association]
        public BaseArticolo BaseArticolo
        {
            get { return fClasse; }
            set { SetPropertyValue<BaseArticolo>("BaseArticolo", ref fClasse, value); }
        }

        Articolo fIngrediente;
        public Articolo Ingrediente
        {
            get { return fIngrediente; }
            set { SetPropertyValue<Articolo>("Ingrediente", ref fIngrediente, value); }
        }
        Formula fFormula;
        public Formula Formula
        {
            get { return fFormula; }
            set { SetPropertyValue<Formula>("Formula", ref fFormula, value); }
        }

        bool bApplica;
        public bool Applica
        {
            get { return bApplica; }
            set { SetPropertyValue<bool>("Applica", ref bApplica, value); }
        }

        public Sostituzione(Session session) : base(session) { }
        public Sostituzione() : base(Session.DefaultSession) { }
        public override void AfterConstruction() 
        {
            Applica = true;
            base.AfterConstruction();
        }
    }
}
