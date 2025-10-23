namespace WindowsFormsApp1.Operación.ImponerCallCenter.Formulario
{
    partial class FormImponerCallCenter
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbDetalleEncomiendas = new System.Windows.Forms.GroupBox();
            this.btnQuitarEncomienda = new System.Windows.Forms.Button();
            this.lstEncomiendas = new System.Windows.Forms.ListBox();
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
            this.lblProvinciaEnvio = new System.Windows.Forms.Label();
            this.gbDatosDestinatario = new System.Windows.Forms.GroupBox();
            this.cmbAgenciaEnvio = new System.Windows.Forms.ComboBox();
            this.lblAgenciaiEnvio = new System.Windows.Forms.Label();
            this.txtDomicilioDestinatario = new System.Windows.Forms.TextBox();
            this.txtLocalidadDestinatario = new System.Windows.Forms.TextBox();
            this.lblDomicilio = new System.Windows.Forms.Label();
            this.txtDniDestinatario = new System.Windows.Forms.TextBox();
            this.lblLocalidad = new System.Windows.Forms.Label();
            this.lblDni = new System.Windows.Forms.Label();
            this.btnAceptarPedido = new System.Windows.Forms.Button();
            this.btnVolverMenuPrincipal = new System.Windows.Forms.Button();
            this.lblEmpresaCliente = new System.Windows.Forms.Label();
            this.cbEmpresaCliente = new System.Windows.Forms.ComboBox();
            this.gbEmpresaCliente = new System.Windows.Forms.GroupBox();
            this.gbDetalleRetiro = new System.Windows.Forms.GroupBox();
            this.lblTipoServicioRetiro = new System.Windows.Forms.Label();
            this.rbRetiroAgencia = new System.Windows.Forms.RadioButton();
            this.rbRetiroDomicilio = new System.Windows.Forms.RadioButton();
            this.cbAgenciaRetiro = new System.Windows.Forms.ComboBox();
            this.lblAgencia = new System.Windows.Forms.Label();
            this.gbDetalleEncomiendas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCantidadEncomienda)).BeginInit();
            this.gbDetalleEnvio.SuspendLayout();
            this.gbDatosDestinatario.SuspendLayout();
            this.gbEmpresaCliente.SuspendLayout();
            this.gbDetalleRetiro.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDetalleEncomiendas
            // 
            this.gbDetalleEncomiendas.Controls.Add(this.btnQuitarEncomienda);
            this.gbDetalleEncomiendas.Controls.Add(this.lstEncomiendas);
            this.gbDetalleEncomiendas.Controls.Add(this.btnGuardarEncomienda);
            this.gbDetalleEncomiendas.Controls.Add(this.numericCantidadEncomienda);
            this.gbDetalleEncomiendas.Controls.Add(this.lblCantidad);
            this.gbDetalleEncomiendas.Controls.Add(this.cbDimension);
            this.gbDetalleEncomiendas.Controls.Add(this.lblDimension);
            this.gbDetalleEncomiendas.Location = new System.Drawing.Point(9, 495);
            this.gbDetalleEncomiendas.Margin = new System.Windows.Forms.Padding(2);
            this.gbDetalleEncomiendas.Name = "gbDetalleEncomiendas";
            this.gbDetalleEncomiendas.Padding = new System.Windows.Forms.Padding(2);
            this.gbDetalleEncomiendas.Size = new System.Drawing.Size(362, 230);
            this.gbDetalleEncomiendas.TabIndex = 4;
            this.gbDetalleEncomiendas.TabStop = false;
            this.gbDetalleEncomiendas.Text = "Detalle de encomiendas";
            // 
            // btnQuitarEncomienda
            // 
            this.btnQuitarEncomienda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuitarEncomienda.Location = new System.Drawing.Point(272, 195);
            this.btnQuitarEncomienda.Name = "btnQuitarEncomienda";
            this.btnQuitarEncomienda.Size = new System.Drawing.Size(68, 23);
            this.btnQuitarEncomienda.TabIndex = 6;
            this.btnQuitarEncomienda.Text = "&Quitar";
            this.btnQuitarEncomienda.UseVisualStyleBackColor = true;
            // 
            // lstEncomiendas
            // 
            this.lstEncomiendas.BackColor = System.Drawing.Color.Gainsboro;
            this.lstEncomiendas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstEncomiendas.FormattingEnabled = true;
            this.lstEncomiendas.IntegralHeight = false;
            this.lstEncomiendas.ItemHeight = 15;
            this.lstEncomiendas.Location = new System.Drawing.Point(16, 88);
            this.lstEncomiendas.Name = "lstEncomiendas";
            this.lstEncomiendas.Size = new System.Drawing.Size(324, 94);
            this.lstEncomiendas.TabIndex = 4;
            // 
            // btnGuardarEncomienda
            // 
            this.btnGuardarEncomienda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardarEncomienda.Location = new System.Drawing.Point(199, 195);
            this.btnGuardarEncomienda.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardarEncomienda.Name = "btnGuardarEncomienda";
            this.btnGuardarEncomienda.Size = new System.Drawing.Size(68, 23);
            this.btnGuardarEncomienda.TabIndex = 5;
            this.btnGuardarEncomienda.Text = "&Guardar";
            this.btnGuardarEncomienda.UseVisualStyleBackColor = true;
            // 
            // numericCantidadEncomienda
            // 
            this.numericCantidadEncomienda.Location = new System.Drawing.Point(81, 55);
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
            this.numericCantidadEncomienda.Size = new System.Drawing.Size(259, 20);
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
            this.cbDimension.FormattingEnabled = true;
            this.cbDimension.Location = new System.Drawing.Point(81, 23);
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
            this.gbDetalleEnvio.Controls.Add(this.lblTipoServicioEnvio);
            this.gbDetalleEnvio.Controls.Add(this.rbEnvioAgencia);
            this.gbDetalleEnvio.Controls.Add(this.rbEnvioCentroDistribucion);
            this.gbDetalleEnvio.Controls.Add(this.rbEnvioDomicilio);
            this.gbDetalleEnvio.Controls.Add(this.cbProvinciaEnvio);
            this.gbDetalleEnvio.Controls.Add(this.lblProvinciaEnvio);
            this.gbDetalleEnvio.Location = new System.Drawing.Point(11, 184);
            this.gbDetalleEnvio.Margin = new System.Windows.Forms.Padding(2);
            this.gbDetalleEnvio.Name = "gbDetalleEnvio";
            this.gbDetalleEnvio.Padding = new System.Windows.Forms.Padding(2);
            this.gbDetalleEnvio.Size = new System.Drawing.Size(362, 131);
            this.gbDetalleEnvio.TabIndex = 2;
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
            this.lblTipoServicioEnvio.TabIndex = 2;
            this.lblTipoServicioEnvio.Text = "Tipo de servicio";
            // 
            // rbEnvioAgencia
            // 
            this.rbEnvioAgencia.AutoSize = true;
            this.rbEnvioAgencia.Location = new System.Drawing.Point(107, 102);
            this.rbEnvioAgencia.Margin = new System.Windows.Forms.Padding(2);
            this.rbEnvioAgencia.Name = "rbEnvioAgencia";
            this.rbEnvioAgencia.Size = new System.Drawing.Size(104, 17);
            this.rbEnvioAgencia.TabIndex = 5;
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
            this.rbEnvioCentroDistribucion.TabIndex = 4;
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
            this.rbEnvioDomicilio.TabIndex = 3;
            this.rbEnvioDomicilio.TabStop = true;
            this.rbEnvioDomicilio.Text = "Envio a domicilio";
            this.rbEnvioDomicilio.UseVisualStyleBackColor = true;
            // 
            // cbProvinciaEnvio
            // 
            this.cbProvinciaEnvio.FormattingEnabled = true;
            this.cbProvinciaEnvio.Location = new System.Drawing.Point(107, 25);
            this.cbProvinciaEnvio.Margin = new System.Windows.Forms.Padding(2);
            this.cbProvinciaEnvio.Name = "cbProvinciaEnvio";
            this.cbProvinciaEnvio.Size = new System.Drawing.Size(231, 21);
            this.cbProvinciaEnvio.TabIndex = 1;
            // 
            // lblProvinciaEnvio
            // 
            this.lblProvinciaEnvio.AutoSize = true;
            this.lblProvinciaEnvio.Location = new System.Drawing.Point(13, 28);
            this.lblProvinciaEnvio.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblProvinciaEnvio.Name = "lblProvinciaEnvio";
            this.lblProvinciaEnvio.Size = new System.Drawing.Size(51, 13);
            this.lblProvinciaEnvio.TabIndex = 0;
            this.lblProvinciaEnvio.Text = "Provincia";
            // 
            // gbDatosDestinatario
            // 
            this.gbDatosDestinatario.Controls.Add(this.cmbAgenciaEnvio);
            this.gbDatosDestinatario.Controls.Add(this.lblAgenciaiEnvio);
            this.gbDatosDestinatario.Controls.Add(this.txtDomicilioDestinatario);
            this.gbDatosDestinatario.Controls.Add(this.txtLocalidadDestinatario);
            this.gbDatosDestinatario.Controls.Add(this.lblDomicilio);
            this.gbDatosDestinatario.Controls.Add(this.txtDniDestinatario);
            this.gbDatosDestinatario.Controls.Add(this.lblLocalidad);
            this.gbDatosDestinatario.Controls.Add(this.lblDni);
            this.gbDatosDestinatario.Location = new System.Drawing.Point(9, 320);
            this.gbDatosDestinatario.Margin = new System.Windows.Forms.Padding(2);
            this.gbDatosDestinatario.Name = "gbDatosDestinatario";
            this.gbDatosDestinatario.Padding = new System.Windows.Forms.Padding(2);
            this.gbDatosDestinatario.Size = new System.Drawing.Size(362, 171);
            this.gbDatosDestinatario.TabIndex = 3;
            this.gbDatosDestinatario.TabStop = false;
            this.gbDatosDestinatario.Text = "Datos Destinatario";
            // 
            // cmbAgenciaEnvio
            // 
            this.cmbAgenciaEnvio.FormattingEnabled = true;
            this.cmbAgenciaEnvio.Location = new System.Drawing.Point(73, 134);
            this.cmbAgenciaEnvio.Margin = new System.Windows.Forms.Padding(2);
            this.cmbAgenciaEnvio.Name = "cmbAgenciaEnvio";
            this.cmbAgenciaEnvio.Size = new System.Drawing.Size(267, 21);
            this.cmbAgenciaEnvio.TabIndex = 7;
            // 
            // lblAgenciaiEnvio
            // 
            this.lblAgenciaiEnvio.AutoSize = true;
            this.lblAgenciaiEnvio.Location = new System.Drawing.Point(13, 137);
            this.lblAgenciaiEnvio.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAgenciaiEnvio.Name = "lblAgenciaiEnvio";
            this.lblAgenciaiEnvio.Size = new System.Drawing.Size(46, 13);
            this.lblAgenciaiEnvio.TabIndex = 6;
            this.lblAgenciaiEnvio.Text = "Agencia";
            // 
            // txtDomicilioDestinatario
            // 
            this.txtDomicilioDestinatario.Location = new System.Drawing.Point(73, 99);
            this.txtDomicilioDestinatario.Margin = new System.Windows.Forms.Padding(2);
            this.txtDomicilioDestinatario.Name = "txtDomicilioDestinatario";
            this.txtDomicilioDestinatario.Size = new System.Drawing.Size(267, 20);
            this.txtDomicilioDestinatario.TabIndex = 5;
            // 
            // txtLocalidadDestinatario
            // 
            this.txtLocalidadDestinatario.Location = new System.Drawing.Point(73, 60);
            this.txtLocalidadDestinatario.Margin = new System.Windows.Forms.Padding(2);
            this.txtLocalidadDestinatario.Name = "txtLocalidadDestinatario";
            this.txtLocalidadDestinatario.Size = new System.Drawing.Size(267, 20);
            this.txtLocalidadDestinatario.TabIndex = 3;
            // 
            // lblDomicilio
            // 
            this.lblDomicilio.AutoSize = true;
            this.lblDomicilio.Location = new System.Drawing.Point(13, 102);
            this.lblDomicilio.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDomicilio.Name = "lblDomicilio";
            this.lblDomicilio.Size = new System.Drawing.Size(49, 13);
            this.lblDomicilio.TabIndex = 4;
            this.lblDomicilio.Text = "Domicilio";
            // 
            // txtDniDestinatario
            // 
            this.txtDniDestinatario.Location = new System.Drawing.Point(73, 26);
            this.txtDniDestinatario.Margin = new System.Windows.Forms.Padding(2);
            this.txtDniDestinatario.Name = "txtDniDestinatario";
            this.txtDniDestinatario.Size = new System.Drawing.Size(267, 20);
            this.txtDniDestinatario.TabIndex = 1;
            // 
            // lblLocalidad
            // 
            this.lblLocalidad.AutoSize = true;
            this.lblLocalidad.Location = new System.Drawing.Point(13, 63);
            this.lblLocalidad.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLocalidad.Name = "lblLocalidad";
            this.lblLocalidad.Size = new System.Drawing.Size(53, 13);
            this.lblLocalidad.TabIndex = 2;
            this.lblLocalidad.Text = "Localidad";
            // 
            // lblDni
            // 
            this.lblDni.AutoSize = true;
            this.lblDni.Location = new System.Drawing.Point(13, 29);
            this.lblDni.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDni.Name = "lblDni";
            this.lblDni.Size = new System.Drawing.Size(26, 13);
            this.lblDni.TabIndex = 1;
            this.lblDni.Text = "DNI";
            // 
            // btnAceptarPedido
            // 
            this.btnAceptarPedido.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptarPedido.Location = new System.Drawing.Point(116, 739);
            this.btnAceptarPedido.Margin = new System.Windows.Forms.Padding(2);
            this.btnAceptarPedido.Name = "btnAceptarPedido";
            this.btnAceptarPedido.Size = new System.Drawing.Size(106, 27);
            this.btnAceptarPedido.TabIndex = 5;
            this.btnAceptarPedido.Text = "&Aceptar pedido";
            this.btnAceptarPedido.UseVisualStyleBackColor = true;
            // 
            // btnVolverMenuPrincipal
            // 
            this.btnVolverMenuPrincipal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVolverMenuPrincipal.Location = new System.Drawing.Point(226, 739);
            this.btnVolverMenuPrincipal.Margin = new System.Windows.Forms.Padding(2);
            this.btnVolverMenuPrincipal.Name = "btnVolverMenuPrincipal";
            this.btnVolverMenuPrincipal.Size = new System.Drawing.Size(145, 27);
            this.btnVolverMenuPrincipal.TabIndex = 6;
            this.btnVolverMenuPrincipal.Text = "Volver al &Menú Principal";
            this.btnVolverMenuPrincipal.UseVisualStyleBackColor = true;
            this.btnVolverMenuPrincipal.Click += new System.EventHandler(this.btnVolverMenuPrincipal_Click);
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
            // cbEmpresaCliente
            // 
            this.cbEmpresaCliente.FormattingEnabled = true;
            this.cbEmpresaCliente.Location = new System.Drawing.Point(102, 21);
            this.cbEmpresaCliente.Margin = new System.Windows.Forms.Padding(2);
            this.cbEmpresaCliente.Name = "cbEmpresaCliente";
            this.cbEmpresaCliente.Size = new System.Drawing.Size(236, 21);
            this.cbEmpresaCliente.TabIndex = 1;
            // 
            // gbEmpresaCliente
            // 
            this.gbEmpresaCliente.Controls.Add(this.cbEmpresaCliente);
            this.gbEmpresaCliente.Controls.Add(this.lblEmpresaCliente);
            this.gbEmpresaCliente.Location = new System.Drawing.Point(11, 9);
            this.gbEmpresaCliente.Margin = new System.Windows.Forms.Padding(2);
            this.gbEmpresaCliente.Name = "gbEmpresaCliente";
            this.gbEmpresaCliente.Padding = new System.Windows.Forms.Padding(2);
            this.gbEmpresaCliente.Size = new System.Drawing.Size(362, 57);
            this.gbEmpresaCliente.TabIndex = 0;
            this.gbEmpresaCliente.TabStop = false;
            // 
            // gbDetalleRetiro
            // 
            this.gbDetalleRetiro.Controls.Add(this.lblTipoServicioRetiro);
            this.gbDetalleRetiro.Controls.Add(this.rbRetiroAgencia);
            this.gbDetalleRetiro.Controls.Add(this.rbRetiroDomicilio);
            this.gbDetalleRetiro.Controls.Add(this.cbAgenciaRetiro);
            this.gbDetalleRetiro.Controls.Add(this.lblAgencia);
            this.gbDetalleRetiro.Location = new System.Drawing.Point(11, 69);
            this.gbDetalleRetiro.Margin = new System.Windows.Forms.Padding(2);
            this.gbDetalleRetiro.Name = "gbDetalleRetiro";
            this.gbDetalleRetiro.Padding = new System.Windows.Forms.Padding(2);
            this.gbDetalleRetiro.Size = new System.Drawing.Size(362, 111);
            this.gbDetalleRetiro.TabIndex = 1;
            this.gbDetalleRetiro.TabStop = false;
            this.gbDetalleRetiro.Text = "Detalle de retiro";
            // 
            // lblTipoServicioRetiro
            // 
            this.lblTipoServicioRetiro.AutoSize = true;
            this.lblTipoServicioRetiro.Location = new System.Drawing.Point(13, 25);
            this.lblTipoServicioRetiro.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTipoServicioRetiro.Name = "lblTipoServicioRetiro";
            this.lblTipoServicioRetiro.Size = new System.Drawing.Size(82, 13);
            this.lblTipoServicioRetiro.TabIndex = 0;
            this.lblTipoServicioRetiro.Text = "Tipo de servicio";
            // 
            // rbRetiroAgencia
            // 
            this.rbRetiroAgencia.AutoSize = true;
            this.rbRetiroAgencia.Location = new System.Drawing.Point(109, 44);
            this.rbRetiroAgencia.Margin = new System.Windows.Forms.Padding(2);
            this.rbRetiroAgencia.Name = "rbRetiroAgencia";
            this.rbRetiroAgencia.Size = new System.Drawing.Size(109, 17);
            this.rbRetiroAgencia.TabIndex = 2;
            this.rbRetiroAgencia.TabStop = true;
            this.rbRetiroAgencia.Text = "Retiro en agencia";
            this.rbRetiroAgencia.UseVisualStyleBackColor = true;
            // 
            // rbRetiroDomicilio
            // 
            this.rbRetiroDomicilio.AutoSize = true;
            this.rbRetiroDomicilio.Location = new System.Drawing.Point(109, 23);
            this.rbRetiroDomicilio.Margin = new System.Windows.Forms.Padding(2);
            this.rbRetiroDomicilio.Name = "rbRetiroDomicilio";
            this.rbRetiroDomicilio.Size = new System.Drawing.Size(111, 17);
            this.rbRetiroDomicilio.TabIndex = 1;
            this.rbRetiroDomicilio.TabStop = true;
            this.rbRetiroDomicilio.Text = "Retiro en domicilio";
            this.rbRetiroDomicilio.UseVisualStyleBackColor = true;
            // 
            // cbAgenciaRetiro
            // 
            this.cbAgenciaRetiro.FormattingEnabled = true;
            this.cbAgenciaRetiro.Location = new System.Drawing.Point(107, 73);
            this.cbAgenciaRetiro.Margin = new System.Windows.Forms.Padding(2);
            this.cbAgenciaRetiro.Name = "cbAgenciaRetiro";
            this.cbAgenciaRetiro.Size = new System.Drawing.Size(231, 21);
            this.cbAgenciaRetiro.TabIndex = 4;
            // 
            // lblAgencia
            // 
            this.lblAgencia.AutoSize = true;
            this.lblAgencia.Location = new System.Drawing.Point(13, 76);
            this.lblAgencia.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAgencia.Name = "lblAgencia";
            this.lblAgencia.Size = new System.Drawing.Size(46, 13);
            this.lblAgencia.TabIndex = 3;
            this.lblAgencia.Text = "Agencia";
            // 
            // FormImponerCallCenter
            // 
            this.AcceptButton = this.btnAceptarPedido;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 777);
            this.Controls.Add(this.gbDetalleRetiro);
            this.Controls.Add(this.gbEmpresaCliente);
            this.Controls.Add(this.gbDetalleEnvio);
            this.Controls.Add(this.gbDatosDestinatario);
            this.Controls.Add(this.gbDetalleEncomiendas);
            this.Controls.Add(this.btnAceptarPedido);
            this.Controls.Add(this.btnVolverMenuPrincipal);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(401, 673);
            this.Name = "FormImponerCallCenter";
            this.Text = "Imponer - Call Center";
            this.gbDetalleEncomiendas.ResumeLayout(false);
            this.gbDetalleEncomiendas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCantidadEncomienda)).EndInit();
            this.gbDetalleEnvio.ResumeLayout(false);
            this.gbDetalleEnvio.PerformLayout();
            this.gbDatosDestinatario.ResumeLayout(false);
            this.gbDatosDestinatario.PerformLayout();
            this.gbEmpresaCliente.ResumeLayout(false);
            this.gbEmpresaCliente.PerformLayout();
            this.gbDetalleRetiro.ResumeLayout(false);
            this.gbDetalleRetiro.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbDetalleEncomiendas;
        private System.Windows.Forms.NumericUpDown numericCantidadEncomienda;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.ComboBox cbDimension;
        private System.Windows.Forms.Label lblDimension;
        private System.Windows.Forms.GroupBox gbDetalleEnvio;
        private System.Windows.Forms.ComboBox cbProvinciaEnvio;
        private System.Windows.Forms.Label lblProvinciaEnvio;
        private System.Windows.Forms.Label lblTipoServicioEnvio;
        private System.Windows.Forms.RadioButton rbEnvioCentroDistribucion;
        private System.Windows.Forms.RadioButton rbEnvioDomicilio;
        private System.Windows.Forms.GroupBox gbDatosDestinatario;
        private System.Windows.Forms.Label lblLocalidad;
        private System.Windows.Forms.Label lblDni;
        private System.Windows.Forms.Label lblDomicilio;
        private System.Windows.Forms.TextBox txtLocalidadDestinatario;
        private System.Windows.Forms.TextBox txtDniDestinatario;
        private System.Windows.Forms.Button btnAceptarPedido;
        private System.Windows.Forms.Button btnVolverMenuPrincipal;
        private System.Windows.Forms.Button btnGuardarEncomienda;
        private System.Windows.Forms.Label lblEmpresaCliente;
        private System.Windows.Forms.ComboBox cbEmpresaCliente;
        private System.Windows.Forms.GroupBox gbEmpresaCliente;
        private System.Windows.Forms.GroupBox gbDetalleRetiro;
        private System.Windows.Forms.Label lblTipoServicioRetiro;
        private System.Windows.Forms.RadioButton rbRetiroAgencia;
        private System.Windows.Forms.RadioButton rbRetiroDomicilio;
        private System.Windows.Forms.ComboBox cbAgenciaRetiro;
        private System.Windows.Forms.Label lblAgencia;
        private System.Windows.Forms.RadioButton rbEnvioAgencia;
        private System.Windows.Forms.TextBox txtDomicilioDestinatario;
        private System.Windows.Forms.ComboBox cmbAgenciaEnvio;
        private System.Windows.Forms.Label lblAgenciaiEnvio;
        private System.Windows.Forms.Button btnQuitarEncomienda;
        private System.Windows.Forms.ListBox lstEncomiendas;
    }
}