namespace XFactoryNET.Module.Controllers
{
    partial class InsaccoViewController
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
            this.simpleActionInsacco = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // simpleActionInsacco
            // 
            this.simpleActionInsacco.Caption = "Insacco";
            this.simpleActionInsacco.ConfirmationMessage = null;
            this.simpleActionInsacco.Id = "Insacco";
            this.simpleActionInsacco.ImageName = null;
            this.simpleActionInsacco.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleActionInsacco.Shortcut = null;
            this.simpleActionInsacco.Tag = null;
            this.simpleActionInsacco.TargetObjectsCriteria = null;
            this.simpleActionInsacco.TargetObjectType = typeof(XFactoryNET.Module.BusinessObjects.Silos);
            this.simpleActionInsacco.TargetViewId = null;
            this.simpleActionInsacco.ToolTip = null;
            this.simpleActionInsacco.TypeOfView = typeof(DevExpress.ExpressApp.View);
            this.simpleActionInsacco.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionInsacco_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionInsacco;
    }
}
