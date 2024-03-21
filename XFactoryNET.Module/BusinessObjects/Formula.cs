using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;

namespace XFactoryNET.Module.BusinessObjects
{
    public enum TipoFormula
    {
        Produzione,
        Sostituzione
    }

    [MapInheritance(MapInheritanceType.OwnTable)]
    [NavigationItem("Formule e Materiali")]
    [DefaultProperty("Codice")]
    public class Formula : BaseXPCustomObject
    {

        public Formula(Session session) : base(session) { }
        public Formula() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        string fCodice;
        string fDescrizione;

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

        string fVersione;
        public string Versione
        {
            get { return fVersione; }
            set { SetPropertyValue<string>("Versione", ref fVersione, value); }
        }

        string fNote;
        public string Note
        {
            get { return fNote; }
            set { SetPropertyValue<string>("Note", ref fNote, value); }
        }

        Lavorazione fLavorazione;
        public Lavorazione Lavorazione
        {
            get { return fLavorazione; }
            set { SetPropertyValue<Lavorazione>("Lavorazione", ref fLavorazione, value); }
        }


        [Association("FormulaIngredienti"), Aggregated]
        public DevExpress.Xpo.XPCollection<Componente> Ingredienti
        {
            get { return GetCollection<Componente>("Ingredienti"); }
        }

        [Association("FormulaProdotti"), Aggregated]
        public DevExpress.Xpo.XPCollection<Componente> Prodotti
        {
            get { return GetCollection<Componente>("Prodotti"); }
        }

        TipoFormula tipoFormula;
        public TipoFormula TipoFormula
        {
            get {return tipoFormula ;}
            set { SetPropertyValue<TipoFormula>("TipoFormula", ref tipoFormula, value); }
        }

        private Classe classeMateriali;
        public Classe ClasseMateriali
        {
            get { return classeMateriali; }
            set { SetPropertyValue<Classe>("ClasseMateriali", ref classeMateriali, value); }
        }

        [Association("FormulaOdl"),Browsable(false)]
        public XPCollection<Odl> Odl
        {
            get { return GetCollection<Odl>("Odl"); }
        }

        [Browsable(false)]
        [Association("FormuleSostituzione_Odl")]
        public XPCollection<Odl> OdlFormuleSostituzione
        {
            get { return GetCollection<Odl>("OdlFormuleSostituzione"); }
        }

        [Association]
        public DevExpress.Xpo.XPCollection<Articolo> Articoli
        {
            get
            {
                return GetCollection<Articolo>("Articoli");
            }
        }

        //protected override XPCollection<T> CreateCollection<T>(DevExpress.Xpo.Metadata.XPMemberInfo property)
        //{
        //    XPCollection<T> result = base.CreateCollection<T>(property);
        //    if (property.Name == "OdlFormuleSostituzione")
        //        result.CollectionChanged += new XPCollectionChangedEventHandler(result_CollectionChanged);
        //    return result;
        //}

        //protected override XPCollection CreateCollection(DevExpress.Xpo.Metadata.XPMemberInfo property)
        //{
        //    XPCollection result = base.CreateCollection(property);
        //    if (property.Name == "OdlFormuleSostituzione")
        //        result.CollectionChanged += new XPCollectionChangedEventHandler(result_CollectionChanged);
        //    return result;
        //}

        //void result_CollectionChanged(object sender, XPCollectionChangedEventArgs e)
        //{
        //    OdlDosaggio odl = (OdlDosaggio)e.ChangedObject;
        //    switch (e.CollectionChangedType)
        //    {
        //        case XPCollectionChangedType.AfterAdd:
        //            Componente comp = odl.IngredientiTeorici.FirstOrDefault<Componente>(c => this.Articoli.Contains(c.Articolo));
        //            if (comp != null)
        //            {
        //                odl.internalSostituisciComponente(comp, this);
        //                odl.Stato = StatoOdL.InPreparazione;
        //            }
        //            else
        //                odl.FormuleSostituzione.Remove(this);
        //            break;
        //        case XPCollectionChangedType.AfterRemove:
        //            break;
        //        case XPCollectionChangedType.BeforeAdd:
        //            break;
        //        case XPCollectionChangedType.BeforeRemove:
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }

}
