using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using XFactoryNET.Custom.Panzoo.ServiceLibrary;

namespace XFactoryNET.Custom.Panzoo.WindowsService
{
    public partial class XFactoryNETService : ServiceBase
    {
        internal static ServiceHost myServiceHost = null; 

        public XFactoryNETService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (myServiceHost != null)
            {
                myServiceHost.Close();
            }

            SvcDosaggio svcDosaggio = new SvcDosaggio();
            myServiceHost = new ServiceHost(svcDosaggio);

            myServiceHost.Open();

            svcDosaggio.Start();

        }

        protected override void OnStop()
        {
            if (myServiceHost != null)
            {
                ((SvcDosaggio)myServiceHost.SingletonInstance).Stop();
                myServiceHost.Close();
                myServiceHost = null;
            }
        }

    }
}
