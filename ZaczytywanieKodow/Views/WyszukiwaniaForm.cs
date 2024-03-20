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

namespace ZaczytywanieKodow.Views
{
    public partial class WyszukiwaniaForm : Form
    {
        public WyszukiwaniaForm(List<Item> items)
        {
            InitializeComponent();
            foreach (Item item in items) 
            {
                dataGridView1.Rows.Add(item.KodOem, item.Wyszukiwania);
            }
        }
    }
}
