// ==================================================================
// FALTA CÓDIGO POR REALIZAR
// EL CÓDIGO AQUÍ ES SOLO DEMOSTRATIVO PARA MOSTRAR LA IDEA GENERAL
// ==================================================================

using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1.EntregaEnCD
{
    public partial class FormEntregarEncomiendaAClienteCD : Form
    {
        public FormEntregarEncomiendaAClienteCD()
        {
            InitializeComponent();
            InicializarFormulario();
        }

        private void InicializarFormulario()
        {
            btnBuscar.Click += BtnBuscar_Click;
            btnEntregar.Click += BtnEntregar_Click;
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            string dni = txtDNI.Text.Trim();

            // Validar DNI reutilizando la clase Validaciones
            if (!Validaciones.EsDniValido(dni, out string error))
            {
                MessageBox.Show(error, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Buscar las guías asociadas a ese DNI
            var guias = ComboData.EntregasDemo
                .Where(g => g.DNI == dni)
                .ToList();

            lvGuias.Items.Clear();

            if (guias.Count == 0)
            {
                MessageBox.Show("No se encontraron guías asociadas al DNI ingresado.",
                    "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var g in guias)
            {
                var item = new ListViewItem(g.NroGuia);
                item.SubItems.Add(g.Destinatario);
                item.SubItems.Add(g.DNI);
                lvGuias.Items.Add(item);
            }
        }

        private void BtnEntregar_Click(object sender, EventArgs e)
        {
            if (lvGuias.Items.Count == 0)
            {
                MessageBox.Show("No hay guías seleccionadas para entregar.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("¿Estás seguro que querés realizar esta acción?",
                "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                MessageBox.Show("¡Su pedido fue entregado!", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                lvGuias.Items.Clear();
                txtDNI.Clear();
            }
        }

        private void btnVolverMenuPrincipal_Click(object sender, EventArgs e)
        {
            FormUtils.VolverAlMenu(this);
        }
    }
}
