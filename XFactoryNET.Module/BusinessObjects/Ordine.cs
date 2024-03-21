using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace XFactoryNET.Module.BusinessObjects
{
    public class Ordine:BaseXPObject
    {
        public Ordine():base(Session.DefaultSession)
        {

        }

        public Ordine(Session session):base(session)
        {

        }

        private int numeroOrdine;
        public int NumeroOrdine
        {
            get { return numeroOrdine; }
            set { SetPropertyValue<int>("NumeroOrdine", ref numeroOrdine, value); }
        }

        private DateTime data;

        public DateTime Data
        {
            get { return data; }
            set { SetPropertyValue<DateTime>("Data", ref data, value); }
        }

        private DateTime dataCarico;
        public DateTime DataCarico
        {
            get { return dataCarico; }
            set { SetPropertyValue<DateTime>("DataCarico", ref dataCarico, value); }
        }

        private Azienda autotrasportatore;
        public Azienda Autotrasportatore
        {
            get { return autotrasportatore; }
            set { SetPropertyValue<Azienda>("Autotrasportatore", ref autotrasportatore, value); }
        }

        private int numeroCarico;
        public int NumeroCarico
        {
            get { return numeroCarico; }
            set { SetPropertyValue<int>("NumeroCarico", ref numeroCarico, value); }
        }



        private Azienda cliente;
        public Azienda Cliente
        {
            get
            {
                return cliente;
            }
            set
            {
                SetPropertyValue<Azienda>("Cliente", ref cliente, value);
            }
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




    }
}
