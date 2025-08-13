using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace XFactoryNET.Module.BusinessObjects
{
    [Appearance("Insufficiente",Criteria="Stato='Insufficiente'",AppearanceItemType="ViewItem",TargetItems="Stato", FontColor="Red",Context="ListView",Priority=0)]
    [Appearance("Modificato", Criteria="StatoModifica='Modificato'", FontColor = "Blue", TargetItems = "*", AppearanceItemType = "ViewItem", Context = "ListView")]
    [Appearance("Aggiunto", Criteria = "StatoModifica='Aggiunto'", FontColor = "Green", TargetItems = "*", AppearanceItemType = "ViewItem", Context = "ListView")]
    [Appearance("Eliminato", Criteria = "StatoModifica='Eliminato'", FontColor = "Red", TargetItems = "*", AppearanceItemType = "ViewItem", Context = "ListView")]
    public class Componente : BaseXPObject
    {
        public Componente(Session session) : base(session) { }
        public Componente() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        public Componente Clona()
        {
            
            Componente c = new Componente(this.Session);
            c.BeginEdit();
            c.Articolo = this.Articolo;
            //c.Modalità = this.Modalità;
            //if (this.Modalità == null)
            c.Modalità = this.Articolo.Modalità;
            c.Percentuale = this.Percentuale;
            c.Stato = this.Stato;
            c.Tolleranza = this.Tolleranza;
            c.TolleranzaPerc = this.TolleranzaPerc;
            c.EndEdit();

            return c;
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (propertyName == "Percentuale")
                OnChanged("Quantità");
        }

        decimal fPercentuale;
        [ModelDefault("DisplayFormat","n4")]
        [ModelDefault("EditMask","n4")]
        [ImmediatePostData]
        [RuleValueComparison(ValueComparisonType.GreaterThan,0,CustomMessageTemplate="Percentuale non valida",TargetContextIDs="Verifica Ingredienti")]
        public decimal Percentuale
        {
            get { return fPercentuale; }
            set { SetPropertyValue<decimal>("Percentuale", ref fPercentuale, value); }
        }

        private decimal quantità;
        [ModelDefault("DisplayFormat", "n3")][ModelDefault("EditMask","n3")]
        //[Appearance(null, AppearanceItemType = "ViewItem", Criteria = "OdlIngredientiTeorici IS NULL AND OdlProdottiTeorici IS NULL", Visibility = ViewItemVisibility.Hide)]
        public decimal Quantità
        {
            get 
            { 
                return quantità;
            }
            set
            {
                SetPropertyValue<decimal>("Quantità", ref quantità, value);
            }
        }


        decimal fTolleranza;
        [ModelDefault("DisplayFormat", "n3")][ModelDefault("EditMask","n3")]
        public decimal Tolleranza
        {
            get { return fTolleranza; }
            set { SetPropertyValue<decimal>("Tolleranza", ref fTolleranza, value); }
        }

        decimal fTolleranzaPerc;
        [ModelDefault("DisplayFormat", "p")]
        public decimal TolleranzaPerc
        {
            get { return fTolleranzaPerc; }
            set { SetPropertyValue<decimal>("TolleranzaPerc", ref fTolleranzaPerc, value); }
        }

        Articolo fArticolo;
        public Articolo Articolo
        {
            get { return fArticolo; }
            set
            {
                if (fArticolo != value)
                {
                    SetPropertyValue<Articolo>("Articolo", ref fArticolo, value);
                }
            }
        }


        Modalità fModalità;
        public Modalità Modalità
        {
            get { return fModalità; }
            set { SetPropertyValue<Modalità>("Modalità", ref fModalità, value); }
        }

        Silos silos;
        [Appearance(null,AppearanceItemType="ViewItem",Criteria="OdlIngredientiTeorici IS NULL AND OdlProdottiTeorici IS NULL",Visibility=ViewItemVisibility.Hide)]
        public Silos Silos
        {
            get { return silos; }
            set { SetPropertyValue<Silos>("Silos", ref silos, value); }
        }

        StatoComponente fStato;
        [RuleValueComparison("RuleComponentePronto","Verifica Ingredienti",ValueComparisonType.Equals,StatoComponente.Pronto,CustomMessageTemplate="Materiale {Articolo}: {DescrizioneArticolo} inesistente o quantità insufficiente")]
        public StatoComponente Stato
        {
            get { return fStato; }
            set { SetPropertyValue<StatoComponente>("Stato", ref fStato, value); }
        }

        StatoModificaComponente fStatoModifica;
        [Browsable(false)]
        public StatoModificaComponente StatoModifica
        {
            get { return fStatoModifica; }
            set { SetPropertyValue<StatoModificaComponente>("StatoModifica", ref fStatoModifica, value); }
        }


        [NonPersistent]
        [Browsable(false)]
        [RuleFromBoolProperty(CustomMessageTemplate = "{Articolo} - {DescrizioneArticolo} non compatibile con {ArticoloIncompatibile} - {DescrizioneArticoloIncompatibile}", TargetContextIDs = "Verifica Ingredienti", InvertResult = true)]
        public bool Incompatibile { get { return ArticoloIncompatibile != null; } }

        [NonPersistent]
        [Browsable(false)]
        public BaseArticolo ArticoloIncompatibile { get; set; }

        [Browsable(false)]
        [NonPersistent]
        public string DescrizioneArticoloIncompatibile { get { return ArticoloIncompatibile == null ? null : ArticoloIncompatibile.Descrizione; } }

        [Browsable(false)]
        [NonPersistent]
        public string DescrizioneArticolo { get { return Articolo == null ? null : Articolo.Descrizione; } }
        
        private Formula fFormulaIngredienti;
        [Association("FormulaIngredienti"), Browsable(false)]
        public Formula FormulaIngredienti
        {
            get { return fFormulaIngredienti; }
            set { SetPropertyValue<Formula>("FormulaIngredienti", ref fFormulaIngredienti, value); }
        }

        private Formula fFormulaProdotti;
        [Association("FormulaProdotti"), Browsable(false)]
        public Formula FormulaProdotti
        {
            get { return fFormulaProdotti; }
            set { SetPropertyValue<Formula>("FormulaProdotti", ref fFormulaProdotti, value); }
        }


        private Odl fOdlIngredientiTeorici;
        [Association("OdlIngredientiTeorici"), Browsable(false)]
        public Odl OdlIngredientiTeorici
        {
            get { return fOdlIngredientiTeorici; }
            set { SetPropertyValue<Odl>("OdlIngredientiTeorici", ref fOdlIngredientiTeorici, value); }
        }

        private Odl fOdlProdottiTeorici;
        [Association("OdlProdottiTeorici"), Browsable(false)]
        public Odl OdlProdottiTeorici
        {
            get { return fOdlProdottiTeorici; }
            set { SetPropertyValue<Odl>("OdlProdottiTeorici", ref fOdlProdottiTeorici, value); }
        }

    }
}
