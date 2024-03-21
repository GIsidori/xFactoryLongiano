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
using XFactoryNET.Custom.MM1.Module.BusinessObjects;
using XFactoryNET.Module.BusinessObjects;

namespace XFactoryNET.Custom.MM1.Module.Controllers
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

        private void singleChoiceActionImport_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            switch (e.SelectedChoiceActionItem.Id)
            {
                default:
                    IObjectSpace objectSpace = Application.CreateObjectSpace();
                    ListView view = Application.CreateListView(Application.GetListViewId(typeof(QArticolo)),
                        new CollectionSource(Application.CreateObjectSpace(), typeof(QArticolo)), false);
                    e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
                    e.ShowViewParameters.CreatedView = view;
                    break;
            }

        }

        private void simpleActionImportaArticolo_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            foreach (QArticolo art in e.SelectedObjects)
            {
                ImportaArticolo(art);
            }

            foreach (QArticolo art in e.SelectedObjects)
            {
                if (art.Formula != null && art.formula.Codice != null)
                {
                    Formula formula = ImportaFormula(art.Formula);
                    if (art.codice == art.formula.codice)
                        formula.Descrizione = art.descrizione;
                    Articolo articolo = ObjectSpace.GetObjectByKey<Articolo>(art.codice);
                    if (articolo != null)
                    {
                        articolo.TipoMateriale = TipoMateriale.Semilavorato;
                        articolo.Formule.Add(formula);
                    }
                }
            }
            this.ObjectSpace.CommitChanges();
        }

        private Formula ImportaFormula(QFormula formula)
        {
            Formula form = ObjectSpace.GetObjectByKey<Formula>(formula.Codice);
            if (form == null)
                form = ObjectSpace.CreateObject<Formula>();
            form.Codice = formula.codice;
            form.Descrizione = string.Format("Formula {0}", formula.codice);
            form.Note = formula.Note;
            form.Versione = formula.Versione;

            foreach (Componente cf in form.Ingredienti.ToArray<Componente>())
            {
                cf.Delete();
            }

            foreach (QComponente qc in formula.Componenti)
            {
                Componente cf = ObjectSpace.CreateObject<Componente>();
                cf.Articolo = ObjectSpace.GetObjectByKey<Articolo>(qc.Articolo.Codice);
                if (cf.Articolo == null)
                    cf.Articolo = ImportaArticolo(qc.Articolo);
                cf.Modalità = ObjectSpace.GetObjectByKey<Modalità>(qc.Modalità);
                cf.Percentuale = Math.Abs(qc.Percentuale);
                cf.Stato = StatoComponente.Pronto;
                cf.Tolleranza = qc.Tolleranza;
                if (qc.percentuale >= 0)
                    form.Ingredienti.Add(cf);
                else
                    form.Prodotti.Add(cf);
            }
            form.Save();
            ObjectSpace.CommitChanges();
            return form;
        }

        private Articolo ImportaArticolo(QArticolo articolo)
        {
            Articolo art = ObjectSpace.GetObjectByKey<Articolo>(articolo.Codice);
            if (art == null)
                art = ObjectSpace.CreateObject<Articolo>();
            art.Codice = articolo.Codice;
            art.Descrizione = articolo.descrizione;
            art.Save();
            ObjectSpace.CommitChanges();
            return art;
        }

    }
}
