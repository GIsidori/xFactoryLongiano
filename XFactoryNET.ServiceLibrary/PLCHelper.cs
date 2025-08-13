using System;
using System.Threading;
using OPC.Common;
using OPC.Data.Interface;
using OPC.Data;
using XFactoryNET.Module.BusinessObjects;


namespace XFactoryNET.Custom.MM1.ServiceLibrary
{
	/// <summary>
	/// Descrizione di riepilogo per PLCHelper.
	/// </summary>
	internal class PLCHelper
	{

		static bool writeAlertSent=false;
		static bool readAlertSent=false;

		public static void PLCWriteWordSync(OpcGroup theGrp,int handle,UInt16 Value)
		{
			PLCWriteSync(theGrp,new int[] {handle},new UInt16[] {0,Value});
		}

		public static void PLCWriteSync(OpcGroup theGrp,int[] handles,UInt16[] values, int offset, int len)
		{
			int[] h = new int[len];
			UInt16[] b = new UInt16[len+1];
			for (int j=0;j<len;j++)
			{
				h[j] =handles[j+offset-1];
				b[j+1] = values[j+offset];					//base 1
			}
			PLCWriteSync(theGrp,h,b);
		}

		public static void PLCWriteSync(OpcGroup theGrp,int[] handles, UInt16[] values)
		{
			int[] aE;
			const int write_delay=4000;	//tempo di attesa dopo ogni write

			object[] itemValues=new Object[values.Length-1];
			
			for (int i=0;i<values.Length-1;i++)
			{
				itemValues[i]= values[i+1];		//Values è in base 1
			}

			try
			{
				if (!theGrp.Write(handles,itemValues,out aE))						//Write Sync
				{
					if (!writeAlertSent)
					{
						AlertProvider.AddAlert("PLCWriteSync Error",OdlDosaggio.IDLavorazione,AlertSeverity.Error);
						writeAlertSent=true;
					}
				}
				else
				{
					if (writeAlertSent)
					{
						AlertProvider.AddAlert("PLCWriteSync Error recovered",OdlDosaggio.IDLavorazione,AlertSeverity.Information);
						writeAlertSent=false;
					}
				}
				Thread.Sleep(write_delay);
			}
			catch (Exception e)
			{
				if (!writeAlertSent)
				{
					AlertProvider.AddAlert("PLCWriteSync Exception:" + e.ToString(),OdlDosaggio.IDLavorazione,AlertSeverity.Error);
					writeAlertSent=true;
				}
			}

		}


		public static void PLCReadSync(OpcGroup theGrp,int[] handles,UInt16[] ReadBuff)
		{
			OPC.Data.OPCItemState[] sts;
			long lVal;

			if (theGrp.Read(OPC.Data.Interface.OPCDATASOURCE.OPC_DS_DEVICE,handles,out sts))
			{
				foreach( OPCItemState s in sts)
				{
					if( HRESULTS.Succeeded( s.Error ) )
					{
						if ((OPC_QUALITY_STATUS) (s.Quality & (short)OPC_QUALITY_MASKS.STATUS_MASK) !=  OPC_QUALITY_STATUS.OK)
						{
							if (!readAlertSent)
							{
								AlertProvider.AddAlert("theGrp_ReadComplete QUALITY="+OpcGroup.QualityToString(s.Quality),OdlDosaggio.IDLavorazione,AlertSeverity.Error);
								readAlertSent=true;
							}
						}
						else
						{
							if (readAlertSent)
							{
								AlertProvider.AddAlert("theGrp_ReadComplete QUALITY="+OpcGroup.QualityToString(s.Quality),OdlDosaggio.IDLavorazione,AlertSeverity.Error);
								readAlertSent=false;
							}
							try
							{
								lVal = System.Convert.ToInt32(s.DataValue);
								ReadBuff[s.HandleClient+1] = (UInt16) (lVal & 0xFFFF);
							}
							catch (Exception ex)
							{
								if (!readAlertSent)
								{
									AlertProvider.AddAlert("PLCReadSync Exception: " + ex.Message,OdlDosaggio.IDLavorazione,AlertSeverity.Error);
									readAlertSent=true;
								}
							}
						}
					}
					else
					{
						if (!readAlertSent)
						{
							AlertProvider.AddAlert(string.Format(" ih={0}    ERROR=0x{1:x} !", s.HandleClient, s.Error ),OdlDosaggio.IDLavorazione,AlertSeverity.Error);
							readAlertSent=true;
						}
					}
				}
			}
		}
	}
}
