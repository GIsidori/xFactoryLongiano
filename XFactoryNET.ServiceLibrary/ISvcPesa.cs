using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace XFactoryNET.ServiceLibrary
{
    [ServiceContract]
    public interface ISvcPesa
    {
        [OperationContract]
        float LeggiPeso(string codicePesa);
    }
}
