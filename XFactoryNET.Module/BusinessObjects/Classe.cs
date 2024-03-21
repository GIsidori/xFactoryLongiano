using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;

namespace XFactoryNET.Module.BusinessObjects
{
    [MapInheritance(MapInheritanceType.ParentTable)]
    [NavigationItem("Formule e Materiali")]
    public class Classe : BaseArticolo
    {

        public Classe(Session session) : base(session) { }
        public Classe() : base(Session.DefaultSession) { }

        [Association]
        public XPCollection<BaseArticolo> Articoli
        {
            get
            {
                return GetCollection<BaseArticolo>("Articoli");
            }
        }

        private bool ereditaria;
        public bool Ereditaria
        {
            get { return ereditaria; }
            set { SetPropertyValue<bool>("Ereditaria", ref ereditaria, value); }
        }
    
        public override void AfterConstruction() { base.AfterConstruction(); }



    }
}
