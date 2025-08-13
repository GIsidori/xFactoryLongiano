using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace XFactoryNET.Custom.MM1.ServiceLibrary
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITrasporto" in both code and config file together.
    [ServiceContract]
    public interface ISvcTrasporto
    {
        [OperationContract]
        void Avvia(int codice, int source, int dest, int lotto);
        [OperationContract]
        void Arresta(int codice, int source, int dest, int lotto);
        [OperationContract]
        void CambiaSource(int codice, int source, int dest, int lotto);
        [OperationContract]
        void CambiaDest(int codice, int source, int dest, int lotto);
    }
}
