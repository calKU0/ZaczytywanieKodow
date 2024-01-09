using System.Diagnostics;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System;
using System.Collections.Generic;
using ZaczytywanieKodow.Models;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace ZaczytywanieKodow
{
    public partial class ListaKodow : Form
    {
        private List<Item> items { get; set; }
        private bool stop;
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
            if (nazwaPlikuTextBox.Text != "") { ZaczytajButton.BackColor = System.Drawing.Color.Lime; }
        }

        private async void ZaczytajButton_Click(object sender, EventArgs e)
        {
            try
            {

                stop = false;
                AnulujButton.Enabled = true;
                items = new List<Item>();
                kodyLista.Columns.AddRange(new DataGridViewColumn[] { kodSystem, kodDostawcy, kodOem, dostawca, wyszukiwania, polaczoneNumery });
                Excel plik = new Excel(nazwaPlikuTextBox.Text);
                plik.Otworz();

                this.smoothProgressBar1.Value = 0;
                this.smoothProgressBar1.Visible = true;
                this.smoothProgressBar1.Minimum = 0;
                this.smoothProgressBar1.Maximum = plik.PobierzIloscWierszy();

                Stopwatch sw;
                long totalTime = 0;
                int secondsLeft;
                int minutesLeft;
                string timeLeft;
                await Task.Run(() =>
                {
                    for (int i = 2; i <= plik.PobierzIloscWierszy() + 2; i++)
                    {
                        sw = Stopwatch.StartNew();
                        Item item = plik.CzytajWiersz(i);

                        if (item.KodOem == "" || stop) { break; }
                        item.Id = i - 2;
                        items.Add(item);

                        this.smoothProgressBar1.Value++;
                        totalTime = totalTime + sw.ElapsedMilliseconds;
                        secondsLeft = (int)Math.Floor((double)totalTime / i / 1000 * (plik.PobierzIloscWierszy() + 2 - i));
                        minutesLeft = (int)Math.Floor((decimal)secondsLeft / 60);
                        timeLeft = (secondsLeft - (minutesLeft * 60)) < 10 ? minutesLeft.ToString() + ":0" + (secondsLeft - (minutesLeft * 60)).ToString() : minutesLeft.ToString() + ":" + (secondsLeft - (minutesLeft * 60)).ToString();

                        textBox1.Invoke(new Action(delegate ()
                        {
                             textBox1.Visible = true;
                        }));
                        textBox2.Invoke(new Action(delegate ()
                        {
                            textBox2.Visible = true;
                            textBox2.Text = timeLeft;
                        }));
                        textBox3.Invoke(new Action(delegate ()
                        {
                            textBox3.Visible = true;
                            textBox3.Text = $"{i - 1}/{plik.PobierzIloscWierszy()}";
                        }));
            }
                });

                var groupedData = items
                .GroupBy(item => item.KodDostawcy)
                .Select(group =>
                {
                    string kodDostawcy = group.Key;
                    string combinedOem = string.Join(", ", group.Select(item => item.KodOem));
                    return new Item { KodDostawcy = kodDostawcy, PolaczoneKody = combinedOem };
                })
                .ToList();

                foreach (var groupedItem in groupedData)
                {
                    var existingItems = items.Where(item => item.KodDostawcy == groupedItem.KodDostawcy);

                    foreach (var existingItem in existingItems)
                    {
                        existingItem.PolaczoneKody = groupedItem.PolaczoneKody;
                    }
                }

                foreach (var item in items)
                {
                    if (!item.WieleKodow)
                    {
                        kodyLista.Rows.Add(item.KodSystem[0], item.KodDostawcy, item.KodOem, item.Dostawca[0], item.Wyszukiwania, item.PolaczoneKody);
                    }
                    else
                    {
                        kodyLista.Rows.Add("Wybierz", item.KodDostawcy, item.KodOem, "", item.Wyszukiwania, item.PolaczoneKody);
                        kodyLista.Rows[item.Id].Cells["kodSystem"].Style.BackColor = System.Drawing.Color.Red;
                    }
                }

                this.smoothProgressBar1.Value = this.smoothProgressBar1.Maximum;
                timeLeft = "0:00";
                textBox2.Text = timeLeft;
                textBox3.Text = $"{plik.PobierzIloscWierszy()}/{plik.PobierzIloscWierszy()}";
                AnulujButton.Enabled = false;
            }
            catch (Exception ex) { Prompt.ShowDialog("Napotkano b³¹d przy próbie zaczytania pliku Excel" + ex, "B³¹d"); }
        }

        private void AnulujButton_Click(object sender, EventArgs e)
        {
            stop = true;
            kodyLista.Rows.Clear();
            kodyLista.Refresh();
            items.Clear();
            AnulujButton.Enabled = false;
            MessageBox.Show("Zatrzymano zaczytywanie pliku");
        }

        private void kodyLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == kodyLista.Columns["kodSystem"].Index && e.RowIndex >= 0 && items.ElementAt(e.RowIndex).WieleKodow == true)
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
                        kodyLista.Rows[item.Id].Cells["kodSystem"].Style.BackColor = DefaultBackColor;
                    }
                }
            }
        }
    }
}