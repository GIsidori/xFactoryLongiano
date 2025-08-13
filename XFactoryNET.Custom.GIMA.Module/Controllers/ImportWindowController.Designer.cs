namespace XFactoryNET.Custom.GIMA.Module.Controllers
{
    partial class ImportWindowController
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
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem1 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem2 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem3 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            this.singleChoiceActionImport = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // singleChoiceActionImport
            // 
            this.singleChoiceActionImport.Caption = "Importa";
            this.singleChoiceActionImport.Category = "Export";
            this.singleChoiceActionImport.ConfirmationMessage = "Confermi l\'importazione dei dati ?";
            this.singleChoiceActionImport.Id = "singleChoiceActionImport";
            choiceActionItem1.Caption = "Articoli, Formule, Sostituzioni e Allegati";
            choiceActionItem1.ImageName = null;
            choiceActionItem1.Shortcut = null;
            choiceActionItem1.ToolTip = null;
            choiceActionItem2.Caption = "Ordini di Produzione";
            choiceActionItem2.ImageName = null;
            choiceActionItem2.Shortcut = null;
            choiceActionItem2.ToolTip = null;
            choiceActionItem3.BeginGroup = true;
            choiceActionItem3.Caption = "Importa e aggiorna tutto";
            choiceActionItem3.ImageName = null;
            choiceActionItem3.Shortcut = null;
            choiceActionItem3.ToolTip = null;
            this.singleChoiceActionImport.Items.Add(choiceActionItem1);
            this.singleChoiceActionImport.Items.Add(choiceActionItem2);
            this.singleChoiceActionImport.Items.Add(choiceActionItem3);
            this.singleChoiceActionImport.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.singleChoiceActionImport.ShowItemsOnClick = true;
            this.singleChoiceActionImport.ToolTip = null;
            this.singleChoiceActionImport.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.singleChoiceActionImport_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction singleChoiceActionImport;
    }
}
