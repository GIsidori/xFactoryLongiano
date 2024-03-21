namespace XFactoryNET.Custom.MM1.Module.Controllers
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
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem3 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem4 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            this.singleChoiceActionImport = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.simpleActionImportaArticolo = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // singleChoiceActionImport
            // 
            this.singleChoiceActionImport.Caption = "Importa";
            this.singleChoiceActionImport.Category = "Export";
            this.singleChoiceActionImport.ConfirmationMessage = null;
            this.singleChoiceActionImport.Id = "singleChoiceActionImport";
            this.singleChoiceActionImport.ImageName = null;
            choiceActionItem3.Caption = "Articoli, Formule e Allegati";
            choiceActionItem3.ImageName = null;
            choiceActionItem3.Shortcut = null;
            choiceActionItem3.ToolTip = null;
            choiceActionItem4.Caption = "Ordini di Produzione";
            choiceActionItem4.ImageName = null;
            choiceActionItem4.Shortcut = null;
            choiceActionItem4.ToolTip = null;
            this.singleChoiceActionImport.Items.Add(choiceActionItem3);
            this.singleChoiceActionImport.Items.Add(choiceActionItem4);
            this.singleChoiceActionImport.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.singleChoiceActionImport.Shortcut = null;
            this.singleChoiceActionImport.ShowItemsOnClick = true;
            this.singleChoiceActionImport.Tag = null;
            this.singleChoiceActionImport.TargetObjectsCriteria = null;
            this.singleChoiceActionImport.TargetViewId = null;
            this.singleChoiceActionImport.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.singleChoiceActionImport.ToolTip = null;
            this.singleChoiceActionImport.TypeOfView = null;
            this.singleChoiceActionImport.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.singleChoiceActionImport_Execute);
            // 
            // simpleActionImportaArticolo
            // 
            this.simpleActionImportaArticolo.Caption = "Importa Articolo";
            this.simpleActionImportaArticolo.Category = "Export";
            this.simpleActionImportaArticolo.ConfirmationMessage = "Confermi l\'importazione degli articoli selezionati?";
            this.simpleActionImportaArticolo.Id = "simpleActionImportaArticolo";
            this.simpleActionImportaArticolo.ImageName = null;
            this.simpleActionImportaArticolo.Shortcut = null;
            this.simpleActionImportaArticolo.Tag = null;
            this.simpleActionImportaArticolo.TargetObjectsCriteria = null;
            this.simpleActionImportaArticolo.TargetObjectType = typeof(XFactoryNET.Custom.MM1.Module.BusinessObjects.QArticolo);
            this.simpleActionImportaArticolo.TargetViewId = null;
            this.simpleActionImportaArticolo.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.simpleActionImportaArticolo.ToolTip = null;
            this.simpleActionImportaArticolo.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.simpleActionImportaArticolo.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionImportaArticolo_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction singleChoiceActionImport;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionImportaArticolo;
    }
}
