using System;
using System.Collections.Generic;
using System.Linq;
using IronXL;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using ZaczytywanieKodow.Models;
using System.Windows.Forms;
using System.Data;
using System.Collections;

namespace ZaczytywanieKodow
{
    public class Excel
    {
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

        public bool Otworz()
        {
            try
            {
                plikExcel = WorkBook.Load(nazwaPliku);
                arkusz = plikExcel.WorkSheets.First();
                czyPlikOtwarty = true;
            }
            catch (FileNotFoundException) { MessageBox.Show("Nie znaleziono pliku", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            return czyPlikOtwarty;
        }

        public int PobierzIloscWierszy()
        {
            if (czyPlikOtwarty == true) { return arkusz.Rows.Count(); }
            else { MessageBox.Show("Nie otwarto pliku", "Ostrzeżenie", MessageBoxButtons.OK, MessageBoxIcon.Warning); return 0; }
        }

        public Item CzytajWiersz(int numerWiersza)
        {
            Item item = new Item();
            try
            {
                if (czyPlikOtwarty == true)
                {
                    item.KodDostawcy = arkusz["B" + numerWiersza.ToString()].StringValue == null ? "" : arkusz["B" + numerWiersza.ToString()].StringValue.Replace(System.Environment.NewLine, "");
                    item.KodOem = arkusz["C" + numerWiersza.ToString()].StringValue == null ? "" : arkusz["C" + numerWiersza.ToString()].StringValue.Replace(System.Environment.NewLine, "");

                    string query = @"SELECT isnull(twr_gidnumer,0) as twrGidNumer,isnull(twr_kod,'') as twrKod, isnull(knt_akronim,'') as kntAkronim, podzapytanie.wyszukiwania as wyszukiwania
                                    from (SELECT count(R_twr_twrid) as wyszukiwania FROM [serwer-sql].[nowe_b2b].[ldd].[RptTowary] with (nolock) where r_twr_Zapytanie = @kodOem) podzapytanie
									left join cdn.twrAplikacjeOpisy with (nolock) on TPO_OpisKrotki like '%" + item.KodOem + @"%' 
                                    left join cdn.twrkarty with (nolock) on Twr_GIDTyp=TPO_ObiTyp AND Twr_GIDNumer=TPO_ObiNumer and Twr_Archiwalny = 0
                                    left join cdn.twrdost with (nolock) on Twr_GIDNumer=TWD_TwrNumer and TWD_KlasaKnt = 8
                                    left join cdn.kntkarty with (nolock) on knt_gidnumer = twd_kntnumer";

                    string queryRowCount = "SELECT @@ROWCOUNT";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand selectCommand = new SqlCommand(query, connection);
                        SqlCommand selectRowCountCommand = new SqlCommand(queryRowCount, connection);
                        selectCommand.Parameters.AddWithValue("@kodOem", item.KodOem);
                        using (SqlDataReader dr = selectCommand.ExecuteReader())
                        {
                            IloscWierszy = (int)selectRowCountCommand.ExecuteScalar();
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    item.TwrGidNumer.Add((int)dr["twrGidNumer"]);
                                    item.KodSystem.Add((string)dr["twrKod"]);
                                    item.Dostawca.Add((string)dr["kntAkronim"]);
                                    item.Wyszukiwania = (int)dr["wyszukiwania"];
                                    if (IloscWierszy > 1) { item.WieleKodow = true; }
                                }
                            }
                            else { item.KodSystem.Add(""); item.WieleKodow = false; }
                        }
                    }
                }
                else { MessageBox.Show("Nie otwarto pliku", "Ostrzeżenie", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            catch (Exception e) { MessageBox.Show("Wystąpił problem w zaczytywaniu pliku" + e.ToString(), "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            return item;
        }

        public static DataTable Szczegoly(int twrGidNumer)
        {
            DataTable dt = new DataTable();
            try
            {
                string select = @"SELECT
	                        ,TWr_kod as [Kod towaru]
                            ,Twr_nazwa as [Nazwa towaru]
                            ,R_TWR_Zapytanie as [Szukany ciąg]
                            ,KNT_Akronim as [Klient]
                            ,Kns_Nazwa as [Osoba]
                            ,count(TWr_kod) as [Ilośc kliknięć]
                            ,r_twr_data as [Data]


                            FROM [serwer-sql].[nowe_b2b].[ldd].[RptTowary] with (nolock)
                            left join cdn.twrkarty with (nolock) on twr_gidnumer=R_TWR_twrID
                            left join [dbo].[Dane_Do_kolumn] WITH (NOLOCK) on  dane_gid = twr_gidnumer
                            left join cdn.KNTKarty with (nolock) on KNT_Gidnumer=R_twr_KntID
                            left join cdn.KNTOSoby with (nolock) on R_twr_KntID=KnS_KntNumer and R_twr_KntLP=KnS_KntLp

	                        WHERE
                            R_twr_twrid = @TwrGidNumer AND r_TWR_stan=0 AND twr_archiwalny=0 AND Twr_WCenniku=1
	                        AND R_TWR_twrID not in (select tre_twrNumer from cdn.TraElem  with (nolock) where convert(datetime, Dateadd(Second, Tre_TrnTStamp, '1990-01-01')) > getdate()-365 )
	                        AND R_TWR_twrID not in (select ZaE_TwrNumer 
                                                    from cdn.ZamElem  with (nolock) 
                                                    join cdn.ZamNag  with (nolock) on  ZaN_GIDNumer=ZaE_GIDNumer 
	                                                where 
	                                                zan_stan<>2
	                                                AND ZaN_ZamTyp=1152
	                                                AND convert(datetime, Dateadd(DAY, ZaE_DataAktywacjiRez, '18001228')) > getdate()-365)

                            group by twr_kod, twr_nazwa, ostatni_dostawca, KNT_Akronim ,Kns_Nazwa, r_twr_data, R_TWR_Zapytanie
                            order by r_twr_data desc";

                string connectionString = ConfigurationManager.ConnectionStrings["GaskaConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(select, connection);
                    selectCommand.Parameters.AddWithValue("@twrGidNumer", twrGidNumer);
                    SqlDataAdapter da = new SqlDataAdapter(selectCommand);
                    da.Fill(dt);
                }
            }
            catch (Exception ex) { MessageBox.Show("Problem z otwarciem szczegółów\n " + ex, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            return dt;
        }
    }
}
