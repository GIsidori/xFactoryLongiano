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

namespace XFactoryNET.WinCE
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
            this.giacenzeSilosTableAdapter.Connection = conn;


            this.serialPort1.PortName = MobileConfiguration.Settings["PortName"];
            this.serialPort1.Parity = (System.IO.Ports.Parity) Enum.Parse(typeof(System.IO.Ports.Parity),MobileConfiguration.Settings["Parity"],true);
            this.serialPort1.DataBits = int.Parse(MobileConfiguration.Settings["DataBits"]);
            this.serialPort1.StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits),MobileConfiguration.Settings["StopBits"],true);
            this.serialPort1.BaudRate = int.Parse(MobileConfiguration.Settings["BaudRate"]);

            FieldInfo fi = dataGrid1.GetType().GetField("m_sbVert", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance);
            VScrollBar bar = (VScrollBar)fi.GetValue(dataGrid1);
            bar.Width = 50;
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
            double peso;
            enumStatoBilancia stato = enumStatoBilancia.Errore;

            try
            {
                peso = double.Parse(data.Substring(2, 5)) / 1000;    //Peso in kg.
                stato = (enumStatoBilancia)int.Parse(data.Substring(1, 1));

                PesoBilancia = peso;
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
            {
                string data = serialPort1.ReadLine();
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
                if (currIngr>0)
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
                }
                else
                {
                    btnDone.Text = "Peso";
                    lblDiff.Visible = true;
                    label7.Visible = true;
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
                    if (skipCheck || cRow.Quantità >= cRow.QtàTeo || (cRow.Tolleranza != 0 && (cRow.Quantità - cRow.QtàTeo) <= cRow.Tolleranza))
                    //(cRow.Tolleranza != 0 && (cRow.Qtà - cRow.QtàTeo) <= cRow.Tolleranza) ||
                    //(cRow.TolleranzaPerc != 0 && (cRow.Qtà - cRow.QtàTeo) / (cRow.QtàTeo) <= cRow.TolleranzaPerc))
                    {
                        RegistraConsumo();
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
                if (NextMisc() == false)
                    CheckOdl();
        }

        private XFactoryNETDataSet.LottoRow cRow;


        private void ShowDiff()
        {
            double diff = 0;
            //diff = PesoBilancia - cRow.QtàTeo - sEstr - TaraBilancia + sEstrPrec;
            if (cRow != null)
            {
                //diff = (PesoBilancia - cRow.QtàTeo - TaraBilancia);
                diff = PesoBilancia - cRow.QtàTeo - TaraBilancia + sEstrPrec;
                double absDiff = Math.Abs(diff);
                if (absDiff < cRow.Tolleranza)
                    lblDiff.ForeColor = Color.Lime;
                else if (diff < 0)
                {
                    lblDiff.ForeColor = Color.Yellow;
                }
                else
                    lblDiff.ForeColor = Color.Red;
            }
            lblDiff.Text = string.Format("{0:n3}", diff);
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (timerKey.Enabled)
                return;
            timerKey.Enabled = true;
            LeggiPeso();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Program.frmGiacenze.Show();
            //this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Program.frmSacchi.Show();
            //this.Hide();
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
            if (MessageBox.Show("Confermi la chiusura del programma", "XFactory.WinCE", MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.No)
                e.Cancel = true;
        }

        private void btnFineIngr_Click(object sender, EventArgs e)
        {
            if (timerKey.Enabled)
                return;
            LeggiPeso(true);
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
                RegistraConsumo();
                Next();
            }
        }

        private void RegistraConsumo()
        {

            cRow.Stato = (int)StatoLotto.Eseguito;

            //giacenzeSacchiTableAdapter.Fill(xFactoryNETDataSet.GiacenzaSacchi);
            //if (cRow.GiacenzaSacchiRowParent != null)
            //{
            //    cRow.GiacenzaSacchiRowParent.Quantità -= cRow.Quantità;
            //    giacenzeSacchiTableAdapter.Update(cRow.GiacenzaSacchiRowParent);
            //}
            lottoTableAdapter.Update(cRow);

            //Incrementa giacenza prodotto finito;
            using (XFactoryNETDataSet ds = new XFactoryNETDataSet())
            {
                lottoTableAdapter.FillProdottiOdl(ds.Lotto, currMisc, odl.OID);
                foreach (var row in ds.Lotto)
                {
                    double qtàProd = cRow.Quantità;
                    row.Quantità += qtàProd;
                    silosTableAdapter.FillByCodice(ds.Silos, row.Silos);
                    if (row.SilosRow != null)
                        row.SilosRow.Quantità += qtàProd;
                    //Gestire la produzione dei sottoprodotti
                    break;  //Per ora fa solo il primo prodotto
                }
                lottoTableAdapter.Update(ds);
                silosTableAdapter.Update(xFactoryNETDataSet);
            }
            
            //Decrementa giacenza materia prima
            //cRow.SilosRow.Quantità -= cRow.Quantità;


        }

        private void timerKey_Tick(object sender, EventArgs e)
        {
            timerKey.Enabled = false;
        }

   }
}