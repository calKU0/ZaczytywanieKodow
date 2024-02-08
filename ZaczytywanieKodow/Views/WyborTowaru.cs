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

namespace ZaczytywanieKodow
{
    public partial class WyborTowaru : Form
    {
        public string ReturnValue1 { get; set; }
        public string ReturnValue2 { get; set; }
        public string ReturnValue3 { get; set; }
        public string ReturnValue4 { get; set; }
        public string ReturnValue5 { get; set; }
        public string ReturnValue6 { get; set; }
        public GrouppedItem item { get;set; }
        private int APIVersion { get; }
        public WyborTowaru(GrouppedItem item, int APIVersion)
        {
            InitializeComponent();

            HashSet<string> uniqueRows = new HashSet<string>();

            for (int i = 0; i < item.KodSystem.Count; i++)
            {
                string rowKey = $"{item.KodSystem[i]}_{item.Nazwa[i]}_{item.Dostawca[i]}_{item.TwrGidNumer[i]}_{item.OstatniaCenaZakupu[i]}_{item.Waluta[i]}";

                if (!uniqueRows.Contains(rowKey))
                {
                    dataGridView1.Rows.Add("", item.KodSystem[i], item.Nazwa[i], item.Dostawca[i], item.TwrGidNumer[i], item.OstatniaCenaZakupu[i], item.Waluta[i]);
                    uniqueRows.Add(rowKey);
                }
            }
            this.APIVersion = APIVersion;
            this.item = item;
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ReturnValue1 = dataGridView1.Rows[e.RowIndex].Cells["kodSystem"].Value.ToString();
            ReturnValue2 = dataGridView1.Rows[e.RowIndex].Cells["nazwa"].Value.ToString();
            ReturnValue3 = dataGridView1.Rows[e.RowIndex].Cells["dostawca"].Value.ToString();
            ReturnValue4 = dataGridView1.Rows[e.RowIndex].Cells["twrGidNumer"].Value.ToString();
            ReturnValue5 = dataGridView1.Rows[e.RowIndex].Cells["ostCenaZak"].Value.ToString();
            ReturnValue6 = dataGridView1.Rows[e.RowIndex].Cells["waluta"].Value.ToString();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void wybierzButton_Click(object sender, EventArgs e)
        {
            ReturnValue1 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["kodSystem"].Value.ToString();
            ReturnValue2 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["nazwa"].Value.ToString();
            ReturnValue3 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["dostawca"].Value.ToString();
            ReturnValue4 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["twrGidNumer"].Value.ToString();
            ReturnValue5 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["ostCenaZak"].Value.ToString();
            ReturnValue6 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["waluta"].Value.ToString();

            this.DialogResult = DialogResult.OK;
            this.Close();
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
                    this.TopMost = false;

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
    }
}
