﻿using System;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using ZaczytywanieKodow.Models;
using System.Windows.Forms;
using System.Data;
using excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

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
                plikExcel = xlApp.Workbooks.Open(nazwaPliku, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
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
                    item.KodDostawcy = ((excel.Range)range.Cells[numerWiersza, 2]).FormulaR1C1Local.ToString() ?? "";
                    item.KodOem = ((excel.Range)range.Cells[numerWiersza, 3]).FormulaR1C1Local.ToString() ?? "";
                    if (item.KodDostawcy == "") { return item; }

                    string query = @"IF NOT EXISTS (SELECT TKO_GIDNumer FROM dbo.TwrKodyOem where TKO_Oem = '" + item.KodOem + @"')
                                    BEGIN
	                                    SELECT distinct isnull(twr_gidnumer,0) as twrGidNumer
	                                    ,isnull(twr_kod,'') as twrKod
                                        ,isnull(twr_nazwa,'') as twrNazwa
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
	                                    from (SELECT count(R_twr_twrid) as wyszukiwania FROM [serwer-sql].[nowe_b2b].[ldd].[RptTowary] with (nolock) where r_twr_Zapytanie = '" + item.KodOem + @"') podzapytanie

	                                    left join cdn.twrAplikacjeOpisy with (nolock) on TPO_OpisKrotki like '%" + item.KodOem + @"%'
	                                    left join cdn.twrkarty with (nolock) on Twr_GIDTyp=TPO_ObiTyp AND Twr_GIDNumer=TPO_ObiNumer and Twr_Archiwalny = 0
                                    END
                                    ELSE
                                    BEGIN
	                                    SELECT distinct isnull(twr_gidnumer,0) as twrGidNumer
	                                    ,isnull(twr_kod,'') as twrKod
                                        ,isnull(twr_nazwa,'') as twrNazwa
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
	                                    from (SELECT count(R_twr_twrid) as wyszukiwania FROM [serwer-sql].[nowe_b2b].[ldd].[RptTowary] with (nolock) where r_twr_Zapytanie = '" + item.KodOem + @"') podzapytanie
	                                    join dbo.TwrKodyOem with (nolock) on TKO_Oem = '" + item.KodOem + @"'
	                                    join cdn.twrkarty with (nolock) on Twr_GIDTyp=16 AND Twr_GIDNumer=TKO_TwrNumer and Twr_Archiwalny = 0
                                    END";

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
