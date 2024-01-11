using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZaczytywanieKodow.Views
{
    public partial class Szczegoly : Form
    {
        private int twrGidNumer { get; set; }
        public Szczegoly(int twrGidNumer)
        {
            this.twrGidNumer = twrGidNumer;
            InitializeComponent();
            dataGridView1.DataSource = Excel.Szczegoly(twrGidNumer);
        }
    }
}
