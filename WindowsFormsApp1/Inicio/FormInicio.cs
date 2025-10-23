using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.EntregaEnCD;
using WindowsFormsApp1.Operación.ImponerCallCenter.Formulario;

namespace WindowsFormsApp1
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
    }
}