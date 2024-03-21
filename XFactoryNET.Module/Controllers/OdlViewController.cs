using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;

using XFactoryNET.Module.BusinessObjects;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Validation;
using DevExpress.ExpressApp.Validation.AllContextsView;

namespace XFactoryNET.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class OdlViewController : ViewController
    {

        ShowAllContextsController showAllContextsController;

        public OdlViewController()
        {
            InitializeComponent();
            RegisterActions(components);

            // Target required Views (via the TargetXXX properties) and create their Actions.
        }

        AllegatoOdl allegatoOdl;

        protected override void OnActivated()
        {
            base.OnActivated();

            showAllContextsController = Frame.GetController<ShowAllContextsController>();

            // Perform various tasks depending on the target View.
            if (this.View.CurrentObject as Odl != null)
                View.ObjectSpace.ObjectChanged+=new EventHandler<ObjectChangedEventArgs>(ObjectSpace_ObjectChanged);
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
            View.ObjectSpace.ObjectChanged -= new EventHandler<ObjectChangedEventArgs>(ObjectSpace_ObjectChanged);
        }

        void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {

            AllegatoOdl ao = e.Object as AllegatoOdl;
            if (ao != null)
            {
                if (!ao.IsLoading && !ao.IsDeleted)
                {
                    ApplicaAllegato(ao.Odl, ao.Allegato);
                }
                return;
            }

            if (e.NewValue == e.OldValue)
                return;

            Odl odl = e.Object as Odl;

            if (odl != null && !odl.IsDeleted && !odl.IsLoading)
            {
                switch (e.PropertyName)
                {
                    case "OrdineProduzione":
                        OnChangedOdp(odl);
                        break;
                    case "Articolo":
                        OnChangedArticolo(odl);
                        odl.Stato = StatoOdL.InPreparazione;
                        break;
                    case "Formula":
                        OnChangedFormula(odl);
                        odl.Stato = StatoOdL.InPreparazione;
                        break;
                    case "Prelievo":
                        if (odl.Prelievo != null)
                        {
                            if (odl.Prelievo.Lotto != null)
                            {
                                if (odl.Prelievo.Lotto.Odl != null)
                                {
                                    odl.OrdineProduzione = odl.Prelievo.Lotto.Odl.OrdineProduzione;
                                    if (odl.OrdineProduzione != null)
                                        odl.Confezione = odl.OrdineProduzione.Confezione;
                                }
                            }

                            if (odl.Prelievo.Articolo != null)
                            {
                                odl.Articolo = odl.Prelievo.Articolo;
                                odl.Quantit‡ = odl.Prelievo.Quantit‡;
                            }
                            else
                            {
                                odl.Articolo = null;
                                odl.Quantit‡ = 0;
                            }
                        }
                        odl.Stato = StatoOdL.Pronto;
                        break;
                    case "Destinazione":
                        if (odl.TipoLavorazione == BusinessObjects.TipoLavorazione.EntrataMerci)
                        {
                            if (odl.Destinazione != null)
                            {
                                if (odl.Destinazione.Articolo != null)
                                {
                                    odl.Articolo = odl.Destinazione.Articolo;
                                }
                                else
                                {
                                    odl.Articolo = null;
                                }
                            }
                        }
                        odl.Stato = StatoOdL.InPreparazione;
                        break;
                    //case "Quantit‡":
                    //case "Quantit‡PerMiscelata":
                    //    if (odl.TipoLavorazione == BusinessObjects.TipoLavorazione.EntrataMerci)
                    //    {
                    //        if (odl.Ingredienti.Count > 0)
                    //            odl.Ingredienti[0].Qt‡Teo = odl.Quantit‡;
                    //        if (odl.Prodotti.Count > 0)
                    //            odl.Prodotti[0].Qt‡Teo = odl.Quantit‡;
                    //    }
                    //    odl.Stato = StatoOdL.InPreparazione;
                    //    break;
                }

            }

            Componente comp = e.Object as Componente;
            if (comp != null && comp.IsLoading == false && comp.IsDeleted == false)
            {
                if (e.PropertyName != "Stato")
                {
                    if (comp.OdlIngredientiTeorici != null)
                    {
                        if (comp.OdlIngredientiTeorici.LoadingFormula == false)
                        {
                            comp.OdlIngredientiTeorici.Stato = StatoOdL.InPreparazione;
                            comp.StatoModifica = StatoModificaComponente.Modificato;
                        }
                    }
                }
            }

        }

        protected void OnChangedOdp(Odl odl)
        {
            PredisponiOdp(odl.OrdineProduzione, odl);
        }

        protected void OnChangedArticolo(Odl odl)
        {
            Formula fPrec = odl.Formula;
            if (odl.Articolo != null)
            {
                odl.Formula = odl.Articolo.Formule.FirstOrDefault(f => f.Lavorazione == odl.Lavorazione);
                if (odl.Formula != null)
                {
                    odl.Quantit‡PerMiscelata = odl.Articolo.Quantit‡PerMiscelata;
                    odl.NumeroMiscelate = 1;
                }
            }
            else
            {
                odl.Formula = null;
            }
            //Se non Ë cambiata la formula ed Ë ancora null, ricalcola componenti per riflettere nuovo articolo selezionato
            if (fPrec == null && odl.Formula == null)
                innerApplicaFormula(odl);
        }

        protected void OnChangedFormula(Odl odl)
        {
            innerApplicaFormula(odl);
        }

        private void actionSostituisci_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            e.DialogController.SaveOnAccept = false;
            e.View = CreateListViewFormuleSostituzione(this.View.ObjectSpace, this.View.CurrentObject as Componente);
        }

        private View CreateListViewFormuleSostituzione(IObjectSpace os, Componente comp)
        {
            //IObjectSpace os = this.View.ObjectSpace;
            //IObjectSpace os = Utils.GetSecondObjectSpace(comp);
            CollectionSource cs = new CollectionSource(os, typeof(Formula));
            cs.Criteria["Criteria"] = new ContainsOperator("Articoli", new BinaryOperator("Codice", comp.Articolo.Codice));
            ListView lv = Application.CreateListView("Formula_ListViewAndDetailView", cs, false);
            lv.Caption = string.Format("Sostituzione materiale {0}: {1}", comp.Articolo.Codice, comp.Articolo.Descrizione);
            return lv;

        }

        private void actionSostituisci_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            Componente comp = (Componente)this.View.CurrentObject;
            Formula sos = (Formula)e.PopupWindowViewCurrentObject;
            if (sos != null)
            {
                if (comp.OdlIngredientiTeorici != null)
                    this.SostituisciComponente(comp, sos);
            }
        }



        public void SostituisciComponente(Componente componente, Formula form)
        {
            internalSostituisciComponente(componente, form);
            componente.OdlIngredientiTeorici.FormuleSostituzione.Add(form);

        }

        internal void internalSostituisciComponente(Componente componente, Formula form)
        {
            float perc = componente.Percentuale;
            componente.StatoModifica = StatoModificaComponente.Eliminato;
            componente.Percentuale = 0;
            //componente.Delete();
            Odl odl = componente.OdlIngredientiTeorici;

            foreach (var comp in form.Ingredienti)
            {
                Componente newComp;
                newComp = odl.IngredientiTeorici.FirstOrDefault<Componente>(c => c.Articolo == comp.Articolo);
                if (newComp == null)
                {
                    newComp = new Componente(odl.Session);
                    newComp.Percentuale = perc * comp.Percentuale / 100;
                    newComp.Tolleranza = comp.Tolleranza;
                    newComp.Articolo = comp.Articolo;
                    newComp.Modalit‡ = comp.Modalit‡;
                    newComp.Modalit‡ = comp.Articolo.Modalit‡;          //Modalit‡ predefinita del materiale
                    newComp.StatoModifica = StatoModificaComponente.Aggiunto;
                    odl.IngredientiTeorici.Add(newComp);
                    ApplicaSostituzioneComponente(comp);
                }
                else
                {
                    newComp.Percentuale += (perc * comp.Percentuale / 100);
                    newComp.StatoModifica = StatoModificaComponente.Modificato;
                }
            }
        }

        bool sostituzioniApplicate = false;

        public void Avvia(Odl odl)
        {

            if (!sostituzioniApplicate)
            {
                sostituzioniApplicate = true;
                ApplicaSostituzioni(odl);
            }

            if (Predisponi(odl) == false)
                return;

            if (odl.OnInAvvio() == false)
            {

                foreach (var lot in odl.Prodotti)
                {
                    if (odl.Destinazione != null)
                        odl.Destinazione.RegistraMovimento(lot);            //Qui la quantit‡ del lotto Ë ancora a zero ma il silos viene gi‡ impegnato.
                    if (odl.MagazzinoDestinazione != null)
                        odl.MagazzinoDestinazione.RegistraMovimento(lot);

                    lot.CodiceEsterno = odl.CodiceEsterno;
                }
                odl.Stato = StatoOdL.Avviato;

                GetOrCreateOdp(odl);

                //odl.Session.CommitTransaction();

                if (odl.Dummy)
                    odl.EseguiTeorico();
                else
                    odl.OnAvviato();
            }
            this.View.ObjectSpace.CommitChanges();
            this.View.Close();
        }

        public void ApplicaAllegato(Odl odl, Allegato allegato)
        {
            ApplicaAllegato(odl, allegato, 1);
        }

        public void ApplicaAllegato(Odl odl,Allegato allegato,int count)
        {
            for (int i = 0; i < count; i++)
            {
                foreach (var item in allegato.DettagliAllegato)
                {
                    ApplicaVariazione(odl, item);
                }
            }
        }

        public void ApplicaAllegatiArticolo(Odl odl)
        {
            if (odl.Articolo != null)
            {
                if (odl.Articolo.AllegatiArticolo != null)
                {
                    foreach (var item in odl.Articolo.AllegatiArticolo)
                    {
                        odl.AllegatiOdl.Add(new AllegatoOdl(odl.Session) { Allegato = item.Allegato });
                    }
                }
            }
        }

        private void innerApplicaFormula(Odl odl)
        {
            sostituzioniApplicate = false;
            ApplicaFormula(odl);
            ApplicaAllegatiArticolo(odl);
        }

        public virtual void ApplicaFormula(Odl odl)
        {
            odl.LoadingFormula = true;

            while (odl.FormuleSostituzione.Count > 0)
            {
                odl.FormuleSostituzione.Remove(odl.FormuleSostituzione[0]);
            }
            

            while (odl.AllegatiOdl.Count > 0)
            {
                odl.AllegatiOdl[0].Delete();
            }

            while (odl.ProdottiTeorici.Count > 0)
            {
                odl.ProdottiTeorici[0].Delete();
            }

            while (odl.IngredientiTeorici.Count > 0)
            {
                odl.IngredientiTeorici[0].Delete();
            }

            while (odl.Ingredienti.Count > 0)
            {
                odl.Ingredienti[0].Delete();
            }

            while (odl.Prodotti.Count > 0)
            {
                odl.Prodotti[0].Delete();
            }

            if (odl.Formula != null)
            {
                foreach (var prod in odl.Formula.Prodotti)
                {
                    odl.ProdottiTeorici.Add(prod.Clona());
                }
                foreach (var ingr in odl.Formula.Ingredienti)
                {
                    odl.IngredientiTeorici.Add(ingr.Clona());
                }
            }

            if (odl.Articolo != null)
            {
                if (odl.ProdottiTeorici.Count == 0 && odl.TipoLavorazione != TipoLavorazione.UscitaMerci)
                {
                    //Prodotto
                    Componente cp = new Componente(odl.Session);
                    cp.Articolo = odl.Articolo;
                    cp.Percentuale = 100;
                    cp.Stato = StatoComponente.Pronto;
                    odl.ProdottiTeorici.Add(cp);
                }

                if (odl.IngredientiTeorici.Count == 0 && odl.TipoLavorazione != TipoLavorazione.EntrataMerci)
                {
                    //Ingrediente
                    Componente cp = new Componente(odl.Session);
                    cp.Articolo = odl.Articolo;
                    cp.Percentuale = 100;
                    cp.Stato = StatoComponente.Pronto;
                    odl.IngredientiTeorici.Add(cp);

                }
            }

            odl.LoadingFormula = false;

        }

        public void ApplicaSostituzioneComponente(Componente item)
        {
            if (item.Articolo.TipoSostituzione == TipoSostituzione.Manuale)
            {
                ShowViewParameters svp = new ShowViewParameters();
                DialogController dlc = Application.CreateController<DialogController>();
                dlc.SaveOnAccept = false;
                dlc.AcceptAction.Execute += new SimpleActionExecuteEventHandler(AcceptAction_Execute);
                svp.Controllers.Add(dlc);
                svp.TargetWindow = TargetWindow.NewModalWindow;
                svp.CreatedView = CreateListViewFormuleSostituzione(this.View.ObjectSpace,item);
                svp.CreatedView.CurrentObjectChanged += new EventHandler(CreatedView_CurrentObjectChanged);
                Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(this.Frame,null));
            }
            else if (item.Articolo.TipoSostituzione == TipoSostituzione.Automatica)
            {
                foreach (Formula formula in item.Articolo.Formule.Where(f => item.OdlIngredientiTeorici.Classi.Contains(f.ClasseMateriali)))
                {
                    SostituisciComponente(item, formula);
                }
            }

        }

        public void ApplicaSostituzioni(Odl odl)
        {
            
            foreach (var item in new List<Componente>(odl.IngredientiTeorici))
            {
                currentComp = item;
                ApplicaSostituzioneComponente(item);
            }
            currentComp = null;
        }

        void CreatedView_CurrentObjectChanged(object sender, EventArgs e)
        {
            currentForm = ((View)sender).CurrentObject as Formula;
        }

        private Componente currentComp = null;
        private Formula currentForm = null;

        void AcceptAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (currentComp != null)
            {
                if (currentForm != null)
                {
                    if (currentForm.Session.Equals(currentComp.Session) == false)
                        currentForm = currentComp.Session.GetObjectByKey<Formula>(currentForm.Codice);
                    SostituisciComponente(currentComp, currentForm);
                }
            }
        }

        public void ApplicaVariazione(Odl odl, DettaglioAllegato item)
        {
            if (item.ArticoloIngrediente == null)
                return;

            Componente comp = odl.IngredientiTeorici.FirstOrDefault<Componente>(c => c.Articolo == item.Articolo);
            if (comp == null)
            {
                comp = new Componente(odl.Session) { StatoModifica = StatoModificaComponente.Aggiunto, Articolo = item.ArticoloIngrediente };
                comp.Modalit‡ = item.ArticoloIngrediente.Modalit‡;
                odl.IngredientiTeorici.Add(comp);
                ApplicaSostituzioneComponente(comp);
            }
            else
            {
                comp.StatoModifica = StatoModificaComponente.Modificato;
            }

            if (item.Assoluta)
                comp.Percentuale = item.Valore;
            else
                comp.Percentuale += item.Valore;

        }

        public bool Predisponi(Odl odl)
        {

            IObjectSpace os = View.ObjectSpace;
            if (Validator.RuleSet.ValidateTarget(os, odl, "AvviaOdl").State != ValidationState.Valid)
            {
                showAllContextsController.Action.DoExecute();
                return false;
            }

            bool eFattibile = OnPredisponi(odl);

            if (!eFattibile || Validator.RuleSet.ValidateAll(os, odl.Ingredienti, "Verifica Ingredienti") == false)
            {
                showAllContextsController.Action.DoExecute();
                return false;
            }

            if (eFattibile)
                CopiaAllegati(odl);

            if (eFattibile)
                eFattibile = !odl.OnPredisposto();

            if (eFattibile)
                odl.Stato = StatoOdL.Pronto;
            else
                odl.Stato = StatoOdL.InPreparazione;

            return eFattibile;
        }

        private bool OnPredisponi(Odl odl)
        {
            switch (odl.Lavorazione.Codice)
            {
                case OdlDosaggio.IDLavorazione:
                    return OnPredisponi((OdlDosaggio)odl);
                case OdlScaricoRinfusa.IDLavorazione:
                    return OnPredisponi((OdlScaricoRinfusa)odl);
                case OdlCaricoRinfusa.IDLavorazione:
                    return OnPredisponi((OdlCaricoRinfusa)odl);
                case OdlInsacco.IDLavorazione:
                    return OnPredisponi((OdlInsacco)odl);
                case OdlPellet.IDLavorazione:
                    return OnPredisponi((OdlPellet)odl);
                case OdlInsilaggio.IDLavorazione:
                    return OnPredisponi((OdlInsilaggio)odl);
                case OdlEntrataSacchi.IDLavorazione:
                    return OnPredisponi((OdlEntrataSacchi)odl);
                case OdlUscitaSacchi.IDLavorazione:
                    return OnPredisponi((OdlUscitaSacchi)odl);
                default:
                    break;
            }
            return false;
        }

        private bool OnPredisponi(OdlDosaggio odl)
        {
            bool eFattibile = true;

            while (odl.Ingredienti.Count > 0)
            {
                odl.Ingredienti[0].Delete();
            }

            while (odl.Prodotti.Count > 0)
            {
                odl.Prodotti[0].Delete();
            }
            //for (int nrLotto = 1; nrLotto <= this.NumeroMiscelate; nrLotto++)
            {
                int nrProdotto = 0;
                foreach (Componente comp in new List<Componente>(odl.ProdottiTeorici))
                {
                    nrProdotto++;

                    float qt‡Ingr = comp.Percentuale * odl.Quantit‡PerMiscelata / 100;

                    for (int nrMisc = 1; nrMisc <= odl.NumeroMiscelate; nrMisc++)
                    {
                        //Prodotto
                        //Crea un nuovo lotto per ciascun prodotto e per ciascuna miscelata
                        Lotto lottoProdotto = new Lotto(odl.Session)
                        {
                            Articolo = comp.Articolo,
                            Qt‡Teo = qt‡Ingr,
                            Stato = StatoLotto.Pronto,
                            NrMisc = nrMisc,
                            NrComp = nrProdotto,
                            TipoMovimento = TipoMovimento.Produzione
                        };

                        //Non posso registrare qui perchË l' OdL potrebbe essere ancora cancellato.
                        //Destinazione.RegistraMovimento(lottoProdotto);

                        lottoProdotto.Odl = odl;
                    }
                }

                Dictionary<string, int> nrIngrediente = new Dictionary<string, int>();
                foreach (Componente comp in new List<Componente>(odl.IngredientiTeorici))
                {
                    float qt‡Ingr = comp.Percentuale * odl.Quantit‡ / 100;
                    if (qt‡Ingr > 0)
                    {
                        //Ingredienti
                        comp.Stato = StatoComponente.Assente;
                        XPCollection<Lotto> lotti = new XPCollection<Lotto>(odl.Session);
                        XPCollection<Silos> silos = new XPCollection<Silos>(odl.Session);
                        XPCollection<Magazzino> magazzini = new XPCollection<Magazzino>(odl.Session);
                        XPCollection<Percorso> percorsi = new XPCollection<Percorso>(odl.Session);
                        Magazzino mNullo = null;
                        var query = from app in silos
                                    join perc in percorsi on app equals perc.ApparatoFrom
                                    where (app.Articolo == comp.Articolo &&
                                        //(comp.Modalit‡ == null || perc.Modalit‡ == null || perc.Modalit‡ == comp.Modalit‡) &&
                                        perc.ApparatoFrom.ScaricoAbilitato &&
                                        perc.ApparatoTo.CaricoAbilitato
                                        )
                                    select new { Apparato = app, Magazzino = mNullo, Percorso = perc, Sacchi = false, Qta = app.Quantit‡,Lot=app.Lotto};

                        if (comp.Modalit‡ != null && comp.Modalit‡.Sacchi)
                        {
                            Percorso pNullo = null;
                            Silos aNull = null;
                            var query2 = from lot in lotti
                                            join maga in magazzini on lot.Magazzino equals maga
                                            where (lot.Articolo == comp.Articolo)
                                            select new { Apparato = aNull, Magazzino = maga, Percorso = pNullo, Sacchi = true, Qta = lot.Quantit‡,Lot = lot };
                            if (comp.Modalit‡ == null)
                                query = query.Union(query2);
                            else
                                query = query2;
                        }

                        float qt‡Teo;
                        foreach (var item in query.OrderByDescending(a => a.Qta))
                        {
                            Apparato apparatoLavorazione = null;
                            string codiceApparatoLavorazione;
                            float qt‡Disp = 0;
                            if (item.Sacchi == false)
                            {
                                //Just One Step Beyond
                                //Controlla che l'apparato di lavorazione sia raggiungibile in, al pi˘, un passo.
                                if (item.Percorso.ApparatoTo != odl.ApparatoLavorazione && item.Percorso.ApparatoTo.ApparatiDestinazione.Contains(odl.ApparatoLavorazione) == false)
                                    continue;
                                apparatoLavorazione = item.Percorso.ApparatoTo;
                                codiceApparatoLavorazione = apparatoLavorazione.Codice;       //Codice Bilancia
                                qt‡Disp = item.Apparato.Quantit‡;
                            }
                            else
                            {
                                codiceApparatoLavorazione = item.Magazzino.Codice;
                                qt‡Disp = item.Qta;
                            }

                            if (qt‡Disp < qt‡Ingr)
                                qt‡Teo = qt‡Disp;
                            else
                                qt‡Teo = qt‡Ingr;


                            if (nrIngrediente.ContainsKey(codiceApparatoLavorazione) == false)
                                nrIngrediente.Add(codiceApparatoLavorazione, 1);
                            int nrIngr = nrIngrediente[codiceApparatoLavorazione];
                            nrIngrediente[codiceApparatoLavorazione]++;

                            for (int nrMisc = 1; nrMisc <= odl.NumeroMiscelate; nrMisc++)
                            {
                                Lotto ingr = new Lotto(odl.Session)
                                {
                                    Articolo = comp.Articolo,
                                    Qt‡Teo = qt‡Teo / odl.NumeroMiscelate,
                                    NrMisc = nrMisc,
                                    NrComp = nrIngr,
                                    TipoMovimento = BusinessObjects.TipoMovimento.Consumo,
                                    Stato = StatoLotto.Pronto,
                                    Odl = odl
                                };

                                if (item.Sacchi)
                                {
                                    ingr.Magazzino = item.Magazzino;
                                    ingr.Modalit‡ = comp.Modalit‡;
                                    ingr.Confezione = item.Lot.Confezione;
                                    ingr.Magazzino.RegistraMovimento(ingr);
                                }
                                else
                                {
                                    item.Apparato.RegistraMovimento(ingr);
                                    ingr.ApparatoLavorazione = apparatoLavorazione;        //ad esempio: Bilancia di dosaggio
                                    ingr.Modalit‡ = item.Percorso.Modalit‡;
                                }

                                ingr.Tolleranza = System.Math.Max(comp.Tolleranza, comp.TolleranzaPerc * qt‡Teo);
                            }
                            qt‡Ingr -= qt‡Teo;
                            if (qt‡Ingr <= 0)
                            {
                                comp.Stato = StatoComponente.Pronto;
                                break;
                            }
                        }
                        if (qt‡Ingr > 0)
                        {
                            comp.Stato = StatoComponente.Insufficiente;
                            eFattibile = false;
                        }

                    }
                }
            }

            return eFattibile;

        }

        private bool OnPredisponi(OdlEntrataSacchi odl)
        {
            bool eFattibile = true;

            if (odl.MagazzinoDestinazione == null || odl.Confezione == null)
                eFattibile = false;

            if (eFattibile)
            {
                while (odl.Ingredienti.Count > 0)
                    odl.Ingredienti[0].Delete();

                while (odl.Prodotti.Count > 0)
                    odl.Prodotti[0].Delete();

                Lotto lot = new Lotto(odl.Session) { Articolo = odl.Articolo, TipoMovimento = TipoMovimento.Produzione,Confezione = odl.Confezione };
                lot.Qt‡Teo = odl.Quantit‡;
                lot.NrComp = 1;
                lot.NrMisc = 1;
                odl.Lotti.Add(lot);

                odl.Dummy = true;
                odl.Stato = StatoOdL.Pronto;
            }

            return eFattibile;
        }


        private bool OnPredisponi(OdlUscitaSacchi odl)
        {
            bool eFattibile = true;

            if (odl.MagazzinoPrelievo == null || odl.Confezione == null)
                eFattibile = false;

            if (eFattibile)
            {
                while (odl.Ingredienti.Count > 0)
                    odl.Ingredienti[0].Delete();

                while (odl.Prodotti.Count > 0)
                    odl.Prodotti[0].Delete();

                Lotto lot = new Lotto(odl.Session)
                {
                    Articolo = odl.Articolo,
                    Magazzino = odl.MagazzinoPrelievo,
                    Confezione = odl.Confezione,
                    TipoMovimento = TipoMovimento.UscitaMerci,
                    Qt‡Teo = odl.Quantit‡,
                    NrComp = 1,
                    NrMisc = 1,
                    Odl = odl
                };

                odl.MagazzinoPrelievo.RegistraMovimento(lot);

                odl.Dummy = true;
                odl.Stato = StatoOdL.Pronto;
            }

            return eFattibile;
        }


        private bool OnPredisponi(OdlInsilaggio odl)
        {
            bool eFattibile = true;

           if (odl.MagazzinoPrelievo == null || odl.Confezione == null)
                eFattibile = false;

            if (!eFattibile || odl.Destinazione == null)
                eFattibile = false;

            if (eFattibile)
            {
                while (odl.Ingredienti.Count > 0)
                    odl.Ingredienti[0].Delete();

                while (odl.Prodotti.Count > 0)
                    odl.Prodotti[0].Delete();

                Lotto lot = new Lotto(odl.Session)
                {
                    Articolo = odl.Articolo,
                    Magazzino = odl.MagazzinoPrelievo,
                    Confezione = odl.Confezione,
                    TipoMovimento = TipoMovimento.Consumo,
                    Qt‡Teo = odl.Quantit‡,
                    NrComp = 1,
                    NrMisc = 1,
                    Odl = odl
                };

                odl.MagazzinoPrelievo.RegistraMovimento(lot);

                lot = new Lotto(odl.Session) { Articolo = odl.Articolo, TipoMovimento = TipoMovimento.Produzione };
                lot.Qt‡Teo = odl.Quantit‡;
                lot.NrComp = 1;
                lot.NrMisc = 1;
                odl.Lotti.Add(lot);

                odl.Dummy = true;
                odl.Stato = StatoOdL.Pronto;
            }

            return eFattibile;
        }

        private bool OnPredisponi(OdlPellet odl)
        {
            bool eFattibile = true;

            if (odl.Prelievo == null || odl.Prelievo.Articolo == null)
                eFattibile = false;

            if (!eFattibile || odl.Destinazione == null)
                eFattibile = false;

            if (eFattibile)
            {

                while (odl.Ingredienti.Count > 0)
                    odl.Ingredienti[0].Delete();

                while (odl.Prodotti.Count > 0)
                    odl.Prodotti[0].Delete();

                Lotto lot = new Lotto(odl.Session)
                    {
                        Articolo = odl.Articolo,
                        Silos = odl.Prelievo,
                        TipoMovimento = TipoMovimento.Consumo,
                        Qt‡Teo = odl.Quantit‡,
                        NrComp = 1,
                        NrMisc = 1,
                        Odl = odl
                    };
                odl.Prelievo.RegistraMovimento(lot);

                lot = new Lotto(odl.Session) { Articolo = odl.Articolo, TipoMovimento = TipoMovimento.Produzione };
                lot.Qt‡Teo = odl.Quantit‡;
                lot.NrComp = 1;
                lot.NrMisc = 1;
                odl.Lotti.Add(lot);

                odl.Dummy = true;
                odl.Stato = StatoOdL.Pronto;



            }
            return eFattibile;
        }

        private bool OnPredisponi(OdlInsacco odl)
        {
            bool eFattibile = true;


            if (odl.Prelievo == null || odl.Prelievo.Articolo == null)
                eFattibile = false;

            if (eFattibile)
            {

                while (odl.Ingredienti.Count > 0)
                    odl.Ingredienti[0].Delete();

                while (odl.Prodotti.Count > 0)
                    odl.Prodotti[0].Delete();

                Lotto lot = new Lotto(odl.Session) { Articolo = odl.Articolo, Silos = odl.Prelievo, TipoMovimento = TipoMovimento.Consumo };
                lot.Qt‡Teo = odl.Quantit‡;
                lot.NrComp = 1;
                lot.NrMisc = 1;
                lot.Odl = odl;
                odl.Prelievo.RegistraMovimento(lot);

                lot = new Lotto(odl.Session) { Articolo = odl.Articolo,Magazzino = odl.MagazzinoDestinazione,  TipoMovimento = TipoMovimento.Produzione };
                lot.Qt‡Teo = odl.Quantit‡;
                lot.Confezione = odl.Confezione;
                lot.NrComp = 1;
                lot.NrMisc = 1;
                lot.Odl = odl;

                odl.Dummy = true;
                odl.Stato = StatoOdL.Pronto;



            }
            return eFattibile;
        }

        private bool OnPredisponi(OdlCaricoRinfusa odl)
        {
            bool eFattibile = true;

            if (eFattibile)
            {

                while (odl.Ingredienti.Count > 0)
                    odl.Ingredienti[0].Delete();

                while (odl.Prodotti.Count > 0)
                    odl.Prodotti[0].Delete();

                Lotto lot = new Lotto(odl.Session) { Articolo = odl.Articolo, TipoMovimento = TipoMovimento.EntrataMerci };
                lot.Qt‡Teo = odl.Quantit‡;
                lot.NrComp = 1;
                lot.NrMisc = 1;
                lot.Odl = odl;
                odl.Dummy = true;
                odl.Stato = StatoOdL.Pronto;

            }
            return eFattibile;
        }


        private bool OnPredisponi(OdlScaricoRinfusa odl)
        {

            bool eFattibile = true;

            if (odl.Prelievo == null || odl.Prelievo.Articolo == null)
                eFattibile = false;
            else
            {

                while (odl.Ingredienti.Count > 0)
                    odl.Ingredienti[0].Delete();

                while (odl.Prodotti.Count > 0)
                    odl.Prodotti[0].Delete();

                Lotto lot = new Lotto(odl.Session)
                {
                    Articolo = odl.Articolo,
                    Silos = odl.Prelievo,
                    TipoMovimento = TipoMovimento.UscitaMerci,
                    Qt‡Teo = odl.Quantit‡,
                    NrComp = 1,
                    NrMisc = 1,
                    Odl = odl
                };
                odl.Prelievo.RegistraMovimento(lot);

                odl.Dummy = true;
                odl.Stato = StatoOdL.Pronto;

            }
            return eFattibile;
        }

        private OrdineProduzione CreaOrdineProduzione(Odl odl)
        {
            OrdineProduzione lp = odl.OrdineProduzione;
            if (lp == null)
            {
                lp = this.ObjectSpace.CreateObject<OrdineProduzione>();
                lp.Data = System.DateTime.Now;
                odl.OrdineProduzione = lp;
                foreach (var item in odl.AllegatiOdl)
                {
                    lp.AllegatiOrdineProduzione.Add(new AllegatoOrdineProduzione(odl.Session) { Allegato = item.Allegato });
                }
                lp.Articolo = odl.Articolo;
                lp.Formula = odl.Formula;
                lp.Note = odl.Note;
                lp.Quantit‡ = odl.Quantit‡;
                lp.Confezione = odl.Confezione;
                lp.FormaFisica = odl.Articolo.FormaFisica;
            }
            return lp;

        }

        private bool loadingOdp = false;

        private OrdineProduzione GetOrCreateOdp(Odl odl)
        {
            loadingOdp = true;
            OrdineProduzione odp = odl.OrdineProduzione;
            if (odp == null)
            {
                if (odl.Prelievo == null || odl.Prelievo.Lotto == null || odl.Prelievo.Lotto.Odl == null || odl.Prelievo.Lotto.Odl.OrdineProduzione == null)
                {
                    if (odl.Lavorazione.Codice == OdlDosaggio.IDLavorazione)
                        odp = CreaOrdineProduzione(odl);
                }
                else
                {
                    odl.Prelievo.Lotto.Odl.OrdineProduzione.Odls.Add(odl);
                    odp =  odl.Prelievo.Lotto.Odl.OrdineProduzione;
                }
            }

            if (odp != null)
            {
                if (odp.NumeroOrdine == 0)
                {
                    odp.SetNextNumeroOrdine(odl.Hidden);
                }
                if (odl.TipoLavorazione == TipoLavorazione.UscitaMerci)
                    odp.Stato = StatoOdP.Eseguito;
                else
                    odp.Stato = StatoOdP.InProduzione;
            }

            loadingOdp = false;

            return odp;

        }


        private void CopiaAllegati(Odl odl)
        {
            foreach (var lotto in odl.Prodotti)
            {
                if (odl.Prelievo != null && odl.Prelievo.Lotto != null)
                {
                    //Copia gli allegati del lotto di prelievo (per operazioni di Insacco/Pellett/...)
                    foreach (var item in odl.Prelievo.Lotto.AllegatiLotto)
                    {
                        lotto.AllegatiLotto.Add(new AllegatoLotto(odl.Session) { Allegato = item.Allegato });
                    }
                }
                foreach (var item in odl.AllegatiOdl)
                {
                    lotto.AllegatiLotto.Add(new AllegatoLotto(odl.Session) { Allegato = item.Allegato });
                }
            }
        }


        private void popupWindowShowActionArchivia_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            Session session = ((DevExpress.ExpressApp.Xpo.XPObjectSpace)ObjectSpace).Session;
            Odl odl = (Odl)this.View.CurrentObject;
            if (odl.Lavorazione != null)
            {
                session.ExecuteSproc("ArchiviaProduzione", odl.Lavorazione.Codice);
            }

            ShowMessageBox(e, "Produzioni archiviate");

        }

        private void ShowMessageBox(CustomizePopupWindowParamsEventArgs e, string text)
        {
            ShowMessageBox(e, text, false);
        }

        private void ShowMessageBox(CustomizePopupWindowParamsEventArgs e, string text, bool enableCancel)
        {
            MessageBoxClass msg = new MessageBoxClass() { Text = text };
            e.DialogController.SaveOnAccept = false;
            e.DialogController.CancelAction.Active["NothingToCancel"] = enableCancel;
            e.View = Application.CreateDetailView(Application.CreateObjectSpace(), msg);
            
        }

        private void simpleActionAvviaOdp_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            OrdineProduzione odp = (OrdineProduzione)this.View.CurrentObject;
            AvviaOdPDosaggio(odp,e);
        }

        private void AvviaOdPDosaggio(OrdineProduzione odp,SimpleActionExecuteEventArgs e)
        {

            IObjectSpace os = Application.CreateObjectSpace();

            odp = os.GetObject(odp);

            Odl odl = os.CreateObject<OdlDosaggio>();
            odl.OrdineProduzione = odp;

            PredisponiOdp(odp, odl);

            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(os, odl, true);

        }


        private void PredisponiOdp(OrdineProduzione odp, Odl odl)
        {
            if (loadingOdp)
                return;

            odl.Articolo = odp.Articolo;
            OnChangedArticolo(odl);
            odl.Formula = odp.Formula;
            innerApplicaFormula(odl);

            odl.Quantit‡ = odp.Quantit‡;
            foreach (var item in odp.AllegatiOrdineProduzione)
            {
                Allegato all = item.Allegato;
                odl.AllegatiOdl.Add(new AllegatoOdl(odl.Session) { Allegato = all });
                ApplicaAllegato(odl, all);
            }


        }

        private void singleChoiceActionSilos_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            switch (e.SelectedChoiceActionItem.Id)
            {
                case "Insacco":
                    CreaOdlPrelievo<OdlInsacco>(e);
                    break;
                case "Pellet":
                    CreaOdlPrelievo<OdlPellet>(e);
                    break;
                case "Scarico Rinfusa":
                    CreaOdlPrelievo<OdlScaricoRinfusa>(e);
                    break;
                case "Carico Rinfusa":
                    CreaOdlDestinazione<OdlCaricoRinfusa>(e);
                    break;
                case "Insila":
                    CreaOdlDestinazione<OdlInsilaggio>(e);
                    break;
                default:
                    break;
            }
        }

        private T CreaOdlDestinazione<T>(ActionBaseEventArgs e)
            where T : Odl
        {
            IObjectSpace os = Application.CreateObjectSpace();
            T odl = os.CreateObject<T>();
            if (this.View.CurrentObject is Silos)
            {
                Silos app = (Silos)os.GetObject(this.View.CurrentObject);
                odl.Destinazione = app;
                odl.Articolo = app.Articolo;
            }
            if (this.View.CurrentObject is Lotto)
            {
                Lotto lot = (Lotto)os.GetObject(this.View.CurrentObject);
                odl.MagazzinoDestinazione = lot.Magazzino;
            }
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(os, odl, true);
            e.ShowViewParameters.CreateAllControllers = true;
            e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
            return odl;
        }

        private T CreaOdlPrelievo<T>(ActionBaseEventArgs e)
            where T:Odl
        {
            IObjectSpace os = Application.CreateObjectSpace();
            T odl = os.CreateObject<T>();
            if (this.View.CurrentObject is Silos)
            {
                Silos app = (Silos)os.GetObject(this.View.CurrentObject);
                odl.Prelievo = app;
                odl.Articolo = app.Articolo;
                odl.Quantit‡ = app.Quantit‡;
            }
            if (this.View.CurrentObject is Lotto)
            {
                Lotto lot = (Lotto)os.GetObject(this.View.CurrentObject);
                odl.MagazzinoPrelievo = lot.Magazzino;
                odl.Articolo = lot.Articolo;
                odl.Confezione = lot.Confezione;
            }
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(os, odl, true);
            e.ShowViewParameters.CreateAllControllers = true;
            e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
            return odl;
        }

        private void simpleActionAvviaOdl_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Odl odl = (Odl)this.View.CurrentObject;
            Avvia(odl);
        }

        private void singleChoiceActionSacchi_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            switch (e.SelectedChoiceActionItem.Id)
            {
                case "Entrata sacchi":
                    CreaOdlDestinazione<OdlEntrataSacchi>(e);
                    break;
                case "Uscita sacchi":
                    CreaOdlPrelievo<OdlUscitaSacchi>(e);
                    break;
                default:
                    break;
            }
        }
    }
}
