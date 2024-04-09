namespace ZaczytywanieKodow.Views
{
    partial class DodajTowar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.kodTextBox = new System.Windows.Forms.TextBox();
            this.nazwaTextBox = new System.Windows.Forms.TextBox();
            this.cenaDstTextBox = new System.Windows.Forms.TextBox();
            this.kodPCNTextBox = new System.Windows.Forms.TextBox();
            this.grupa6TextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.grupaTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dodajButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.kodDostawcyTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.walutaDostawcyComboBox = new System.Windows.Forms.ComboBox();
            this.kodyOemDataGridView = new System.Windows.Forms.DataGridView();
            this.kntGidNumer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Oem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.akronim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.szukajB2B = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pokazB2B = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.kodyOemDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 21);
            this.label1.TabIndex = 99;
            this.label1.Text = "Kod";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(8, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 21);
            this.label2.TabIndex = 99;
            this.label2.Text = "Nazwa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(453, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "Cena zakupu od dst";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(8, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 21);
            this.label4.TabIndex = 99;
            this.label4.Text = "Kod PCN";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(665, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "Numery OEM";
            // 
            // kodTextBox
            // 
            this.kodTextBox.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kodTextBox.Location = new System.Drawing.Point(12, 33);
            this.kodTextBox.Name = "kodTextBox";
            this.kodTextBox.Size = new System.Drawing.Size(173, 26);
            this.kodTextBox.TabIndex = 1;
            // 
            // nazwaTextBox
            // 
            this.nazwaTextBox.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.nazwaTextBox.Location = new System.Drawing.Point(12, 110);
            this.nazwaTextBox.Name = "nazwaTextBox";
            this.nazwaTextBox.Size = new System.Drawing.Size(173, 26);
            this.nazwaTextBox.TabIndex = 2;
            // 
            // cenaDstTextBox
            // 
            this.cenaDstTextBox.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cenaDstTextBox.Location = new System.Drawing.Point(457, 33);
            this.cenaDstTextBox.Name = "cenaDstTextBox";
            this.cenaDstTextBox.ReadOnly = true;
            this.cenaDstTextBox.Size = new System.Drawing.Size(173, 26);
            this.cenaDstTextBox.TabIndex = 19;
            // 
            // kodPCNTextBox
            // 
            this.kodPCNTextBox.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kodPCNTextBox.Location = new System.Drawing.Point(12, 193);
            this.kodPCNTextBox.Name = "kodPCNTextBox";
            this.kodPCNTextBox.Size = new System.Drawing.Size(173, 26);
            this.kodPCNTextBox.TabIndex = 3;
            // 
            // grupa6TextBox
            // 
            this.grupa6TextBox.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.grupa6TextBox.Location = new System.Drawing.Point(237, 193);
            this.grupa6TextBox.Name = "grupa6TextBox";
            this.grupa6TextBox.ReadOnly = true;
            this.grupa6TextBox.Size = new System.Drawing.Size(173, 26);
            this.grupa6TextBox.TabIndex = 99;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label11.Location = new System.Drawing.Point(233, 169);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 21);
            this.label11.TabIndex = 0;
            this.label11.Text = "Grupa 6.1";
            // 
            // grupaTextBox
            // 
            this.grupaTextBox.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.grupaTextBox.Location = new System.Drawing.Point(237, 110);
            this.grupaTextBox.Name = "grupaTextBox";
            this.grupaTextBox.Size = new System.Drawing.Size(173, 26);
            this.grupaTextBox.TabIndex = 4;
            this.grupaTextBox.Click += new System.EventHandler(this.grupaTextBox_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(233, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 21);
            this.label6.TabIndex = 0;
            this.label6.Text = "Grupa";
            // 
            // dodajButton
            // 
            this.dodajButton.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dodajButton.Location = new System.Drawing.Point(12, 259);
            this.dodajButton.Name = "dodajButton";
            this.dodajButton.Size = new System.Drawing.Size(1020, 55);
            this.dodajButton.TabIndex = 2;
            this.dodajButton.Text = "Dodaj Kartę";
            this.dodajButton.UseVisualStyleBackColor = true;
            this.dodajButton.Click += new System.EventHandler(this.DodajButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(233, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 21);
            this.label7.TabIndex = 0;
            this.label7.Text = "Kod Dostawcy";
            // 
            // kodDostawcyTextBox
            // 
            this.kodDostawcyTextBox.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kodDostawcyTextBox.Location = new System.Drawing.Point(237, 33);
            this.kodDostawcyTextBox.Name = "kodDostawcyTextBox";
            this.kodDostawcyTextBox.ReadOnly = true;
            this.kodDostawcyTextBox.Size = new System.Drawing.Size(173, 26);
            this.kodDostawcyTextBox.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(453, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(140, 21);
            this.label8.TabIndex = 3;
            this.label8.Text = "Waluta Dostawcy";
            // 
            // walutaDostawcyComboBox
            // 
            this.walutaDostawcyComboBox.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.walutaDostawcyComboBox.FormattingEnabled = true;
            this.walutaDostawcyComboBox.Location = new System.Drawing.Point(457, 110);
            this.walutaDostawcyComboBox.Name = "walutaDostawcyComboBox";
            this.walutaDostawcyComboBox.Size = new System.Drawing.Size(173, 26);
            this.walutaDostawcyComboBox.TabIndex = 5;
            // 
            // kodyOemDataGridView
            // 
            this.kodyOemDataGridView.AllowUserToAddRows = false;
            this.kodyOemDataGridView.AllowUserToDeleteRows = false;
            this.kodyOemDataGridView.AllowUserToResizeRows = false;
            this.kodyOemDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.kodyOemDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.kodyOemDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.kodyOemDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.kntGidNumer,
            this.Oem,
            this.akronim,
            this.szukajB2B,
            this.pokazB2B});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.kodyOemDataGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.kodyOemDataGridView.Location = new System.Drawing.Point(669, 33);
            this.kodyOemDataGridView.Name = "kodyOemDataGridView";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.kodyOemDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.kodyOemDataGridView.RowHeadersVisible = false;
            this.kodyOemDataGridView.Size = new System.Drawing.Size(364, 220);
            this.kodyOemDataGridView.TabIndex = 5;
            this.kodyOemDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.KodyOemDataGridView_CellClick);
            // 
            // kntGidNumer
            // 
            this.kntGidNumer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.kntGidNumer.HeaderText = "gid";
            this.kntGidNumer.Name = "kntGidNumer";
            this.kntGidNumer.ReadOnly = true;
            this.kntGidNumer.Visible = false;
            // 
            // Oem
            // 
            this.Oem.HeaderText = "OEM";
            this.Oem.Name = "Oem";
            this.Oem.ReadOnly = true;
            // 
            // akronim
            // 
            this.akronim.HeaderText = "Akronim";
            this.akronim.Name = "akronim";
            this.akronim.ReadOnly = true;
            // 
            // szukajB2B
            // 
            this.szukajB2B.HeaderText = "Szukaj B2B";
            this.szukajB2B.Name = "szukajB2B";
            this.szukajB2B.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.szukajB2B.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // pokazB2B
            // 
            this.pokazB2B.HeaderText = "Pokaz B2B";
            this.pokazB2B.Name = "pokazB2B";
            this.pokazB2B.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.pokazB2B.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // DodajTowar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 322);
            this.Controls.Add(this.kodyOemDataGridView);
            this.Controls.Add(this.walutaDostawcyComboBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dodajButton);
            this.Controls.Add(this.grupa6TextBox);
            this.Controls.Add(this.grupaTextBox);
            this.Controls.Add(this.kodPCNTextBox);
            this.Controls.Add(this.kodDostawcyTextBox);
            this.Controls.Add(this.cenaDstTextBox);
            this.Controls.Add(this.nazwaTextBox);
            this.Controls.Add(this.kodTextBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DodajTowar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dodaj towar";
            this.Load += new System.EventHandler(this.DodajTowar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kodyOemDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox kodTextBox;
        private System.Windows.Forms.TextBox nazwaTextBox;
        private System.Windows.Forms.TextBox cenaDstTextBox;
        private System.Windows.Forms.TextBox kodPCNTextBox;
        private System.Windows.Forms.TextBox grupa6TextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox grupaTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button dodajButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox kodDostawcyTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox walutaDostawcyComboBox;
        private System.Windows.Forms.DataGridView kodyOemDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn kntGidNumer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Oem;
        private System.Windows.Forms.DataGridViewTextBoxColumn akronim;
        private System.Windows.Forms.DataGridViewCheckBoxColumn szukajB2B;
        private System.Windows.Forms.DataGridViewCheckBoxColumn pokazB2B;
    }
}