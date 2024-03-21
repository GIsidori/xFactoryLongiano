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
using XFactoryNET.Module.BusinessObjects;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Validation.AllContextsView;

namespace XFactoryNET.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class XFactoryViewController : ViewController
    {
        public XFactoryViewController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void simpleActionSvuota_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Silos silos = (Silos)View.CurrentObject;
            silos.Svuota();
            //if (View is DetailView && ((DetailView)View).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.View)
            View.ObjectSpace.CommitChanges();
        }


        private void actionCaricaQuantità_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {
            IObjectSpace os = View.ObjectSpace; //Application.CreateObjectSpace();  
            Silos silos = (Silos)os.GetObject(View.CurrentObject);
            
            if (silos.Articolo != null)
                silos.Quantità += (int)e.ParameterCurrentValue;
        }





        private void popupWindowShowActionCaricaApparato_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace os = this.View.ObjectSpace;    // Application.CreateObjectSpace();
            Silos app = (Silos)os.GetObject(this.View.CurrentObject);
            Lotto lot = os.CreateObject<Lotto>();
            lot.Articolo = app.Articolo;

            e.View = Application.CreateDetailView(os, lot, false);
            e.DialogController.SaveOnAccept = false;
        }

        private void popupWindowShowActionCaricaApparato_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            Lotto lot = (Lotto) e.PopupWindowViewCurrentObject;
            Silos app = (Silos)e.CurrentObject;

            app.RegistraMovimento(lot,TipoMovimento.EntrataMerci);

            View.ObjectSpace.CommitChanges();
            
            //View.Refresh();
        }

        private void popupWindowShowActionCaricaArticolo_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace os = Application.CreateObjectSpace();
            Articolo art = (Articolo)os.GetObject(this.View.CurrentObject);
            Lotto lot = os.CreateObject<Lotto>();
            lot.Articolo = art;
            e.View = Application.CreateDetailView(os, lot, true);
            e.DialogController.SaveOnAccept = true;
        }

        private void popupWindowShowActionCaricaArticolo_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            View.Refresh();
        }

        private void popupWindowShowActionScaricaApparato_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace os = this.View.ObjectSpace;    // Application.CreateObjectSpace();
            Silos app = (Silos)os.GetObject(this.View.CurrentObject);
            Lotto lot = os.CreateObject<Lotto>();
            lot.Articolo = app.Articolo;

            e.View = Application.CreateDetailView(os, lot, false);
            e.DialogController.SaveOnAccept = false;

        }

        private void popupWindowShowActionScaricaApparato_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            Lotto lot = (Lotto)e.PopupWindowViewCurrentObject;
            Silos app = (Silos)e.CurrentObject;

            app.RegistraMovimento(lot,TipoMovimento.UscitaMerci);

            View.ObjectSpace.CommitChanges();

        }


    }
}
