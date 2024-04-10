using cdn_api;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZaczytywanieKodow.Models;

namespace ZaczytywanieKodow.Views
{
    public partial class DodajTowar : Form
    {
        public int IdZalozonegoTowaru;
        public string Kod { get; set; }
        public string Nazwa { get; set; }
        private string Grupa6  { get; set; }
        private string Grupa { get; set; }
        private string CenaDst { get; set; }
        private string KodPcn { get; set; }
        private string KodDostawcy { get; set; }
        private List<string> NumeryOEM { get; set; }
        private string WalutaDostawcy { get; set; }
        private string Search { get; set; } = "";
        public int GidZalozonegoTowaru { get; set; }
        public string KodZalozonegoTowaru { get; set; }
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["GaskaConnectionString"].ConnectionString;

        public DodajTowar(GrouppedItem item)
        {
            CenaDst = item.CenaZakupu;
            NumeryOEM = item.PolaczoneKody.Split(',').ToList();
            KodDostawcy = item.KodDostawcy;
            InitializeComponent();
        }

        private void DodajTowar_Load(object sender, EventArgs e)
        {
            string[] waluty = { "PLN", "EUR", "RON" };
            walutaDostawcyComboBox.Items.AddRange(waluty);

            kodDostawcyTextBox.Text = KodDostawcy;
            cenaDstTextBox.Text = CenaDst;

            foreach (string OEM in NumeryOEM)
            {
                kodyOemDataGridView.Rows.Add(null, OEM.Trim(), "");
            }
        }

        private void DodajButton_Click(object sender, EventArgs e)
        {
            try
            {
                Kod = kodTextBox.Text.Trim();
                Nazwa = nazwaTextBox.Text.Trim();
                KodPcn = kodPCNTextBox.Text.Trim();
                WalutaDostawcy = walutaDostawcyComboBox.Text.Trim();

                if (String.IsNullOrEmpty(Kod)
                    || String.IsNullOrEmpty(Nazwa)
                    || String.IsNullOrEmpty(Grupa6)
                    || String.IsNullOrEmpty(Grupa)
                    || String.IsNullOrEmpty(KodPcn)
                    || String.IsNullOrEmpty(KodDostawcy)
                    || String.IsNullOrEmpty(WalutaDostawcy)
                    )
                {
                    MessageBox.Show("Uzupełnij wszystkie parametry!");
                    return;
                }
                Cursor.Current = Cursors.WaitCursor;
                XLTowarInfo_20231 towarInfo = new XLTowarInfo_20231();
                towarInfo.Wersja = ListaKodow.APIVersion;
                towarInfo.Typ = 1;
                towarInfo.Kod = Kod;
                towarInfo.Nazwa = Nazwa;
                towarInfo.TwrGrupa = Grupa;
                towarInfo.PCN = KodPcn;

                //Generowanie EAN
                XLEANInfo_20231 EANInfo = new XLEANInfo_20231();
                EANInfo.Wersja = ListaKodow.APIVersion;
                EANInfo.Zakres = 1;
                int wynik_XLGenerujEAN = cdn_api.cdn_api.XLGenerujEAN(EANInfo);
                if (wynik_XLGenerujEAN == 0)
                {
                    towarInfo.EAN = EANInfo.EAN;
                }

                int wynik_XLNowyTowar = cdn_api.cdn_api.XLNowyTowar(ListaKodow.IDSesjiXL, ref IdZalozonegoTowaru, towarInfo);

                if (wynik_XLNowyTowar == 0)
                {
                    GidZalozonegoTowaru = towarInfo.GIDNumer;
                    XLTwDInfo_20231 TwDInfo = new XLTwDInfo_20231();
                    TwDInfo.Wersja = ListaKodow.APIVersion;
                    TwDInfo.KlasaKnt = 8;
                    TwDInfo.TwrTyp = towarInfo.GIDTyp;
                    TwDInfo.TwrNumer = GidZalozonegoTowaru;
                    TwDInfo.KntTyp = ListaKodow.kontrahentGidTyp;
                    TwDInfo.KntNumer = ListaKodow.kontrahentGidNumer;
                    TwDInfo.KodKnt = KodDostawcy;
                    TwDInfo.Cena = CenaDst.ToString();
                    TwDInfo.Waluta = WalutaDostawcy;

                    int wynik_XLDodajDostawce = cdn_api.cdn_api.XLDodajDostawce(ListaKodow.IDSesjiXL, TwDInfo);
                    if (wynik_XLDodajDostawce != 0)
                    {
                        MessageBox.Show("Błąd dodawania dostawcy: " + wynik_XLDodajDostawce, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    string query = "INSERT INTO dbo.TwrKodyOEM VALUES (@TKO_TwrNumer, @TKO_KntNumer, @TKO_Oem, @TKO_SzukajB2B, @TKO_PokazB2B)";
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);
                        foreach (DataGridViewRow row in kodyOemDataGridView.Rows)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@TKO_TwrNumer", towarInfo.GIDNumer);
                            cmd.Parameters.AddWithValue("@TKO_KntNumer", row.Cells["kntGidNumer"].Value ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@TKO_Oem", row.Cells["Oem"].Value);
                            cmd.Parameters.AddWithValue("@TKO_SzukajB2B", Convert.ToInt32(row.Cells["szukajB2B"].Value));
                            cmd.Parameters.AddWithValue("@TKO_PokazB2B", Convert.ToInt32(row.Cells["pokazB2B"].Value));

                            int wynik = cmd.ExecuteNonQuery();
                            if (wynik == 1)
                            {
                                row.DefaultCellStyle.BackColor = Color.LightGreen;
                            }
                            else
                            {
                                row.DefaultCellStyle.BackColor = Color.Red;
                            }
                        }
                    }
                    MessageBox.Show("Założono kartę", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Błąd dodawania karty towaru: " + wynik_XLNowyTowar, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex) { MessageBox.Show("Błąd dodawania karty towaru: " + ex, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            finally 
            { 
                Cursor.Current = Cursors.Default;
            }
        }

        private void KodyOemDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == kodyOemDataGridView.Columns["akronim"].Index && e.RowIndex >= 0)
            {
                using (var forma = new Dostawcy(ConnectionString, Search))
                {
                    var result = forma.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        string wybranyDostawca = forma.Dostawca;
                        string wybranyDostawcaGid = forma.DostawcaGidNumer;
                        Search = forma.Search;
                        kodyOemDataGridView.Rows[e.RowIndex].Cells["akronim"].Value = wybranyDostawca;
                        kodyOemDataGridView.Rows[e.RowIndex].Cells["kntGidNumer"].Value = wybranyDostawcaGid;
                    }
                }
            }
        }
        private void grupaTextBox_Click(object sender, EventArgs e)
        {
            try
            {
                XLGIDGrupaInfo_20231 XLGIDGrupaInfo = new XLGIDGrupaInfo_20231();
                XLGIDGrupaInfo.Wersja = ListaKodow.APIVersion;
                XLGIDGrupaInfo.GIDTyp = -16;
                XLGIDGrupaInfo.GIDNumer = -1;
                XLGIDGrupaInfo.GIDLp = -1;

                int wynik = cdn_api.cdn_api.XLUruchomFormatkeWgGID(XLGIDGrupaInfo);
                int GIDNumer = 0;
                GIDNumer = XLGIDGrupaInfo.GIDNumer;

                if (wynik != 0)
                {
                    MessageBox.Show("Błąd wybierania grupy towarowej", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    grupaTextBox.Text = PobierzNazweGrupyTowarowej(XLGIDGrupaInfo.GIDNumer);
                }
            }
            catch (Exception ex)
            {
                string lineNumber = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
                MessageBox.Show("Błąd wybierania grupy towarowej " + lineNumber, "BŁĄD");
                //throw new Exception(ex.Message);
            }
        }

        public string PobierzNazweGrupyTowarowej(int GID_Grupy)
        {
            string NazwaGrupy = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // twg_kod jako kod grupy albo twg_nazwa jako nazwa grupy

                    string zapytanie = @"SELECT 

REPLACE(CDN.TwrGrupaPelnaNazwa(twg_gidnumer),'/', '\|') as [Ścieżka]

,TwG_Kod as [Nazwa grupy]

from CDN.TwrGrupy

where TwG_GIDTyp = -16 AND TwG_GIDFirma = 449892 AND twg_gidnumer = " + GID_Grupy;

                    SqlCommand command = new SqlCommand(zapytanie, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //NazwaGrupy = reader["Nazwa grupy"].ToString();
                            NazwaGrupy = reader["Ścieżka"].ToString();
                        }
                    }
                }
            }
            catch
            {
            }

            return NazwaGrupy;
        }
    }
}
