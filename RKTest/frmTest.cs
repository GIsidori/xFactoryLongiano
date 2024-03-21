using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ISISoft.Siemens.Library;

namespace RKTest
{
    public partial class frmTest : Form
    {
        private RK512 rk512;
        

        public frmTest()
        {
            InitializeComponent();
            this.comboBox1.DataSource = Enum.GetValues(typeof(ISISoft.Siemens.Library.RK512.rkEnum));
            rk512 = new RK512();
            rk512.DataSend += new EventHandler<DataEventArgs>(rk512_DataSend);
            rk512.DataReceive += new EventHandler<DataEventArgs>(rk512_DataReceive);

        }

        void rk512_DataReceive(object sender, DataEventArgs e)
        {
            listBox1.Items.Add(string.Format("PC<-PLC: {0}", DumpToHex(e.Data)));
        }

        void rk512_DataSend(object sender, DataEventArgs e)
        {
            listBox1.Items.Add(string.Format("PC->PLC: {0}", DumpToHex(e.Data)));
        }

        private string DumpToHex(string data)
        {
            string s = null;
            for (int i = 0; i < data.Length; i++)
            {
                if (s != null)
                    s += " ";
                s += ((int)data[i]).ToString("X2");
            }
            return s;
        }

        private string DumpToHex(int[] data)
        {
            string s = null;
            for (int i = 0; i < data.Length; i++)
            {
                if (s != null)
                    s += " ";
                s += data[i].ToString("X4");
            }
            return s;
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            int len = Convert.ToInt16(numLEN.Value);
            int[] buff = new int[len];

            int rc = rk512.RK512Get((RK512.rkEnum)this.comboBox1.SelectedItem,Convert.ToInt16(numDB.Value),Convert.ToInt16(numDW.Value),len,ref buff);

            listBox1.Items.Add(string.Format("GET complete rc={0} Data: {1}", rc,DumpToHex(buff)));

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (rk512.IsOpen == false)
                rk512.OpenPort(txtSettings.Text);

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
