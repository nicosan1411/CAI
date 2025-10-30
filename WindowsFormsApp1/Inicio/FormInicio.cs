using System;
using System.Windows.Forms;
using WindowsFormsApp1.Consultas.CuentaCorriente.Model;
using WindowsFormsApp1.Consultas.EstadoGuia.Model;
using WindowsFormsApp1.Consultas.ResultadosCostosVentas.Model;
using WindowsFormsApp1.Operación.AdmitirEnCD.Model;
using WindowsFormsApp1.Operación.EmitirFactura.Model;
using WindowsFormsApp1.Operacion.EntregarEncomiendaClienteCD.Model;
using WindowsFormsApp1.Operación.ImponerCallCenter.Model;
using WindowsFormsApp1.Operación.RecepcionarMicros.Model;
using WindowsFormsApp1.Operación.RendirEncomienda.Model;

namespace WindowsFormsApp1.Inicio
{
    public partial class FormInicio : Form
    {
        public FormInicio()
        {
            InitializeComponent();
        }

        private void OpenAsDialog(Form form)
        {
            form.StartPosition = FormStartPosition.CenterParent;

            this.Hide();

            try
            {
                form.ShowDialog(this);
            }
            finally
            {
                this.Show();
                this.Activate();
            }
        }

        public static void VolverAlMenu(Form formularioActual)
        {
            var result = MessageBox.Show(
                "¿Deseás volver al menú principal?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (result == DialogResult.Yes)
                formularioActual.Close();
        }

        private void btnImposicionCallCenter_Click(object sender, EventArgs e)
        {
            OpenAsDialog(new FormImponerCallCenter());
        }

        private void btnAdmisionCD_Click(object sender, EventArgs e)
        {
            OpenAsDialog(new FormAdmitirEnCD());
        }

        private void btnRendicionPrimeraMilla_Click(object sender, EventArgs e)
        {
            OpenAsDialog(new FormRendirEncomiendas());
        }

        private void btnForm4_Click(object sender, EventArgs e)
        {
            OpenAsDialog(new FormRecepcionarMicros());
        }

        private void btnEmitirFactura_Click(object sender, EventArgs e)
        {
            OpenAsDialog(new FormEmitirFactura());
        }

        private void btnEstadoGuia_Click(object sender, EventArgs e)
        {
            OpenAsDialog(new FormEstadoGuia());
        }

        private void btnResultadosCostosVentas_Click(object sender, EventArgs e)
        {
            OpenAsDialog(new FormResultadosCostosVentas());
        }

        private void btnCuentaCorriente_Click(object sender, EventArgs e)
        {
            OpenAsDialog(new FormCuentaCorriente());
        }

        private void btnEntregasEnCD_Click(object sender, EventArgs e)
        {
            OpenAsDialog(new FormEntregarEncomiendaAClienteCD());
        }

        public static void ConfigureListView(ListView lv, bool withCheckboxes)
        {
            lv.View = View.Details;
            lv.CheckBoxes = withCheckboxes;
            lv.FullRowSelect = true;
            lv.MultiSelect = false;
            lv.Items.Clear();
        }
    }
}