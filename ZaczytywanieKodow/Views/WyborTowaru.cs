﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZaczytywanieKodow.Models;
using cdn_api;
using System.Configuration;
using System.Runtime.InteropServices;
using ZaczytywanieKodow.Views;

namespace ZaczytywanieKodow
{
    public partial class WyborTowaru : Form
    {
        public List<string> ReturnValue1 { get; set; } = new List<string>();
        public List<string> ReturnValue2 { get; set; } = new List<string>();
        public List<string> ReturnValue3 { get; set; } = new List<string>();
        public List<string> ReturnValue4 { get; set; } = new List<string>();
        public List<string> ReturnValue5 { get; set; } = new List<string>();
        public List<string> ReturnValue6 { get; set; } = new List<string>();
        public List<string> ReturnValue7 { get; set; } = new List<string>();
        public GrouppedItem item { get;set; }
        private int APIVersion { get; }
        public WyborTowaru(GrouppedItem item, int APIVersion)
        {
            InitializeComponent();
            this.APIVersion = APIVersion;
            this.item = item;
            InitializeRows();
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ReturnValue1.Add(dataGridView1.Rows[e.RowIndex].Cells["kodSystem"].Value.ToString());
            ReturnValue2.Add(dataGridView1.Rows[e.RowIndex].Cells["nazwa"].Value.ToString());
            ReturnValue3.Add(dataGridView1.Rows[e.RowIndex].Cells["dostawca"].Value.ToString());
            ReturnValue4.Add(dataGridView1.Rows[e.RowIndex].Cells["twrGidNumer"].Value.ToString());
            ReturnValue5.Add(dataGridView1.Rows[e.RowIndex].Cells["ostCenaZak"].Value.ToString());
            ReturnValue6.Add(dataGridView1.Rows[e.RowIndex].Cells["waluta"].Value.ToString());
            ReturnValue7.Add(dataGridView1.Rows[e.RowIndex].Cells["Stan"].Value.ToString());

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void wybierzButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["check"].Value.ToString() == "True")
                {
                    ReturnValue1.Add(row.Cells["kodSystem"].Value.ToString());
                    ReturnValue2.Add(row.Cells["nazwa"].Value.ToString());
                    ReturnValue3.Add(row.Cells["dostawca"].Value.ToString());
                    ReturnValue4.Add(row.Cells["twrGidNumer"].Value.ToString());
                    ReturnValue5.Add(row.Cells["ostCenaZak"].Value.ToString());
                    ReturnValue6.Add(row.Cells["waluta"].Value.ToString());
                    ReturnValue7.Add(row.Cells["Stan"].Value.ToString());
                }
            }

            if (ReturnValue1.Count() >= 1)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView1.Columns["podglad"].Index)
                {
                    this.Enabled = false;
                    XLGIDGrupaInfo_20231 XLGIDGrupaInfo = new XLGIDGrupaInfo_20231();
                    XLGIDGrupaInfo.Wersja = APIVersion;
                    XLGIDGrupaInfo.GIDTyp = 16;
                    XLGIDGrupaInfo.GIDNumer = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["twrGidNumer"].Value.ToString());
                    XLGIDGrupaInfo.GIDFirma = 449892;
                    XLGIDGrupaInfo.GIDLp = 0;

                    int wynik = cdn_api.cdn_api.XLUruchomFormatkeWgGID(XLGIDGrupaInfo);
                    this.Enabled = true;
                    this.TopMost = true;

                    if (wynik != 0)
                    {
                        MessageBox.Show("Błąd funkcji API ERP XL ! Numer błędu: " + wynik.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch { throw; }
        }
        private void dgv_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.search.Width;
                var h = Properties.Resources.search.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.search, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void WybierzKarteZListyButton_Click(object sender, EventArgs e)
        {
            /*using (var forma = new DodajTowar(item))
            {
                var result = forma.ShowDialog();
                if (forma.DialogResult == DialogResult.OK)
                {
                    item.TwrGidNumer.Add(forma.GidZalozonegoTowaru);
                    item.Nazwa.Add(forma.Nazwa);
                    item.KodSystem.Add(forma.Kod);
                    item.Dostawca.Add(ListaKodow.kontrahentAkronim);
                    item.OstatniaCenaZakupu.Add(Convert.ToDecimal(0.00));
                    item.Waluta.Add("");

                    dataGridView1.Rows.Clear();
                    InitializeRows();
                }
            }*/

            try
            {
                XLGIDGrupaInfo_20231 XLGIDGrupaInfo = new XLGIDGrupaInfo_20231();
                XLGIDGrupaInfo.Wersja = ListaKodow.APIVersion;
                XLGIDGrupaInfo.GIDTyp = 16;
                XLGIDGrupaInfo.GIDNumer = -1;
                XLGIDGrupaInfo.GIDLp = 0;

                int wynik = cdn_api.cdn_api.XLUruchomFormatkeWgGID(XLGIDGrupaInfo);
                int GIDNumer = 0;
                GIDNumer = XLGIDGrupaInfo.GIDNumer;

                if (wynik != 0)
                {
                    MessageBox.Show("Błąd wybierania karty towarowej", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (GIDNumer == 0)
                {
                    return;
                }
                else
                {
                    Dictionary<string,string> daneTowaru = Excel.PobierzDaneTowaru(GIDNumer);

                    item.TwrGidNumer.Add(GIDNumer);
                    item.KodSystem.Add(daneTowaru["twrKod"]);
                    item.Nazwa.Add(daneTowaru["twrNazwa"]);
                    item.Dostawca.Add(daneTowaru["kntAkronim"]);
                    item.OstatniaCenaZakupu.Add(Convert.ToDecimal(daneTowaru["ostCena"]));
                    item.Waluta.Add(daneTowaru["waluta"]);
                    item.Stan.Add(Convert.ToInt32(daneTowaru["stan"]));

                    dataGridView1.Rows.Clear();
                    InitializeRows();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd wybierania karty towarowej " + ex, "BŁĄD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw new Exception(ex.Message);
            }
        }
        private void InitializeRows()
        {
            HashSet<string> uniqueRows = new HashSet<string>();

            for (int i = 0; i < item.KodSystem.Count; i++)
            {
                string rowKey = $"{item.KodSystem[i]}_{item.Nazwa[i]}_{item.Dostawca[i]}_{item.TwrGidNumer[i]}_{item.OstatniaCenaZakupu[i]}_{item.Waluta[i]}";

                if (!uniqueRows.Contains(rowKey) && item.KodSystem[i] != "")
                {
                    dataGridView1.Rows.Add("", false, item.KodSystem[i], item.Nazwa[i], item.Dostawca[i], item.TwrGidNumer[i], item.Stan[i], item.OstatniaCenaZakupu[i].ToString("0.00"), item.Waluta[i]);
                    uniqueRows.Add(rowKey);
                }
            }
        }
    }
}
