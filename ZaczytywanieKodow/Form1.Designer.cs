using System.Windows.Forms;

namespace ZaczytywanieKodow
{
    partial class Form1
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
            this.kodSystem = new System.Windows.Forms.DataGridViewButtonColumn();
            this.kodDostawcy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kodOem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dostawca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wyszukiwania = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.polaczoneNumery = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.smoothProgressBar1 = new SmoothProgressBar.SmoothProgressBar();
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
            this.kodyLista.AllowUserToDeleteRows = false;
            this.kodyLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.kodyLista.Location = new System.Drawing.Point(12, 61);
            this.kodyLista.Name = "kodyLista";
            this.kodyLista.RowTemplate.Height = 25;
            this.kodyLista.Size = new System.Drawing.Size(1169, 874);
            this.kodyLista.TabIndex = 3;
            this.kodyLista.CellClick += new DataGridViewCellEventHandler(this.kodyLista_CellClick);
            // 
            // kodSystem
            // 
            this.kodSystem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.kodSystem.HeaderText = "Kod Systemowy";
            this.kodSystem.Name = "kodSystem";
            // 
            // kodDostawcy
            // 
            this.kodDostawcy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.kodDostawcy.HeaderText = "Kod Dostawcy";
            this.kodDostawcy.Name = "kodDostawcy";
            this.kodDostawcy.ReadOnly = true;
            // 
            // kodOem
            // 
            this.kodOem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.kodOem.HeaderText = "OEM";
            this.kodOem.Name = "kodOem";
            this.kodOem.ReadOnly = true;
            // 
            // dostawca
            // 
            this.dostawca.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dostawca.HeaderText = "Dostawca";
            this.dostawca.Name = "dostawca";
            this.dostawca.ReadOnly = true;
            // 
            // wyszukiwania
            // 
            this.wyszukiwania.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.wyszukiwania.HeaderText = "Wyszukiwania";
            this.wyszukiwania.Name = "wyszukiwania";
            this.wyszukiwania.ReadOnly = true;
            // 
            // polaczoneNumery
            // 
            this.polaczoneNumery.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.polaczoneNumery.HeaderText = "Połączone Numery";
            this.polaczoneNumery.Name = "polaczoneNumery";
            this.polaczoneNumery.ReadOnly = true;
            // 
            // smoothProgressBar1
            // 
            this.smoothProgressBar1.Location = new System.Drawing.Point(674, 11);
            this.smoothProgressBar1.Name = "smoothProgressBar1";
            this.smoothProgressBar1.ProgressBarColor = System.Drawing.Color.Lime;
            this.smoothProgressBar1.Size = new System.Drawing.Size(507, 25);
            this.smoothProgressBar1.TabIndex = 4;
            this.smoothProgressBar1.Value = 0;
            this.smoothProgressBar1.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1664, 947);
            this.Controls.Add(this.smoothProgressBar1);
            this.Controls.Add(this.kodyLista);
            this.Controls.Add(this.ZaczytajButton);
            this.Controls.Add(this.nazwaPlikuTextBox);
            this.Controls.Add(this.WybierzPlikButton);
            this.HelpButton = true;
            this.Name = "Form1";
            this.Text = "Form1";
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
        private DataGridViewButtonColumn kodSystem;
        private DataGridViewTextBoxColumn kodDostawcy;
        private DataGridViewTextBoxColumn kodOem;
        private DataGridViewTextBoxColumn dostawca;
        private DataGridViewTextBoxColumn wyszukiwania;
        private DataGridViewTextBoxColumn polaczoneNumery;
        private SmoothProgressBar.SmoothProgressBar smoothProgressBar1;
    }
}