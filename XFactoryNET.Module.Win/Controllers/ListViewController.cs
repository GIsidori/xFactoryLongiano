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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.ExpressApp.Model;
using System.IO;
using DevExpress.Utils.Serializing;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils;
using DevExpress.Utils.Serializing.Helpers;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace XFactoryNET.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class ListViewController : ViewController
    {

        private GridView grid;

        public ListViewController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            if (View is ListView)
            {
                ListView lv = (ListView)View;

                if (lv.Editor is GridListEditor)
                {
                    grid = ((GridListEditor)lv.Editor).GridView;
                    //grid.OptionsView.ColumnAutoWidth = ((IModelCustomListView)this.View.Model).ColumnAutoWidth;
                    
                    string settings = ((IModelCustomListView)this.View.Model).LayoutSettings;
                    if (string.IsNullOrEmpty(settings) == false)
                    {
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        System.IO.StreamWriter sw = new System.IO.StreamWriter(ms);
                        sw.Write(settings);
                        sw.Flush();
                        ms.Position = 0;
                        GridLayoutSerializer.LoadLayout(grid, ms);
                        ms.Close();
                        sw.Close();
                    }
                }
            }
        }


        protected override void OnActivated()
        {
            base.OnActivated();
            bool isGrid = ((ListView)View).Editor is GridListEditor;
            actionColumnAutoWidth.Active.SetItemValue("GridView", isGrid);
            actionFormatConditions.Active.SetItemValue("GridView", isGrid);

        }


        protected override void OnDeactivated()
        {
            //((IModelCustomListView)this.View.Model).ColumnAutoWidth = grid.OptionsView.ColumnAutoWidth;
            if (grid != null)
            {
                System.IO.MemoryStream ms = new MemoryStream();
                GridLayoutSerializer.SaveLayout(grid, ms);
                ms.Position = 0;
                System.IO.StreamReader sr = new System.IO.StreamReader(ms);
                string settings = sr.ReadToEnd();
                sr.Close();
                ms.Close();

                ((IModelCustomListView)this.View.Model).LayoutSettings = settings;
            }
          
            base.OnDeactivated();
        }

        private void actionColumnAutoWidth_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (grid != null)
                grid.OptionsView.ColumnAutoWidth = !grid.OptionsView.ColumnAutoWidth;
            
        }

        private void actionFormatConditions_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (grid != null)
            {
                frmFormatConditions frm = new frmFormatConditions(grid);
                frm.ShowDialog();
            }
        }


    }

    public class GridLayoutSerializer : XmlXtraSerializer
    {

       public static void SaveLayout(ColumnView view, Stream stream)
       {
           GridLayoutSerializer serializer = new GridLayoutSerializer();
           serializer.Serialize(stream, serializer.GetFilterProps(view), "View");
       }

       public static void LoadLayout(ColumnView view, Stream stream)
       {
           //DevExpress.Utils.OptionsLayoutGrid options = new OptionsLayoutGrid();
           //options.StoreAppearance = true;
           //options.StoreDataSettings = false;
           //options.StoreVisualOptions = false;
           //options.Columns.StoreAppearance = true;
           view.RestoreLayoutFromStream(stream, DevExpress.Utils.OptionsLayoutGrid.FullLayout);
       }

       protected XtraPropertyInfoCollection GetFilterProps(ColumnView view)
       {

           XtraPropertyInfoCollection store = new XtraPropertyInfoCollection();

           ArrayList propList = new ArrayList();

           PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(view);

           propList.Add(properties.Find("FormatConditions", false));

           propList.Add(properties.Find("OptionsView", false));

           MethodInfo mi = typeof(SerializeHelper).GetMethod("SerializeProperty", BindingFlags.NonPublic | BindingFlags.Instance);

           MethodInfo miGetXtraSerializableProperty = typeof(SerializeHelper).GetMethod("GetXtraSerializableProperty", BindingFlags.NonPublic | BindingFlags.Instance);

           SerializeHelper helper = new SerializeHelper(view);

           (view as IXtraSerializable).OnStartSerializing();

           foreach (PropertyDescriptor prop in propList)
           {
               XtraSerializableProperty newXtraSerializableProperty = miGetXtraSerializableProperty.Invoke(helper, new object[] { view, prop }) as XtraSerializableProperty;
               SerializablePropertyDescriptorPair p = new SerializablePropertyDescriptorPair(prop, newXtraSerializableProperty);
               mi.Invoke(helper, new object[] { store, view, p, XtraSerializationFlags.None, OptionsLayoutBase.FullLayout });
           }
           (view as IXtraSerializable).OnEndSerializing();
           return store;

       }

   }
}
