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
using XFactoryNET.Module.BusinessObjects;
using XFactoryNET.Custom.MM1.Module.BusinessObjects;

namespace XFactoryNET.Custom.MM1.Module.Controllers
{
    // For more information on Controllers and their life cycle, check out the http://documentation.devexpress.com/#Xaf/CustomDocument2621 and http://documentation.devexpress.com/#Xaf/CustomDocument3118 help articles.
    public partial class CaricoRinfusaViewController : ViewController
    {
        // Use this to do something when a Controller is instantiated (do not execute heavy operations here!).
        public CaricoRinfusaViewController()
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
            this.OnCurrentObjectChanged();
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


        View prevView = null;
        protected override void OnViewChanged()
        {
            base.OnViewChanged();
            if (prevView != null)
                prevView.CurrentObjectChanged -= new EventHandler(View_CurrentObjectChanged);
            prevView = View;
            View.CurrentObjectChanged += new EventHandler(View_CurrentObjectChanged);
        }

        void View_CurrentObjectChanged(object sender, EventArgs e)
        {
            OnCurrentObjectChanged();
        }

        private void OnCurrentObjectChanged()
        {
            Apparato apparato = this.View.CurrentObject as Apparato;
            if (apparato != null)
                this.caricaRinfusa.Active["ApparatoValido"] = (apparato.IsValidDestination(OdlCaricoRinfusa.IDLavorazione));
        }


        private void MostraParametri_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace os = Application.CreateObjectSpace();
            Odl odl = (Odl) os.GetObject(e.CurrentObject);
            ParametriCaricoRinfusa param = os.FindObject<ParametriCaricoRinfusa>(new BinaryOperator("Odl", odl));
            if (param == null)
                param = os.CreateObject<ParametriCaricoRinfusa>();
            param.Odl = odl;
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(os, param, true);
            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;

        }

        private void popupWindowShowActionCaricaRinfusa_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace os = Application.CreateObjectSpace();
            OdlCaricoRinfusa odl = os.CreateObject<OdlCaricoRinfusa>();
            Silos apparato = (Silos)os.GetObject(View.CurrentObject);
            odl.Destinazione = apparato;
            e.View = Application.CreateDetailView(os, odl, true);
            e.DialogController.SaveOnAccept = false;

        }

        private void popupWindowShowActionCaricaRinfusa_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            OdlCaricoRinfusa odl = (OdlCaricoRinfusa)e.PopupWindow.View.CurrentObject;
            if (View is DetailView && ((DetailView)View).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.View)
                View.ObjectSpace.CommitChanges();

            Services.Odl.SvcOdlClient svc = new Services.Odl.SvcOdlClient();
            svc.AvviaOdl(odl.Oid);
            svc.Close();
        }

        private void leggiTara_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            OdlCaricoRinfusa odl = (OdlCaricoRinfusa)e.CurrentObject;
            Pesa pesa = ((Pesa)odl.ApparatoLavorazione);

            Services.Pesa.SvcPesaClient svc = new Services.Pesa.SvcPesaClient();
            float peso = svc.LeggiPeso(pesa.Codice);
            //odl.Tara = peso;
            svc.Close();
        }

        private void caricaRinfusa_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace os = Application.CreateObjectSpace();  // View.ObjectSpace;
            OdlCaricoRinfusa odl = os.CreateObject<OdlCaricoRinfusa>();
            Silos apparato = (Silos)os.GetObject(View.CurrentObject);
            odl.Destinazione = apparato;
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(os, odl, true);
        }


    }
}
