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
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem8 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            this.singleChoiceActionSilos = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.actionSostituisci = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.simpleActionAvviaOdp = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.singleChoiceActionSacchi = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.popupWindowShowActionArchivia = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.popupWindowShowActionAvviaOdl = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.simpleActionSvuota = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.simpleActionTrackingOdl = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.simpleActionStampaConsumiTurno = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.simpleActionCancelOdL = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
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
            choiceActionItem1.Data = "CaricoRinfusa";
            choiceActionItem1.ImageName = null;
            choiceActionItem1.Shortcut = null;
            choiceActionItem1.ToolTip = null;
            choiceActionItem2.Caption = "Insilaggio";
            choiceActionItem2.Data = "Insilaggio";
            choiceActionItem2.ImageName = null;
            choiceActionItem2.Shortcut = null;
            choiceActionItem2.ToolTip = null;
            choiceActionItem3.Caption = "Pellet";
            choiceActionItem3.Data = "Pellet";
            choiceActionItem3.ImageName = null;
            choiceActionItem3.Shortcut = null;
            choiceActionItem3.ToolTip = null;
            choiceActionItem4.Caption = "Insacco";
            choiceActionItem4.Data = "Insacco";
            choiceActionItem4.ImageName = null;
            choiceActionItem4.Shortcut = null;
            choiceActionItem4.ToolTip = null;
            choiceActionItem5.Caption = "Scarico Rinfusa";
            choiceActionItem5.Data = "ScaricoRinfusa";
            choiceActionItem5.ImageName = null;
            choiceActionItem5.Shortcut = null;
            choiceActionItem5.ToolTip = null;
            choiceActionItem6.BeginGroup = true;
            choiceActionItem6.Caption = "Trasferimento";
            choiceActionItem6.Data = "Trasferimento";
            choiceActionItem6.ImageName = null;
            choiceActionItem6.Shortcut = null;
            choiceActionItem6.ToolTip = null;
            this.singleChoiceActionSilos.Items.Add(choiceActionItem1);
            this.singleChoiceActionSilos.Items.Add(choiceActionItem2);
            this.singleChoiceActionSilos.Items.Add(choiceActionItem3);
            this.singleChoiceActionSilos.Items.Add(choiceActionItem4);
            this.singleChoiceActionSilos.Items.Add(choiceActionItem5);
            this.singleChoiceActionSilos.Items.Add(choiceActionItem6);
            this.singleChoiceActionSilos.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.singleChoiceActionSilos.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.singleChoiceActionSilos.ShowItemsOnClick = true;
            this.singleChoiceActionSilos.TargetObjectsCriteria = "";
            this.singleChoiceActionSilos.ToolTip = null;
            this.singleChoiceActionSilos.TypeOfView = typeof(DevExpress.ExpressApp.View);
            this.singleChoiceActionSilos.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.singleChoiceActionSilos_Execute);
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
            this.simpleActionAvviaOdp.ConfirmationMessage = "Confermi l\'avvio dell\'ordine selezionato?";
            this.simpleActionAvviaOdp.Id = "AvviaOdp";
            this.simpleActionAvviaOdp.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleActionAvviaOdp.TargetObjectsCriteria = "Stato IN (\'Nuovo\')";
            this.simpleActionAvviaOdp.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.OrdineProduzione);
            this.simpleActionAvviaOdp.ToolTip = null;
            this.simpleActionAvviaOdp.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionAvviaOdp_Execute);
            // 
            // singleChoiceActionSacchi
            // 
            this.singleChoiceActionSacchi.Caption = "Lavorazioni";
            this.singleChoiceActionSacchi.Category = "RecordEdit";
            this.singleChoiceActionSacchi.ConfirmationMessage = null;
            this.singleChoiceActionSacchi.DefaultItemMode = DevExpress.ExpressApp.Actions.DefaultItemMode.LastExecutedItem;
            this.singleChoiceActionSacchi.Id = "LavorazioniSacchi";
            this.singleChoiceActionSacchi.ImageName = "BO_Task";
            choiceActionItem7.Caption = "Entrata sacchi";
            choiceActionItem7.ImageName = "BO_Task";
            choiceActionItem7.Shortcut = null;
            choiceActionItem7.ToolTip = null;
            choiceActionItem8.Caption = "Uscita sacchi";
            choiceActionItem8.ImageName = "BO_Task";
            choiceActionItem8.Shortcut = null;
            choiceActionItem8.ToolTip = null;
            this.singleChoiceActionSacchi.Items.Add(choiceActionItem7);
            this.singleChoiceActionSacchi.Items.Add(choiceActionItem8);
            this.singleChoiceActionSacchi.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.singleChoiceActionSacchi.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.singleChoiceActionSacchi.ShowItemsOnClick = true;
            this.singleChoiceActionSacchi.TargetObjectsCriteria = "";
            this.singleChoiceActionSacchi.ToolTip = null;
            this.singleChoiceActionSacchi.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.singleChoiceActionSacchi_Execute);
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
            this.popupWindowShowActionArchivia.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.popupWindowShowActionArchivia.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.popupWindowShowActionArchivia.ToolTip = "Archivia le produzioni eseguite";
            this.popupWindowShowActionArchivia.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.popupWindowShowActionArchivia.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionArchivia_CustomizePopupWindowParams);
            this.popupWindowShowActionArchivia.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionArchivia_Execute);
            // 
            // popupWindowShowActionAvviaOdl
            // 
            this.popupWindowShowActionAvviaOdl.AcceptButtonCaption = null;
            this.popupWindowShowActionAvviaOdl.CancelButtonCaption = null;
            this.popupWindowShowActionAvviaOdl.Caption = "Avvia";
            this.popupWindowShowActionAvviaOdl.Category = "RecordEdit";
            this.popupWindowShowActionAvviaOdl.ConfirmationMessage = null;
            this.popupWindowShowActionAvviaOdl.Id = "AvviaOdl";
            this.popupWindowShowActionAvviaOdl.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.popupWindowShowActionAvviaOdl.TargetObjectsCriteria = "[Stato] IN (\'InPreparazione\',\'Pronto\')";
            this.popupWindowShowActionAvviaOdl.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.Odl);
            this.popupWindowShowActionAvviaOdl.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.popupWindowShowActionAvviaOdl.ToolTip = "Avvia la lavorazione";
            this.popupWindowShowActionAvviaOdl.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.popupWindowShowActionAvviaOdl.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowAction1_CustomizePopupWindowParams);
            this.popupWindowShowActionAvviaOdl.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowAction1_Execute);
            // 
            // simpleActionSvuota
            // 
            this.simpleActionSvuota.Caption = "Svuota";
            this.simpleActionSvuota.Category = "RecordEdit";
            this.simpleActionSvuota.ConfirmationMessage = "Confermi lo svuotamento del silos?";
            this.simpleActionSvuota.Id = "Svuota";
            this.simpleActionSvuota.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleActionSvuota.TargetObjectsCriteria = "Lotto IS NOT NULL";
            this.simpleActionSvuota.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.Silos);
            this.simpleActionSvuota.ToolTip = null;
            this.simpleActionSvuota.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionSvuota_Execute);
            // 
            // simpleActionTrackingOdl
            // 
            this.simpleActionTrackingOdl.Caption = "Rintraccia";
            this.simpleActionTrackingOdl.Category = "RecordsNavigation";
            this.simpleActionTrackingOdl.ConfirmationMessage = null;
            this.simpleActionTrackingOdl.Id = "OdlTracking";
            this.simpleActionTrackingOdl.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.Odl);
            this.simpleActionTrackingOdl.ToolTip = null;
            this.simpleActionTrackingOdl.TypeOfView = typeof(DevExpress.ExpressApp.View);
            this.simpleActionTrackingOdl.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionForwardOdl_Execute);
            // 
            // simpleActionStampaConsumiTurno
            // 
            this.simpleActionStampaConsumiTurno.Caption = "Stampa Consumi del Turno";
            this.simpleActionStampaConsumiTurno.ConfirmationMessage = null;
            this.simpleActionStampaConsumiTurno.Id = "StampaConsumiTurno";
            this.simpleActionStampaConsumiTurno.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleActionStampaConsumiTurno.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.OdlDosaggio);
            this.simpleActionStampaConsumiTurno.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleActionStampaConsumiTurno.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.simpleActionStampaConsumiTurno.ToolTip = "Stampa i consumi per il turno selezionato";
            this.simpleActionStampaConsumiTurno.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.simpleActionStampaConsumiTurno.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionStampaConsumiTurno_Execute);
            // 
            // simpleActionCancelOdL
            // 
            this.simpleActionCancelOdL.Caption = "Cancel Odl";
            this.simpleActionCancelOdL.Category = "RecordEdit";
            this.simpleActionCancelOdL.ConfirmationMessage = "Confermi l\'interruzione dell\'ordine di lavoro selezionato?";
            this.simpleActionCancelOdL.Id = "CancelOdl";
            this.simpleActionCancelOdL.ImageName = "BO_Rules";
            this.simpleActionCancelOdL.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleActionCancelOdL.TargetObjectsCriteria = "Stato IN (\'Pronto\',\'InEsecuzione\',\'Avviato\')";
            this.simpleActionCancelOdL.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.Odl);
            this.simpleActionCancelOdL.ToolTip = null;
            this.simpleActionCancelOdL.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionAbortOdL_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionArchivia;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction actionSostituisci;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionAvviaOdp;
        private DevExpress.ExpressApp.Actions.SingleChoiceAction singleChoiceActionSilos;
        private DevExpress.ExpressApp.Actions.SingleChoiceAction singleChoiceActionSacchi;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionAvviaOdl;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionSvuota;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionTrackingOdl;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionStampaConsumiTurno;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionCancelOdL;
    }
}
