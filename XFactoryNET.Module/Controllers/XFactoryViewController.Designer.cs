namespace XFactoryNET.Module.Controllers
{
    partial class XFactoryViewController
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
            this.simpleActionSvuotaApparato = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.popupWindowShowActionCaricaApparato = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.popupWindowShowActionCaricaArticolo = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.popupWindowShowActionScaricaApparato = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // simpleActionSvuotaApparato
            // 
            this.simpleActionSvuotaApparato.Caption = "Svuota apparato";
            this.simpleActionSvuotaApparato.Category = "RecordEdit";
            this.simpleActionSvuotaApparato.ConfirmationMessage = "Confermi?";
            this.simpleActionSvuotaApparato.Id = "SvuotaApparato";
            this.simpleActionSvuotaApparato.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleActionSvuotaApparato.TargetObjectsCriteria = "[Articolo] Is Not Null";
            this.simpleActionSvuotaApparato.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.Silos);
            this.simpleActionSvuotaApparato.ToolTip = "Svuota il silos o l\'apparato selezionato";
            this.simpleActionSvuotaApparato.TypeOfView = typeof(DevExpress.ExpressApp.View);
            this.simpleActionSvuotaApparato.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionSvuota_Execute);
            // 
            // popupWindowShowActionCaricaApparato
            // 
            this.popupWindowShowActionCaricaApparato.AcceptButtonCaption = null;
            this.popupWindowShowActionCaricaApparato.CancelButtonCaption = null;
            this.popupWindowShowActionCaricaApparato.Caption = "Carica Materiale";
            this.popupWindowShowActionCaricaApparato.Category = "RecordEdit";
            this.popupWindowShowActionCaricaApparato.ConfirmationMessage = null;
            this.popupWindowShowActionCaricaApparato.Id = "CaricaApparato";
            this.popupWindowShowActionCaricaApparato.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.popupWindowShowActionCaricaApparato.TargetObjectsCriteria = "";
            this.popupWindowShowActionCaricaApparato.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.Silos);
            this.popupWindowShowActionCaricaApparato.ToolTip = null;
            this.popupWindowShowActionCaricaApparato.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionCaricaApparato_CustomizePopupWindowParams);
            this.popupWindowShowActionCaricaApparato.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionCaricaApparato_Execute);
            // 
            // popupWindowShowActionCaricaArticolo
            // 
            this.popupWindowShowActionCaricaArticolo.AcceptButtonCaption = null;
            this.popupWindowShowActionCaricaArticolo.CancelButtonCaption = null;
            this.popupWindowShowActionCaricaArticolo.Caption = "Carica Articolo";
            this.popupWindowShowActionCaricaArticolo.Category = "RecordEdit";
            this.popupWindowShowActionCaricaArticolo.ConfirmationMessage = null;
            this.popupWindowShowActionCaricaArticolo.Id = "CaricaArticolo";
            this.popupWindowShowActionCaricaArticolo.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.popupWindowShowActionCaricaArticolo.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.Articolo);
            this.popupWindowShowActionCaricaArticolo.ToolTip = null;
            this.popupWindowShowActionCaricaArticolo.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionCaricaArticolo_CustomizePopupWindowParams);
            this.popupWindowShowActionCaricaArticolo.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionCaricaArticolo_Execute);
            // 
            // popupWindowShowActionScaricaApparato
            // 
            this.popupWindowShowActionScaricaApparato.AcceptButtonCaption = null;
            this.popupWindowShowActionScaricaApparato.CancelButtonCaption = null;
            this.popupWindowShowActionScaricaApparato.Caption = "Scarica Materiale";
            this.popupWindowShowActionScaricaApparato.Category = "RecordEdit";
            this.popupWindowShowActionScaricaApparato.ConfirmationMessage = null;
            this.popupWindowShowActionScaricaApparato.Id = "ScaricaApparato";
            this.popupWindowShowActionScaricaApparato.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.popupWindowShowActionScaricaApparato.TargetObjectsCriteria = "[Articolo] IS NOT NULL";
            this.popupWindowShowActionScaricaApparato.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.Silos);
            this.popupWindowShowActionScaricaApparato.ToolTip = null;
            this.popupWindowShowActionScaricaApparato.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionScaricaApparato_CustomizePopupWindowParams);
            this.popupWindowShowActionScaricaApparato.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionScaricaApparato_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionSvuotaApparato;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionCaricaApparato;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionCaricaArticolo;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionScaricaApparato;
    }
}
