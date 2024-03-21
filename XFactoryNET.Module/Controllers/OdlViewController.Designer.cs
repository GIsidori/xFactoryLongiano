namespace XFactoryNET.Module.Controllers
{
    partial class OdlViewController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</paramF>
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
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem4 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem5 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem6 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem7 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            this.popupWindowShowActionArchivia = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.actionSostituisci = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.simpleActionAvviaOdp = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.singleChoiceActionSilos = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.simpleActionAvviaOdl = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.singleChoiceActionSacchi = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // popupWindowShowActionArchivia
            // 
            this.popupWindowShowActionArchivia.AcceptButtonCaption = null;
            this.popupWindowShowActionArchivia.CancelButtonCaption = null;
            this.popupWindowShowActionArchivia.Caption = "Archivia";
            this.popupWindowShowActionArchivia.Category = "Export";
            this.popupWindowShowActionArchivia.ConfirmationMessage = "Confermi l\'archiviazione delle produzioni?";
            this.popupWindowShowActionArchivia.Id = "Archivia";
            this.popupWindowShowActionArchivia.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.Odl);
            this.popupWindowShowActionArchivia.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.popupWindowShowActionArchivia.ToolTip = "Archivia le produzioni eseguite";
            this.popupWindowShowActionArchivia.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.popupWindowShowActionArchivia.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionArchivia_CustomizePopupWindowParams);
            // 
            // actionSostituisci
            // 
            this.actionSostituisci.AcceptButtonCaption = null;
            this.actionSostituisci.CancelButtonCaption = null;
            this.actionSostituisci.Caption = "Sostituisci";
            this.actionSostituisci.Category = "RecordEdit";
            this.actionSostituisci.ConfirmationMessage = null;
            this.actionSostituisci.Id = "Sostituisci";
            this.actionSostituisci.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.actionSostituisci.TargetObjectsCriteria = "OdlIngredientiTeorici IS NOT NULL";
            this.actionSostituisci.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.Componente);
            this.actionSostituisci.ToolTip = null;
            this.actionSostituisci.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.actionSostituisci_CustomizePopupWindowParams);
            this.actionSostituisci.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.actionSostituisci_Execute);
            // 
            // simpleActionAvviaOdp
            // 
            this.simpleActionAvviaOdp.Caption = "Avvia";
            this.simpleActionAvviaOdp.Category = "View";
            this.simpleActionAvviaOdp.ConfirmationMessage = null;
            this.simpleActionAvviaOdp.Id = "AvviaOdp";
            this.simpleActionAvviaOdp.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.OrdineProduzione);
            this.simpleActionAvviaOdp.ToolTip = null;
            this.simpleActionAvviaOdp.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionAvviaOdp_Execute);
            // 
            // singleChoiceActionSilos
            // 
            this.singleChoiceActionSilos.Caption = "Lavorazioni";
            this.singleChoiceActionSilos.Category = "RecordEdit";
            this.singleChoiceActionSilos.ConfirmationMessage = null;
            this.singleChoiceActionSilos.DefaultItemMode = DevExpress.ExpressApp.Actions.DefaultItemMode.LastExecutedItem;
            this.singleChoiceActionSilos.Id = "LavorazioniSilos";
            this.singleChoiceActionSilos.ImageName = "BO_Task";
            choiceActionItem1.Caption = "Carico Rinfusa";
            choiceActionItem1.ImageName = null;
            choiceActionItem1.Shortcut = null;
            choiceActionItem1.ToolTip = null;
            choiceActionItem2.Caption = "Insilaggio";
            choiceActionItem2.ImageName = null;
            choiceActionItem2.Shortcut = null;
            choiceActionItem2.ToolTip = null;
            choiceActionItem3.Caption = "Pellet";
            choiceActionItem3.ImageName = null;
            choiceActionItem3.Shortcut = null;
            choiceActionItem3.ToolTip = null;
            choiceActionItem4.Caption = "Insacco";
            choiceActionItem4.ImageName = null;
            choiceActionItem4.Shortcut = null;
            choiceActionItem4.ToolTip = null;
            choiceActionItem5.Caption = "Scarico Rinfusa";
            choiceActionItem5.ImageName = null;
            choiceActionItem5.Shortcut = null;
            choiceActionItem5.ToolTip = null;
            this.singleChoiceActionSilos.Items.Add(choiceActionItem1);
            this.singleChoiceActionSilos.Items.Add(choiceActionItem2);
            this.singleChoiceActionSilos.Items.Add(choiceActionItem3);
            this.singleChoiceActionSilos.Items.Add(choiceActionItem4);
            this.singleChoiceActionSilos.Items.Add(choiceActionItem5);
            this.singleChoiceActionSilos.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.singleChoiceActionSilos.ShowItemsOnClick = true;
            this.singleChoiceActionSilos.TargetObjectsCriteria = "";
            this.singleChoiceActionSilos.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.Silos);
            this.singleChoiceActionSilos.ToolTip = null;
            this.singleChoiceActionSilos.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.singleChoiceActionSilos_Execute);
            // 
            // simpleActionAvviaOdl
            // 
            this.simpleActionAvviaOdl.Caption = "Avvia";
            this.simpleActionAvviaOdl.Category = "RecordEdit";
            this.simpleActionAvviaOdl.ConfirmationMessage = null;
            this.simpleActionAvviaOdl.Id = "AvviaOdl";
            this.simpleActionAvviaOdl.TargetObjectsCriteria = "[Stato] IN (\'InPreparazione\',\'Pronto\')";
            this.simpleActionAvviaOdl.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.Odl);
            this.simpleActionAvviaOdl.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleActionAvviaOdl.ToolTip = "Avvia la lavorazione";
            this.simpleActionAvviaOdl.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleActionAvviaOdl.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionAvviaOdl_Execute);
            // 
            // singleChoiceActionSacchi
            // 
            this.singleChoiceActionSacchi.Caption = "Lavorazioni";
            this.singleChoiceActionSacchi.Category = "RecordEdit";
            this.singleChoiceActionSacchi.ConfirmationMessage = null;
            this.singleChoiceActionSacchi.DefaultItemMode = DevExpress.ExpressApp.Actions.DefaultItemMode.LastExecutedItem;
            this.singleChoiceActionSacchi.Id = "LavorazioniSacchi";
            choiceActionItem6.Caption = "Entrata sacchi";
            choiceActionItem6.ImageName = "BO_Task";
            choiceActionItem6.Shortcut = null;
            choiceActionItem6.ToolTip = null;
            choiceActionItem7.Caption = "Uscita sacchi";
            choiceActionItem7.ImageName = "BO_Task";
            choiceActionItem7.Shortcut = null;
            choiceActionItem7.ToolTip = null;
            this.singleChoiceActionSacchi.Items.Add(choiceActionItem6);
            this.singleChoiceActionSacchi.Items.Add(choiceActionItem7);
            this.singleChoiceActionSacchi.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.singleChoiceActionSacchi.ShowItemsOnClick = true;
            this.singleChoiceActionSacchi.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.Lotto);
            this.singleChoiceActionSacchi.ToolTip = null;
            this.singleChoiceActionSacchi.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.singleChoiceActionSacchi_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionArchivia;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction actionSostituisci;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionAvviaOdp;
        private DevExpress.ExpressApp.Actions.SingleChoiceAction singleChoiceActionSilos;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionAvviaOdl;
        private DevExpress.ExpressApp.Actions.SingleChoiceAction singleChoiceActionSacchi;
    }
}
