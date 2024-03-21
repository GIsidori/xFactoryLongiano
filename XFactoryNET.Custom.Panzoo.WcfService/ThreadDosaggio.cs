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

namespace XFactoryNET.Custom.Panzoo.WcfService
{
    public class ThreadDosaggio : IDisposable
    {

        public static ThreadDosaggio threadDosaggio;

        private static Thread thread;
        private PLCConnection myConn;

        Dictionary<string, PLCTag> wordList = new Dictionary<string, PLCTag>();
        Dictionary<string, List<PLCTag>> buffList = new Dictionary<string, List<PLCTag>>();
        PLCTag tagNomeFormula = null;

        private static Session session;
        private static Queue<int> msgQueue = new Queue<int>();

        private bool bAbort = false;
        private int iStopMisc = 0;  //Miscelata a cui stoppare
        private int currMisc = 0;   //Miscelata corrente
        private bool bRunning = false;

        bool[] lStart = new bool[cNRBIL];                   //Bilancia in dosaggio
        bool[] iStato = new bool[cNRBIL];
        float[] sPeso = new float[cNRBIL];                  //Peso sulla bilancia
        float[] sEstratto = new float[cNRBIL];              //Peso estratto su bilancia
        int[] iProgr = new int[cNRBIL];                     //Indice ingrediente in estrazione;
        Odl odl;
        Lotto lotto;
        ParametriDosaggio pDos;
        const int cNRINGR = 20;
        const int cNRBIL = 5;

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
            string connectionString = null;
            if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }

            session = new Session() { ConnectionString = connectionString };
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
            lock(msgQueue)
                msgQueue.Enqueue(oid);
        }

        public void Abort()
        {
            bAbort = true;
        }

        public void StopMisc(int nrMisc)
        {
            iStopMisc = nrMisc;
        }

        public int IdOdl()
        {
            return odl == null ? 0 : odl.Oid;
        }

        public int CurrMisc()
        {
            return currMisc;
        }

        public int NrMisc()
        {
            return odl == null ? 0 : odl.NumeroLotti;
        }

        public bool Running()
        {
            return bRunning;
        }

        public float GetPeso(int iBil)
        {
            return this.sPeso[iBil];
        }

        public string Silos(int iBil)
        {
            if (this.odl == null)
                return null;
            int iIngr = NrIngrediente(iBil);
            if (this.odl.Ingredienti.Count < iIngr)
                return null;
            if (odl.Ingredienti[iIngr].AreaStoccaggio == null)
                return null;
            return this.odl.Ingredienti[iIngr].AreaStoccaggio.Codice;

        }

        public float QtàEstratta(int iBil)
        {
            return sEstratto[iBil];
        }

        public int NrIngrediente(int iBil)
        {
            return iProgr[iBil];
        }

        public float QtàTeorica(int iBil)
        {
            if (this.odl == null)
                return 0;
            int iIngr = NrIngrediente(iBil);
            if (this.odl.Ingredienti.Count < iIngr)
                return 0;
            return this.odl.Ingredienti[iIngr].QtàTeo;
        }

        public bool StatoBilancia(int iBil)
        {
            return iStato[iBil];
        }

        public void Run()
        {
            try
            {

                bilance = new XPCollection<Bilancia>(session,true);
                
                myConn = new PLCConnection("PLCConfig");

                myConn.Connect();

                do
                {
                    Thread.Sleep(2000);
                    lock (msgQueue)
                    {
                        LeggeStatoProduzione();
                        if (msgQueue.Count > 0)
                        {
                            int oid = msgQueue.Peek();
                            if (AvviaProduzione(oid))
                                msgQueue.Dequeue();
                        }
                    }
                }
                while (true);
            }
            finally
            {
                myConn.Disconnect();
            }
        }

        private bool AvviaProduzione(int oid)
        {

            try
            {

                if (ReadDW(39, 20) == 0)
                    return false;     //PC non abilitato a scrivere su PLC

                bRunning = true;

                //int[] aData = new int[2 * cNRINGR];
                int fEnable = 0;

                odl = session.GetObjectByKey<OdlDosaggio>(oid);
                odl.Stato = StatoOdL.InEsecuzione;
                odl.Save();

                pDos = session.FindObject<ParametriDosaggio>(new BinaryOperator("Odl", odl));
                bool bAdditivi = odl.IngredientiTeorici.Any<Componente>(c => c.Modalità.Codice == "ADD");

                iStopMisc = odl.NumeroLotti;
                for (int iBil = 0; iBil < cNRBIL;iBil++ )
                {
                    int Qtà = 0;
                    int iSilos = 0;
                    lStart[iBil] = false;
                    sPeso[iBil] = 0;
                    iProgr[iBil] = 1;
                    sEstratto[iBil] = 0;
                    Bilancia bil = Bilancia(iBil + 1);
                    odl.Ingredienti.Filter = CriteriaOperator.Parse("ApparatoLavorazione=? AND NrLotto=?", bil,1);  
                    for (int j = 0; j < cNRINGR; j++)
                    {
                        if (odl.Ingredienti.Count <= j)
                        {
                            Qtà = 0;
                            iSilos = 0;
                        }
                        else
                        {
                            fEnable = fEnable | (2 ^ iBil);
                            Componente c = odl.Ingredienti[j];
                            Qtà = (int)c.QtàTeo;
                            if (bil.kMult != 0)
                                Qtà = (int) (c.QtàTeo * bil.kMult);
                            iSilos = ((Apparato)c.AreaStoccaggio).Numero;
                            lStart[iBil] = true;
                            
                        }
                        WriteDBW(51 + iBil, 102 + j * 4, iSilos);
                        WriteDBW(51 + iBil, 104 + j * 4, Qtà);

                    }
                    if (lStart[iBil])          
                    {
                        //WriteBuff(51 + iBil, 102, cNRINGR * 2, aData);
                        Componente prod = odl.Prodotti.FirstOrDefault<Componente>(p => p.NrLotto == 1 && p.NrComp == 0);
                        lotto = prod.Lotto;         //Lotto in produzione
                        WriteDBW(51 + iBil, 44, lotto.Oid);         //Oid lotto prodotto
                        WriteDBW(51 + iBil, 46, odl.NumeroLotti);
                        int codiceFormula;
                        if (odl.Formula == null || false == int.TryParse(odl.Formula.Codice,out codiceFormula))
                            codiceFormula = 9999;
                        WriteDBW(51 + iBil, 48, codiceFormula);
                    }

                }

                //int flag = pDos.GranMenù ? 0x100 : 0x00;
                //flag = flag | (pDos.MIX2 ? 0x01 : 0x00);
                //WriteDW(39, 30, flag);
                WriteDBB(39, 30, (byte)(pDos.GranMenù ? 1 : 0));
                WriteDBB(39, 31, (byte) (pDos.MIX2 ? 1 : 0));

                //byte flag = (byte) (bAdditivi ? 0x02 : 0x00);
                //flag = (byte) (flag | (pDos.DS1 ? 0x04 : 0x00));
                //WriteDW(39, 38, flag);
                WriteDBX(39, 38,1, bAdditivi);
                WriteDBX(39, 38, 2, pDos.DS1);

                string sName = odl.Articolo.Descrizione;
                sName = sName.PadRight(24).Substring(0,24);

                WriteString(39, 102, 24, sName);

                for (int iBil = 0; iBil < cNRBIL; iBil++)
                {
                    WriteDBW(109, (iBil + 1) * 20 + 8, 0);                 //Flag reset
                    WriteDBW(109, (iBil + 1) * 20,lStart[iBil] ? 1 : 0);   //Flag avvio dosaggio bilancia
                    WriteDBW(109, (iBil + 1) * 20 + 4, 0);                 //Flag fine dosaggio
                }

                WriteDBW(109, 4, fEnable);             //Bilance abilitate
                WriteDBW(109, 2, 1);                   //EOT Avvia dosaggio

                return true;
            }
            catch
            {
                bRunning = false;
                return false;
            }

        }

        private void LeggeStatoProduzione()
        {
            bool fineLotto = true;
            bool inCorso = false;
            session.BeginTransaction();
            for (int iBil = 0; iBil < cNRBIL; iBil++)
            {
                int db=51+iBil;
                lStart[iBil] = (ReadDW(109,(iBil+1)*20) == 1);

                int oidLotto = ReadDW(51, 44);
                lotto = session.GetObjectByKey<Lotto>(oidLotto);      //Lotto in produzione
                if (lotto == null)
                    break;

                inCorso = true;
                odl = lotto.Componente.Odl;
                currMisc = lotto.Componente.NrLotto;

                iStato[iBil] = (ReadDW(109, (iBil + 1) * 20 + 4) != 0);
                sPeso[iBil] = ReadDW(db, 2);
                iProgr[iBil] = ReadDW(db, 8);
                sEstratto[iBil] = ReadDW(db, 32);

                if ((lStart[iBil] && iStato[iBil]) || (bAbort))
                {

                    //int[] data = new int[cNRINGR * 20];
                    //ReadBuff(db, 202, cNRINGR, data);
                    for (int j = 0; j < cNRINGR; j++)
                    {
                        Bilancia bil = Bilancia(iBil + 1);
                        float qtà = System.Convert.ToSingle(ReadDW(db, 202 + j*2));
                        //float qtà = data[j];
                        if (bil.kMult != 0)
                            qtà = qtà / bil.kMult;
                        Componente comp = odl.Ingredienti.FirstOrDefault<Componente>(c => c.NrLotto == currMisc && ((Silos)c.AreaStoccaggio).Numero == j + 1);
                        if (comp != null)
                        {
                            comp.Qtà = qtà;
                            comp.Lotto.Quantità -= qtà;
                            comp.Save();
                        }
                    }

                }
                else if (lStart[iBil])
                    fineLotto = false;
            }   //for iBil

            session.CommitTransaction();

            if (inCorso && fineLotto)
            {
                session.BeginTransaction();
                float qtàTot = odl.Ingredienti.Where<Componente>(c => c.NrLotto == currMisc).Sum<Componente>(c => c.Qtà);
                lotto.Quantità += qtàTot;
                lotto.Componente.Qtà = qtàTot;
                odl.NumeroLottiEseguiti++;
                currMisc++;
                odl.Save();
                lotto.Save();

                if (currMisc <= odl.NumeroLotti && currMisc <= iStopMisc && !bAbort)
                {
                    for (int iBil = 0; iBil < cNRBIL; iBil++)
                    {
                        if (lStart[iBil])
                        {
                            Componente prod = odl.Prodotti.FirstOrDefault<Componente>(p => p.NrLotto == currMisc && p.NrComp == 0);
                            lotto = prod.Lotto;         //Lotto in produzione
                            WriteDBW(51 + iBil, 44, lotto.Oid);         //Oid lotto prodotto

                            WriteDBW(109, (iBil + 1) * 20 + 8, 0);                 //Flag reset
                            WriteDBW(109, (iBil + 1) * 20, lStart[iBil] ? 1 : 0);   //Flag avvio dosaggio bilancia
                            WriteDBW(109, (iBil + 1) * 20 + 4, 0);                 //Flag fine dosaggio
                        }
                    }
                    WriteDBW(109, 2, 1);                   //EOT Avvia dosaggio
                }
                else
                {
                    odl.Stato = StatoOdL.Eseguito;
                    odl.Save();
                    bRunning = false;
                }
                session.CommitTransaction();
            }

        }

        private Bilancia Bilancia(int index)
        {
            return bilance.FirstOrDefault<Bilancia>(b => b.Numero == index);
        }

        private PLCTag GetPLCTagDB(int db, int dbb)
        {
            string key = string.Format("DB{0}.DBB{1}", db, dbb);
            return GetPLCTag(key);
        }

        private PLCTag GetPLCTagDW(int db, int dw)
        {
            string key = string.Format("DB{0}.DBW{1}", db, dw);
            return GetPLCTag(key);
        }

        private PLCTag GetPLCTagDX(int db, int dw,int dx)
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

        private bool? ReadDX(int db, int dw, int dx)
        {
            PLCTag tag = GetPLCTagDX(db, dw, dx);
            ReadTag(tag);
            return (bool?)tag.Value;
        }

        private void WriteDBX(int db, int dw, int dx, bool val)
        {
            PLCTag tag = GetPLCTagDX(db, dw, dx);
            tag.Value = val;
            WriteTag(tag);
        }

        private int ReadDW(int db, int dw)
        {
            PLCTag tag = GetPLCTagDW(db, dw);
            ReadTag(tag);
            return (tag.Value == null ? (ushort)0 : (ushort)tag.Value);
        }

        private void WriteDBW(int db, int dw, int value)
        {
            PLCTag tag = GetPLCTagDW(db, dw);
            ushort shortValue = System.Convert.ToUInt16(value);
            tag.Value = shortValue;
            WriteTag(tag);
        }

        private byte ReadDBB(int db, int dbb)
        {
            PLCTag tag = GetPLCTagDB(db, db);
            ReadTag(tag);
            return (tag.Value == null ? (byte)0 : (byte)tag.Value);
        }

        private void WriteDBB(int db, int dbb, byte value)
        {
            PLCTag tag = GetPLCTagDB(db, dbb);
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
            tag.ValueAsString = data;
            WriteTag(tag);
        }

        private void WriteTag(PLCTag tag)
        {
            #if !SIMUL
                myConn.WriteValue(tag);
            #endif
        }

        private void ReadTag(PLCTag tag)
        {
            #if !SIMUL
                myConn.ReadValue(tag);
            #endif
        }
     


    }
}