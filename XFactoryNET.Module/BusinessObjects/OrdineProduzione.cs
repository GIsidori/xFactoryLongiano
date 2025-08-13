using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;

namespace XFactoryNET.Module.BusinessObjects
{

    [DefaultProperty("NumeroOrdine")]
    [NavigationItem("Ordini")]
    public class OrdineProduzione : BaseXPObject
    {
        public OrdineProduzione():base (Session.DefaultSession)
        {

        }

        public OrdineProduzione(Session session):base(session)
        {

        }

        public override void AfterConstruction()
        {
            this.Data = System.DateTime.Now;
            base.AfterConstruction();
        }

        private short numeroOrdine;
        [Indexed]
        [ModelDefault("DisplayFormat","{0:D}")]
        [ModelDefault("AllowEdit", "False")]
        public short NumeroOrdine
        {
            get { return numeroOrdine; }
            set { SetPropertyValue<short>("NumeroOrdine", ref numeroOrdine, value); }
        }


        internal void SetNextNumeroOrdine(bool isHidden)
        {
            this.NumeroOrdine = GetNextNumeroOrdine(this.Session,isHidden);
        }

        internal static short GetNextNumeroOrdine(Session session, bool isHidden)
        {
            short nr = 0;
            var odp = new XPCollection<OrdineProduzione>(session);
            if (isHidden)
            {
                odp.Criteria = new BinaryOperator("NumeroOrdine",0,BinaryOperatorType.Less);
                if (odp.Count > 0)
                {
                    //nr = odp.Min(o => o.NumeroOrdine);
                    OrdineProduzione o = odp.OrderBy<OrdineProduzione, System.DateTime>(a => a.Data).LastOrDefault();

                    nr = o.NumeroOrdine;
                    if (nr > short.MinValue)
                        nr--;
                    else
                        nr = -1;
                }
                else nr = -1;
            }
            else
            {
                odp.Criteria = new BinaryOperator("NumeroOrdine", 0, BinaryOperatorType.Greater);
                if (odp.Count > 0)
                {
                   // nr = odp.Max(o => o.NumeroOrdine);
                    OrdineProduzione o = odp.OrderBy<OrdineProduzione, System.DateTime>(a => a.Data).LastOrDefault();

                    nr = o.NumeroOrdine;
                    if (nr < short.MaxValue)
                        nr++;
                    else
                        nr = 1;
                }
                else nr = 1;
            }
            return nr;
        }

        private StatoOdP stato;
        public StatoOdP Stato
        {
            get { return stato; }
            set { SetPropertyValue<StatoOdP>("Stato", ref stato, value); }
        }

        private bool archiviato;
        public bool Archiviato
        {
            get { return archiviato; }
            set { SetPropertyValue<bool>("Archiviato", ref archiviato, value); }
        }


        private DateTime data;
        [ModelDefault("DisplayFormat", "g")]
        public DateTime Data
        {
            get { return data; }
            set { SetPropertyValue<DateTime>("Data", ref data, value); }
        }

        private OrdineCliente ordineCliente;
        [Association]
        //[EditorAlias(EditorAliases.DetailPropertyEditor), ExpandObjectMembers(ExpandObjectMembers.Never)]
        public OrdineCliente OrdineCliente
        {
            get { return ordineCliente; }
            set { SetPropertyValue<OrdineCliente>("OrdineCliente", ref ordineCliente, value); }
        }

        private OrdineCarico ordineCarico;
        [Association]
        //[EditorAlias(EditorAliases.DetailPropertyEditor), ExpandObjectMembers(ExpandObjectMembers.Never)]
        public OrdineCarico OrdineCarico
        {
            get { return ordineCarico; }
            set { SetPropertyValue<OrdineCarico>("OrdineCarico", ref ordineCarico, value); }
        }


        private Articolo articolo;
        public Articolo Articolo
        {
            get { return articolo; }
            set { SetPropertyValue<Articolo>("Articolo", ref articolo, value); }
        }

        private Formula formula;
        public Formula Formula
        {
            get { return formula; }
            set { SetPropertyValue<Formula>("Formula", ref formula, value); }
        }


        private decimal quantità;
        [ModelDefault("DisplayFormat","n0")]
        [ModelDefault("EditMask","n0")]
        public decimal Quantità
        {
            get { return quantità; }
            set { SetPropertyValue<decimal>("Quantità", ref quantità, value); }
        }

        private decimal quantitàEffettiva;
        [ModelDefault("DisplayFormat", "n0")]
        [ModelDefault("EditMask", "n0")]
        public decimal QuantitàEffettiva
        {
            get { return quantitàEffettiva; }
            set { SetPropertyValue<decimal>("QuantitàEffettiva", ref quantitàEffettiva, value); }
        }


        private string note;
        public string Note
        {
            get { return note; }
            set { SetPropertyValue<string>("Note", ref note, value); }
        }

        private Confezione confezione;
        public Confezione Confezione
        {
            get { return confezione; }
            set { SetPropertyValue<Confezione>("Confezione", ref confezione, value); }
        }

        private FormaFisica formaFisica;
        public FormaFisica FormaFisica
        {
            get { return formaFisica; }
            set { SetPropertyValue<FormaFisica>("FormaFisica", ref formaFisica, value); }
        }

        private string codiceEsterno;
        public string CodiceEsterno
        {
            get { return codiceEsterno; }
            set { SetPropertyValue<string>("CodiceEsterno", ref codiceEsterno, value); }
        }


        [Association,Aggregated]
        public XPCollection<AllegatoOrdineProduzione> AllegatiOrdineProduzione
        {
            get { return GetCollection<AllegatoOrdineProduzione>("AllegatiOrdineProduzione"); }
        }

        [Association]
        public XPCollection<Odl> Odls
        {
            get { return GetCollection<Odl>("Odls"); }
        }


}


    public class AllegatoOrdineProduzione : BaseXPObject
    {
        public AllegatoOrdineProduzione(Session session)
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

        private OrdineProduzione ordineProduzione;
        [Association]
        public OrdineProduzione OrdineProduzione
        {
            get { return ordineProduzione; }
            set { SetPropertyValue<OrdineProduzione>("OrdineProduzione", ref ordineProduzione, value); }
        }

        //private int count;
        //public int Count
        //{
        //    get { return count; }
        //    set { SetPropertyValue<int>("Count", ref count, value); }
        //}

    }
}
