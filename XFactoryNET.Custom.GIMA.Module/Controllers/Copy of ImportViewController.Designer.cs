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
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem5 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem6 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem7 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem8 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            this.singleChoiceActionImport = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.actionImportaArticolo = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.actionImportaFormula = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // singleChoiceActionImport
            // 
            this.singleChoiceActionImport.Caption = "Importa";
            this.singleChoiceActionImport.Category = "Export";
            this.singleChoiceActionImport.ConfirmationMessage = null;
            this.singleChoiceActionImport.Id = "singleChoiceActionImport";
            choiceActionItem5.Caption = "Articoli";
            choiceActionItem5.ImageName = null;
            choiceActionItem5.Shortcut = null;
            choiceActionItem5.ToolTip = null;
            choiceActionItem6.Caption = "Formule";
            choiceActionItem6.ImageName = null;
            choiceActionItem6.Shortcut = null;
            choiceActionItem6.ToolTip = null;
            choiceActionItem7.Caption = "Allegati";
            choiceActionItem7.ImageName = null;
            choiceActionItem7.Shortcut = null;
            choiceActionItem7.ToolTip = null;
            choiceActionItem8.Caption = "Ordini di Produzione";
            choiceActionItem8.ImageName = null;
            choiceActionItem8.Shortcut = null;
            choiceActionItem8.ToolTip = null;
            this.singleChoiceActionImport.Items.Add(choiceActionItem5);
            this.singleChoiceActionImport.Items.Add(choiceActionItem6);
            this.singleChoiceActionImport.Items.Add(choiceActionItem7);
            this.singleChoiceActionImport.Items.Add(choiceActionItem8);
            this.singleChoiceActionImport.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.singleChoiceActionImport.ShowItemsOnClick = true;
            this.singleChoiceActionImport.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.singleChoiceActionImport.ToolTip = null;
            this.singleChoiceActionImport.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.singleChoiceActionImport_Execute);
            // 
            // actionImportaArticolo
            // 
            this.actionImportaArticolo.Caption = "Importa Articolo";
            this.actionImportaArticolo.Category = "RecordEdit";
            this.actionImportaArticolo.ConfirmationMessage = "Confermi l\'importazione degli Articoli selezionati?";
            this.actionImportaArticolo.Id = "ImportaArticolo";
            this.actionImportaArticolo.TargetObjectType = typeof(XFactoryNET.Custom.GIMA.Module.BusinessObjects.QArticolo);
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
            this.actionImportaFormula.TargetObjectType = typeof(XFactoryNET.Custom.GIMA.Module.BusinessObjects.QFormula);
            this.actionImportaFormula.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.actionImportaFormula.ToolTip = null;
            this.actionImportaFormula.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.actionImportaFormula.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.actionImportaFormula_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction singleChoiceActionImport;
        private DevExpress.ExpressApp.Actions.SimpleAction actionImportaArticolo;
        private DevExpress.ExpressApp.Actions.SimpleAction actionImportaFormula;
    }
}
