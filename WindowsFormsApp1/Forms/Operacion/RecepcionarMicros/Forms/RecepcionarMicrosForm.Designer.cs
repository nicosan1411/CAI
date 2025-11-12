namespace CAI_Proyecto.Forms.Operacion.RecepcionarMicros.Forms
{
    partial class RecepcionarMicrosForm
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
            this.gbRecepciones = new System.Windows.Forms.GroupBox();
            this.lvRecepciones = new System.Windows.Forms.ListView();
            this.columnNroGuia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnEntregadoEnCD = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.cbPatente = new System.Windows.Forms.ComboBox();
            this.lblPatenteMicro = new System.Windows.Forms.Label();
            this.btnVolverMenuPrincipal = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.gbRecepciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbRecepciones
            // 
            this.gbRecepciones.Controls.Add(this.lvRecepciones);
            this.gbRecepciones.Location = new System.Drawing.Point(19, 42);
            this.gbRecepciones.Margin = new System.Windows.Forms.Padding(2);
            this.gbRecepciones.Name = "gbRecepciones";
            this.gbRecepciones.Padding = new System.Windows.Forms.Padding(2);
            this.gbRecepciones.Size = new System.Drawing.Size(343, 167);
            this.gbRecepciones.TabIndex = 2;
            this.gbRecepciones.TabStop = false;
            this.gbRecepciones.Text = "Recepciones";
            // 
            // lvRecepciones
            // 
            this.lvRecepciones.BackColor = System.Drawing.Color.Gainsboro;
            this.lvRecepciones.CheckBoxes = true;
            this.lvRecepciones.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnEntregadoEnCD,
            this.columnNroGuia});
            this.lvRecepciones.FullRowSelect = true;
            this.lvRecepciones.GridLines = true;
            this.lvRecepciones.HideSelection = false;
            this.lvRecepciones.Location = new System.Drawing.Point(19, 21);
            this.lvRecepciones.MultiSelect = false;
            this.lvRecepciones.Name = "lvRecepciones";
            this.lvRecepciones.Size = new System.Drawing.Size(305, 130);
            this.lvRecepciones.TabIndex = 3;
            this.lvRecepciones.UseCompatibleStateImageBehavior = false;
            this.lvRecepciones.View = System.Windows.Forms.View.Details;
            // 
            // columnNroGuia
            // 
            this.columnNroGuia.Text = "N° Guía";
            this.columnNroGuia.Width = 168;
            // 
            // columnEntregadoEnCD
            // 
            this.columnEntregadoEnCD.Text = "Entregado en CD Destino";
            this.columnEntregadoEnCD.Width = 133;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, -16);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Fletero";
            // 
            // cbPatente
            // 
            this.cbPatente.FormattingEnabled = true;
            this.cbPatente.Location = new System.Drawing.Point(103, 12);
            this.cbPatente.Margin = new System.Windows.Forms.Padding(2);
            this.cbPatente.Name = "cbPatente";
            this.cbPatente.Size = new System.Drawing.Size(259, 21);
            this.cbPatente.TabIndex = 1;
            // 
            // lblPatenteMicro
            // 
            this.lblPatenteMicro.AutoSize = true;
            this.lblPatenteMicro.Location = new System.Drawing.Point(26, 15);
            this.lblPatenteMicro.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPatenteMicro.Name = "lblPatenteMicro";
            this.lblPatenteMicro.Size = new System.Drawing.Size(73, 13);
            this.lblPatenteMicro.TabIndex = 0;
            this.lblPatenteMicro.Text = "Patente Micro";
            // 
            // btnVolverMenuPrincipal
            // 
            this.btnVolverMenuPrincipal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVolverMenuPrincipal.Location = new System.Drawing.Point(217, 222);
            this.btnVolverMenuPrincipal.Margin = new System.Windows.Forms.Padding(2);
            this.btnVolverMenuPrincipal.Name = "btnVolverMenuPrincipal";
            this.btnVolverMenuPrincipal.Size = new System.Drawing.Size(145, 25);
            this.btnVolverMenuPrincipal.TabIndex = 7;
            this.btnVolverMenuPrincipal.Text = "Volver al Menú Principal";
            this.btnVolverMenuPrincipal.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.Location = new System.Drawing.Point(142, 222);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(71, 25);
            this.btnGuardar.TabIndex = 6;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            // 
            // RecepcionarMicrosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 262);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnVolverMenuPrincipal);
            this.Controls.Add(this.cbPatente);
            this.Controls.Add(this.lblPatenteMicro);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbRecepciones);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(402, 301);
            this.MinimumSize = new System.Drawing.Size(402, 301);
            this.Name = "RecepcionarMicrosForm";
            this.Text = "Recepcionar Micros";
            this.gbRecepciones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPatente;
        private System.Windows.Forms.Label lblPatenteMicro;
        private System.Windows.Forms.Button btnVolverMenuPrincipal;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.GroupBox gbRecepciones;
        private System.Windows.Forms.ListView lvRecepciones;
        private System.Windows.Forms.ColumnHeader columnEntregadoEnCD;
        private System.Windows.Forms.ColumnHeader columnNroGuia;
    }
}