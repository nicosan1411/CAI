namespace CAI_Proyecto.Forms.Operacion.EntregarEncomiendaClienteCD.Forms
{
    partial class EntregarEncomiendaAClienteCDForm
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
            this.components = new System.ComponentModel.Container();
            this.gbComprobante = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lvGuias = new System.Windows.Forms.ListView();
            this.columnGuiaNumero = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDNI = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnEntregar = new System.Windows.Forms.Button();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.lblDni = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnVolverMenuPrincipal = new System.Windows.Forms.Button();
            this.gbComprobante.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbComprobante
            // 
            this.gbComprobante.Controls.Add(this.btnBuscar);
            this.gbComprobante.Controls.Add(this.lvGuias);
            this.gbComprobante.Controls.Add(this.btnEntregar);
            this.gbComprobante.Controls.Add(this.txtDNI);
            this.gbComprobante.Controls.Add(this.lblDni);
            this.gbComprobante.Location = new System.Drawing.Point(11, 11);
            this.gbComprobante.Margin = new System.Windows.Forms.Padding(2);
            this.gbComprobante.Name = "gbComprobante";
            this.gbComprobante.Padding = new System.Windows.Forms.Padding(2);
            this.gbComprobante.Size = new System.Drawing.Size(633, 226);
            this.gbComprobante.TabIndex = 0;
            this.gbComprobante.TabStop = false;
            this.gbComprobante.Text = "Comprobante de entrega";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(211, 23);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "&Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // lvGuias
            // 
            this.lvGuias.BackColor = System.Drawing.Color.Gainsboro;
            this.lvGuias.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnGuiaNumero,
            this.columnDNI});
            this.lvGuias.FullRowSelect = true;
            this.lvGuias.GridLines = true;
            this.lvGuias.HideSelection = false;
            this.lvGuias.Location = new System.Drawing.Point(22, 57);
            this.lvGuias.Margin = new System.Windows.Forms.Padding(2);
            this.lvGuias.MultiSelect = false;
            this.lvGuias.Name = "lvGuias";
            this.lvGuias.Size = new System.Drawing.Size(584, 114);
            this.lvGuias.TabIndex = 3;
            this.toolTip1.SetToolTip(this.lvGuias, "Lista de guías asociadas al comprobante. Seleccione una si corresponde.");
            this.lvGuias.UseCompatibleStateImageBehavior = false;
            this.lvGuias.View = System.Windows.Forms.View.Details;
            // 
            // columnGuiaNumero
            // 
            this.columnGuiaNumero.Text = "N° de guía";
            this.columnGuiaNumero.Width = 300;
            // 
            // columnDNI
            // 
            this.columnDNI.Text = "DNI Destinatario";
            this.columnDNI.Width = 280;
            // 
            // btnEntregar
            // 
            this.btnEntregar.Location = new System.Drawing.Point(501, 185);
            this.btnEntregar.Margin = new System.Windows.Forms.Padding(2);
            this.btnEntregar.Name = "btnEntregar";
            this.btnEntregar.Size = new System.Drawing.Size(105, 27);
            this.btnEntregar.TabIndex = 4;
            this.btnEntregar.Text = "&Entregar";
            this.toolTip1.SetToolTip(this.btnEntregar, "Registra la entrega para el comprobante. (Enter)");
            this.btnEntregar.UseVisualStyleBackColor = true;
            // 
            // txtDNI
            // 
            this.txtDNI.Location = new System.Drawing.Point(60, 26);
            this.txtDNI.Margin = new System.Windows.Forms.Padding(2);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Size = new System.Drawing.Size(136, 20);
            this.txtDNI.TabIndex = 1;
            this.txtDNI.MaxLength = 8;
            this.toolTip1.SetToolTip(this.txtDNI, "Ingrese el DNI del destinatario y presione Enter o Entregar.");
            // 
            // lblDni
            // 
            this.lblDni.AutoSize = true;
            this.lblDni.Location = new System.Drawing.Point(22, 28);
            this.lblDni.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDni.Name = "lblDni";
            this.lblDni.Size = new System.Drawing.Size(26, 13);
            this.lblDni.TabIndex = 0;
            this.lblDni.Text = "DNI";
            // 
            // btnVolverMenuPrincipal
            // 
            this.btnVolverMenuPrincipal.Location = new System.Drawing.Point(499, 252);
            this.btnVolverMenuPrincipal.Margin = new System.Windows.Forms.Padding(2);
            this.btnVolverMenuPrincipal.Name = "btnVolverMenuPrincipal";
            this.btnVolverMenuPrincipal.Size = new System.Drawing.Size(145, 27);
            this.btnVolverMenuPrincipal.TabIndex = 5;
            this.btnVolverMenuPrincipal.Text = "Volver al &Menú Principal";
            this.btnVolverMenuPrincipal.UseVisualStyleBackColor = true;
            this.btnVolverMenuPrincipal.Click += new System.EventHandler(this.btnVolverMenuPrincipal_Click);
            // 
            // EntregarEncomiendaAClienteCDForm
            // 
            this.AcceptButton = this.btnEntregar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 290);
            this.Controls.Add(this.btnVolverMenuPrincipal);
            this.Controls.Add(this.gbComprobante);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(677, 329);
            this.MinimumSize = new System.Drawing.Size(677, 329);
            this.Name = "EntregarEncomiendaAClienteCDForm";
            this.Text = "Entregar Encomienda a Cliente - Centro De Distribución";
            this.gbComprobante.ResumeLayout(false);
            this.gbComprobante.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbComprobante;
        private System.Windows.Forms.Label lblDni;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Button btnEntregar;
        private System.Windows.Forms.ListView lvGuias;
        private System.Windows.Forms.ColumnHeader columnGuiaNumero;
        private System.Windows.Forms.ColumnHeader columnDNI;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnVolverMenuPrincipal;
        private System.Windows.Forms.Button btnBuscar;
    }
}