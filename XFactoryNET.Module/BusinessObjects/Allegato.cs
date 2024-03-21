using System;
using DevExpress.Xpo;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;

namespace XFactoryNET.Module.BusinessObjects
{
    [DefaultProperty("CodiceAllegato")]
    public class Allegato : BaseXPCustomObject
    {

        internal static string GetElencoAllegati(IEnumerable<Allegato> allegati)
        {
            string codice = string.Empty;
            if (allegati != null)
            {
                foreach (var all in allegati)
                {
                    if (string.IsNullOrEmpty(codice) == false)
                        codice += " ";
                    codice += all.CodiceAllegato;
                }
            }
            return codice;
        }


        string fCodiceAllegato;
        [Key]
        [Size(12)]
        public string CodiceAllegato
        {
            get { return fCodiceAllegato; }
            set { SetPropertyValue<string>("Codice", ref fCodiceAllegato, value); }
        }

        [Association,Aggregated, Browsable(false)]
        public XPCollection<AllegatoOdl> AllegatiOdl
        {
            get { return GetCollection<AllegatoOdl>("AllegatiOdl"); }
        }

        [Association,Aggregated,  Browsable(false)]
        public XPCollection<AllegatoOrdineProduzione> AllegatiOrdineProduzione
        {
            get { return GetCollection<AllegatoOrdineProduzione>("AllegatiOrdineProduzione"); }
        }


        [Association,Aggregated, Browsable(false)]
        public XPCollection<AllegatoLotto> AllegatiLotto
        {
            get { return GetCollection<AllegatoLotto>("AllegatiLotto"); }
        }

        [Association,Aggregated,  Browsable(false)]
        public XPCollection<AllegatoArticolo> AllegatiArticolo
        {
            get { return GetCollection<AllegatoArticolo>("AllegatiArticolo"); }
        }

        [Association, Aggregated]
        public XPCollection<DettaglioAllegato> DettagliAllegato
        {
            get { return GetCollection<DettaglioAllegato>("DettagliAllegato"); }
        }

        protected override XPCollection<T> CreateCollection<T>(DevExpress.Xpo.Metadata.XPMemberInfo property)
        {
            XPCollection<T> result = base.CreateCollection<T>(property);
            if (property.Name == "Odl")
                result.CollectionChanged += new XPCollectionChangedEventHandler(result_CollectionChanged);
            return result;
        }

        protected override XPCollection CreateCollection(DevExpress.Xpo.Metadata.XPMemberInfo property)
        {
            XPCollection result = base.CreateCollection(property);
            if (property.Name == "Odl")
                result.CollectionChanged += new XPCollectionChangedEventHandler(result_CollectionChanged);
            return result;
        }

        void result_CollectionChanged(object sender, XPCollectionChangedEventArgs e)
        {
        }

        public Allegato(Session session) : base(session) { }
        public Allegato() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
