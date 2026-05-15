using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using XFactoryNET.Module.BusinessObjects;
using System.Reflection;
using System.Data.SqlClient;
using System.Security;

namespace XFactoryNET.Custom.Panzoo.AggMan
{
    public partial class frmAggMan : Form
    {

        public event EventHandler Running;

        private const double pesoMax = 25;
        XFactoryNETDataSet.OdlRow odl;      //Odl in corso
        
        private int currOdl;                        //Position odl in corso
        private int currMisc;                       //Miscelata in corso
        private int currIngr;                       //Ingrediente in corso

        public frmAggMan()
        {
            InitializeComponent();

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Program.ConnectionString);

            this.odlTableAdapter.Connection = conn;
            this.lottoTableAdapter.Connection = conn;
            this.silosTableAdapter.Connection = conn;

            this.serialPort1.PortName = Properties.Settings.Default.PortName;
            this.serialPort1.Parity = Properties.Settings.Default.Parity;
            this.serialPort1.DataBits = Properties.Settings.Default.DataBits;
            this.serialPort1.StopBits = Properties.Settings.Default.StopBits;
            this.serialPort1.BaudRate = Properties.Settings.Default.BaudRate;

            //FieldInfo fi = dataGrid1.GetType().GetField("m_sbVert", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance);
            //VScrollBar bar = (VScrollBar)fi.GetValue(dataGrid1);
            //bar.Width = 50;
        }

        private void frmAggMan_Load(object sender, EventArgs e)
        {

            this.odlTableAdapter.ClearBeforeFill = true;
            this.lottoTableAdapter.ClearBeforeFill = true;

            this.serialPort1.NewLine = new string((char)13, 1);

            try
            {
                this.serialPort1.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            CheckOdl();

        }

        delegate void delegateSetBilancia(string data);

        private void SetBilancia(string data)
        {
            enumStatoBilancia stato = enumStatoBilancia.Errore;

            try
            {
                stato = (enumStatoBilancia)int.Parse(data.Substring(1, 1));
                PesoBilancia = double.Parse(data.Substring(2, 5)) / 1000;
                StatoBilancia = stato;
            }
            catch
            {
                StatoBilancia = enumStatoBilancia.Errore;
            }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //if (e.EventType == System.IO.Ports.SerialData.Eof)
            ReadComm();
        }

        private void ReadComm()
        {
            
            //string data = serialPort1.ReadLine();
            int count = serialPort1.BytesToRead;
            char[] buff = new char[count];
            serialPort1.Read(buff, 0, count);
            string data = new string(buff);
            int index = data.LastIndexOf('$');
            if (index != -1)
            {
                data = data.Substring(index);
                Invoke(new delegateSetBilancia(SetBilancia), data);
            }
        }

        private double taraBilancia;

        public double TaraBilancia
        {
            get { return taraBilancia; }
            set { taraBilancia = value; }
        }

        private double pesoBilancia;

        public double PesoBilancia
        {
            get { return pesoBilancia; }
            set
            {
                pesoBilancia = value;
                if (pesoBilancia > pesoMax)
                {
                    lblPesoBilancia.ForeColor = Color.Red;
                }
                else
                    lblPesoBilancia.ForeColor = Color.Lime;
                lblPesoBilancia.Text = pesoBilancia.ToString("n4");
                ShowDiff();
            }
        }

        private enumStatoBilancia statoBilancia;

        private enumStatoBilancia StatoBilancia
        {
            get { return statoBilancia; }
            set
            {
                statoBilancia = value;
                switch (statoBilancia)
                {
                    case enumStatoBilancia.Errore:
                        lblPesoBilancia.ForeColor = Color.Purple;
                        lblStatoBilancia.Text = "Errore di lettura";
                        break;
                    case enumStatoBilancia.Stabile:
                        lblPesoBilancia.ForeColor = Color.Lime;
                        lblStatoBilancia.Text = "Peso stabile";
                        break;
                    case enumStatoBilancia.NonStabile:
                        lblPesoBilancia.ForeColor = Color.Yellow;
                        lblStatoBilancia.Text = "Peso non stabile";
                        break;
                    case enumStatoBilancia.NonValido:
                        lblPesoBilancia.ForeColor = Color.Red;
                        lblStatoBilancia.Text = "Peso non valido";
                        break;
                    default:
                        break;
                }
            }
        }

        private enum enumStatoBilancia
        {
            Errore = -1,
            Stabile = 0,
            NonStabile = 1,
            NonValido = 3
        }


        private bool NextOdl()
        {

            this.odlTableAdapter.FillOdlDosaggioByStato(this.xFactoryNETDataSet.Odl, (int)StatoOdL.InEsecuzione);
            currOdl = -1;
            foreach (XFactoryNETDataSet.OdlRow odlRow in xFactoryNETDataSet.Odl)
            {
                currOdl++;
                odl = odlRow;
                currMisc = 0;
                if (NextMisc())
                    return true;
            }
            cRow = null;
            FasePesatura = enumFasePesatura.Tara;
            currOdl = -1;
            xFactoryNETDataSet.Odl.Clear();
            return false;
        }

        private bool NextMisc()
        {
            while (odl != null && currMisc < odl.NumeroMiscelate)
            {
                currMisc++;
                this.xFactoryNETDataSet.Lotto.Clear();
                //Fill dei componenti da eseguire....
                this.lottoTableAdapter.FillIngredientiOdl(this.xFactoryNETDataSet.Lotto, odl.OID, currMisc);
                if (this.xFactoryNETDataSet.Lotto.Count > 0)
                {
                    currIngr = 0;
                    if (NextIngr())
                        return true;
                }
            }

            this.xFactoryNETDataSet.Lotto.Clear();
            currMisc = 0;
            return false;
        }

        private bool additivo = false;

        private bool NextIngr()
        {
            if (currIngr < xFactoryNETDataSet.Lotto.Count)
            {
                dataGrid1.UnSelect(currIngr);
                currIngr++;
                ingredientiBindingSource.Position = currIngr-1;
                cRow = xFactoryNETDataSet.Lotto[currIngr - 1];
                additivo = (cRow.IsModalitàNull() == false && cRow.Modalità == "ADD");
                if (currIngr == 1 || (PesoBilancia + cRow.QtàTeo >= pesoMax))
                    FasePesatura = enumFasePesatura.Tara;
                else
                    FasePesatura = enumFasePesatura.Lordo;

                txtCurrMisc.Text = currMisc.ToString();
                dataGrid1.Select(currIngr-1);
                return true;
            
            }

            currIngr = 0;
            additivo = false;
            return false;

        }

        enum enumFasePesatura
        {
            Tara,
            Lordo
        }

        enumFasePesatura fasePesatura;

        private enumFasePesatura FasePesatura
        {
            get { return fasePesatura; }
            set
            {
                fasePesatura = value;
                if (fasePesatura == enumFasePesatura.Tara)
                {
                    btnDone.Text = "Tara";
                    lblDiff.Visible = false;
                    label7.Visible = false;
                    lblSetpoint.Visible = false;
                    label10.Visible = false;
                }
                else
                {
                    btnDone.Text = "Peso";
                    lblDiff.Visible = true;
                    label7.Visible = true;
                    lblSetpoint.Visible = true;
                    label10.Visible = true;
                }
                ShowDiff();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CheckOdl();
        }

        private void CheckOdl()
        {
            bool running;
            currOdl = -1;
            timer1.Enabled = false;
            odlBindingSource.SuspendBinding();
            running = NextOdl();
            timer1.Enabled = !running;
            timerBlink.Enabled = running;
            if (running)
                OnRunning();
            odlBindingSource.ResumeBinding();
            odlBindingSource.Position = currOdl;
        }

        private void OnRunning()
        {
            if (Running != null)
                Running(this,new EventArgs());
        }

        private void timerBlink_Tick(object sender, EventArgs e)
        {
            //lblFasePesatura.Visible = !lblFasePesatura.Visible;
            lblSac.Visible = !lblSac.Visible && additivo;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)32:
                    LeggiPeso();
                    break;
                default:
                    break;
            }
        }

        private double sEstr;
        private double sEstrPrec;

        private void LeggiPeso()
        {
            LeggiPeso(false);
        }

        private void LeggiPeso(bool skipCheck)
        {
            if (cRow == null)
                return;

            if (StatoBilancia == enumStatoBilancia.Stabile)
            {
                if (FasePesatura == enumFasePesatura.Tara)
                {
                    sEstr = 0;
                    TaraBilancia = PesoBilancia;
                    FasePesatura = enumFasePesatura.Lordo;
                }
                else
                {
                    double qtà = PesoBilancia - TaraBilancia;
                    sEstr += qtà;
                    cRow.Quantità = sEstrPrec + sEstr;
                    if (skipCheck || cRow.Quantità >= cRow.QtàTeo || (cRow.Tolleranza != 0 && (System.Math.Abs(cRow.Quantità - cRow.QtàTeo)/cRow.QtàTeo) * 100 <= cRow.Tolleranza))
                    //(cRow.Tolleranza != 0 && (cRow.Qtà - cRow.QtàTeo) <= cRow.Tolleranza) ||
                    //(cRow.TolleranzaPerc != 0 && (cRow.Qtà - cRow.QtàTeo) / (cRow.QtàTeo) <= cRow.TolleranzaPerc))
                    {
                        //RegistraConsumo();
                        Next();
                    }
                    else
                    {
                        //L'operatore svuota il contenitore: memorizzo quantità dell'ingrediente pesato finora e mi predispongo per rifare la tara.
                        sEstrPrec += sEstr;
                        FasePesatura = enumFasePesatura.Tara;
                        TaraBilancia = 0;
                    }
                }
            }
        }

        private void Next()
        {
            sEstr = 0;
            sEstrPrec = 0;
            TaraBilancia = PesoBilancia;
            if (NextIngr() == false)
            {
                RegistraConsumo();
                if (NextMisc() == false)
                    CheckOdl();
            }
        }

        private XFactoryNETDataSet.LottoRow cRow;


        private void ShowDiff()
        {
            double diff = 0;
            double setpoint = 0;
            //diff = PesoBilancia - cRow.QtàTeo - sEstr - TaraBilancia + sEstrPrec;
            if (cRow != null)
            {
                //diff = (PesoBilancia - cRow.QtàTeo - TaraBilancia);
                setpoint = cRow.QtàTeo + TaraBilancia - sEstrPrec;
                diff = PesoBilancia - setpoint; // -cRow.QtàTeo - TaraBilancia + sEstrPrec;
                double absDiff = Math.Abs(diff);
                if ((absDiff/cRow.QtàTeo)*100 < cRow.Tolleranza)
                    lblDiff.ForeColor = Color.Lime;
                else if (diff < 0)
                {
                    lblDiff.ForeColor = Color.Yellow;
                }
                else
                    lblDiff.ForeColor = Color.Red;
            }
            lblDiff.Text = string.Format("{0:n3}", diff);
            lblSetpoint.Text = string.Format("{0:n3}", setpoint);
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (timerKey.Enabled)
                return;
            timerKey.Enabled = true;
            LeggiPeso();
        }

        private void textBox6_Validated(object sender, EventArgs e)
        {
            if (cRow != null)
            {
                cRow.CodiceEsterno = textBox6.Text;
            }
        }

        private void frmAggMan_Closed(object sender, EventArgs e)
        {
            serialPort1.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(serialPort1_DataReceived);
            serialPort1.Close();
            serialPort1.Dispose();
            serialPort1 = null;
        }

        private void frmAggMan_Closing(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("Confermi la chiusura del programma", "XFactory.WinCE", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                e.Cancel = true;
        }

        private void btnFineMisc_Click(object sender, EventArgs e)
        {
            if (timerKey.Enabled)
                return;

            if (currIngr == 0)
                return;

            for (; currIngr <= xFactoryNETDataSet.Lotto.Count;currIngr++ )
            {
                xFactoryNETDataSet.Lotto[currIngr - 1].Stato = (int)StatoLotto.Eseguito;
            }
            int retries = 0;
            bool success = false;
            while (!success && retries<10)
            {
                var tran = lottoTableAdapter.Connection.BeginTransaction();
                try
                {
                    lottoTableAdapter.Update(xFactoryNETDataSet.Lotto);
                    success = true;
                    tran.Commit();
                }
                catch (SqlException)
                {
                    tran.Rollback();
                    retries++;
                    System.Threading.Thread.Sleep(1000);
                }
            }
            Next();
            //LeggiPeso(true);
        }

        private void btnTeorico_Click(object sender, EventArgs e)
        {
            if (timerKey.Enabled)
                return;
            RegistraTeorico();
        }

        private void RegistraTeorico()
        {
            if (cRow != null)
            {
                cRow.Quantità = cRow.QtàTeo;
                //RegistraConsumo();
                Next();
            }
        }

        private void RegistraConsumo()
        {


            //Incrementa giacenza prodotto finito;
            using (XFactoryNETDataSet ds = new XFactoryNETDataSet())
            {
                bool success = false;
                int retries = 0;
                while (!success && retries < 10)
                {
                    var tran = lottoTableAdapter.Connection.BeginTransaction();
                    try
                    {
                        lottoTableAdapter.FillProdottiOdl(ds.Lotto, odl.OID, currMisc);
                        odlTableAdapter.FillByKey(ds.Odl, cRow.Odl);

                        //Gestire la produzione dei sottoprodotti
                        XFactoryNETDataSet.LottoRow prod = ds.Lotto[0];
                        double qtàProd = 0;
                        foreach (var lotto in xFactoryNETDataSet.Lotto)
                        {
                            lotto.Stato = (int)StatoLotto.Eseguito;

                            qtàProd += lotto.Quantità;

                            lottoTableAdapter.Update(lotto);
                            lottoTableAdapter.RegistraConsumoMagazzino(lotto.OID);

                        }

                        prod.Quantità += qtàProd;
                        silosTableAdapter.FillByCodice(ds.Silos, prod.Silos);
                        if (prod.SilosRow != null)
                            prod.SilosRow.Quantità += qtàProd;
                        ds.Odl[0].QuantitàEffettiva += qtàProd;

                        lottoTableAdapter.Update(ds);
                        silosTableAdapter.Update(ds);
                        odlTableAdapter.Update(ds);
                        tran.Commit();
                        success = true;
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        retries++;
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            }


        }


        private void timerKey_Tick(object sender, EventArgs e)
        {
            timerKey.Enabled = false;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            ReadComm();
        }

        private void timer2_Tick_1(object sender, EventArgs e)
        {
            ReadComm();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }



   }
}