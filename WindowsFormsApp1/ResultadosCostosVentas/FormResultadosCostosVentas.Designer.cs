namespace WindowsFormsApp1
{
    partial class FormResultadosCostosVentas
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
            this.label3 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Empresa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Envíos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostoTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VentasTotales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Resultado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Margen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnVolverMenuPrincipal = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(155, 22);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Hasta:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(306, 15);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(2);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(95, 27);
            this.btnBuscar.TabIndex = 12;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(56, 19);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(88, 20);
            this.dateTimePicker1.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Desde:";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(197, 19);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(88, 20);
            this.dateTimePicker2.TabIndex = 15;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Empresa,
            this.Envíos,
            this.CostoTotal,
            this.VentasTotales,
            this.Resultado,
            this.Margen});
            this.dataGridView1.Location = new System.Drawing.Point(11, 56);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(772, 162);
            this.dataGridView1.TabIndex = 17;
            // 
            // Empresa
            // 
            this.Empresa.HeaderText = "Empresa";
            this.Empresa.MinimumWidth = 6;
            this.Empresa.Name = "Empresa";
            this.Empresa.ReadOnly = true;
            this.Empresa.Width = 125;
            // 
            // Envíos
            // 
            this.Envíos.HeaderText = "Envíos";
            this.Envíos.MinimumWidth = 6;
            this.Envíos.Name = "Envíos";
            this.Envíos.ReadOnly = true;
            this.Envíos.Width = 90;
            // 
            // CostoTotal
            // 
            this.CostoTotal.HeaderText = "Costo Total";
            this.CostoTotal.MinimumWidth = 6;
            this.CostoTotal.Name = "CostoTotal";
            this.CostoTotal.ReadOnly = true;
            this.CostoTotal.Width = 125;
            // 
            // VentasTotales
            // 
            this.VentasTotales.HeaderText = "Ventas Totales";
            this.VentasTotales.MinimumWidth = 6;
            this.VentasTotales.Name = "VentasTotales";
            this.VentasTotales.ReadOnly = true;
            this.VentasTotales.Width = 125;
            // 
            // Resultado
            // 
            this.Resultado.HeaderText = "Resultado";
            this.Resultado.MinimumWidth = 6;
            this.Resultado.Name = "Resultado";
            this.Resultado.ReadOnly = true;
            this.Resultado.Width = 125;
            // 
            // Margen
            // 
            this.Margen.HeaderText = "Margen";
            this.Margen.MinimumWidth = 6;
            this.Margen.Name = "Margen";
            this.Margen.ReadOnly = true;
            this.Margen.Width = 125;
            // 
            // btnVolverMenuPrincipal
            // 
            this.btnVolverMenuPrincipal.Location = new System.Drawing.Point(638, 232);
            this.btnVolverMenuPrincipal.Margin = new System.Windows.Forms.Padding(2);
            this.btnVolverMenuPrincipal.Name = "btnVolverMenuPrincipal";
            this.btnVolverMenuPrincipal.Size = new System.Drawing.Size(145, 27);
            this.btnVolverMenuPrincipal.TabIndex = 20;
            this.btnVolverMenuPrincipal.Text = "Volver al Menú Principal";
            this.btnVolverMenuPrincipal.UseVisualStyleBackColor = true;
            this.btnVolverMenuPrincipal.Click += new System.EventHandler(this.btnVolverMenuPrincipal_Click);
            // 
            // FormResultadosCostosVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 271);
            this.Controls.Add(this.btnVolverMenuPrincipal);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.btnBuscar);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormResultadosCostosVentas";
            this.Text = "Resultado Costos-Ventas";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Empresa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Envíos;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostoTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasTotales;
        private System.Windows.Forms.DataGridViewTextBoxColumn Resultado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Margen;
        private System.Windows.Forms.Button btnVolverMenuPrincipal;
    }
}