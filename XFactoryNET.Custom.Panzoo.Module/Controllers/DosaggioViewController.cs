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
using XFactoryNET.Custom.Panzoo.Module.BusinessObjects;
using DevExpress.ExpressApp.Xpo;
using System.ComponentModel;

namespace XFactoryNET.Custom.Panzoo.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class DosaggioViewController : ViewController
    {
        Odl odl = null;
        System.Timers.Timer timer;

        public DosaggioViewController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.

            
            if (this.View.ObjectTypeInfo == null || (this.View.ObjectTypeInfo.Type != typeof(OdlDosaggio) && !this.View.ObjectTypeInfo.Type.IsSubclassOf(typeof(BaseArticolo))))
            {
                this.popupWindowShowActionMostraParametri.Active["type"] = false;
            }

            odl = this.View.CurrentObject as Odl;
            if (odl != null)
            {
                odl.Avviato += new EventHandler(odl_Avviato);
                odl.InAvvio += new EventHandler<System.ComponentModel.CancelEventArgs>(odl_InAvvio);
                odl.Predisposto += new EventHandler<CancelEventArgs>(odl_Predisposto);
            }

            if (this.View.Id == "StatoDosaggio_DetailView")
            {
                Frame.GetController<RefreshController>().Actions["Refresh"].Active["NonStatoDosaggio"] = false;
            }

            //if (this.View.Id == "DosaggioDashboardView")
            //{
            //    DashboardViewItem viewItem = ((DashboardView)View).FindItem("StatoDosaggioDashboardItem") as DashboardViewItem;
            //    viewItem.ControlCreated += new EventHandler<EventArgs>(viewItem_ControlCreated);
            //}

            
        }

        private void popupWindowShowActionMostraParametri_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace os = this.ObjectSpace;
            object param = GetParametriLavorazione(os);
            if (param != null)
            {
                e.View = Application.CreateDetailView(os, param, false);
                e.DialogController.SaveOnAccept = false;
                e.DialogController.CancelAction.Active["CanCancel"] = false;
            }
        }


        void odl_Predisposto(object sender, CancelEventArgs e)
        {
            IObjectSpace os = this.ObjectSpace;
            object pDos = GetParametriLavorazione(os);
            if (pDos != null)
            {
                ShowViewParameters svp = new ShowViewParameters();
                svp.CreatedView = Application.CreateDetailView(os, pDos, false);
                svp.TargetWindow = TargetWindow.NewModalWindow;
                svp.Context = TemplateContext.PopupWindow;
                DialogController dlg = Application.CreateController<DialogController>();
                dlg.SaveOnAccept = false;
                dlg.CancelAction.Active["CanCancel"] = false;

                svp.Controllers.Add(dlg);

                Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));

            }
        }

        private object GetParametriLavorazione(IObjectSpace os)
        {
            ParametriDosaggio param = null;
            if (View.CurrentObject.GetType() == typeof(OdlDosaggio))
            {
                OdlDosaggio odlDosaggio = (OdlDosaggio)os.GetObject(View.CurrentObject);
                param = os.FindObject<ParametriDosaggio>(new BinaryOperator("Odl", odlDosaggio));
                if (param == null)
                {
                    param = os.CreateObject<ParametriDosaggio>();
                    param.Odl = odlDosaggio;
                    if (odl.Articolo != null)
                    {
                        BaseArticolo art = (BaseArticolo)os.GetObject(odl.Articolo);
                        ParametriDosaggio p2 = os.FindObject<ParametriDosaggio>(new BinaryOperator("Articolo", art));
                        if (p2 != null)
                        {
                            param.DS1 = p2.DS1;
                            param.GranMenù = p2.GranMenù;
                        }
                    }
                }
            }

            if (View.CurrentObject.GetType().IsSubclassOf(typeof(BaseArticolo)))
            {
                BaseArticolo art = (BaseArticolo)os.GetObject(View.CurrentObject);
                param = os.FindObject<ParametriDosaggio>(new BinaryOperator("Articolo", art));
                if (param == null)
                {
                    param = os.CreateObject<ParametriDosaggio>();
                    param.Articolo = art;
                }
            }

            return param;
        }

        //void viewItem_ControlCreated(object sender, EventArgs e)
        //{
        //    DetailView detailView = (DetailView)((DashboardViewItem)sender).Frame.View;
        //    detailView.ViewEditMode = ViewEditMode.Edit;
        //    detailView.CurrentObject = detailView.ObjectSpace.CreateObject<StatoDosaggio>();

        //    timer = new System.Timers.Timer(500);
        //    timer.SynchronizingObject = (System.ComponentModel.ISynchronizeInvoke)Frame.Template;
        //    timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
        //    timer.Start();

        //}


        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.

            if (this.View.Id == "StatoDosaggio_DetailView")
            {
                this.View.CurrentObject = this.View.ObjectSpace.CreateObject<StatoDosaggio>();

                timer = new System.Timers.Timer(500);
                timer.SynchronizingObject = (System.ComponentModel.ISynchronizeInvoke)Frame.Template;
                timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
                timer.Start();

            }

        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            odl = this.View.CurrentObject as OdlDosaggio;
            if (odl != null)
            {
                odl.InAvvio -= new EventHandler<System.ComponentModel.CancelEventArgs>(odl_InAvvio);
                odl.Avviato -= new EventHandler(odl_Avviato);
                odl.Predisposto -= new EventHandler<CancelEventArgs>(odl_Predisposto);
            }
            //if (this.View.Id == "DosaggioDashboardView")
            if (this.View.Id == "StatoDosaggio_DetailView")
            {
                timer.Elapsed -= new System.Timers.ElapsedEventHandler(timer_Elapsed);
                timer.Stop();
                timer.Dispose();
                Frame.GetController<RefreshController>().Actions["Refresh"].Active["NonStatoDosaggio"] = true;
            }


            base.OnDeactivated();
        }

        //delegate void refreshCallback();

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //System.ComponentModel.ISynchronizeInvoke synchInvoke = (System.ComponentModel.ISynchronizeInvoke)this.Frame.Template;
            //if (synchInvoke.InvokeRequired)
            //{
            //    refreshCallback callBack = new refreshCallback(RefreshData);
            //    synchInvoke.Invoke(callBack, new object[] { });
            //}
            //else
                RefreshData();
        }

        void RefreshData()
        {
            //if (this.View != null && this.View.Id == "DosaggioDashboardView")
            //{
            //    DashboardViewItem viewItem = ((DashboardView)View).FindItem("StatoDosaggioDashboardItem") as DashboardViewItem;
            //    if (viewItem.Frame != null)
            //    {
            //        DetailView detailView = (DetailView)viewItem.Frame.View;
            //        StatoDosaggio dos = detailView.CurrentObject as StatoDosaggio;
            //        if (dos != null)
            //            dos.RefreshData();
            //    }
            //}
            //if (this.View != null && this.View.Id == "StatoDosaggio_DetailView")
            //{
            //    StatoDosaggio dos = View.CurrentObject as StatoDosaggio;
            //    if (dos != null)
            //        dos.RefreshData();

            //}

            if (this.View != null)
            {
                StatoDosaggio dos = View.CurrentObject as StatoDosaggio;
                if (dos != null)
                {
                    dos.RefreshData();
                    actionStopDosaggio.Enabled["Running"] = (dos.Running);
                    actionAbort.Enabled["Running"] = dos.Running;
                }
            }
        }

        void odl_InAvvio(object sender, CancelEventArgs args)
        {
            //popupWindowShowActionMostraParametri.DoExecute((Window)this.Frame);   
        }

        void odl_Avviato(object sender, EventArgs e)
        {
            Odl odl = (Odl)sender;
            switch (odl.Lavorazione.Codice)
            {
                case OdlDosaggio.IDLavorazione:
                    //
                    //Non fa nulla poichè il server cicla periodicamente per OdlAvviati
                    //
                    //Services.Dosaggio.SvcDosaggioClient svc = null;
                    //try
                    //{
                    //    svc = new Services.Dosaggio.SvcDosaggioClient();
                    //    svc.Avvia(odl.Oid);
                    //    svc.Close();
                    //}
                    //finally
                    //{
                    //    if (svc != null)
                    //    {
                    //        svc.Close();
                    //        ((IDisposable)svc).Dispose();
                    //        svc = null;
                    //    }
                    //}
                    break;
                default:
                    break;
            }

        }


        private void actionAbort_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            StatoDosaggio dos = View.CurrentObject as StatoDosaggio;
            if (dos == null)
            {
                IObjectSpace os = Application.CreateObjectSpace();
                dos = os.CreateObject<StatoDosaggio>();
            }
            dos.Arresta();
        }

        private void actionStopDosaggio_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {
            StatoDosaggio dos = (StatoDosaggio)View.CurrentObject;
            if (dos.Odl != null)
            {
                if (e.ParameterCurrentValue != null)
                {
                    int nrMisc = (int)e.ParameterCurrentValue;
                    if (nrMisc < dos.Odl.NumeroMiscelate)
                        dos.Arresta(nrMisc);
                }
            }
        }


        private void popupWindowShowActionMostraParametri_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {

        }

        private void simpleActionStatoDosaggio_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace os = Application.CreateObjectSpace();
            StatoDosaggio dos = os.CreateObject<StatoDosaggio>();
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(os, dos, true);
            e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Default;
            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            e.ShowViewParameters.CreateAllControllers = true;
        }

    }
}
