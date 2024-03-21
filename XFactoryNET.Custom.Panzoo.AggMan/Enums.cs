using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace XFactoryNET.Common
{
    public enum StatoOdL
    {
        InPreparazione,
        Pronto,
        Avviato,
        InEsecuzione,
        Annullato,
        Eseguito = -1,
        Archiviato = -2
    }

    public enum StatoLotto
    {
        InPreparazione,
        Pronto,
        Avviato,
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

    public enum TipoLavorazione
    {
        EntrataMerci,
        Trasformazione,
        UscitaMerci
    }

}
