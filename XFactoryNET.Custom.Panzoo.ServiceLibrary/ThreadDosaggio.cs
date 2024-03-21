//#define SIMUL

using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using DevExpress.Xpo;
using System.Threading;
using XFactoryNET.Module.BusinessObjects;
using XFactoryNET.Custom.Panzoo.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DotNetSiemensPLCToolBoxLibrary.Communication;
using System.Data;


namespace XFactoryNET.Custom.Panzoo.ServiceLibrary
{
    public class ThreadDosaggio : IDisposable
    {
        public const int cNRINGR = 20;
        public const int cNRBIL = 5;

        public static ThreadDosaggio threadDosaggio;

        private static Thread thread;
        
        //private static Queue<int> msgQueue = new Queue<int>();

        private static bool bAbort = false;
        private static int iStopMisc = 0;  //Lotto a cui stoppare
        private static int currMisc = 0;   //Lotto corrente
        private static bool bRunning = false;
        private static int numeroOdp;
        private static int idOdl;
        private static int idCompProd;

        static bool[] lStart = new bool[cNRBIL];                   //Bilancia in dosaggio
        static bool[] iStato = new bool[cNRBIL];
        static float[] sPeso = new float[cNRBIL];                  //Peso sulla bilancia
        static float[] sEstratto = new float[cNRBIL];              //Peso estratto su bilancia
        static int[] iProgr = new int[cNRBIL];                     //Indice ingrediente in estrazione;

        private float qtàTot = 0F;                              //Peso Totale Estratto per la miscelata corrente

        private PLCConnection myConn;
        Dictionary<string, PLCTag> wordList = new Dictionary<string, PLCTag>();
        Dictionary<string, List<PLCTag>> buffList = new Dictionary<string, List<PLCTag>>();

        private UnitOfWork unitOfWork;

        OrdineProduzione odp;
        Odl odl;
        Lotto lotto;

        ParametriDosaggio pDos;

        XPCollection<Bilancia> bilance;

        public void Dispose()
        {
            if (myConn != null)
            {
                myConn.Dispose();
                myConn = null;
            }
        }

        public static void StartThread()
        {

            threadDosaggio = new ThreadDosaggio();
            ThreadStart start = new ThreadStart(threadDosaggio.Run);
            thread = new Thread(start) { Name = "Dosaggio" };
            thread.Start();

        }

        public static void StopThread()
        {
            if (thread != null)
            {
                thread.Abort();
                thread.Join();
            }
            thread = null;
        }

        public static void AvviaDosaggio(int oid)
        {
            //lock(msgQueue)
            //    msgQueue.Enqueue(oid);
        }

        public static void Arresta()
        {
            bAbort = true;
        }

        public static void StopMisc(int nrMisc)
        {
            iStopMisc = nrMisc;
        }

#if SIMUL
        private static Session staticSession;
#endif


        public static int IdOdl()
        {
#if SIMUL
            if (staticSession == null)
            {
                string connectionString = null;
                if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
                {
                    connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                }

                Console.WriteLine("Connection string: {0}", connectionString);

                staticSession = new Session() { ConnectionString = connectionString };
            }

            OdlDosaggio odl = staticSession.FindObject<OdlDosaggio>(null);
            if (odl != null)
                idOdl = odl.Oid;
#endif
            return idOdl;
        }

        public static int CurrMisc()
        {
#if SIMUL
            return 1;
#else
            return currMisc;
#endif
        }
        public static int NrMisc()
        {
            return iStopMisc;
        }

        public static bool Running()
        {
            return bRunning;
        }

        public static float GetPeso(int iBil)
        {
#if SIMUL
            return new Random().Next(1000);
#else
            return sPeso[iBil];
#endif
        }

        public static float QtàEstratta(int iBil)
        {
            return sEstratto[iBil];
        }

#if SIMUL
        static OdlDosaggio staticOdl;
        static int[] iProgrSIMUL = new int[cNRBIL];                     
#endif

        public static int NrIngrediente(int iBil)
        {
#if SIMUL
            if (staticOdl == null)
            {
                if (idOdl != 0)
                {
                    staticOdl = staticSession.GetObjectByKey<OdlDosaggio>(idOdl);
                }
            }
            if (staticOdl != null)
            {
                IEnumerable<Lotto> ingredienti = staticOdl.Ingredienti.Where(i =>i.ApparatoLavorazione != null &&  i.ApparatoLavorazione.Codice == "B" + (iBil + 1));
                if (ingredienti.Count() > 0)
                {
                    int iMax = ingredienti.Max(i => i.NrComp);
                    if (iProgrSIMUL[iBil] < iMax)
                        iProgrSIMUL[iBil]++;
                    else
                        iProgrSIMUL[iBil] = 1;
                }
                else
                    iProgrSIMUL[iBil] = 0;
            }
            return iProgrSIMUL[iBil];
#else
            return iProgr[iBil];
#endif
        }

        public static bool StatoBilancia(int iBil)
        {
            return iStato[iBil];
        }


        public void Run()
        {

                string connectionString = null;
                if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
                {
                    connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                }

                //var dl = XpoDefault.GetDataLayer(connectionString, DevExpress.Xpo.DB.AutoCreateOption.SchemaAlreadyExists);
                //var xpObjectSpaceDataProvider = new XPObjectSpaceProvider(new ConnectionDataStoreProvider(dl.Connection));
                //IObjectSpace os = (IObjectSpace)xpObjectSpaceDataProvider.CreateUpdatingObjectSpace(false);

                unitOfWork = new UnitOfWork() { ConnectionString = connectionString };


                bilance = new XPCollection<Bilancia>(unitOfWork, true);
#if !SIMUL
                myConn = new PLCConnection("PLCConfig");
                myConn.Connect();
#endif
                do
                {
                    try
                    {
                        Thread.Sleep(3000);
                        LeggeStatoProduzione();
                        AvviaProduzione();
                    }
                    catch (Exception ex)
                    {
                        unitOfWork.DropChanges();
                        System.Console.WriteLine(ex.Message);
                    }
                }
                while (true);

        }

        private void LogMessage(string format,params object[] args)
        {
            LogMessage(string.Format(format,args));
        }

        private void LogMessage(string message)
        {
            System.Console.WriteLine("{0:g} - {1}", System.DateTime.Now, message);
        }

        private bool AvviaProduzione()
        {

            try
            {

                numeroOdp = ReadDBW(51 , 44);         //OdP in corso

                if (numeroOdp != 0)
                    return false;

                if (ReadDBW(39, 20) == 0)
                    return false;     //PC non abilitato a scrivere su PLC

                odl = unitOfWork.FindObject<OdlDosaggio>(CriteriaOperator.Parse("Stato = ? OR Stato = ?", StatoOdL.Avviato, StatoOdL.InEsecuzione));
                if (odl == null)
                    return false;

                bRunning = true;
                
                idOdl = odl.Oid;
                odp = odl.OrdineProduzione;
                numeroOdp = odp.NumeroOrdine;

                currMisc = odl.NumeroMiscelateEseguite + 1;

                LogMessage(@"Avvio Produzione nr. {0} - {1} ""{2}""", numeroOdp, odl.Articolo.Codice, odl.Articolo.Descrizione);
                
                bool bAdditivi = odl.IngredientiTeorici.Any<Componente>(c =>c.Modalità != null && c.Modalità.Codice == "ADD");
                pDos = unitOfWork.FindObject<ParametriDosaggio>(new BinaryOperator("Odl", odl));

                for (int iBil = 0; iBil < cNRBIL;iBil++ )
                {
                    Bilancia bil = Bilancia(iBil);
                    if (bil == null)
                        continue;
                    short Qtà = 0;
                    short iSilos = 0;
                    lStart[iBil] = false;
                    sPeso[iBil] = 0;
                    iProgr[iBil] = 1;
                    sEstratto[iBil] = 0;

                    IList<Lotto> ingr = odl.Ingredienti.Where(c => c.Silos != null && c.ApparatoLavorazione == bil && c.NrMisc == currMisc).ToList();
                    //odl.Ingredienti.Filter = CriteriaOperator.Parse("ApparatoLavorazione=? AND NrMisc=? AND Silos IS NOT NULL", bil,currMisc);  
                    //odl.Ingredienti.Sorting.Add(new SortProperty("QtàTeo",DevExpress.Xpo.DB.SortingDirection.Descending));
                    for (int j = 0; j < cNRINGR; j++)
                    {
                        if (ingr.Count <= j)
                        {
                            Qtà = 0;
                            iSilos = 0;
                        }
                        else
                        {
                            Lotto c = ingr[j];
                            Qtà = (short)c.QtàTeo;
                            if (bil.kMult != 0)
                                Qtà = (short) (c.QtàTeo * bil.kMult);
                            iSilos = (short)((Apparato)c.Silos).Numero;
                            lStart[iBil] = true;
                        }
                        WriteDBW(51 + iBil, 102 + j * 4, iSilos);
                        WriteDBW(51 + iBil, 104 + j * 4, Qtà);

                    }
                    odl.Ingredienti.Filter = null;
                    if (lStart[iBil])          
                    {
                        lotto = odl.Prodotti.FirstOrDefault<Lotto>(p => p.NrMisc == currMisc && p.NrComp == 1);
                        idCompProd = lotto.Oid;
                        WriteDBW(51 + iBil, 44, odp.NumeroOrdine);         //OdP
                        //
                        WriteDBW(51 + iBil, 46, (short)odl.NumeroMiscelate);
                        int codiceFormula;
                        if (odl.Formula == null || false == int.TryParse(odl.Formula.Codice,out codiceFormula))
                            codiceFormula = 9999;
                        WriteDBD(51 + iBil, 48, codiceFormula);     //Double Word (32bit)
                    }

                }

                if (pDos != null)
                {
                    WriteDBB(39, 30, (byte)(pDos.GranMenù ? 1 : 0));
                    WriteDBB(39, 31, (byte)(odl.ApparatoLavorazione.Numero));
                    WriteDBX(39, 38, 2, pDos.DS1);
                }

                WriteDBX(39, 38,1, bAdditivi);

                string sName = odl.Articolo.Descrizione;
                sName = sName.PadRight(24).Substring(0,24);

                WriteString(39, 102, 24, sName);

                double fEnable = 0;
                qtàTot = 0F;
                for (int iBil = 0; iBil <cNRBIL; iBil++)
                {
                    WriteDBW(109, (iBil + 1) * 10 + 4, 0);                 //Flag reset
                    WriteDBW(109, (iBil + 1) * 10,(short) (lStart[iBil] ? 1 : 0));   //Flag avvio dosaggio bilancia
                    WriteDBW(109, (iBil + 1) * 10 + 2, 0);                 //Flag fine dosaggio
                    if (lStart[iBil])
                        fEnable = fEnable + Math.Pow(2,iBil);
                }
                short flag = System.Convert.ToInt16(fEnable);
                WriteDBW(109, 4, flag);             //Bilance abilitate
                WriteDBW(109, 2, 1);                   //EOT Avvia dosaggio
                
                lotto.Stato = StatoLotto.InEsecuzione;
                odl.Stato = StatoOdL.InEsecuzione;

                unitOfWork.CommitChanges();
                
                return true;
            }
            catch
            {
                unitOfWork.DropChanges();
                bRunning = false;
                return false;
            }

        }

        private void LeggeStatoProduzione()
        {
            bool inCorso = false;
            bool fineLotto = true;

            try
            {
                odl = null;
                numeroOdp = ReadDBW(51, 44);
                odp = unitOfWork.FindObject<OrdineProduzione>(new BinaryOperator("NumeroOrdine", numeroOdp));
                if (odp != null)
                    odl = odp.Odls.FirstOrDefault(o => o.Lavorazione != null && o.Lavorazione.Codice == OdlDosaggio.IDLavorazione);
                else
                    numeroOdp = 0;

                do
                {
                    if (odl == null)
                        break;

                    idOdl = odl.Oid;
                    currMisc = odl.NumeroMiscelateEseguite + 1;
                    lotto = odl.Prodotti.FirstOrDefault(c => c.NrMisc == currMisc);
                    if (lotto == null)
                        break;

                } while (false);

                for (int iBil = 0; iBil < cNRBIL; iBil++)
                {
                    Bilancia bil = Bilancia(iBil);
                    if (bil == null)
                        continue;

                    int db = 51 + iBil;
                    lStart[iBil] = (ReadDBW(109, (iBil + 1) * 10) == 1);                      //DB109.DW10        //Avvio dosaggio
                    iStato[iBil] = (ReadDBW(109, (iBil + 1) * 10 + 2) != 0);             //DB109.DW12        //Fine Dosaggio

                    sPeso[iBil] = ReadDBW(db, 2);                                        //DB51.DW2          //Peso sulla bilancia
                    if (bil.kMult != 0)
                        sPeso[iBil] = sPeso[iBil] / bil.kMult;

                    iProgr[iBil] = ReadDBW(db, 8);                                       //DB51.DW8          //Progressivo ingrediente
                    sEstratto[iBil] = ReadDBW(db, 32);                                   //DB51.DW32         //Peso estratto
                    if (bil.kMult != 0)
                        sEstratto[iBil] = sEstratto[iBil] / bil.kMult;

                    if (numeroOdp == 0)
                        continue;

                    if (iStato[iBil] || bAbort)
                    {
                        inCorso = true;
                        for (int j = 0; j < cNRINGR; j++)
                        {
                            float qtà = System.Convert.ToSingle(ReadDBW(db, 202 + j * 2));
                            int silos = ReadDBW(db, 102 + j * 4);       // Silos da cui ho estratto
                            if (bil.kMult != 0)
                                qtà = qtà / bil.kMult;
                            Lotto lottoIngrediente = odl.Ingredienti.FirstOrDefault(c => c.NrMisc == currMisc && c.Silos != null && c.ApparatoLavorazione != null && c.ApparatoLavorazione.Numero == bil.Numero && c.Silos.Numero == silos);
                            if (lottoIngrediente != null && lottoIngrediente.Stato != StatoLotto.Eseguito)
                            {
                                qtàTot += qtà;
                                lottoIngrediente.Stato = StatoLotto.Eseguito;
                                lottoIngrediente.Quantità = qtà;
                                LogMessage(@"Fine dosaggio da bilancia {0} - Ordine di produzione nr. {1} - Silos {2} {3} ""{4}"" Quantità Teorica {5:n3} Quantità estratta {6:n3}", bil.Codice,numeroOdp,lottoIngrediente.Silos.Codice,lottoIngrediente.Articolo.Codice,lottoIngrediente.Articolo.Descrizione,lottoIngrediente.QtàTeo,lottoIngrediente.Quantità);
                            }
                        }

                    }
                    else if (lStart[iBil])
                        fineLotto = false;
                }

                if (inCorso && fineLotto)
                {
                    if (lotto.Stato != StatoLotto.Eseguito)
                    {
                        lotto.Quantità = qtàTot;
                        lotto.Stato = StatoLotto.Eseguito;
                        odl.QuantitàEffettiva += qtàTot;
                        odl.NumeroMiscelateEseguite++;
                        LogMessage(@"Fine miscelata nr {0} di {1} - Ordine di produzione nr. {2}",currMisc,odl.NumeroMiscelate, numeroOdp);
                        currMisc++;
                    }

                    if (currMisc <= odl.NumeroMiscelate && !bAbort)
                    {
                        double fEnable = 0;
                        qtàTot = 0F;
                        for (int iBil = 0; iBil < cNRBIL; iBil++)
                        {
                            Bilancia bil = Bilancia(iBil);
                            if (bil != null)
                            {
                                Lotto comp = odl.Ingredienti.FirstOrDefault<Lotto>(c => c.NrMisc == currMisc && c.ApparatoLavorazione != null && c.ApparatoLavorazione.Numero == Bilancia(iBil).Numero);
                                if (comp != null)
                                {
                                    lotto = odl.Prodotti.FirstOrDefault<Lotto>(p => p.NrMisc == currMisc && p.NrComp == 1);
                                    lotto.Stato = StatoLotto.InEsecuzione;
                                    idCompProd = lotto.Oid;
                                    WriteDBW(109, (iBil + 1) * 10 + 4, 0);                 //Flag reset
                                    WriteDBW(109, (iBil + 1) * 10, 1);                      //Flag avvio dosaggio bilancia
                                    WriteDBW(109, (iBil + 1) * 10 + 2, 0);                 //Flag fine dosaggio
                                    fEnable = fEnable + Math.Pow(2, iBil);
                                }
                            }
                        }
                        short flag = System.Convert.ToInt16(fEnable);
                        WriteDBW(109, 4, flag);             //Abilitazione bilance
                        WriteDBW(109, 2, 1);                   //EOT Avvia dosaggio
                        LogMessage(@"Avvio miscelata nr {0} di {1} - Ordine di produzione nr. {2}", currMisc, odl.NumeroMiscelate, numeroOdp);
                    }
                    else
                    {
                        odl.Stato = bAbort ? StatoOdL.Annullato : StatoOdL.Eseguito;
                        WriteDBW(51, 44, 0);
                        LogMessage(@"Fine Ordine di produzione nr. {1} - Quantità Totale {1:n0}", numeroOdp,odl.QuantitàEffettiva);
                        bRunning = false;
                    }
                }
                unitOfWork.CommitChanges();
            }
            catch
            {
                unitOfWork.DropChanges();
            }

        }

        private Bilancia Bilancia(int index)
        {
            return bilance.FirstOrDefault<Bilancia>(b => b.Numero == index+1);
        }

        private PLCTag GetPLCTagDBB(int db, int dbb)
        {
            string key = string.Format("DB{0}.DBB{1}", db, dbb);
            return GetPLCTag(key);
        }

        private PLCTag GetPLCTagDBD(int db, int dd)
        {
            string key = string.Format("DB{0}.DBD{1}", db, dd);
            return GetPLCTag(key);
        }

        private PLCTag GetPLCTagDBW(int db, int dw)
        {
            string key = string.Format("DB{0}.DBW{1}", db, dw);
            return GetPLCTag(key);
        }

        private PLCTag GetPLCTagDBX(int db, int dw,int dx)
        {
            string key = string.Format("DB{0}.DBX{1}.{2}", db, dw, dx);
            return GetPLCTag(key);
        }

        private PLCTag GetPLCTagArray(int db, int dw, int len)
        {
            string key = string.Format("P#DB{0}.DBX{1}.{2} BYTE {3}", db, dw, 0, len);
            return GetPLCTag(key);
        }

        private PLCTag GetPLCTag(string key)
        {
            
            if (wordList.ContainsKey(key) == false)
            {
                wordList.Add(key, new PLCTag(key));
            }

            PLCTag tag = wordList[key];

            return tag;

        }

        private bool ReadDBX(int db, int dw, int dx)
        {
            PLCTag tag = GetPLCTagDBX(db, dw, dx);
            ReadTag(tag);
            if (tag == null)
                return false;
            return (bool)tag.Value;
        }

        private void WriteDBX(int db, int dw, int dx, bool val)
        {
            PLCTag tag = GetPLCTagDBX(db, dw, dx);
            tag.Value = val;
            WriteTag(tag);
        }

        private short ReadDBW(int db, int dw)
        {
            PLCTag tag = GetPLCTagDBW(db, dw);
            ReadTag(tag);
            try
            {
                ushort res = (tag.Value == null ? (ushort)0 : System.Convert.ToUInt16(tag.Value));
                return (short)(res <= short.MaxValue ? res : res - UInt16.MaxValue); 
            }
            catch
            {
                return 0;
            }
        }

        private void WriteDBW(int db, int dw, short shortValue)
        {
            PLCTag tag = GetPLCTagDBW(db, dw);
            //short shortValue = System.Convert.ToInt16(value);
            tag.Value = (ushort)(shortValue >= 0 ? shortValue : UInt16.MaxValue + shortValue);
            WriteTag(tag);
        }

        private int ReadDBD(int db, int dd)
        {
            PLCTag tag = GetPLCTagDBD(db, dd);
            ReadTag(tag);
            try
            {
                return (tag.Value == null ? (int)0 : System.Convert.ToInt32(tag.Value));
            }
            catch
            {
                return 0;
            }
        }

        private void WriteDBD(int db, int dd, int value)
        {
            PLCTag tag = GetPLCTagDBD(db, dd);
            tag.Value = value;
            WriteTag(tag);
        }

        private byte ReadDBB(int db, int dbb)
        {
            PLCTag tag = GetPLCTagDBB(db, db);
            ReadTag(tag);
            try
            {
                return (tag.Value == null ? (byte)0 : System.Convert.ToByte(tag.Value));
            }
            catch
            {
                return 0;
            }
        }

        private void WriteDBB(int db, int dbb, byte value)
        {
            PLCTag tag = GetPLCTagDBB(db, dbb);
            tag.Value = value;
            WriteTag(tag);
            
        }

        private byte[] ReadArray(int db, int dw, int len)
        {
            PLCTag tag = GetPLCTagArray(db, dw, len);
            ReadTag(tag);
            return (byte[]) tag.Value;
        }

        private void WriteArray(int db,int dw, int len, byte[] data)
        {
            PLCTag tag = GetPLCTagArray(db, dw, len);
            tag.Controlvalue = data;
            WriteTag(tag);
        }

        private string ReadString(int db, int dw, int len)
        {
            PLCTag tag = GetPLCTagArray(db, dw, len);
            ReadTag(tag);
            return tag.ValueAsString;
        }

        private void WriteString(int db, int dw, int len, string data)
        {
            PLCTag tag = GetPLCTagArray(db, dw, len);
            tag.Value = data;
            WriteTag(tag);
        }

        private void WriteTag(PLCTag tag)
        {
            #if !SIMUL
            try
            {
                myConn.WriteValue(tag);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            #endif
        }

        private void ReadTag(PLCTag tag)
        {
            #if !SIMUL
            try
            {
                myConn.ReadValue(tag);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endif
        }
     


    }
}