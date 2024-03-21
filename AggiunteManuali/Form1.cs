using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AggiunteManuali
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'xFactoryNETDataSet.Odl' table. You can move, or remove it, as needed.
            this.odlTableAdapter.Fill(this.xFactoryNETDataSet.Odl);

        }
    }
}