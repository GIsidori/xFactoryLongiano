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
    public partial class frmSacchi : Form
    {
        public frmSacchi()
        {
            InitializeComponent();

            this.giacenzeSacchiTableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Program.ConnectionString);

            FieldInfo fi = dataGrid1.GetType().GetField("m_sbVert", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance);
            VScrollBar bar = (VScrollBar)fi.GetValue(dataGrid1);
            bar.ValueChanged += new EventHandler(bar_ValueChanged);
            bar.Width = 50;

        }

        void bar_ValueChanged(object sender, EventArgs e)
        {
            //Resetta il timer
            timer1.Enabled = false;
            timer1.Enabled = true;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Program.frmGiacenze.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.frmAggMan.Show();
            this.Hide();
        }

        private void frmSacchi_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'xFactoryNETDataSet.GiacenzeSacchi' table. You can move, or remove it, as needed.
            this.giacenzeSacchiTableAdapter.Fill(this.xFactoryNETDataSet.GiacenzaSacchi);
            this.button3.ForeColor = Color.Black;
            Program.frmAggMan.Running += new EventHandler(frmAggMan_Running);
        }

        void frmAggMan_Running(object sender, EventArgs e)
        {
            this.button3.ForeColor = Color.Red;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.giacenzeSacchiTableAdapter.Fill(this.xFactoryNETDataSet.GiacenzaSacchi);
        }

        private void frmSacchi_Activated(object sender, EventArgs e)
        {
            this.giacenzeSacchiTableAdapter.Fill(this.xFactoryNETDataSet.GiacenzaSacchi);
            timer1.Enabled = true;
        }

        private void frmSacchi_Deactivate(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

    }
}