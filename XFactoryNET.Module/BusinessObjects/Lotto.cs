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
        [Indexed(Unique=false)]
        public TipoMovimento TipoMovimento
        {
            get { return tipoMovimento; }
            set { SetPropertyValue<TipoMovimento>("TipoMovimento", ref tipoMovimento, value); }
        }

        decimal fQuantit‡;
        [ModelDefault("DisplayFormat", "n0")][ModelDefault("EditMask","n0")]
        public decimal Quantit‡
        {
            get { return fQuantit‡; }
            set {
                decimal qPrec = fQuantit‡;
                SetPropertyValue<decimal>("Quantit‡", ref fQuantit‡, value);
                if (!this.IsLoading && !this.IsDeleted && qPrec != fQuantit‡)
                {
                    decimal deltaQ = fQuantit‡ - qPrec;
                    if (Silos != null)
                        Silos.RegistraMovimento(this,deltaQ);
                    if (Magazzino != null)
                        Magazzino.RegistraMovimento(this);
                }
            }
        }

        decimal fQuantit‡Prelevata;
        [ModelDefault("AllowEdit","False")]
        [ModelDefault("DisplayFormat", "n0")][ModelDefault("EditMask","n0")]
        public decimal Quantit‡Prelevata
        {
            get { return fQuantit‡Prelevata; }
            set { SetPropertyValue<decimal>("Quantit‡Prelevata", ref fQuantit‡Prelevata, value); }
        }

        [ModelDefault("DisplayFormat", "n0")][ModelDefault("EditMask","n0")]
        [PersistentAlias("Quantit‡ - Quantit‡Prelevata")]
        public decimal Quantit‡Residua
        {
            get { return (decimal)EvaluateAlias("Quantit‡Residua");}
        }


        [Appearance(null, Criteria = "Confezione IS NULL OR Confezione.TipoConfezione <> 'Sacchi'", Enabled = false)]
        public int NumeroSacchiResidui
        {
            get
            {
                return GetNumeroSacchi(this.Quantit‡Residua);
            }
        }

        private int GetNumeroSacchi(decimal quantit‡)
        {
            if (Confezione != null)
            {
                decimal pesoSacco = 0;
                if (Confezione.TipoConfezione == TipoConfezione.Sacchi && Confezione.PesoSacco != 0)
                    pesoSacco = Confezione.PesoSacco;
                if (pesoSacco != 0)
                    return (int)Math.Floor(quantit‡ / pesoSacco);
            }
            return 0;

        }

        decimal fQt‡Teo;

        [ModelDefault("DisplayFormat", "n4")]
        [ModelDefault("EditFormat", "n4")]
        public decimal Qt‡Teo
        {
            get { return fQt‡Teo; }
            set { SetPropertyValue<decimal>("Qt‡Teo", ref fQt‡Teo, value); }
        }

        Modalit‡ fModalit‡;
        public Modalit‡ Modalit‡
        {
            get { return fModalit‡; }
            set { SetPropertyValue<Modalit‡>("Modalit‡", ref fModalit‡, value); }
        }

        decimal fTolleranza;
        [ModelDefault("DisplayFormat", "n3")][ModelDefault("EditMask","n3")]
        public decimal Tolleranza
        {
            get { return fTolleranza; }
            set { SetPropertyValue<decimal>("Tolleranza", ref fTolleranza, value); }
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

        [Association("UtilizzoLotti"),Browsable(true)]
        public XPCollection<Lotto> Prodotti
        {
            get
            {
                return GetCollection<Lotto>("Prodotti");
                //return Odl.Prodotti.Contains(this) ? null : Odl.Prodotti;
            }
        }

        [Association("UtilizzoLotti"),Browsable(true)]
        public XPCollection<Lotto> Utilizzi
        {
            get
            {
                return GetCollection<Lotto>("Utilizzi");
                //return Odl.Ingredienti.Contains(this) ? null : Odl.Ingredienti;
            }
        }
        
        [Browsable(true)]
        public IList<Odl> OdlProdotti
        {
            get
            {
                List<Odl> list = new List<Odl>();
                foreach (var odl in Prodotti.Select<Lotto, Odl>(l => l.Odl))
                {
                    if (list.Contains(odl) == false)
                    {
                        list.Add(odl);
                    }
                }                
                return list;
            }
        }

        
        [Browsable(true)]
        public IList<Odl> OdlUtilizzati
        {
            get
            {
                List<Odl> list = new List<Odl>();
                if (this.odl != null)
                {
                    foreach (var lot in this.Odl.Ingredienti.SelectMany(l => l.Utilizzi))
                    {
                        if (lot.Odl != null && list.Contains(lot.odl) == false)
                        {
                            list.Add(lot.Odl);
                        }
                    }
                    if (this.TipoMovimento == BusinessObjects.TipoMovimento.EntrataMerci || this.TipoMovimento == BusinessObjects.TipoMovimento.Produzione)
                        list.Add(this.odl);
                }
                return list;
            }
        }
        
        public static void UtilizzaLotti(IList<Lotto> lottiPrelievo, Lotto lotto)
        {
            //Utilizza i lotti a magazzino con metodo FIFO
            decimal qt‡ = lotto.Quantit‡;
            int index = 0;
            
            var lotti = lottiPrelievo.Where(l => l.TipoMovimento >= 0 && l.DataFine == null && l.Quantit‡Residua > 0).OrderBy(l => l.DataInizio).ToList();
            int count = lotti.Count();
            while (qt‡ >= 0 && index < count)
            {
                Lotto lPrel = lotti.ElementAt(index);
                lotto.Utilizzi.Add(lPrel);
                if (lPrel.Quantit‡Residua > qt‡)
                {
                    lPrel.Quantit‡Prelevata += qt‡;
                    break;
                }
                else
                {
                    qt‡ -= lPrel.Quantit‡Residua;
                    lPrel.Quantit‡Prelevata = lPrel.Quantit‡;
                    lPrel.DataFine = System.DateTime.Now;
                    index++;
                }
                //Svuota il magazzino se rimane meno di un sacco.
                if (lPrel.Confezione != null && lPrel.NumeroSacchiResidui < 1)
                {
                    lPrel.Quantit‡Prelevata = lPrel.Quantit‡;
                    lPrel.DataFine = System.DateTime.Now;
                }
            }
        }

        private string codiceEsterno;
        public string CodiceEsterno
        {
            get { return codiceEsterno; }
            set { SetPropertyValue<string>("CodiceEsterno", ref codiceEsterno, value); }
        }



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
                    Magazzino = null;
                }

            }
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
        [Indexed(Unique=false)]
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
