using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Threading;
using System.Runtime.InteropServices;
using System.Linq;

using OPC.Common;
using OPC.Data.Interface;
using OPC.Data;
using DevExpress.Xpo;
using XFactoryNET.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using XFactoryNET.Custom.MM1.Module.BusinessObjects;
using System.Collections.Generic;

namespace XFactoryNET.Custom.MM1.ServiceLibrary
{

	public struct CmdMsg
	{
		public string ID;
		public Cmd cmd;
		public object value;
	}

	public enum Cmd
	{
		ResetBil,
		AckBil,
		InitBil,
		InitSilos,
		SetManual,
		SetQt‡Man,
		SetSilosMan,
		StartMan,
		StopMan,
		SetSilosDest
	}

	/// <summary>
	/// Descrizione di riepilogo per ThreadDos.
	/// </summary>
	internal static class ThreadDos
	{

		private static Thread thread;

		static OpcServer		theSrv;
		static string OPCDevice;
		static string OPCServerProgID;
		static string OPCItemStringFormat;							//Formato OPCItem
		const int updateRate=0;				//tempo di update gruppo OPC

        static OpcGroup opcGrpCom;
		const int COMDB = 103;								//DataBloc Comunicazione con PLC
		const int COMDBSIZE=230;						//Dimensione
        static int[] ComHandles = new int[COMDBSIZE];
        static UInt16[] COMBUFF = new UInt16[COMDBSIZE + 1];				//Base 1

        static OpcGroup opcGrpCmd;
		const int CMDDB = 104;								//DataBloc Comandi al PLC
		const int CMDDBSIZE=230;						//Dimensione
        static int[] CmdHandles = new int[CMDDBSIZE];
        static UInt16[] CMDBUFF = new UInt16[CMDDBSIZE + 1];				//Base 1

		//OpcGroup opcGrpTag;
		const int TAGDB = 250;
		const int TAGDBSIZE=1;
		//int[,] TagItems = new int[,] {{250,9}}; 
        static int[] TagHandles = new int[TAGDBSIZE];
        static UInt16[] TAGBUFF = new UInt16[TAGDBSIZE + 1];				//Base 1

		#region DB Dati Silos

		const int ART1DB = 144;							//Codice Materiale in silos (HiWord)
		const int ART2DB = 145;							//Codice Materiale in silos (LowWord)
		const int TOLLDB = 146;							//Tolleranze silos
		const int BLOKDB = 147;							//Abilitazioni silos	
		const int GRMIDB = 148;							//Capacit‡ di dosaggio in grosso/medio/fine
		const int TDOSDB = 149;							//Tempo massimo di dosaggio

		const int SILOS_UPDATED_DW=7;

        static OpcGroup opcGrpSilos;
        static int[][] SilosHandles;
        static UInt16[][] SILOSBUFF;

        static int[] SILOSDB = new int[] { ART1DB, ART2DB, TOLLDB, BLOKDB, GRMIDB, TDOSDB };
        static int[] SILOSDBSIZE = new int[] { 254, 254, 254, 254, 254, 254 };

		#endregion

		#region DB Dati Bilance

        static OpcGroup opcGrpBil;
        const int BILDBSIZE = 12;
        static int[][] bilHandles;
        static UInt16[][] BILBUFF;

		#endregion

        public static Queue CmdQueue = new Queue();
		
		enum eComStatus
		{
			IDLE,
			WAITING
		};

        static eComStatus ComStatus = new eComStatus();

        static UnitOfWork uow;
        static XPCollection<Bilancia> bilance = null;
        static List<Silos> silosCollection = null;
        static Odl odl;
        static Lotto lotto;
        static int currMisc;

		public static void StartThread()
		{
			//threadDos = new ThreadDos();
			ThreadStart threadStart = new ThreadStart(ThreadDos.Run);
			thread = new Thread(threadStart);
			thread.Name="ThreadDos";
			thread.Start();			
		}

		public static void StopThread()
		{
			if (thread != null)
			{
				thread.Abort();
				thread.Join();
			}
		}

		public static void EnqCmd(Cmd cmd,string ID)
		{
			EnqCmd(cmd,ID,null);
		}

		public static void EnqCmd(Cmd cmd,string ID,object value)
		{
			CmdMsg msg = new CmdMsg();
			msg.cmd=cmd;
			msg.ID = ID;
			msg.value=value;
			CmdQueue.Enqueue(msg);
		}


		public static void Run()
		{
            OPCDevice = Properties.Settings.Default.OPCDeviceDos;
            OPCServerProgID = Properties.Settings.Default.OPCServerProgID;
            OPCItemStringFormat = Properties.Settings.Default.OPCItemStringFormat;

            //NameValueCollection values = (NameValueCollection)ConfigurationSettings.GetConfig( "appParams" ); 
            //if (values == null)
            //    throw new ConfigurationException("File di Configurazione non trovato");
					 
            //OPCDevice =values["OPCDeviceDos"];
            //if (OPCDevice== null)
            //    throw new ConfigurationException("OPCDeviceDos non configurato");

            //OPCServerProgID = values["OPCServerProgID"];
            //if (OPCServerProgID== null)
            //    throw new ConfigurationException("OPCServerProgID non configurato");

            //OPCItemStringFormat = values["OPCItemStringFormat"];
            //if (OPCItemStringFormat == null)
            //    throw new ConfigurationException("OPCItemStringFormat non configurato");

			theSrv = new OpcServer();
			theSrv.Connect(OPCServerProgID);

			Thread.Sleep(500);				// we are faster than some servers!

			opcGrpCom = theSrv.AddGroup("ComDB", true, updateRate);

			if (!AddOpcItems(opcGrpCom,COMDB,COMDBSIZE,ComHandles,1))
				return;

			opcGrpCom.SetEnable(true);
			opcGrpCom.Active=true;

			opcGrpCmd = theSrv.AddGroup("CmdDB",true,updateRate);
			if (!AddOpcItems(opcGrpCmd,CMDDB,CMDDBSIZE,CmdHandles,1))
				return;
			opcGrpCmd.SetEnable(true);
			opcGrpCmd.Active=true;

//			this.opcGrpTag = theSrv.AddGroup("TagDB",true,updateRate);
//			
//			if (!AddOpcItems(opcGrpTag,TAGDB,TAGDBSIZE,TagHandles,1))
//				return;
//			opcGrpTag.SetEnable(true);
//			opcGrpTag.Active=true;


            string connectionString = null;
            if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }

            try
            {
                uow = new UnitOfWork() { ConnectionString = connectionString };
                {


                    InitSilos();

                    InitBilance();

                    InitStadioLav();
                }
                uow.CommitChanges();
                uow.Dispose();
				//lock (this)
				{
					while (true)
					{
                        try
                        {
                            uow = new UnitOfWork() { ConnectionString = connectionString };
                            Monitor.Wait(uow,2000);																								//Ogni 2 sec.
                            PLCHelper.PLCReadSync(opcGrpCom, ComHandles, COMBUFF);			//Read Sync
                            CheckComDb();
                            uow.CommitChanges();
                        }
                        catch
                        {
                            uow.DropChanges();
                        }
                        finally
                        {
                            uow.Dispose();
                        }
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
					}
				}
			}
			catch (ThreadAbortException)
			{
				Thread.ResetAbort();
			}
			catch (Exception ex)
			{
				System.Console.WriteLine(ex.Message);
			}
			finally
			{
				opcGrpBil.Remove(true);
				opcGrpCmd.Remove(true);
				opcGrpCom.Remove(true);
				opcGrpSilos.Remove(true);
				theSrv.Disconnect();
			}

		}



        private static bool AddOpcItems(OpcGroup opcGrp, int DB, int DBSIZE, int[] Handles, int offset)
		{

			OPCItemDef[]	ItemDefs= new OPCItemDef[DBSIZE];

			// add items and save server handles
			for (int i =0;i<DBSIZE;i++)
			{
				ItemDefs[i] = new OPCItemDef(string.Format(OPCItemStringFormat,OPCDevice,DB,i+offset), true,i+offset-1, VarEnum.VT_EMPTY);
			}

			OPCItemResult[]	rItm;
			opcGrp.AddItems( ItemDefs, out rItm );
			if( rItm == null )
				return false;
			
			for (int i=0;i<DBSIZE;i++)
			{
				if( HRESULTS.Failed( rItm[i].Error ))
				{ 
					AlertProvider.AddAlert(string.Format("Errore connessione OPCGroup: {0} - OPCItem: DB{1}:DW{2}",opcGrp.Name,DB,i+offset),OdlDosaggio.IDLavorazione,AlertSeverity.Error);
					opcGrp.Remove( true ); 
					return false;
				}
				else
					Handles[i] = rItm[i].HandleServer;
			}
			
			return true;

		}


		#region Inizializzazione Stadi Lavorazione

        public static void InitStadioLav()
		{
		}
		#endregion

		#region Inizializzazione Silos

        private static void InitSilos()
		{

			opcGrpSilos = theSrv.AddGroup("OPCGroup:Silos", true, updateRate);

			SilosHandles = new int[SILOSDB.Length][];
			SILOSBUFF=new UInt16[SILOSDB.Length][];

			for (int j=0;j<SILOSDB.Length;j++)
			{
				SilosHandles[j] = new int[SILOSDBSIZE[j]];
				SILOSBUFF[j] = new UInt16[SILOSDBSIZE[j]+1];				//Base 1
				AddOpcItems(opcGrpSilos,SILOSDB[j],SILOSDBSIZE[j],SilosHandles[j],1);
			}


			int dw;
			UInt16 CodiceMateriale=0;
			UInt16 maxTDos=0;
			decimal toll;
			decimal factor;
			UInt16 Abilit=0;
			int gmf=0;

			bool bUpdate=false;

            silosCollection = new XPCollection<Silos>(uow).OrderBy(s => s.Numero).ToList();

			foreach (Silos silos in silosCollection)
			{
				try
				{
					if (string.IsNullOrEmpty(silos.Settings))
						continue;

					factor = 1;
					bUpdate=true;
					dw = int.Parse(silos.Settings);

					CodiceMateriale=0;
					if (silos.Articolo != null)
						CodiceMateriale=mCodiceMateriale(silos.Articolo.Codice);

                    if (silos.ApparatiDestinazione.Count == 0)
                        continue;

                    Bilancia bil = silos.ApparatiDestinazione[0] as Bilancia;
                    if (bil == null)
                        continue;


                    factor = bil.kMult;
					maxTDos = System.Convert.ToUInt16(bil.tMax*10/factor);				//Al PLC va trasmesso moltiplicato per 10

					toll = silos.Tolleranza;

					Abilit=0;
					if (!silos.CaricoAbilitato)
						Abilit |= 0x1;
					if (!(bool)silos.ScaricoAbilitato)
						Abilit |= 0x2;

					gmf = silos.Velocit‡Fine & 0x000F;
					gmf |= ((silos.Velocit‡Media & 0x000F) <<4);
					gmf |= ((silos.Velocit‡Grosso & 0x000F) <<8);

					SILOSBUFF[0][dw] = (UInt16) (CodiceMateriale>>16);
					SILOSBUFF[1][dw] = (UInt16) (CodiceMateriale & 0x0000FFFF);
					SILOSBUFF[2][dw] = System.Convert.ToUInt16(System.Math.Round(toll*factor));				//Tolleranza
					SILOSBUFF[3][dw] = Abilit;
					SILOSBUFF[4][dw] =(UInt16) gmf;
					SILOSBUFF[5][dw] =maxTDos;

				}
				catch (Exception ex)
				{
					System.Console.WriteLine(ex.Message);
				}
			}

			if (bUpdate)
			{
				PLCHelper.PLCWriteWordSync(opcGrpSilos,SilosHandles[0][0],2);

				for (int j=0;j< SILOSDB.Length;j++)
				{
					PLCHelper.PLCWriteSync(opcGrpSilos,SilosHandles[j],SILOSBUFF[j]);
				}


				COMBUFF[SILOS_UPDATED_DW]=1;
				PLCHelper.PLCWriteWordSync(opcGrpCom,ComHandles[SILOS_UPDATED_DW-1],1);

			}

		}


		#endregion

        private static UInt16 mCodiceMateriale(string IDMateriale)
		{

			//			char[] digits = new char[] {'0','1','2','3','5','6','7','8','9'};
			try
			{
				//				int i=0;
				//				do
				//				{
				//					i++;
				//				}while (IDMateriale.Substring(i,1).IndexOfAny(digits)!=-1);
				//
				//				return UInt16.Parse(IDMateriale.Substring(0,i));
				return UInt16.Parse(IDMateriale);
			}
			catch
			{
			}
			return 0;
		}

		#region Inizializzazione Bilance

        private static decimal[] pesoBil;
        private static void InitBilance()
		{
		
			int nrBil;
            bilance = new XPCollection<Bilancia>(uow);
			opcGrpBil = theSrv.AddGroup( "OPCGroup:DosPnt" , true, updateRate);

            bilHandles = new int[bilance.Count][];
            BILBUFF = new ushort[bilance.Count][];
            
            pesoBil = new decimal[bilance.Count];


			int i=0;

            foreach (Bilancia dr in bilance.OrderBy(b => b.Numero))
			{
				if (dr.Numero != 0)
				{
					bilHandles[i] = new int[BILDBSIZE];
					BILBUFF[i] = new UInt16[BILDBSIZE+1];				//Base 1
					nrBil = dr.Numero;
					AddOpcItems(opcGrpBil,10+nrBil,BILDBSIZE,bilHandles[i],1);
				}
				i++;
			}

			InitBilancia(0);

		}

        private static void InitBilancia(int nrBil)
		{

			int i=0;
            CriteriaOperator filter = null;
            if (nrBil != 0)
                filter = new BinaryOperator("Numero", nrBil);

			foreach (Bilancia dr in new XPCollection<Bilancia>(uow,filter,new SortProperty[] {new SortProperty("Numero",DevExpress.Xpo.DB.SortingDirection.Ascending)}))
			{
				if (dr.Numero != 0)
				{
					decimal kMult = dr.kMult;
					//TODO gestire OverflowException
					try
					{
						UInt16 uPesoMax = System.Convert.ToUInt16((decimal)dr.Quantit‡Max*kMult);
						BILBUFF[i][1] = System.Convert.ToUInt16(dr.TaraMax*kMult); 
						BILBUFF[i][2] = uPesoMax ;
						BILBUFF[i][3] = System.Convert.ToUInt16(1000 / kMult);
						BILBUFF[i][4] = System.Convert.ToUInt16(dr.MaxVar * kMult);
						BILBUFF[i][5] = System.Convert.ToUInt16(dr.tMin*kMult);
						long lMax = System.Convert.ToInt32((dr.tMax * kMult));
						BILBUFF[i][6] = (UInt16) ((UInt16) lMax & 0x0000FFFF);
						BILBUFF[i][7] = System.Convert.ToUInt16(dr.VoloMassimo * kMult);
						BILBUFF[i][8] = System.Convert.ToUInt16(dr.Precisione);
						BILBUFF[i][9] = uPesoMax;
						BILBUFF[i][10] = uPesoMax;
						BILBUFF[i][11] = uPesoMax;
						BILBUFF[i][12] = uPesoMax;
					}
					catch (Exception e)
					{
						AlertProvider.AddAlert(string.Format("Errore in inizializzazione bilancia nr {0}: {1}",nrBil,e.Message),OdlDosaggio.IDLavorazione,AlertSeverity.Error);
					}
					finally
					{
						PLCHelper.PLCWriteSync(opcGrpBil,bilHandles[i],BILBUFF[i]);
					}
				}
				i++;
			}
		}

		#endregion

		#region Comandi Dosaggio
        private static bool RichiestaDosaggio()
		{
			const int RECDB=108;								//DataBloc per trasmissione ricetta
			const int RECDBOFFSET=10;
			const int RECDBSIZE=51;

			OpcGroup theGrp;
			int[]  handles=new int[RECDBSIZE];
			UInt16[] DBBUFF = new UInt16[RECDBSIZE+1];				//Base 1

			bool bRiciclo=false;

            odl = uow.FindObject<OdlDosaggio>(PersistentCriteriaEvaluationBehavior.BeforeTransaction, CriteriaOperator.Parse("Stato = ? OR Stato = ?", StatoOdL.Avviato, StatoOdL.InEsecuzione));
            if (odl == null)
                return false;

            if (odl.NumeroMiscelate <= 0)
                return false;

            if (odl.NumeroMiscelateEseguite >= odl.NumeroMiscelate)
                return false;

            currMisc = odl.NumeroMiscelateEseguite+1;

			lotto = odl.Prodotti.FirstOrDefault(l=>l.NrMisc == currMisc && l.NrComp == 1);

            ParametriDosaggioMM1 drParam = uow.FindObject<ParametriDosaggioMM1>(new BinaryOperator("Odl", odl));

            bRiciclo = odl.ApparatoLavorazione.Codice == "RICICLO";

			if (bRiciclo)
			{
                throw new NotImplementedException("Riciclo");
				//new Factory().Trasporti.Avvia(drOdL.IDApparato,drOdL.IDSilosDest);
			}

			InitSilos();

			theGrp = theSrv.AddGroup("RecDB",true,updateRate);
			AddOpcItems(theGrp,RECDB,RECDBSIZE,handles,RECDBOFFSET+1);

			DBBUFF[1] = 1;			//Codice materiale di default
			try
			{
				DBBUFF[1]=mCodiceMateriale(odl.Formula.Codice);
				DBBUFF[2] = 0;		//Versione
			}
			catch (FormatException e)
			{
				AlertProvider.AddAlert(string.Format("RichiestaDosaggio CodiceMateriale '{0}' formato non valido: ({2})",odl.Formula, e.Message),OdlDosaggio.IDLavorazione,AlertSeverity.Error);
			}

			DBBUFF[3]=(UInt16) (odl.Oid>>16);
			DBBUFF[4]=(UInt16) (odl.Oid & 0xFFFF);

			DBBUFF[5]=System.Convert.ToUInt16(currMisc);		//Progressivo Cotta

			if (bRiciclo)
				DBBUFF[6] = 2000;					//Silos fittizio per indicare riciclo
			else
			{
				try
				{
					DBBUFF[6]=System.Convert.ToUInt16(odl.Destinazione.Numero);
				}
				catch (System.FormatException e)
				{
					AlertProvider.AddAlert(string.Format("Richiesta dosaggio Codice Silos '{0}' formato non valido: ({1})",odl.Destinazione,e.Message),OdlDosaggio.IDLavorazione,AlertSeverity.Error);
				}
			}

			//Trasmissione ParametriLavorazione della macinazione
			if (drParam != null && !bRiciclo)
			{
				try
				{
					DBBUFF[20]= System.Convert.ToUInt16(drParam.Rotazione175);
					if (drParam.TipoRete == TipoRete.Fine)
						DBBUFF[21] = 0x1;
					else
						DBBUFF[21] = 0x2;
					//DBBUFF[22] = 0;		//Reserved

					DBBUFF[22] = (ushort)(drParam.Velocit‡Coclea);

					if (drParam.TipoMolino == TipoMolino.MolinoRulli)
						DBBUFF[23] = 0x04;
					else
					{
						DBBUFF[23] = 0x00;
						if (drParam.TipoMolino == TipoMolino.Molino178)
							DBBUFF[23] |= 0x01;
						if (drParam.TipoMolino == TipoMolino.Molino179)
							DBBUFF[23] |= 0x02;
					}

					DBBUFF[24] = System.Convert.ToUInt16(drParam.TipoSetaccio);
					DBBUFF[25] = System.Convert.ToUInt16(drParam.TempoMiscelazione223);
					DBBUFF[26] = System.Convert.ToUInt16(drParam.TempoMiscelazione227);

					if (drParam.TipoMolino == TipoMolino.MolinoRulli)
					{
						if (drParam.AmpereMolino != 0)
							DBBUFF[27] = System.Convert.ToUInt16(drParam.AmpereMolino);
						DBBUFF[31]=0;
					}
					else
					{
						if (drParam.TipoMolino == TipoMolino.Molino178)
							DBBUFF[27] = System.Convert.ToUInt16(drParam.AmpereMolino);

                        if (drParam.TipoMolino == TipoMolino.Molino178)
                            DBBUFF[28] = System.Convert.ToUInt16(drParam.AmpereMolino);
                        //						if (!drParam.IsMol178Null())
//							DBBUFF[31] = (ushort)(drParam.Mol178?1:0);

					}

					DBBUFF[29] = System.Convert.ToUInt16(drParam.PesoSupportoMCD);

					//Trasmissione frequenza inverter (%)
					DBBUFF[31] = System.Convert.ToUInt16(drParam.PercInverter);
					
					DBBUFF[30] = (ushort)(drParam.Setaccio106?1:0);


				}
				catch (Exception ex)
				{	
					AlertProvider.AddAlert(string.Format("Errore in trasmissione parametri: {0}",ex.Message),OdlDosaggio.IDLavorazione,AlertSeverity.Error);
				}
			}

			PLCHelper.PLCWriteSync(theGrp,handles,DBBUFF);

			theGrp.Remove(false);

            lotto.Stato = StatoLotto.InEsecuzione;
            odl.Stato = StatoOdL.InEsecuzione;

            uow.CommitChanges();
		
			AlertProvider.AddAlert(string.Format("Richiesta dosaggio: NrOdL = {0}, CodiceFormula = {1}, NrBatch = {2}, SilosDest = {3}",odl.Oid,DBBUFF[1],DBBUFF[5],DBBUFF[6]),OdlDosaggio.IDLavorazione,AlertSeverity.Debug);

			return true;

		}

        private static void FineDosaggio()
		{

			UInt16 iStatus;
			int nrBatches;
			int IDOdL;
			int NrBatch;
			int iSilos;
			decimal Qt‡Tot;

			iStatus = COMBUFF[9];
			nrBatches = COMBUFF[16];
			IDOdL = (COMBUFF[18]<<16) + COMBUFF[19];
			NrBatch = COMBUFF[20];
			iSilos = COMBUFF[24];

			AlertProvider.AddAlert(string.Format("FineDosaggio NrOdL = {0}, NrBatch = {1}, Stato = 0x{2:x}, SilosDest = {3}",IDOdL,NrBatch,iStatus,iSilos),OdlDosaggio.IDLavorazione,AlertSeverity.Debug);

			if (!IsBitHigh(iStatus,2))				//nessun errore nel dosaggio...
			{
				//TODO Controllare che ci siano tutti gli ingredienti
			}
			
			bool bRiciclo = false;
			if (!IsBitHigh(iStatus,4))				//non Ë dosata in manuale...
			{
                odl = uow.GetObjectByKey<OdlDosaggio>(IDOdL);
                if (odl == null)
                    return;
                lotto = odl.Prodotti.FirstOrDefault(l => l.NrMisc == currMisc);
                if (lotto == null)
                    return;
                Qt‡Tot = odl.Prodotti.Sum(p => p.Quantit‡);// new dacLotto().Qt‡TotProd(IDLotto);
                if (lotto.Stato != StatoLotto.Eseguito)
				{
                    lotto.Quantit‡ = Qt‡Tot;
                    lotto.Stato = StatoLotto.Eseguito;
                    odl.Quantit‡Effettiva += Qt‡Tot;
					odl.NumeroMiscelateEseguite++;
                    currMisc++;
				}
                bRiciclo = (odl.ApparatoLavorazione.Codice == "RICICLO");
				if (!bRiciclo)
				{
                    Silos drSil = uow.FindObject<Silos>(new BinaryOperator("Numero", iSilos));  // this.dtSilosByNr.Rows.Find(iSilos);
					if (drSil != null)
					{
                        odl.Destinazione = drSil;
					}
				}
                lotto.Silos = odl.Destinazione;

			}
		}

        private static Lotto FindLottoByNr(int IDOdL, int NrBatch)
		{
			if (IDOdL==0 || NrBatch == 0)
				return null;
            return uow.FindObject<Lotto>(CriteriaOperator.Parse("Odl = ? AND NrMisc=? AND TipoMovimento=?",IDOdL,NrBatch,TipoMovimento.Produzione));

		}

        private static void LeggiPesiEstratti(ulong bitFineDosaggio)
		{
			int nrBil;
			foreach (Bilancia dr in bilance)
			{
				nrBil= dr.Numero;
				if ((bitFineDosaggio & (ulong)(1<<nrBil)) != 0)
				{
					LeggiPesiEstrattiBilancia(nrBil,dr);
					return;			//1 per ciclo
				}
			}
		}

        private static void InviaPesiTeorici(ulong bitBilanciaPronta)
		{
			int nrBil;
            foreach (var dr in bilance)
			{
				nrBil= dr.Numero;
				if ((bitBilanciaPronta & (ulong)(1<<nrBil)) != 0)
				{
					InviaPesiTeoriciBilancia(nrBil,dr);
					return;			//1 per ciclo
				}
			}
		}

        private static void LeggiPesiEstrattiBilancia(int NrBil, Bilancia drBil)
		{

			const int QTADOS_DB = 50;
			const int QTADOS_DBSIZE=160;
			const int MAX_DOS_COMP=16;

			OpcGroup		theGrp;
			int[]  handles= new int[QTADOS_DBSIZE];
			UInt16[] DBBUFF = new UInt16[QTADOS_DBSIZE+1];

			decimal fFactor;

			theGrp = theSrv.AddGroup("QTADOSDB",true,updateRate);
			AddOpcItems(theGrp,QTADOS_DB+NrBil,QTADOS_DBSIZE,handles,1);

			Thread.Sleep(4000);
			PLCHelper.PLCReadSync(theGrp,handles,DBBUFF);		//Read and wait

			int IDOdL = DBBUFF[2] + (DBBUFF[1]<<16);
			int nrBatch = DBBUFF[3];		//Nr progressivo del lotto all'interno dell'OdL
			int  bitEstr = (DBBUFF[4]<<16) + DBBUFF[5];
			int bitLetti = 0;

			AlertProvider.AddAlert(string.Format("LeggiPesiEstratti Bilancia nr. {0}, NrOdL={1}, NrBatch={2}, bitEstr=0x{3:x}",NrBil,IDOdL,nrBatch,bitEstr),OdlDosaggio.IDLavorazione,AlertSeverity.Debug);

			int iLow,iHigh;

			if (!HL_Bit(bitEstr,out iHigh,out iLow))
			{
				theGrp.Remove(false);
				return;
			}

			if (iHigh>20)
				iHigh=20;

			decimal Qt‡,fVolo,Qt‡Batch;
			int iIngr,NrBatchs;
			int iStatus;
			int dw;
			int IDLotto;
			int IDLottoIngr;
			int nrSilos;
			string IDSilos;
			string IDMagazzino;
			string IDMateriale;
			string IDArticolo=null;

			Lotto drLot;    //Lotto del Prodotto Finito

            odl = uow.GetObjectByKey<OdlDosaggio>(IDOdL);

            drLot = FindLottoByNr(IDOdL,nrBatch);

			//IDLotto puÚ essere = 0 per la bilancia 11 (recupero conigli)
			IDLotto=0;	
			if (drLot != null)
				IDLotto =  drLot.Oid;

			for (int i=iLow;i<=iHigh;i++)
			{
				if (i>=MAX_DOS_COMP)
					break;

				if (IsBitHigh(bitEstr,i))
				{
					dw = (i+1)*10;

					iStatus = DBBUFF[dw+6];
					iIngr = DBBUFF[dw+7];
					nrSilos = DBBUFF[dw];
					IDSilos = string.Format("S{0}",nrSilos);

					if (NrBil==11)
					{
						//Bilancia recupero conigli: il materiale pesato viene stoccato nel silos 46
						IDSilos = "S46";
						Qt‡ = (decimal) DBBUFF[dw+1];
                        Silos silos = uow.GetObjectByKey<Silos>(IDSilos);
                        lotto.Silos = silos;
                        lotto.Quantit‡ += Qt‡;
					}
					else
					{
                        Lotto drIngr = odl.Ingredienti.FirstOrDefault(ingr => ingr.NrComp == iIngr && ingr.NrMisc == currMisc);
                        if (drIngr != null)
                            drIngr.Stato = StatoLotto.Eseguito;

						if (drIngr == null)
						{
							//Verifico se Ë stato dosata in manuale controllando il silos di estrazione
                            Silos silos = uow.GetObjectByKey<Silos>(IDSilos);
                            drIngr = silos.Lotto;
							if (drIngr == null)
							{
								AlertProvider.AddAlert(string.Format("LeggiPesiEstrattiBilancia nr. {0}: Ingrediente nr. {1} da silos nr. {2} non valido",NrBil,iIngr,nrSilos),OdlDosaggio.IDLavorazione,AlertSeverity.Error);
								continue;
							}
							if (lotto != null)
							{
                                //Aggiunge l'ingrediente dosato in manuale
                                Lotto ingr = new Lotto(odl.Session)
                                {
                                    Articolo = lotto.Articolo,
                                    Qt‡Teo = 0,
                                    NrMisc = currMisc,
                                    NrComp = iIngr,
                                    TipoMovimento = TipoMovimento.Consumo,
                                    Stato = StatoLotto.Pronto,
                                    Odl = odl
                                };
                                lotto.Silos.RegistraMovimento(ingr);
                                
							}
						}

						//Determina se Ë un aggiunta manuale e in quel caso non considera
						//il silos restituito dal PLC perchË Ë fittizio
						//ma decrementa dal magazzino sacchi...
						
						fFactor = drBil.kMult;
						if (drBil.Volumetrica)
						{
                            fFactor = fFactor / drIngr.Articolo.PesoSpecifico;
						}
						Qt‡ = (decimal) (DBBUFF[dw+1]/fFactor);

						NrBatchs = DBBUFF[dw+8];
						if (NrBatchs==0)
							NrBatchs=1;

						fVolo = (decimal) (DBBUFF[dw+2] / fFactor);
						Qt‡Batch = Qt‡/NrBatchs;

						AlertProvider.AddAlert(string.Format("Fine dosaggio: Bil nr. {0}, NrOdL={1}, NrBatch={2}, bitEstr=0x{3:x}, NrSilos={4}, Qt‡={5}, Volo={6}, Status=0x{7:x}, NrIngr={8}, NrTotBatch={9}",
							NrBil,IDOdL,nrBatch,bitEstr,nrSilos,Qt‡,fVolo,iStatus,iIngr,NrBatchs),OdlDosaggio.IDLavorazione,AlertSeverity.Debug);

						/*
						if (this.IsBitHigh(iStatus,10))			//Cambio silos - non funziona - 
						{
							//TODO Cambio silos
							DataTable dtSos;
							DataRow drIngrSos;
							if (drIngr != null)
							{
								float fDiff = (float) drIngr["Qt‡Teo"] - Qt‡Batch;
								ShowMsg("LeggiPesiEstrattiBilancia nr. {0}: Cambio silos; Q.t‡ residua: {1:n}",NrBil,fDiff);
								dtSos = mIngr.GetDataSet("IDLotto = " + IDLotto + " AND IDMateriale = " + IDMateriale + " AND Stato=" + (int) StatoIngrediente.Sostitutivo).Tables[0];
								if (dtSos.Rows.Count != 0)
								{
									drIngrSos = dtSos.Rows[0];
									if (fDiff>0)
									{
										drIngrSos["Qt‡Teo"] = fDiff;
										drIngrSos["Stato"] = StatoIngrediente.Pronto;
									}
									else
										drIngrSos.Delete();
									mIngr.UpdateDataSet(dtSos.DataSet);
								}
								drIngr["Qt‡Teo"] = Qt‡*NrBatchs;
							}
						}
						*/

						//Registra consumo
						switch (nrSilos)
						{
							case 1096:
							case 1097:
							case 1098:
							case 1099:
							{
								IDSilos=string.Empty;
								break;
							}
								//non pi˘ utilizzato
							case 901:
							{
								IDSilos= "S4";
								break;
							}
								// Silos fittizi utilizati per l'iniezione di liquidi in pressa....
								//Il PC invia i codici 903..905 (invece di quelli reali) al PLC che quindi non esegue alcuna estrazione
								//L'iniezione avviene durante la pellettatura che non ne registra il consumo.
								//Qui viene registrato solo il consumo teorico
							case 903:
							{
								IDSilos="S113";
								break;
							}
							case 904:						
							{
								IDSilos="S114";
								break;
							}
							case 905:
							{
								IDSilos="S115";
								break;
							}
						}

						if (IDSilos != string.Empty && drIngr != null)
						{
                            drIngr.Quantit‡ = Qt‡Batch;
                            drIngr.Stato = StatoLotto.Eseguito;
						}

						if (IsBitHigh(iStatus,15))							//Modifica del volo
						{
							AlertProvider.AddAlert(string.Format("LeggiPesiEstrattiBilancia nr. {0}: Variazione volo: Silos nr. {1}, Volo = {2:n}",NrBil,nrSilos,fVolo),OdlDosaggio.IDLavorazione,AlertSeverity.Debug);
                            drIngr.Silos.Volo = fVolo;
							//new dacMateriale().SetVolo(IDMateriale,IDSilos,fVolo);
						}
					}		//endif NrBil==11
					SetBitHigh(ref bitLetti,i);
				}			// endif IsBitHigh()
			}			//endfor i

			//23.11.04: Segna come letti tutti i pesi ricevuti dal PLC
			bitLetti = bitEstr;
			//
			DBBUFF[6]=(UInt16) ((bitLetti & 0xFFFF0000) >> 16);
			DBBUFF[7]= (UInt16) (bitLetti & 0x0000FFFF);
			PLCHelper.PLCWriteSync(theGrp,handles,DBBUFF,6,2);
			theGrp.Remove(false);

		}

        private static void InviaPesiTeoriciBilancia(int NrBil, Bilancia drBil)
		{
			const int QTATEO_DBSIZE=160;
			const int QTATEO_DBOFFSET=30;
			int MAX_COMP=16;			//Nr. massimo componenti per bilancia
			OpcGroup		grpQt‡Teo;
			int[]  hQt‡Teo = new int[QTATEO_DBSIZE];
			UInt16[] DBBUFF = new UInt16[QTATEO_DBSIZE+1];			//Base 1

			Lotto drLot;
			Silos drSilos;

			Formula drMat;

			bool bEsc=false;

			int iMin,iNum,iComp;
			decimal fFactor;
			decimal fVolo;
			decimal pSpec;
			bool bVolum;
			string IDSilos;
			string IDMateriale;

			grpQt‡Teo = theSrv.AddGroup("QTATEODB",true,updateRate);
			AddOpcItems(grpQt‡Teo,QTATEO_DBOFFSET+NrBil,QTATEO_DBSIZE,hQt‡Teo,1);

			//21.10.05 aumentato delay da 2000 a 4000 ms
			Thread.Sleep(4000);

			PLCHelper.PLCReadSync(grpQt‡Teo,hQt‡Teo,DBBUFF);		//Read and wait

			int IDOdL = (DBBUFF[1]<<16) + DBBUFF[2];
			int NrBatch = DBBUFF[3];
			int bitBusy = (DBBUFF[4]<<16) + DBBUFF[5];
			int bitSent=0;
			bool bSent=false;
			int iSent=0;

			if (IDOdL == 0)
			{
				AlertProvider.AddAlert(string.Format("ERR: InviaPesiTeorici Bilancia nr. {0}: NrOdL={1} non valido",NrBil,IDOdL),OdlDosaggio.IDLavorazione,AlertSeverity.Debug);
				goto skip;
//				grpQt‡Teo.Remove(false);
//				return;
			}

            odl = uow.GetObjectByKey<OdlDosaggio>(IDOdL);

			drLot = FindLottoByNr(IDOdL,NrBatch);

			if (drLot == null)
			{
				AlertProvider.AddAlert(string.Format("ERR: InviaPesiTeorici Bilancia nr. {0}: NrBatch={1} non valido",NrBil,NrBatch),OdlDosaggio.IDLavorazione,AlertSeverity.Debug);
				goto skip;
//				grpQt‡Teo.Remove(false);
//				return;
			}

			int IDLotto = drLot.Oid;

			string IDBilancia = drBil.Codice;

			//numero ingredienti da eseguire
            System.Collections.Generic.IList<Lotto> dtIngr;
            if (NrBil==9)
                dtIngr = odl.Ingredienti.Where(l => l.NrMisc == NrBatch && l.Stato == StatoLotto.Pronto && l.Modalit‡.Codice == "MAN").ToList();
            else
                dtIngr = odl.Ingredienti.Where(l => l.NrMisc == NrBatch && l.Stato == StatoLotto.Pronto && l.ApparatoLavorazione == drBil).ToList();

            int NrIngr = dtIngr.Count;

			DBBUFF[8] = (UInt16) NrIngr;			//NrIngredienti

			AlertProvider.AddAlert(string.Format("InviaPesiTeorici Bilancia nr. {0}, NrOdL={1}, NrBatch={2}, NrIngredienti={3}, bitBusy=0x{4:x}",NrBil,IDOdL,NrBatch,NrIngr,bitBusy),OdlDosaggio.IDLavorazione,AlertSeverity.Debug);

			Get_L_Bit(bitBusy,out iMin,out iNum);

			if (iNum==0)
			{
				goto skip;
//				grpQt‡Teo.Remove(true);
//				return;
			}

			//Azzera nr.ingr.
			for (int i=10;i<QTATEO_DBSIZE-9;i+=10)
				DBBUFF[i+9]=0;

			iComp=iMin;
			UInt16 iIngr;
			UInt16 iSilos;

			foreach (var drIngr in dtIngr)
			{
				if (iComp>MAX_COMP)
				{
					bEsc=true;
					break;
				}

				if (drIngr.Stato == StatoLotto.Pronto)
				{
					iSent++;
					fFactor = drBil.kMult;
                    pSpec = drIngr.Articolo.PesoSpecifico;
					bVolum = drBil.Volumetrica;
					if (bVolum)
						fFactor = fFactor / pSpec;
					if (drIngr.Silos != null)
					{
                        drSilos = drIngr.Silos;
						iSilos = (UInt16) drSilos.Numero;
						DBBUFF[(iComp+1)*10+2] = System.Convert.ToUInt16(drSilos.SogliaGrosso * fFactor);
						DBBUFF[(iComp+1)*10+3] = System.Convert.ToUInt16(drSilos.SogliaFine*fFactor);

                        fVolo = drSilos.Volo;
                        //fVolo = dacMat.GetVolo(IDMateriale,IDSilos);
                        //if (fVolo==0)
                        //{
                        //    if (!drSilos.IsVoloNull())
                        //        fVolo = drSilos.Volo;																														//Volo predefinito del silos;
                        //}
						DBBUFF[(iComp+1)*10+4] = System.Convert.ToUInt16(fVolo*fFactor);
						DBBUFF[(iComp+1)*10+5] = 0;																																
						DBBUFF[(iComp+1)*10+6] = 0;
						DBBUFF[(iComp+1)*10+7] = System.Convert.ToUInt16(drIngr.Tolleranza*fFactor);			//Tolleranza positiva (kg)
						DBBUFF[(iComp+1)*10+8] = System.Convert.ToUInt16(drIngr.Tolleranza*fFactor);			//Tolleranza negativa (kg)
					}
					else
					{
						//Se Ë un aggiunta manuale non trasmetto codice Apparato di estrazione...
						iSilos= 1001;												//Silos fittizio per aggiunte manuali
						DBBUFF[(iComp+1)*10+2] = 0;
						DBBUFF[(iComp+1)*10+3] = 0;
						DBBUFF[(iComp+1)*10+4] = 0;
						DBBUFF[(iComp+1)*10+5] = mCodiceMateriale(drIngr.Articolo.Codice);																						//Codice Materia Prima																																//Nr. Materia Prima (?)
						DBBUFF[(iComp+1)*10+6] = 0;																																		//Nr. Materia Prima (?)
						DBBUFF[(iComp+1)*10+7] = 0;
						DBBUFF[(iComp+1)*10+8] = 0;
					}

					decimal fQt‡ = drIngr.Qt‡Teo*fFactor;

					DBBUFF[(iComp+1)*10]=iSilos;
					DBBUFF[(iComp+1)*10+1] = (UInt16)System.Math.Round(fQt‡);
					iIngr = System.Convert.ToUInt16((int) drIngr.NrComp);
					DBBUFF[(iComp+1)*10+9] = iIngr;

					SetBitHigh(ref bitBusy,iComp);
					SetBitHigh(ref bitSent,iComp);
					AlertProvider.AddAlert(string.Format("InviaPesiTeorici NrBil = {0}, NrOdL = {1}, NrBatch = {2}, Ingr={3}, Silos={4}, Qt‡={5}",NrBil,IDOdL,NrBatch,iIngr,iSilos,fQt‡),OdlDosaggio.IDLavorazione,AlertSeverity.Debug);

					iComp++;
					if (iComp>=iMin+iNum)
					{
						bSent=true;
						PLCHelper.PLCWriteSync(grpQt‡Teo,hQt‡Teo,DBBUFF,(iMin+1)*10,iSent*10);
						iSent=0;
						Get_L_Bit(bitBusy,out iMin,out iNum);	
						if (iNum==0)
						{
							bEsc=true;
							break;
						}
						iComp = iMin;
					}
				}		//end if
			}		//end foreach

			if (iSent>0)
			{
				bSent=true;
				PLCHelper.PLCWriteSync(grpQt‡Teo,hQt‡Teo,DBBUFF,(iMin+1)*10,iSent*10);
			}

			if (bSent)
			{
				for (int j=1;j<=NrIngr;j++)
				{
					if (DBBUFF[j*10] != 0)
					{
						iIngr=DBBUFF[j*10+9];
						Lotto dr = dtIngr.FirstOrDefault(i => i.NrComp == iIngr); // new object[] {IDLotto,iIngr});
                        if (dr != null)
                            dr.Stato = StatoLotto.InEsecuzione;
					}
				}
                uow.CommitChanges();
				DBBUFF[6] = (UInt16) ((bitSent & 0xFFFF0000)>>16);
				DBBUFF[7] = (UInt16) (bitSent & 0x0000FFFF);
				if (!bEsc)
				{
					DBBUFF[6] |= (UInt16) 0x8000;			//ho inviato tutti gli ingredienti
					AlertProvider.AddAlert(string.Format("InviaPesiTeoriciBilancia nr. {0}, Inviati tutti i componenti",NrBil),OdlDosaggio.IDLavorazione,AlertSeverity.Debug);
					bEsc=true;
				}
				PLCHelper.PLCWriteSync(grpQt‡Teo,hQt‡Teo,DBBUFF,6,3);	
			}
skip:			
			if (!bEsc)
			{
				DBBUFF[6] = (UInt16) ((bitSent & 0xFFFF0000)>>16);
				DBBUFF[7] = (UInt16) (bitSent & 0x0000FFFF);
				DBBUFF[6] |= (UInt16) 0x8000;			//ho inviato tutti gli ingredienti
				PLCHelper.PLCWriteSync(grpQt‡Teo,hQt‡Teo,DBBUFF,6,3);
				AlertProvider.AddAlert(string.Format("InviaPesiTeoriciBilancia nr. {0}, Inviati tutti i componenti",NrBil),OdlDosaggio.IDLavorazione,AlertSeverity.Debug);
			}
			grpQt‡Teo.Remove(false);
			return;

		}

		#endregion

		#region Operazioni sui bit
        private static bool HL_Bit(int iWord, out int iHighest, out int iLowest)
		{
		
			int		nTel;
			iHighest= 0;
			iLowest= 32;


			for(nTel=0; nTel<32; nTel++)
			{
				if(IsBitHigh(iWord, nTel))
				{
					if(nTel>iHighest) iHighest=nTel;
					if(nTel<iLowest) iLowest=nTel;
				}
			}

			if(iLowest>iHighest)
				return false;	//alle bits laag
	
			return true;
		
		}


        private static void SetBitHigh(ref int iWord, int nBit)
		{
			int iHulpLong=1;
	
			iHulpLong <<= nBit;
			iWord |= iHulpLong;
		}

        private static void Get_L_Bit(int iWord, out int iMin, out int iNum)
		{

			//Restituisce in iMin la posizione del bit pi˘ basso con valore=0
			//e in iNum il numero di bit con valore=0 consecutivi a partire da iMin
	
			int		i;
			iNum= 0;
			iMin= 31;


			for(i=0; i<32; i++)
			{
				if(!IsBitHigh(iWord, i))
				{
					if(i<iMin) 
						iMin=i;
				}
				else
				{
					if(iMin<31)
						break;
				}
			}
			iNum=i-iMin;
		}


        static bool IsBitHigh(int iWord, int iBit)
		{
			int iHulpLong=1;
	
			iHulpLong <<= iBit;
			if((iWord & iHulpLong)!=0)
				return true;

			return false;

		}


		#endregion

		#region Metodi Check
        private static void CheckComDb()
		{
			int DW_DOS_RIC = 6;			//1=PLC Pronto per dosare; 0=PC Ack
			int DW_DOS_TEO	= 30;		//bit mask Bilancia pronta per dosare
			int DW_DOS_EFF = 34;		//bit mask Fine Dosaggio Bilancia
			int DW_DOS_END=4;			//1=PLC Fine Dosaggio; 0=PC Ack

			int DW_DOS_RECEPT_SET = 2;			// (PC->PLC) : 1=Richiesta dosaggio da PC; 2=Nulla da dosare
			int DW_DOS_RECEPT_ACK=3;			//(PLC->PC) : 1=Ack Dosaggio
			
			ulong bitBilanciaPronta=0;
			ulong bitFineDosaggio=0;

			//Controllo richieste di comando dosaggio
			CheckCmd();

			if (ComStatus==eComStatus.WAITING)
			{
				if (COMBUFF[DW_DOS_RECEPT_ACK]!=0)
				{
					COMBUFF[DW_DOS_RECEPT_ACK]=0;
					PLCHelper.PLCWriteWordSync(opcGrpCom,ComHandles[DW_DOS_RECEPT_ACK-1],0);
					COMBUFF[DW_DOS_RIC]=0;
					PLCHelper.PLCWriteWordSync(opcGrpCom,ComHandles[DW_DOS_RIC-1],0);
					ComStatus=eComStatus.IDLE;
				}
				return;
			}

			if (COMBUFF[DW_DOS_RIC] == 1)
			{
				if (RichiestaDosaggio())
				{
					COMBUFF[DW_DOS_RECEPT_SET]=1;
					PLCHelper.PLCWriteWordSync(opcGrpCom,ComHandles[DW_DOS_RECEPT_SET-1],1);
					ComStatus=eComStatus.WAITING;
					return;
				}
				else
				{
					COMBUFF[DW_DOS_RECEPT_SET]=2;
					PLCHelper.PLCWriteWordSync(opcGrpCom,ComHandles[DW_DOS_RECEPT_SET-1],2);
					COMBUFF[DW_DOS_RIC]=0;
					PLCHelper.PLCWriteWordSync(opcGrpCom,ComHandles[DW_DOS_RIC-1],0);
					return;
				}
			}
			else
			{
				bitBilanciaPronta = (ulong) (COMBUFF[DW_DOS_TEO]<<16 | COMBUFF[DW_DOS_TEO+1]);
				bitFineDosaggio= (ulong) (COMBUFF[DW_DOS_EFF]<<16 | COMBUFF[DW_DOS_EFF+1]);
				if (bitBilanciaPronta != 0 || bitFineDosaggio != 0)
				{
					AlertProvider.AddAlert(string.Format("CheckComDb: bitBilanciaPronta= 0x{0:x}, bitFineDosaggio = 0x{1:x}",bitBilanciaPronta,bitFineDosaggio),OdlDosaggio.IDLavorazione,AlertSeverity.Debug);
				}

				if (bitBilanciaPronta!=0)
				{
					bitFineDosaggio &= bitBilanciaPronta;
					if (COMBUFF[DW_DOS_END]!=0)
					{
						COMBUFF[DW_DOS_END]=0;
						AlertProvider.AddAlert("CheckComDb: Ricevuta Segnalazione di fine dosaggio e richiesta componenti",OdlDosaggio.IDLavorazione,AlertSeverity.Debug);
					}
				}

				if (bitFineDosaggio!=0)
				{
					LeggiPesiEstratti(bitFineDosaggio);
				}
				if (bitBilanciaPronta!=0)
				{
					InviaPesiTeorici(bitBilanciaPronta);
				}

				if (COMBUFF[DW_DOS_END]!=0)
				{
					FineDosaggio();
					COMBUFF[DW_DOS_END]=0;
					PLCHelper.PLCWriteWordSync(opcGrpCom,ComHandles[DW_DOS_END-1],0);
				}
			}
		}

		private static void CheckCmd()
		{
			CmdMsg mMsg;
			int nrBil=0;
			int nrSil=0;
			{
				if (CmdQueue.Count>0)
				{
					mMsg = (CmdMsg) CmdQueue.Dequeue();
					Bilancia drBil = bilance.FirstOrDefault(b => b.Codice == mMsg.ID);
					if (drBil != null)
						nrBil = drBil.Numero;
					switch (mMsg.cmd)
					{
						case Cmd.AckBil:
						{
							AckBilancia(nrBil);
							break;
						}
						case Cmd.ResetBil:
						{
							ResetBilancia(nrBil);
							break;
						}
						case Cmd.InitBil:
						{
							InitBilancia(nrBil);
							break;
						}
						case Cmd.InitSilos:
						{
                            //Silos drSil = uow.GetObjectByKey<Silos>(mMsg.ID);
                            //if (drSil != null)
                            //    nrSil = drSil.Numero;
							InitSilos();
							break;
						}
						case Cmd.SetManual:
						{
							BilanciaInManuale(nrBil,(bool)mMsg.value);	
							break;
						}
						case Cmd.SetQt‡Man:
						{
							ManualQt‡(nrBil,System.Convert.ToDecimal(mMsg.value));
							break;
						}
						case Cmd.SetSilosMan:
						{
							Silos drSil = uow.GetObjectByKey<Silos>(mMsg.value.ToString());
							if (drSil != null)
								nrSil = drSil.Numero;
							ManualNrSilos(nrBil,nrSil);
							break;
						}
						case Cmd.StartMan:
						{
							ManualStart(nrBil);
							break;
						}
						case Cmd.StopMan:
						{
							ManualStop(nrBil);
							break;
						}
						case Cmd.SetSilosDest:
						{
							Silos drSil = uow.GetObjectByKey<Silos>(mMsg.value.ToString());
							if (drSil != null)
								nrSil = drSil.Numero;
							SetIDSilosDest(int.Parse(mMsg.ID),nrSil);
							break;
						}
					}
				}
			}
		}

        private static bool bAlertPeso = false;

        private static void CheckTag()
		{
			if ((TAGBUFF[0] & 0x0200) == 1)
			{
				if (!bAlertPeso)
					AlertProvider.AddAlert("Peso supporto non congruo",OdlDosaggio.IDLavorazione,AlertSeverity.Error);
				bAlertPeso=true;
			}
			else
				bAlertPeso=false;
		}

        #endregion

		#region Acquisizione variabili di stato bilancia

        private static int NrOdL(int nrBil)
		{
			if (nrBil<10)
				return COMBUFF[nrBil*10+130+1];
			return 0;
		}

        private static int NrBatch(int nrBil)
		{
			//TODO Verificare limite della matrice
			if (nrBil<10)
				return COMBUFF[nrBil*10+130+2];
			return 0;
		}

        private static int NrIngrInEstrazione(int nrBil)
		{
			//TODO Verificare limite della matrice
			if (nrBil<10)
				return COMBUFF[nrBil*10+130+3];
			return 0;
		}

        private static bool IsFault(int nrBil)
		{
			int Offset = nrBil*10+130;
			byte status;
			byte mskFault = 0x04;

			if (Offset+8 > COMBUFF.Length-1) return false;

			status=(byte) ((COMBUFF[Offset+8] & 0xFF00)>>8);

			return ((status & mskFault) != 0);
		}

		private static  bool IsManual(int nrBil)
		{
			int Offset = nrBil*10+130;
			byte status;
			byte mskManual = 0x40;

			if (Offset+8 > COMBUFF.Length-1) return false;

			status=(byte) ((COMBUFF[Offset+8] & 0xFF00)>>8);

			return ((status & mskManual) != 0);
		}

        private static bool IsWorking(int nrBil)
		{
			int Offset = nrBil*10+130;
			byte status;
			byte mskWorking = 0x80;

			if (Offset+8 > COMBUFF.Length-1) return false;

			status=(byte) ((COMBUFF[Offset+8] & 0xFF00)>>8);

			return ((status & mskWorking) != 0);
		}


        private static int StatusCode(int nrBil)
		{
			int Offset = nrBil*10+130;
			byte statusCode;

			if (Offset+8 > COMBUFF.Length-1) return 0;

			statusCode = (byte) (COMBUFF[Offset+8] & 0x00FF);
			return statusCode;
		}

        private static string StatusMessage(int nrBil)
		{
			int sCode = StatusCode(nrBil);
			switch (sCode)
			{
				case 1:
					return "Pausa";
				case 2:
					return "Non richiesto";
				case 3:
					return "Attesa";
				case 4:
					return "Avviato";
				case 5:
					return "Finito";
				case 6:
					return "Interrotto";
				case 7:
					return "Dosaggio manuale";
				case 8:
					return "Non automatico";
				case 9:
					return "Allarme";

				case 20:
					return "Errore di tolleranza";
				case 21:
					return "Fuori limite";
				case 22:
					return "Fuori tempo di dosaggio";
				case 23:
					return "Nessun incremento di peso";
				case 24:
					return "Silos sconosciuto";
				case 25:
					return "Silos bloccato";
				case 26:
					return "Raggiunto numero massimo di impulsi";

				case 30:
					return "Elemento di dosaggio non avviato";
				case 31:
					return "Elemento di dosaggio non fermo";
				case 32:
					return "Non tutte le serrande chiuse";
				case 33:
					return "Serranda non aperta";
				case 34:
					return "Serranda erronea";
				case 35:
					return "Test della serranda fallito";
				case 36:
					return "La serranda non ha raggiunto la velocit‡";
				case 37:
					return "Nessun impulso dai liquidi";

				case 39:
					return "Silos vuoto";
				case 40:
					return "Bilancia oltre tara massima";
				case 41:
					return "Peso negativo";
				case 42:
					return "Errore indicatore";
				case 43:
					return "Incremento di peso invalido";
				case 44:
					return "Decremento di peso invalido";
				case 45:
					return "Bilancia sovraccarica";
				default:
					return string.Format("Messaggio sconosciuto ({0})",sCode);
			}

		}

        private static int Velocit‡(int nrBil)
		{
			//TODO Verificare limite della matrice
			if (nrBil<10)
				return (int) COMBUFF[nrBil*10+130+5];
			return 0;
		}

        private static decimal NettoIngrediente(int nrBil)
		{
			return (PesoBil(nrBil)-TaraIngrediente(nrBil));
		}

        private static decimal SetPoint(int nrBil)
		{
			//TODO Verificare limite della matrice
			if (nrBil<10)
			{
                Bilancia dr = bilance.FirstOrDefault(b => b.Numero == nrBil);
				if (dr != null)
				{
					decimal k = dr.kMult;
					return (decimal) COMBUFF[nrBil*10+130+6]/k;
				}
			}
			return 0;
		}

        private static decimal TaraIngrediente(int nrBil)
		{
			//TODO Verificare limite della matrice
			if (nrBil<10)
			{
                Bilancia dr = bilance.FirstOrDefault(b => b.Numero == nrBil);
                if (dr != null)
				{
					decimal k = dr.kMult;
					return (decimal) COMBUFF[nrBil*10+130+7]/k;
				}
			}
			return 0;
		}

        private static int CodiceSilosInEstrazione(int nrBil)
		{
			//TODO Verificare limite della matrice
			if (nrBil<10)
				return COMBUFF[nrBil*10+130+4];
			return 0;
		}

		public static decimal PesoBil(int nrBil)
		{
			//TODO Verificare limite della matrice
			if (nrBil<10)
			{
                Bilancia dr = bilance.FirstOrDefault(b => b.Numero == nrBil);
                if (dr != null)
				{
					decimal k = dr.kMult;
					return (decimal) COMBUFF[nrBil*10+130+9]/k;
				}
			}
			return 0;
		}

		#endregion

		#region Stato Lavorazione

        private static string GetIDSilosDestStadio(int nrStadio)
		{
			int Offset = nrStadio*12+28;
			ushort NrSilos;

			if (Offset+6 > COMBUFF.Length-1) return string.Empty;

			NrSilos = COMBUFF[Offset+6];

            Silos dr = silosCollection.FirstOrDefault(s => s.Numero == NrSilos);

			string IDSilos =string.Empty;
			if (dr != null)
				IDSilos = dr.Codice;
			return IDSilos;
		}

        private static void SetIDSilosDestStadio(int nrStadio, int NrSilos)
		{

			int IDLotto = IDLottoStadio(nrStadio);

			if (IDLotto != 0)
				SetIDSilosDest(IDLotto,NrSilos);

		}

        private static int GetTimerStadio(int nrStadio)
		{
			int Offset = nrStadio*12+28;

			if (Offset+11 > COMBUFF.Length-1) 
				return 0;			//TimeSpan.Zero;

			int nSec = COMBUFF[Offset+11];

			//return TimeSpan.FromSeconds(nSec);
			return nSec;

		}

        private static int SetIDSilosDest(int IDLotto, int NrSilos)
		{

			if (IDLotto == 0)
				return 0;

            //dsLotto dsLot = new dsLotto();
            //new dacLotto().Fill(dsLot,IDLotto);
            //dsLotto.LottoRow dr = dsLot.Lotto[0];

            Lotto dr = uow.GetObjectByKey<Lotto>(IDLotto);

			int IDOdL = dr.Odl.Oid;
			int nrBatch = dr.NrMisc;

			PLCHelper.PLCReadSync(opcGrpCmd,CmdHandles,CMDBUFF);

			CMDBUFF[5] = (UInt16) (IDOdL>>16);
			CMDBUFF[6] = (UInt16) (IDOdL & 0xFFFF);
			CMDBUFF[7] = (UInt16) nrBatch;
			CMDBUFF[8] = (UInt16)  NrSilos;

			PLCHelper.PLCWriteSync(opcGrpCmd,CmdHandles,CMDBUFF);

			return 1;

		}

        private static int IDLottoStadio(int nrStadio)
		{
			int Offset = nrStadio*12+28;
			int NrOdL,NrBatch;

			if (Offset+5 > COMBUFF.Length-1) return 0;

			NrOdL = COMBUFF[Offset+4] + (COMBUFF[Offset+3]<<16);
			NrBatch = COMBUFF[Offset+5];

			Lotto dr = FindLottoByNr(NrOdL,NrBatch);
			if (dr != null)
				return dr.Oid;
			return 0;

		}

        private static decimal PesoStadio(int nrStadio)
		{
			int Offset = nrStadio*12+28;
			

			if (Offset+10 > COMBUFF.Length-1) return 0;

			return (decimal) COMBUFF[Offset+10];

		}


		#endregion

		#region Comandi Bilance

        private static void AckBilancia(int nrBil)
		{
			PLCHelper.PLCReadSync(opcGrpCmd,CmdHandles,CMDBUFF);
			PLCHelper.PLCWriteWordSync(opcGrpCmd,CmdHandles[5+10*nrBil],CMDBUFF[6+10*nrBil] |= 0x10);
			AlertProvider.AddAlert(string.Format("Bilancia nr. {0} - Ack",nrBil),OdlDosaggio.IDLavorazione,AlertSeverity.Information);
		}


        private static void ManualStart(int nrBil)
		{
			PLCHelper.PLCReadSync(opcGrpCmd,CmdHandles,CMDBUFF);
			PLCHelper.PLCWriteWordSync(opcGrpCmd,CmdHandles[5+10*nrBil],CMDBUFF[6+10*nrBil] |= 0x01);
			AlertProvider.AddAlert(string.Format("Bilancia nr. {0} - Avvio dosaggio in manuale ",nrBil),OdlDosaggio.IDLavorazione,AlertSeverity.Information);
		}


        private static void ManualStop(int nrBil)
		{
			PLCHelper.PLCReadSync(opcGrpCmd,CmdHandles,CMDBUFF);
			PLCHelper.PLCWriteWordSync(opcGrpCmd,CmdHandles[5+10*nrBil],CMDBUFF[6+10*nrBil] |= 0x02);
			AlertProvider.AddAlert(string.Format("Bilancia nr. {0} - Arresto dosaggio in manuale",nrBil),OdlDosaggio.IDLavorazione,AlertSeverity.Information);
		}

        private static void ManualNrSilos(int nrBil, int nrSilos)
		{
			PLCHelper.PLCWriteWordSync(opcGrpCmd,CmdHandles[10*nrBil-1],(UInt16)nrSilos);
			AlertProvider.AddAlert(string.Format("Bilancia nr. {0} - Dosaggio in manuale: silos nr. {1}",nrBil,nrSilos),OdlDosaggio.IDLavorazione,AlertSeverity.Information);
		}

        private static void ManualQt‡(int nrBil, decimal Qt‡)
		{
            Bilancia dr = bilance.FirstOrDefault(b => b.Numero == nrBil);
			if (dr != null)
			{
				decimal k = dr.kMult;
				PLCHelper.PLCWriteWordSync(opcGrpCmd,CmdHandles[10*nrBil],(UInt16) System.Math.Round(Qt‡*k));
				AlertProvider.AddAlert(string.Format("Bilancia nr. {0} - Dosaggio in manuale: Quantit‡ da estrarre {1}",nrBil,Qt‡),OdlDosaggio.IDLavorazione,AlertSeverity.Information);
			}
		}

        private static void BilanciaInManuale(int nrBil, bool value)
		{
			PLCHelper.PLCReadSync(opcGrpCmd,CmdHandles,CMDBUFF);
			CMDBUFF[6+10*nrBil] &= (ushort) 0xFCFF;					//Azzero i due bit che indicano man/aut
			CMDBUFF[6+10*nrBil] |= (ushort) (value?0x0200:0x0100);	//Setto il bit giusto
			PLCHelper.PLCWriteWordSync(opcGrpCmd,CmdHandles[5+10*nrBil],CMDBUFF[6+10*nrBil]);
			AlertProvider.AddAlert(string.Format("Bilancia nr. {0} - Dosaggio {1}",nrBil,value?"Manuale":"Automatico"),OdlDosaggio.IDLavorazione,AlertSeverity.Information);
		}

        private static void ResetBilancia(int nrBil)
		{
			PLCHelper.PLCReadSync(opcGrpCmd,CmdHandles,CMDBUFF);
			PLCHelper.PLCWriteWordSync(opcGrpCmd,CmdHandles[5+10*nrBil],CMDBUFF[6+10*nrBil] |= 0x800);
			AlertProvider.AddAlert(string.Format("Bilancia nr. {0} - Reset",nrBil),OdlDosaggio.IDLavorazione,AlertSeverity.Information);
		}

        private static void AbortBilancia(int nrBil)
		{
			PLCHelper.PLCReadSync(opcGrpCmd,CmdHandles,CMDBUFF);
			PLCHelper.PLCWriteWordSync(opcGrpCmd,CmdHandles[5+10*nrBil],CMDBUFF[6+10*nrBil] |= 0x20);
			AlertProvider.AddAlert(string.Format("Bilancia nr. {0} - Abort",nrBil),OdlDosaggio.IDLavorazione,AlertSeverity.Information);
		}
		#endregion
	}
}
