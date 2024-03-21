using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XFactoryNET.Custom.Panzoo.ServiceLibrary;
using System.ServiceModel;

namespace XFactoryNETServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Starting...:");

            SvcDosaggio svcDosaggio = new SvcDosaggio();
            svcDosaggio.Start();

            ServiceHost myServiceHost = new ServiceHost(svcDosaggio);

            myServiceHost.Open();
            Console.WriteLine("Listening...");
            Console.WriteLine("Press q to quit");
            do {} while (Console.Read() != 'q');

            myServiceHost.Close();

        }
    }
}
