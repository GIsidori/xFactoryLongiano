    using System;
using System.ServiceModel;
namespace XFactoryNET.Custom.Panzoo.ServiceLibrary
{
    [ServiceContract]
    interface ISvcDosaggio
    {
        [OperationContract]
        void Arresta();
        [OperationContract]
        void Avvia(int oid);
        [OperationContract]
        int CurrMisc();
        [OperationContract]
        decimal GetPeso(int iBil);
        [OperationContract]
        int IdOdL();
        [OperationContract]
        int NrIngrediente(int iBil);
        [OperationContract]
        int NrMisc();
        [OperationContract]
        decimal QtàEstratta(int iBil);
        [OperationContract]
        bool Running();
        [OperationContract]
        bool StatoBilancia(int iBil);
        [OperationContract]
        void StopMisc(int nrMisc);
    }
}
