using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;

namespace XFactoryNET.Module.BusinessObjects
{
    
    [DefaultProperty("Codice")]
    public abstract class BaseArticolo:BaseXPCustomObject
    {
        string fCodice;
        string fDescrizione;

        public BaseArticolo(Session session) : base(session) { }
        public BaseArticolo() : base(Session.DefaultSession) { }

        [Size(50)]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }

        [Key]
        [Size(12)]
        public string Codice
        {
            get { return fCodice; }
            set { SetPropertyValue<string>("Codice", ref fCodice, value); }
        }


        [Association, Browsable(false)]
        public XPCollection<Categoria> CategorieProprie
        {
            get { return GetCollection<Categoria>("CategorieProprie"); }
        }

        XPCollection<Categoria> categorie = null;
        [NonPersistent]
        [ModelDefault("AllowEdit","False")]
        [ModelDefault("AllowNew", "False")]
        [ModelDefault("AllowDelete", "False")]
        [ModelDefault("Caption","Categorie di appartenenza")]
        public XPCollection<Categoria> Categorie
        {
            get
            {
                if (categorie == null)
                {
                    XPCollection<Categoria> collectionCategorie = new XPCollection<Categoria>(Session);
                    categorie = new XPCollection<Categoria>(CategorieProprie);
                    foreach (var cat in collectionCategorie)
                    {
                        if (cat.CriteriaObjectType == this.GetType())
                        {
                            if ((string.IsNullOrEmpty(cat.Criteria) == false) && this.Fit(Session.ParseCriteria(cat.Criteria)))
                            {
                                categorie.Add(cat);
                            }
                        }
                    }
                }

                XPCollection<Categoria> copiaColl = new XPCollection<Categoria>(categorie);
                //Recurse
                foreach (var item in copiaColl)
                {
                    categorie.AddRange(item.Categorie);
                }

                return categorie;
            }
        }

        [Association, Aggregated]
        public XPCollection<Vincolo> Vincoli
        {
            get { return GetCollection<Vincolo>("Vincoli"); }
        }





    }
}
