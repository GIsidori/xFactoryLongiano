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
//using DevExpress.ExpressApp.Reports;
//using DevExpress.ExpressApp.PivotChart;
using DevExpress.ExpressApp.Security.Strategy;
//using XFactoryNET.Module.BusinessObjects;
using System.Drawing;
using XFactoryNET.Module.BusinessObjects;

namespace XFactoryNET.Module.DatabaseUpdate
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

            // If a user named 'Admin' does not exist in the database, create this user
            SecuritySystemUser  adminUser = ObjectSpace.FindObject<SecuritySystemUser>(new BinaryOperator("UserName", "Admin"));
            if (adminUser == null)
            {
                adminUser = ObjectSpace.CreateObject<SecuritySystemUser>();
                adminUser.UserName = "Admin";
                // Set a password if the standard authentication type is used
                adminUser.SetPassword("Admin");
            }
            // If a user named 'User' does not exist in the database, create this user
            SecuritySystemUser user = ObjectSpace.FindObject<SecuritySystemUser>(new BinaryOperator("UserName", "User"));
            if (user == null)
            {
                user = ObjectSpace.CreateObject<SecuritySystemUser>();
                user.UserName = "User";
                // Set a password if the standard authentication type is used
                user.SetPassword("User");
            }

            // If a role with the Administrators name does not exist in the database, create this role
            SecuritySystemRole adminRole = ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", "Administrators"));
            if (adminRole == null)
            {
                adminRole = ObjectSpace.CreateObject<SecuritySystemRole>();
                adminRole.Name = "Administrators";
                adminRole.IsAdministrative = true;
            }

            // If a role with the Users name does not exist in the database, create this role
            SecuritySystemRole userRole = ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", "Users"));
            if (userRole == null)
            {
                userRole = ObjectSpace.CreateObject<SecuritySystemRole>();
                userRole.Name = "Users";
                userRole.IsAdministrative = false;

                //SecuritySystemTypePermissionObject userTypePermission =
                //    ObjectSpace.CreateObject<SecuritySystemTypePermissionObject>();
                //userTypePermission.TargetType = typeof(SecuritySystemUser);
                //SecuritySystemObjectPermissionsObject currentUserObjectPermission =
                //    ObjectSpace.CreateObject<SecuritySystemObjectPermissionsObject>();
                //currentUserObjectPermission.Criteria = "[Oid] = CurrentUserId()";
                //currentUserObjectPermission.AllowNavigate = true;
                //currentUserObjectPermission.AllowRead = true;
                //userTypePermission.ObjectPermissions.Add(currentUserObjectPermission);
                //userRole.TypePermissions.Add(userTypePermission);

            }

            // Save the Users role to the database
            userRole.Save();
            // Add the Administrators role to the user1
            adminUser.Roles.Add(adminRole);
            // Add the Users role to the user2
            user.Roles.Add(userRole);
            // Save the users to the database
            adminUser.Save();
            user.Save();

            OdlDosaggio.GetOrCreateLavorazione(ObjectSpace,OdlDosaggio.IDLavorazione);
            OdlInsacco.GetOrCreateLavorazione(ObjectSpace, OdlInsacco.IDLavorazione);
            OdlPellet.GetOrCreateLavorazione(ObjectSpace, OdlPellet.IDLavorazione);

            OdlCaricoRinfusa.GetOrCreateLavorazione(ObjectSpace, OdlCaricoRinfusa.IDLavorazione);
            OdlScaricoRinfusa.GetOrCreateLavorazione(ObjectSpace, OdlScaricoRinfusa.IDLavorazione);
            OdlUscitaSacchi.GetOrCreateLavorazione(ObjectSpace, OdlUscitaSacchi.IDLavorazione);
            OdlEntrataSacchi.GetOrCreateLavorazione(ObjectSpace, OdlEntrataSacchi.IDLavorazione);


        }

        // Override to perform the required changes with the database structure before the database schema is updated (http://documentation.devexpress.com/#Xaf/DevExpressExpressAppUpdatingModuleUpdater_UpdateDatabaseBeforeUpdateSchematopic).
        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }

        
    }
}
