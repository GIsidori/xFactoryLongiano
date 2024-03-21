using System.Reflection;
using System.Windows.Forms;
namespace XFactoryNET.WinCE
{
    partial class frmGiacenze
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
            this.components = new System.ComponentModel.Container();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonPesa = new System.Windows.Forms.Button();
            this.giacenzeSilosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.xFactoryNETDataSet = new XFactoryNET.WinCE.XFactoryNETDataSet();
            this.dataGrid2 = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn6 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn7 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.giacenzeSilosTableAdapter = new XFactoryNET.WinCE.XFactoryNETDataSetTableAdapters.SilosTableAdapter();
            this.timer1 = new System.Windows.Forms.Timer();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.giacenzeSilosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xFactoryNETDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.buttonPesa);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(798, 70);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(289, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 57);
            this.button1.TabIndex = 34;
            this.button1.Text = "Silos";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(147, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(136, 57);
            this.button2.TabIndex = 33;
            this.button2.Text = "Sacchi";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonPesa
            // 
            this.buttonPesa.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.buttonPesa.Location = new System.Drawing.Point(5, 3);
            this.buttonPesa.Name = "buttonPesa";
            this.buttonPesa.Size = new System.Drawing.Size(136, 57);
            this.buttonPesa.TabIndex = 32;
            this.buttonPesa.Text = "Pesa";
            this.buttonPesa.Click += new System.EventHandler(this.button3_Click);
            // 
            // giacenzeSilosBindingSource
            // 
            this.giacenzeSilosBindingSource.DataMember = "Silos";
            this.giacenzeSilosBindingSource.DataSource = this.xFactoryNETDataSet;
            // 
            // xFactoryNETDataSet
            // 
            this.xFactoryNETDataSet.DataSetName = "XFactoryNETDataSet";
            this.xFactoryNETDataSet.Prefix = "";
            this.xFactoryNETDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGrid2
            // 
            this.dataGrid2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid2.DataSource = this.giacenzeSilosBindingSource;
            this.dataGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.dataGrid2.Location = new System.Drawing.Point(0, 70);
            this.dataGrid2.Name = "dataGrid2";
            this.dataGrid2.RowHeadersVisible = false;
            this.dataGrid2.Size = new System.Drawing.Size(798, 505);
            this.dataGrid2.TabIndex = 5;
            this.dataGrid2.TableStyles.Add(this.dataGridTableStyle1);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn1);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn3);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn4);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn5);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn6);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn7);
            this.dataGridTableStyle1.MappingName = "Silos";
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "Silos";
            this.dataGridTextBoxColumn1.MappingName = "Numero";
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "Articolo";
            this.dataGridTextBoxColumn3.MappingName = "Articolo";
            this.dataGridTextBoxColumn3.NullText = "";
            this.dataGridTextBoxColumn3.Width = 80;
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "Descrizione";
            this.dataGridTextBoxColumn4.MappingName = "Descrizione";
            this.dataGridTextBoxColumn4.NullText = "";
            this.dataGridTextBoxColumn4.Width = 264;
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Format = "n0";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "Quantità";
            this.dataGridTextBoxColumn5.MappingName = "Quantità";
            this.dataGridTextBoxColumn5.NullText = "";
            this.dataGridTextBoxColumn5.Width = 80;
            // 
            // dataGridTextBoxColumn6
            // 
            this.dataGridTextBoxColumn6.Format = "";
            this.dataGridTextBoxColumn6.FormatInfo = null;
            this.dataGridTextBoxColumn6.HeaderText = "Note";
            this.dataGridTextBoxColumn6.MappingName = "Note";
            this.dataGridTextBoxColumn6.NullText = "";
            this.dataGridTextBoxColumn6.Width = 140;
            // 
            // dataGridTextBoxColumn7
            // 
            this.dataGridTextBoxColumn7.Format = "";
            this.dataGridTextBoxColumn7.FormatInfo = null;
            this.dataGridTextBoxColumn7.HeaderText = "Cliente";
            this.dataGridTextBoxColumn7.MappingName = "Cliente";
            this.dataGridTextBoxColumn7.NullText = "";
            this.dataGridTextBoxColumn7.Width = 150;
            // 
            // giacenzeSilosTableAdapter
            // 
            this.giacenzeSilosTableAdapter.ClearBeforeFill = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmGiacenze
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(798, 575);
            this.ControlBox = false;
            this.Controls.Add(this.dataGrid2);
            this.Controls.Add(this.panel3);
            this.Name = "frmGiacenze";
            this.Text = "Giacenze in silos";
            this.Deactivate += new System.EventHandler(this.frmGiacenze_Deactivate);
            this.Load += new System.EventHandler(this.frmGiacenze_Load);
            this.Activated += new System.EventHandler(this.frmGiacenze_Activated);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.giacenzeSilosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xFactoryNETDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonPesa;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGrid dataGrid2;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn5;
        private XFactoryNETDataSet xFactoryNETDataSet;
        private System.Windows.Forms.BindingSource giacenzeSilosBindingSource;
        private XFactoryNET.WinCE.XFactoryNETDataSetTableAdapters.SilosTableAdapter giacenzeSilosTableAdapter;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn6;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn7;
        private System.Windows.Forms.Timer timer1;

    }
}