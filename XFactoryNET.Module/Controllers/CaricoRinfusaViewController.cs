using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Actions;
using XFactoryNET.Module.BusinessObjects;

namespace XFactoryNET.Module.Controllers
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


        private void simpleActionSvuota_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Apparato apparato = (Apparato)View.CurrentObject;
            apparato.Svuota();
            if (View is DetailView && ((DetailView)View).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.View)
                View.ObjectSpace.CommitChanges();

        }

        private void popupWindowShowActionCarica_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace os = View.ObjectSpace;
            Lotto lot = os.CreateObject<Lotto>();
            lot.Apparato = (Apparato) os.GetObject(View.CurrentObject);
            e.View = Application.CreateDetailView(os, lot, false);
            e.DialogController.SaveOnAccept = false;
        }

        private void popupWindowShowActionCarica_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
        }

    }
}
