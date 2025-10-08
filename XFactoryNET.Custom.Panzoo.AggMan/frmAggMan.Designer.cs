namespace XFactoryNET.Custom.Panzoo.AggMan
{
    partial class frmAggMan
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label lottoInCorsoLabel;
            System.Windows.Forms.Label numeroLottiLabel;
            System.Windows.Forms.Label noteLabel;
            System.Windows.Forms.Label articoloLabel;
            System.Windows.Forms.Label nrOrdLabel;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label9;
            this.panel2 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.lblSetpoint = new System.Windows.Forms.Label();
            this.lblSac = new System.Windows.Forms.Label();
            this.btnTeorico = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.ingredientiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.odlBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.xFactoryNETDataSet = new XFactoryNET.Custom.Panzoo.AggMan.XFactoryNETDataSet();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.btnFineMisc = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.btnDone = new System.Windows.Forms.Button();
            this.lblStatoBilancia = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblDiff = new System.Windows.Forms.Label();
            this.lblPesoBilancia = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblQtàIngr = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timerBlink = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCurrMisc = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lottoInCorsoTextBox = new System.Windows.Forms.TextBox();
            this.numeroLottiTextBox = new System.Windows.Forms.TextBox();
            this.noteTextBox = new System.Windows.Forms.TextBox();
            this.descrizioneTextBox = new System.Windows.Forms.TextBox();
            this.articoloTextBox = new System.Windows.Forms.TextBox();
            this.nrOrdTextBox = new System.Windows.Forms.TextBox();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.timerKey = new System.Windows.Forms.Timer(this.components);
            this.odlTableAdapter = new XFactoryNET.Custom.Panzoo.AggMan.XFactoryNETDataSetTableAdapters.OdlTableAdapter();
            this.lottoTableAdapter = new XFactoryNET.Custom.Panzoo.AggMan.XFactoryNETDataSetTableAdapters.LottoTableAdapter();
            this.silosTableAdapter = new XFactoryNET.Custom.Panzoo.AggMan.XFactoryNETDataSetTableAdapters.SilosTableAdapter();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            lottoInCorsoLabel = new System.Windows.Forms.Label();
            numeroLottiLabel = new System.Windows.Forms.Label();
            noteLabel = new System.Windows.Forms.Label();
            articoloLabel = new System.Windows.Forms.Label();
            nrOrdLabel = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ingredientiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.odlBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xFactoryNETDataSet)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            label2.Location = new System.Drawing.Point(621, 41);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(107, 17);
            label2.TabIndex = 61;
            label2.Text = "Destinazione:";
            label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            label1.Location = new System.Drawing.Point(681, 10);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(47, 17);
            label1.TabIndex = 62;
            label1.Text = "Q.tà:";
            label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lottoInCorsoLabel
            // 
            lottoInCorsoLabel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            lottoInCorsoLabel.Location = new System.Drawing.Point(419, 8);
            lottoInCorsoLabel.Name = "lottoInCorsoLabel";
            lottoInCorsoLabel.Size = new System.Drawing.Size(74, 17);
            lottoInCorsoLabel.TabIndex = 63;
            lottoInCorsoLabel.Text = "In mixer:";
            // 
            // numeroLottiLabel
            // 
            numeroLottiLabel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            numeroLottiLabel.Location = new System.Drawing.Point(339, 9);
            numeroLottiLabel.Name = "numeroLottiLabel";
            numeroLottiLabel.Size = new System.Drawing.Size(40, 20);
            numeroLottiLabel.TabIndex = 64;
            numeroLottiLabel.Text = "Nr. misc.:";
            // 
            // noteLabel
            // 
            noteLabel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            noteLabel.Location = new System.Drawing.Point(5, 72);
            noteLabel.Name = "noteLabel";
            noteLabel.Size = new System.Drawing.Size(47, 17);
            noteLabel.TabIndex = 65;
            noteLabel.Text = "Note:";
            // 
            // articoloLabel
            // 
            articoloLabel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            articoloLabel.Location = new System.Drawing.Point(5, 40);
            articoloLabel.Name = "articoloLabel";
            articoloLabel.Size = new System.Drawing.Size(66, 17);
            articoloLabel.TabIndex = 66;
            articoloLabel.Text = "Articolo:";
            // 
            // nrOrdLabel
            // 
            nrOrdLabel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            nrOrdLabel.Location = new System.Drawing.Point(5, 10);
            nrOrdLabel.Name = "nrOrdLabel";
            nrOrdLabel.Size = new System.Drawing.Size(60, 17);
            nrOrdLabel.TabIndex = 67;
            nrOrdLabel.Text = "Nr Ord:";
            // 
            // label5
            // 
            label5.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            label5.Location = new System.Drawing.Point(177, 8);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(49, 20);
            label5.TabIndex = 60;
            label5.Text = "Data:";
            // 
            // label8
            // 
            label8.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            label8.Location = new System.Drawing.Point(6, 10);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(66, 17);
            label8.TabIndex = 31;
            label8.Text = "Articolo:";
            // 
            // label9
            // 
            label9.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            label9.Location = new System.Drawing.Point(535, 8);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(86, 20);
            label9.TabIndex = 0;
            label9.Text = "Da pesare:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.lblSetpoint);
            this.panel2.Controls.Add(this.lblSac);
            this.panel2.Controls.Add(this.btnTeorico);
            this.panel2.Controls.Add(label8);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.textBox6);
            this.panel2.Controls.Add(this.textBox4);
            this.panel2.Controls.Add(this.btnFineMisc);
            this.panel2.Controls.Add(this.textBox5);
            this.panel2.Controls.Add(this.btnDone);
            this.panel2.Controls.Add(this.lblStatoBilancia);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.lblDiff);
            this.panel2.Controls.Add(this.lblPesoBilancia);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.lblQtàIngr);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 611);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1032, 130);
            this.panel2.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(289, 67);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(136, 24);
            this.label10.TabIndex = 68;
            this.label10.Text = "Setpoint";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // lblSetpoint
            // 
            this.lblSetpoint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblSetpoint.Font = new System.Drawing.Font("Tahoma", 18F);
            this.lblSetpoint.ForeColor = System.Drawing.Color.Lime;
            this.lblSetpoint.Location = new System.Drawing.Point(289, 95);
            this.lblSetpoint.Name = "lblSetpoint";
            this.lblSetpoint.Size = new System.Drawing.Size(136, 32);
            this.lblSetpoint.TabIndex = 69;
            this.lblSetpoint.Text = "0.000";
            this.lblSetpoint.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblSac
            // 
            this.lblSac.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblSac.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblSac.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblSac.Location = new System.Drawing.Point(839, 3);
            this.lblSac.Name = "lblSac";
            this.lblSac.Size = new System.Drawing.Size(136, 27);
            this.lblSac.TabIndex = 0;
            this.lblSac.Text = "Additivo";
            this.lblSac.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblSac.Visible = false;
            // 
            // btnTeorico
            // 
            this.btnTeorico.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.btnTeorico.Location = new System.Drawing.Point(839, 30);
            this.btnTeorico.Name = "btnTeorico";
            this.btnTeorico.Size = new System.Drawing.Size(136, 48);
            this.btnTeorico.TabIndex = 30;
            this.btnTeorico.Text = "Peso teorico";
            this.btnTeorico.Click += new System.EventHandler(this.btnTeorico_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(183, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 20);
            this.label6.TabIndex = 32;
            this.label6.Text = "Lotto";
            // 
            // textBox6
            // 
            this.textBox6.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.ingredientiBindingSource, "CodiceEsterno", true));
            this.textBox6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.textBox6.Location = new System.Drawing.Point(264, 5);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(161, 27);
            this.textBox6.TabIndex = 60;
            this.textBox6.Validated += new System.EventHandler(this.textBox6_Validated);
            // 
            // ingredientiBindingSource
            // 
            this.ingredientiBindingSource.DataMember = "Ingredienti";
            this.ingredientiBindingSource.DataSource = this.odlBindingSource;
            // 
            // odlBindingSource
            // 
            this.odlBindingSource.DataMember = "Odl";
            this.odlBindingSource.DataSource = this.xFactoryNETDataSet;
            // 
            // xFactoryNETDataSet
            // 
            this.xFactoryNETDataSet.DataSetName = "XFactoryNETDataSet";
            this.xFactoryNETDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // textBox4
            // 
            this.textBox4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.ingredientiBindingSource, "Descrizione", true));
            this.textBox4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.textBox4.Location = new System.Drawing.Point(6, 38);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(419, 27);
            this.textBox4.TabIndex = 59;
            // 
            // btnFineMisc
            // 
            this.btnFineMisc.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.btnFineMisc.Location = new System.Drawing.Point(839, 79);
            this.btnFineMisc.Name = "btnFineMisc";
            this.btnFineMisc.Size = new System.Drawing.Size(136, 48);
            this.btnFineMisc.TabIndex = 50;
            this.btnFineMisc.Text = "Fine misc.";
            this.btnFineMisc.Click += new System.EventHandler(this.btnFineMisc_Click);
            // 
            // textBox5
            // 
            this.textBox5.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.ingredientiBindingSource, "Articolo", true));
            this.textBox5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.textBox5.Location = new System.Drawing.Point(78, 5);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 27);
            this.textBox5.TabIndex = 58;
            // 
            // btnDone
            // 
            this.btnDone.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.btnDone.Location = new System.Drawing.Point(620, 30);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(206, 97);
            this.btnDone.TabIndex = 9;
            this.btnDone.Text = "Pesa/Tara";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // lblStatoBilancia
            // 
            this.lblStatoBilancia.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.lblStatoBilancia.Location = new System.Drawing.Point(620, 3);
            this.lblStatoBilancia.Name = "lblStatoBilancia";
            this.lblStatoBilancia.Size = new System.Drawing.Size(204, 24);
            this.lblStatoBilancia.TabIndex = 61;
            this.lblStatoBilancia.Text = "Stato bilancia";
            this.lblStatoBilancia.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(431, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 24);
            this.label7.TabIndex = 62;
            this.label7.Text = "Differenza";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblDiff
            // 
            this.lblDiff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDiff.Font = new System.Drawing.Font("Tahoma", 18F);
            this.lblDiff.ForeColor = System.Drawing.Color.Lime;
            this.lblDiff.Location = new System.Drawing.Point(431, 95);
            this.lblDiff.Name = "lblDiff";
            this.lblDiff.Size = new System.Drawing.Size(136, 32);
            this.lblDiff.TabIndex = 63;
            this.lblDiff.Text = "0.000";
            this.lblDiff.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPesoBilancia
            // 
            this.lblPesoBilancia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPesoBilancia.Font = new System.Drawing.Font("Tahoma", 18F);
            this.lblPesoBilancia.ForeColor = System.Drawing.Color.Lime;
            this.lblPesoBilancia.Location = new System.Drawing.Point(147, 95);
            this.lblPesoBilancia.Name = "lblPesoBilancia";
            this.lblPesoBilancia.Size = new System.Drawing.Size(136, 32);
            this.lblPesoBilancia.TabIndex = 64;
            this.lblPesoBilancia.Text = "0.000";
            this.lblPesoBilancia.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(147, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 24);
            this.label4.TabIndex = 65;
            this.label4.Text = "Bilancia";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblQtàIngr
            // 
            this.lblQtàIngr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblQtàIngr.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.ingredientiBindingSource, "QtàTeo", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N3"));
            this.lblQtàIngr.Font = new System.Drawing.Font("Tahoma", 18F);
            this.lblQtàIngr.ForeColor = System.Drawing.Color.Lime;
            this.lblQtàIngr.Location = new System.Drawing.Point(5, 95);
            this.lblQtàIngr.Name = "lblQtàIngr";
            this.lblQtàIngr.Size = new System.Drawing.Size(136, 32);
            this.lblQtàIngr.TabIndex = 66;
            this.lblQtàIngr.Text = "0.000";
            this.lblQtàIngr.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(6, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 21);
            this.label3.TabIndex = 67;
            this.label3.Text = "Da pesare";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timerBlink
            // 
            this.timerBlink.Interval = 500;
            this.timerBlink.Tick += new System.EventHandler(this.timerBlink_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(label9);
            this.panel1.Controls.Add(this.txtCurrMisc);
            this.panel1.Controls.Add(label5);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(label2);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(label1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(lottoInCorsoLabel);
            this.panel1.Controls.Add(this.lottoInCorsoTextBox);
            this.panel1.Controls.Add(numeroLottiLabel);
            this.panel1.Controls.Add(this.numeroLottiTextBox);
            this.panel1.Controls.Add(noteLabel);
            this.panel1.Controls.Add(this.noteTextBox);
            this.panel1.Controls.Add(this.descrizioneTextBox);
            this.panel1.Controls.Add(articoloLabel);
            this.panel1.Controls.Add(this.articoloTextBox);
            this.panel1.Controls.Add(this.nrOrdTextBox);
            this.panel1.Controls.Add(nrOrdLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1032, 104);
            this.panel1.TabIndex = 13;
            // 
            // txtCurrMisc
            // 
            this.txtCurrMisc.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.txtCurrMisc.Location = new System.Drawing.Point(627, 6);
            this.txtCurrMisc.Name = "txtCurrMisc";
            this.txtCurrMisc.Size = new System.Drawing.Size(48, 27);
            this.txtCurrMisc.TabIndex = 59;
            // 
            // textBox3
            // 
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.odlBindingSource, "Data", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "t"));
            this.textBox3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.textBox3.Location = new System.Drawing.Point(232, 5);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(101, 27);
            this.textBox3.TabIndex = 50;
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.odlBindingSource, "Destinazione", true));
            this.textBox2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.textBox2.Location = new System.Drawing.Point(734, 36);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(72, 27);
            this.textBox2.TabIndex = 18;
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.odlBindingSource, "QuantitàPerMiscelata", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N0"));
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.textBox1.Location = new System.Drawing.Point(734, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(72, 27);
            this.textBox1.TabIndex = 15;
            // 
            // lottoInCorsoTextBox
            // 
            this.lottoInCorsoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.odlBindingSource, "NumeroMiscelateEseguite", true));
            this.lottoInCorsoTextBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lottoInCorsoTextBox.Location = new System.Drawing.Point(499, 5);
            this.lottoInCorsoTextBox.Name = "lottoInCorsoTextBox";
            this.lottoInCorsoTextBox.Size = new System.Drawing.Size(30, 27);
            this.lottoInCorsoTextBox.TabIndex = 13;
            // 
            // numeroLottiTextBox
            // 
            this.numeroLottiTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.odlBindingSource, "NumeroMiscelate", true));
            this.numeroLottiTextBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.numeroLottiTextBox.Location = new System.Drawing.Point(385, 5);
            this.numeroLottiTextBox.Name = "numeroLottiTextBox";
            this.numeroLottiTextBox.Size = new System.Drawing.Size(28, 27);
            this.numeroLottiTextBox.TabIndex = 12;
            // 
            // noteTextBox
            // 
            this.noteTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.odlBindingSource, "Note", true));
            this.noteTextBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.noteTextBox.Location = new System.Drawing.Point(71, 67);
            this.noteTextBox.Name = "noteTextBox";
            this.noteTextBox.Size = new System.Drawing.Size(715, 27);
            this.noteTextBox.TabIndex = 11;
            // 
            // descrizioneTextBox
            // 
            this.descrizioneTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.odlBindingSource, "Descrizione", true));
            this.descrizioneTextBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.descrizioneTextBox.Location = new System.Drawing.Point(177, 35);
            this.descrizioneTextBox.Name = "descrizioneTextBox";
            this.descrizioneTextBox.Size = new System.Drawing.Size(418, 27);
            this.descrizioneTextBox.TabIndex = 7;
            // 
            // articoloTextBox
            // 
            this.articoloTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.odlBindingSource, "Articolo", true));
            this.articoloTextBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.articoloTextBox.Location = new System.Drawing.Point(71, 35);
            this.articoloTextBox.Name = "articoloTextBox";
            this.articoloTextBox.Size = new System.Drawing.Size(100, 27);
            this.articoloTextBox.TabIndex = 5;
            // 
            // nrOrdTextBox
            // 
            this.nrOrdTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.odlBindingSource, "NumeroOrdine", true));
            this.nrOrdTextBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.nrOrdTextBox.Location = new System.Drawing.Point(71, 5);
            this.nrOrdTextBox.Name = "nrOrdTextBox";
            this.nrOrdTextBox.Size = new System.Drawing.Size(100, 27);
            this.nrOrdTextBox.TabIndex = 1;
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.DataMember = "";
            this.dataGrid1.DataSource = this.ingredientiBindingSource;
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid1.Enabled = false;
            this.dataGrid1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGrid1.Location = new System.Drawing.Point(0, 104);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.PreferredRowHeight = 32;
            this.dataGrid1.RowHeadersVisible = false;
            this.dataGrid1.Size = new System.Drawing.Size(1032, 507);
            this.dataGrid1.TabIndex = 12;
            this.dataGrid1.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.DataGrid = this.dataGrid1;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn2,
            this.dataGridTextBoxColumn3,
            this.dataGridTextBoxColumn4,
            this.dataGridTextBoxColumn5});
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "IBindingList";
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "Codice";
            this.dataGridTextBoxColumn1.MappingName = "Articolo";
            this.dataGridTextBoxColumn1.Width = 150;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "Descrizione";
            this.dataGridTextBoxColumn2.MappingName = "Descrizione";
            this.dataGridTextBoxColumn2.Width = 590;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "n3";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "Teorico";
            this.dataGridTextBoxColumn3.MappingName = "QtàTeo";
            this.dataGridTextBoxColumn3.Width = 75;
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Format = "n3";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "Pesato";
            this.dataGridTextBoxColumn4.MappingName = "Quantità";
            this.dataGridTextBoxColumn4.Width = 75;
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Format = "n3";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "Tolleranza";
            this.dataGridTextBoxColumn5.MappingName = "Tolleranza";
            this.dataGridTextBoxColumn5.Width = 75;
            // 
            // timerKey
            // 
            this.timerKey.Interval = 1500;
            this.timerKey.Tick += new System.EventHandler(this.timerKey_Tick);
            // 
            // odlTableAdapter
            // 
            this.odlTableAdapter.ClearBeforeFill = true;
            // 
            // lottoTableAdapter
            // 
            this.lottoTableAdapter.ClearBeforeFill = true;
            // 
            // silosTableAdapter
            // 
            this.silosTableAdapter.ClearBeforeFill = true;
            // 
            // frmAggMan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1032, 741);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Arial", 14F);
            this.KeyPreview = true;
            this.Name = "frmAggMan";
            this.Text = "Aggiunte Manuali";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmAggMan_Closing);
            this.Closed += new System.EventHandler(this.frmAggMan_Closed);
            this.Load += new System.EventHandler(this.frmAggMan_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ingredientiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.odlBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xFactoryNETDataSet)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private XFactoryNETDataSet xFactoryNETDataSet;
        private System.Windows.Forms.BindingSource odlBindingSource;
        private XFactoryNET.Custom.Panzoo.AggMan.XFactoryNETDataSetTableAdapters.OdlTableAdapter odlTableAdapter;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.BindingSource ingredientiBindingSource;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label lblQtàIngr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblStatoBilancia;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblDiff;
        private System.Windows.Forms.Label lblPesoBilancia;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnTeorico;
        private System.Windows.Forms.Timer timerBlink;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox lottoInCorsoTextBox;
        private System.Windows.Forms.TextBox numeroLottiTextBox;
        private System.Windows.Forms.TextBox noteTextBox;
        private System.Windows.Forms.TextBox descrizioneTextBox;
        private System.Windows.Forms.TextBox articoloTextBox;
        private System.Windows.Forms.TextBox nrOrdTextBox;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnFineMisc;
        private System.Windows.Forms.TextBox txtCurrMisc;
        private System.Windows.Forms.Label lblSac;
        private XFactoryNET.Custom.Panzoo.AggMan.XFactoryNETDataSetTableAdapters.LottoTableAdapter lottoTableAdapter;
        private XFactoryNET.Custom.Panzoo.AggMan.XFactoryNETDataSetTableAdapters.SilosTableAdapter silosTableAdapter;
        private System.Windows.Forms.Timer timerKey;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblSetpoint;
    }
}