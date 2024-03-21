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
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.Xpo;
using XFactoryNET.Custom.GIMA.Module.BusinessObjects;
using DevExpress.ExpressApp.Xpo;

namespace XFactoryNET.Custom.GIMA.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppWindowControllertopic.
    public partial class ImportWindowController : WindowController
    {
        public ImportWindowController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Windows (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target Window.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void singleChoiceActionImport_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            Session session = ((XPObjectSpace)objectSpace).Session;

            switch (e.SelectedChoiceActionItem.Id)
            {
                case "Formule":
                    //e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
                    //e.ShowViewParameters.CreatedView = Application.CreateListView(objectSpace, typeof(TBFormula), true);     // ShowListView<QFormula>(QFormula.GetObjects(session),objectSpace);
                    session.ExecuteSproc("ImportaFormule");
                    break;
                case "Articoli":
                    //e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
                    //e.ShowViewParameters.CreatedView = Application.CreateListView(objectSpace, typeof(TBArticolo), true);    // ShowListView<QArticolo>(QArticolo.GetObjects(session),objectSpace);
                    session.ExecuteSproc("ImportaArticoli");
                    break;
                case "Allegati":
                    //e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
                    //e.ShowViewParameters.CreatedView = Application.CreateListView(objectSpace, typeof(TBAllegato), true);    // ShowListView<QAllegato>(QAllegato.GetObjects(session),objectSpace);
                    session.ExecuteSproc("ImportaAllegati");
                    break;
                case "Ordini di Produzione":
                    session.ExecuteSproc("ImportaOrdini");
                    break;
                case "Importa e aggiorna tutto":
                    session.ExecuteSproc("ImportaTutto");
                    break;

            }

        }

    }
}
