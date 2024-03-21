using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace XFactoryNET.WinCE
{
    public partial class frmGiacenze : Form
    {
        public frmGiacenze()
        {
            InitializeComponent();

            this.giacenzeSilosTableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Program.ConnectionString);

            FieldInfo fi = dataGrid2.GetType().GetField("m_sbVert", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance);
            VScrollBar bar = (VScrollBar)fi.GetValue(dataGrid2);
            bar.ValueChanged += new EventHandler(bar_ValueChanged);
            bar.Width = 50;

            Program.frmAggMan.Running += new EventHandler(frmAggMan_Running);
        }

        void bar_ValueChanged(object sender, EventArgs e)
        {
            //Resetta il timer
            timer1.Enabled = false;
            timer1.Enabled = true;
        }

        void frmAggMan_Running(object sender, EventArgs e)
        {
            this.buttonPesa.ForeColor = Color.Red;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.frmAggMan.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.frmSacchi.Show();
            this.Hide();
        }

        private void frmGiacenze_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'xFactoryNETDataSet.Apparato' table. You can move, or remove it, as needed.
            this.giacenzeSilosTableAdapter.Fill(this.xFactoryNETDataSet.Silos);
            this.buttonPesa.ForeColor = Color.Black;


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.giacenzeSilosTableAdapter.Fill(this.xFactoryNETDataSet.Silos);
        }

        private void frmGiacenze_Activated(object sender, EventArgs e)
        {
            this.giacenzeSilosTableAdapter.Fill(this.xFactoryNETDataSet.Silos);
            timer1.Enabled = true;
        }

        private void frmGiacenze_Deactivate(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}