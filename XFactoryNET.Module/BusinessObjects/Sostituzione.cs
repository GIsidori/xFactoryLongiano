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

        public Sostituzione(Session session) : base(session) { }
        public Sostituzione() : base(Session.DefaultSession) { }
        public override void AfterConstruction()
        {
            Applica = false;
            Abilitata = false;
            base.AfterConstruction();
        }

        BaseArticolo fBaseArticolo;

        public BaseArticolo BaseArticolo
        {
            get { return fBaseArticolo; }
            set { SetPropertyValue<BaseArticolo>("BaseArticolo", ref fBaseArticolo, value); }
        }

        Categoria fCategoria;
        public Categoria Categoria
        {
            get { return fCategoria; }
            set { SetPropertyValue<Categoria>("Categoria", ref fCategoria, value); }
        }

        Articolo fIngrediente;
        [Association]
        public Articolo Ingrediente
        {
            get { return fIngrediente; }
            set { SetPropertyValue<Articolo>("Ingrediente", ref fIngrediente, value); }
        }

        Formula fFormula;
        [Association]
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

        bool bAbilitata;
        public bool Abilitata
        {
            get { return bAbilitata; }
            set { SetPropertyValue<bool>("Abilitata", ref bAbilitata, value); }
        }

    }
}
