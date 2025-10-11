namespace WindowsFormsApp1
{
    partial class FormAdmisionCD
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
            this.gbEmpresaCliente = new System.Windows.Forms.GroupBox();
            this.cbEmpresaCliente = new System.Windows.Forms.ComboBox();
            this.lblEmpresaCliente = new System.Windows.Forms.Label();
            this.btnAdmitirEncomienda = new System.Windows.Forms.Button();
            this.btnVolverMenuPrincipal = new System.Windows.Forms.Button();
            this.gbDatosDestinatario = new System.Windows.Forms.GroupBox();
            this.txtDomicilioDestinatario = new System.Windows.Forms.TextBox();
            this.txtLocalidadDestinatario = new System.Windows.Forms.TextBox();
            this.lblDomicilio = new System.Windows.Forms.Label();
            this.txtDniDestinatario = new System.Windows.Forms.TextBox();
            this.lblLocalidad = new System.Windows.Forms.Label();
            this.lblDni = new System.Windows.Forms.Label();
            this.lblProvinciaEnvio = new System.Windows.Forms.Label();
            this.gbDetalleEncomiendas = new System.Windows.Forms.GroupBox();
            this.btnBorrarEncomienda = new System.Windows.Forms.Button();
            this.btnGuardarEncomienda = new System.Windows.Forms.Button();
            this.numericCantidadEncomienda = new System.Windows.Forms.NumericUpDown();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.cbDimension = new System.Windows.Forms.ComboBox();
            this.lblDimension = new System.Windows.Forms.Label();
            this.gbDetalleEnvio = new System.Windows.Forms.GroupBox();
            this.lblTipoServicioEnvio = new System.Windows.Forms.Label();
            this.rbEnvioAgencia = new System.Windows.Forms.RadioButton();
            this.rbEnvioCentroDistribucion = new System.Windows.Forms.RadioButton();
            this.rbEnvioDomicilio = new System.Windows.Forms.RadioButton();
            this.cbProvinciaEnvio = new System.Windows.Forms.ComboBox();
            this.gbEmpresaCliente.SuspendLayout();
            this.gbDatosDestinatario.SuspendLayout();
            this.gbDetalleEncomiendas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCantidadEncomienda)).BeginInit();
            this.gbDetalleEnvio.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbEmpresaCliente
            // 
            this.gbEmpresaCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbEmpresaCliente.Controls.Add(this.cbEmpresaCliente);
            this.gbEmpresaCliente.Controls.Add(this.lblEmpresaCliente);
            this.gbEmpresaCliente.Location = new System.Drawing.Point(11, 11);
            this.gbEmpresaCliente.Margin = new System.Windows.Forms.Padding(2);
            this.gbEmpresaCliente.Name = "gbEmpresaCliente";
            this.gbEmpresaCliente.Padding = new System.Windows.Forms.Padding(2);
            this.gbEmpresaCliente.Size = new System.Drawing.Size(361, 57);
            this.gbEmpresaCliente.TabIndex = 7;
            this.gbEmpresaCliente.TabStop = false;
            // 
            // cbEmpresaCliente
            // 
            this.cbEmpresaCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEmpresaCliente.FormattingEnabled = true;
            this.cbEmpresaCliente.Location = new System.Drawing.Point(101, 21);
            this.cbEmpresaCliente.Margin = new System.Windows.Forms.Padding(2);
            this.cbEmpresaCliente.Name = "cbEmpresaCliente";
            this.cbEmpresaCliente.Size = new System.Drawing.Size(238, 21);
            this.cbEmpresaCliente.TabIndex = 1;
            // 
            // lblEmpresaCliente
            // 
            this.lblEmpresaCliente.AutoSize = true;
            this.lblEmpresaCliente.Location = new System.Drawing.Point(13, 24);
            this.lblEmpresaCliente.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEmpresaCliente.Name = "lblEmpresaCliente";
            this.lblEmpresaCliente.Size = new System.Drawing.Size(85, 13);
            this.lblEmpresaCliente.TabIndex = 0;
            this.lblEmpresaCliente.Text = "Empresa/Cliente";
            // 
            // btnAdmitirEncomienda
            // 
            this.btnAdmitirEncomienda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdmitirEncomienda.Location = new System.Drawing.Point(101, 485);
            this.btnAdmitirEncomienda.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdmitirEncomienda.Name = "btnAdmitirEncomienda";
            this.btnAdmitirEncomienda.Size = new System.Drawing.Size(123, 27);
            this.btnAdmitirEncomienda.TabIndex = 12;
            this.btnAdmitirEncomienda.Text = "&Admitir Encomienda";
            this.btnAdmitirEncomienda.UseVisualStyleBackColor = true;
            // 
            // btnVolverMenuPrincipal
            // 
            this.btnVolverMenuPrincipal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVolverMenuPrincipal.Location = new System.Drawing.Point(228, 485);
            this.btnVolverMenuPrincipal.Margin = new System.Windows.Forms.Padding(2);
            this.btnVolverMenuPrincipal.Name = "btnVolverMenuPrincipal";
            this.btnVolverMenuPrincipal.Size = new System.Drawing.Size(145, 27);
            this.btnVolverMenuPrincipal.TabIndex = 13;
            this.btnVolverMenuPrincipal.Text = "Volver al &Menú Principal";
            this.btnVolverMenuPrincipal.UseVisualStyleBackColor = true;
            // 
            // gbDatosDestinatario
            // 
            this.gbDatosDestinatario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDatosDestinatario.Controls.Add(this.txtDomicilioDestinatario);
            this.gbDatosDestinatario.Controls.Add(this.txtLocalidadDestinatario);
            this.gbDatosDestinatario.Controls.Add(this.lblDomicilio);
            this.gbDatosDestinatario.Controls.Add(this.txtDniDestinatario);
            this.gbDatosDestinatario.Controls.Add(this.lblLocalidad);
            this.gbDatosDestinatario.Controls.Add(this.lblDni);
            this.gbDatosDestinatario.Location = new System.Drawing.Point(9, 208);
            this.gbDatosDestinatario.Margin = new System.Windows.Forms.Padding(2);
            this.gbDatosDestinatario.Name = "gbDatosDestinatario";
            this.gbDatosDestinatario.Padding = new System.Windows.Forms.Padding(2);
            this.gbDatosDestinatario.Size = new System.Drawing.Size(361, 140);
            this.gbDatosDestinatario.TabIndex = 10;
            this.gbDatosDestinatario.TabStop = false;
            this.gbDatosDestinatario.Text = "Datos Destinatario";
            // 
            // txtDomicilioDestinatario
            // 
            this.txtDomicilioDestinatario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDomicilioDestinatario.Location = new System.Drawing.Point(72, 99);
            this.txtDomicilioDestinatario.Margin = new System.Windows.Forms.Padding(2);
            this.txtDomicilioDestinatario.Name = "txtDomicilioDestinatario";
            this.txtDomicilioDestinatario.Size = new System.Drawing.Size(267, 20);
            this.txtDomicilioDestinatario.TabIndex = 6;
            // 
            // txtLocalidadDestinatario
            // 
            this.txtLocalidadDestinatario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocalidadDestinatario.Location = new System.Drawing.Point(72, 60);
            this.txtLocalidadDestinatario.Margin = new System.Windows.Forms.Padding(2);
            this.txtLocalidadDestinatario.Name = "txtLocalidadDestinatario";
            this.txtLocalidadDestinatario.Size = new System.Drawing.Size(267, 20);
            this.txtLocalidadDestinatario.TabIndex = 6;
            // 
            // lblDomicilio
            // 
            this.lblDomicilio.AutoSize = true;
            this.lblDomicilio.Location = new System.Drawing.Point(13, 102);
            this.lblDomicilio.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDomicilio.Name = "lblDomicilio";
            this.lblDomicilio.Size = new System.Drawing.Size(49, 13);
            this.lblDomicilio.TabIndex = 9;
            this.lblDomicilio.Text = "Domicilio";
            // 
            // txtDniDestinatario
            // 
            this.txtDniDestinatario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDniDestinatario.Location = new System.Drawing.Point(72, 26);
            this.txtDniDestinatario.Margin = new System.Windows.Forms.Padding(2);
            this.txtDniDestinatario.Name = "txtDniDestinatario";
            this.txtDniDestinatario.Size = new System.Drawing.Size(267, 20);
            this.txtDniDestinatario.TabIndex = 6;
            // 
            // lblLocalidad
            // 
            this.lblLocalidad.AutoSize = true;
            this.lblLocalidad.Location = new System.Drawing.Point(13, 63);
            this.lblLocalidad.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLocalidad.Name = "lblLocalidad";
            this.lblLocalidad.Size = new System.Drawing.Size(53, 13);
            this.lblLocalidad.TabIndex = 9;
            this.lblLocalidad.Text = "Localidad";
            // 
            // lblDni
            // 
            this.lblDni.AutoSize = true;
            this.lblDni.Location = new System.Drawing.Point(13, 29);
            this.lblDni.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDni.Name = "lblDni";
            this.lblDni.Size = new System.Drawing.Size(26, 13);
            this.lblDni.TabIndex = 4;
            this.lblDni.Text = "DNI";
            // 
            // lblProvinciaEnvio
            // 
            this.lblProvinciaEnvio.AutoSize = true;
            this.lblProvinciaEnvio.Location = new System.Drawing.Point(13, 28);
            this.lblProvinciaEnvio.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblProvinciaEnvio.Name = "lblProvinciaEnvio";
            this.lblProvinciaEnvio.Size = new System.Drawing.Size(51, 13);
            this.lblProvinciaEnvio.TabIndex = 4;
            this.lblProvinciaEnvio.Text = "Provincia";
            // 
            // gbDetalleEncomiendas
            // 
            this.gbDetalleEncomiendas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDetalleEncomiendas.Controls.Add(this.btnBorrarEncomienda);
            this.gbDetalleEncomiendas.Controls.Add(this.btnGuardarEncomienda);
            this.gbDetalleEncomiendas.Controls.Add(this.numericCantidadEncomienda);
            this.gbDetalleEncomiendas.Controls.Add(this.lblCantidad);
            this.gbDetalleEncomiendas.Controls.Add(this.cbDimension);
            this.gbDetalleEncomiendas.Controls.Add(this.lblDimension);
            this.gbDetalleEncomiendas.Location = new System.Drawing.Point(9, 351);
            this.gbDetalleEncomiendas.Margin = new System.Windows.Forms.Padding(2);
            this.gbDetalleEncomiendas.Name = "gbDetalleEncomiendas";
            this.gbDetalleEncomiendas.Padding = new System.Windows.Forms.Padding(2);
            this.gbDetalleEncomiendas.Size = new System.Drawing.Size(361, 122);
            this.gbDetalleEncomiendas.TabIndex = 11;
            this.gbDetalleEncomiendas.TabStop = false;
            this.gbDetalleEncomiendas.Text = "Detalle de encomiendas";
            // 
            // btnBorrarEncomienda
            // 
            this.btnBorrarEncomienda.Location = new System.Drawing.Point(274, 89);
            this.btnBorrarEncomienda.Margin = new System.Windows.Forms.Padding(2);
            this.btnBorrarEncomienda.Name = "btnBorrarEncomienda";
            this.btnBorrarEncomienda.Size = new System.Drawing.Size(68, 21);
            this.btnBorrarEncomienda.TabIndex = 7;
            this.btnBorrarEncomienda.Text = "&Borrar";
            this.btnBorrarEncomienda.UseVisualStyleBackColor = true;
            // 
            // btnGuardarEncomienda
            // 
            this.btnGuardarEncomienda.Location = new System.Drawing.Point(205, 89);
            this.btnGuardarEncomienda.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardarEncomienda.Name = "btnGuardarEncomienda";
            this.btnGuardarEncomienda.Size = new System.Drawing.Size(68, 21);
            this.btnGuardarEncomienda.TabIndex = 6;
            this.btnGuardarEncomienda.Text = "&Guardar";
            this.btnGuardarEncomienda.UseVisualStyleBackColor = true;
            // 
            // numericCantidadEncomienda
            // 
            this.numericCantidadEncomienda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericCantidadEncomienda.Location = new System.Drawing.Point(80, 55);
            this.numericCantidadEncomienda.Margin = new System.Windows.Forms.Padding(2);
            this.numericCantidadEncomienda.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericCantidadEncomienda.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericCantidadEncomienda.Name = "numericCantidadEncomienda";
            this.numericCantidadEncomienda.Size = new System.Drawing.Size(258, 20);
            this.numericCantidadEncomienda.TabIndex = 3;
            this.numericCantidadEncomienda.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(13, 58);
            this.lblCantidad.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(49, 13);
            this.lblCantidad.TabIndex = 2;
            this.lblCantidad.Text = "Cantidad";
            // 
            // cbDimension
            // 
            this.cbDimension.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDimension.FormattingEnabled = true;
            this.cbDimension.Location = new System.Drawing.Point(80, 23);
            this.cbDimension.Margin = new System.Windows.Forms.Padding(2);
            this.cbDimension.Name = "cbDimension";
            this.cbDimension.Size = new System.Drawing.Size(259, 21);
            this.cbDimension.TabIndex = 1;
            // 
            // lblDimension
            // 
            this.lblDimension.AutoSize = true;
            this.lblDimension.Location = new System.Drawing.Point(13, 26);
            this.lblDimension.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDimension.Name = "lblDimension";
            this.lblDimension.Size = new System.Drawing.Size(56, 13);
            this.lblDimension.TabIndex = 0;
            this.lblDimension.Text = "Dimensión";
            // 
            // gbDetalleEnvio
            // 
            this.gbDetalleEnvio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDetalleEnvio.Controls.Add(this.lblTipoServicioEnvio);
            this.gbDetalleEnvio.Controls.Add(this.rbEnvioAgencia);
            this.gbDetalleEnvio.Controls.Add(this.rbEnvioCentroDistribucion);
            this.gbDetalleEnvio.Controls.Add(this.rbEnvioDomicilio);
            this.gbDetalleEnvio.Controls.Add(this.cbProvinciaEnvio);
            this.gbDetalleEnvio.Controls.Add(this.lblProvinciaEnvio);
            this.gbDetalleEnvio.Location = new System.Drawing.Point(11, 72);
            this.gbDetalleEnvio.Margin = new System.Windows.Forms.Padding(2);
            this.gbDetalleEnvio.Name = "gbDetalleEnvio";
            this.gbDetalleEnvio.Padding = new System.Windows.Forms.Padding(2);
            this.gbDetalleEnvio.Size = new System.Drawing.Size(361, 131);
            this.gbDetalleEnvio.TabIndex = 9;
            this.gbDetalleEnvio.TabStop = false;
            this.gbDetalleEnvio.Text = "Detalle de envío";
            // 
            // lblTipoServicioEnvio
            // 
            this.lblTipoServicioEnvio.AutoSize = true;
            this.lblTipoServicioEnvio.Location = new System.Drawing.Point(13, 62);
            this.lblTipoServicioEnvio.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTipoServicioEnvio.Name = "lblTipoServicioEnvio";
            this.lblTipoServicioEnvio.Size = new System.Drawing.Size(82, 13);
            this.lblTipoServicioEnvio.TabIndex = 9;
            this.lblTipoServicioEnvio.Text = "Tipo de servicio";
            // 
            // rbEnvioAgencia
            // 
            this.rbEnvioAgencia.AutoSize = true;
            this.rbEnvioAgencia.Location = new System.Drawing.Point(107, 102);
            this.rbEnvioAgencia.Margin = new System.Windows.Forms.Padding(2);
            this.rbEnvioAgencia.Name = "rbEnvioAgencia";
            this.rbEnvioAgencia.Size = new System.Drawing.Size(104, 17);
            this.rbEnvioAgencia.TabIndex = 8;
            this.rbEnvioAgencia.TabStop = true;
            this.rbEnvioAgencia.Text = "Envío a agencia";
            this.rbEnvioAgencia.UseVisualStyleBackColor = true;
            // 
            // rbEnvioCentroDistribucion
            // 
            this.rbEnvioCentroDistribucion.AutoSize = true;
            this.rbEnvioCentroDistribucion.Location = new System.Drawing.Point(107, 81);
            this.rbEnvioCentroDistribucion.Margin = new System.Windows.Forms.Padding(2);
            this.rbEnvioCentroDistribucion.Name = "rbEnvioCentroDistribucion";
            this.rbEnvioCentroDistribucion.Size = new System.Drawing.Size(167, 17);
            this.rbEnvioCentroDistribucion.TabIndex = 8;
            this.rbEnvioCentroDistribucion.TabStop = true;
            this.rbEnvioCentroDistribucion.Text = "Envío a centro de distribución";
            this.rbEnvioCentroDistribucion.UseVisualStyleBackColor = true;
            // 
            // rbEnvioDomicilio
            // 
            this.rbEnvioDomicilio.AutoSize = true;
            this.rbEnvioDomicilio.Location = new System.Drawing.Point(107, 60);
            this.rbEnvioDomicilio.Margin = new System.Windows.Forms.Padding(2);
            this.rbEnvioDomicilio.Name = "rbEnvioDomicilio";
            this.rbEnvioDomicilio.Size = new System.Drawing.Size(104, 17);
            this.rbEnvioDomicilio.TabIndex = 7;
            this.rbEnvioDomicilio.TabStop = true;
            this.rbEnvioDomicilio.Text = "Envio a domicilio";
            this.rbEnvioDomicilio.UseVisualStyleBackColor = true;
            // 
            // cbProvinciaEnvio
            // 
            this.cbProvinciaEnvio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbProvinciaEnvio.FormattingEnabled = true;
            this.cbProvinciaEnvio.Location = new System.Drawing.Point(106, 25);
            this.cbProvinciaEnvio.Margin = new System.Windows.Forms.Padding(2);
            this.cbProvinciaEnvio.Name = "cbProvinciaEnvio";
            this.cbProvinciaEnvio.Size = new System.Drawing.Size(233, 21);
            this.cbProvinciaEnvio.TabIndex = 6;
            // 
            // FormAdmisionCD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 524);
            this.Controls.Add(this.gbEmpresaCliente);
            this.Controls.Add(this.btnAdmitirEncomienda);
            this.Controls.Add(this.btnVolverMenuPrincipal);
            this.Controls.Add(this.gbDatosDestinatario);
            this.Controls.Add(this.gbDetalleEncomiendas);
            this.Controls.Add(this.gbDetalleEnvio);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(401, 563);
            this.Name = "FormAdmisionCD";
            this.Text = "Imposición y Admisión de encomiendas en CD";
            this.gbEmpresaCliente.ResumeLayout(false);
            this.gbEmpresaCliente.PerformLayout();
            this.gbDatosDestinatario.ResumeLayout(false);
            this.gbDatosDestinatario.PerformLayout();
            this.gbDetalleEncomiendas.ResumeLayout(false);
            this.gbDetalleEncomiendas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCantidadEncomienda)).EndInit();
            this.gbDetalleEnvio.ResumeLayout(false);
            this.gbDetalleEnvio.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbEmpresaCliente;
        private System.Windows.Forms.ComboBox cbEmpresaCliente;
        private System.Windows.Forms.Label lblEmpresaCliente;
        private System.Windows.Forms.Button btnAdmitirEncomienda;
        private System.Windows.Forms.Button btnVolverMenuPrincipal;
        private System.Windows.Forms.GroupBox gbDatosDestinatario;
        private System.Windows.Forms.TextBox txtDomicilioDestinatario;
        private System.Windows.Forms.TextBox txtLocalidadDestinatario;
        private System.Windows.Forms.Label lblDomicilio;
        private System.Windows.Forms.TextBox txtDniDestinatario;
        private System.Windows.Forms.Label lblLocalidad;
        private System.Windows.Forms.Label lblDni;
        private System.Windows.Forms.Label lblProvinciaEnvio;
        private System.Windows.Forms.GroupBox gbDetalleEncomiendas;
        private System.Windows.Forms.Button btnBorrarEncomienda;
        private System.Windows.Forms.Button btnGuardarEncomienda;
        private System.Windows.Forms.NumericUpDown numericCantidadEncomienda;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.ComboBox cbDimension;
        private System.Windows.Forms.Label lblDimension;
        private System.Windows.Forms.GroupBox gbDetalleEnvio;
        private System.Windows.Forms.Label lblTipoServicioEnvio;
        private System.Windows.Forms.RadioButton rbEnvioAgencia;
        private System.Windows.Forms.RadioButton rbEnvioCentroDistribucion;
        private System.Windows.Forms.RadioButton rbEnvioDomicilio;
        private System.Windows.Forms.ComboBox cbProvinciaEnvio;
    }
}