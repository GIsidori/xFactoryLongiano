using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XFactoryNET.Module.BusinessObjects;


namespace XFactoryNET.Custom.Panzoo.AggMan
{
    public partial class Form1 : Form
    {

        /*
         *
'Formato protocollo standard Coop. Bilanciai
'1° Carattere    $ (24h)
'2° Carattere    S (stabilità) (0=Stabile,1=Non Stabile;3=Non Valido)
'3° Carattere    Decine di migliaia
'4° Carattere    Migliaia
'5° Carattere    Centinaia
'6° Carattere    Decine
'7° Carattere    Unità
'8° Carattere    CR (0Dh)
         * 
         */

        private const decimal pesoMax = 25;
        XFactoryNETDataSet.OdlRow odl;      //Odl in corso
        int currMisc;                       //Miscelata in corso

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.odlTableAdapter.ClearBeforeFill = true;
            this.componenteTableAdapter.ClearBeforeFill = true;

            this.serialPort1.NewLine = new string((char)13,1);

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

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (e.EventType == System.IO.Ports.SerialData.Eof)
            {
                string data = serialPort1.ReadLine();
                decimal dPeso;
                if (decimal.TryParse(data.Substring(2, 5), out dPeso))
                {
                    PesoBilancia = dPeso/1000; //Peso in kg.
                    try
                    {
                        //StatoBilancia = (enumStatoBilancia)Enum.Parse(typeof(enumStatoBilancia), data.Substring(1, 1));
                        int i;
                        if (int.TryParse(data.Substring(1, 1), out i))
                            StatoBilancia = (enumStatoBilancia)i;
                        else
                            StatoBilancia = enumStatoBilancia.Errore;
                    }
                    catch
                    {
                        StatoBilancia = enumStatoBilancia.Errore;
                    }
                }
                else
                    StatoBilancia = enumStatoBilancia.Errore;
            }
        }

        private decimal taraBilancia;

        public decimal TaraBilancia
        {
            get { return taraBilancia; }
            set { taraBilancia = value; }
        }

        private decimal pesoBilancia;

        public decimal PesoBilancia
        {
            get { return pesoBilancia; }
            set { 
                pesoBilancia = value;
                if (pesoBilancia > pesoMax)
                {
                    txtPesoBilancia.ForeColor = Color.Red;
                }
                else
                    txtPesoBilancia.ForeColor = Color.Lime;
                txtPesoBilancia.Text = pesoBilancia.ToString("n4");
                ShowDiff();
            }
        }

        private enumStatoBilancia statoBilancia;

        private enumStatoBilancia StatoBilancia
        {
            get { return statoBilancia; }
            set {
                statoBilancia = value;
                switch (statoBilancia)
                {
                    case enumStatoBilancia.Errore:
                        txtPesoBilancia.ForeColor = Color.Purple;
                        lblStatoBilancia.Text = "Errore di lettura";
                        break;
                    case enumStatoBilancia.Stabile:
                        txtPesoBilancia.ForeColor = Color.Lime;
                        lblStatoBilancia.Text = "Peso stabile";
                        break;
                    case enumStatoBilancia.NonStabile:
                        txtPesoBilancia.ForeColor = Color.Yellow;
                        lblStatoBilancia.Text = "Peso non stabile";
                        break;
                    case enumStatoBilancia.NonValido:
                        txtPesoBilancia.ForeColor = Color.Red;
                        lblStatoBilancia.Text = "Peso non valido";
                        break;
                    default:
                        break;
                }
            }
        }

        private enum enumStatoBilancia
        {
            Errore=-1,
            Stabile=0,
            NonStabile=1,
            NonValido=3
        }


        private bool NextOdl()
        {
            
            bool result = false;
            
            this.odlTableAdapter.FillFirstByStato(this.xFactoryNETDataSet.Odl, (int)StatoOdL.InEsecuzione);
            if (xFactoryNETDataSet.Odl.Count != 0)
            {
                odl = ((DataRowView)odlBindingSource.Current).Row as XFactoryNETDataSet.OdlRow;
                currMisc = odl.MiscelataInCorso - 1;
                result = NextMisc();
            }
            return result;
        }

        private bool NextMisc()
        {

            if (odl != null && currMisc < odl.NumeroMiscelate)
            {
                currMisc++;
                this.xFactoryNETDataSet.Componente.Clear();
                this.componenteTableAdapter.Fill(this.xFactoryNETDataSet.Componente, odl.OID, currMisc);
                if (this.xFactoryNETDataSet.Componente.Count > 0)
                {
                    FasePesatura = enumFasePesatura.Tara;
                    return true;
                }
            }

            return false;
        }

        private bool NextIngr()
        {
            if (ingredientiBindingSource.Position < ingredientiBindingSource.Count - 1)
            {
                ingredientiBindingSource.MoveNext();
                return true;
            }
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
            set { 
                fasePesatura = value;
                if (fasePesatura == enumFasePesatura.Tara)
                {
                    lblFasePesatura.Text = "Eseguire Tara";
                    lblFasePesatura.ForeColor = Color.Red;
                }
                else
                {
                    lblFasePesatura.Text = "Eseguire Peso";
                    lblFasePesatura.ForeColor = Color.Lime;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CheckOdl();
        }

        private void CheckOdl()
        {
            timer1.Enabled = false;
            timer1.Enabled = !NextOdl();
        }

        private void timerBlink_Tick(object sender, EventArgs e)
        {
            if (FasePesatura == enumFasePesatura.Tara)
            {
                lblFasePesatura.Visible = !lblFasePesatura.Visible;
            }
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

        private decimal sEstr;
        private decimal sEstrPrec;

        private void LeggiPeso()
        {
            if (StatoBilancia == enumStatoBilancia.Stabile)
            {
                if (FasePesatura == enumFasePesatura.Tara)
                {
                    TaraBilancia = PesoBilancia;
                    FasePesatura = enumFasePesatura.Lordo;
                    sEstr = 0;
                }
                else
                {
                    decimal qtà = PesoBilancia - TaraBilancia;
                    sEstr += qtà;
                    cRow.Qtà = sEstrPrec + sEstr;
                    if (cRow.Qtà > cRow.QtàTeo || 
                        (cRow.Tolleranza != 0 && (cRow.Qtà - cRow.QtàTeo) <= cRow.Tolleranza) || 
                        (cRow.TolleranzaPerc != 0 && (cRow.Qtà - cRow.QtàTeo) / (cRow.QtàTeo) <= cRow.TolleranzaPerc))
                    {
                        cRow.Stato = (int) (StatoComponente.Eseguito);
                        componenteTableAdapter.Update(cRow);
                        sEstr = 0;
                        sEstrPrec = 0;
                        if (NextIngr())
                        {
                            if (PesoBilancia + cRow.QtàTeo < pesoMax)
                            {
                                FasePesatura = enumFasePesatura.Lordo;
                                TaraBilancia = PesoBilancia;
                            }
                            else
                            {
                                FasePesatura = enumFasePesatura.Tara;
                            }
                        }
                        else
                        {
                            sEstrPrec += sEstr;
                            FasePesatura = enumFasePesatura.Tara;
                            if (NextMisc() == false)
                                CheckOdl();
                        }

                    }
                }
            }
        }

        private XFactoryNETDataSet.ComponenteRow cRow
        {
            get
            {
                if (ingredientiBindingSource.Current != null)
                    return (ingredientiBindingSource.Current as DataRowView).Row as XFactoryNETDataSet.ComponenteRow;
                return null;
            }
        }

        private void ingredientiBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            bool visible = false;
            if (cRow != null)
                visible = cRow.IsModalitàNull() == false && cRow.Modalità == "ADD";

            lblSac.Visible = lblFasePesatura.Visible;

            ShowDiff();

        }

        private void ShowDiff()
        {
            decimal diff = 0;
            //diff = PesoBilancia - cRow.QtàTeo - sEstr - TaraBilancia + sEstrPrec;
            if (cRow != null)
            {
                diff = PesoBilancia - cRow.QtàTeo - TaraBilancia;
                decimal absDiff = Math.Abs(diff);
                if (absDiff / cRow.QtàTeo < cRow.TolleranzaPerc && cRow.TolleranzaPerc != 0 && absDiff < cRow.Tolleranza && cRow.Tolleranza != 0)
                    txtPesoDiff.ForeColor = Color.Lime;
                else if (diff < 0)
                {
                    txtPesoDiff.ForeColor = Color.Yellow;
                }
                else
                    txtPesoDiff.ForeColor = Color.Red;
            }
            txtPesoDiff.Text = string.Format("n4", diff);
        }
    }
}
