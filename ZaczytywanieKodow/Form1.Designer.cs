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
            WybierzPlikButton = new Button();
            nazwaPlikuTextBox = new TextBox();
            ZaczytajButton = new Button();
            kodyLista = new DataGridView();
            kodSystem = new DataGridViewButtonColumn();
            kodDostawcy = new DataGridViewTextBoxColumn();
            kodOem = new DataGridViewTextBoxColumn();
            dostawca = new DataGridViewTextBoxColumn();
            wyszukiwania = new DataGridViewTextBoxColumn();
            polaczoneNumery = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)kodyLista).BeginInit();
            SuspendLayout();
            // 
            // WybierzPlikButton
            // 
            WybierzPlikButton.Location = new System.Drawing.Point(1508, 12);
            WybierzPlikButton.Name = "WybierzPlikButton";
            WybierzPlikButton.Size = new System.Drawing.Size(144, 23);
            WybierzPlikButton.TabIndex = 0;
            WybierzPlikButton.Text = "Wybierz Plik";
            WybierzPlikButton.UseVisualStyleBackColor = true;
            WybierzPlikButton.Click += WybierzPlikButton_Click;
            // 
            // nazwaPlikuTextBox
            // 
            nazwaPlikuTextBox.Location = new System.Drawing.Point(1054, 12);
            nazwaPlikuTextBox.Name = "nazwaPlikuTextBox";
            nazwaPlikuTextBox.ReadOnly = true;
            nazwaPlikuTextBox.Size = new System.Drawing.Size(439, 23);
            nazwaPlikuTextBox.TabIndex = 1;
            // 
            // ZaczytajButton
            // 
            ZaczytajButton.BackColor = SystemColors.ButtonFace;
            ZaczytajButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            ZaczytajButton.FlatAppearance.BorderSize = 2;
            ZaczytajButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            ZaczytajButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            ZaczytajButton.FlatStyle = FlatStyle.Flat;
            ZaczytajButton.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            ZaczytajButton.Location = new System.Drawing.Point(1466, 880);
            ZaczytajButton.Name = "ZaczytajButton";
            ZaczytajButton.Size = new System.Drawing.Size(186, 55);
            ZaczytajButton.TabIndex = 2;
            ZaczytajButton.Text = "Zaczytaj";
            ZaczytajButton.UseVisualStyleBackColor = false;
            ZaczytajButton.Click += ZaczytajButton_Click;
            // 
            // kodyLista
            // 
            kodyLista.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            kodyLista.Location = new System.Drawing.Point(12, 61);
            kodyLista.Name = "kodyLista";
            kodyLista.RowTemplate.Height = 25;
            kodyLista.Size = new System.Drawing.Size(1169, 493);
            kodyLista.TabIndex = 3;
            kodyLista.AllowUserToAddRows = false;
            kodyLista.AllowUserToDeleteRows = false;
            kodyLista.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(kodyLista_CellClick);

            //
            // kodSystem
            // 
            kodSystem.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            kodSystem.HeaderText = "Kod Systemowy";
            kodSystem.Name = "kodSystem";
            // 
            // kodDostawcy
            // 
            kodDostawcy.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            kodDostawcy.HeaderText = "Kod Dostawcy";
            kodDostawcy.Name = "kodDostawcy";
            kodDostawcy.ReadOnly = true;
            // 
            // kodOem
            // 
            kodOem.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            kodOem.HeaderText = "OEM";
            kodOem.Name = "kodOem";
            kodOem.ReadOnly = true;
            // 
            // dostawca
            // 
            dostawca.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dostawca.HeaderText = "Dostawca";
            dostawca.Name = "dostawca";
            dostawca.ReadOnly = true;
            // 
            // wyszukiwania
            // 
            wyszukiwania.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            wyszukiwania.HeaderText = "Wyszukiwania";
            wyszukiwania.Name = "wyszukiwania";
            wyszukiwania.ReadOnly = true;
            // 
            // polaczoneNumery
            // 
            polaczoneNumery.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            polaczoneNumery.HeaderText = "Połączone Numery";
            polaczoneNumery.Name = "polaczoneNumery";
            polaczoneNumery.ReadOnly = true;
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.Inherit;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new System.Drawing.Size(1664, 947);
            Controls.Add(kodyLista);
            Controls.Add(ZaczytajButton);
            Controls.Add(nazwaPlikuTextBox);
            Controls.Add(WybierzPlikButton);
            ForeColor = SystemColors.ControlText;
            HelpButton = true;
            Name = "Form1";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)kodyLista).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
    }
}