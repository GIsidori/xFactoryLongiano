using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;

namespace XFactoryNET.Module.BusinessObjects
{
    [MapInheritance(MapInheritanceType.ParentTable)]
    [NavigationItem("Formule e Materiali")]
    public class Categoria : BaseArticolo
    {

        public Categoria(Session session) : base(session) { }
        public Categoria() : base(Session.DefaultSession) { }

        [Association]
        [ModelDefault("Caption", "Componenti della categoria")]
        public XPCollection<BaseArticolo> Articoli
        {
            get
            {
                return GetCollection<BaseArticolo>("Articoli");
            }
        }

        [MemberDesignTimeVisibility(false)]
        public Type CriteriaObjectType
        {
            get { return typeof(Articolo); }
        }

        private string _Criteria;
        [CriteriaOptions("CriteriaObjectType")]
        public string Criteria
        {
            get { return _Criteria; }
            set { SetPropertyValue("Criteria", ref _Criteria, value); }
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
