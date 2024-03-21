using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Xpo;


namespace XFactoryNET.Module.BusinessObjects
{
 
    [DefaultProperty("Codice")]
    //[RuleCriteria(DefaultContexts.Delete, "NOT Stato IN ('InEsecuzione')")]
    [Appearance(null, AppearanceItemType = "ViewItem", Criteria = "NOT Stato IS NULL AND NOT Stato IN ('InPreparazione','Pronto')", TargetItems = "*", Enabled = false)]
    public abstract class Odl : BaseXPObject
    {
        public Odl(Session session) : base(session) { }
        public Odl() : base(Session.DefaultSession) { }

        protected virtual string idLavorazione
        {
            get { return null; }
        }

        public event EventHandler Avviato;
        public event EventHandler<CancelEventArgs> InAvvio;
        public event EventHandler<CancelEventArgs> Predisposto;

        public virtual bool OnPredisposto()
        {
            CancelEventArgs args = new CancelEventArgs(false);
            if (Predisposto != null)
            {
                Predisposto(this, args);
            }
            return args.Cancel;
        }

        public virtual bool OnInAvvio()
        {
            CancelEventArgs args = new CancelEventArgs(false);
            if (InAvvio != null)
            {
                InAvvio(this, args);
            }
            return args.Cancel;
        }

        public virtual void OnAvviato()
        {
            if (Avviato != null)
            {
                Avviato(this, new EventArgs());
            }
        }

        public override void AfterConstruction()
        {
            this.Stato = StatoOdL.InPreparazione;
            this.Data = System.DateTime.Now;
            this.NumeroMiscelate = 1;
            SecuritySystemUser user = (SecuritySystemUser)SecuritySystem.CurrentUser;
            this.Operatore = this.Session.GetObjectByKey<SecuritySystemUser>(user.Oid);
            Lavorazione = Session.GetObjectByKey<Lavorazione>(idLavorazione);
            if (Lavorazione == null)
                Lavorazione = new Lavorazione(Session) { Codice = idLavorazione,Descrizione = idLavorazione };
            //if (this.Lavorazione.NOrd == short.MaxValue)
            //    this.Lavorazione.NOrd = 0;
            //this.Lavorazione.NOrd++;
            //this.NrArch = this.Lavorazione.NArch;
            //this.NrOrd = this.Lavorazione.NOrd;
            base.AfterConstruction();
        }

        internal bool LoadingFormula;

        [NonPersistent,Browsable(false)]
        public abstract TipoLavorazione TipoLavorazione { get; }

        internal static Lavorazione GetOrCreateLavorazione(IObjectSpace ObjectSpace,string IDLavorazione)
        {
            Lavorazione lav = ObjectSpace.GetObjectByKey<Lavorazione>(IDLavorazione);
            if (lav == null)
            {
                lav = ObjectSpace.CreateObject<Lavorazione>();
                lav.Codice = IDLavorazione;
                lav.Descrizione = IDLavorazione;
                lav.Save();
            }
            return lav;
        }

        [ModelDefault("AllowEdit", "False")]
        public string Codice
        {
            get { return string.Format("OdL{0:d4}", this.Oid); }
        }

        //private short nrArch;
        //[ModelDefault("AllowEdit", "False")]
        //public short NrArch
        //{
        //    get { return nrArch; }
        //    set { SetPropertyValue<short>("NrArch", ref nrArch, value); }
        //}

        //private short nrOrd;
        //[ModelDefault("AllowEdit", "False")]
        //public short NrOrd
        //{
        //    get { return nrOrd; }
        //    set { SetPropertyValue<short>("NrOrd", ref nrOrd, value); }
        //}

        StatoOdL fStato;
        [ModelDefault("AllowEdit","False")]
        [Indexed(Unique=false)]
        public StatoOdL Stato
        {
            get { return fStato; }
            set { SetPropertyValue<StatoOdL>("Stato", ref fStato, value); }
        }


        DateTime fData;
        [ModelDefault("AllowEdit", "False")]
        public DateTime Data
        {
            get { return fData; }
            set { SetPropertyValue<DateTime>("Data", ref fData, value); }
        }

        int fNumeroMiscelate;

        int fNumeroMiscelateEseguite;

        [ImmediatePostData]
        [ModelDefault("DisplayFormat", "d")]
        [RuleValueComparison("", "AvviaOdl", ValueComparisonType.GreaterThan, 0)]
        public int NumeroMiscelate
        {
            get { return fNumeroMiscelate; }
            set { SetPropertyValue<int>("NumeroMiscelate", ref fNumeroMiscelate, value); }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "d")]
        public int NumeroMiscelateEseguite
        {
            get { return fNumeroMiscelateEseguite; }
            set { SetPropertyValue<int>("NumeroMiscelateEseguite", ref fNumeroMiscelateEseguite, value); }
        }


        float fQuantitàPerMiscelata;
        [ModelDefault("DisplayFormat", "d")]
        [RuleValueComparison("", "AvviaOdl", ValueComparisonType.GreaterThan, 0, CustomMessageTemplate = "QuantitàPerMiscelata non valida")]
        [ImmediatePostData]
        public float QuantitàPerMiscelata
        {
            get { return fQuantitàPerMiscelata; }
            set { SetPropertyValue<float>("QuantitàPerMiscelata", ref fQuantitàPerMiscelata, value); }
        }

        [NonPersistent]
        [ModelDefault("DisplayFormat", "d")]
        [ImmediatePostData]
        public virtual float Quantità
        {
            get { return this.QuantitàPerMiscelata * this.NumeroMiscelate; }
            set
            {
                if (this.Articolo != null && this.Articolo.QuantitàPerMiscelata != 0)
                    this.NumeroMiscelate = (int)Math.Ceiling(value / this.Articolo.QuantitàPerMiscelata);
                this.QuantitàPerMiscelata = value / this.NumeroMiscelate;
                //if (this.NumeroLotti != 0)
                //    this.QuantitàPerLotto = value / this.NumeroLotti; 
            }

        }

        [NonPersistent]
        [Appearance(null,Criteria="Confezione IS NULL OR Confezione.TipoConfezione <> 'Sacchi'",Enabled=false)]
        [ImmediatePostData]
        public int NumeroSacchi
        {
            get
            {
                if (Confezione != null && Confezione.TipoConfezione == TipoConfezione.Sacchi)
                {
                    return (int)Math.Floor(QuantitàPerMiscelata / Confezione.PesoSacco);
                }
                return 0;
            }
            set {
                if (Confezione != null && Confezione.TipoConfezione == TipoConfezione.Sacchi)
                {
                    QuantitàPerMiscelata = value * Confezione.PesoSacco;
                }
            }
        }

        private float quantitàEffettiva;
        [ModelDefault("DisplayFormat", "d")]
        public float QuantitàEffettiva
        {
            get { return quantitàEffettiva; }
            set { SetPropertyValue<float>("QuantitàEffettiva",ref quantitàEffettiva,value); }
        }


        Confezione fIDConfezione;
        [ImmediatePostData]
        public Confezione Confezione
        {
            get { return fIDConfezione; }
            set { SetPropertyValue<Confezione>("Confezione", ref fIDConfezione, value); }
        }


        string fNote;
        [ModelDefault("Rows", "2")]
        public string Note
        {
            get { return fNote; }
            set { SetPropertyValue<string>("Note", ref fNote, value); }
        }

        Articolo fIDArticolo;
        [ImmediatePostData]
        //[DataSourceCriteria("Lavorazione='@This.Lavorazione'")]
        [RuleRequiredField("","AvviaOdl")]
        public Articolo Articolo
        {
            get { return fIDArticolo; }
            set { SetPropertyValue<Articolo>("Articolo", ref fIDArticolo, value); }
        }


        Lavorazione fLavorazione;
        [ModelDefault("AllowEdit", "False")]
        [Browsable(false)]
        [RuleRequiredField("", "AvviaOdl")]
        public Lavorazione Lavorazione
        {
            get { return fLavorazione; }
            set 
            {
                SetPropertyValue<Lavorazione>("Lavorazione", ref fLavorazione, value);
                if (Session.IsObjectsLoading == false)
                {
                    if (fLavorazione != null)
                    {
                        if (this.ApparatoLavorazione == null || this.ApparatoLavorazione.Lavorazione != fLavorazione)
                            this.ApparatoLavorazione = fLavorazione.Apparato.FirstOrDefault<Apparato>();
                    }
                    else
                        this.ApparatoLavorazione = null;
                }
            }
        }

        Apparato fApparatoLavorazione;
        [ImmediatePostData]
        [DataSourceCriteria("Lavorazione = '@This.Lavorazione'")]
        [RuleRequiredField("", "AvviaOdl")]
        public Apparato ApparatoLavorazione
        {
            get { return fApparatoLavorazione; }
            set { SetPropertyValue<Apparato>("ApparatoLavorazione", ref fApparatoLavorazione, value); }
        }

        public string ElencoAllegati
        {
            get
            {
                return Allegato.GetElencoAllegati(this.AllegatiOdl.Select<AllegatoOdl,Allegato>(a => a.Allegato));
            }
        }


        public XPCollection<Odl> OdlProdotti
        {
            get
            {
                XPCollection<Odl> coll = new XPCollection<Odl>();
                List<Odl> odls = new List<Odl>();
                var q = this.Prodotti.SelectMany<Lotto, Lotto>(l => l.Prodotti).Select(l => l.Odl);
                odls.AddRange(q);
                foreach (var item in q)
                {
                    odls.AddRange(item.OdlProdotti);
                }
                coll.HintCollection = odls.ToList();
                return coll;
            }
        }

        public XPCollection<Odl> OdlUtilizzati
        {
            get
            {
                XPCollection<Odl> coll = new XPCollection<Odl>();
                List<Odl> odls = new List<Odl>();
                var q = this.Ingredienti.SelectMany<Lotto, Lotto>(l => l.Utilizzi).Select(l => l.Odl);
                odls.AddRange(q);
                foreach (var item in q)
                {
                    odls.AddRange(item.OdlUtilizzati);
                }
                coll.HintCollection = odls.ToList();
                return coll;
            }

        }


        bool fExport;
        [Browsable(false)]
        public bool Export
        {
            get { return fExport; }
            set { SetPropertyValue<bool>("Export", ref fExport, value); }
        }

        private SecuritySystemUser operatore;
        [ModelDefault("AllowEdit","False")]
        public SecuritySystemUser Operatore
        {
            get { return operatore; }
            set { SetPropertyValue<SecuritySystemUser>("Operatore", ref operatore, value); }
        }

        private bool dummy;
        public bool Dummy
        {
            get { return dummy; }
            set { 
                SetPropertyValue<bool>("Dummy", ref dummy, value);
            }
        }

        private bool hidden;
        public bool Hidden
        {
            get { return hidden; }
            set {
                //if (!IsLoading && !IsDeleted)
                //{
                //    if (value && !hidden)       //Se è diventata hidden
                //    {
                //        XPCollection<Odl> list = new XPCollection<Odl>(this.Session, CriteriaOperator.Parse("NrArch=? AND NrOrd>?", NrArch, NrOrd));
                //        foreach (var item in list)
                //        {
                //            item.NrOrd--;
                //        }
                //        --this.Lavorazione.NOrd;    //Decremento il progressivo per non lasciare buchi nella numerazione.
                //        this.NrOrd = (short)-this.nrOrd;   // cambio di segno
                //    }
                //    if (hidden && !value)      // Se è tornata visibile
                //    {
                //        XPCollection<Odl> list = new XPCollection<Odl>(this.Session, CriteriaOperator.Parse("NrArch=? AND NrOrd>=?", NrArch, NrOrd));
                //        foreach (var item in list)
                //        {
                //            item.NrOrd++;
                //        }
                //        this.NrOrd = (short)-this.NrOrd;
                //        ++this.Lavorazione.NOrd;    //Incremento progressivo
                //    }
                //}
                SetPropertyValue<bool>("Hidden", ref hidden, value);
            }
        }

        private bool skipCheck;
        public bool SkipCheck
        {
            get { return skipCheck; }
            set { SetPropertyValue<bool>("SkipCheck", ref skipCheck, value); }
        }

        public void EseguiTeorico()
        {
            Odl odl = this;

            //Esegue l'ordine di lavoro in forma teorica movimentando le quantità teoriche.
            foreach (var prod in odl.Prodotti)
            {
                prod.Quantità = prod.QtàTeo;
                //Il movimento è già stato registrato all'avvio dell'OdL
                //if (odl.Destinazione != null)
                //    odl.Destinazione.RegistraMovimento(prod);
                //if (odl.MagazzinoDestinazione != null)
                //    odl.MagazzinoDestinazione.RegistraMovimento(prod);
                prod.Stato = StatoLotto.Eseguito;
            }

            foreach (var ingr in odl.Ingredienti)
            {
                ingr.Quantità = ingr.QtàTeo;
                //Il movimento è già stato registrato alla predisposizione dell'OdL
                //if (ingr.Silos != null)
                //    ingr.Silos.RegistraMovimento(ingr);
                //if (ingr.Magazzino != null)
                //    ingr.Magazzino.RegistraMovimento(ingr);
                ingr.Stato = StatoLotto.Eseguito;
            }

            odl.NumeroMiscelateEseguite = odl.NumeroMiscelate;
            odl.QuantitàEffettiva = odl.Quantità;
            odl.Stato = StatoOdL.Eseguito;

        }


        //public void ApplicaSostituzioni()
        //{
        //    IList<Sostituzione> applicate = new List<Sostituzione>();

        //    if (this.Articolo != null)
        //    {
        //        if (this.Articolo.Allegati != null)
        //        {
        //            Allegati.AddRange(this.Articolo.Allegati);
        //        }

        //        foreach (var classe in this.Articolo.Classi)
        //        {
        //            innerApplicaSostituzioni(classe.Sostituzioni, applicate);
        //        }

        //        innerApplicaSostituzioni(this.Articolo.Sostituzioni, applicate);
        //    }

        //    IList<Componente> list = new List<Componente>(this.IngredientiTeorici);
        //    foreach (var item in list)
        //    {
        //        innerApplicaSostituzioni(item.Articolo.Sostituzioni, applicate);
        //    }

        //}

        //private void innerApplicaSostituzioni(IList<Sostituzione> sostituzioni,IList<Sostituzione> applicate)
        //{
        //    foreach (var item in sostituzioni)
        //    {
        //        if (item.Applica && applicate.Contains(item) == false)
        //        {
        //            Sostituisci(item);
        //            applicate.Add(item);
        //        }
        //    }

        //}


        private bool checkFattibileLastResult = false;
        private bool skipSecondCheckFattibile = false;

        [RuleFromBoolProperty(TargetContextIDs = "AvviaOdl", CustomMessageTemplate = "Ordine non eseguibile")]
        [NonPersistent, Browsable(false)]
        public bool CheckFattibile
        {
            get {
                if (!skipSecondCheckFattibile)
                {
                    checkFattibileLastResult = VerificaCompatibilità();     // && Predisponi();
                    skipSecondCheckFattibile = true;
                }
                else
                    skipSecondCheckFattibile = false;
                return checkFattibileLastResult;
            }
        }


        private bool VerificaCompatibilità()
        {

            //Verifica compatibilità degli ingredienti con le classi, con l'articolo e, ricursivamente, con tutte le classi a cui l'articolo appartiene
            //
            if (this.skipCheck)
                return true;
            
            foreach (Componente comp in this.IngredientiTeorici)
            {
                comp.ArticoloIncompatibile = null;

                foreach (var cls in this.Classi)
                {
                    if (recursiveCompatibile(cls, comp) == false)
                        return false;
                }


                if (false == recursiveCompatibile(this.Articolo, comp))
                {
                    return false;
                }
                foreach (var comp2 in this.IngredientiTeorici)
                {
                    if (comp == comp2)
                        continue;
                    if (recursiveCompatibile(comp.Articolo,comp2) == false)
                        return false;
                }
            }

            return true;

        }

        private bool recursiveCompatibile(BaseArticolo art1, Componente comp)
        {
            BaseArticolo art2 = comp.Articolo;
            if (art1.Vincoli.Any<Vincolo>(v => v.Incompatibile && v.Articolo2 == art2))
            {
                comp.ArticoloIncompatibile = art1;
                return false;
            }

            foreach (var cls in art1.Classi)
            {
                if (recursiveCompatibile(cls, comp) == false)
                    return false;
            }

            return true;
        }


        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

        }

        private Formula fFormula;
        [ImmediatePostData]
        [Association("FormulaOdl")]
        public Formula Formula
        {
            get
            {
                return fFormula;
            }
            set
            {
                SetPropertyValue<Formula>("Formula", ref fFormula, value);
            }
        }

        [Browsable(false)]
        public XPCollection<Lotto> Ingredienti
        {
            get {
                var q = from l in Lotti where l.TipoMovimento < 0 select l;
                IList<Lotto> list = q.ToList<Lotto>();
                XPCollection<Lotto> lot = new XPCollection<Lotto>(Session);
                lot.HintCollection = list;
                return lot;
            }
        }

        [Association,Aggregated,Browsable(true)]
        public XPCollection<Lotto> Lotti
        {
            get { return GetCollection<Lotto>("Lotti"); }
        }

        [Browsable(false)]
        public XPCollection<Lotto> Prodotti
        {
            get
            {
                var q = from l in Lotti where l.TipoMovimento >= 0 select l;
                IList<Lotto> list = q.ToList<Lotto>();
                XPCollection<Lotto> lot = new XPCollection<Lotto>(Session);
                lot.HintCollection = list;
                return lot;
            }
        }

        [Association("OdlIngredientiTeorici"), Aggregated]
        public DevExpress.Xpo.XPCollection<Componente> IngredientiTeorici
        {
            get { return GetCollection<Componente>("IngredientiTeorici"); }
        }


        [Association("OdlProdottiTeorici"), Aggregated]
        public DevExpress.Xpo.XPCollection<Componente> ProdottiTeorici
        {
            get { return GetCollection<Componente>("ProdottiTeorici"); }
        }

        [Association,Browsable(true),Aggregated]
        public XPCollection<AllegatoOdl> AllegatiOdl
        {
            get { return GetCollection<AllegatoOdl>("AllegatiOdl"); }
        }

        //private XPCollection<Allegato> allegati;
        //public XPCollection<Allegato> Allegati
        //{
        //    get
        //    {
        //        if (allegati == null)
        //        {
        //            allegati = new XPCollection<Allegato>(Session, false);
        //            foreach (var item in AllegatiOdl)
        //            {
        //                allegati.Add(item.Allegato);
        //            }
        //            allegati.CollectionChanged += new XPCollectionChangedEventHandler(allegati_CollectionChanged);
        //        }
        //        return allegati;
        //    }
        //}

        //void allegati_CollectionChanged(object sender, XPCollectionChangedEventArgs e)
        //{
        //    if (e.CollectionChangedType == XPCollectionChangedType.AfterAdd)
        //    {
        //        Allegato all = (Allegato)e.ChangedObject;
        //        AllegatoOdl a = new AllegatoOdl(Session);
        //        a.Allegato = all;
        //        AllegatiOdl.Add(a);
        //        a.Save();
        //    }
        //    else if (e.CollectionChangedType == XPCollectionChangedType.AfterRemove)
        //    {
        //        Allegato removed = (Allegato)e.ChangedObject;
        //        foreach (var item in AllegatiOdl)
        //        {
        //            if (item.Allegato == removed && item.Odl == this)
        //            {
        //                item.Delete();
        //                item.Save();
        //                break;
        //            }
        //        }
        //    }

        //}


        [Association("FormuleSostituzione_Odl")]
        [ModelDefault("AllowEdit","False")]
        public XPCollection<Formula> FormuleSostituzione
        {
            get { return GetCollection<Formula>("FormuleSostituzione"); }
        }

        Magazzino magazzinoDestinazione;
        public Magazzino MagazzinoDestinazione
        {
            get { return magazzinoDestinazione; }
            set { SetPropertyValue<Magazzino>("MagazzinoDestinazione", ref magazzinoDestinazione, value); }
        }

        Magazzino magazzinoPrelievo;
        public Magazzino MagazzinoPrelievo
        {
            get { return magazzinoPrelievo; }
            set { SetPropertyValue<Magazzino>("MagazzinoPrelievo", ref magazzinoPrelievo, value); }
        }

        Silos fDestinazione;
        [DataSourceProperty("ApparatiDestinazione", DataSourcePropertyIsNullMode.SelectAll)]
        [ImmediatePostData]
        public Silos Destinazione
        {
            get { return fDestinazione; }
            set { SetPropertyValue<Silos>("Destinazione", ref fDestinazione, value); }
        }

        [Browsable(false)]
        [NonPersistent]
        [RuleFromBoolProperty(CustomMessageTemplate = "Silos di destinazione {Destinazione} non valido", SkipNullOrEmptyValues = false,UsedProperties="Destinazione",TargetContextIDs="AvviaOdl")]
        public bool DestinazioneValida
        {
            get
            {
                if (this.TipoLavorazione == BusinessObjects.TipoLavorazione.UscitaMerci)
                    return true;
                if (Destinazione == null && MagazzinoDestinazione == null)
                    return false;
                if (Destinazione != null)
                {
                    if (Destinazione.Articolo == null)
                        return true;
                    if (Destinazione.Articolo != this.Articolo)
                        return false;
                }
                return true;
            }
        }


        Silos fPrelievo;
        [ImmediatePostData]
        [DataSourceProperty("ApparatiPrelievo", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
        public Silos Prelievo
        {
            get { return fPrelievo; }
            set { SetPropertyValue<Silos>("Prelievo", ref fPrelievo, value); }
        }

        [Browsable(false)]
        [NonPersistent]
        [RuleFromBoolProperty(CustomMessageTemplate = "Silos di prelievo {Prelievo} non valido", SkipNullOrEmptyValues = false, UsedProperties = "Prelievo", TargetContextIDs = "AvviaOdl")]
        public bool PrelievoValido
        {
            get
            {
                if (this.TipoLavorazione == BusinessObjects.TipoLavorazione.EntrataMerci)
                    return true;
                if (Prelievo != null)
                {
                    if (Prelievo.Articolo == null)
                        return false;
                    if (this.Articolo != null && Prelievo.Articolo != this.Articolo)
                        return false;
                }
                return true;
            }
        }

        [NonPersistent, Browsable(false)]
        public virtual IList<Silos> ApparatiDestinazione
        {
            get
            {
                return ApparatoLavorazione == null ? new List<Silos>() : ApparatoLavorazione.ApparatiDestinazione.Cast<Silos>().ToList();
            }
        }

        [NonPersistent, Browsable(false)]
        public virtual IList<Silos> ApparatiPrelievo
        {
            get
            {
                return ApparatoLavorazione == null ? new List<Silos>() : ApparatoLavorazione.ApparatiPrelievo.Cast<Silos>().ToList();
            }
        }

        private OrdineProduzione ordineProduzione;
        [Association]
        //[EditorAlias(EditorAliases.DetailPropertyEditor), ExpandObjectMembers(ExpandObjectMembers.Never)]
        public OrdineProduzione OrdineProduzione
        {
            get { return ordineProduzione; }
            set { SetPropertyValue<OrdineProduzione>("OrdineProduzione", ref ordineProduzione, value); }
        }

        private string codiceEsterno;
        public string CodiceEsterno
        {
            get { return codiceEsterno; }
            set { SetPropertyValue<string>("CodiceEsterno", ref codiceEsterno, value); }
        }

        [Browsable(false)]
        public IList<Classe> Classi
        {
            get
            {
                return Articolo.Classi.Union<Classe>(IngredientiTeorici.SelectMany(c => c.Articolo.Classi).Where(c=>c.Ereditaria)).Distinct().ToList();
            }
        }

    }


    public class AllegatoOdl : BaseXPObject
    {
        public AllegatoOdl(Session session)
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

        private Odl odl;
        [Association]
        public Odl Odl
        {
            get { return odl; }
            set { SetPropertyValue<Odl>("Odl", ref odl, value); }
        }

        //private int count;
        //public int Count
        //{
        //    get { return count; }
        //    set { SetPropertyValue<int>("Count", ref count, value); }
        //}

    }

}
