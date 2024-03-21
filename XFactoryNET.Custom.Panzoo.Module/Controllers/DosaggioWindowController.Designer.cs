namespace XFactoryNET.Custom.Panzoo.Module.Controllers
{
    partial class DosaggioWindowController
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
            this.actionStatoLavorazione = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // actionStatoLavorazione
            // 
            this.actionStatoLavorazione.Caption = "Stato Lavorazione";
            this.actionStatoLavorazione.ConfirmationMessage = null;
            this.actionStatoLavorazione.Id = "StatoLavorazione";
            this.actionStatoLavorazione.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.OdlDosaggio);
            this.actionStatoLavorazione.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.actionStatoLavorazione.ToolTip = null;
            this.actionStatoLavorazione.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.actionStatoLavorazione_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction actionStatoLavorazione;
    }
}
