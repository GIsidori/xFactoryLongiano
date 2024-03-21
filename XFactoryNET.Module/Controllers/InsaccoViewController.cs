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

namespace XFactoryNET.Module.Controllers
{
    // For more information on Controllers and their life cycle, check out the http://documentation.devexpress.com/#Xaf/CustomDocument2621 and http://documentation.devexpress.com/#Xaf/CustomDocument3118 help articles.
    public partial class InsaccoViewController : ViewController
    {
        // Use this to do something when a Controller is instantiated (do not execute heavy operations here!).
        public InsaccoViewController()
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
            OnCurrentObjectChanged();
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
                simpleActionInsacco.Active["ApparatoValido"] = (apparato.Lotto != null && apparato.IsValidSource(OdlInsacco.IDLavorazione));

        }

        private void simpleActionInsacco_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace os = Application.CreateObjectSpace();
            OdlInsacco odl = os.CreateObject<OdlInsacco>();

            Silos silos = (Silos)os.GetObject(this.View.CurrentObject);

            //TODO

            e.ShowViewParameters.CreatedView = Application.CreateDetailView(os, odl, true);
            e.ShowViewParameters.CreateAllControllers = true;
            e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;

        }

    }
}
