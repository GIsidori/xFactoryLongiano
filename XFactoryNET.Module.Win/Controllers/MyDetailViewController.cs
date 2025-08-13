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
using DevExpress.ExpressApp.Win.Editors;

namespace XFactoryNET.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class MyDetailViewController : ViewController<DetailView>
    {
        public MyDetailViewController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            //foreach (LookupPropertyEditor lookup in View.GetItems<LookupPropertyEditor>())
            //{
            //    lookup.ControlCreated += new EventHandler<EventArgs>(lookup_ControlCreated);
            //}
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

        //void lookup_ControlCreated(object sender, EventArgs e)
        //{
        //    LookupPropertyEditor lpe = (LookupPropertyEditor)sender;
        //    lpe.Control.Popup += new EventHandler(Control_Popup);
        //}

        //void Control_Popup(object sender, EventArgs e)
        //{
        //    LookupEdit lookup = (LookupEdit)sender;
        //    ListView lv = (ListView)lookup.Frame.View;
        //    foreach (var item in lv.CollectionSource.List)
        //    {
        //        lv.ObjectSpace.ReloadObject(item);
        //    }
        //    //lv.CollectionSource.Reload();
        //}

    }
}
