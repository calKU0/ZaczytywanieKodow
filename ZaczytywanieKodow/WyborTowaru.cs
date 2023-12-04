using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZaczytywanieKodow
{
    public partial class WyborTowaru : Form
    {
        public string ReturnValue1 { get; set; }
        public WyborTowaru(List<string> kodySystem)
        {
            InitializeComponent();
            foreach(string k in kodySystem) 
            {
                dataGridView1.Rows.Add(k);
            }
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ReturnValue1 = dataGridView1.Rows[e.RowIndex].Cells["kodSystem"].Value.ToString();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
