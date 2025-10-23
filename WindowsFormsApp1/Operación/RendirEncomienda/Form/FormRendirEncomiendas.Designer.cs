namespace WindowsFormsApp1
{
    partial class FormRendirEncomiendas
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
            this.gbRetirosDomicilioAgenciaAdmitir = new System.Windows.Forms.GroupBox();
            this.lvRetirosDomicilioAdmitir = new System.Windows.Forms.ListView();
            this.columnEntregadoEnCD = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnNroGuiaAdmitir = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbEntregasDomicilioAgenciaRealizadas = new System.Windows.Forms.GroupBox();
            this.lvEntregasDomicilioRealizadas = new System.Windows.Forms.ListView();
            this.columnEntregadoDomicilio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnNroGuiaRealizado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnVolverMenuPrincipal = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.gbRetirosDomicilioAgenciaAdmitir.SuspendLayout();
            this.gbEntregasDomicilioAgenciaRealizadas.SuspendLayout();
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
            this.cbFletero.Size = new System.Drawing.Size(277, 21);
            this.cbFletero.TabIndex = 1;
            // 
            // gbRetirosDomicilioAgenciaAdmitir
            // 
            this.gbRetirosDomicilioAgenciaAdmitir.Controls.Add(this.lvRetirosDomicilioAdmitir);
            this.gbRetirosDomicilioAgenciaAdmitir.Location = new System.Drawing.Point(11, 51);
            this.gbRetirosDomicilioAgenciaAdmitir.Margin = new System.Windows.Forms.Padding(2);
            this.gbRetirosDomicilioAgenciaAdmitir.Name = "gbRetirosDomicilioAgenciaAdmitir";
            this.gbRetirosDomicilioAgenciaAdmitir.Padding = new System.Windows.Forms.Padding(2);
            this.gbRetirosDomicilioAgenciaAdmitir.Size = new System.Drawing.Size(327, 181);
            this.gbRetirosDomicilioAgenciaAdmitir.TabIndex = 2;
            this.gbRetirosDomicilioAgenciaAdmitir.TabStop = false;
            this.gbRetirosDomicilioAgenciaAdmitir.Text = "Retiros a domicilio o agencia a admitir";
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
            // gbEntregasDomicilioAgenciaRealizadas
            // 
            this.gbEntregasDomicilioAgenciaRealizadas.Controls.Add(this.lvEntregasDomicilioRealizadas);
            this.gbEntregasDomicilioAgenciaRealizadas.Location = new System.Drawing.Point(11, 236);
            this.gbEntregasDomicilioAgenciaRealizadas.Margin = new System.Windows.Forms.Padding(2);
            this.gbEntregasDomicilioAgenciaRealizadas.Name = "gbEntregasDomicilioAgenciaRealizadas";
            this.gbEntregasDomicilioAgenciaRealizadas.Padding = new System.Windows.Forms.Padding(2);
            this.gbEntregasDomicilioAgenciaRealizadas.Size = new System.Drawing.Size(327, 181);
            this.gbEntregasDomicilioAgenciaRealizadas.TabIndex = 4;
            this.gbEntregasDomicilioAgenciaRealizadas.TabStop = false;
            this.gbEntregasDomicilioAgenciaRealizadas.Text = "Entregas a domicilio o agencia realizadas";
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
            // btnVolverMenuPrincipal
            // 
            this.btnVolverMenuPrincipal.Location = new System.Drawing.Point(193, 428);
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
            this.btnGuardar.Location = new System.Drawing.Point(105, 428);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(84, 27);
            this.btnGuardar.TabIndex = 6;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            // 
            // FormRendirEncomiendas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 466);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnVolverMenuPrincipal);
            this.Controls.Add(this.gbRetirosDomicilioAgenciaAdmitir);
            this.Controls.Add(this.gbEntregasDomicilioAgenciaRealizadas);
            this.Controls.Add(this.cbFletero);
            this.Controls.Add(this.lblFletero);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormRendirEncomiendas";
            this.Text = "Rendir Encomiendas";
            this.gbRetirosDomicilioAgenciaAdmitir.ResumeLayout(false);
            this.gbEntregasDomicilioAgenciaRealizadas.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFletero;
        private System.Windows.Forms.ComboBox cbFletero;
        private System.Windows.Forms.GroupBox gbRetirosDomicilioAgenciaAdmitir;
        private System.Windows.Forms.GroupBox gbEntregasDomicilioAgenciaRealizadas;
        private System.Windows.Forms.Button btnVolverMenuPrincipal;
        private System.Windows.Forms.ListView lvRetirosDomicilioAdmitir;
        private System.Windows.Forms.ColumnHeader columnEntregadoEnCD;
        private System.Windows.Forms.ColumnHeader columnNroGuiaAdmitir;
        private System.Windows.Forms.ListView lvEntregasDomicilioRealizadas;
        private System.Windows.Forms.ColumnHeader columnEntregadoDomicilio;
        private System.Windows.Forms.ColumnHeader columnNroGuiaRealizado;
        private System.Windows.Forms.Button btnGuardar;
    }
}