using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

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
                frmAggMan = new frmAggMan();
                Application.Run(frmAggMan);
            }
            else
                Application.Exit();
        }
    }
}