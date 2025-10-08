using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace XFactoryNET.Custom.Panzoo.AggMan
{
    static class Program
    {
        public static string ConnectionString;
        public static frmAggMan frmAggMan;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            //ConnectionString = Properties.Settings.Default.ConnectionString;
            ConnectionString = frmSettings.GetConnectionString();
            if (ConnectionString != null)
            {
                try
                {
                    frmAggMan = new frmAggMan();
                    Application.Run(frmAggMan);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"{ex.Message}\n{ex.Number}\n{ex.Procedure}\n{ex.InnerException}\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
            else
                Application.Exit();
        }
    }
}