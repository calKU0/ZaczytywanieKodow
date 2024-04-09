using System.Drawing;
using System.Windows.Forms;

namespace ZaczytywanieKodow
{
    partial class WyborTowaru
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.podglad = new System.Windows.Forms.DataGridViewButtonColumn();
            this.kodSystem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nazwa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dostawca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.twrGidNumer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ostCenaZak = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.waluta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.wybierzButton = new System.Windows.Forms.Button();
            this.WybierzKarteZListyButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.podglad,
            this.kodSystem,
            this.nazwa,
            this.dostawca,
            this.twrGidNumer,
            this.ostCenaZak,
            this.waluta});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(10, 70);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(732, 423);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            this.dataGridView1.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgv_RowPrePaint);
            // 
            // podglad
            // 
            this.podglad.HeaderText = "";
            this.podglad.Name = "podglad";
            this.podglad.ReadOnly = true;
            this.podglad.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.podglad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.podglad.Width = 25;
            // 
            // kodSystem
            // 
            this.kodSystem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.kodSystem.HeaderText = "Kod systemowy";
            this.kodSystem.Name = "kodSystem";
            this.kodSystem.ReadOnly = true;
            // 
            // nazwa
            // 
            this.nazwa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nazwa.HeaderText = "Nazwa";
            this.nazwa.Name = "nazwa";
            this.nazwa.ReadOnly = true;
            // 
            // dostawca
            // 
            this.dostawca.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dostawca.HeaderText = "Dostawca";
            this.dostawca.Name = "dostawca";
            this.dostawca.ReadOnly = true;
            // 
            // twrGidNumer
            // 
            this.twrGidNumer.HeaderText = "twrgidnumer";
            this.twrGidNumer.Name = "twrGidNumer";
            this.twrGidNumer.ReadOnly = true;
            this.twrGidNumer.Visible = false;
            // 
            // ostCenaZak
            // 
            this.ostCenaZak.HeaderText = "ostCenaZak";
            this.ostCenaZak.Name = "ostCenaZak";
            this.ostCenaZak.ReadOnly = true;
            this.ostCenaZak.Visible = false;
            // 
            // waluta
            // 
            this.waluta.HeaderText = "waluta";
            this.waluta.Name = "waluta";
            this.waluta.ReadOnly = true;
            this.waluta.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Montserrat", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox1.Location = new System.Drawing.Point(10, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(732, 41);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Wybierz nasz kod XL";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // wybierzButton
            // 
            this.wybierzButton.Font = new System.Drawing.Font("Montserrat", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.wybierzButton.Location = new System.Drawing.Point(389, 499);
            this.wybierzButton.Name = "wybierzButton";
            this.wybierzButton.Size = new System.Drawing.Size(353, 37);
            this.wybierzButton.TabIndex = 2;
            this.wybierzButton.Text = "Wybierz";
            this.wybierzButton.UseVisualStyleBackColor = true;
            this.wybierzButton.Click += new System.EventHandler(this.wybierzButton_Click);
            // 
            // WybierzKarteZListyButton
            // 
            this.WybierzKarteZListyButton.Font = new System.Drawing.Font("Montserrat", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.WybierzKarteZListyButton.Location = new System.Drawing.Point(10, 499);
            this.WybierzKarteZListyButton.Name = "WybierzKarteZListyButton";
            this.WybierzKarteZListyButton.Size = new System.Drawing.Size(373, 37);
            this.WybierzKarteZListyButton.TabIndex = 2;
            this.WybierzKarteZListyButton.Text = "Wybierz z pełnej listy towarowej";
            this.WybierzKarteZListyButton.UseVisualStyleBackColor = true;
            this.WybierzKarteZListyButton.Click += new System.EventHandler(this.WybierzKarteZListyButton_Click);
            // 
            // WyborTowaru
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 541);
            this.Controls.Add(this.WybierzKarteZListyButton);
            this.Controls.Add(this.wybierzButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "WyborTowaru";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Wybierz kod";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView dataGridView1;
        private TextBox textBox1;
        private Button wybierzButton;
        private DataGridViewButtonColumn podglad;
        private DataGridViewTextBoxColumn kodSystem;
        private DataGridViewTextBoxColumn nazwa;
        private DataGridViewTextBoxColumn dostawca;
        private DataGridViewTextBoxColumn twrGidNumer;
        private DataGridViewTextBoxColumn ostCenaZak;
        private DataGridViewTextBoxColumn waluta;
        private Button WybierzKarteZListyButton;
    }
}