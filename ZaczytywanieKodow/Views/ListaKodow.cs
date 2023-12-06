using System.Diagnostics;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System;
using System.Collections.Generic;
using ZaczytywanieKodow.Models;
using System.Linq;

namespace ZaczytywanieKodow
{
    public partial class ListaKodow : Form
    {
        private List<Item> items { get; set; }
        public ListaKodow()
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
            items = new List<Item>();
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
                Item item = plik.Czytaj(i);
                if (item.KodOem == "") { break; } 
                item.Id = i - 2;
                item.PolaczoneKody = "";
                items.Add(item);
                if (i % 5 == 0) { Application.DoEvents(); }
                this.smoothProgressBar1.Value++;

            }
            foreach (var item in items)
            {
                if (item.KodSystem.Count == 1)
                {
                    kodyLista.Rows.Add(item.KodSystem[0], item.KodDostawcy, item.KodOem, item.Dostawca[0], item.Wyszukiwania, item.PolaczoneKody);
                }
                else
                {
                    kodyLista.Rows.Add("Wybierz", item.KodDostawcy, item.KodOem, "", item.Wyszukiwania, item.PolaczoneKody);
                }
            }
        }

        private void kodyLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == kodyLista.Columns["kodSystem"].Index && e.RowIndex >= 0 && (string)kodyLista.Rows[e.RowIndex].Cells["kodSystem"].Value == "Wybierz")
            {
                var item = items.Where((Item arg) => arg.Id == e.RowIndex).FirstOrDefault();
                using (var forma = new WyborTowaru(item))
                {
                    var result = forma.ShowDialog();

                    if (result == DialogResult.OK) // jesli forma zwraca wynik
                    {
                        string wybranyKod = forma.ReturnValue1;
                        string dostawca = forma.ReturnValue2;
                        kodyLista.Rows[e.RowIndex].Cells["kodSystem"].Value = wybranyKod;
                        kodyLista.Rows[e.RowIndex].Cells["dostawca"].Value = dostawca;
                    }
                }
            }
        }
    }
}