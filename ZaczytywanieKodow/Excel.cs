using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using IronXL;
using System.Configuration;
using System.Data.SqlClient;
using System.CodeDom;
using System.Net.Sockets;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.IO;

namespace ZaczytywanieKodow
{
    public class Excel
    {
        public string kodSystemowy { get; set; }
        public string kodDostawcy { get; set; }
        public string kodOem { get; set; }
        public string dostawca { get; set; }
        public int wyszukiwania { get; set; }
        public string polaczoneNumery { get; set; }
        public static List<string> kodySystemoweLista = new List<string>();
        public static List<string> dostawcyLista = new List<string>();
        public int IloscWierszy { get; set; }
        private string nazwaPliku { get; set; }
        private bool czyPlikOtwarty { get; set; } = false;
        private WorkBook plikExcel { get; set; }
        private WorkSheet arkusz { get; set; }
        private SqlConnection connection { get; set; }
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["GaskaConnectionString"].ConnectionString;

        public Excel(string nazwaPliku)
        {
            this.nazwaPliku = nazwaPliku;
        }

        public void Otworz()
        {
            try
            {
                plikExcel = WorkBook.Load(nazwaPliku);
                arkusz = plikExcel.WorkSheets.First();
                czyPlikOtwarty = true;
            }
            catch (FileNotFoundException ex) { Prompt.ShowDialog("Nie otwarto pliku", "Błąd"); }
        }

        public int PobierzIloscWierszy()
        {
            if (czyPlikOtwarty == true) { return arkusz.Rows.Count(); }
            else { Prompt.ShowDialog("Nie otwarto pliku", "Ostrzeżenie"); return 0; }
        }

        public void WyszukajDane(int numerWiersza)
        {
            try
            {
                if (czyPlikOtwarty == true)
                {
                    //Prompt.ShowDialog(arkusz["B" + numerWiersza.ToString()].StringValue, "DEBUG");
                    kodDostawcy = arkusz["B" + numerWiersza.ToString()].StringValue == null ? "" : arkusz["B" + numerWiersza.ToString()].StringValue.Replace(System.Environment.NewLine, "");
                    kodOem = arkusz["C" + numerWiersza.ToString()].StringValue == null ? "" : arkusz["C" + numerWiersza.ToString()].StringValue.Replace(System.Environment.NewLine, "");

                    string query = @"SELECT isnull(twr_kod,'') as twrKod, isnull(knt_akronim,'') as kntAkronim, podzapytanie.wyszukiwania as wyszukiwania
                                    from (SELECT count(R_twr_twrid) as wyszukiwania FROM [serwer-sql].[nowe_b2b].[ldd].[RptTowary] with (nolock) where r_twr_Zapytanie = @kodOem) podzapytanie
									left join cdn.twrAplikacjeOpisy with (nolock) on TPO_OpisKrotki like '%" + kodOem + @"%' 
                                    left join cdn.twrkarty with (nolock) on Twr_GIDTyp=TPO_ObiTyp AND Twr_GIDNumer=TPO_ObiNumer and Twr_Archiwalny = 0
                                    left join cdn.twrdost with (nolock) on Twr_GIDNumer=TWD_TwrNumer and TWD_KlasaKnt = 8
                                    left join cdn.kntkarty with (nolock) on knt_gidnumer = twd_kntnumer";

                    string queryRowCount = "SELECT @@ROWCOUNT";

                    connection = new SqlConnection(connectionString);
                    connection.Open();

                    SqlCommand selectCommand = new SqlCommand(query, connection);
                    SqlCommand selectRowCountCommand = new SqlCommand(queryRowCount, connection);
                    selectCommand.Parameters.AddWithValue("@kodOem", kodOem);
                    using (SqlDataReader dr = selectCommand.ExecuteReader())
                    {
                        IloscWierszy = (int)selectRowCountCommand.ExecuteScalar();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                if (IloscWierszy == 1)
                                {
                                    kodSystemowy = (string)dr["twrKod"];
                                    dostawca = (string)dr["kntAkronim"];
                                    wyszukiwania = (int)dr["wyszukiwania"];
                                }
                            }
                        }
                        else { kodSystemowy = ""; }
                    }
                    connection.Close();
                }
                else { Prompt.ShowDialog("Nie otwarto pliku", "Ostrzeżenie"); }
            }
            catch (Exception e) { Prompt.ShowDialog("Wystąpił problem w zaczytywaniu pliku" + e.ToString(), "Błąd"); }
        }
        public static List<string> WyszukajDane(string kodOem)
        {
            try
            {
                SqlConnection connection;
                string connectionString = ConfigurationManager.ConnectionStrings["GaskaConnectionString"].ConnectionString;
                kodySystemoweLista.Clear();
                dostawcyLista.Clear();
                string query = @"SELECT isnull(twr_kod,'') as twrKod, isnull(knt_akronim,'') as kntAkronim, podzapytanie.wyszukiwania as wyszukiwania
                                    from (SELECT count(R_twr_twrid) as wyszukiwania FROM [serwer-sql].[nowe_b2b].[ldd].[RptTowary] with (nolock) where r_twr_Zapytanie = @kodOem) podzapytanie
									left join cdn.twrAplikacjeOpisy with (nolock) on TPO_OpisKrotki like '%" + kodOem + @"%' 
                                    left join cdn.twrkarty with (nolock) on Twr_GIDTyp=TPO_ObiTyp AND Twr_GIDNumer=TPO_ObiNumer and Twr_Archiwalny = 0
                                    left join cdn.twrdost with (nolock) on Twr_GIDNumer=TWD_TwrNumer and TWD_KlasaKnt = 8
                                    left join cdn.kntkarty with (nolock) on knt_gidnumer = twd_kntnumer";

                connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand selectCommand = new SqlCommand(query, connection);
                selectCommand.Parameters.AddWithValue("@kodOem", kodOem);
                using (SqlDataReader dr = selectCommand.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            kodySystemoweLista.Add((string)dr["twrKod"]);
                        }
                    }
                }
                connection.Close();
                return kodySystemoweLista;
            }
            catch (Exception e) { Prompt.ShowDialog("Wystąpił problem w zaczytywaniu pliku" + e.ToString(), "Błąd"); return kodySystemoweLista; }
        }
    }
}
