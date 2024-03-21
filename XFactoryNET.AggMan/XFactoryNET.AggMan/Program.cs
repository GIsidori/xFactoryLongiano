using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace XFactoryNET.WinCE
{
    static class Program
    {
        public static string ConnectionString;
        public static frmAggMan frmAggMan;
        //public static frmGiacenze frmGiacenze;
        //public static frmSacchi frmSacchi;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            ConnectionString = MobileConfiguration.Settings["ConnectionString"];
            frmAggMan = new frmAggMan();
            //frmGiacenze = new frmGiacenze();
            //frmSacchi = new frmSacchi();
            Application.Run(frmAggMan);
        }
    }
}