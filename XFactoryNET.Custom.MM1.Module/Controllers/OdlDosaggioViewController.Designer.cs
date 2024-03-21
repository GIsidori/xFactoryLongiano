namespace XFactoryNET.Custom.MM1.Module.Controllers
{
    partial class OdlDosaggioViewController
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
            this.MostraParametri = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // MostraParametri
            // 
            this.MostraParametri.Caption = "Parametri";
            this.MostraParametri.Category = "View";
            this.MostraParametri.ConfirmationMessage = null;
            this.MostraParametri.Id = "actionMostraParametriDosaggio";
            this.MostraParametri.ImageName = null;
            this.MostraParametri.Shortcut = null;
            this.MostraParametri.Tag = null;
            this.MostraParametri.TargetObjectsCriteria = null;
            this.MostraParametri.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.OdlDosaggio);
            this.MostraParametri.TargetViewId = null;
            this.MostraParametri.ToolTip = "Visualizza parametri di lavorazione";
            this.MostraParametri.TypeOfView = typeof(DevExpress.ExpressApp.View);
            this.MostraParametri.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.MostraParametri_Execute);
            // 
            // OdlDosaggioViewController
            // 
            this.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.OdlDosaggio);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction MostraParametri;
    }
}
