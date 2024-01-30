using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZaczytywanieKodow.Models;

namespace ZaczytywanieKodow
{
    public partial class WyborTowaru : Form
    {
        public string ReturnValue1 { get; set; }
        public string ReturnValue2 { get; set; }
        public string ReturnValue3 { get; set; }
        public WyborTowaru(Item item)
        {
            InitializeComponent();
            for (int i = 0; i < item.KodSystem.Count && i < item.Dostawca.Count; i++)
            {
                dataGridView1.Rows.Add(item.KodSystem[i], item.Dostawca[i], item.TwrGidNumer[i]);
            }
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ReturnValue1 = dataGridView1.Rows[e.RowIndex].Cells["kodSystem"].Value.ToString();
            ReturnValue2 = dataGridView1.Rows[e.RowIndex].Cells["dostawca"].Value.ToString();
            ReturnValue3 = dataGridView1.Rows[e.RowIndex].Cells["twrGidNumer"].Value.ToString();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void wybierzButton_Click(object sender, EventArgs e)
        {
            ReturnValue1 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["kodSystem"].Value.ToString();
            ReturnValue2 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["dostawca"].Value.ToString();
            ReturnValue3 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["twrGidNumer"].Value.ToString();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void dgv_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }
    }
}
