using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PLCConfig
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            string selected = cboxConfig.Text;
            cboxConfig.Items.Clear();
            string[] names = DotNetSiemensPLCToolBoxLibrary.Communication.PLCConnectionConfiguration.GetConfigurationNames();
            foreach (var name in names)
            {
                int index = cboxConfig.Items.Add(name);
                if (name == selected)
                    cboxConfig.SelectedIndex = index;
            }
            if (cboxConfig.Items.Count>0 && cboxConfig.SelectedIndex == -1)
                cboxConfig.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DotNetSiemensPLCToolBoxLibrary.Communication.Configuration.ShowConfiguration(cboxConfig.Text, true);
            RefreshList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confermi l'eliminazione della configurazione selezionata","Conferma eliminazione",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) 
            {
                DotNetSiemensPLCToolBoxLibrary.Communication.PLCConnectionConfiguration.DeleteConfiguration(cboxConfig.Text);
                RefreshList();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
