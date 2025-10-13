namespace WindowsFormsApp1
{
    partial class FormRendicionPrimeraMilla
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
            this.lblFletero = new System.Windows.Forms.Label();
            this.cbFletero = new System.Windows.Forms.ComboBox();
            this.gbRetirarDomicilio = new System.Windows.Forms.GroupBox();
            this.lvRetirarDomicilio = new System.Windows.Forms.ListView();
            this.columnRetirarDomicilio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbRetirosDomicilioAdmitir = new System.Windows.Forms.GroupBox();
            this.lvRetirosDomicilioAdmitir = new System.Windows.Forms.ListView();
            this.columnEntregadoEnCD = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnNroGuiaAdmitir = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbEntregasDomicilioRealizadas = new System.Windows.Forms.GroupBox();
            this.lvEntregasDomicilioRealizadas = new System.Windows.Forms.ListView();
            this.columnEntregadoDomicilio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnNroGuiaRealizado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbEntregasDomicilioARealizar = new System.Windows.Forms.GroupBox();
            this.lvEntregasDomicilioARealizar = new System.Windows.Forms.ListView();
            this.columnEntregasDomicilioARealizar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnVolverMenuPrincipal = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnGenerarAsignaciones = new System.Windows.Forms.Button();
            this.gbRetirarDomicilio.SuspendLayout();
            this.gbRetirosDomicilioAdmitir.SuspendLayout();
            this.gbEntregasDomicilioRealizadas.SuspendLayout();
            this.gbEntregasDomicilioARealizar.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFletero
            // 
            this.lblFletero.AutoSize = true;
            this.lblFletero.Location = new System.Drawing.Point(18, 17);
            this.lblFletero.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFletero.Name = "lblFletero";
            this.lblFletero.Size = new System.Drawing.Size(39, 13);
            this.lblFletero.TabIndex = 0;
            this.lblFletero.Text = "Fletero";
            // 
            // cbFletero
            // 
            this.cbFletero.FormattingEnabled = true;
            this.cbFletero.Location = new System.Drawing.Point(61, 14);
            this.cbFletero.Margin = new System.Windows.Forms.Padding(2);
            this.cbFletero.Name = "cbFletero";
            this.cbFletero.Size = new System.Drawing.Size(482, 21);
            this.cbFletero.TabIndex = 1;
            // 
            // gbRetirarDomicilio
            // 
            this.gbRetirarDomicilio.Controls.Add(this.lvRetirarDomicilio);
            this.gbRetirarDomicilio.Location = new System.Drawing.Point(346, 51);
            this.gbRetirarDomicilio.Margin = new System.Windows.Forms.Padding(2);
            this.gbRetirarDomicilio.Name = "gbRetirarDomicilio";
            this.gbRetirarDomicilio.Padding = new System.Windows.Forms.Padding(2);
            this.gbRetirarDomicilio.Size = new System.Drawing.Size(327, 181);
            this.gbRetirarDomicilio.TabIndex = 3;
            this.gbRetirarDomicilio.TabStop = false;
            this.gbRetirarDomicilio.Text = "A retirar en domicilio";
            // 
            // lvRetirarDomicilio
            // 
            this.lvRetirarDomicilio.BackColor = System.Drawing.Color.Gainsboro;
            this.lvRetirarDomicilio.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnRetirarDomicilio});
            this.lvRetirarDomicilio.FullRowSelect = true;
            this.lvRetirarDomicilio.GridLines = true;
            this.lvRetirarDomicilio.HideSelection = false;
            this.lvRetirarDomicilio.Location = new System.Drawing.Point(10, 23);
            this.lvRetirarDomicilio.MultiSelect = false;
            this.lvRetirarDomicilio.Name = "lvRetirarDomicilio";
            this.lvRetirarDomicilio.Size = new System.Drawing.Size(305, 148);
            this.lvRetirarDomicilio.TabIndex = 0;
            this.lvRetirarDomicilio.UseCompatibleStateImageBehavior = false;
            this.lvRetirarDomicilio.View = System.Windows.Forms.View.Details;
            // 
            // columnRetirarDomicilio
            // 
            this.columnRetirarDomicilio.Text = "N° de guía";
            this.columnRetirarDomicilio.Width = 300;
            // 
            // gbRetirosDomicilioAdmitir
            // 
            this.gbRetirosDomicilioAdmitir.Controls.Add(this.lvRetirosDomicilioAdmitir);
            this.gbRetirosDomicilioAdmitir.Location = new System.Drawing.Point(11, 51);
            this.gbRetirosDomicilioAdmitir.Margin = new System.Windows.Forms.Padding(2);
            this.gbRetirosDomicilioAdmitir.Name = "gbRetirosDomicilioAdmitir";
            this.gbRetirosDomicilioAdmitir.Padding = new System.Windows.Forms.Padding(2);
            this.gbRetirosDomicilioAdmitir.Size = new System.Drawing.Size(327, 181);
            this.gbRetirosDomicilioAdmitir.TabIndex = 2;
            this.gbRetirosDomicilioAdmitir.TabStop = false;
            this.gbRetirosDomicilioAdmitir.Text = "Retiros a domicilio a admitir";
            // 
            // lvRetirosDomicilioAdmitir
            // 
            this.lvRetirosDomicilioAdmitir.BackColor = System.Drawing.Color.Gainsboro;
            this.lvRetirosDomicilioAdmitir.CheckBoxes = true;
            this.lvRetirosDomicilioAdmitir.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnEntregadoEnCD,
            this.columnNroGuiaAdmitir});
            this.lvRetirosDomicilioAdmitir.FullRowSelect = true;
            this.lvRetirosDomicilioAdmitir.GridLines = true;
            this.lvRetirosDomicilioAdmitir.HideSelection = false;
            this.lvRetirosDomicilioAdmitir.Location = new System.Drawing.Point(10, 23);
            this.lvRetirosDomicilioAdmitir.MultiSelect = false;
            this.lvRetirosDomicilioAdmitir.Name = "lvRetirosDomicilioAdmitir";
            this.lvRetirosDomicilioAdmitir.Size = new System.Drawing.Size(304, 148);
            this.lvRetirosDomicilioAdmitir.TabIndex = 0;
            this.lvRetirosDomicilioAdmitir.UseCompatibleStateImageBehavior = false;
            this.lvRetirosDomicilioAdmitir.View = System.Windows.Forms.View.Details;
            // 
            // columnEntregadoEnCD
            // 
            this.columnEntregadoEnCD.DisplayIndex = 1;
            this.columnEntregadoEnCD.Text = "Entregado en CD Origen";
            this.columnEntregadoEnCD.Width = 180;
            // 
            // columnNroGuiaAdmitir
            // 
            this.columnNroGuiaAdmitir.DisplayIndex = 0;
            this.columnNroGuiaAdmitir.Text = "N° de guía";
            this.columnNroGuiaAdmitir.Width = 120;
            // 
            // gbEntregasDomicilioRealizadas
            // 
            this.gbEntregasDomicilioRealizadas.Controls.Add(this.lvEntregasDomicilioRealizadas);
            this.gbEntregasDomicilioRealizadas.Location = new System.Drawing.Point(11, 236);
            this.gbEntregasDomicilioRealizadas.Margin = new System.Windows.Forms.Padding(2);
            this.gbEntregasDomicilioRealizadas.Name = "gbEntregasDomicilioRealizadas";
            this.gbEntregasDomicilioRealizadas.Padding = new System.Windows.Forms.Padding(2);
            this.gbEntregasDomicilioRealizadas.Size = new System.Drawing.Size(327, 181);
            this.gbEntregasDomicilioRealizadas.TabIndex = 4;
            this.gbEntregasDomicilioRealizadas.TabStop = false;
            this.gbEntregasDomicilioRealizadas.Text = "Entregas a domicilio realizadas";
            // 
            // lvEntregasDomicilioRealizadas
            // 
            this.lvEntregasDomicilioRealizadas.BackColor = System.Drawing.Color.Gainsboro;
            this.lvEntregasDomicilioRealizadas.CheckBoxes = true;
            this.lvEntregasDomicilioRealizadas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnEntregadoDomicilio,
            this.columnNroGuiaRealizado});
            this.lvEntregasDomicilioRealizadas.FullRowSelect = true;
            this.lvEntregasDomicilioRealizadas.GridLines = true;
            this.lvEntregasDomicilioRealizadas.HideSelection = false;
            this.lvEntregasDomicilioRealizadas.Location = new System.Drawing.Point(10, 23);
            this.lvEntregasDomicilioRealizadas.MultiSelect = false;
            this.lvEntregasDomicilioRealizadas.Name = "lvEntregasDomicilioRealizadas";
            this.lvEntregasDomicilioRealizadas.Size = new System.Drawing.Size(305, 148);
            this.lvEntregasDomicilioRealizadas.TabIndex = 0;
            this.lvEntregasDomicilioRealizadas.UseCompatibleStateImageBehavior = false;
            this.lvEntregasDomicilioRealizadas.View = System.Windows.Forms.View.Details;
            // 
            // columnEntregadoDomicilio
            // 
            this.columnEntregadoDomicilio.DisplayIndex = 1;
            this.columnEntregadoDomicilio.Text = "Entregado en Domicilio/Agencia";
            this.columnEntregadoDomicilio.Width = 180;
            // 
            // columnNroGuiaRealizado
            // 
            this.columnNroGuiaRealizado.DisplayIndex = 0;
            this.columnNroGuiaRealizado.Text = "N° de guía";
            this.columnNroGuiaRealizado.Width = 120;
            // 
            // gbEntregasDomicilioARealizar
            // 
            this.gbEntregasDomicilioARealizar.Controls.Add(this.lvEntregasDomicilioARealizar);
            this.gbEntregasDomicilioARealizar.Location = new System.Drawing.Point(346, 236);
            this.gbEntregasDomicilioARealizar.Margin = new System.Windows.Forms.Padding(2);
            this.gbEntregasDomicilioARealizar.Name = "gbEntregasDomicilioARealizar";
            this.gbEntregasDomicilioARealizar.Padding = new System.Windows.Forms.Padding(2);
            this.gbEntregasDomicilioARealizar.Size = new System.Drawing.Size(327, 181);
            this.gbEntregasDomicilioARealizar.TabIndex = 5;
            this.gbEntregasDomicilioARealizar.TabStop = false;
            this.gbEntregasDomicilioARealizar.Text = "Entregas a domicilio a realizar";
            // 
            // lvEntregasDomicilioARealizar
            // 
            this.lvEntregasDomicilioARealizar.BackColor = System.Drawing.Color.Gainsboro;
            this.lvEntregasDomicilioARealizar.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnEntregasDomicilioARealizar});
            this.lvEntregasDomicilioARealizar.FullRowSelect = true;
            this.lvEntregasDomicilioARealizar.GridLines = true;
            this.lvEntregasDomicilioARealizar.HideSelection = false;
            this.lvEntregasDomicilioARealizar.Location = new System.Drawing.Point(10, 23);
            this.lvEntregasDomicilioARealizar.MultiSelect = false;
            this.lvEntregasDomicilioARealizar.Name = "lvEntregasDomicilioARealizar";
            this.lvEntregasDomicilioARealizar.Size = new System.Drawing.Size(305, 148);
            this.lvEntregasDomicilioARealizar.TabIndex = 0;
            this.lvEntregasDomicilioARealizar.UseCompatibleStateImageBehavior = false;
            this.lvEntregasDomicilioARealizar.View = System.Windows.Forms.View.Details;
            // 
            // columnEntregasDomicilioARealizar
            // 
            this.columnEntregasDomicilioARealizar.Text = "N° de guía";
            this.columnEntregasDomicilioARealizar.Width = 300;
            // 
            // btnVolverMenuPrincipal
            // 
            this.btnVolverMenuPrincipal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVolverMenuPrincipal.Location = new System.Drawing.Point(528, 428);
            this.btnVolverMenuPrincipal.Margin = new System.Windows.Forms.Padding(2);
            this.btnVolverMenuPrincipal.Name = "btnVolverMenuPrincipal";
            this.btnVolverMenuPrincipal.Size = new System.Drawing.Size(145, 27);
            this.btnVolverMenuPrincipal.TabIndex = 7;
            this.btnVolverMenuPrincipal.Text = "Volver al Menú Principal";
            this.btnVolverMenuPrincipal.UseVisualStyleBackColor = true;
            this.btnVolverMenuPrincipal.Click += new System.EventHandler(this.btnVolverMenuPrincipal_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.Location = new System.Drawing.Point(440, 428);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(84, 27);
            this.btnGuardar.TabIndex = 6;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnVolverMenuPrincipal_Click);
            // 
            // btnGenerarAsignaciones
            // 
            this.btnGenerarAsignaciones.Location = new System.Drawing.Point(549, 12);
            this.btnGenerarAsignaciones.Name = "btnGenerarAsignaciones";
            this.btnGenerarAsignaciones.Size = new System.Drawing.Size(125, 27);
            this.btnGenerarAsignaciones.TabIndex = 8;
            this.btnGenerarAsignaciones.Text = "Generar asignaciones";
            this.btnGenerarAsignaciones.UseVisualStyleBackColor = true;
            // 
            // FormRendicionPrimeraMilla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 466);
            this.Controls.Add(this.btnGenerarAsignaciones);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnVolverMenuPrincipal);
            this.Controls.Add(this.gbRetirosDomicilioAdmitir);
            this.Controls.Add(this.gbEntregasDomicilioARealizar);
            this.Controls.Add(this.gbEntregasDomicilioRealizadas);
            this.Controls.Add(this.gbRetirarDomicilio);
            this.Controls.Add(this.cbFletero);
            this.Controls.Add(this.lblFletero);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(702, 505);
            this.Name = "FormRendicionPrimeraMilla";
            this.Text = "Rendición y Asignación Primera Milla";
            this.gbRetirarDomicilio.ResumeLayout(false);
            this.gbRetirosDomicilioAdmitir.ResumeLayout(false);
            this.gbEntregasDomicilioRealizadas.ResumeLayout(false);
            this.gbEntregasDomicilioARealizar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFletero;
        private System.Windows.Forms.ComboBox cbFletero;
        private System.Windows.Forms.GroupBox gbRetirarDomicilio;
        private System.Windows.Forms.GroupBox gbRetirosDomicilioAdmitir;
        private System.Windows.Forms.GroupBox gbEntregasDomicilioRealizadas;
        private System.Windows.Forms.GroupBox gbEntregasDomicilioARealizar;
        private System.Windows.Forms.Button btnVolverMenuPrincipal;
        private System.Windows.Forms.ListView lvRetirosDomicilioAdmitir;
        private System.Windows.Forms.ColumnHeader columnEntregadoEnCD;
        private System.Windows.Forms.ColumnHeader columnNroGuiaAdmitir;
        private System.Windows.Forms.ListView lvEntregasDomicilioRealizadas;
        private System.Windows.Forms.ColumnHeader columnEntregadoDomicilio;
        private System.Windows.Forms.ColumnHeader columnNroGuiaRealizado;
        private System.Windows.Forms.ListView lvRetirarDomicilio;
        private System.Windows.Forms.ColumnHeader columnRetirarDomicilio;
        private System.Windows.Forms.ListView lvEntregasDomicilioARealizar;
        private System.Windows.Forms.ColumnHeader columnEntregasDomicilioARealizar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnGenerarAsignaciones;
    }
}