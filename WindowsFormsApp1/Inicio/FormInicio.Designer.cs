namespace WindowsFormsApp1
{
    partial class FormInicio
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
            this.grpConsultas = new System.Windows.Forms.GroupBox();
            this.btnCuentaCorriente = new System.Windows.Forms.Button();
            this.btnResultadosCostosVentas = new System.Windows.Forms.Button();
            this.btnEstadoGuia = new System.Windows.Forms.Button();
            this.grpOperacion = new System.Windows.Forms.GroupBox();
            this.btnEntregasEnCD = new System.Windows.Forms.Button();
            this.btnForm4 = new System.Windows.Forms.Button();
            this.btnEmitirFactura = new System.Windows.Forms.Button();
            this.btnImposicionCallCenter = new System.Windows.Forms.Button();
            this.btnRendicionPrimeraMilla = new System.Windows.Forms.Button();
            this.btnAdmisionCD = new System.Windows.Forms.Button();
            this.grpConsultas.SuspendLayout();
            this.grpOperacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpConsultas
            // 
            this.grpConsultas.Controls.Add(this.btnCuentaCorriente);
            this.grpConsultas.Controls.Add(this.btnResultadosCostosVentas);
            this.grpConsultas.Controls.Add(this.btnEstadoGuia);
            this.grpConsultas.Location = new System.Drawing.Point(16, 287);
            this.grpConsultas.Name = "grpConsultas";
            this.grpConsultas.Size = new System.Drawing.Size(536, 100);
            this.grpConsultas.TabIndex = 6;
            this.grpConsultas.TabStop = false;
            this.grpConsultas.Text = "Consultas";
            // 
            // btnCuentaCorriente
            // 
            this.btnCuentaCorriente.Location = new System.Drawing.Point(189, 24);
            this.btnCuentaCorriente.Name = "btnCuentaCorriente";
            this.btnCuentaCorriente.Size = new System.Drawing.Size(150, 64);
            this.btnCuentaCorriente.TabIndex = 8;
            this.btnCuentaCorriente.Text = "Estado de Cuenta Corriente";
            this.btnCuentaCorriente.UseVisualStyleBackColor = true;
            this.btnCuentaCorriente.Click += new System.EventHandler(this.btnCuentaCorriente_Click);
            // 
            // btnResultadosCostosVentas
            // 
            this.btnResultadosCostosVentas.Location = new System.Drawing.Point(368, 24);
            this.btnResultadosCostosVentas.Name = "btnResultadosCostosVentas";
            this.btnResultadosCostosVentas.Size = new System.Drawing.Size(150, 64);
            this.btnResultadosCostosVentas.TabIndex = 9;
            this.btnResultadosCostosVentas.Text = "Resultados Costos-Ventas";
            this.btnResultadosCostosVentas.UseVisualStyleBackColor = true;
            this.btnResultadosCostosVentas.Click += new System.EventHandler(this.btnResultadosCostosVentas_Click);
            // 
            // btnEstadoGuia
            // 
            this.btnEstadoGuia.Location = new System.Drawing.Point(12, 24);
            this.btnEstadoGuia.Name = "btnEstadoGuia";
            this.btnEstadoGuia.Size = new System.Drawing.Size(150, 64);
            this.btnEstadoGuia.TabIndex = 7;
            this.btnEstadoGuia.Text = "Estado de Guía";
            this.btnEstadoGuia.UseVisualStyleBackColor = true;
            this.btnEstadoGuia.Click += new System.EventHandler(this.btnEstadoGuia_Click);
            // 
            // grpOperacion
            // 
            this.grpOperacion.Controls.Add(this.btnEntregasEnCD);
            this.grpOperacion.Controls.Add(this.btnForm4);
            this.grpOperacion.Controls.Add(this.btnEmitirFactura);
            this.grpOperacion.Controls.Add(this.btnImposicionCallCenter);
            this.grpOperacion.Controls.Add(this.btnRendicionPrimeraMilla);
            this.grpOperacion.Controls.Add(this.btnAdmisionCD);
            this.grpOperacion.Location = new System.Drawing.Point(105, 12);
            this.grpOperacion.Name = "grpOperacion";
            this.grpOperacion.Size = new System.Drawing.Size(353, 259);
            this.grpOperacion.TabIndex = 0;
            this.grpOperacion.TabStop = false;
            this.grpOperacion.Text = "Operación";
            // 
            // btnEntregasEnCD
            // 
            this.btnEntregasEnCD.Location = new System.Drawing.Point(12, 177);
            this.btnEntregasEnCD.Name = "btnEntregasEnCD";
            this.btnEntregasEnCD.Size = new System.Drawing.Size(150, 64);
            this.btnEntregasEnCD.TabIndex = 5;
            this.btnEntregasEnCD.Text = "Entrega de Encomiendas en Centro de Distribución";
            this.btnEntregasEnCD.UseVisualStyleBackColor = true;
            this.btnEntregasEnCD.Click += new System.EventHandler(this.btnEntregasEnCD_Click);
            // 
            // btnForm4
            // 
            this.btnForm4.Location = new System.Drawing.Point(189, 98);
            this.btnForm4.Name = "btnForm4";
            this.btnForm4.Size = new System.Drawing.Size(150, 64);
            this.btnForm4.TabIndex = 4;
            this.btnForm4.Text = "Entrega de Encomiendas y Recepción de Micros";
            this.btnForm4.UseVisualStyleBackColor = true;
            this.btnForm4.Click += new System.EventHandler(this.btnForm4_Click);
            // 
            // btnEmitirFactura
            // 
            this.btnEmitirFactura.Location = new System.Drawing.Point(189, 177);
            this.btnEmitirFactura.Name = "btnEmitirFactura";
            this.btnEmitirFactura.Size = new System.Drawing.Size(150, 64);
            this.btnEmitirFactura.TabIndex = 5;
            this.btnEmitirFactura.Text = "Emitir Factura";
            this.btnEmitirFactura.UseVisualStyleBackColor = true;
            this.btnEmitirFactura.Click += new System.EventHandler(this.btnEmitirFactura_Click);
            // 
            // btnImposicionCallCenter
            // 
            this.btnImposicionCallCenter.Location = new System.Drawing.Point(12, 19);
            this.btnImposicionCallCenter.Name = "btnImposicionCallCenter";
            this.btnImposicionCallCenter.Size = new System.Drawing.Size(150, 64);
            this.btnImposicionCallCenter.TabIndex = 1;
            this.btnImposicionCallCenter.Text = "Imposición Call Center";
            this.btnImposicionCallCenter.UseVisualStyleBackColor = true;
            this.btnImposicionCallCenter.Click += new System.EventHandler(this.btnImposicionCallCenter_Click);
            // 
            // btnRendicionPrimeraMilla
            // 
            this.btnRendicionPrimeraMilla.Location = new System.Drawing.Point(12, 98);
            this.btnRendicionPrimeraMilla.Name = "btnRendicionPrimeraMilla";
            this.btnRendicionPrimeraMilla.Size = new System.Drawing.Size(150, 64);
            this.btnRendicionPrimeraMilla.TabIndex = 3;
            this.btnRendicionPrimeraMilla.Text = "Rendir Encomiendas y Asignar Fletes";
            this.btnRendicionPrimeraMilla.UseVisualStyleBackColor = true;
            this.btnRendicionPrimeraMilla.Click += new System.EventHandler(this.btnRendicionPrimeraMilla_Click);
            // 
            // btnAdmisionCD
            // 
            this.btnAdmisionCD.Location = new System.Drawing.Point(189, 19);
            this.btnAdmisionCD.Name = "btnAdmisionCD";
            this.btnAdmisionCD.Size = new System.Drawing.Size(150, 64);
            this.btnAdmisionCD.TabIndex = 2;
            this.btnAdmisionCD.Text = "Imposición y Admisión de Encomiendas en Centro de Distribución";
            this.btnAdmisionCD.UseVisualStyleBackColor = true;
            this.btnAdmisionCD.Click += new System.EventHandler(this.btnAdmisionCD_Click);
            // 
            // FormInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 401);
            this.Controls.Add(this.grpConsultas);
            this.Controls.Add(this.grpOperacion);
            this.MinimumSize = new System.Drawing.Size(587, 440);
            this.Name = "FormInicio";
            this.Text = "Menú Principal";
            this.grpConsultas.ResumeLayout(false);
            this.grpOperacion.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpConsultas;
        private System.Windows.Forms.Button btnCuentaCorriente;
        private System.Windows.Forms.Button btnResultadosCostosVentas;
        private System.Windows.Forms.Button btnEstadoGuia;
        private System.Windows.Forms.GroupBox grpOperacion;
        private System.Windows.Forms.Button btnForm4;
        private System.Windows.Forms.Button btnEmitirFactura;
        private System.Windows.Forms.Button btnImposicionCallCenter;
        private System.Windows.Forms.Button btnRendicionPrimeraMilla;
        private System.Windows.Forms.Button btnAdmisionCD;
        private System.Windows.Forms.Button btnEntregasEnCD;
    }
}