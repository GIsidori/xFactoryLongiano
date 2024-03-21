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

        ICollection<QArticolo> qArticoli;
        ICollection<QFormuleArticoli> qFormuleArticoli;
        ICollection<QComponente> qComponenti;
        ICollection<QFormula> qFormule;

        XPCollection<Articolo> articoli;
        XPCollection<Formula> formule;

        private void singleChoiceActionImport_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {

            IObjectSpace objectSpace = Application.CreateObjectSpace();
            Session session = ((XPObjectSpace)ObjectSpace).Session;

            switch (e.SelectedChoiceActionItem.Id)
            {
                case "Formule":
                    e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
                    e.ShowViewParameters.CreatedView = ShowListView<QFormula>(QFormula.GetObjects(session),objectSpace);
                    break;
                case "Articoli":
                    e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
                    e.ShowViewParameters.CreatedView = ShowListView<QArticolo>(QArticolo.GetObjects(session),objectSpace);
                    break;
                case "Allegati":
                    e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
                    e.ShowViewParameters.CreatedView = ShowListView<QAllegato>(QAllegato.GetObjects(session),objectSpace);
                    break;

            }

        }

        private ListView ShowListView<T>(ICollection<T> list,IObjectSpace objectSpace)
        {
            //ICollection<T> list = session.GetObjectsFromQuery<T>(sql);
            CollectionSource cs = new CollectionSource(objectSpace, typeof(T));
            foreach (var item in list)
            {
                cs.List.Add(item);
            }
            ListView view = Application.CreateListView(Application.GetListViewId(typeof(T)),
                cs, false);

            return view;
        }

        private void simpleActionImportaArticolo_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Session session = ((XPObjectSpace)ObjectSpace).Session;

            qArticoli = QArticolo.GetObjects(session);  // session.GetObjectsFromQuery<QArticolo>("SELECT Codice, Descrizione FROM QArticolo");
            qFormuleArticoli = QFormuleArticoli.GetObjects(session);    // session.GetObjectsFromQuery<QFormuleArticoli>("SELECT Articolo, Formula  FROM QFormuleArticoli");
            qFormule = QFormula.GetObjects(session);    // session.GetObjectsFromQuery<QFormula>("SELECT Codice, Descrizione, Note  FROM QFormula");
            qComponenti = QComponente.GetObjects(session);  // session.GetObjectsFromQuery<QComponente>("SELECT Formula, Versione, Articolo, Percentuale, Tolleranza, Modalità FROM QComponente");

            articoli = new XPCollection<Articolo>(session);
            articoli.Load();

            formule = new XPCollection<Formula>(session);
            formule.Load();

            foreach (QArticolo qArt in e.SelectedObjects)
            {
                ImportaArticolo(qArt.Codice);
            }

            foreach (QArticolo qArt in e.SelectedObjects)
            {
                
                foreach (QFormuleArticoli qArtForm in qFormuleArticoli.Where<QFormuleArticoli>(f => f.Articolo == qArt.Codice))
                {
                    Formula formula = ImportaFormula(qArtForm.Formula,OdlDosaggio.IDLavorazione);
                    if (qArt.Codice == qArtForm.Formula)
                        formula.Descrizione = qArt.Descrizione;
                    Articolo articolo = articoli.FirstOrDefault<Articolo>(a => a.Codice == qArt.Codice); // ObjectSpace.GetObjectByKey<Articolo>(art.Codice);
                    if (articolo != null)
                    {
                        articolo.TipoMateriale = TipoMateriale.Semilavorato;
                        articolo.Formule.Add(formula);
                    }
                }
            }
            this.ObjectSpace.CommitChanges();
        }

        private void actionImportaFormula_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Session session = ((XPObjectSpace)ObjectSpace).Session;

            qArticoli = QArticolo.GetObjects(session);  // session.GetObjectsFromQuery<QArticolo>("SELECT Codice, Descrizione FROM QArticolo");
            qFormuleArticoli = QFormuleArticoli.GetObjects(session);    // session.GetObjectsFromQuery<QFormuleArticoli>("SELECT Articolo, Formula  FROM QFormuleArticoli");
            qFormule = QFormula.GetObjects(session);    // session.GetObjectsFromQuery<QFormula>("SELECT Codice, Descrizione, Note  FROM QFormula");
            qComponenti = QComponente.GetObjects(session);  // session.GetObjectsFromQuery<QComponente>("SELECT Formula, Versione, Articolo, Percentuale, Tolleranza, Modalità FROM QComponente");

            articoli = new XPCollection<Articolo>(session);
            articoli.Load();

            formule = new XPCollection<Formula>(session);
            formule.Load();

            foreach (QFormula qFor in e.SelectedObjects)
            {
                ImportaFormula(qFor.Codice, OdlDosaggio.IDLavorazione);
            }

            foreach (QFormula qFor in e.SelectedObjects)
            {
                foreach (QFormuleArticoli qArtForm in qFormuleArticoli.Where<QFormuleArticoli>(f => f.Formula == qFor.Codice))
                {
                    Formula formula = formule.FirstOrDefault<Formula>(f => f.Codice == qFor.Codice); // ObjectSpace.GetObjectByKey<Articolo>(art.Codice);
                    Articolo articolo = ImportaArticolo(qArtForm.Articolo);
                    if (qFor.Codice == qArtForm.Articolo)
                        formula.Descrizione = articolo.Descrizione;
                    if (articolo != null)
                    {
                        articolo.TipoMateriale = TipoMateriale.Semilavorato;
                        articolo.Formule.Add(formula);
                    }
                }
            }

            ObjectSpace.CommitChanges();

        }

        private Formula ImportaFormula(string formula,string codiceLavorazione)
        {
            QFormula qForm = qFormule.FirstOrDefault<QFormula>(q => q.Codice == formula);
            Formula form = formule.FirstOrDefault<Formula>(f => f.Codice == formula);   // ObjectSpace.GetObjectByKey<Formula>(formula);
            if (form == null)
            {
                form = ObjectSpace.CreateObject<Formula>();
                form.Codice = formula;
                formule.Add(form);
            }

            form.Descrizione = string.Format("Formula {0}", formula);
            form.Note = qForm.Note;
            form.Lavorazione = ObjectSpace.GetObjectByKey<Lavorazione>(codiceLavorazione);
            form.ClasseMateriali = ObjectSpace.GetObjectByKey<Classe>(qForm.Classe);
            form.TipoFormula = (qForm.TipoFormula == "S") ? TipoFormula.Sostituzione : TipoFormula.Produzione;

            foreach (Componente cf in form.Ingredienti.ToArray<Componente>())
            {
                cf.Delete();
            }

            foreach (QComponente qc in qComponenti.Where<QComponente>(c => c.Formula == formula))  // formula.Componenti)
            {
                Componente cf = ObjectSpace.CreateObject<Componente>();
                cf.Articolo = articoli.FirstOrDefault<Articolo>(a => a.Codice == qc.Articolo);  // ObjectSpace.GetObjectByKey<Articolo>(qc.Articolo);
                if (cf.Articolo == null)
                {
                    QArticolo qArt = qArticoli.FirstOrDefault<QArticolo>(q => q.Codice == qc.Articolo);
                    cf.Articolo = ImportaArticolo(qArt.Codice);
                }
                cf.Modalità = ObjectSpace.GetObjectByKey<Modalità>(qc.Modalità);
                cf.Percentuale = Math.Abs(System.Convert.ToSingle(qc.Percentuale));
                cf.Stato = StatoComponente.Pronto;
                cf.Tolleranza = System.Convert.ToSingle(qc.Tolleranza);
                if (cf.Percentuale >= 0)
                    form.Ingredienti.Add(cf);
                else
                    form.Prodotti.Add(cf);
                form.Versione = qc.Versione.ToString();
            }
            form.Save();
            return form;
        }

        private Articolo ImportaArticolo(string codiceArticolo)
        {
            QArticolo articolo = qArticoli.FirstOrDefault<QArticolo>(a => a.Codice == codiceArticolo);
            Articolo art = articoli.FirstOrDefault<Articolo>(a => a.Codice == articolo.Codice); // ObjectSpace.GetObjectByKey<Articolo>(articolo.Codice);
            if (art == null)
            {
                art = ObjectSpace.CreateObject<Articolo>();
                art.Codice = articolo.Codice;
                articoli.Add(art);
            }
            art.Descrizione = articolo.Descrizione;
            art.Save();
            return art;
        }


    }
}
