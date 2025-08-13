using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace XFactoryNET.Module.BusinessObjects
{
    public enum StatoOdL
    {
        InPreparazione,     //In editing
        Pronto,             //Verificato e avviabile
        Avviato,            //Avviato
        InEsecuzione,       //In esecuzione su PLC
        Annullato,          //Abort
        Eseguito = -1      //Eseguito
    }
    
    public enum StatoOdP
    {
        Nuovo,
        Disponibile,
        InProduzione,
        Eseguito = -1
    }

    //Valori negativi indicano prelievi di materiale
    //Valori positivi indicano stoccaggio di materiale
    public enum TipoMovimento
    {
        Giacenza = 0,
        EntrataMerci = 1,
        Produzione = 2,
        Recupero = 3,
        Consumo = -1,
        UscitaMerci = -2,
        Scarto = -3
    }

    public enum StatoLotto
    {
        InPreparazione,
        Pronto,
        InEsecuzione,
        Sospeso,
        Eseguito
    }

    public enum StatoTrasporto
    {
        Arrestato,
        InAvvio,
        InArresto,
        Avviato,
        InManuale,
        InCambio
    }

    public enum StatoComponente
    {
        Pronto,
        Insufficiente,
        Assente,
        Eseguito
    }

    public enum StatoModificaComponente
    {
        Originale,
        Modificato,
        Aggiunto,
        Eliminato
    }


    public enum TipoLavorazione
    {
        EntrataMerci,
        Trasformazione,
        UscitaMerci
    }

    //public enum TipoSostituzione
    //{
    //    Nessuna,
    //    Manuale,
    //    Automatica
    //}

}
