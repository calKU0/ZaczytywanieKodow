using System.Windows.Forms;

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
            this.WybierzPlikButton = new System.Windows.Forms.Button();
            this.nazwaPlikuTextBox = new System.Windows.Forms.TextBox();
            this.ZaczytajButton = new System.Windows.Forms.Button();
            this.kodyLista = new System.Windows.Forms.DataGridView();
            this.kodSystem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kodDostawcy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kodOem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dostawca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wyszukiwania = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.polaczoneNumery = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.smoothProgressBar1 = new SmoothProgressBar.SmoothProgressBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.kodyLista)).BeginInit();
            this.SuspendLayout();
            // 
            // WybierzPlikButton
            // 
            this.WybierzPlikButton.Location = new System.Drawing.Point(469, 13);
            this.WybierzPlikButton.Name = "WybierzPlikButton";
            this.WybierzPlikButton.Size = new System.Drawing.Size(144, 23);
            this.WybierzPlikButton.TabIndex = 0;
            this.WybierzPlikButton.Text = "Wybierz Plik";
            this.WybierzPlikButton.UseVisualStyleBackColor = true;
            this.WybierzPlikButton.Click += new System.EventHandler(this.WybierzPlikButton_Click);
            // 
            // nazwaPlikuTextBox
            // 
            this.nazwaPlikuTextBox.Location = new System.Drawing.Point(12, 13);
            this.nazwaPlikuTextBox.Name = "nazwaPlikuTextBox";
            this.nazwaPlikuTextBox.ReadOnly = true;
            this.nazwaPlikuTextBox.Size = new System.Drawing.Size(439, 20);
            this.nazwaPlikuTextBox.TabIndex = 1;
            // 
            // ZaczytajButton
            // 
            this.ZaczytajButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.ZaczytajButton.FlatAppearance.BorderSize = 2;
            this.ZaczytajButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.ZaczytajButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.ZaczytajButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ZaczytajButton.Location = new System.Drawing.Point(1466, 880);
            this.ZaczytajButton.Name = "ZaczytajButton";
            this.ZaczytajButton.Size = new System.Drawing.Size(186, 55);
            this.ZaczytajButton.TabIndex = 2;
            this.ZaczytajButton.Text = "Zaczytaj";
            this.ZaczytajButton.UseVisualStyleBackColor = false;
            this.ZaczytajButton.Click += new System.EventHandler(this.ZaczytajButton_Click);
            // 
            // kodyLista
            // 
            this.kodyLista.AllowUserToAddRows = false;
            this.kodyLista.RowHeadersVisible = false;
            this.kodyLista.AllowUserToDeleteRows = false;
            this.kodyLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.kodyLista.Location = new System.Drawing.Point(12, 87);
            this.kodyLista.Name = "kodyLista";
            this.kodyLista.RowTemplate.Height = 25;
            this.kodyLista.Size = new System.Drawing.Size(1406, 848);
            this.kodyLista.TabIndex = 3;
            this.kodyLista.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.kodyLista_CellClick);
            // 
            // kodSystem
            // 
            this.kodSystem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.kodSystem.HeaderText = "Kod Systemowy";
            this.kodSystem.Name = "kodSystem";
            this.kodSystem.ReadOnly = true;
            // 
            // kodDostawcy
            // 
            this.kodDostawcy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.kodDostawcy.HeaderText = "Kod Dostawcy";
            this.kodDostawcy.Name = "kodDostawcy";
            this.kodDostawcy.ReadOnly = true;
            // 
            // kodOem
            // 
            this.kodOem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.kodOem.HeaderText = "OEM";
            this.kodOem.Name = "kodOem";
            this.kodOem.ReadOnly = true;
            // 
            // dostawca
            // 
            this.dostawca.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dostawca.HeaderText = "Dostawca";
            this.dostawca.Name = "dostawca";
            this.dostawca.ReadOnly = true;
            // 
            // wyszukiwania
            // 
            this.wyszukiwania.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.wyszukiwania.HeaderText = "Wyszukiwania";
            this.wyszukiwania.Name = "wyszukiwania";
            this.wyszukiwania.ReadOnly = true;
            // 
            // polaczoneNumery
            // 
            this.polaczoneNumery.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.polaczoneNumery.HeaderText = "Połączone Numery";
            this.polaczoneNumery.Name = "polaczoneNumery";
            this.polaczoneNumery.ReadOnly = true;
            // 
            // smoothProgressBar1
            // 
            this.smoothProgressBar1.Location = new System.Drawing.Point(674, 11);
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
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox1.Location = new System.Drawing.Point(1189, 11);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(167, 24);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "Pozostało czasu:";
            this.textBox1.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox2.Location = new System.Drawing.Point(1358, 13);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(63, 24);
            this.textBox2.TabIndex = 6;
            this.textBox2.Visible = false;
            // 
            // ListaKodow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1664, 947);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.smoothProgressBar1);
            this.Controls.Add(this.kodyLista);
            this.Controls.Add(this.ZaczytajButton);
            this.Controls.Add(this.nazwaPlikuTextBox);
            this.Controls.Add(this.WybierzPlikButton);
            this.HelpButton = true;
            this.Name = "ListaKodow";
            this.Text = "Lista Kodów";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.kodyLista)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button WybierzPlikButton;
        private TextBox nazwaPlikuTextBox;
        private Button ZaczytajButton;
        private DataGridView kodyLista;
        private DataGridViewTextBoxColumn kodSystem;
        private DataGridViewTextBoxColumn kodDostawcy;
        private DataGridViewTextBoxColumn kodOem;
        private DataGridViewTextBoxColumn dostawca;
        private DataGridViewTextBoxColumn wyszukiwania;
        private DataGridViewTextBoxColumn polaczoneNumery;
        private SmoothProgressBar.SmoothProgressBar smoothProgressBar1;
        private TextBox textBox1;
        private TextBox textBox2;
    }
}