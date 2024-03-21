using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;

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
        [ModelDefault("Format","D")]
        public short NumeroOrdine
        {
            get { return numeroOrdine; }
            set { SetPropertyValue<short>("NumeroOrdine", ref numeroOrdine, value); }
        }

        internal void SetNextNumeroOrdine(bool isHidden)
        {
            short nr = 0;
            var odp = new XPCollection<OrdineProduzione>(this.Session);
            if (isHidden)
            {
                if (odp.Count > 0)
                {
                    nr = odp.Min(o => o.NumeroOrdine);
                    if (nr > short.MinValue)
                        nr--;
                }
                else nr = -1;
            }
            else
            {
                if (odp.Count > 0)
                {
                    nr = odp.Max(o => o.NumeroOrdine);
                    if (nr < short.MaxValue)
                        nr++;
                }
                else nr = 1;
            }
            this.NumeroOrdine = nr;
        }

        private StatoOdP stato;
        public StatoOdP Stato
        {
            get { return stato; }
            set { SetPropertyValue<StatoOdP>("Stato", ref stato, value); }
        }

        private DateTime data;
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


        private float quantità;
        public float Quantità
        {
            get { return quantità; }
            set { SetPropertyValue<float>("Quantità", ref quantità, value); }
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
