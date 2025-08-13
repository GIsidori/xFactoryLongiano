namespace XFactoryNET.Module.Win
{
    partial class frmSettings
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnConnect = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtFile = new DevExpress.XtraEditors.TextEdit();
            this.checkEdit2 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.textEdit3 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit4 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(22, 32);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(62, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Nome Server";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(22, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(76, 13);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Nome Database";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(22, 125);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(33, 13);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "UserID";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(22, 151);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(46, 13);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Password";
            // 
            // btnConnect
            // 
            this.btnConnect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConnect.Location = new System.Drawing.Point(22, 191);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(80, 27);
            this.btnConnect.TabIndex = 9;
            this.btnConnect.Text = "Ok";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(162, 191);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 27);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Annulla";
            // 
            // txtFile
            // 
            this.txtFile.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtFile.Location = new System.Drawing.Point(0, 268);
            this.txtFile.Name = "txtFile";
            this.txtFile.Properties.ReadOnly = true;
            this.txtFile.Size = new System.Drawing.Size(280, 20);
            this.txtFile.TabIndex = 13;
            // 
            // checkEdit2
            // 
            this.checkEdit2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", global::XFactoryNET.Module.Win.Properties.Settings.Default, "DontAsk", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkEdit2.EditValue = global::XFactoryNET.Module.Win.Properties.Settings.Default.DontAsk;
            this.checkEdit2.Location = new System.Drawing.Point(12, 234);
            this.checkEdit2.Name = "checkEdit2";
            this.checkEdit2.Properties.Caption = "Non chiedere più";
            this.checkEdit2.Size = new System.Drawing.Size(230, 19);
            this.checkEdit2.TabIndex = 12;
            // 
            // checkEdit1
            // 
            this.checkEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", global::XFactoryNET.Module.Win.Properties.Settings.Default, "SQLAuth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkEdit1.EditValue = global::XFactoryNET.Module.Win.Properties.Settings.Default.SQLAuth;
            this.checkEdit1.Location = new System.Drawing.Point(20, 96);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "Autenticazione SQL";
            this.checkEdit1.Size = new System.Drawing.Size(222, 19);
            this.checkEdit1.TabIndex = 2;
            // 
            // textEdit3
            // 
            this.textEdit3.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", global::XFactoryNET.Module.Win.Properties.Settings.Default, "UserID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textEdit3.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::XFactoryNET.Module.Win.Properties.Settings.Default, "SQLAuth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textEdit3.EditValue = global::XFactoryNET.Module.Win.Properties.Settings.Default.UserID;
            this.textEdit3.Enabled = global::XFactoryNET.Module.Win.Properties.Settings.Default.SQLAuth;
            this.textEdit3.Location = new System.Drawing.Point(109, 122);
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Size = new System.Drawing.Size(133, 20);
            this.textEdit3.TabIndex = 3;
            // 
            // textEdit4
            // 
            this.textEdit4.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", global::XFactoryNET.Module.Win.Properties.Settings.Default, "Password", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textEdit4.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::XFactoryNET.Module.Win.Properties.Settings.Default, "SQLAuth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textEdit4.EditValue = global::XFactoryNET.Module.Win.Properties.Settings.Default.Password;
            this.textEdit4.Enabled = global::XFactoryNET.Module.Win.Properties.Settings.Default.SQLAuth;
            this.textEdit4.Location = new System.Drawing.Point(109, 148);
            this.textEdit4.Name = "textEdit4";
            this.textEdit4.Properties.UseSystemPasswordChar = true;
            this.textEdit4.Size = new System.Drawing.Size(133, 20);
            this.textEdit4.TabIndex = 4;
            // 
            // textEdit2
            // 
            this.textEdit2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", global::XFactoryNET.Module.Win.Properties.Settings.Default, "Database", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textEdit2.EditValue = global::XFactoryNET.Module.Win.Properties.Settings.Default.Database;
            this.textEdit2.Location = new System.Drawing.Point(109, 55);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Size = new System.Drawing.Size(133, 20);
            this.textEdit2.TabIndex = 1;
            // 
            // textEdit1
            // 
            this.textEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", global::XFactoryNET.Module.Win.Properties.Settings.Default, "Server", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textEdit1.EditValue = global::XFactoryNET.Module.Win.Properties.Settings.Default.Server;
            this.textEdit1.Location = new System.Drawing.Point(109, 29);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(133, 20);
            this.textEdit1.TabIndex = 0;
            // 
            // frmSettings
            // 
            this.AcceptButton = this.btnConnect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(280, 288);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.checkEdit2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.checkEdit1);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.textEdit3);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.textEdit4);
            this.Controls.Add(this.textEdit2);
            this.Controls.Add(this.textEdit1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connessione a database SQL";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraEditors.TextEdit textEdit3;
        private DevExpress.XtraEditors.TextEdit textEdit4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnConnect;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.CheckEdit checkEdit2;
        private DevExpress.XtraEditors.TextEdit txtFile;
    }
}