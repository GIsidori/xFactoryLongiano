namespace XFactoryNET.Module.Win.Controllers
{
    partial class ListViewController
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
            this.actionColumnAutoWidth = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.actionFormatConditions = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // actionColumnAutoWidth
            // 
            this.actionColumnAutoWidth.Caption = "Column Auto Width";
            this.actionColumnAutoWidth.Category = "View";
            this.actionColumnAutoWidth.ConfirmationMessage = null;
            this.actionColumnAutoWidth.Id = "ColumnAutoWidth";
            this.actionColumnAutoWidth.ImageName = "PageWidthHS";
            this.actionColumnAutoWidth.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.actionColumnAutoWidth.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.actionColumnAutoWidth.ToolTip = "Dimensiona automaticamente le colonne";
            this.actionColumnAutoWidth.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.actionColumnAutoWidth.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.actionColumnAutoWidth_Execute);
            // 
            // actionFormatConditions
            // 
            this.actionFormatConditions.Caption = "Formattazione condizionale";
            this.actionFormatConditions.Category = "View";
            this.actionFormatConditions.ConfirmationMessage = null;
            this.actionFormatConditions.Id = "FormatConditions";
            this.actionFormatConditions.ImageName = "DisplayInColorHS";
            this.actionFormatConditions.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.actionFormatConditions.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.actionFormatConditions.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.actionFormatConditions.ToolTip = "Formattazione condizionale";
            this.actionFormatConditions.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.actionFormatConditions.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.actionFormatConditions_Execute);
            // 
            // ListViewController
            // 
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.ListView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction actionColumnAutoWidth;
        private DevExpress.ExpressApp.Actions.SimpleAction actionFormatConditions;
    }
}
