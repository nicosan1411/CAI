namespace WindowsFormsApp1
{
    partial class FormCuentaCorriente
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
            this.btnBuscar = new System.Windows.Forms.Button();
            this.cbCliente = new System.Windows.Forms.ComboBox();
            this.lblClientes = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblDesde = new System.Windows.Forms.Label();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.gbEstadoCuenta = new System.Windows.Forms.GroupBox();
            this.lvCuentasCorrientes = new System.Windows.Forms.ListView();
            this.columnCliente = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnFecha = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnNroComprobante = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnConcepto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnMonto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPago = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSaldo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnVolverMenuPrincipal = new System.Windows.Forms.Button();
            this.gbEstadoCuenta.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(833, 18);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(2);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(67, 27);
            this.btnBuscar.TabIndex = 5;
            this.btnBuscar.Text = "&Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // cbCliente
            // 
            this.cbCliente.FormattingEnabled = true;
            this.cbCliente.Location = new System.Drawing.Point(66, 21);
            this.cbCliente.Margin = new System.Windows.Forms.Padding(2);
            this.cbCliente.Name = "cbCliente";
            this.cbCliente.Size = new System.Drawing.Size(481, 21);
            this.cbCliente.TabIndex = 4;
            // 
            // lblClientes
            // 
            this.lblClientes.AutoSize = true;
            this.lblClientes.Location = new System.Drawing.Point(20, 25);
            this.lblClientes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblClientes.Name = "lblClientes";
            this.lblClientes.Size = new System.Drawing.Size(42, 13);
            this.lblClientes.TabIndex = 3;
            this.lblClientes.Text = "Cliente:";
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(596, 22);
            this.dtpDesde.Margin = new System.Windows.Forms.Padding(2);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(88, 20);
            this.dtpDesde.TabIndex = 6;
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Location = new System.Drawing.Point(551, 25);
            this.lblDesde.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(41, 13);
            this.lblDesde.TabIndex = 7;
            this.lblDesde.Text = "Desde:";
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(688, 25);
            this.lblHasta.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(38, 13);
            this.lblHasta.TabIndex = 9;
            this.lblHasta.Text = "Hasta:";
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(730, 22);
            this.dtpHasta.Margin = new System.Windows.Forms.Padding(2);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(88, 20);
            this.dtpHasta.TabIndex = 8;
            // 
            // gbEstadoCuenta
            // 
            this.gbEstadoCuenta.Controls.Add(this.lvCuentasCorrientes);
            this.gbEstadoCuenta.Location = new System.Drawing.Point(11, 59);
            this.gbEstadoCuenta.Margin = new System.Windows.Forms.Padding(2);
            this.gbEstadoCuenta.Name = "gbEstadoCuenta";
            this.gbEstadoCuenta.Padding = new System.Windows.Forms.Padding(2);
            this.gbEstadoCuenta.Size = new System.Drawing.Size(889, 174);
            this.gbEstadoCuenta.TabIndex = 10;
            this.gbEstadoCuenta.TabStop = false;
            this.gbEstadoCuenta.Text = "Estado de Cuenta";
            // 
            // lvCuentasCorrientes
            // 
            this.lvCuentasCorrientes.BackColor = System.Drawing.Color.Gainsboro;
            this.lvCuentasCorrientes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnCliente,
            this.columnFecha,
            this.columnNroComprobante,
            this.columnConcepto,
            this.columnMonto,
            this.columnPago,
            this.columnSaldo});
            this.lvCuentasCorrientes.FullRowSelect = true;
            this.lvCuentasCorrientes.GridLines = true;
            this.lvCuentasCorrientes.HideSelection = false;
            this.lvCuentasCorrientes.Location = new System.Drawing.Point(12, 28);
            this.lvCuentasCorrientes.Margin = new System.Windows.Forms.Padding(2);
            this.lvCuentasCorrientes.MultiSelect = false;
            this.lvCuentasCorrientes.Name = "lvCuentasCorrientes";
            this.lvCuentasCorrientes.Size = new System.Drawing.Size(860, 122);
            this.lvCuentasCorrientes.TabIndex = 13;
            this.lvCuentasCorrientes.UseCompatibleStateImageBehavior = false;
            this.lvCuentasCorrientes.View = System.Windows.Forms.View.Details;
            // 
            // columnCliente
            // 
            this.columnCliente.Text = "Cliente";
            this.columnCliente.Width = 200;
            // 
            // columnFecha
            // 
            this.columnFecha.Text = "Fecha";
            this.columnFecha.Width = 80;
            // 
            // columnNroComprobante
            // 
            this.columnNroComprobante.Text = "N° Comprobante";
            this.columnNroComprobante.Width = 110;
            // 
            // columnConcepto
            // 
            this.columnConcepto.Text = "Concepto";
            this.columnConcepto.Width = 260;
            // 
            // columnMonto
            // 
            this.columnMonto.Text = "Monto";
            this.columnMonto.Width = 70;
            // 
            // columnPago
            // 
            this.columnPago.Text = "Pago";
            this.columnPago.Width = 70;
            // 
            // columnSaldo
            // 
            this.columnSaldo.Text = "Saldo";
            this.columnSaldo.Width = 66;
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(663, 246);
            this.btnExportar.Margin = new System.Windows.Forms.Padding(2);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(88, 27);
            this.btnExportar.TabIndex = 11;
            this.btnExportar.Text = "&Exportar";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnVolverMenuPrincipal
            // 
            this.btnVolverMenuPrincipal.Location = new System.Drawing.Point(755, 246);
            this.btnVolverMenuPrincipal.Margin = new System.Windows.Forms.Padding(2);
            this.btnVolverMenuPrincipal.Name = "btnVolverMenuPrincipal";
            this.btnVolverMenuPrincipal.Size = new System.Drawing.Size(145, 27);
            this.btnVolverMenuPrincipal.TabIndex = 21;
            this.btnVolverMenuPrincipal.Text = "Volver al &Menú Principal";
            this.btnVolverMenuPrincipal.UseVisualStyleBackColor = true;
            this.btnVolverMenuPrincipal.Click += new System.EventHandler(this.btnVolverMenuPrincipal_Click);
            // 
            // FormCuentaCorriente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 284);
            this.Controls.Add(this.btnVolverMenuPrincipal);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.gbEstadoCuenta);
            this.Controls.Add(this.lblHasta);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.lblDesde);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.cbCliente);
            this.Controls.Add(this.lblClientes);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(927, 323);
            this.Name = "FormCuentaCorriente";
            this.Text = "Consulta Estado Cuenta Corriente";
            this.gbEstadoCuenta.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.ComboBox cbCliente;
        private System.Windows.Forms.Label lblClientes;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.GroupBox gbEstadoCuenta;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.ListView lvCuentasCorrientes;
        private System.Windows.Forms.ColumnHeader columnCliente;
        private System.Windows.Forms.ColumnHeader columnFecha;
        private System.Windows.Forms.ColumnHeader columnNroComprobante;
        private System.Windows.Forms.ColumnHeader columnConcepto;
        private System.Windows.Forms.ColumnHeader columnMonto;
        private System.Windows.Forms.ColumnHeader columnPago;
        private System.Windows.Forms.ColumnHeader columnSaldo;
        private System.Windows.Forms.Button btnVolverMenuPrincipal;
    }
}