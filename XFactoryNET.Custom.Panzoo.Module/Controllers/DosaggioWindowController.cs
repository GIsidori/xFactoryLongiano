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
using System.ComponentModel;
using DevExpress.ExpressApp.Xpo;

namespace XFactoryNET.Custom.Panzoo.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppWindowControllertopic.
    public partial class DosaggioWindowController : WindowController
    {
        System.Timers.Timer timer;
        public DosaggioWindowController()
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


        private void actionStatoLavorazione_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            XPObjectSpace os = (XPObjectSpace) Application.CreateObjectSpace(typeof(BusinessObjects.StatoDosaggio));
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(os, new BusinessObjects.StatoDosaggio(os.Session));
            e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Default;
            e.ShowViewParameters.TargetWindow = TargetWindow.Default;
        }
    }
}
