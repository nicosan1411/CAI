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
            this.lblHasta = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblDesde = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.lvCuentasCorrientes = new System.Windows.Forms.ListView();
            this.columnEmpresa = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnEnvios = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnCostoTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnVentasTotales = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnResultado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnMargen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnVolverMenuPrincipal = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(148, 19);
            this.lblHasta.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(38, 13);
            this.lblHasta.TabIndex = 2;
            this.lblHasta.Text = "Hasta:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscar.Location = new System.Drawing.Point(282, 12);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(2);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(95, 27);
            this.btnBuscar.TabIndex = 12;
            this.btnBuscar.Text = "&Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(56, 16);
            this.dtpDesde.Margin = new System.Windows.Forms.Padding(2);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(88, 20);
            this.dtpDesde.TabIndex = 1;
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Location = new System.Drawing.Point(11, 19);
            this.lblDesde.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(41, 13);
            this.lblDesde.TabIndex = 0;
            this.lblDesde.Text = "Desde:";
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(190, 16);
            this.dtpHasta.Margin = new System.Windows.Forms.Padding(2);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(88, 20);
            this.dtpHasta.TabIndex = 3;
            // 
            // lvCuentasCorrientes
            // 
            this.lvCuentasCorrientes.BackColor = System.Drawing.Color.Gainsboro;
            this.lvCuentasCorrientes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnEmpresa,
            this.columnEnvios,
            this.columnCostoTotal,
            this.columnVentasTotales,
            this.columnResultado,
            this.columnMargen});
            this.lvCuentasCorrientes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lvCuentasCorrientes.FullRowSelect = true;
            this.lvCuentasCorrientes.GridLines = true;
            this.lvCuentasCorrientes.HideSelection = false;
            this.lvCuentasCorrientes.Location = new System.Drawing.Point(11, 43);
            this.lvCuentasCorrientes.Margin = new System.Windows.Forms.Padding(2);
            this.lvCuentasCorrientes.MultiSelect = false;
            this.lvCuentasCorrientes.Name = "lvCuentasCorrientes";
            this.lvCuentasCorrientes.Size = new System.Drawing.Size(724, 122);
            this.lvCuentasCorrientes.TabIndex = 5;
            this.lvCuentasCorrientes.UseCompatibleStateImageBehavior = false;
            this.lvCuentasCorrientes.View = System.Windows.Forms.View.Details;
            // 
            // columnEmpresa
            // 
            this.columnEmpresa.Text = "Empresa";
            this.columnEmpresa.Width = 280;
            // 
            // columnEnvios
            // 
            this.columnEnvios.Text = "Envíos";
            this.columnEnvios.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnEnvios.Width = 80;
            // 
            // columnCostoTotal
            // 
            this.columnCostoTotal.Text = "Costo Total";
            this.columnCostoTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnCostoTotal.Width = 110;
            // 
            // columnVentasTotales
            // 
            this.columnVentasTotales.Text = "Ventas Totales";
            this.columnVentasTotales.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnVentasTotales.Width = 110;
            // 
            // columnResultado
            // 
            this.columnResultado.Text = "Resultado";
            this.columnResultado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnResultado.Width = 70;
            // 
            // columnMargen
            // 
            this.columnMargen.Text = "Margen";
            this.columnMargen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnMargen.Width = 70;
            // 
            // btnVolverMenuPrincipal
            // 
            this.btnVolverMenuPrincipal.Location = new System.Drawing.Point(590, 178);
            this.btnVolverMenuPrincipal.Margin = new System.Windows.Forms.Padding(2);
            this.btnVolverMenuPrincipal.Name = "btnVolverMenuPrincipal";
            this.btnVolverMenuPrincipal.Size = new System.Drawing.Size(145, 27);
            this.btnVolverMenuPrincipal.TabIndex = 22;
            this.btnVolverMenuPrincipal.Text = "Volver al &Menú Principal";
            this.btnVolverMenuPrincipal.UseVisualStyleBackColor = true;
            // 
            // FormResultadosCostosVentas
            // 
            this.AcceptButton = this.btnBuscar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 218);
            this.Controls.Add(this.btnVolverMenuPrincipal);
            this.Controls.Add(this.lvCuentasCorrientes);
            this.Controls.Add(this.lblHasta);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.lblDesde);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.btnBuscar);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormResultadosCostosVentas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Resultado Costos-Ventas";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.ListView lvCuentasCorrientes;
        private System.Windows.Forms.ColumnHeader columnEmpresa;
        private System.Windows.Forms.ColumnHeader columnEnvios;
        private System.Windows.Forms.ColumnHeader columnCostoTotal;
        private System.Windows.Forms.ColumnHeader columnVentasTotales;
        private System.Windows.Forms.ColumnHeader columnResultado;
        private System.Windows.Forms.ColumnHeader columnMargen;
        private System.Windows.Forms.Button btnVolverMenuPrincipal;
    }
}