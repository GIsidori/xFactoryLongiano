using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using System.Linq;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.Threading;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;


namespace XFactoryNET.Module.BusinessObjects
{
    [DefaultProperty("CodiceLotto")]
    public class Lotto : BaseXPObject
    {

        public Lotto(Session session) : base(session) 
        {
        }

        public Lotto() : base(Session.DefaultSession) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        protected override void OnDeleting()
        {
            base.OnDeleting();
        }

        private TipoMovimento tipoMovimento;
        [ModelDefault("AllowEdit", "False")]
        public TipoMovimento TipoMovimento
        {
            get { return tipoMovimento; }
            set { SetPropertyValue<TipoMovimento>("TipoMovimento", ref tipoMovimento, value); }
        }

        float fQuantit‡;
        public float Quantit‡
        {
            get { return fQuantit‡; }
            set {
                float qPrec = fQuantit‡;
                SetPropertyValue<float>("Quantit‡", ref fQuantit‡, value);
                if (qPrec != fQuantit‡)
                {
                    float deltaQ = fQuantit‡ - qPrec;
                    if (Silos != null)
                        Silos.RegistraMovimento(this,deltaQ);
                    if (Magazzino != null)
                        Magazzino.RegistraMovimento(this);
                }
            }
        }

        float fQuantit‡Prelevata;
        [ModelDefault("AllowEdit","False")]
        public float Quantit‡Prelevata
        {
            get { return fQuantit‡Prelevata; }
            set { SetPropertyValue<float>("Quantit‡Prelevata", ref fQuantit‡Prelevata, value); }
        }

        [NonPersistent]
        public float Quantit‡Residua
        {
            get { return Quantit‡ - Quantit‡Prelevata; }
            set
            {
                Quantit‡Prelevata = Quantit‡ - value;
            }
        }

        [Appearance(null, Criteria = "Confezione IS NULL OR Confezione.TipoConfezione <> 'Sacchi'", Enabled = false)]
        public int NumeroSacchiResidui
        {
            get
            {
                return GetNumeroSacchi(this.Quantit‡Residua);
            }
        }

        private int GetNumeroSacchi(float quantit‡)
        {
            if (Confezione != null)
            {
                float pesoSacco = 0;
                if (Confezione.TipoConfezione == TipoConfezione.Sacchi && Confezione.PesoSacco != 0)
                    pesoSacco = Confezione.PesoSacco;
                if (pesoSacco != 0)
                    return (int)Math.Floor(quantit‡ / pesoSacco);
            }
            return 0;

        }

        float fQt‡Teo;

        [ModelDefault("DisplayFormat", "n4")]
        [ModelDefault("EditFormat", "n4")]
        public float Qt‡Teo
        {
            get { return fQt‡Teo; }
            set { SetPropertyValue<float>("Qt‡Teo", ref fQt‡Teo, value); }
        }

        Modalit‡ fModalit‡;
        public Modalit‡ Modalit‡
        {
            get { return fModalit‡; }
            set { SetPropertyValue<Modalit‡>("Modalit‡", ref fModalit‡, value); }
        }

        float fTolleranza;
        public float Tolleranza
        {
            get { return fTolleranza; }
            set { SetPropertyValue<float>("Tolleranza", ref fTolleranza, value); }
        }

        Articolo fArticolo;
        [Association]
        [ModelDefault("AllowEdit", "False")]
        public Articolo Articolo
        {
            get { return fArticolo; }
            set { SetPropertyValue<Articolo>("Articolo", ref fArticolo, value); }
        }

        Confezione fConfezione;
        [Size(12)]
        public Confezione Confezione
        {
            get { return fConfezione; }
            set { SetPropertyValue<Confezione>("Confezione", ref fConfezione, value); }
        }


        StatoLotto fStato;
        [Browsable(false)]
        [ModelDefault("AllowEdit", "False")]
        public StatoLotto Stato
        {
            get { return fStato; }
            set { SetPropertyValue<StatoLotto>("Stato", ref fStato, value); }
        }
        string fNote;
        public string Note
        {
            get { return fNote; }
            set { SetPropertyValue<string>("Note", ref fNote, value); }
        }

        [Association,Aggregated]
        public XPCollection<AllegatoLotto> AllegatiLotto
        {
            get { return GetCollection<AllegatoLotto>("AllegatiLotto"); }
        }

        private Odl odl;
        [Association]
        //[ModelDefault("AllowEdit", "False")]
        public Odl Odl
        {
            get
            {
                return odl;
                //if (TipoMovimento == BusinessObjects.TipoMovimento.Produzione || TipoMovimento == BusinessObjects.TipoMovimento.EntrataMerci)
                //    return OdlProdotti;
                //return OdlIngredienti;
            }
            set { SetPropertyValue<Odl>("Odl", ref odl, value); }
        }

        [Association("UtilizzoLotti"),Browsable(false)]
        public XPCollection<Lotto> Prodotti
        {
            get
            {
                return GetCollection<Lotto>("Prodotti");
                //return Odl.Prodotti.Contains(this) ? null : Odl.Prodotti;
            }
        }

        [Association("UtilizzoLotti"),Browsable(false)]
        public XPCollection<Lotto> Utilizzi
        {
            get
            {
                return GetCollection<Lotto>("Utilizzi");
                //return Odl.Ingredienti.Contains(this) ? null : Odl.Ingredienti;
            }
        }

        //public XPCollection<Lotto> LottiUtilizzatiDirettamente
        //{
        //    get
        //    {
        //        if (odl != null)
        //            return this.Odl.Ingredienti.Contains(this) ? null : this.Odl.Ingredienti;
        //        return null;
        //    }
        //}

        //private XPCollection<Lotto> LottiProdottiDirettamente
        //{
        //    get
        //    {
        //        if (odl != null)
        //            return Odl.Prodotti.Contains(this) ? null : Odl.Prodotti;
        //        return null;
        //    }
        //}

        public IList<Odl> OdlProdotti
        {
            get
            {
                List<Odl> list = new List<Odl>();
                foreach (var odl in Prodotti.Select<Lotto, Odl>(l => l.Odl))
                {
                    list.Add(odl);
                    list.AddRange(odl.Prodotti.SelectMany<Lotto, Odl>(l => l.OdlProdotti));
                }

                return list;
            }
        }

        public IList<Odl> OdlUtilizzati
        {
            get
            {
                List<Odl> list = new List<Odl>();
                if (this.odl != null)
                {
                    foreach (var lot in this.Odl.Ingredienti.SelectMany(l => l.Utilizzi))
                    {
                        if (lot.Odl != null)
                        {
                            list.Add(lot.Odl);
                            list.AddRange(lot.OdlUtilizzati);
                        }
                    }
                }
                return list;
            }
        }
        
        public static void UtilizzaLotti(XPCollection<Lotto> lottiPrelievo, Lotto lotto)
        {
            //Utilizza i lotti a magazzino con metodo FIFO
            float qt‡ = lotto.Quantit‡;
            int index = 0;
            var lotti = lottiPrelievo.Where(l => l.TipoMovimento >= 0 && l.DataFine == null && l.Quantit‡Residua > 0).OrderBy(l => l.DataInizio);
            while (qt‡ > 0 && index < lotti.Count())
            {
                Lotto lPrel = lotti.ElementAt(index);
                lotto.Utilizzi.Add(lPrel);
                if (lPrel.Quantit‡Residua > qt‡)
                {
                    lPrel.Quantit‡Residua -= qt‡;
                    break;
                }
                else
                {
                    qt‡ -= lPrel.Quantit‡Residua;
                    lPrel.Quantit‡Residua = 0;
                    lPrel.DataFine = System.DateTime.Now;
                    index++;
                }
            }
        }

        private string codiceEsterno;
        public string CodiceEsterno
        {
            get { return codiceEsterno; }
            set { SetPropertyValue<string>("CodiceEsterno", ref codiceEsterno, value); }
        }


        //Apparato apparatoStoccaggio;
        //[Association,Browsable(false)]
        //public Apparato ApparatoStoccaggio
        //{
        //    get { return apparatoStoccaggio; }
        //    set { SetPropertyValue<Apparato>("ApparatoStoccaggio", ref apparatoStoccaggio, value); }
        //}

        Silos silos;
        [DataSourceProperty("SilosValidi")]
        [Association("LottiInSilos")]
        [ModelDefault("AllowEdit","False")]
        public Silos Silos
        {
            get { return silos; }
            set 
            {
                Silos pApp = silos;

                SetPropertyValue<Silos>("Silos", ref silos, value);

                if (pApp == silos)
                    return;

                if (IsLoading)
                    return;

                if (silos != null)
                {
                    //apparato.Lotto = this;
                    Magazzino = null;
                }

                //OnChanged("Apparato");

            }
            //set {
            //    if (apparato == value)
            //        return;

            //    //Memorizza il riferimento all'apparato precedente
            //    Apparato pApp = apparato;
            //    apparato = value;

            //    if (IsLoading)
            //        return;

            //    if (pApp != null && pApp.Lotto == this)
            //        pApp.Lotto = null;

            //    if (apparato != null)
            //    {
            //        apparato.Lotto = this;
            //        Magazzino = null;
            //    }
            //    OnChanged("Apparato");
            //}
        }

        [NonPersistent, Browsable(false)]
        public virtual XPCollection<Silos> SilosValidi
        {
            get
            {
                return new XPCollection<Silos>(Session,CriteriaOperator.Parse("CaricoAbilitato"));
            }
        }

        Magazzino magazzino;
        [Association]
        public Magazzino Magazzino
        {
            get { return magazzino; }
            set
            {
                SetPropertyValue<Magazzino>("Magazzino", ref magazzino, value);
                if (!IsLoading && magazzino != null)
                    Silos = null;
            }
        }

        private DateTime dataInizio;
        [ModelDefault("AllowEdit", "False")]
        public DateTime DataInizio
        {
            get { return dataInizio; }
            set { SetPropertyValue<DateTime>("DataInizio", ref dataInizio, value); }
        }

        private DateTime? dataFine;
        [ModelDefault("AllowEdit", "False")]
        public DateTime? DataFine
        {
            get { return dataFine; }
            set { SetPropertyValue<DateTime?>("DataFine", ref dataFine, value); }
        }

        [NonPersistent]
        public string CodiceLotto
        {
            get
            {
                return string.Format("Lot{0}", Oid);
            }
        }
        
        /// <summary>
        /// Apparato di lavorazione del componente
        /// </summary>
        public Apparato ApparatoLavorazione
        {
            get { return fApparatoLavorazione; }
            set { SetPropertyValue<Apparato>("ApparatoLavorazione", ref fApparatoLavorazione, value); }
        }

        Apparato fApparatoLavorazione;

        public int NrComp
        {
            get { return fNrComp; }
            set { SetPropertyValue<int>("NrComp", ref fNrComp, value); }
        }

        public int NrMisc
        {
            get { return fNrLotto; }
            set { SetPropertyValue<int>("NrMisc", ref fNrLotto, value); }
        }

        //private Odl fOdlIngredienti;
        //[Association("OdlIngredienti"), Browsable(false)]
        //public Odl OdlIngredienti
        //{
        //    get { return fOdlIngredienti; }
        //    set { SetPropertyValue<Odl>("OdlIngredienti", ref fOdlIngredienti, value); }
        //}

        //private Odl fOdlProdotti;
        //[Association("OdlProdotti"), Browsable(false)]
        //public Odl OdlProdotti
        //{
        //    get { return fOdlProdotti; }
        //    set { SetPropertyValue<Odl>("OdlProdotti", ref fOdlProdotti, value); }
        //}

        int fNrComp;

        int fNrLotto;


    }

    public class AllegatoLotto : BaseXPObject
    {
        public AllegatoLotto(Session session)
            : base(session)
        {

        }

        public override void AfterConstruction()
        {
            //this.Count = 1;
            base.AfterConstruction();
        }

        private Allegato allegato;
        [Association]
        public Allegato Allegato
        {
            get { return allegato; }
            set { SetPropertyValue<Allegato>("Allegato", ref allegato, value); }
        }

        private Lotto lotto;
        [Association]
        public Lotto Lotto
        {
            get { return lotto; }
            set { SetPropertyValue<Lotto>("Lotto", ref lotto, value); }
        }

        //private int count;
        //public int Count
        //{
        //    get { return count; }
        //    set { SetPropertyValue<int>("Count", ref count, value); }
        //}

    }


}
