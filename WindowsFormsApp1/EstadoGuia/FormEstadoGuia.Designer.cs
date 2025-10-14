namespace WindowsFormsApp1
{
    partial class FormEstadoGuia
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
            this.lblGuia = new System.Windows.Forms.Label();
            this.cbGuia = new System.Windows.Forms.ComboBox();
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.gbGuiasEntregadas = new System.Windows.Forms.GroupBox();
            this.lvGuias = new System.Windows.Forms.ListView();
            this.columnNroGuia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDimensión = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnCDDestino = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTipoEnvio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDniDestinatario = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnFechaIngreso = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnEstado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnVolverMenuPrincipal = new System.Windows.Forms.Button();
            this.gbGuiasEntregadas.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblGuia
            // 
            this.lblGuia.AutoSize = true;
            this.lblGuia.Location = new System.Drawing.Point(23, 15);
            this.lblGuia.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGuia.Name = "lblGuia";
            this.lblGuia.Size = new System.Drawing.Size(34, 13);
            this.lblGuia.TabIndex = 0;
            this.lblGuia.Text = "Guía:";
            // 
            // cbGuia
            // 
            this.cbGuia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbGuia.FormattingEnabled = true;
            this.cbGuia.Location = new System.Drawing.Point(61, 12);
            this.cbGuia.Margin = new System.Windows.Forms.Padding(2);
            this.cbGuia.Name = "cbGuia";
            this.cbGuia.Size = new System.Drawing.Size(680, 21);
            this.cbGuia.TabIndex = 1;
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSeleccionar.Location = new System.Drawing.Point(745, 12);
            this.btnSeleccionar.Margin = new System.Windows.Forms.Padding(2);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(82, 21);
            this.btnSeleccionar.TabIndex = 2;
            this.btnSeleccionar.Text = "&Seleccionar";
            this.btnSeleccionar.UseVisualStyleBackColor = true;
            // 
            // gbGuiasEntregadas
            // 
            this.gbGuiasEntregadas.Controls.Add(this.lvGuias);
            this.gbGuiasEntregadas.Location = new System.Drawing.Point(17, 43);
            this.gbGuiasEntregadas.Margin = new System.Windows.Forms.Padding(2);
            this.gbGuiasEntregadas.Name = "gbGuiasEntregadas";
            this.gbGuiasEntregadas.Padding = new System.Windows.Forms.Padding(2);
            this.gbGuiasEntregadas.Size = new System.Drawing.Size(810, 157);
            this.gbGuiasEntregadas.TabIndex = 3;
            this.gbGuiasEntregadas.TabStop = false;
            this.gbGuiasEntregadas.Text = "Guías entregadas";
            // 
            // lvGuias
            // 
            this.lvGuias.BackColor = System.Drawing.Color.Gainsboro;
            this.lvGuias.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnNroGuia,
            this.columnDimensión,
            this.columnCDDestino,
            this.columnTipoEnvio,
            this.columnDniDestinatario,
            this.columnFechaIngreso,
            this.columnEstado});
            this.lvGuias.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lvGuias.FullRowSelect = true;
            this.lvGuias.GridLines = true;
            this.lvGuias.HideSelection = false;
            this.lvGuias.Location = new System.Drawing.Point(18, 23);
            this.lvGuias.Margin = new System.Windows.Forms.Padding(2);
            this.lvGuias.MultiSelect = false;
            this.lvGuias.Name = "lvGuias";
            this.lvGuias.Size = new System.Drawing.Size(775, 119);
            this.lvGuias.TabIndex = 4;
            this.lvGuias.UseCompatibleStateImageBehavior = false;
            this.lvGuias.View = System.Windows.Forms.View.Details;
            // 
            // columnNroGuia
            // 
            this.columnNroGuia.Text = "N° Guía";
            this.columnNroGuia.Width = 100;
            // 
            // columnDimensión
            // 
            this.columnDimensión.Text = "Dimensión";
            this.columnDimensión.Width = 80;
            // 
            // columnCDDestino
            // 
            this.columnCDDestino.Text = "CD Destino";
            this.columnCDDestino.Width = 210;
            // 
            // columnTipoEnvio
            // 
            this.columnTipoEnvio.Text = "Tipo de Envío";
            this.columnTipoEnvio.Width = 100;
            // 
            // columnDniDestinatario
            // 
            this.columnDniDestinatario.Text = "DNI Destinatario";
            this.columnDniDestinatario.Width = 90;
            // 
            // columnFechaIngreso
            // 
            this.columnFechaIngreso.Text = "Fecha de Ingreso";
            this.columnFechaIngreso.Width = 100;
            // 
            // columnEstado
            // 
            this.columnEstado.Text = "Estado";
            this.columnEstado.Width = 90;
            // 
            // btnVolverMenuPrincipal
            // 
            this.btnVolverMenuPrincipal.Location = new System.Drawing.Point(682, 207);
            this.btnVolverMenuPrincipal.Margin = new System.Windows.Forms.Padding(2);
            this.btnVolverMenuPrincipal.Name = "btnVolverMenuPrincipal";
            this.btnVolverMenuPrincipal.Size = new System.Drawing.Size(145, 27);
            this.btnVolverMenuPrincipal.TabIndex = 5;
            this.btnVolverMenuPrincipal.Text = "Volver al &Menú Principal";
            this.btnVolverMenuPrincipal.UseVisualStyleBackColor = true;
            this.btnVolverMenuPrincipal.Click += new System.EventHandler(this.btnVolverMenuPrincipal_Click);
            // 
            // FormEstadoGuia
            // 
            this.AcceptButton = this.btnSeleccionar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 245);
            this.Controls.Add(this.btnVolverMenuPrincipal);
            this.Controls.Add(this.gbGuiasEntregadas);
            this.Controls.Add(this.btnSeleccionar);
            this.Controls.Add(this.cbGuia);
            this.Controls.Add(this.lblGuia);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormEstadoGuia";
            this.Text = "Consulta Estado Guia";
            this.gbGuiasEntregadas.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGuia;
        private System.Windows.Forms.ComboBox cbGuia;
        private System.Windows.Forms.Button btnSeleccionar;
        private System.Windows.Forms.GroupBox gbGuiasEntregadas;
        private System.Windows.Forms.Button btnVolverMenuPrincipal;
        private System.Windows.Forms.ListView lvGuias;
        private System.Windows.Forms.ColumnHeader columnNroGuia;
        private System.Windows.Forms.ColumnHeader columnDimensión;
        private System.Windows.Forms.ColumnHeader columnCDDestino;
        private System.Windows.Forms.ColumnHeader columnTipoEnvio;
        private System.Windows.Forms.ColumnHeader columnDniDestinatario;
        private System.Windows.Forms.ColumnHeader columnFechaIngreso;
        private System.Windows.Forms.ColumnHeader columnEstado;
    }
}