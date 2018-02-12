namespace BDpA_U2_2InformeBDD
{
    partial class Form1
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
            this.btnGenerar = new System.Windows.Forms.Button();
            this.btnGenerarExcel = new System.Windows.Forms.Button();
            this.btnGenerarHtml = new System.Windows.Forms.Button();
            this.spmostrar_ReporteTableAdapter = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.spmostrar_ReporteTableAdapter)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenerar
            // 
            this.btnGenerar.Location = new System.Drawing.Point(634, 42);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(101, 32);
            this.btnGenerar.TabIndex = 0;
            this.btnGenerar.Text = "Generar Reporte";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // btnGenerarExcel
            // 
            this.btnGenerarExcel.Location = new System.Drawing.Point(634, 133);
            this.btnGenerarExcel.Name = "btnGenerarExcel";
            this.btnGenerarExcel.Size = new System.Drawing.Size(101, 33);
            this.btnGenerarExcel.TabIndex = 1;
            this.btnGenerarExcel.Text = "Generar Excel";
            this.btnGenerarExcel.UseVisualStyleBackColor = true;
            this.btnGenerarExcel.Click += new System.EventHandler(this.btnGenerarExcel_Click);
            // 
            // btnGenerarHtml
            // 
            this.btnGenerarHtml.Location = new System.Drawing.Point(634, 230);
            this.btnGenerarHtml.Name = "btnGenerarHtml";
            this.btnGenerarHtml.Size = new System.Drawing.Size(101, 32);
            this.btnGenerarHtml.TabIndex = 2;
            this.btnGenerarHtml.Text = "Generar Html";
            this.btnGenerarHtml.UseVisualStyleBackColor = true;
            this.btnGenerarHtml.Click += new System.EventHandler(this.btnGenerarHtml_Click);
            // 
            // spmostrar_ReporteTableAdapter
            // 
            this.spmostrar_ReporteTableAdapter.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.spmostrar_ReporteTableAdapter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.spmostrar_ReporteTableAdapter.Location = new System.Drawing.Point(12, 42);
            this.spmostrar_ReporteTableAdapter.Name = "spmostrar_ReporteTableAdapter";
            this.spmostrar_ReporteTableAdapter.ReadOnly = true;
            this.spmostrar_ReporteTableAdapter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.spmostrar_ReporteTableAdapter.Size = new System.Drawing.Size(561, 231);
            this.spmostrar_ReporteTableAdapter.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(752, 312);
            this.Controls.Add(this.spmostrar_ReporteTableAdapter);
            this.Controls.Add(this.btnGenerarHtml);
            this.Controls.Add(this.btnGenerarExcel);
            this.Controls.Add(this.btnGenerar);
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Name = "Form1";
            this.Text = "Ventada Reporte";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spmostrar_ReporteTableAdapter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Button btnGenerarExcel;
        private System.Windows.Forms.Button btnGenerarHtml;
        public System.Windows.Forms.DataGridView spmostrar_ReporteTableAdapter;
    }
}

