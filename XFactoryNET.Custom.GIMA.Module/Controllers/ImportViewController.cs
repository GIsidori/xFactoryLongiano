using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using XFactoryNET.Custom.GIMA.Module.BusinessObjects;
using XFactoryNET.Module.BusinessObjects;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;

namespace XFactoryNET.Custom.GIMA.Module.Controllers
{
    // For more information on Controllers and their life cycle, check out the http://documentation.devexpress.com/#Xaf/CustomDocument2621 and http://documentation.devexpress.com/#Xaf/CustomDocument3118 help articles.
    public partial class ImportViewController : ViewController
    {
        // Use this to do something when a Controller is instantiated (do not execute heavy operations here!).
        public ImportViewController()
        {
            InitializeComponent();
            RegisterActions(components);
            // For instance, you can specify activation conditions of a Controller or create its Actions (http://documentation.devexpress.com/#Xaf/CustomDocument2622).
            //TargetObjectType = typeof(DomainObject1);
            //TargetViewType = ViewType.DetailView;
            //TargetViewId = "DomainObject1_DetailView";
            //TargetViewNesting = Nesting.Root;
            //SimpleAction myAction = new SimpleAction(this, "MyActionId", DevExpress.Persistent.Base.PredefinedCategory.RecordEdit);
        }
        // Override to do something before Controllers are activated within the current Frame (their View property is not yet assigned).
        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();
            //For instance, you can access another Controller via the Frame.GetController<AnotherControllerType>() method to customize it or subscribe to its events.
        }
        // Override to do something when a Controller is activated and its View is assigned.
        protected override void OnActivated()
        {
            base.OnActivated();
            //For instance, you can customize the current View and its editors (http://documentation.devexpress.com/#Xaf/CustomDocument2729) or manage the Controller's Actions visibility and availability (http://documentation.devexpress.com/#Xaf/CustomDocument2728).
        }
        // Override to access the controls of a View for which the current Controller is intended.
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // For instance, refer to the http://documentation.devexpress.com/Xaf/CustomDocument3165.aspx help article to see how to access grid control properties.
        }
        // Override to do something when a Controller is deactivated.
        protected override void OnDeactivated()
        {
            // For instance, you can unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }


        private void simpleActionImportaArticolo_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Session session = ((XPObjectSpace)ObjectSpace).Session;

            foreach (TBArticolo qArt in e.SelectedObjects)
            {
                session.ExecuteSprocParametrized("ImportaArticolo", new SprocParameter("Codice", qArt.Codice));
                //ImportaArticolo(qArt.Codice);
            }

            //foreach (TBArticolo qArt in e.SelectedObjects)
            //{
                
            //    foreach (TBFormuleArticoli qArtForm in qArt.FormuleArticoli)
            //    {
            //        Formula formula = ImportaFormula(qArtForm.Formula,OdlDosaggio.IDLavorazione);
            //        if (qArt.Codice == qArtForm.Formula)
            //            formula.Descrizione = qArt.Descrizione;
            //        Articolo articolo = session.FindObject<Articolo>(PersistentCriteriaEvaluationBehavior.InTransaction,new BinaryOperator("Codice", qArt.Codice));  // ObjectSpace.GetObjectByKey<Articolo>(qArt.Codice);
            //        if (articolo != null)
            //        {
            //            articolo.TipoMateriale = TipoMateriale.Semilavorato;
            //            articolo.Formule.Add(formula);
            //        }
            //    }
            //}
            //session.CommitTransaction();

        }

        private void actionImportaFormula_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            

            Session session = ((XPObjectSpace)ObjectSpace).Session;
            
            foreach (TBFormula qFor in e.SelectedObjects)
            {
                session.ExecuteSprocParametrized("ImportaFormula",new SprocParameter("Codice",qFor.Codice));
                //ImportaFormula(qFor.Codice, OdlDosaggio.IDLavorazione);
            }

            //foreach (TBFormula qFor in e.SelectedObjects)
            //{
            //    foreach (TBFormuleArticoli qArtForm in qFor.FormuleArticoli)
            //    {
            //        Formula formula = session.FindObject<Formula>(PersistentCriteriaEvaluationBehavior.InTransaction,new BinaryOperator("Codice", qFor.Codice)); // ObjectSpace.GetObjectByKey<Formula>(qFor.Codice);
            //        Articolo articolo = ImportaArticolo(qArtForm.Articolo);
            //        if (qFor.Codice == qArtForm.Articolo)
            //            formula.Descrizione = articolo.Descrizione;
            //        if (articolo != null)
            //        {
            //            articolo.TipoMateriale = TipoMateriale.Semilavorato;
            //            articolo.Formule.Add(formula);
            //        }
            //    }
            //}
        }

        private void actionImportaAllegato_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Session session = ((XPObjectSpace)ObjectSpace).Session;

            foreach (TBAllegato qAll in e.SelectedObjects)
            {
                session.ExecuteSprocParametrized("ImportaAllegato", new SprocParameter("Codice", qAll.Codice));
                //ImportaFormula(qFor.Codice, OdlDosaggio.IDLavorazione);
            }

        }

        //private Formula ImportaFormula(string codiceFormula,string codiceLavorazione)
        //{
        //    Session session = ((XPObjectSpace)ObjectSpace).Session;
        //    TBFormula qForm = session.FindObject<TBFormula>(PersistentCriteriaEvaluationBehavior.InTransaction, new BinaryOperator("Codice", codiceFormula)); // ObjectSpace.GetObjectByKey<TBFormula>(codiceFormula);
        //    if (qForm == null)
        //        return null;

        //    Formula formula = session.FindObject<Formula>(PersistentCriteriaEvaluationBehavior.InTransaction, new BinaryOperator("Codice", codiceFormula));   // ObjectSpace.GetObjectByKey<Formula>(codiceFormula);
        //    try
        //    {
        //        session.BeginTransaction();
        //        if (formula == null)
        //        {
        //            formula = new Formula(session); // ObjectSpace.CreateObject<Formula>();
        //            formula.Codice = codiceFormula;
        //        }

        //        formula.Descrizione = string.Format("Formula {0}", codiceFormula);
        //        formula.Lavorazione = session.GetObjectByKey<Lavorazione>(codiceLavorazione);   // ObjectSpace.GetObjectByKey<Lavorazione>(codiceLavorazione);
        //        formula.ClasseMateriali = session.GetObjectByKey<Classe>(qForm.Classe); // ObjectSpace.GetObjectByKey<Classe>(qForm.Classe);
        //        formula.TipoFormula = (qForm.TipoFormula == "S") ? TipoFormula.Sostituzione : TipoFormula.Produzione;

        //        foreach (Componente cf in formula.Ingredienti.ToArray<Componente>())
        //        {
        //            cf.Delete();
        //        }

        //        foreach (TBComponente qc in qForm.Componenti)
        //        {
        //            Componente cf = new Componente(session);    // ObjectSpace.CreateObject<Componente>();
        //            cf.Articolo = session.FindObject<Articolo>(PersistentCriteriaEvaluationBehavior.InTransaction, new BinaryOperator("Codice", qc.Articolo));    // ObjectSpace.GetObjectByKey<Articolo>(qc.Articolo);
        //            if (cf.Articolo == null)
        //            {
        //                cf.Articolo = ImportaArticolo(qc.Articolo);
        //                if (cf.Articolo == null)
        //                {
        //                    session.RollbackTransaction();
        //                    break;
        //                }
        //            }
        //            cf.Modalità = session.GetObjectByKey<Modalità>(qc.Modalità);    // ObjectSpace.GetObjectByKey<Modalità>(qc.Modalità);
        //            cf.Percentuale = Math.Abs(System.Convert.ToSingle(qc.Percentuale));
        //            cf.Stato = StatoComponente.Pronto;
        //            cf.Tolleranza = System.Convert.ToSingle(qc.Tolleranza);
        //            if (cf.Percentuale >= 0)
        //                formula.Ingredienti.Add(cf);
        //            else
        //                formula.Prodotti.Add(cf);
        //            formula.Versione = qc.Versione.ToString();
        //        }
        //        formula.Save();
        //        session.CommitTransaction();
        //    }
        //    catch
        //    {
        //        session.RollbackTransaction();
        //    }

        //    return formula;
        //}

        //private Articolo ImportaArticolo(string codiceArticolo)
        //{
        //    Session session = ((XPObjectSpace)ObjectSpace).Session;

        //    TBArticolo tbArt = session.FindObject<TBArticolo>(PersistentCriteriaEvaluationBehavior.InTransaction,new BinaryOperator("Codice", codiceArticolo));  // ObjectSpace.GetObjectByKey<TBArticolo>(codiceArticolo);
        //    if (tbArt == null)
        //        return null;
        //    try
        //    {
        //        Articolo articolo = session.FindObject<Articolo>(PersistentCriteriaEvaluationBehavior.InTransaction, new BinaryOperator("Codice", codiceArticolo));   // ObjectSpace.GetObjectByKey<Articolo>(tbArt.Codice);
        //        session.BeginTransaction();

        //        if (articolo == null)
        //        {
        //            articolo = new Articolo(session);   // ObjectSpace.CreateObject<Articolo>();
        //            articolo.Codice = tbArt.Codice;
        //            articolo.Note = tbArt.Note;
        //        }
        //        articolo.Descrizione = tbArt.Descrizione;
        //        articolo.Save();
        //        return articolo;
        //    }
        //    catch
        //    {
        //        session.RollbackTransaction();
        //        return null;
        //    }
        //}


    }
}
