using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;


namespace XFactoryNET.Module.BusinessObjects
{
    [NavigationItem("Lavorazioni")]
    [MapInheritance(MapInheritanceType.ParentTable)]
    [CreatableItem]
    public class OdlDosaggio : Odl
    {
        public const string IDLavorazione = "Dosaggio";

        protected override string idLavorazione
        {
            get { return IDLavorazione; }
        }

        public OdlDosaggio(Session session) : base(session) { }
        public OdlDosaggio() : base(Session.DefaultSession) { }

        public override void AfterConstruction()
        {
            //if (this.Lavorazione == null)
            //{
            //    Lavorazione = Session.GetObjectByKey<Lavorazione>(OdlDosaggio.IDLavorazione);
            //    if (Lavorazione == null)
            //        Lavorazione = new Lavorazione(Session) { Codice = OdlDosaggio.IDLavorazione };
            //    Lavorazione.Descrizione = "Dosaggio e miscelazione";
            //}
            base.AfterConstruction();
        }

        public override TipoLavorazione TipoLavorazione
        {
            get { return TipoLavorazione.Trasformazione; }
        }

        private bool checkFattibileLastResult = false;
        private bool skipSecondCheckFattibile = false;

        [RuleFromBoolProperty(TargetContextIDs = "AvviaOdl", CustomMessageTemplate = "Ordine non eseguibile per il vincolo seguente: {DescrizioneVincolo}")]
        [NonPersistent, Browsable(false)]
        public bool CheckFattibile
        {
            get
            {
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

        [NonPersistent]
        private Vincolo VincoloBloccante
        {
            get;
            set;
        }

        [Browsable(false)]
        public string DescrizioneVincolo
        {
            get { return VincoloBloccante==null ? string.Empty : VincoloBloccante.Descrizione; }
        }
        private bool VerificaCompatibilità()
        {

            //Verifica compatibilità degli ingredienti con le classi, con l'articolo e, ricursivamente, con tutte le classi a cui l'articolo appartiene
            //
            if (this.SkipCheck)
                return true;

            if (Bloccata(this.Articolo))
                return false;

            foreach (Componente comp in this.IngredientiTeorici)
            {
                if (Bloccata(comp.Articolo))
                    return false;

                comp.ArticoloIncompatibile = null;

                foreach (var cls in this.Categorie)
                {
                    if (recursiveCompatibile(cls, comp) == false)
                        return false;
                }
                if (this.Articolo != null && false == recursiveCompatibile(this.Articolo, comp))
                {
                    return false;
                }
                foreach (var comp2 in this.IngredientiTeorici)
                {
                    if (comp == comp2)
                        continue;
                    if (recursiveCompatibile(comp.Articolo, comp2) == false)
                        return false;
                }
            }

            VincoloBloccante = null;
            return true;

        }

        private bool innerBloccata(BaseArticolo art1)
        {
            if (art1 == null)
                return false;

            var odls = new XPCollection<OdlDosaggio>(Session, CriteriaOperator.Parse("Stato<>?", StatoOdL.InPreparazione), new SortProperty("Oid", DevExpress.Xpo.DB.SortingDirection.Descending));
            foreach (var vincolo in art1.Vincoli.Where(v => v.NProduzioni > 0))
            {
                VincoloBloccante = vincolo;
                if (art1 == vincolo.Articolo2)
                    break;

                if (art1.Categorie.Contains(vincolo.Articolo2))
                    break;

                if (this.IngredientiTeorici.Any(ingr => ingr.Percentuale > 0 && (ingr.Articolo == vincolo.Articolo2 || ingr.Articolo.Categorie.Contains(vincolo.Articolo2))))
                    break;

                int i = 0;
                var nMisc = vincolo.NProduzioni;
                while (nMisc > 0 && i < odls.Count)
                {
                    if (odls[i].Articolo == vincolo.Articolo2)
                        return true;
                    if (odls[i].Articolo.Categorie.Contains(vincolo.Articolo2))
                        return true;
                    if (odls[i].IngredientiTeorici.Any(ingr => ingr.Percentuale > 0 && (ingr.Articolo == vincolo.Articolo2 || ingr.Articolo.Categorie.Contains(vincolo.Articolo2))))
                        return true;
                    nMisc -= odls[i].NumeroMiscelateEseguite;
                    i++;
                }
            }

            return false;

        }
        private bool Bloccata(BaseArticolo art1)
        {
            if (art1 != null)
            {

                if (innerBloccata(art1))
                    return true;

                foreach (var categ in art1.Categorie)
                {
                    if (innerBloccata(categ))
                        return true;
                }
            }

            return false;
        }

        private bool recursiveCompatibile(BaseArticolo art1, Componente comp)
        {
            BaseArticolo art2 = comp.Articolo;
            VincoloBloccante = art1.Vincoli.FirstOrDefault<Vincolo>(v => v.Incompatibile && v.Articolo2 == art2);
            if (VincoloBloccante != null)
            {
                comp.ArticoloIncompatibile = art1;
                return false;
            }

            foreach (var cls in art1.Categorie)
            {
                if (recursiveCompatibile(cls, comp) == false)
                    return false;
            }

            return true;
        }



        //[ Browsable(false)]
        //public new IList<Apparato> ApparatiDestinazione
        //{
        //    get
        //    {
        //        return base.ApparatiDestinazione.Cast<Apparato>().ToList<Apparato>();
        //    }
        //}

        //[ Browsable(false)]
        //public new IList<Apparato> ApparatiPrelievo
        //{
        //    get
        //    {

        //        return base.ApparatiPrelievo.Cast<Apparato>().ToList<Apparato>();
        //    }
        //}

    }

}
