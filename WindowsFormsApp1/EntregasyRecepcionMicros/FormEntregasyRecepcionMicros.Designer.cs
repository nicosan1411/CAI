namespace WindowsFormsApp1
{
    partial class FormEntregasyRecepcionMicros
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
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "test"}, -1);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "test"}, -1);
            this.gbRecepciones = new System.Windows.Forms.GroupBox();
            this.gbDespachos = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbPatenteMicro = new System.Windows.Forms.ComboBox();
            this.lblPatenteMicro = new System.Windows.Forms.Label();
            this.btnVolverMenuPrincipal = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnEntregadoEnCD = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnNroGuia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnGuia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbDespachos.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbRecepciones
            // 
            this.gbRecepciones.Location = new System.Drawing.Point(19, 42);
            this.gbRecepciones.Margin = new System.Windows.Forms.Padding(2);
            this.gbRecepciones.Name = "gbRecepciones";
            this.gbRecepciones.Padding = new System.Windows.Forms.Padding(2);
            this.gbRecepciones.Size = new System.Drawing.Size(343, 167);
            this.gbRecepciones.TabIndex = 2;
            this.gbRecepciones.TabStop = false;
            this.gbRecepciones.Text = "Recepciones";
            // 
            // gbDespachos
            // 
            this.gbDespachos.Controls.Add(this.listView1);
            this.gbDespachos.Location = new System.Drawing.Point(378, 42);
            this.gbDespachos.Margin = new System.Windows.Forms.Padding(2);
            this.gbDespachos.Name = "gbDespachos";
            this.gbDespachos.Padding = new System.Windows.Forms.Padding(2);
            this.gbDespachos.Size = new System.Drawing.Size(343, 167);
            this.gbDespachos.TabIndex = 4;
            this.gbDespachos.TabStop = false;
            this.gbDespachos.Text = "Despachos";
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
            // cbPatenteMicro
            // 
            this.cbPatenteMicro.FormattingEnabled = true;
            this.cbPatenteMicro.Location = new System.Drawing.Point(103, 12);
            this.cbPatenteMicro.Margin = new System.Windows.Forms.Padding(2);
            this.cbPatenteMicro.Name = "cbPatenteMicro";
            this.cbPatenteMicro.Size = new System.Drawing.Size(259, 21);
            this.cbPatenteMicro.TabIndex = 1;
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
            this.btnVolverMenuPrincipal.Location = new System.Drawing.Point(576, 222);
            this.btnVolverMenuPrincipal.Margin = new System.Windows.Forms.Padding(2);
            this.btnVolverMenuPrincipal.Name = "btnVolverMenuPrincipal";
            this.btnVolverMenuPrincipal.Size = new System.Drawing.Size(145, 25);
            this.btnVolverMenuPrincipal.TabIndex = 7;
            this.btnVolverMenuPrincipal.Text = "Volver al Menú Principal";
            this.btnVolverMenuPrincipal.UseVisualStyleBackColor = true;
            this.btnVolverMenuPrincipal.Click += new System.EventHandler(this.btnVolverMenuPrincipal_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(501, 222);
            this.btnAceptar.Margin = new System.Windows.Forms.Padding(2);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(71, 25);
            this.btnAceptar.TabIndex = 6;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnVolverMenuPrincipal_Click);
            // 
            // listView2
            // 
            this.listView2.BackColor = System.Drawing.Color.Gainsboro;
            this.listView2.CheckBoxes = true;
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnEntregadoEnCD,
            this.columnNroGuia});
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            this.listView2.HideSelection = false;
            listViewItem2.Checked = true;
            listViewItem2.StateImageIndex = 1;
            this.listView2.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.listView2.Location = new System.Drawing.Point(39, 60);
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(305, 130);
            this.listView2.TabIndex = 3;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnEntregadoEnCD
            // 
            this.columnEntregadoEnCD.DisplayIndex = 1;
            this.columnEntregadoEnCD.Text = "Entregado en CD Destino";
            this.columnEntregadoEnCD.Width = 180;
            // 
            // columnNroGuia
            // 
            this.columnNroGuia.DisplayIndex = 0;
            this.columnNroGuia.Text = "N° Guía";
            this.columnNroGuia.Width = 120;
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.Gainsboro;
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnGuia});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            listViewItem1.Checked = true;
            listViewItem1.StateImageIndex = 1;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listView1.Location = new System.Drawing.Point(20, 18);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(305, 130);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.DisplayIndex = 1;
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 180;
            // 
            // columnGuia
            // 
            this.columnGuia.DisplayIndex = 0;
            this.columnGuia.Text = "N° Guía";
            this.columnGuia.Width = 120;
            // 
            // FormEntregasyRecepcionMicros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 262);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnVolverMenuPrincipal);
            this.Controls.Add(this.cbPatenteMicro);
            this.Controls.Add(this.lblPatenteMicro);
            this.Controls.Add(this.gbDespachos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbRecepciones);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(759, 301);
            this.Name = "FormEntregasyRecepcionMicros";
            this.Text = "Entregas y Recepción Micros";
            this.gbDespachos.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox gbDespachos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPatenteMicro;
        private System.Windows.Forms.Label lblPatenteMicro;
        private System.Windows.Forms.Button btnVolverMenuPrincipal;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.GroupBox gbRecepciones;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnGuia;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnEntregadoEnCD;
        private System.Windows.Forms.ColumnHeader columnNroGuia;
    }
}