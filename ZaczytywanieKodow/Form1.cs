using System.Diagnostics;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System;

namespace ZaczytywanieKodow
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void WybierzPlikButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog plikExcel = new OpenFileDialog();
            plikExcel.InitialDirectory = @"C:\";
            plikExcel.Title = "Wybierz plik Excel";
            plikExcel.DefaultExt = "xls";
            plikExcel.Filter = "Pliki Excel (*.xlsx)|*.xlsx";
            plikExcel.CheckFileExists = true;
            plikExcel.CheckPathExists = true;
            plikExcel.ShowDialog();
            nazwaPlikuTextBox.Text = plikExcel.FileName;
        }

        private void ZaczytajButton_Click(object sender, EventArgs e)
        {
            ZaczytajButton.BackColor = System.Drawing.Color.Lime;
            kodyLista.Columns.AddRange(new DataGridViewColumn[] { kodSystem, kodDostawcy, kodOem, dostawca, wyszukiwania, polaczoneNumery });
            Excel plik = new Excel(nazwaPlikuTextBox.Text);
            plik.Otworz();

            this.smoothProgressBar1.Value = 0;
            this.smoothProgressBar1.Visible = true;
            this.smoothProgressBar1.Minimum = 0;
            this.smoothProgressBar1.Maximum = plik.PobierzIloscWierszy();

            for (int i = 2; i <= plik.PobierzIloscWierszy() + 2; i++)
            {
                this.smoothProgressBar1.Value++;
                plik.WyszukajDane(i);
                if (plik.kodOem != "")
                {
                    if (plik.IloscWierszy == 1)
                    {
                        kodyLista.Rows.Add(plik.kodSystemowy, plik.kodDostawcy, plik.kodOem, plik.dostawca, plik.wyszukiwania, "test");

                        //Co 10 wype³nieñ odœwie¿amy stronê. Nie ustawiaæ czêœciej, bo mo¿e wolniej zaczytywaæ
                        if (i%5 == 0){ Application.DoEvents(); }
                    }
                    else
                    {
                        kodyLista.Rows.Add("Wybierz", "", plik.kodOem, "", "", "");
                    }
                }
                else { break; }
            }
        }

        private void kodyLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == kodyLista.Columns["kodSystem"].Index && e.RowIndex >= 0 && (string)kodyLista.Rows[e.RowIndex].Cells["kodSystem"].Value == "Wybierz")
            {
                using (var forma = new WyborTowaru(Excel.WyszukajDane((string)kodyLista.Rows[e.RowIndex].Cells["kodOem"].Value)))
                {
                    var result = forma.ShowDialog();

                    if (result == DialogResult.OK) // jesli forma zwraca wynik
                    {
                        string wybrany_towar = forma.ReturnValue1;
                        kodyLista.Rows[e.RowIndex].Cells["kodSystem"].Value = wybrany_towar;
                    }
                }
            }
        }
    }
}