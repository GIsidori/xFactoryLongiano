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
using DevExpress.ExpressApp.Win.Templates;
using DevExpress.XtraBars;

namespace XFactoryNET.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppWindowControllertopic.
    public partial class MyWindowController : WindowController
    {
        public MyWindowController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Windows (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target Window.
            BarActionItemsFactory.CustomizeActionControl += new EventHandler<DevExpress.ExpressApp.Win.Templates.ActionContainers.CustomizeActionControlEventArgs>(BarActionItemsFactory_CustomizeActionControl);
        }

        void BarActionItemsFactory_CustomizeActionControl(object sender, DevExpress.ExpressApp.Win.Templates.ActionContainers.CustomizeActionControlEventArgs e)
        {
            //if (e.Action.Id == "AvviaOdl")
            //{
            //    BarButtonItem barItem = (BarButtonItem) e.ActionControl.Control;
            //    barItem.ButtonStyle = BarButtonStyle.Check;
            //}
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
            BarActionItemsFactory.CustomizeActionControl -= new EventHandler<DevExpress.ExpressApp.Win.Templates.ActionContainers.CustomizeActionControlEventArgs>(BarActionItemsFactory_CustomizeActionControl);
        }
    }
}
