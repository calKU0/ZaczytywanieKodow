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

namespace ZaczytywanieKodow
{
    public class Excel
    {
        public string? kodSystemowy { get; set; }
        public string? kodDostawcy { get; set; }
        public string? kodOem { get; set; }
        public string? dostawca { get; set; }
        public int? wyszukiwania { get; set; }
        public string? polaczoneNumery { get; set; }
        public static List<string> kodySystemoweLista = new List<string>();
        public int IloscWierszy { get; set; } 
        private string? nazwaPliku { get; set; }
        private bool czyPlikOtwarty { get; set; } = false;
        private WorkBook? plikExcel { get; set; }
        private WorkSheet? arkusz { get; set; }
        private SqlConnection? connection { get; set; }
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["GaskaConnectionString"].ConnectionString;

        public Excel(string nazwaPliku)
        {
            this.nazwaPliku = nazwaPliku;
        }

        public void Otworz()
        {
            plikExcel = WorkBook.Load(nazwaPliku);
            arkusz = plikExcel.WorkSheets.First();
            czyPlikOtwarty = true;
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
                    kodDostawcy = arkusz["B" + numerWiersza.ToString()].StringValue == null ? "" : arkusz["B" + numerWiersza.ToString()].StringValue.Replace(System.Environment.NewLine,"");
                    kodOem = arkusz["C" + numerWiersza.ToString()].StringValue == null ? "" : arkusz["C" + numerWiersza.ToString()].StringValue.Replace(System.Environment.NewLine, "");

                    string query = @"SELECT twr_kod, knt_akronim,(SELECT count(R_twr_twrid) FROM [serwer-sql].[nowe_b2b].[ldd].[RptTowary] with (nolock) WHERE Twr_GIDNumer = R_twr_twrid) as [wyszukiwania]
                                    from cdn.twrkody with (nolock)
                                    left join cdn.twrkarty with (nolock) on Twr_GIDNumer=TwK_TwrNumer  
                                    left join cdn.twrdost with (nolock) on Twr_GIDNumer=TWD_TwrNumer
                                    join cdn.TwrKodyKnt with (nolock) on TwK_Id=TKK_TwKId
                                    join cdn.kntkarty with (nolock) on knt_gidnumer = twd_kntnumer
                                    WHERE twk_kod = @kodDostawcy";

                    string queryRowCount = "SELECT @@ROWCOUNT";

                    connection = new SqlConnection(connectionString);
                    connection.Open();

                    SqlCommand selectCommand = new SqlCommand(query, connection);
                    SqlCommand selectRowCountCommand = new SqlCommand(queryRowCount, connection);
                    selectCommand.Parameters.AddWithValue("@kodDostawcy", kodDostawcy);
                    using (SqlDataReader dr = selectCommand.ExecuteReader())
                    {
                        IloscWierszy = (int)selectRowCountCommand.ExecuteScalar();
                        Debug.WriteLine(IloscWierszy);
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                if (IloscWierszy == 1)
                                {
                                    kodSystemowy = (string)dr["twr_kod"];
                                    dostawca = (string)dr["knt_akronim"];
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
        public static List<string> WyszukajDane(string kodDostawcy)
        {
            try
            {
                SqlConnection? connection;
                string connectionString = ConfigurationManager.ConnectionStrings["GaskaConnectionString"].ConnectionString;
                kodySystemoweLista.Clear();
                string query = @"SELECT twr_kod, knt_akronim,(SELECT count(R_twr_twrid) FROM [serwer-sql].[nowe_b2b].[ldd].[RptTowary] with (nolock) WHERE Twr_GIDNumer = R_twr_twrid) as [wyszukiwania]
                                    from cdn.twrkody with (nolock)
                                    left join cdn.twrkarty with (nolock) on Twr_GIDNumer=TwK_TwrNumer  
                                    left join cdn.twrdost with (nolock) on Twr_GIDNumer=TWD_TwrNumer
                                    join cdn.kntkarty with (nolock) on knt_gidnumer = twd_kntnumer
                                    join cdn.TwrKodyKnt with (nolock) on TwK_Id=TKK_TwKId
                                    WHERE twk_kod = @kodDostawcy";

                connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand selectCommand = new SqlCommand(query, connection);
                selectCommand.Parameters.AddWithValue("@kodDostawcy", kodDostawcy);
                using (SqlDataReader dr = selectCommand.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            kodySystemoweLista.Add((string)dr["twr_kod"]);
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
