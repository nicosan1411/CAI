using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            OpenAsDialog(new FormImposicionCallCenter());
        }

        private void btnAdmisionCD_Click(object sender, EventArgs e)
        {
            OpenAsDialog(new FormAdmisionCD());
        }

        private void btnRendicionPrimeraMilla_Click(object sender, EventArgs e)
        {
            OpenAsDialog(new FormRendicionPrimeraMilla());
        }

        private void btnForm4_Click(object sender, EventArgs e)
        {
            OpenAsDialog(new Form4());
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
    }
}