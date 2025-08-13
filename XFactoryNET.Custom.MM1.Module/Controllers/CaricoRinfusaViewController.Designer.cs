namespace XFactoryNET.Custom.MM1.Module.Controllers
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
            this.mostraParametri = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.leggiTara = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.caricaRinfusa = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // mostraParametri
            // 
            this.mostraParametri.Caption = "Parametri";
            this.mostraParametri.Category = "View";
            this.mostraParametri.ConfirmationMessage = null;
            this.mostraParametri.Id = "actionMostraParametriCaricoRinfusa";
            this.mostraParametri.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.OdlCaricoRinfusa);
            this.mostraParametri.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.mostraParametri.ToolTip = "Visualizza parametri di lavorazione";
            this.mostraParametri.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.mostraParametri.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.MostraParametri_Execute);
            // 
            // leggiTara
            // 
            this.leggiTara.Caption = "Tara";
            this.leggiTara.ConfirmationMessage = null;
            this.leggiTara.Id = "leggiTara";
            this.leggiTara.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.leggiTara.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.OdlCaricoRinfusa);
            this.leggiTara.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.leggiTara.ToolTip = null;
            this.leggiTara.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.leggiTara.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.leggiTara_Execute);
            // 
            // caricaRinfusa
            // 
            this.caricaRinfusa.Caption = "Carica Rinfusa";
            this.caricaRinfusa.ConfirmationMessage = null;
            this.caricaRinfusa.Id = "caricaRinfusa";
            this.caricaRinfusa.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.caricaRinfusa.TargetObjectsCriteria = "[Lotto] Is Null";
            this.caricaRinfusa.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.Silos);
            this.caricaRinfusa.ToolTip = "Carica materiale alla rinfusa";
            this.caricaRinfusa.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.caricaRinfusa_Execute);
            // 
            // CaricoRinfusaViewController
            // 
            this.TypeOfView = typeof(DevExpress.ExpressApp.View);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction mostraParametri;
        private DevExpress.ExpressApp.Actions.SimpleAction leggiTara;
        private DevExpress.ExpressApp.Actions.SimpleAction caricaRinfusa;
    }
}
