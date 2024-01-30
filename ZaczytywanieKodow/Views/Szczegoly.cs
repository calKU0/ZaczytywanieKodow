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
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
