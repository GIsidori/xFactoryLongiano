using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace XFactoryNET.Custom.MM1.ServiceLibrary
{
    [ServiceContract]
    interface ISvcDosaggio
    {
        [OperationContract]
        decimal GetPeso(int nrBil);

    }
}
