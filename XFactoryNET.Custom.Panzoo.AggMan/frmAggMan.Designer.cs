namespace XFactoryNET.Custom.Panzoo.AggMan
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSac = new System.Windows.Forms.Label();
            this.lblStatoBilancia = new System.Windows.Forms.Label();
            this.lblFasePesatura = new System.Windows.Forms.Label();
            this.txtPesoDiff = new System.Windows.Forms.TextBox();
            this.txtPesoBilancia = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ingredientiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.odlBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.xFactoryNETDataSet = new XFactoryNET.Custom.Panzoo.AggMan.XFactoryNETDataSet();
            this.odlBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.panel2 = new System.Windows.Forms.Panel();
            this.quantitàPerLottoLabel = new System.Windows.Forms.Label();
            this.quantitàPerLottoTextBox = new System.Windows.Forms.TextBox();
            this.destinazioneLabel = new System.Windows.Forms.Label();
            this.destinazioneTextBox = new System.Windows.Forms.TextBox();
            this.numeroLottiEseguitiLabel = new System.Windows.Forms.Label();
            this.inCorsoTextBox = new System.Windows.Forms.TextBox();
            this.numeroMiscelateLabel = new System.Windows.Forms.Label();
            this.numeroLottiTextBox = new System.Windows.Forms.TextBox();
            this.noteLabel = new System.Windows.Forms.Label();
            this.noteTextBox = new System.Windows.Forms.TextBox();
            this.dataLabel = new System.Windows.Forms.Label();
            this.dataDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.formulaLabel = new System.Windows.Forms.Label();
            this.formulaTextBox = new System.Windows.Forms.TextBox();
            this.descrizioneTextBox = new System.Windows.Forms.TextBox();
            this.articoloLabel = new System.Windows.Forms.Label();
            this.articoloTextBox = new System.Windows.Forms.TextBox();
            this.nrOrdLabel = new System.Windows.Forms.Label();
            this.nrOrdTextBox = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.odlTableAdapter = new XFactoryNET.Custom.Panzoo.AggMan.XFactoryNETDataSetTableAdapters.OdlTableAdapter();
            this.tableAdapterManager = new XFactoryNET.Custom.Panzoo.AggMan.XFactoryNETDataSetTableAdapters.TableAdapterManager();
            this.componenteTableAdapter = new XFactoryNET.Custom.Panzoo.AggMan.XFactoryNETDataSetTableAdapters.ComponenteTableAdapter();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timerBlink = new System.Windows.Forms.Timer(this.components);
            this.articoloDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descrizioneDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qtàDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qtàTeoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modalitàDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.areaStoccaggioDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ingredientiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.odlBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xFactoryNETDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.odlBindingNavigator)).BeginInit();
            this.odlBindingNavigator.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Quantità da pesare";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(178, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Peso su bilancia";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(352, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "Differenza";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.lblSac);
            this.panel1.Controls.Add(this.lblStatoBilancia);
            this.panel1.Controls.Add(this.lblFasePesatura);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtPesoDiff);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtPesoBilancia);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 435);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(838, 86);
            this.panel1.TabIndex = 1;
            // 
            // lblSac
            // 
            this.lblSac.AutoSize = true;
            this.lblSac.BackColor = System.Drawing.Color.Black;
            this.lblSac.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSac.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSac.ForeColor = System.Drawing.Color.Yellow;
            this.lblSac.Location = new System.Drawing.Point(526, 51);
            this.lblSac.Name = "lblSac";
            this.lblSac.Size = new System.Drawing.Size(59, 26);
            this.lblSac.TabIndex = 8;
            this.lblSac.Text = "SAC.";
            this.lblSac.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatoBilancia
            // 
            this.lblStatoBilancia.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatoBilancia.Location = new System.Drawing.Point(589, 12);
            this.lblStatoBilancia.Name = "lblStatoBilancia";
            this.lblStatoBilancia.Size = new System.Drawing.Size(246, 24);
            this.lblStatoBilancia.TabIndex = 7;
            this.lblStatoBilancia.Text = "Stato bilancia";
            this.lblStatoBilancia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFasePesatura
            // 
            this.lblFasePesatura.BackColor = System.Drawing.Color.Black;
            this.lblFasePesatura.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFasePesatura.ForeColor = System.Drawing.Color.Red;
            this.lblFasePesatura.Location = new System.Drawing.Point(592, 42);
            this.lblFasePesatura.Name = "lblFasePesatura";
            this.lblFasePesatura.Size = new System.Drawing.Size(243, 41);
            this.lblFasePesatura.TabIndex = 6;
            this.lblFasePesatura.Text = "Eseguire tara";
            this.lblFasePesatura.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPesoDiff
            // 
            this.txtPesoDiff.BackColor = System.Drawing.Color.Black;
            this.txtPesoDiff.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPesoDiff.ForeColor = System.Drawing.Color.Lime;
            this.txtPesoDiff.Location = new System.Drawing.Point(352, 39);
            this.txtPesoDiff.Name = "txtPesoDiff";
            this.txtPesoDiff.ReadOnly = true;
            this.txtPesoDiff.Size = new System.Drawing.Size(168, 44);
            this.txtPesoDiff.TabIndex = 4;
            this.txtPesoDiff.Text = "0";
            this.txtPesoDiff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPesoBilancia
            // 
            this.txtPesoBilancia.BackColor = System.Drawing.Color.Black;
            this.txtPesoBilancia.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPesoBilancia.ForeColor = System.Drawing.Color.Lime;
            this.txtPesoBilancia.Location = new System.Drawing.Point(178, 39);
            this.txtPesoBilancia.Name = "txtPesoBilancia";
            this.txtPesoBilancia.ReadOnly = true;
            this.txtPesoBilancia.Size = new System.Drawing.Size(168, 44);
            this.txtPesoBilancia.TabIndex = 2;
            this.txtPesoBilancia.Text = "0";
            this.txtPesoBilancia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.ingredientiBindingSource, "QtàTeo", true));
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.Lime;
            this.textBox1.Location = new System.Drawing.Point(4, 39);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(168, 44);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "0";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ingredientiBindingSource
            // 
            this.ingredientiBindingSource.DataMember = "Ingredienti";
            this.ingredientiBindingSource.DataSource = this.odlBindingSource;
            this.ingredientiBindingSource.CurrentChanged += new System.EventHandler(this.ingredientiBindingSource_CurrentChanged);
            // 
            // odlBindingSource
            // 
            this.odlBindingSource.AllowNew = false;
            this.odlBindingSource.DataMember = "Odl";
            this.odlBindingSource.DataSource = this.xFactoryNETDataSet;
            // 
            // xFactoryNETDataSet
            // 
            this.xFactoryNETDataSet.DataSetName = "XFactoryNETDataSet";
            this.xFactoryNETDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // odlBindingNavigator
            // 
            this.odlBindingNavigator.AddNewItem = null;
            this.odlBindingNavigator.BindingSource = this.odlBindingSource;
            this.odlBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.odlBindingNavigator.DeleteItem = null;
            this.odlBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2});
            this.odlBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.odlBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.odlBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.odlBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.odlBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.odlBindingNavigator.Name = "odlBindingNavigator";
            this.odlBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.odlBindingNavigator.Size = new System.Drawing.Size(838, 25);
            this.odlBindingNavigator.TabIndex = 7;
            this.odlBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.quantitàPerLottoLabel);
            this.panel2.Controls.Add(this.quantitàPerLottoTextBox);
            this.panel2.Controls.Add(this.destinazioneLabel);
            this.panel2.Controls.Add(this.destinazioneTextBox);
            this.panel2.Controls.Add(this.numeroLottiEseguitiLabel);
            this.panel2.Controls.Add(this.inCorsoTextBox);
            this.panel2.Controls.Add(this.numeroMiscelateLabel);
            this.panel2.Controls.Add(this.numeroLottiTextBox);
            this.panel2.Controls.Add(this.noteLabel);
            this.panel2.Controls.Add(this.noteTextBox);
            this.panel2.Controls.Add(this.dataLabel);
            this.panel2.Controls.Add(this.dataDateTimePicker);
            this.panel2.Controls.Add(this.formulaLabel);
            this.panel2.Controls.Add(this.formulaTextBox);
            this.panel2.Controls.Add(this.descrizioneTextBox);
            this.panel2.Controls.Add(this.articoloLabel);
            this.panel2.Controls.Add(this.articoloTextBox);
            this.panel2.Controls.Add(this.nrOrdLabel);
            this.panel2.Controls.Add(this.nrOrdTextBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(838, 177);
            this.panel2.TabIndex = 23;
            // 
            // quantitàPerLottoLabel
            // 
            this.quantitàPerLottoLabel.AutoSize = true;
            this.quantitàPerLottoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quantitàPerLottoLabel.Location = new System.Drawing.Point(638, 84);
            this.quantitàPerLottoLabel.Name = "quantitàPerLottoLabel";
            this.quantitàPerLottoLabel.Size = new System.Drawing.Size(84, 24);
            this.quantitàPerLottoLabel.TabIndex = 18;
            this.quantitàPerLottoLabel.Text = "Quantità:";
            // 
            // quantitàPerLottoTextBox
            // 
            this.quantitàPerLottoTextBox.BackColor = System.Drawing.Color.White;
            this.quantitàPerLottoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.odlBindingSource, "QuantitàPerLotto", true));
            this.quantitàPerLottoTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quantitàPerLottoTextBox.Location = new System.Drawing.Point(728, 81);
            this.quantitàPerLottoTextBox.Name = "quantitàPerLottoTextBox";
            this.quantitàPerLottoTextBox.ReadOnly = true;
            this.quantitàPerLottoTextBox.Size = new System.Drawing.Size(100, 29);
            this.quantitàPerLottoTextBox.TabIndex = 19;
            this.quantitàPerLottoTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // destinazioneLabel
            // 
            this.destinazioneLabel.AutoSize = true;
            this.destinazioneLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.destinazioneLabel.Location = new System.Drawing.Point(599, 119);
            this.destinazioneLabel.Name = "destinazioneLabel";
            this.destinazioneLabel.Size = new System.Drawing.Size(123, 24);
            this.destinazioneLabel.TabIndex = 16;
            this.destinazioneLabel.Text = "Destinazione:";
            // 
            // destinazioneTextBox
            // 
            this.destinazioneTextBox.BackColor = System.Drawing.Color.White;
            this.destinazioneTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.odlBindingSource, "Destinazione", true));
            this.destinazioneTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.destinazioneTextBox.Location = new System.Drawing.Point(728, 116);
            this.destinazioneTextBox.Name = "destinazioneTextBox";
            this.destinazioneTextBox.ReadOnly = true;
            this.destinazioneTextBox.Size = new System.Drawing.Size(100, 29);
            this.destinazioneTextBox.TabIndex = 17;
            // 
            // numeroLottiEseguitiLabel
            // 
            this.numeroLottiEseguitiLabel.AutoSize = true;
            this.numeroLottiEseguitiLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numeroLottiEseguitiLabel.Location = new System.Drawing.Point(570, 50);
            this.numeroLottiEseguitiLabel.Name = "numeroLottiEseguitiLabel";
            this.numeroLottiEseguitiLabel.Size = new System.Drawing.Size(152, 24);
            this.numeroLottiEseguitiLabel.TabIndex = 14;
            this.numeroLottiEseguitiLabel.Text = "Miscelata attuale:";
            // 
            // inCorsoTextBox
            // 
            this.inCorsoTextBox.BackColor = System.Drawing.Color.White;
            this.inCorsoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.odlBindingSource, "MiscelataInCorso", true));
            this.inCorsoTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inCorsoTextBox.Location = new System.Drawing.Point(728, 47);
            this.inCorsoTextBox.Name = "inCorsoTextBox";
            this.inCorsoTextBox.ReadOnly = true;
            this.inCorsoTextBox.Size = new System.Drawing.Size(100, 29);
            this.inCorsoTextBox.TabIndex = 15;
            this.inCorsoTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numeroMiscelateLabel
            // 
            this.numeroMiscelateLabel.AutoSize = true;
            this.numeroMiscelateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numeroMiscelateLabel.Location = new System.Drawing.Point(554, 18);
            this.numeroMiscelateLabel.Name = "numeroMiscelateLabel";
            this.numeroMiscelateLabel.Size = new System.Drawing.Size(168, 24);
            this.numeroMiscelateLabel.TabIndex = 12;
            this.numeroMiscelateLabel.Text = "Numero miscelate:";
            // 
            // numeroLottiTextBox
            // 
            this.numeroLottiTextBox.BackColor = System.Drawing.Color.White;
            this.numeroLottiTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.odlBindingSource, "NumeroMiscelate", true));
            this.numeroLottiTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numeroLottiTextBox.Location = new System.Drawing.Point(728, 15);
            this.numeroLottiTextBox.Name = "numeroLottiTextBox";
            this.numeroLottiTextBox.ReadOnly = true;
            this.numeroLottiTextBox.Size = new System.Drawing.Size(100, 29);
            this.numeroLottiTextBox.TabIndex = 13;
            this.numeroLottiTextBox.Text = "0";
            this.numeroLottiTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // noteLabel
            // 
            this.noteLabel.AutoSize = true;
            this.noteLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noteLabel.Location = new System.Drawing.Point(6, 116);
            this.noteLabel.Name = "noteLabel";
            this.noteLabel.Size = new System.Drawing.Size(55, 24);
            this.noteLabel.TabIndex = 10;
            this.noteLabel.Text = "Note:";
            // 
            // noteTextBox
            // 
            this.noteTextBox.BackColor = System.Drawing.Color.White;
            this.noteTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.odlBindingSource, "Note", true));
            this.noteTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noteTextBox.Location = new System.Drawing.Point(88, 116);
            this.noteTextBox.Multiline = true;
            this.noteTextBox.Name = "noteTextBox";
            this.noteTextBox.ReadOnly = true;
            this.noteTextBox.Size = new System.Drawing.Size(476, 46);
            this.noteTextBox.TabIndex = 11;
            // 
            // dataLabel
            // 
            this.dataLabel.AutoSize = true;
            this.dataLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataLabel.Location = new System.Drawing.Point(194, 15);
            this.dataLabel.Name = "dataLabel";
            this.dataLabel.Size = new System.Drawing.Size(46, 24);
            this.dataLabel.TabIndex = 8;
            this.dataLabel.Text = "Ora:";
            // 
            // dataDateTimePicker
            // 
            this.dataDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.odlBindingSource, "Data", true));
            this.dataDateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dataDateTimePicker.Location = new System.Drawing.Point(246, 12);
            this.dataDateTimePicker.Name = "dataDateTimePicker";
            this.dataDateTimePicker.Size = new System.Drawing.Size(137, 29);
            this.dataDateTimePicker.TabIndex = 9;
            // 
            // formulaLabel
            // 
            this.formulaLabel.AutoSize = true;
            this.formulaLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.formulaLabel.Location = new System.Drawing.Point(6, 84);
            this.formulaLabel.Name = "formulaLabel";
            this.formulaLabel.Size = new System.Drawing.Size(85, 24);
            this.formulaLabel.TabIndex = 6;
            this.formulaLabel.Text = "Formula:";
            // 
            // formulaTextBox
            // 
            this.formulaTextBox.BackColor = System.Drawing.Color.White;
            this.formulaTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.odlBindingSource, "Formula", true));
            this.formulaTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.formulaTextBox.Location = new System.Drawing.Point(88, 81);
            this.formulaTextBox.Name = "formulaTextBox";
            this.formulaTextBox.ReadOnly = true;
            this.formulaTextBox.Size = new System.Drawing.Size(100, 29);
            this.formulaTextBox.TabIndex = 7;
            // 
            // descrizioneTextBox
            // 
            this.descrizioneTextBox.BackColor = System.Drawing.Color.White;
            this.descrizioneTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.odlBindingSource, "Descrizione", true));
            this.descrizioneTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descrizioneTextBox.Location = new System.Drawing.Point(194, 47);
            this.descrizioneTextBox.Name = "descrizioneTextBox";
            this.descrizioneTextBox.ReadOnly = true;
            this.descrizioneTextBox.Size = new System.Drawing.Size(370, 29);
            this.descrizioneTextBox.TabIndex = 5;
            // 
            // articoloLabel
            // 
            this.articoloLabel.AutoSize = true;
            this.articoloLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.articoloLabel.Location = new System.Drawing.Point(6, 50);
            this.articoloLabel.Name = "articoloLabel";
            this.articoloLabel.Size = new System.Drawing.Size(78, 24);
            this.articoloLabel.TabIndex = 2;
            this.articoloLabel.Text = "Articolo:";
            // 
            // articoloTextBox
            // 
            this.articoloTextBox.BackColor = System.Drawing.Color.White;
            this.articoloTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.odlBindingSource, "Articolo", true));
            this.articoloTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.articoloTextBox.Location = new System.Drawing.Point(88, 47);
            this.articoloTextBox.Name = "articoloTextBox";
            this.articoloTextBox.ReadOnly = true;
            this.articoloTextBox.Size = new System.Drawing.Size(100, 29);
            this.articoloTextBox.TabIndex = 3;
            // 
            // nrOrdLabel
            // 
            this.nrOrdLabel.AutoSize = true;
            this.nrOrdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nrOrdLabel.Location = new System.Drawing.Point(6, 15);
            this.nrOrdLabel.Name = "nrOrdLabel";
            this.nrOrdLabel.Size = new System.Drawing.Size(72, 24);
            this.nrOrdLabel.TabIndex = 0;
            this.nrOrdLabel.Text = "Nr Ord:";
            // 
            // nrOrdTextBox
            // 
            this.nrOrdTextBox.BackColor = System.Drawing.Color.White;
            this.nrOrdTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.odlBindingSource, "NrOrd", true));
            this.nrOrdTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nrOrdTextBox.Location = new System.Drawing.Point(88, 12);
            this.nrOrdTextBox.Name = "nrOrdTextBox";
            this.nrOrdTextBox.ReadOnly = true;
            this.nrOrdTextBox.Size = new System.Drawing.Size(100, 29);
            this.nrOrdTextBox.TabIndex = 1;
            this.nrOrdTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.articoloDataGridViewTextBoxColumn,
            this.descrizioneDataGridViewTextBoxColumn,
            this.qtàDataGridViewTextBoxColumn,
            this.qtàTeoDataGridViewTextBoxColumn,
            this.modalitàDataGridViewTextBoxColumn,
            this.areaStoccaggioDataGridViewTextBoxColumn,
            this.statoDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.ingredientiBindingSource;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Enabled = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 202);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(838, 233);
            this.dataGridView1.TabIndex = 24;
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = global::XFactoryNET.Custom.Panzoo.AggMan.Properties.Settings.Default.BaudRate;
            this.serialPort1.DataBits = global::XFactoryNET.Custom.Panzoo.AggMan.Properties.Settings.Default.DataBits;
            this.serialPort1.Parity = global::XFactoryNET.Custom.Panzoo.AggMan.Properties.Settings.Default.Parity;
            this.serialPort1.PortName = global::XFactoryNET.Custom.Panzoo.AggMan.Properties.Settings.Default.PortName;
            this.serialPort1.StopBits = global::XFactoryNET.Custom.Panzoo.AggMan.Properties.Settings.Default.StopBits;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // odlTableAdapter
            // 
            this.odlTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.ComponenteTableAdapter = null;
            this.tableAdapterManager.Connection = null;
            this.tableAdapterManager.LottoTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = XFactoryNET.Custom.Panzoo.AggMan.XFactoryNETDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // componenteTableAdapter
            // 
            this.componenteTableAdapter.ClearBeforeFill = true;
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timerBlink
            // 
            this.timerBlink.Interval = 800;
            this.timerBlink.Tick += new System.EventHandler(this.timerBlink_Tick);
            // 
            // articoloDataGridViewTextBoxColumn
            // 
            this.articoloDataGridViewTextBoxColumn.DataPropertyName = "Articolo";
            this.articoloDataGridViewTextBoxColumn.HeaderText = "Articolo";
            this.articoloDataGridViewTextBoxColumn.Name = "articoloDataGridViewTextBoxColumn";
            this.articoloDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descrizioneDataGridViewTextBoxColumn
            // 
            this.descrizioneDataGridViewTextBoxColumn.DataPropertyName = "Descrizione";
            this.descrizioneDataGridViewTextBoxColumn.HeaderText = "Descrizione";
            this.descrizioneDataGridViewTextBoxColumn.Name = "descrizioneDataGridViewTextBoxColumn";
            this.descrizioneDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // qtàDataGridViewTextBoxColumn
            // 
            this.qtàDataGridViewTextBoxColumn.DataPropertyName = "Qtà";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N4";
            dataGridViewCellStyle2.NullValue = null;
            this.qtàDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.qtàDataGridViewTextBoxColumn.HeaderText = "Qtà";
            this.qtàDataGridViewTextBoxColumn.Name = "qtàDataGridViewTextBoxColumn";
            this.qtàDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // qtàTeoDataGridViewTextBoxColumn
            // 
            this.qtàTeoDataGridViewTextBoxColumn.DataPropertyName = "QtàTeo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N4";
            dataGridViewCellStyle3.NullValue = null;
            this.qtàTeoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.qtàTeoDataGridViewTextBoxColumn.HeaderText = "QtàTeo";
            this.qtàTeoDataGridViewTextBoxColumn.Name = "qtàTeoDataGridViewTextBoxColumn";
            this.qtàTeoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // modalitàDataGridViewTextBoxColumn
            // 
            this.modalitàDataGridViewTextBoxColumn.DataPropertyName = "Modalità";
            this.modalitàDataGridViewTextBoxColumn.HeaderText = "Modalità";
            this.modalitàDataGridViewTextBoxColumn.Name = "modalitàDataGridViewTextBoxColumn";
            this.modalitàDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // areaStoccaggioDataGridViewTextBoxColumn
            // 
            this.areaStoccaggioDataGridViewTextBoxColumn.DataPropertyName = "AreaStoccaggio";
            this.areaStoccaggioDataGridViewTextBoxColumn.HeaderText = "Magazzino";
            this.areaStoccaggioDataGridViewTextBoxColumn.Name = "areaStoccaggioDataGridViewTextBoxColumn";
            this.areaStoccaggioDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // statoDataGridViewTextBoxColumn
            // 
            this.statoDataGridViewTextBoxColumn.DataPropertyName = "Stato";
            this.statoDataGridViewTextBoxColumn.HeaderText = "Stato";
            this.statoDataGridViewTextBoxColumn.Name = "statoDataGridViewTextBoxColumn";
            this.statoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 521);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.odlBindingNavigator);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Aggiunte Manuali";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ingredientiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.odlBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xFactoryNETDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.odlBindingNavigator)).EndInit();
            this.odlBindingNavigator.ResumeLayout(false);
            this.odlBindingNavigator.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Panel panel1;
        private XFactoryNETDataSet xFactoryNETDataSet;
        private System.Windows.Forms.BindingSource odlBindingSource;
        private XFactoryNETDataSetTableAdapters.OdlTableAdapter odlTableAdapter;
        private XFactoryNETDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.Label lblFasePesatura;
        private System.Windows.Forms.TextBox txtPesoDiff;
        private System.Windows.Forms.TextBox txtPesoBilancia;
        private System.Windows.Forms.TextBox textBox1;
        private XFactoryNETDataSetTableAdapters.ComponenteTableAdapter componenteTableAdapter;
        private System.Windows.Forms.BindingSource ingredientiBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.BindingNavigator odlBindingNavigator;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label quantitàPerLottoLabel;
        private System.Windows.Forms.TextBox quantitàPerLottoTextBox;
        private System.Windows.Forms.Label destinazioneLabel;
        private System.Windows.Forms.TextBox destinazioneTextBox;
        private System.Windows.Forms.Label numeroLottiEseguitiLabel;
        private System.Windows.Forms.TextBox inCorsoTextBox;
        private System.Windows.Forms.Label numeroMiscelateLabel;
        private System.Windows.Forms.TextBox numeroLottiTextBox;
        private System.Windows.Forms.Label noteLabel;
        private System.Windows.Forms.TextBox noteTextBox;
        private System.Windows.Forms.Label dataLabel;
        private System.Windows.Forms.DateTimePicker dataDateTimePicker;
        private System.Windows.Forms.Label formulaLabel;
        private System.Windows.Forms.TextBox formulaTextBox;
        private System.Windows.Forms.TextBox descrizioneTextBox;
        private System.Windows.Forms.Label articoloLabel;
        private System.Windows.Forms.TextBox articoloTextBox;
        private System.Windows.Forms.Label nrOrdLabel;
        private System.Windows.Forms.TextBox nrOrdTextBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblStatoBilancia;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timerBlink;
        private System.Windows.Forms.Label lblSac;
        private System.Windows.Forms.DataGridViewTextBoxColumn articoloDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descrizioneDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qtàDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qtàTeoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modalitàDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn areaStoccaggioDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statoDataGridViewTextBoxColumn;
    }
}

