namespace WindowsFormsApp1
{
    partial class Main
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
            this.btnEstadoCuentaCorriente = new System.Windows.Forms.Button();
            this.btnEstadoGuia = new System.Windows.Forms.Button();
            this.btnResultadoCostosVentas = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.grpConsultas = new System.Windows.Forms.GroupBox();
            this.grpOperacion = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // btnEstadoCuentaCorriente
            // 
            this.btnEstadoCuentaCorriente.Location = new System.Drawing.Point(217, 224);
            this.btnEstadoCuentaCorriente.Name = "btnEstadoCuentaCorriente";
            this.btnEstadoCuentaCorriente.Size = new System.Drawing.Size(150, 64);
            this.btnEstadoCuentaCorriente.TabIndex = 0;
            this.btnEstadoCuentaCorriente.Text = "Estado de Cuenta Corriente";
            this.btnEstadoCuentaCorriente.UseVisualStyleBackColor = true;
            // 
            // btnEstadoGuia
            // 
            this.btnEstadoGuia.Location = new System.Drawing.Point(40, 224);
            this.btnEstadoGuia.Name = "btnEstadoGuia";
            this.btnEstadoGuia.Size = new System.Drawing.Size(150, 64);
            this.btnEstadoGuia.TabIndex = 1;
            this.btnEstadoGuia.Text = "Estado de Guía";
            this.btnEstadoGuia.UseVisualStyleBackColor = true;
            // 
            // btnResultadoCostosVentas
            // 
            this.btnResultadoCostosVentas.Location = new System.Drawing.Point(396, 224);
            this.btnResultadoCostosVentas.Name = "btnResultadoCostosVentas";
            this.btnResultadoCostosVentas.Size = new System.Drawing.Size(150, 64);
            this.btnResultadoCostosVentas.TabIndex = 2;
            this.btnResultadoCostosVentas.Text = "Resultado Costos-Ventas";
            this.btnResultadoCostosVentas.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(109, 76);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(121, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "Emitir Factura";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(26, 120);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(123, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "Imposición Call Center";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(155, 120);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(254, 23);
            this.button6.TabIndex = 5;
            this.button6.Text = "Imposición y Admisión de Encomiendas en CD";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(26, 165);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(191, 23);
            this.button7.TabIndex = 6;
            this.button7.Text = "Rendición y Asignación Primera Milla";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(334, 165);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 7;
            this.button8.Text = "button8";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // grpConsultas
            // 
            this.grpConsultas.Location = new System.Drawing.Point(26, 203);
            this.grpConsultas.Name = "grpConsultas";
            this.grpConsultas.Size = new System.Drawing.Size(536, 100);
            this.grpConsultas.TabIndex = 8;
            this.grpConsultas.TabStop = false;
            this.grpConsultas.Text = "Consultas";
            // 
            // grpOperacion
            // 
            this.grpOperacion.Location = new System.Drawing.Point(374, 14);
            this.grpOperacion.Name = "grpOperacion";
            this.grpOperacion.Size = new System.Drawing.Size(301, 100);
            this.grpOperacion.TabIndex = 9;
            this.grpOperacion.TabStop = false;
            this.grpOperacion.Text = "Operación";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 432);
            this.Controls.Add(this.btnEstadoCuentaCorriente);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnResultadoCostosVentas);
            this.Controls.Add(this.btnEstadoGuia);
            this.Controls.Add(this.grpConsultas);
            this.Controls.Add(this.grpOperacion);
            this.Name = "Main";
            this.Text = "Menú Principal";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEstadoCuentaCorriente;
        private System.Windows.Forms.Button btnEstadoGuia;
        private System.Windows.Forms.Button btnResultadoCostosVentas;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.GroupBox grpConsultas;
        private System.Windows.Forms.GroupBox grpOperacion;
    }
}