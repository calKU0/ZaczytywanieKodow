using System;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using ZaczytywanieKodow.Models;
using System.Windows.Forms;
using System.Data;
using excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace ZaczytywanieKodow
{
    public class Excel
    {
        public int IloscWierszy { get; set; }
        private string nazwaPliku { get; set; }
        public bool czyPlikOtwarty { get; set; } = false;
        private excel.Workbook plikExcel { get; set; }
        private excel.Worksheet arkusz { get; set; }
        private excel.Application xlApp { get; set; }
        private excel.Range range { get; set; }

        private readonly string connectionString = ConfigurationManager.ConnectionStrings["GaskaConnectionString"].ConnectionString;

        public Excel(string nazwaPliku)
        {
            this.nazwaPliku = nazwaPliku;
        }

        public bool Otworz()
        {
            try
            {
                xlApp = new excel.Application();
                plikExcel = xlApp.Workbooks.Open(nazwaPliku, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", true, false, 0, false, 1, 0);
                arkusz = (excel.Worksheet)plikExcel.Worksheets.get_Item(1);
                range = arkusz.UsedRange;
                czyPlikOtwarty = true;
            }
            catch (FileNotFoundException) { MessageBox.Show("Nie znaleziono pliku", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            return czyPlikOtwarty;
        }

        public int PobierzIloscWierszy()
        {
            if (czyPlikOtwarty == true) { return arkusz.UsedRange.Rows.Count; }
            else { MessageBox.Show("Nie otwarto pliku", "Ostrzeżenie", MessageBoxButtons.OK, MessageBoxIcon.Warning); return 0; }
        }

        public void Zamknij()
        {
            if (czyPlikOtwarty == true)
            {
                plikExcel.Close(true, null, null);
                xlApp.Quit();

                Marshal.ReleaseComObject(arkusz);
                Marshal.ReleaseComObject(plikExcel);
                Marshal.ReleaseComObject(xlApp);
                czyPlikOtwarty = false;
            }
        }

        public Item CzytajWiersz(int numerWiersza)
        {
            Item item = new Item();
            try
            {
                if (czyPlikOtwarty == true)
                {
                    item.KodDostawcy = ((excel.Range)range.Cells[numerWiersza, 1]).FormulaR1C1Local.ToString().Trim() ?? "";
                    item.KodOem = ((excel.Range)range.Cells[numerWiersza, 2]).FormulaR1C1Local.ToString().Trim() ?? "";
                    item.CenaZakupu = ((excel.Range)range.Cells[numerWiersza, 3]).FormulaR1C1Local.ToString().Trim() ?? "";
                    item.Zastosowanie = ((excel.Range)range.Cells[numerWiersza, 4]).FormulaR1C1Local.ToString().Trim() ?? "";
                    item.KodOem = item.KodOem.Replace("\n", "");
                    item.KodOem = item.KodOem.Replace("\t", "");
                    if (item.KodDostawcy == "" || item.KodOem == "") { return item; }

                    string query = $@"
	                                    SELECT distinct isnull(twr_gidnumer,0) as twrGidNumer
	                                    ,isnull(twr_kod,'') as twrKod
                                        ,isnull(twr_nazwa,'') as twrNazwa
                                        ,isnull((select convert(int,round(sum(TwZ_IlMag),0)) from cdn.TwrZasoby where Twr_GIDNumer = TwZ_TwrNumer and TwZ_MagNumer = 1),0) as [stan]
	                                    ,isnull((select top 1 isnull(knt_akronim,'') from cdn.twrdost with (nolock) join cdn.kntkarty with (nolock) on knt_gidnumer = twd_kntnumer where Twr_GIDNumer=TWD_TwrNumer and TWD_KlasaKnt = 8),'') as kntAkronim
	                                    ,podzapytanie.wyszukiwania as wyszukiwania
                                        ,isnull((select top 1 zakupowe.Cena from(
											select ImE_Cena as [Cena]
											,ImN_GIDNumer as [DokumentGidNumer]
											,ImE_TwrNumer as [TowarGidNumer]
											,ImN_DataWystawienia as [DataWystawienia]
											from cdn.impnag with (nolock)
											join cdn.impelem with(nolock) on ImE_GIDNumer = ImN_GIDNumer
											where ImN_GIDTyp=3344

											union all

											select TrE_Cena as [Cena]
											,TrN_GIDNumer as [DokumentGidNumer]
											,TrE_TwrNumer as [TowarGidNumer]
											,TrN_Data2 as [DataWystawienia]
											from cdn.TraNag with (nolock)
											join cdn.TraElem with (nolock) on TrN_GIDTyp=TrE_GIDTyp AND TrN_GIDNumer=TrE_GIDNumer
											where TrN_GIDTyp = 1521
										) zakupowe
										where zakupowe.TowarGidNumer = Twr_GIDNumer
										order by zakupowe.DataWystawienia desc, zakupowe.DokumentGidNumer desc
									),0) as [ostCena]
	                                    ,isnull((
		                                    select top 1 zakupowe.Waluta
		                                    from
		                                    (
			                                    select ImE_Cena as [Cena]
			                                    ,ImN_Waluta as [Waluta]
			                                    ,ImN_GIDNumer as [DokumentGidNumer]
			                                    ,ImE_TwrNumer as [TowarGidNumer]
			                                    ,ImN_DataWystawienia as [DataWystawienia]
			                                    from cdn.impnag with (nolock)
			                                    join cdn.impelem with(nolock) on ImE_GIDNumer = ImN_GIDNumer
			                                    where ImN_GIDTyp=3344

			                                    union all

			                                    select TrE_Cena as [Cena]
			                                    ,tre_waluta as [Waluta]
			                                    ,TrN_GIDNumer as [DokumentGidNumer]
			                                    ,TrE_TwrNumer as [TowarGidNumer]
			                                    ,TrN_Data2 as [DataWystawienia]
			                                    from cdn.TraNag with (nolock)
			                                    join cdn.TraElem with (nolock) on TrN_GIDTyp=TrE_GIDTyp AND TrN_GIDNumer=TrE_GIDNumer
			                                    where TrN_GIDTyp = 1521
		                                    ) zakupowe
		                                    where zakupowe.TowarGidNumer = Twr_GIDNumer
		                                    order by zakupowe.DataWystawienia desc, zakupowe.DokumentGidNumer desc
	                                    ),'') as waluta
	                                    from (SELECT count(distinct R_SRC_KntID) as wyszukiwania FROM [serwer-sql].[nowe_b2b].[ldd].[RptWyszukiwanie] with (nolock) 
                                        where R_SRC_Zapytanie = '{item.KodOem}'
                                        and convert(date,R_SRC_Data) between convert(date,getdate()-730) and convert(date,getdate())) podzapytanie
                                        join cdn.TwrAplikacjeOpisy with (nolock) on TPO_OpisKrotki like '%{item.KodOem}%'
                                        join cdn.twrkarty with (nolock) on (Twr_GIDNumer=TPO_ObiNumer and Twr_Archiwalny = 0)
                                    UNION
	                                    SELECT distinct isnull(twr_gidnumer,0) as twrGidNumer
	                                    ,isnull(twr_kod,'') as twrKod
                                        ,isnull(twr_nazwa,'') as twrNazwa
                                        ,isnull((select convert(int,round(sum(TwZ_IlMag),0)) from cdn.TwrZasoby where Twr_GIDNumer = TwZ_TwrNumer and TwZ_MagNumer = 1),0) as [stan]
	                                    ,isnull((select top 1 knt_akronim from cdn.twrdost with (nolock) join cdn.kntkarty with (nolock) on knt_gidnumer = twd_kntnumer where Twr_GIDNumer=TWD_TwrNumer and TWD_KlasaKnt = 8),'') as kntAkronim
	                                    ,podzapytanie.wyszukiwania as wyszukiwania
                                        ,isnull((select top 1 zakupowe.Cena from(
											select ImE_Cena as [Cena]
											,ImN_GIDNumer as [DokumentGidNumer]
											,ImE_TwrNumer as [TowarGidNumer]
											,ImN_DataWystawienia as [DataWystawienia]
											from cdn.impnag with (nolock)
											join cdn.impelem with(nolock) on ImE_GIDNumer = ImN_GIDNumer
											where ImN_GIDTyp=3344

											union all

											select TrE_Cena as [Cena]
											,TrN_GIDNumer as [DokumentGidNumer]
											,TrE_TwrNumer as [TowarGidNumer]
											,TrN_Data2 as [DataWystawienia]
											from cdn.TraNag with (nolock)
											join cdn.TraElem with (nolock) on TrN_GIDTyp=TrE_GIDTyp AND TrN_GIDNumer=TrE_GIDNumer
											where TrN_GIDTyp = 1521
										) zakupowe
										where zakupowe.TowarGidNumer = Twr_GIDNumer
										order by zakupowe.DataWystawienia desc, zakupowe.DokumentGidNumer desc
									),0) as [ostCena]
	                                    ,isnull((
		                                    select top 1 zakupowe.Waluta
		                                    from
		                                    (
			                                    select ImE_Cena as [Cena]
			                                    ,ImN_Waluta as [Waluta]
			                                    ,ImN_GIDNumer as [DokumentGidNumer]
			                                    ,ImE_TwrNumer as [TowarGidNumer]
			                                    ,ImN_DataWystawienia as [DataWystawienia]
			                                    from cdn.impnag with (nolock)
			                                    join cdn.impelem with(nolock) on ImE_GIDNumer = ImN_GIDNumer
			                                    where ImN_GIDTyp=3344

			                                    union all

			                                    select TrE_Cena as [Cena]
			                                    ,tre_waluta as [Waluta]
			                                    ,TrN_GIDNumer as [DokumentGidNumer]
			                                    ,TrE_TwrNumer as [TowarGidNumer]
			                                    ,TrN_Data2 as [DataWystawienia]
			                                    from cdn.TraNag with (nolock)
			                                    join cdn.TraElem with (nolock) on TrN_GIDTyp=TrE_GIDTyp AND TrN_GIDNumer=TrE_GIDNumer
			                                    where TrN_GIDTyp = 1521
		                                    ) zakupowe
		                                    where zakupowe.TowarGidNumer = Twr_GIDNumer
		                                    order by zakupowe.DataWystawienia desc, zakupowe.DokumentGidNumer desc
	                                    ),'') as waluta
	                                    from (SELECT count(distinct R_SRC_KntID) as wyszukiwania FROM [serwer-sql].[nowe_b2b].[ldd].[RptWyszukiwanie] with (nolock) 
                                        where R_SRC_Zapytanie = '{item.KodOem}'
                                        and convert(date,R_SRC_Data) between convert(date,getdate()-730) and convert(date,getdate())) podzapytanie
	                                    left join dbo.TwrKodyOem with (nolock) on TKO_Oem like '%{item.KodOem}%'
	                                    left join cdn.twrkarty with (nolock) on Twr_GIDTyp=16 AND Twr_GIDNumer=TKO_TwrNumer and Twr_Archiwalny = 0";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand selectCommand = new SqlCommand(query, connection);
                        selectCommand.Parameters.AddWithValue("@kodOem", item.KodOem);
                        using (SqlDataReader dr = selectCommand.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {   
                                    item.TwrGidNumer.Add((int)dr["twrGidNumer"]);
                                    item.KodSystem.Add((string)dr["twrKod"]);
                                    item.Nazwa.Add((string)dr["twrNazwa"]);
                                    item.Dostawca.Add((string)dr["kntAkronim"]);
                                    item.Wyszukiwania = (int)dr["wyszukiwania"];
                                    item.OstatniaCenaZakupu.Add((decimal)dr["ostCena"]);
                                    item.Waluta.Add((string)dr["waluta"]);
                                    item.Stan.Add((int)dr["stan"]);
                                }
                            }
                            else { item.KodSystem.Add(""); }
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
	                        TWr_kod as [Kod towaru]
                            ,Twr_nazwa as [Nazwa towaru]
                            ,R_TWR_Zapytanie as [Szukany ciąg]
                            ,KNT_Akronim as [Klient]
                            ,Kns_Nazwa as [Osoba]
                            ,r_twr_data as [Data]
                            ,count(TWr_kod) as [Ilośc kliknięć]


                            FROM [serwer-sql].[nowe_b2b].[ldd].[RptTowary] with (nolock)
                            left join cdn.twrkarty with (nolock) on twr_gidnumer=R_TWR_twrID
                            left join [dbo].[Dane_Do_kolumn] WITH (NOLOCK) on  dane_gid = twr_gidnumer
                            left join cdn.KNTKarty with (nolock) on KNT_Gidnumer=R_twr_KntID
                            left join cdn.KNTOSoby with (nolock) on R_twr_KntID=KnS_KntNumer and R_twr_KntLP=KnS_KntLp

	                        WHERE
                            R_twr_twrid = @TwrGidNumer AND twr_archiwalny=0 AND Twr_WCenniku=1
	                        AND r_twr_data>=getdate()-360

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

        public static string ZwrocAkronimKontrahenta(int kntGidNumer)
        {
            string wynik = "";
            try
            {
                string select = @"SELECT knt_akronim FROM cdn.kntkarty where knt_gidnumer = @kntgid";

                string connectionString = ConfigurationManager.ConnectionStrings["GaskaConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(select, connection);
                    selectCommand.Parameters.AddWithValue("@kntgid", kntGidNumer);
                    wynik = (string)selectCommand.ExecuteScalar();
                }
            }
            catch (Exception ex) { MessageBox.Show("Problem z otwarciem szczegółów\n " + ex, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            return wynik;
        }

        public static Dictionary<string,string> PobierzDaneTowaru(int twrGid)
        {
            Dictionary<string,string> result = new Dictionary<string,string>();

            try
            {
                string select = @"SELECT distinct isnull(twr_gidnumer,0) as twrGidNumer
	                                    ,isnull(twr_kod,'') as twrKod
                                        ,isnull(twr_nazwa,'') as twrNazwa
                                        ,isnull((select convert(int,round(sum(TwZ_IlMag),0)) from cdn.TwrZasoby where Twr_GIDNumer = TwZ_TwrNumer and TwZ_MagNumer = 1),0) as [stan]
	                                    ,isnull((select top 1 isnull(knt_akronim,'') from cdn.twrdost with (nolock) join cdn.kntkarty with (nolock) on knt_gidnumer = twd_kntnumer where Twr_GIDNumer=TWD_TwrNumer and TWD_KlasaKnt = 8),'') as kntAkronim
                                        ,isnull((select top 1 zakupowe.Cena from(
											select ImE_Cena as [Cena]
											,ImN_GIDNumer as [DokumentGidNumer]
											,ImE_TwrNumer as [TowarGidNumer]
											,ImN_DataWystawienia as [DataWystawienia]
											from cdn.impnag with (nolock)
											join cdn.impelem with(nolock) on ImE_GIDNumer = ImN_GIDNumer
											where ImN_GIDTyp=3344

											union all

											select TrE_Cena as [Cena]
											,TrN_GIDNumer as [DokumentGidNumer]
											,TrE_TwrNumer as [TowarGidNumer]
											,TrN_Data2 as [DataWystawienia]
											from cdn.TraNag with (nolock)
											join cdn.TraElem with (nolock) on TrN_GIDTyp=TrE_GIDTyp AND TrN_GIDNumer=TrE_GIDNumer
											where TrN_GIDTyp = 1521
										) zakupowe
										where zakupowe.TowarGidNumer = Twr_GIDNumer
										order by zakupowe.DataWystawienia desc, zakupowe.DokumentGidNumer desc
									),0) as [ostCena]
	                                    ,isnull((
		                                    select top 1 zakupowe.Waluta
		                                    from
		                                    (
			                                    select ImE_Cena as [Cena]
			                                    ,ImN_Waluta as [Waluta]
			                                    ,ImN_GIDNumer as [DokumentGidNumer]
			                                    ,ImE_TwrNumer as [TowarGidNumer]
			                                    ,ImN_DataWystawienia as [DataWystawienia]
			                                    from cdn.impnag with (nolock)
			                                    join cdn.impelem with(nolock) on ImE_GIDNumer = ImN_GIDNumer
			                                    where ImN_GIDTyp=3344

			                                    union all

			                                    select TrE_Cena as [Cena]
			                                    ,tre_waluta as [Waluta]
			                                    ,TrN_GIDNumer as [DokumentGidNumer]
			                                    ,TrE_TwrNumer as [TowarGidNumer]
			                                    ,TrN_Data2 as [DataWystawienia]
			                                    from cdn.TraNag with (nolock)
			                                    join cdn.TraElem with (nolock) on TrN_GIDTyp=TrE_GIDTyp AND TrN_GIDNumer=TrE_GIDNumer
			                                    where TrN_GIDTyp = 1521
		                                    ) zakupowe
		                                    where zakupowe.TowarGidNumer = Twr_GIDNumer
		                                    order by zakupowe.DataWystawienia desc, zakupowe.DokumentGidNumer desc
	                                    ),'') as waluta
	                                    from cdn.twrkarty with (nolock)
                                        where Twr_Archiwalny = 0 and Twr_Gidnumer = @gidnumer";

                string connectionString = ConfigurationManager.ConnectionStrings["GaskaConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(select, connection);
                    selectCommand.Parameters.AddWithValue("@gidnumer", twrGid);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add("twrKod", reader["twrKod"].ToString());
                            result.Add("twrNazwa", reader["twrNazwa"].ToString());
                            result.Add("kntAkronim", reader["kntAkronim"].ToString());
                            result.Add("ostCena", reader["ostCena"].ToString());
                            result.Add("waluta", reader["waluta"].ToString());
                            result.Add("stan", reader["stan"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Problem z otwarciem szczegółów\n " + ex, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            return result;
        }
    }
}
