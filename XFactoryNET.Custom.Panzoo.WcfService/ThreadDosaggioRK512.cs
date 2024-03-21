using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using DevExpress.Xpo;
using System.Threading;
using System.Drawing;
using XFactoryNET.Module.BusinessObjects;
using XFactoryNET.Custom.Panzoo.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using ISISoft.Siemens.Library;

namespace XFactoryNET.Custom.Panzoo.WcfService
{
    public class ThreadDosaggio : IDisposable
    {

        public static ThreadDosaggio threadDosaggio;

        private static Thread thread;
        private RK512 rk512;
        private static Session session;
        private static Queue<int> msgQueue = new Queue<int>();

        public void Dispose()
        {
            if (rk512 != null)
            {
                rk512.Dispose();
                rk512 = null;
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

        public void Run()
        {
            try
            {
                rk512 = new RK512();
                rk512.OpenPort(Properties.Settings.Default.CommSettings);

                do
                {
                    Thread.Sleep(2000);
                    lock (msgQueue)
                    {
                        if (msgQueue.Count > 0)
                        {
                            int oid = msgQueue.Peek();
                            if (AvviaProduzione(oid))
                                msgQueue.Dequeue();
                        }
                    }
                    LeggeStatoProduzione();
                }
                while (true);
            }
            finally
            {
                rk512.ClosePort();
                rk512.Dispose();
                rk512 = null;
            }

        }

        const int cNRINGR = 20;
        const int cNRBIL = 5;

        bool[] lStart = new bool[cNRBIL];                   //Bilancia in dosaggio
        bool[] bFine = new bool[cNRBIL];                    //Fine dosaggio su bilancia
        bool[] iStato = new bool[cNRBIL];
        float[] sPeso = new float[cNRBIL];                  //Peso sulla bilancia
        float[] sEstratto = new float[cNRBIL];              //Peso estratto su bilancia
        int[] iProgr = new int[cNRBIL];                     //Indice ingrediente in estrazione;
        


        private bool AvviaProduzione(int oid)
        {
            try
            {
                if (rk512.ReadDW(39, 20) == 0)
                    return false;     //PC non abilitato a scrivere su PLC

                int[] aData = new int[2 * cNRINGR];
                int fEnable = 0;

                OdlDosaggio odl = session.GetObjectByKey<OdlDosaggio>(oid);
                ParametriDosaggio pDos = session.FindObject<ParametriDosaggio>(new BinaryOperator("Odl", odl));
                bool bAdditivi = odl.Ingredienti.Any<Componente>(c => c.Modalità.Codice == "ADD");

                XPCollection<Bilancia> bilance = new XPCollection<Bilancia>(session);

                for (int iBil = 0; iBil < cNRBIL;iBil++ )
                {
                    int Qtà = 0;
                    int iSilos = 0;
                    lStart[iBil] = false;
                    sPeso[iBil] = 0;
                    iProgr[iBil] = 1;
                    sEstratto[iBil] = 0;
                    Bilancia bil = bilance.First<Bilancia>(b => b.Numero == iBil);
                    odl.Ingredienti.Filter = new BinaryOperator("ApparatoLavorazione", bil);
                    for (int j = 0; j < cNRINGR; j++)
                    {
                        if (odl.Ingredienti.Count < j)
                        {
                            Qtà = 0;
                            iSilos = 0;
                        }
                        else
                        {
                            bFine[iBil] = false;
                            fEnable = fEnable | (2 ^ iBil);
                            Componente c = odl.Ingredienti[j];
                            Qtà = (int) (c.QtàTeo / bil.kMult);
                            iSilos = ((Apparato)c.AreaStoccaggio).Numero;
                            lStart[iBil] = true;
                            
                        }
                        aData[j * 2] = iSilos;
                        aData[j * 2 + 1] = Qtà;

                    }
                    if (lStart[iBil])
                    {

                        rk512.RK512Put(RK512.rkEnum.Data, 102 + iBil, 51, cNRINGR * 2, aData);
                        rk512.WriteDW(51 + iBil, 44, odl.Oid);
                        rk512.WriteDW(51 + iBil, 46, odl.NumeroLotti);
                        int codiceFormula;
                        if (false == int.TryParse(odl.Formula.Codice,out codiceFormula))
                            codiceFormula = 9999;
                        rk512.WriteDW(51 + iBil, 48, codiceFormula);
                    }

                }

                int flag = pDos.GranMenù ? 0x100 : 0x00;
                flag = flag | (pDos.MIX2 ? 0x01 : 0x00);
                rk512.WriteDW(39, 30, flag);

                flag = bAdditivi ? 0x02 : 0x00;
                flag = flag | (pDos.DS1 ? 0x04 : 0x00);
                rk512.WriteDW(39, 38, flag);

                string sName = odl.Articolo.Descrizione;
                sName = sName.PadRight(24).Substring(0,24);
                byte[] bData = new byte[24];
                System.Text.Encoding.UTF8.GetBytes(sName, 0, 24,bData, 0);
                int[] sData = new int[12];
                for (int i = 0; i < 12; i++)
                {
                    sData[i] = bData[i*2] * 256 + bData[i*2 + 1];
                }
                rk512.RK512Put(RK512.rkEnum.Data, 39, 102, 12, sData);
                for (int iBil = 0; iBil < cNRBIL; iBil++)
                {
                    rk512.WriteDW(109, (iBil + 1) * 20 + 8, 0);                 //Flag reset
                    rk512.WriteDW(109, (iBil + 1) * 20,lStart[iBil] ? 1 : 0);   //Flag avvio dosaggio bilancia
                    rk512.WriteDW(109, (iBil + 1) * 20 + 4, 0);                 //Flag fine dosaggio


                }
                rk512.WriteDW(109, 4, fEnable);             //Bilance abilitate
                rk512.WriteDW(109, 2, 1);                   //EOT Avvia dosaggio

                return true;
            }
            catch
            {
                return false;
            }

        }

        private void LeggeStatoProduzione()
        {
            for (int iBil = 0; iBil < cNRBIL; iBil++)
            {
                if (lStart[iBil] && !bFine[iBil])
                {
                    iStato[iBil] = (rk512.ReadDW(109, (iBil + 1) * 20 + 4) != 0);
                    sPeso[iBil] = rk512.ReadDW(51 + iBil, 2);

                }

            }
        }
    }
}