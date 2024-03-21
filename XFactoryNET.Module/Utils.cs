using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;

namespace XFactoryNET.Module
{
    class Utils
    {
        public static IObjectSpace GetSecondObjectSpace(PersistentBase obj)
        {
            System.Data.IDbConnection conn = obj.Session.Connection;
            if (obj.Session.GetType() == typeof(NestedUnitOfWork))
            {
                var nuow = (NestedUnitOfWork)obj.Session;
                conn = nuow.Parent.Connection;
            }
            var dl = XpoDefault.GetDataLayer(conn, DevExpress.Xpo.DB.AutoCreateOption.SchemaAlreadyExists);
            var xpObjectSpaceDataProvider = new XPObjectSpaceProvider(new ConnectionDataStoreProvider(conn));
            IObjectSpace os = (IObjectSpace)xpObjectSpaceDataProvider.CreateUpdatingObjectSpace(false);
            return os;
        }

    }
}
