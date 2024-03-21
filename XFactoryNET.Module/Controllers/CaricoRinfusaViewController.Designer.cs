namespace XFactoryNET.Module.Controllers
{
    partial class CaricoRinfusaViewController
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
            this.simpleActionSvuota = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.popupWindowShowActionCarica = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // simpleActionSvuota
            // 
            this.simpleActionSvuota.Caption = "Svuota apparato";
            this.simpleActionSvuota.ConfirmationMessage = "Confermi?";
            this.simpleActionSvuota.Id = "SvuotaApparato";
            this.simpleActionSvuota.ImageName = null;
            this.simpleActionSvuota.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleActionSvuota.Shortcut = null;
            this.simpleActionSvuota.Tag = null;
            this.simpleActionSvuota.TargetObjectsCriteria = "[Lotto] Is Not Null";
            this.simpleActionSvuota.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.Apparato);
            this.simpleActionSvuota.TargetViewId = null;
            this.simpleActionSvuota.ToolTip = "Svuota il silos o l\'apparato selezionato";
            this.simpleActionSvuota.TypeOfView = null;
            this.simpleActionSvuota.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionSvuota_Execute);
            // 
            // popupWindowShowActionCarica
            // 
            this.popupWindowShowActionCarica.AcceptButtonCaption = null;
            this.popupWindowShowActionCarica.CancelButtonCaption = null;
            this.popupWindowShowActionCarica.Caption = "Carica Materiale";
            this.popupWindowShowActionCarica.ConfirmationMessage = null;
            this.popupWindowShowActionCarica.Id = "CaricaApparato";
            this.popupWindowShowActionCarica.ImageName = null;
            this.popupWindowShowActionCarica.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.popupWindowShowActionCarica.Shortcut = null;
            this.popupWindowShowActionCarica.Tag = null;
            this.popupWindowShowActionCarica.TargetObjectsCriteria = null;
            this.popupWindowShowActionCarica.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.Apparato);
            this.popupWindowShowActionCarica.TargetViewId = null;
            this.popupWindowShowActionCarica.ToolTip = "Carica un nuovo lotto nel silos o apparato selezionato";
            this.popupWindowShowActionCarica.TypeOfView = null;
            this.popupWindowShowActionCarica.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionCarica_CustomizePopupWindowParams);
            this.popupWindowShowActionCarica.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionCarica_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionSvuota;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionCarica;
    }
}
