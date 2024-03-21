using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ISISoft.Siemens.Library;

namespace RKTest
{
    public partial class Form1 : Form
    {
        private RK512 rk512;
        

        public Form1()
        {
            InitializeComponent();
            this.comboBox1.DataSource = Enum.GetValues(typeof(ISISoft.Siemens.Library.RK512.rkEnum));
            rk512 = new RK512();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            int len = Convert.ToInt16(numLEN.Value);
            int[] buff = new int[len];

            int rc = rk512.RK512Get((RK512.rkEnum)this.comboBox1.SelectedItem,Convert.ToInt16(numDB.Value),Convert.ToInt16(numDW.Value),len,buff);

            listBox1.Items.Add(string.Format("GET complete rc= {0}", rc));
            DisplayBuffer(buff);

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (rk512.IsOpen == false)
                rk512.OpenPort(txtSettings.Text);

        }

        private void DisplayBuffer(int[] buff)
        {
            string txt = string.Empty;
            for (int i = 0;i<buff.Length;i++)
            {
                if (i > 0)
                    txt += ' ';
                txt += buff[i].ToString("X2");
            }
            listBox1.Items.Add(txt);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (rk512.IsOpen)
                rk512.ClosePort();
        }

        private void btnPut_Click(object sender, EventArgs e)
        {
            int len = Convert.ToInt16(numLEN.Value);
            int[] buff = new int[len];
            int val = Convert.ToInt16(numVAL.Value);
            for (int i = 0; i < len; i++)
            {
                buff[i] = val;
            }

            int rc= rk512.RK512Put((RK512.rkEnum)this.comboBox1.SelectedItem, Convert.ToInt16(numDB.Value), Convert.ToInt16(numDW.Value), len, buff);
            listBox1.Items.Add(string.Format("PUT complete rc= {0}", rc));


        }
    }
}
