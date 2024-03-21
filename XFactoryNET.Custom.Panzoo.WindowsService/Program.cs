using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using XFactoryNET.Custom.Panzoo.ServiceLibrary;

namespace XFactoryNET.Custom.Panzoo.WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new XFactoryNETService() 
            };
            ServiceBase.Run(ServicesToRun);

        }
    }
}
