using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace XFactoryNET.Custom.MM1.ServiceLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SvcDosaggio : ISvcDosaggio
    {
        public void Start()
        {
            ThreadDos.StartThread();
        }

        public void Stop()
        {
            ThreadDos.StopThread();
        }


        public decimal GetPeso(int nrBil)
        {
            return ThreadDos.PesoBil(nrBil);
        }
    }
}
