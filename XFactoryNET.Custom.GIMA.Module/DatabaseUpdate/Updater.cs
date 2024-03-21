using System;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using XFactoryNET.Module.BusinessObjects;
//using DevExpress.ExpressApp.Reports;
//using DevExpress.ExpressApp.PivotChart;
//using DevExpress.ExpressApp.Security.Strategy;
//using XFactoryNET.Custom.GIMA.Module.Module.BusinessObjects;

namespace XFactoryNET.Custom.GIMA.Module.DatabaseUpdate
{
    // Allows you to handle a database update when the application version changes (http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppUpdatingModuleUpdatertopic help article for more details).
    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion)
        {
        }
        // Override to specify the database update code which should be performed after the database schema is updated (http://documentation.devexpress.com/#Xaf/DevExpressExpressAppUpdatingModuleUpdater_UpdateDatabaseAfterUpdateSchematopic).
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();


            //string name = "MyName";
            //DomainObject1 theObject = ObjectSpace.FindObject<DomainObject1>(CriteriaOperator.Parse("Name=?", name));
            //if(theObject == null) {
            //    theObject = ObjectSpace.CreateObject<DomainObject1>();
            //    theObject.Name = name;
            //}

            if (this.CurrentDBVersion == new Version(0, 0, 0))
            {

                XPObjectSpace os = (XPObjectSpace)this.ObjectSpace;
                System.Data.SqlClient.SqlConnection conn = (System.Data.SqlClient.SqlConnection)os.Connection;
                System.Data.SqlClient.SqlConnectionStringBuilder bld = new System.Data.SqlClient.SqlConnectionStringBuilder();
                bld.ConnectionString = conn.ConnectionString;

                string dbPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                dbPath = System.IO.Path.GetDirectoryName(dbPath);

                string dataSource = bld.DataSource;
                string catalog = bld.InitialCatalog;
                string userID = bld.UserID;
                string password = bld.Password;
                bool integratedSecurity = bld.IntegratedSecurity;

                ExecuteScript(dataSource, catalog, userID, password, integratedSecurity, dbPath + "\\DatabaseUpdate\\InitDB.sql");
            }

        }

        // Override to perform the required changes with the database structure before the database schema is updated (http://documentation.devexpress.com/#Xaf/DevExpressExpressAppUpdatingModuleUpdater_UpdateDatabaseBeforeUpdateSchematopic).
        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            /*
            if (ViewExists("QAllegato") == false)
                ExecuteNonQueryCommand(
                    "CREATE VIEW [dbo].[QAllegato] " +
                    "AS SELECT     RTRIM(Allegato) AS Allegato, TipoVariazione " +
                    "FROM [CB-SRV-PROD].DB2Link.dbo.QAllegato AS Allegati", false);

            if (this.ViewExists("QArticolo") == false)
                ExecuteNonQueryCommand(
                    "CREATE VIEW [dbo].[QArticolo] " +
                    "AS SELECT     RTRIM(Codice) AS Codice, RTRIM(Descrizione) AS Descrizione " +
                    "FROM [CB-SRV-PROD].DB2Link.dbo.QArticolo AS Articolo", false);

            if (this.ViewExists("QFormula") == false)
                ExecuteNonQueryCommand(
                    "CREATE VIEW [dbo].[QFormula] " + 
                    "AS SELECT     RTRIM(Codice) AS Codice, RTRIM(Descrizione) AS Descrizione, RTRIM(Note) AS Note, TipoFormula, Classe " +
                    "FROM [CB-SRV-PROD].DB2Link.dbo.QFormula AS Formula", false);

            if (this.ViewExists("QDettaglioAllegato") == false)
                ExecuteNonQueryCommand(
                    "CREATE VIEW [dbo].[QDettaglioAllegato] " +
                    "AS SELECT     RTRIM(Allegato) AS Allegato, TipoVariazione, Segno, RTRIM(Articolo) AS Articolo, Percentuale " +
                    "FROM [CB-SRV-PROD].DB2Link.dbo.QDettaglioAllegato AS DettaglioAllegato", false);


            if (this.ViewExists("QComponente") == false)
                ExecuteNonQueryCommand(
                    "CREATE VIEW [dbo].[QComponente] "+
                    "AS SELECT     RTRIM(Formula) AS Formula,Versione, RTRIM(Articolo) AS Articolo, Percentuale, Tolleranza, RTRIM(Modalità) AS Modalità " +
                    "FROM [CB-SRV-PROD].DB2Link.dbo.QComponente AS Componente", false);

            if (ViewExists("QFormuleArticoli") == false)
                ExecuteNonQueryCommand(
                    "CREATE VIEW [dbo].[QFormuleArticoli] " +
                    "AS SELECT RTRIM(Articolo) AS Articolo, RTRIM(Formula) AS Formula " +
                    "FROM [CB-SRV-PROD].DB2Link.dbo.QFormuleArticoli AS FormuleArticoli", false);
            */

            base.UpdateDatabaseBeforeUpdateSchema();

            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }

        private bool ViewExists(string viewName)
        {
            return (int)this.ExecuteScalarCommand(string.Format("SELECT COUNT(*) FROM sys.views WHERE object_id = OBJECT_ID(N'[{0}]')", viewName), true) != 0;
        }

        private void ExecuteScript(string server, string catalog, string userName, string password, bool trusted, string file)
        {
            //string appName = @"c:\Programmi\Microsoft SQL Server\90\Tools\Binn\SQLCMD.EXE";
            string appName = "SQLCMD.EXE";
            //string appName = "OSQL.EXE";

            string argument;
            if (trusted)
                argument = string.Format(@"-S {0} -d {1} -E -i ""{2}"" ", server, catalog, file);
            else
                argument = string.Format(@"-S {0} -d {1} -U {2} -P {3} -i ""{4}"" ", server, catalog, userName, password, file);

            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = appName;
            startInfo.Arguments = argument;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = trusted;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;
            try
            {
                RunApplication(startInfo, 120000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static void RunApplication(System.Diagnostics.ProcessStartInfo startInfo, int timeToWait)
        {
            using (System.Diagnostics.Process appToRun = new System.Diagnostics.Process())
            {
                string args = "no";
                if (!string.IsNullOrEmpty(startInfo.Arguments))
                {
                    args = String.Format("\"{0}\"", startInfo.Arguments);
                }
                appToRun.StartInfo = startInfo;
                appToRun.Start();
                if (!appToRun.WaitForExit(timeToWait))
                {
                    Console.WriteLine("Timeout");
                    return;
                }
                if (appToRun.ExitCode != 0)
                {
                    string err = appToRun.StandardError.ReadToEnd();
                    Console.WriteLine("Failed");
                }
                else
                {
                    string output = appToRun.StandardOutput.ReadToEnd();
                }
            }
        }

    }
}
