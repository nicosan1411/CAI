namespace WindowsFormsApp1
{
    partial class FormEmitirFactura
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
            this.lblCliente = new System.Windows.Forms.Label();
            this.cbCliente = new System.Windows.Forms.ComboBox();
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.gbClientesAFacturar = new System.Windows.Forms.GroupBox();
            this.lvFacturasClientes = new System.Windows.Forms.ListView();
            this.columnNroGuia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnConcepto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnMonto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnEmitirFactura = new System.Windows.Forms.Button();
            this.btnVolverMenuPrincipal = new System.Windows.Forms.Button();
            this.gbClientesAFacturar.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(21, 21);
            this.lblCliente.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(42, 13);
            this.lblCliente.TabIndex = 0;
            this.lblCliente.Text = "Cliente:";
            // 
            // cbCliente
            // 
            this.cbCliente.FormattingEnabled = true;
            this.cbCliente.Location = new System.Drawing.Point(69, 18);
            this.cbCliente.Margin = new System.Windows.Forms.Padding(2);
            this.cbCliente.Name = "cbCliente";
            this.cbCliente.Size = new System.Drawing.Size(307, 21);
            this.cbCliente.TabIndex = 1;
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Location = new System.Drawing.Point(380, 14);
            this.btnSeleccionar.Margin = new System.Windows.Forms.Padding(2);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(87, 27);
            this.btnSeleccionar.TabIndex = 2;
            this.btnSeleccionar.Text = "&Seleccionar";
            this.btnSeleccionar.UseVisualStyleBackColor = true;
            // 
            // gbClientesAFacturar
            // 
            this.gbClientesAFacturar.Controls.Add(this.lvFacturasClientes);
            this.gbClientesAFacturar.Location = new System.Drawing.Point(11, 50);
            this.gbClientesAFacturar.Margin = new System.Windows.Forms.Padding(2);
            this.gbClientesAFacturar.Name = "gbClientesAFacturar";
            this.gbClientesAFacturar.Padding = new System.Windows.Forms.Padding(2);
            this.gbClientesAFacturar.Size = new System.Drawing.Size(456, 169);
            this.gbClientesAFacturar.TabIndex = 3;
            this.gbClientesAFacturar.TabStop = false;
            this.gbClientesAFacturar.Text = "Clientes a Facturar";
            // 
            // lvFacturasClientes
            // 
            this.lvFacturasClientes.BackColor = System.Drawing.Color.Gainsboro;
            this.lvFacturasClientes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnNroGuia,
            this.columnConcepto,
            this.columnMonto});
            this.lvFacturasClientes.FullRowSelect = true;
            this.lvFacturasClientes.GridLines = true;
            this.lvFacturasClientes.HideSelection = false;
            this.lvFacturasClientes.Location = new System.Drawing.Point(12, 20);
            this.lvFacturasClientes.Margin = new System.Windows.Forms.Padding(2);
            this.lvFacturasClientes.MultiSelect = false;
            this.lvFacturasClientes.Name = "lvFacturasClientes";
            this.lvFacturasClientes.Size = new System.Drawing.Size(435, 143);
            this.lvFacturasClientes.TabIndex = 23;
            this.lvFacturasClientes.UseCompatibleStateImageBehavior = false;
            this.lvFacturasClientes.View = System.Windows.Forms.View.Details;
            // 
            // columnNroGuia
            // 
            this.columnNroGuia.Text = "N° Guía";
            this.columnNroGuia.Width = 90;
            // 
            // columnConcepto
            // 
            this.columnConcepto.Text = "Concepto";
            this.columnConcepto.Width = 260;
            // 
            // columnMonto
            // 
            this.columnMonto.Text = "Monto";
            this.columnMonto.Width = 80;
            // 
            // btnEmitirFactura
            // 
            this.btnEmitirFactura.Location = new System.Drawing.Point(200, 223);
            this.btnEmitirFactura.Margin = new System.Windows.Forms.Padding(2);
            this.btnEmitirFactura.Name = "btnEmitirFactura";
            this.btnEmitirFactura.Size = new System.Drawing.Size(118, 27);
            this.btnEmitirFactura.TabIndex = 4;
            this.btnEmitirFactura.Text = "&Emitir Factura";
            this.btnEmitirFactura.UseVisualStyleBackColor = true;
            // 
            // btnVolverMenuPrincipal
            // 
            this.btnVolverMenuPrincipal.Location = new System.Drawing.Point(322, 223);
            this.btnVolverMenuPrincipal.Margin = new System.Windows.Forms.Padding(2);
            this.btnVolverMenuPrincipal.Name = "btnVolverMenuPrincipal";
            this.btnVolverMenuPrincipal.Size = new System.Drawing.Size(145, 27);
            this.btnVolverMenuPrincipal.TabIndex = 22;
            this.btnVolverMenuPrincipal.Text = "Volver al &Menú Principal";
            this.btnVolverMenuPrincipal.UseVisualStyleBackColor = true;
            this.btnVolverMenuPrincipal.Click += new System.EventHandler(this.btnVolverMenuPrincipal_Click);
            // 
            // FormEmitirFactura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.btnVolverMenuPrincipal);
            this.Controls.Add(this.btnEmitirFactura);
            this.Controls.Add(this.gbClientesAFacturar);
            this.Controls.Add(this.btnSeleccionar);
            this.Controls.Add(this.cbCliente);
            this.Controls.Add(this.lblCliente);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "FormEmitirFactura";
            this.Text = "Emitir Factura";
            this.gbClientesAFacturar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.ComboBox cbCliente;
        private System.Windows.Forms.Button btnSeleccionar;
        private System.Windows.Forms.GroupBox gbClientesAFacturar;
        private System.Windows.Forms.Button btnEmitirFactura;
        private System.Windows.Forms.Button btnVolverMenuPrincipal;
        private System.Windows.Forms.ListView lvFacturasClientes;
        private System.Windows.Forms.ColumnHeader columnNroGuia;
        private System.Windows.Forms.ColumnHeader columnConcepto;
        private System.Windows.Forms.ColumnHeader columnMonto;
    }
}