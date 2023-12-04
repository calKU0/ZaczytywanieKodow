using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            ZaczytajButton.BackColor = SystemColors.ButtonFace;
        }

        private void ZaczytajButton_Click(object sender, EventArgs e)
        {
            ZaczytajButton.BackColor = System.Drawing.Color.Lime;
            kodyLista.Columns.AddRange(new DataGridViewColumn[] { kodSystem, kodDostawcy, kodOem, dostawca, wyszukiwania, polaczoneNumery });
            Excel plik = new Excel(nazwaPlikuTextBox.Text);
            plik.Otworz();
            for (int i = 2; i <= plik.PobierzIloscWierszy() + 2; i++)
            {
                plik.WyszukajDane(i);

                if (plik.IloscWierszy == 1)
                {
                    kodyLista.Rows.Add(plik.kodSystemowy, plik.kodDostawcy, plik.kodOem, plik.dostawca, plik.wyszukiwania, "test");
                }
                else
                {
                    kodyLista.Rows.Add("Wybierz", "", "", "", "", "");
                }

            }
        }

        private void kodyLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == kodyLista.Columns["kodSystem"].Index && e.RowIndex >= 0 && (string)kodyLista.Rows[e.RowIndex].Cells["kodSystem"].Value == "Wybierz")
            {
                using (var forma = new WyborTowaru(Excel.WyszukajDane((string)kodyLista.Rows[e.RowIndex].Cells[1].Value)))
                {
                    var result = forma.ShowDialog();

                    if (result == DialogResult.OK) // jesli forma zwraca wynik
                    {
                        string wybrany_towar = forma.ReturnValue1;
                    }
                }
            }
        }
    }
}