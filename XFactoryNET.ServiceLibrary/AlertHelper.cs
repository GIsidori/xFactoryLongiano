using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XFactoryNET.Custom.MM1.ServiceLibrary
{
    public enum AlertSeverity
    {
        Debug,
        Information,
        Warning,
        Error,
        Panic
    }

    public static class AlertProvider
    {
        public static void AddAlert(string Message, string IDCiclo, AlertSeverity alertSeverity)
        {
            System.Console.WriteLine("{0}-{3}-{1}: {2}", System.DateTime.Now, IDCiclo, Message, alertSeverity.ToString());
        }
    }
}
