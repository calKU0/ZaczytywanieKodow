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
using System.Drawing;
using System.Configuration;
using cdn_api;

namespace ZaczytywanieKodow
{
    public partial class ListaKodow : Form
    {
        private List<Item> items { get; set; } = new List<Item>();
        private bool stop;
        private List<GrouppedItem> grouppedItems { get; set; } = new List<GrouppedItem>();
        public static readonly int APIVersion = 20231;
        public static int IDSesjiXL = 0;
        public static int kontrahentGidNumer = 0;
        public static int kontrahentGidTyp = 0;
        public static string kontrahentAkronim = "";
        public ListaKodow()
        {
            InitializeComponent();
            kodyLista.Columns.AddRange(new DataGridViewColumn[] { twrGidNumer, kodSystem, nazwa, kodDostawcy, dostawca, cena, ostCenaZak, waluta, polaczoneNumery, zastosowanie, wyszukiwania, uwagi, szczegoly });
        }

        public void ListaKodow_Load(object sender, EventArgs e)
        {
            try
            {
                cdn_api.XLLoginInfo_20231 xlLoginInfo = new cdn_api.XLLoginInfo_20231();
                xlLoginInfo.ProgramID = "ZaczytywanieKodow";
                xlLoginInfo.Winieta = -1;
                xlLoginInfo.Wersja = APIVersion;
                xlLoginInfo.Baza = ConfigurationManager.AppSettings["XLBaza"];
                xlLoginInfo.OpeIdent = ConfigurationManager.AppSettings["XLLogin"];
                xlLoginInfo.OpeHaslo = ConfigurationManager.AppSettings["XLHas³o"];
                Int32 wynik = cdn_api.cdn_api.XLLogin(xlLoginInfo, ref IDSesjiXL);

                if (wynik != 0)
                {
                    MessageBox.Show("B³¹d logowania");
                    Close();
                }

                if (xlLoginInfo.Baza == "")
                {
                    MessageBox.Show("Nie zalogowano do XL-a, program koñczy swoje dzia³anie");
                    Close();
                }
            }

            catch (Exception ex)
            {

                MessageBox.Show("B³¹d logowania do XL-a, program koñczy swoje dzia³anie" + "\r" + ex.ToString());

                Close();
            }

        }
        private void ListaKodow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Wylogowanie(IDSesjiXL);
        }
        private void Wylogowanie(Int32 SessionID)
        {
            try
            {
                cdn_api.cdn_api.XLLogout(SessionID);

            }

            catch
            {
                MessageBox.Show("Nie uda³o siê wylogowaæ");
            }
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
            this.smoothProgressBar1.Maximum = iloscWierszy - 1;

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
                        if (item.KodDostawcy == "" || stop) { iloscWierszy = i - 2; plik.Zamknij(); return; }
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
                plik.Zamknij();
            }
            catch (Exception ex) { plik.Zamknij(); MessageBox.Show($"Napotkano b³¹d przy próbie zaczytania pliku Excel\n{ex}", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            try
            {
                var groupedData = items
                .GroupBy(item => item.KodDostawcy)
                .Select(group =>
                {
                    string kodDostawcy = group.Key;
                    string combinedOem = string.Join(", ", group.Select(item => item.KodOem));
                    string zastosowanie = string.Join(", ", group.Select(item => item.Zastosowanie).Distinct());
                    return new Item { KodDostawcy = kodDostawcy, PolaczoneKody = combinedOem, Zastosowanie = zastosowanie};
                })
                .ToList();

                foreach (var groupedItem in groupedData)
                {
                    var existingItems = items.Where(item => item.KodDostawcy == groupedItem.KodDostawcy);

                    foreach (var existingItem in existingItems)
                    {
                        existingItem.PolaczoneKody = groupedItem.PolaczoneKody;
                        existingItem.PolaczoneZastosowanie = groupedItem.Zastosowanie;
                    }
                }

                HashSet<string> uniqueKodDostawcy = new HashSet<string>();
                int index = 0;

                foreach (var item in items)
                {
                    if (!uniqueKodDostawcy.Contains(item.KodDostawcy))
                    {
                        GrouppedItem grouppedItem = new GrouppedItem();
                        grouppedItem.Id = index;
                        grouppedItem.KodDostawcy = item.KodDostawcy;
                        grouppedItem.CenaZakupu = item.CenaZakupu;
                        grouppedItem.PolaczoneKody = item.PolaczoneKody;
                        grouppedItem.Zastosowanie = item.PolaczoneZastosowanie;
                        grouppedItem.KodSystem = items
                                     .Where(i => i.PolaczoneKody == item.PolaczoneKody)
                                     .SelectMany(i => i.KodSystem)
                                     .ToList();
                        grouppedItem.Nazwa = items
                                     .Where(i => i.PolaczoneKody == item.PolaczoneKody)
                                     .SelectMany(i => i.Nazwa)
                                     .ToList();
                        grouppedItem.TwrGidNumer = items
                                     .Where(i => i.PolaczoneKody == item.PolaczoneKody)
                                     .SelectMany(i => i.TwrGidNumer)
                                     .ToList();
                        grouppedItem.Dostawca= items
                                     .Where(i => i.PolaczoneKody == item.PolaczoneKody)
                                     .SelectMany(i => i.Dostawca)
                                     .ToList();
                        grouppedItem.OstatniaCenaZakupu = items
                                     .Where(i => i.PolaczoneKody == item.PolaczoneKody)
                                     .SelectMany(i => i.OstatniaCenaZakupu)
                                     .ToList();
                        grouppedItem.Waluta = items
                                     .Where(i => i.PolaczoneKody == item.PolaczoneKody)
                                     .SelectMany(i => i.Waluta)
                                     .ToList();
                        grouppedItem.Wyszukiwania = items
                                     .Where(i => i.PolaczoneKody == item.PolaczoneKody)
                                     .Sum(i => i.Wyszukiwania);

                        grouppedItems.Add(grouppedItem);
                        index += 1;
                        uniqueKodDostawcy.Add(item.KodDostawcy);

                        if (grouppedItem.TwrGidNumer.Count() >= 1)
                        {
                            grouppedItem.WieleKodow = true;
                            int indexList = grouppedItem.TwrGidNumer.FindIndex(x => x.Equals(0));
                            while (indexList != -1)
                            {
                                grouppedItem.TwrGidNumer.RemoveAt(indexList);
                                grouppedItem.Nazwa.RemoveAt(indexList);
                                grouppedItem.KodSystem.RemoveAt(indexList);
                                grouppedItem.Dostawca.RemoveAt(indexList);
                                grouppedItem.OstatniaCenaZakupu.RemoveAt(indexList);
                                grouppedItem.Waluta.RemoveAt(indexList);
                                indexList = grouppedItem.TwrGidNumer.FindIndex(x => x.Equals(0));
                            }
                        }
                        else
                        {
                            grouppedItem.WieleKodow = false;
                        }
                    }
                }

                grouppedItems = grouppedItems.OrderByDescending(x => x.Wyszukiwania).ToList();
                foreach (var grouppedItem in grouppedItems)
                {
                    index = kodyLista.Rows.Add(0, "Wybierz kartê", String.Empty, grouppedItem.KodDostawcy, String.Empty, grouppedItem.CenaZakupu, 0.00.ToString("0.00"), "", grouppedItem.PolaczoneKody, grouppedItem.Zastosowanie, grouppedItem.Wyszukiwania, String.Empty, "szczegó³y");
                    grouppedItem.Id = index;
                    if (grouppedItem.TwrGidNumer.Count() >= 1)
                    {
                        kodyLista.Rows[index].Cells["kodSystem"].Style.ForeColor = Color.MediumVioletRed;
                        kodyLista.Rows[index].Cells["kodSystem"].Style.BackColor = Color.MediumVioletRed;
                        kodyLista.Rows[index].Cells["kodSystem"].Style.SelectionBackColor = Color.MediumVioletRed;
                        kodyLista.Rows[index].Cells["kodSystem"].Style.SelectionForeColor = Color.MediumVioletRed;
                    }
                }
                foreach (DataGridViewRow dr in this.kodyLista.Rows)
                {
                    if (dr.Cells["twrGidNumer"].Value.ToString() == "0")
                    {
                        DataGridViewTextBoxCell szczegolyTxtCell = new DataGridViewTextBoxCell();
                        dr.Cells["szczegoly"] = szczegolyTxtCell;
                    }        
                }

                this.smoothProgressBar1.Value = this.smoothProgressBar1.Maximum;
                timeLeft = "0:00";
                textBox2.Text = timeLeft;
                textBox3.Text = $"{iloscWierszy}/{iloscWierszy}";
                AnulujButton.Enabled = false;
                WyczyscButton.Enabled = true;
                WybierzPlikButton.Enabled = true;
                deleteRowButton.Visible = true;
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
            /*try
            {
                if (e.ColumnIndex == kodyLista.Columns["kodSystem"].Index
                    && e.RowIndex >= 0
                    && kodyLista.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Za³ó¿ kartê")
                {
                    var item = grouppedItems.Where((GrouppedItem arg) => arg.Id == e.RowIndex).FirstOrDefault();
                    using (var forma = new DodajTowar(item))
                    {
                        var result = forma.ShowDialog();
                        if (forma.DialogResult == DialogResult.OK)
                        {
                            item.TwrGidNumer.Add(forma.GidZalozonegoTowaru);
                            item.Nazwa.Add(forma.Nazwa);
                            item.KodSystem.Add(forma.Kod);
                            item.Dostawca.Add(kontrahentAkronim);
                            item.OstatniaCenaZakupu.Add(Convert.ToDecimal(0.00));
                            item.Waluta.Add("");

                            kodyLista.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "Wybierz kartê";
                        }
                    }
        }
    }
            catch(Exception ex) { MessageBox.Show("Wyst¹pi³ b³¹d przy próbie za³o¿enia karty " + ex, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            */
            try
            {
                if (e.ColumnIndex == kodyLista.Columns["kodSystem"].Index 
                    && e.RowIndex >= 0 
                    && kodyLista.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "Za³ó¿ kartê")
                {
                    var item = grouppedItems.Where(i => i.PolaczoneKody == kodyLista.Rows[e.RowIndex].Cells["polaczoneNumery"].Value.ToString()).FirstOrDefault();
                    using (var forma = new WyborTowaru(item, APIVersion))
                    {
                        var result = forma.ShowDialog();

                        if (result == DialogResult.OK) // jesli forma zwraca wynik
                        {
                            string wybranyKod = forma.ReturnValue1;
                            string nazwa = forma.ReturnValue2;
                            string dostawca = forma.ReturnValue3;
                            string twrGid = forma.ReturnValue4;
                            string ostCenaZak = forma.ReturnValue5;
                            string waluta = forma.ReturnValue6;
                            grouppedItems[e.RowIndex] = forma.item;

                            kodyLista.Rows[e.RowIndex].Cells["nazwa"].Value = nazwa;
                            kodyLista.Rows[e.RowIndex].Cells["dostawca"].Value = dostawca;
                            kodyLista.Rows[e.RowIndex].Cells["twrGidNumer"].Value = twrGid;
                            kodyLista.Rows[e.RowIndex].Cells["ostCenaZak"].Value = ostCenaZak;
                            kodyLista.Rows[e.RowIndex].Cells["waluta"].Value = waluta;
                            kodyLista.Rows[e.RowIndex].Cells["kodSystem"].Style.BackColor = DefaultBackColor;

                            if (kodyLista.Rows[e.RowIndex].Cells["twrGidNumer"].Value.ToString() == "0")
                            {
                                DataGridViewTextBoxCell szczegolyTxtCell = new DataGridViewTextBoxCell();
                                szczegolyTxtCell.Value = "szczegó³y";
                                kodyLista.Rows[e.RowIndex].Cells["szczegoly"] = szczegolyTxtCell;
                            }
                            else
                            {
                                DataGridViewButtonCell szczegolyButtonCell = new DataGridViewButtonCell();
                                szczegolyButtonCell.Value = "szczegó³y";
                                szczegolyButtonCell.Style = kodyLista.Columns["szczegoly"].DefaultCellStyle;
                                kodyLista.Rows[e.RowIndex].Cells["szczegoly"] = szczegolyButtonCell;

                                DataGridViewTextBoxCell kodSystemTxtCell = new DataGridViewTextBoxCell();
                                kodSystemTxtCell.Value = wybranyKod;
                                kodyLista.Rows[e.RowIndex].Cells["kodSystem"] = kodSystemTxtCell;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Wyst¹pi³ b³¹d przy próbie wybrania kodu systemowego " + ex, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            try
            {
                if (e.ColumnIndex == kodyLista.Columns["szczegoly"].Index && e.RowIndex >= 0)
                {
                    if (kodyLista.Rows[e.RowIndex].Cells["twrGidNumer"].Value.ToString() != "0")
                    {
                        using (var forma = new Szczegoly(Convert.ToInt32(kodyLista.Rows[e.RowIndex].Cells["twrGidNumer"].Value)))
                        {
                            forma.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Wyst¹pi³ b³¹d przy próbie wejœcia w szczegó³y " + ex, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void kodyLista_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.ColumnIndex == kodyLista.Columns["wyszukiwania"].Index 
                    || e.ColumnIndex == kodyLista.Columns["polaczoneNumery"].Index 
                    || e.ColumnIndex == kodyLista.Columns["zastosowanie"].Index)
                    && e.RowIndex >= 0)


                {
                    using (var forma = new WyszukiwaniaForm(items
                                     .Where(i => i.PolaczoneKody == grouppedItems.ElementAt(e.RowIndex).PolaczoneKody)
                                     .ToList()))
                    {
                        forma.ShowDialog();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Wyst¹pi³ b³¹d przy próbie zaczytania wyszukiwañ " + ex, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void kodyLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == kodyLista.Columns["wyszukiwania"].Index 
                || e.ColumnIndex == kodyLista.Columns["polaczoneNumery"].Index
                || e.ColumnIndex == kodyLista.Columns["zastosowanie"].Index) 
                && e.RowIndex >= 0)
            {
                var cell = kodyLista.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ToolTipText = "Kliknij podwójnie, aby wyœwietliæ poszczególne wyszukiwania kodów OEM";
            }
            else if (e.ColumnIndex == kodyLista.Columns["szczegoly"].Index)
            {
                var cell = kodyLista.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ToolTipText = "Kliknij, aby wyœwietliæ szczegó³y wyszukiwañ karty towarowej";
            }
        }

        private void Wyczysc()
        {
            items.Clear();
            grouppedItems.Clear();
            kodyLista.Rows.Clear();
            kodyLista.Refresh();
            AnulujButton.Enabled = false;
            nazwaPlikuTextBox.Text = String.Empty;
            ZaczytajButton.Enabled = false;
            WyczyscButton.Enabled = false;
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

        private void DeleteRowButton_Click(object sender, EventArgs e)
        {
            if (kodyLista.CurrentCell.RowIndex < 0)
            {
                MessageBox.Show("Nie zaznaczono ¿adnego wiersza\nZaznacz wiersz i spróbuj ponownie", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            kodyLista.Rows.RemoveAt(kodyLista.CurrentCell.RowIndex);
        }
    }
}