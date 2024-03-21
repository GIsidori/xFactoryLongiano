namespace RKTest
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnGet = new System.Windows.Forms.Button();
            this.btnPut = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSettings = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numDB = new System.Windows.Forms.NumericUpDown();
            this.numDW = new System.Windows.Forms.NumericUpDown();
            this.numLEN = new System.Windows.Forms.NumericUpDown();
            this.numVAL = new System.Windows.Forms.NumericUpDown();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLEN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVAL)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "DB";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tipo";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(67, 17);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(59, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "DW";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Len";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(247, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(292, 212);
            this.listBox1.TabIndex = 6;
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(149, 92);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(53, 23);
            this.btnGet.TabIndex = 5;
            this.btnGet.Text = "Get";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnPut
            // 
            this.btnPut.Location = new System.Drawing.Point(149, 122);
            this.btnPut.Name = "btnPut";
            this.btnPut.Size = new System.Drawing.Size(53, 23);
            this.btnPut.TabIndex = 6;
            this.btnPut.Text = "Put";
            this.btnPut.UseVisualStyleBackColor = true;
            this.btnPut.Click += new System.EventHandler(this.btnPut_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Settings";
            // 
            // txtSettings
            // 
            this.txtSettings.Location = new System.Drawing.Point(79, 15);
            this.txtSettings.Name = "txtSettings";
            this.txtSettings.Size = new System.Drawing.Size(155, 20);
            this.txtSettings.TabIndex = 8;
            this.txtSettings.Text = "COM1,9600,7,Even,None";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(78, 41);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(59, 21);
            this.btnOpen.TabIndex = 9;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numVAL);
            this.groupBox1.Controls.Add(this.numLEN);
            this.groupBox1.Controls.Add(this.numDW);
            this.groupBox1.Controls.Add(this.numDB);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnGet);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnPut);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 156);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 128);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Value";
            // 
            // numDB
            // 
            this.numDB.Location = new System.Drawing.Point(67, 44);
            this.numDB.Name = "numDB";
            this.numDB.Size = new System.Drawing.Size(58, 20);
            this.numDB.TabIndex = 9;
            // 
            // numDW
            // 
            this.numDW.Location = new System.Drawing.Point(67, 70);
            this.numDW.Name = "numDW";
            this.numDW.Size = new System.Drawing.Size(58, 20);
            this.numDW.TabIndex = 10;
            // 
            // numLEN
            // 
            this.numLEN.Location = new System.Drawing.Point(67, 96);
            this.numLEN.Name = "numLEN";
            this.numLEN.Size = new System.Drawing.Size(58, 20);
            this.numLEN.TabIndex = 11;
            // 
            // numVAL
            // 
            this.numVAL.Location = new System.Drawing.Point(66, 122);
            this.numVAL.Name = "numVAL";
            this.numVAL.Size = new System.Drawing.Size(58, 20);
            this.numVAL.TabIndex = 12;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(175, 41);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(59, 21);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 256);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.txtSettings);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listBox1);
            this.Name = "Form1";
            this.Text = "Test RK512";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLEN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVAL)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Button btnPut;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSettings;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numVAL;
        private System.Windows.Forms.NumericUpDown numLEN;
        private System.Windows.Forms.NumericUpDown numDW;
        private System.Windows.Forms.NumericUpDown numDB;
        private System.Windows.Forms.Button btnClose;
    }
}

