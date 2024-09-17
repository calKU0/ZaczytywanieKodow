using System;
using System.Data;
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
            DataTable dt = Excel.Szczegoly(twrGidNumer); 
            dataGridView1.DataSource = dt;

            // Set AutoSizeMode for columns
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // Calculate sum
            int sum = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (row["Ilośc kliknięć"] != DBNull.Value) // Ensure the cell has a value
                {
                    sum += Convert.ToInt32(row["Ilośc kliknięć"]);
                }
            }

            // Add a new row with the sum to the DataTable
            DataRow totalRow = dt.NewRow();
            totalRow[0] = "Suma";  // Label the first column as "Suma"
            totalRow[6] = sum;      // Place the sum in the last column
            dt.Rows.Add(totalRow);  // Add the row to the DataTable
        }
    }
}
