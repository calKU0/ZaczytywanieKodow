using System.Diagnostics;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using ZaczytywanieKodow.Models;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using ZaczytywanieKodow.Views;
using System.Linq.Expressions;

namespace ZaczytywanieKodow
{
    public partial class ListaKodow : Form
    {
        private List<Item> items { get; set; } = new List<Item>();
        private bool stop;
        public ListaKodow()
        {
            InitializeComponent();
            kodyLista.Columns.AddRange(new DataGridViewColumn[] { twrGidNumer,kodSystem, kodDostawcy, kodOem, dostawca, polaczoneNumery, wyszukiwania, szczegoly });
        }

        private void WybierzPlikButton_Click(object sender, EventArgs e)
        {
            if (kodyLista.Rows.Count > 0)
            {
                var result = MessageBox.Show("Zaczytanie nowego pliku spowoduje usuniêcie aktualnych danych\nCzy napewno chcesz wczytaæ nowy plik?","PotwierdŸ",MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result != DialogResult.Yes)
                {
                    return;
                }
                Wyczysc();
            }

            OpenFileDialog plikExcel = new OpenFileDialog();
            plikExcel.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            plikExcel.Title = "Wybierz plik Excel";
            plikExcel.DefaultExt = "xls";
            plikExcel.Filter = "Pliki Excel (*.xlsx)|*.xlsx";
            plikExcel.CheckFileExists = true;
            plikExcel.CheckPathExists = true;
            plikExcel.ShowDialog();
            nazwaPlikuTextBox.Text = plikExcel.FileName;
            if (nazwaPlikuTextBox.Text != string.Empty) { ZaczytajButton.Enabled = true; WyczyscButton.Enabled = true; }
        }

        private async void ZaczytajButton_Click(object sender, EventArgs e)
        {
            Excel plik = new Excel(nazwaPlikuTextBox.Text);
            bool otwarty = plik.Otworz();

            if (!otwarty)
            {
                Wyczysc();
                return;
            }

            int iloscWierszy = plik.PobierzIloscWierszy();
            stop = false;
            AnulujButton.Enabled = true;
            ZaczytajButton.Enabled = false;
            WyczyscButton.Enabled = false;
            WybierzPlikButton.Enabled = false;

            this.smoothProgressBar1.Value = 0;
            this.smoothProgressBar1.Visible = true;
            this.smoothProgressBar1.Minimum = 0;
            this.smoothProgressBar1.Maximum = iloscWierszy;

            Stopwatch sw;
            long totalTime = 0;
            int secondsLeft;
            int minutesLeft;
            string timeLeft;

            try
            {
                await Task.Run(() =>
                {
                    for (int i = 2; i <= iloscWierszy + 2; i++)
                    {
                        sw = Stopwatch.StartNew();

                        Item item = plik.CzytajWiersz(i);
                        if (item.KodOem == "" || stop) { iloscWierszy = i - 2; return; }
                        item.Id = i - 2;
                        items.Add(item);

                        this.smoothProgressBar1.Value++;
                        totalTime = totalTime + sw.ElapsedMilliseconds;
                        secondsLeft = (int)Math.Floor((double)totalTime / i / 1000 * (iloscWierszy + 2 - i));
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
                            textBox3.Text = $"{i - 1}/{iloscWierszy}";
                        }));
                    }
                });
            }
            catch (Exception ex) { MessageBox.Show($"Napotkano b³¹d przy próbie zaczytania pliku Excel\n{ex}", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            try
            {
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
                    int previousRowIndex = kodyLista.RowCount;
                    if (!item.WieleKodow)
                    {
                        kodyLista.Rows.Add(item.TwrGidNumer[0], item.KodSystem[0], item.KodDostawcy, item.KodOem, item.Dostawca[0], item.PolaczoneKody, item.Wyszukiwania, "szczegó³y");
                    }
                    else
                    {
                        kodyLista.Rows.Add(0, "Wybierz", item.KodDostawcy, item.KodOem, "", item.PolaczoneKody, item.Wyszukiwania, "szczegó³y");
                        kodyLista.Rows[previousRowIndex].Cells["kodSystem"].Style.BackColor = System.Drawing.Color.Red;
                    }
                }

                this.smoothProgressBar1.Value = this.smoothProgressBar1.Maximum;
                timeLeft = "0:00";
                textBox2.Text = timeLeft;
                textBox3.Text = $"{iloscWierszy}/{iloscWierszy}";
                AnulujButton.Enabled = false;
                WyczyscButton.Enabled = true;
                WybierzPlikButton.Enabled = true;
            }
            catch (Exception ex) { MessageBox.Show("Napotkano b³¹d przy próbie wyœwietlenia wyników" + ex, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            
        }

        private void AnulujButton_Click(object sender, EventArgs e)
        {
            stop = true;
            Wyczysc();
            MessageBox.Show("Zatrzymano zaczytywanie pliku", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void kodyLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
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
                            string twrGid = forma.ReturnValue3;

                            kodyLista.Rows[e.RowIndex].Cells["kodSystem"].Value = wybranyKod;
                            kodyLista.Rows[e.RowIndex].Cells["dostawca"].Value = dostawca;
                            kodyLista.Rows[e.RowIndex].Cells["twrGidNumer"].Value = twrGid;
                            kodyLista.Rows[e.RowIndex].Cells["kodSystem"].Style.BackColor = DefaultBackColor;
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Wyst¹pi³ b³¹d przy próbie wybrania kodu systemowego " + ex, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            try
            {
                if (e.ColumnIndex == kodyLista.Columns["szczegoly"].Index && e.RowIndex >= 0)
                {
                    if ((string)kodyLista.Rows[e.RowIndex].Cells["kodSystem"].Value != "" && (string)kodyLista.Rows[e.RowIndex].Cells["kodSystem"].Value != "Wybierz")
                    {
                        using (var forma = new Szczegoly((int)kodyLista.Rows[e.RowIndex].Cells["twrGidNumer"].Value))
                        {
                            forma.ShowDialog();
                            //TO DO
                            //B³¹d przy otwieraniu szczegó³ów
                            //Zobaczyæ czy sortowanie dzia³a
                        }
                    }
                    else
                    {
                        MessageBox.Show("Brak kodu w XL", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Wyst¹pi³ b³¹d przy próbie wejœcia w szczegó³y " + ex, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void Wyczysc()
        {
            items.Clear();
            kodyLista.Rows.Clear();
            kodyLista.Refresh();
            AnulujButton.Enabled = false;
            ZaczytajButton.Enabled = false;
            WyczyscButton.Enabled = false;
            nazwaPlikuTextBox.Text = string.Empty;
            this.smoothProgressBar1.Visible = false;
            this.textBox1.Visible = false;
            this.textBox2.Visible = false;
            this.textBox3.Visible = false;
        }

        private void WyczyscButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Czy napewno chcesz usun¹æ wszystkie dane?","Potwierdzenie",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                return;
            }
            Wyczysc();
        }
    }
}