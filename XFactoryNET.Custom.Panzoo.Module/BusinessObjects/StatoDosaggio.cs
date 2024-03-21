using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using XFactoryNET.Custom.Panzoo.Module.Services.Dosaggio;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using XFactoryNET.Module.BusinessObjects;
using System.ComponentModel;
using DevExpress.Data.Filtering;

namespace XFactoryNET.Custom.Panzoo.Module.BusinessObjects
{

    [NonPersistent]
    public class StatoBilancia:PersistentBase
    {
        SvcDosaggioClient svc;
        int nrBil;
        StatoDosaggio statoDosaggio;

        public StatoBilancia(StatoDosaggio statoDosaggio, SvcDosaggioClient svc,int nrBilancia):base(statoDosaggio.Session)
        {
            this.svc = svc;
            this.statoDosaggio = statoDosaggio;
            nrBil = nrBilancia;
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (propertyName == "NrIngrediente" || propertyName == "CurrMisc")
            {
                if (statoDosaggio.Odl != null)
                    Ingrediente = statoDosaggio.Odl.Ingredienti.FirstOrDefault(c => c.NrComp == this.NrIngrediente && c.NrMisc == statoDosaggio.CurrMisc && c.ApparatoLavorazione == this.bilancia);
                else
                    Ingrediente = null;

            }
        }

        [Key,Browsable(false)]
        public int NrBil
        {
            get { return nrBil; }
            set { SetPropertyValue<int>("NrBil", ref nrBil, value); }
        }

        float peso;
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "False")]
        public float Peso
        {
            get { return peso; }
            set { SetPropertyValue<float>("Peso", ref peso, value); }
        }
        private bool stato;
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "False")]
        public bool Stato
        {
            get { return stato; }
            set { SetPropertyValue<bool>("Stato", ref stato, value); }
        }

        private string errorString;
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "False")]
        public string ErrorString
        {
            get { return errorString; }
            set { SetPropertyValue<string>("ErrorString", ref errorString, value); }
        }


        private int currMisc;
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "False")]
        public int CurrMisc
        {
            get { return currMisc; }
            set { SetPropertyValue<int>("CurrMisc", ref currMisc, value); }
        }

        private int nrIngrediente;
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "False")]
        public int NrIngrediente
        {
            get { return nrIngrediente; }
            set { SetPropertyValue<int>("NrIngrediente", ref nrIngrediente, value); }
        }


        private float qtàEstratta;
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "False")]
        public float QtàEstratta
        {
            get { return qtàEstratta; }
            set { SetPropertyValue<float>("QtàEstratta", ref qtàEstratta, value); }
        }

        private Bilancia bilancia;
        private Bilancia Bilancia
        {
            get
            {
                if (bilancia == null)
                    bilancia = Session.FindObject<Bilancia>(PersistentCriteriaEvaluationBehavior.InTransaction, new BinaryOperator("Numero",nrBil+1));
                return bilancia;
            }
        }

        public string CodiceBilancia
        {
            get
            {
                if (Bilancia != null)
                    return Bilancia.Codice;
                return null;
            }
        }

        internal Lotto Ingrediente;

        public string Codice
        {
            get
            {
                if (Ingrediente != null && Ingrediente.Articolo != null)
                    return Ingrediente.Articolo.Codice;
                return null;
            }
        }

        public string Descrizione
        {
            get
            {
                if (Ingrediente != null && Ingrediente.Articolo != null)
                    return Ingrediente.Articolo.Descrizione;
                return null;
            }
        }

 

        public string Silos
        {
            get
            {
                if (Ingrediente != null && Ingrediente.Silos != null)
                    return Ingrediente.Silos.Codice;
                return null;
            }
        }



        [ModelDefault("AllowEdit", "False")]
        public float QtàTeorica
        {
            get
            {
                if (Ingrediente != null)
                    return Ingrediente.QtàTeo;
                return 0F;
            }
        }

        public void RefreshData()
        {
            try
            {
                this.NrIngrediente = svc.NrIngrediente(nrBil);
                this.Peso = svc.GetPeso(nrBil);
                this.QtàEstratta = svc.QtàEstratta(nrBil);
                this.Stato = svc.StatoBilancia(nrBil);
                ErrorString = string.Empty;
                OnLoaded();
            }
            catch (Exception ex)
            {
                ErrorString = ex.Message;
            }

        }


    }

    [NonPersistent]
    public class StatoDosaggio:PersistentBase, IDisposable
    {
        SvcDosaggioClient svc;
        List<StatoBilancia> statoBilance = new List<StatoBilancia>();
        public StatoDosaggio(Session session):base(session)
        {
            svc = new SvcDosaggioClient();
            for (int i = 0; i < 5 ; i++)
            {
                statoBilance.Add(new StatoBilancia(this,svc,i));
            }
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch (propertyName)
            {
                case "IdOdl":
                    if (idOdl != 0)
                        Odl = Session.GetObjectByKey<OdlDosaggio>(idOdl);
                    else
                        Odl = null;
                    break;
                case "CurrMisc":
                    foreach (var bil in statoBilance)
                    {
                        bil.CurrMisc = (int)newValue;
                    }
                    break;
                default:
                    break;
            }
        }

        [Key,Browsable(false)]
        [ModelDefault("AllowEdit", "False")]
        public int Key
        {
            get;
            set;
        }

        public IList<StatoBilancia> StatoBilance
        {
            get {return statoBilance ;}
        }

        int currMisc;
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "False")]
        public int CurrMisc
        {
            get { return currMisc; }
            set { SetPropertyValue<int>("CurrMisc", ref currMisc, value); }
        }

        int idOdl;
        [ImmediatePostData, Browsable(false)]
        [ModelDefault("AllowEdit", "False")]
        public int IdOdl
        {
            get { return idOdl; }
            set { SetPropertyValue<int>("IdOdl", ref idOdl, value); }
        }

        int nrMisc;
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "False")]
        public int NrMisc
        {
            get { return nrMisc; }
            set { SetPropertyValue<int>("NrMisc", ref nrMisc, value); }
        }

        bool running;
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "False")]
        public bool Running
        {
            get { return running; }
            set { SetPropertyValue<bool>("Running", ref running, value); }
        }

        string errorString;
        [ModelDefault("AllowEdit", "False")]
        public string ErrorString
        {
            get { return errorString; }
            set { SetPropertyValue<string>("ErrorString", ref errorString, value); }
        }

        public string Note
        {
            get
            {
                if (Odl != null)
                    return Odl.Note;
                return null;
            }
        }

        public float QuantitàTotale
        {
            get
            {
                if (Odl != null)
                    return Odl.Quantità;
                return 0F;
            }
        }

        public float QuantitàperLotto
        {
            get
            {
                if (Odl != null)
                    return Odl.QuantitàPerMiscelata;
                return 0F;
            }
        }

        public string SilosDestinazione
        {
            get
            {
                if (Odl != null && Odl.Destinazione != null)
                    return Odl.Destinazione.Codice;
                return null;
            }
        }

        [ModelDefault("AllowEdit", "False")]
        public string Articolo
        {
            get {
                if (Prodotto != null && Prodotto.Articolo != null)
                    return Prodotto.Articolo.Codice;
                return null;
            }
        }

        [ModelDefault("AllowEdit", "False")]
        public string Descrizione
        {
            get
            {
                if (Prodotto != null && Prodotto.Articolo != null)
                    return Prodotto.Articolo.Descrizione;
                return null;
            }
        }


        [ModelDefault("AllowEdit", "False")]
        public string Formula
        {
            get
            {
                if (Odl != null && Odl.Formula != null)
                    return Odl.Formula.Codice;
                return null;
            }
        }

        public int NrOrd
        {
            get
            {
                if (this.Odl != null && Odl.OrdineProduzione != null)
                    return this.Odl.OrdineProduzione.NumeroOrdine;
                return 0;
            }
        }

        internal OdlDosaggio Odl;

        internal Lotto Prodotto
        {
            get
            {
                if (Odl != null && Odl.Prodotti != null && Odl.Prodotti.Count>0)
                    return Odl.Prodotti[0];
                return null;
            }
        }

        public void Arresta()
        {
            try
            {
                svc.Arresta();
            }
            catch (Exception ex)
            {
                ErrorString = ex.Message;
            }

        }

        public void Arresta(int nrMisc)
        {
            try
            {
                svc.StopMisc(nrMisc);
            }
            catch (Exception ex)
            {
                ErrorString = ex.Message;
            }
        }

        public void RefreshData()
        {
            try
            {
                if (svc.State == System.ServiceModel.CommunicationState.Faulted == false)
                {
                    this.Running = svc.Running();
                    this.NrMisc = svc.NrMisc();
                    this.IdOdl = svc.IdOdL();
                    this.CurrMisc = svc.CurrMisc();
                    foreach (var bil in this.StatoBilance)
                    {
                        bil.RefreshData();
                    }
                    OnLoaded();
                    ErrorString = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ErrorString = ex.Message + '\n' + ex.InnerException.Message;
            }

        }

        public void Dispose()
        {
            svc.Close();
        }
    }
}
