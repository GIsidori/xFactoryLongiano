namespace XFactoryNET.Custom.Panzoo.AggMan
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
            this.labelControl1 = new System.Windows.Forms.Label();
            this.labelControl2 = new System.Windows.Forms.Label();
            this.labelControl3 = new System.Windows.Forms.Label();
            this.labelControl4 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCommPort = new System.Windows.Forms.TextBox();
            this.checkEdit2 = new System.Windows.Forms.CheckBox();
            this.checkEdit1 = new System.Windows.Forms.CheckBox();
            this.textEdit3 = new System.Windows.Forms.TextBox();
            this.textEdit4 = new System.Windows.Forms.TextBox();
            this.textEdit2 = new System.Windows.Forms.TextBox();
            this.textEdit1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSize = true;
            this.labelControl1.Location = new System.Drawing.Point(9, 32);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(69, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Nome Server";
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSize = true;
            this.labelControl2.Location = new System.Drawing.Point(9, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(84, 13);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Nome Database";
            // 
            // labelControl3
            // 
            this.labelControl3.AutoSize = true;
            this.labelControl3.Location = new System.Drawing.Point(22, 125);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(40, 13);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "UserID";
            // 
            // labelControl4
            // 
            this.labelControl4.AutoSize = true;
            this.labelControl4.Location = new System.Drawing.Point(22, 151);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(53, 13);
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
            this.txtFile.Location = new System.Drawing.Point(0, 311);
            this.txtFile.Name = "txtFile";
            this.txtFile.ReadOnly = true;
            this.txtFile.Size = new System.Drawing.Size(280, 20);
            this.txtFile.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 288);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Comm port:";
            // 
            // txtCommPort
            // 
            this.txtCommPort.Location = new System.Drawing.Point(109, 285);
            this.txtCommPort.Name = "txtCommPort";
            this.txtCommPort.Size = new System.Drawing.Size(133, 20);
            this.txtCommPort.TabIndex = 14;
            this.txtCommPort.Text = global::XFactoryNET.Custom.Panzoo.AggMan.Properties.Settings.Default.PortName;
            // 
            // checkEdit2
            // 
            this.checkEdit2.AutoSize = true;
            this.checkEdit2.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::XFactoryNET.Custom.Panzoo.AggMan.Properties.Settings.Default, "DontAsk", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkEdit2.Location = new System.Drawing.Point(9, 234);
            this.checkEdit2.Name = "checkEdit2";
            this.checkEdit2.Size = new System.Drawing.Size(107, 17);
            this.checkEdit2.TabIndex = 12;
            this.checkEdit2.Text = "Non chiedere più";
            // 
            // checkEdit1
            // 
            this.checkEdit1.AutoSize = true;
            this.checkEdit1.Checked = true;
            this.checkEdit1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkEdit1.Location = new System.Drawing.Point(9, 96);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Size = new System.Drawing.Size(120, 17);
            this.checkEdit1.TabIndex = 2;
            this.checkEdit1.Text = "Autenticazione SQL";
            // 
            // textEdit3
            // 
            this.textEdit3.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::XFactoryNET.Custom.Panzoo.AggMan.Properties.Settings.Default, "UserID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textEdit3.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::XFactoryNET.Custom.Panzoo.AggMan.Properties.Settings.Default, "SQLAuth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textEdit3.Location = new System.Drawing.Point(109, 122);
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Size = new System.Drawing.Size(133, 20);
            this.textEdit3.TabIndex = 3;
            this.textEdit3.Text = global::XFactoryNET.Custom.Panzoo.AggMan.Properties.Settings.Default.UserID;
            // 
            // textEdit4
            // 
            this.textEdit4.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::XFactoryNET.Custom.Panzoo.AggMan.Properties.Settings.Default, "Password", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textEdit4.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::XFactoryNET.Custom.Panzoo.AggMan.Properties.Settings.Default, "SQLAuth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textEdit4.Enabled = global::XFactoryNET.Custom.Panzoo.AggMan.Properties.Settings.Default.SQLAuth;
            this.textEdit4.Location = new System.Drawing.Point(109, 148);
            this.textEdit4.Name = "textEdit4";
            this.textEdit4.Size = new System.Drawing.Size(133, 20);
            this.textEdit4.TabIndex = 4;
            this.textEdit4.Text = global::XFactoryNET.Custom.Panzoo.AggMan.Properties.Settings.Default.Password;
            this.textEdit4.UseSystemPasswordChar = true;
            // 
            // textEdit2
            // 
            this.textEdit2.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::XFactoryNET.Custom.Panzoo.AggMan.Properties.Settings.Default, "Database", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textEdit2.Location = new System.Drawing.Point(109, 55);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Size = new System.Drawing.Size(133, 20);
            this.textEdit2.TabIndex = 1;
            this.textEdit2.Text = global::XFactoryNET.Custom.Panzoo.AggMan.Properties.Settings.Default.Database;
            // 
            // textEdit1
            // 
            this.textEdit1.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::XFactoryNET.Custom.Panzoo.AggMan.Properties.Settings.Default, "Server", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textEdit1.Location = new System.Drawing.Point(109, 29);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(133, 20);
            this.textEdit1.TabIndex = 0;
            this.textEdit1.Text = global::XFactoryNET.Custom.Panzoo.AggMan.Properties.Settings.Default.Server;
            // 
            // frmSettings
            // 
            this.AcceptButton = this.btnConnect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(280, 331);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCommPort);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textEdit1;
        private System.Windows.Forms.TextBox textEdit2;
        private System.Windows.Forms.CheckBox checkEdit1;
        private System.Windows.Forms.TextBox textEdit3;
        private System.Windows.Forms.TextBox textEdit4;
        private System.Windows.Forms.Label labelControl1;
        private System.Windows.Forms.Label labelControl2;
        private System.Windows.Forms.Label labelControl3;
        private System.Windows.Forms.Label labelControl4;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox checkEdit2;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCommPort;
    }
}