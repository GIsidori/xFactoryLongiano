namespace XFactoryNET.Custom.Panzoo.Module.Controllers
{
    partial class DosaggioViewController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.popupWindowShowActionMostraParametri = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.simpleActionStatoDosaggio = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.actionAbort = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.actionStopDosaggio = new DevExpress.ExpressApp.Actions.ParametrizedAction(this.components);
            // 
            // popupWindowShowActionMostraParametri
            // 
            this.popupWindowShowActionMostraParametri.AcceptButtonCaption = null;
            this.popupWindowShowActionMostraParametri.CancelButtonCaption = null;
            this.popupWindowShowActionMostraParametri.Caption = "Parametri";
            this.popupWindowShowActionMostraParametri.Category = "View";
            this.popupWindowShowActionMostraParametri.ConfirmationMessage = null;
            this.popupWindowShowActionMostraParametri.Id = "MostraParametriOdL";
            this.popupWindowShowActionMostraParametri.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.popupWindowShowActionMostraParametri.ToolTip = "Visualizza parametri di lavorazione";
            this.popupWindowShowActionMostraParametri.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.popupWindowShowActionMostraParametri.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionMostraParametri_CustomizePopupWindowParams);
            this.popupWindowShowActionMostraParametri.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionMostraParametri_Execute);
            // 
            // simpleActionStatoDosaggio
            // 
            this.simpleActionStatoDosaggio.Caption = "Stato Dosaggio";
            this.simpleActionStatoDosaggio.Category = "RecordEdit";
            this.simpleActionStatoDosaggio.ConfirmationMessage = null;
            this.simpleActionStatoDosaggio.Id = "StatoDosaggio";
            this.simpleActionStatoDosaggio.ImageName = "ModelEditor_Settings";
            this.simpleActionStatoDosaggio.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.OdlDosaggio);
            this.simpleActionStatoDosaggio.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleActionStatoDosaggio.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.simpleActionStatoDosaggio.ToolTip = null;
            this.simpleActionStatoDosaggio.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.simpleActionStatoDosaggio.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionStatoDosaggio_Execute);
            // 
            // actionAbort
            // 
            this.actionAbort.Caption = "Arresta Dosaggio";
            this.actionAbort.Category = "RecordEdit";
            this.actionAbort.ConfirmationMessage = "Confermi l\'arresto della produzione in corso?";
            this.actionAbort.Id = "AbortDosaggio";
            this.actionAbort.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.actionAbort.TargetObjectsCriteria = "";
            this.actionAbort.TargetObjectType = typeof(XFactoryNET.Custom.Panzoo.Module.BusinessObjects.StatoDosaggio);
            this.actionAbort.ToolTip = null;
            this.actionAbort.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.actionAbort_Execute);
            // 
            // actionStopDosaggio
            // 
            this.actionStopDosaggio.Caption = "Stop Dosaggio";
            this.actionStopDosaggio.Category = "RecordEdit";
            this.actionStopDosaggio.ConfirmationMessage = "Confermi l\'arresto della produzione alla miscelata selezionata?";
            this.actionStopDosaggio.Id = "StopDosaggio";
            this.actionStopDosaggio.NullValuePrompt = null;
            this.actionStopDosaggio.ShortCaption = null;
            this.actionStopDosaggio.TargetObjectType = typeof(XFactoryNET.Custom.Panzoo.Module.BusinessObjects.StatoDosaggio);
            this.actionStopDosaggio.ToolTip = null;
            this.actionStopDosaggio.ValueType = typeof(int);
            this.actionStopDosaggio.Execute += new DevExpress.ExpressApp.Actions.ParametrizedActionExecuteEventHandler(this.actionStopDosaggio_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction actionAbort;
        private DevExpress.ExpressApp.Actions.ParametrizedAction actionStopDosaggio;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionMostraParametri;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionStatoDosaggio;
    }
}
