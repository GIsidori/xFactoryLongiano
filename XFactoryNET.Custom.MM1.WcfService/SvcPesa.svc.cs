using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using XFactoryNET.Custom.MM1.ServiceLibrary;

namespace XFactoryNET.Custom.MM1.WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SvcPesa" in code, svc and config file together.
    public class SvcPesa : XFactoryNET.ServiceLibrary.ISvcPesa
    {

        public float LeggiPeso(string codicePesa)
        {
            return ThreadPesa.GetPeso(codicePesa);
        }
    }
}
