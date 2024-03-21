namespace XFactoryNET.Custom.GIMA.Module.Controllers
{
    partial class ImportViewController
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
            this.actionImportaArticolo = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.actionImportaFormula = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.actionImportaAllegato = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // actionImportaArticolo
            // 
            this.actionImportaArticolo.Caption = "Importa Articolo";
            this.actionImportaArticolo.Category = "RecordEdit";
            this.actionImportaArticolo.ConfirmationMessage = "Confermi l\'importazione degli Articoli selezionati?";
            this.actionImportaArticolo.Id = "ImportaArticolo";
            this.actionImportaArticolo.TargetObjectType = typeof(XFactoryNET.Custom.GIMA.Module.BusinessObjects.TBArticolo);
            this.actionImportaArticolo.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.actionImportaArticolo.ToolTip = null;
            this.actionImportaArticolo.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.actionImportaArticolo.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionImportaArticolo_Execute);
            // 
            // actionImportaFormula
            // 
            this.actionImportaFormula.Caption = "Importa Formula";
            this.actionImportaFormula.Category = "RecordEdit";
            this.actionImportaFormula.ConfirmationMessage = "Confermi l\'importazione delle Formule selezionate";
            this.actionImportaFormula.Id = "ImportaFormula";
            this.actionImportaFormula.TargetObjectType = typeof(XFactoryNET.Custom.GIMA.Module.BusinessObjects.TBFormula);
            this.actionImportaFormula.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.actionImportaFormula.ToolTip = null;
            this.actionImportaFormula.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.actionImportaFormula.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.actionImportaFormula_Execute);
            // 
            // actionImportaAllegato
            // 
            this.actionImportaAllegato.Caption = "Importa Allegato";
            this.actionImportaAllegato.Category = "RecordEdit";
            this.actionImportaAllegato.ConfirmationMessage = "Confermi l\'importazione delle Formule selezionate";
            this.actionImportaAllegato.Id = "ImportaAllegato";
            this.actionImportaAllegato.TargetObjectType = typeof(XFactoryNET.Custom.GIMA.Module.BusinessObjects.TBAllegato);
            this.actionImportaAllegato.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.actionImportaAllegato.ToolTip = null;
            this.actionImportaAllegato.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.actionImportaAllegato.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.actionImportaAllegato_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction actionImportaArticolo;
        private DevExpress.ExpressApp.Actions.SimpleAction actionImportaFormula;
        private DevExpress.ExpressApp.Actions.SimpleAction actionImportaAllegato;
    }
}
