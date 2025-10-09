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
            this.btnForm4 = new System.Windows.Forms.Button();
            this.btnEmitirFactura = new System.Windows.Forms.Button();
            this.btnImposicionCallCenter = new System.Windows.Forms.Button();
            this.btnRendicionPrimeraMilla = new System.Windows.Forms.Button();
            this.btnAdmisionCD = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.grpConsultas.SuspendLayout();
            this.grpOperacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpConsultas
            // 
            this.grpConsultas.Controls.Add(this.btnCuentaCorriente);
            this.grpConsultas.Controls.Add(this.btnResultadosCostosVentas);
            this.grpConsultas.Controls.Add(this.btnEstadoGuia);
            this.grpConsultas.Location = new System.Drawing.Point(21, 353);
            this.grpConsultas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpConsultas.Name = "grpConsultas";
            this.grpConsultas.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpConsultas.Size = new System.Drawing.Size(715, 123);
            this.grpConsultas.TabIndex = 12;
            this.grpConsultas.TabStop = false;
            this.grpConsultas.Text = "Consultas";
            // 
            // btnCuentaCorriente
            // 
            this.btnCuentaCorriente.Location = new System.Drawing.Point(252, 30);
            this.btnCuentaCorriente.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCuentaCorriente.Name = "btnCuentaCorriente";
            this.btnCuentaCorriente.Size = new System.Drawing.Size(200, 79);
            this.btnCuentaCorriente.TabIndex = 6;
            this.btnCuentaCorriente.Text = "Estado de Cuenta Corriente";
            this.btnCuentaCorriente.UseVisualStyleBackColor = true;
            this.btnCuentaCorriente.Click += new System.EventHandler(this.btnCuentaCorriente_Click);
            // 
            // btnResultadosCostosVentas
            // 
            this.btnResultadosCostosVentas.Location = new System.Drawing.Point(491, 30);
            this.btnResultadosCostosVentas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnResultadosCostosVentas.Name = "btnResultadosCostosVentas";
            this.btnResultadosCostosVentas.Size = new System.Drawing.Size(200, 79);
            this.btnResultadosCostosVentas.TabIndex = 7;
            this.btnResultadosCostosVentas.Text = "Resultados Costos-Ventas";
            this.btnResultadosCostosVentas.UseVisualStyleBackColor = true;
            this.btnResultadosCostosVentas.Click += new System.EventHandler(this.btnResultadosCostosVentas_Click);
            // 
            // btnEstadoGuia
            // 
            this.btnEstadoGuia.Location = new System.Drawing.Point(16, 30);
            this.btnEstadoGuia.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEstadoGuia.Name = "btnEstadoGuia";
            this.btnEstadoGuia.Size = new System.Drawing.Size(200, 79);
            this.btnEstadoGuia.TabIndex = 5;
            this.btnEstadoGuia.Text = "Estado de Guía";
            this.btnEstadoGuia.UseVisualStyleBackColor = true;
            this.btnEstadoGuia.Click += new System.EventHandler(this.btnEstadoGuia_Click);
            // 
            // grpOperacion
            // 
            this.grpOperacion.Controls.Add(this.button1);
            this.grpOperacion.Controls.Add(this.btnForm4);
            this.grpOperacion.Controls.Add(this.btnEmitirFactura);
            this.grpOperacion.Controls.Add(this.btnImposicionCallCenter);
            this.grpOperacion.Controls.Add(this.btnRendicionPrimeraMilla);
            this.grpOperacion.Controls.Add(this.btnAdmisionCD);
            this.grpOperacion.Location = new System.Drawing.Point(140, 15);
            this.grpOperacion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpOperacion.Name = "grpOperacion";
            this.grpOperacion.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpOperacion.Size = new System.Drawing.Size(471, 319);
            this.grpOperacion.TabIndex = 13;
            this.grpOperacion.TabStop = false;
            this.grpOperacion.Text = "Operación";
            // 
            // btnForm4
            // 
            this.btnForm4.Location = new System.Drawing.Point(252, 121);
            this.btnForm4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnForm4.Name = "btnForm4";
            this.btnForm4.Size = new System.Drawing.Size(200, 79);
            this.btnForm4.TabIndex = 3;
            this.btnForm4.Text = "Entregas y Recepciones Micros";
            this.btnForm4.UseVisualStyleBackColor = true;
            this.btnForm4.Click += new System.EventHandler(this.btnForm4_Click);
            // 
            // btnEmitirFactura
            // 
            this.btnEmitirFactura.Location = new System.Drawing.Point(252, 218);
            this.btnEmitirFactura.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEmitirFactura.Name = "btnEmitirFactura";
            this.btnEmitirFactura.Size = new System.Drawing.Size(200, 79);
            this.btnEmitirFactura.TabIndex = 4;
            this.btnEmitirFactura.Text = "Emitir Factura";
            this.btnEmitirFactura.UseVisualStyleBackColor = true;
            this.btnEmitirFactura.Click += new System.EventHandler(this.btnEmitirFactura_Click);
            // 
            // btnImposicionCallCenter
            // 
            this.btnImposicionCallCenter.Location = new System.Drawing.Point(16, 23);
            this.btnImposicionCallCenter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnImposicionCallCenter.Name = "btnImposicionCallCenter";
            this.btnImposicionCallCenter.Size = new System.Drawing.Size(200, 79);
            this.btnImposicionCallCenter.TabIndex = 0;
            this.btnImposicionCallCenter.Text = "Imposición Call Center";
            this.btnImposicionCallCenter.UseVisualStyleBackColor = true;
            this.btnImposicionCallCenter.Click += new System.EventHandler(this.btnImposicionCallCenter_Click);
            // 
            // btnRendicionPrimeraMilla
            // 
            this.btnRendicionPrimeraMilla.Location = new System.Drawing.Point(16, 121);
            this.btnRendicionPrimeraMilla.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRendicionPrimeraMilla.Name = "btnRendicionPrimeraMilla";
            this.btnRendicionPrimeraMilla.Size = new System.Drawing.Size(200, 79);
            this.btnRendicionPrimeraMilla.TabIndex = 2;
            this.btnRendicionPrimeraMilla.Text = "Rendir y Asignar Fletes";
            this.btnRendicionPrimeraMilla.UseVisualStyleBackColor = true;
            this.btnRendicionPrimeraMilla.Click += new System.EventHandler(this.btnRendicionPrimeraMilla_Click);
            // 
            // btnAdmisionCD
            // 
            this.btnAdmisionCD.Location = new System.Drawing.Point(252, 23);
            this.btnAdmisionCD.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAdmisionCD.Name = "btnAdmisionCD";
            this.btnAdmisionCD.Size = new System.Drawing.Size(200, 79);
            this.btnAdmisionCD.TabIndex = 1;
            this.btnAdmisionCD.Text = "Imposición y Admisión de Encomiendas en CD";
            this.btnAdmisionCD.UseVisualStyleBackColor = true;
            this.btnAdmisionCD.Click += new System.EventHandler(this.btnAdmisionCD_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 218);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 79);
            this.button1.TabIndex = 5;
            this.button1.Text = "Entregas en CD";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // FormInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 494);
            this.Controls.Add(this.grpConsultas);
            this.Controls.Add(this.grpOperacion);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
        private System.Windows.Forms.Button button1;
    }
}