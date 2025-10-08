using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace XFactoryNET.Custom.Panzoo.AggMan
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            txtFile.Text = config.FilePath;
        }

        public static string GetConnectionString()
        {
            Properties.Settings settings = Properties.Settings.Default;

            System.Data.SqlClient.SqlConnectionStringBuilder bld = new System.Data.SqlClient.SqlConnectionStringBuilder();
            bool connectionOK = true;
            do
            {
                if (!settings.DontAsk || !connectionOK)
                {
                    frmSettings frm = new frmSettings();
                    DialogResult result = frm.ShowDialog();
                    if (result == DialogResult.Cancel)
                    {
                        //winApplication.Exit();
                        return null;
                    }

                }

                bld.IntegratedSecurity = !settings.SQLAuth;
                bld.DataSource = settings.Server;
                bld.InitialCatalog = settings.Database;
                if (!bld.IntegratedSecurity)
                {
                    bld.UserID = settings.UserID;
                    bld.Password = settings.Password;
                }

                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(bld.ConnectionString);

                try
                {
                    conn.Open();
                    conn.Close();
                    connectionOK = true;
                }
                catch (System.Data.SqlClient.SqlException sqlex)
                {
                    if (sqlex.Number != 4060)
                    {
                        connectionOK = false;
                        DialogResult res = MessageBox.Show(sqlex.Message, "Errore di connessione al database SQL", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        if (res == DialogResult.Cancel)
                        {
                            //winApplication.Exit();
                            return null;
                        }
                    }
                }
                finally
                {
                    conn.Dispose();
                }

            } while (!connectionOK);

            return bld.ConnectionString;


        }

    }
}
