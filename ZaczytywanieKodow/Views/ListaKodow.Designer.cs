using System.Windows.Forms;
using ZaczytywanieKodow.Properties;

namespace ZaczytywanieKodow
{
    partial class ListaKodow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        { 
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListaKodow));
            this.nazwaPlikuTextBox = new System.Windows.Forms.TextBox();
            this.kodyLista = new System.Windows.Forms.DataGridView();
            this.twrGidNumer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kodSystem = new System.Windows.Forms.DataGridViewButtonColumn();
            this.nazwa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kodDostawcy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kodOem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dostawca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ostCenaZak = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.waluta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grupa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zastosowanie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wyszukiwania = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.polaczoneNumery = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.szczegoly = new System.Windows.Forms.DataGridViewButtonColumn();
            this.smoothProgressBar1 = new SmoothProgressBar.SmoothProgressBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.AnulujButton = new System.Windows.Forms.Button();
            this.ZaczytajButton = new System.Windows.Forms.Button();
            this.WybierzPlikButton = new System.Windows.Forms.Button();
            this.WyczyscButton = new System.Windows.Forms.Button();
            this.dstLabel = new System.Windows.Forms.Label();
            this.dstTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.kodyLista)).BeginInit();
            this.SuspendLayout();
            // 
            // nazwaPlikuTextBox
            // 
            this.nazwaPlikuTextBox.Location = new System.Drawing.Point(12, 13);
            this.nazwaPlikuTextBox.Name = "nazwaPlikuTextBox";
            this.nazwaPlikuTextBox.ReadOnly = true;
            this.nazwaPlikuTextBox.Size = new System.Drawing.Size(364, 20);
            this.nazwaPlikuTextBox.TabIndex = 1;
            // 
            // kodyLista
            // 
            this.kodyLista.AllowUserToAddRows = false;
            this.kodyLista.AllowUserToDeleteRows = false;
            this.kodyLista.AllowUserToResizeColumns = false;
            this.kodyLista.AllowUserToResizeRows = false;
            this.kodyLista.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.kodyLista.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.kodyLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.kodyLista.DefaultCellStyle = dataGridViewCellStyle2;
            this.kodyLista.Location = new System.Drawing.Point(12, 87);
            this.kodyLista.Name = "kodyLista";
            this.kodyLista.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.kodyLista.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.kodyLista.RowHeadersVisible = false;
            this.kodyLista.RowTemplate.Height = 25;
            this.kodyLista.Size = new System.Drawing.Size(1423, 848);
            this.kodyLista.TabIndex = 3;
            this.kodyLista.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.kodyLista_CellClick);
            this.kodyLista.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.kodyLista_CellDoubleClick);
            this.kodyLista.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.kodyLista_CellFormatting);
            // 
            // twrGidNumer
            // 
            this.twrGidNumer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.twrGidNumer.HeaderText = "twrgidnumer";
            this.twrGidNumer.Name = "twrGidNumer";
            this.twrGidNumer.ReadOnly = true;
            this.twrGidNumer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.twrGidNumer.Visible = false;
            // 
            // kodSystem
            // 
            this.kodSystem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.kodSystem.HeaderText = "Kod Systemowy";
            this.kodSystem.Name = "kodSystem";
            this.kodSystem.ReadOnly = true;
            // 
            // nazwa
            // 
            this.nazwa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nazwa.HeaderText = "Nazwa towaru";
            this.nazwa.Name = "nazwa";
            this.nazwa.ReadOnly = true;
            this.nazwa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // kodDostawcy
            // 
            this.kodDostawcy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.kodDostawcy.HeaderText = "Kod Dostawcy";
            this.kodDostawcy.Name = "kodDostawcy";
            this.kodDostawcy.ReadOnly = true;
            this.kodDostawcy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // kodOem
            // 
            this.kodOem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.kodOem.HeaderText = "OEM";
            this.kodOem.Name = "kodOem";
            this.kodOem.ReadOnly = true;
            this.kodOem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dostawca
            // 
            this.dostawca.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dostawca.HeaderText = "Dostawca";
            this.dostawca.Name = "dostawca";
            this.dostawca.ReadOnly = true;
            this.dostawca.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cena
            // 
            this.cena.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cena.HeaderText = "Cena";
            this.cena.Name = "cena";
            this.cena.ReadOnly = true;
            this.cena.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ostCenaZak
            // 
            this.ostCenaZak.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ostCenaZak.HeaderText = "Cena FZ/FAI";
            this.ostCenaZak.Name = "ostCenaZak";
            this.ostCenaZak.ReadOnly = true;
            this.ostCenaZak.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // waluta
            // 
            this.waluta.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.waluta.HeaderText = "Waluta";
            this.waluta.Name = "waluta";
            this.waluta.ReadOnly = true;
            this.waluta.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // grupa
            // 
            this.grupa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.grupa.HeaderText = "Grupa 6.1";
            this.grupa.Name = "grupa";
            this.grupa.ReadOnly = true;
            this.grupa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // zastosowanie
            // 
            this.zastosowanie.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.zastosowanie.HeaderText = "Zastosowanie";
            this.zastosowanie.Name = "zastosowanie";
            this.zastosowanie.ReadOnly = true;
            this.zastosowanie.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // wyszukiwania
            // 
            this.wyszukiwania.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.wyszukiwania.HeaderText = "Wyszukiwania";
            this.wyszukiwania.Name = "wyszukiwania";
            this.wyszukiwania.ReadOnly = true;
            this.wyszukiwania.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // polaczoneNumery
            // 
            this.polaczoneNumery.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.polaczoneNumery.HeaderText = "Połączone Numery OEM";
            this.polaczoneNumery.Name = "polaczoneNumery";
            this.polaczoneNumery.ReadOnly = true;
            this.polaczoneNumery.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // szczegoly
            // 
            this.szczegoly.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.szczegoly.HeaderText = "Szczegóły";
            this.szczegoly.Name = "szczegoly";
            this.szczegoly.ReadOnly = true;
            // 
            // smoothProgressBar1
            // 
            this.smoothProgressBar1.Location = new System.Drawing.Point(718, 10);
            this.smoothProgressBar1.Maximum = 100;
            this.smoothProgressBar1.Minimum = 0;
            this.smoothProgressBar1.Name = "smoothProgressBar1";
            this.smoothProgressBar1.ProgressBarColor = System.Drawing.Color.Lime;
            this.smoothProgressBar1.Size = new System.Drawing.Size(507, 25);
            this.smoothProgressBar1.TabIndex = 4;
            this.smoothProgressBar1.Value = 0;
            this.smoothProgressBar1.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox1.Location = new System.Drawing.Point(1233, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(175, 26);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "Pozostało czasu:";
            this.textBox1.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox2.Location = new System.Drawing.Point(1409, 11);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(63, 26);
            this.textBox2.TabIndex = 6;
            this.textBox2.Visible = false;
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox3.Location = new System.Drawing.Point(718, 41);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(62, 20);
            this.textBox3.TabIndex = 5;
            this.textBox3.Visible = false;
            // 
            // AnulujButton
            // 
            this.AnulujButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AnulujButton.BackColor = System.Drawing.SystemColors.Control;
            this.AnulujButton.Enabled = false;
            this.AnulujButton.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.AnulujButton.Image = global::ZaczytywanieKodow.Properties.Resources.cancel;
            this.AnulujButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AnulujButton.Location = new System.Drawing.Point(1466, 880);
            this.AnulujButton.Name = "AnulujButton";
            this.AnulujButton.Size = new System.Drawing.Size(186, 55);
            this.AnulujButton.TabIndex = 2;
            this.AnulujButton.Text = "Anuluj";
            this.AnulujButton.UseVisualStyleBackColor = false;
            this.AnulujButton.Click += new System.EventHandler(this.AnulujButton_Click);
            // 
            // ZaczytajButton
            // 
            this.ZaczytajButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ZaczytajButton.BackColor = System.Drawing.SystemColors.Control;
            this.ZaczytajButton.Enabled = false;
            this.ZaczytajButton.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ZaczytajButton.Image = ((System.Drawing.Image)(resources.GetObject("ZaczytajButton.Image")));
            this.ZaczytajButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ZaczytajButton.Location = new System.Drawing.Point(1466, 740);
            this.ZaczytajButton.Name = "ZaczytajButton";
            this.ZaczytajButton.Size = new System.Drawing.Size(186, 55);
            this.ZaczytajButton.TabIndex = 2;
            this.ZaczytajButton.Text = "Zaczytaj";
            this.ZaczytajButton.UseVisualStyleBackColor = false;
            this.ZaczytajButton.Click += new System.EventHandler(this.ZaczytajButton_Click);
            // 
            // WybierzPlikButton
            // 
            this.WybierzPlikButton.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.WybierzPlikButton.Image = ((System.Drawing.Image)(resources.GetObject("WybierzPlikButton.Image")));
            this.WybierzPlikButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.WybierzPlikButton.Location = new System.Drawing.Point(11, 39);
            this.WybierzPlikButton.Name = "WybierzPlikButton";
            this.WybierzPlikButton.Size = new System.Drawing.Size(151, 39);
            this.WybierzPlikButton.TabIndex = 0;
            this.WybierzPlikButton.Text = "  Wybierz Plik";
            this.WybierzPlikButton.UseVisualStyleBackColor = true;
            this.WybierzPlikButton.Click += new System.EventHandler(this.WybierzPlikButton_Click);
            // 
            // WyczyscButton
            // 
            this.WyczyscButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.WyczyscButton.BackColor = System.Drawing.SystemColors.Control;
            this.WyczyscButton.Enabled = false;
            this.WyczyscButton.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.WyczyscButton.Image = global::ZaczytywanieKodow.Properties.Resources.trash_bin;
            this.WyczyscButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.WyczyscButton.Location = new System.Drawing.Point(1466, 810);
            this.WyczyscButton.Name = "WyczyscButton";
            this.WyczyscButton.Size = new System.Drawing.Size(186, 55);
            this.WyczyscButton.TabIndex = 2;
            this.WyczyscButton.Text = "Wyczyść";
            this.WyczyscButton.UseVisualStyleBackColor = false;
            this.WyczyscButton.Click += new System.EventHandler(this.WyczyscButton_Click);
            // 
            // dstLabel
            // 
            this.dstLabel.AutoSize = true;
            this.dstLabel.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dstLabel.Location = new System.Drawing.Point(406, 12);
            this.dstLabel.Name = "dstLabel";
            this.dstLabel.Size = new System.Drawing.Size(90, 21);
            this.dstLabel.TabIndex = 7;
            this.dstLabel.Text = "Dostawca: ";
            // 
            // dstTextBox
            // 
            this.dstTextBox.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dstTextBox.Location = new System.Drawing.Point(502, 10);
            this.dstTextBox.Name = "dstTextBox";
            this.dstTextBox.ReadOnly = true;
            this.dstTextBox.Size = new System.Drawing.Size(186, 23);
            this.dstTextBox.TabIndex = 8;
            this.dstTextBox.Click += new System.EventHandler(this.dstTextBox_Click);
            // 
            // ListaKodow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1664, 947);
            this.Controls.Add(this.dstTextBox);
            this.Controls.Add(this.dstLabel);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.smoothProgressBar1);
            this.Controls.Add(this.kodyLista);
            this.Controls.Add(this.AnulujButton);
            this.Controls.Add(this.WyczyscButton);
            this.Controls.Add(this.ZaczytajButton);
            this.Controls.Add(this.nazwaPlikuTextBox);
            this.Controls.Add(this.WybierzPlikButton);
            this.HelpButton = true;
            this.Name = "ListaKodow";
            this.Text = "Lista Kodów";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ListaKodow_FormClosing);
            this.Load += new System.EventHandler(this.ListaKodow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kodyLista)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button WybierzPlikButton;
        private TextBox nazwaPlikuTextBox;
        private Button ZaczytajButton;
        private DataGridView kodyLista;
        private DataGridViewTextBoxColumn twrGidNumer;
        private DataGridViewButtonColumn kodSystem;
        private DataGridViewTextBoxColumn nazwa;
        private DataGridViewTextBoxColumn kodDostawcy;
        private DataGridViewTextBoxColumn kodOem;
        private DataGridViewTextBoxColumn dostawca;
        private DataGridViewTextBoxColumn cena;
        private DataGridViewTextBoxColumn ostCenaZak;
        private DataGridViewTextBoxColumn waluta;
        private DataGridViewTextBoxColumn grupa;
        private DataGridViewTextBoxColumn zastosowanie;
        private DataGridViewTextBoxColumn wyszukiwania;
        private DataGridViewTextBoxColumn polaczoneNumery;
        private DataGridViewButtonColumn szczegoly;
        private SmoothProgressBar.SmoothProgressBar smoothProgressBar1;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Button AnulujButton;
        private Button WyczyscButton;
        private Label dstLabel;
        private TextBox dstTextBox;
    }
}