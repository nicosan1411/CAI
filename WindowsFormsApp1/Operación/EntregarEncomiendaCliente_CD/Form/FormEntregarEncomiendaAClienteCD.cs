// ==================================================================
// FALTA CÓDIGO POR REALIZAR
// EL CÓDIGO AQUÍ ES SOLO DEMOSTRATIVO PARA MOSTRAR LA IDEA GENERAL
// ==================================================================

using System;
using System.Windows.Forms;
using WindowsFormsApp1.Operacion.EntregarEncomiendaCliente_CD.Model;

namespace WindowsFormsApp1.EntregaEnCD
{
    public partial class FormEntregarEncomiendaAClienteCD : Form
    {
        // Modelo del formulario
        internal FormEntregarEncomiendaCliente_CDModelo modelo = new FormEntregarEncomiendaCliente_CDModelo();

        public FormEntregarEncomiendaAClienteCD()
        {
            InitializeComponent();
            InicializarFormulario();
        }

        private void InicializarFormulario()
        {
            // Configuración del ListView
            lvGuias.FullRowSelect = true;
            lvGuias.GridLines = true;
            lvGuias.View = View.Details;
            lvGuias.MultiSelect = false;

            // Asegura columnas (por si el Designer no las tiene)
            if (lvGuias.Columns.Count == 0)
            {
                lvGuias.Columns.Add("N° Guía", 120);
                lvGuias.Columns.Add("Destinatario", 180);
                lvGuias.Columns.Add("DNI", 120);
                lvGuias.Columns.Add("Estado", 160);
            }

            // Eventos
            btnBuscar.Click += BtnBuscar_Click;
            btnEntregar.Click += BtnEntregar_Click;
            btnVolverMenuPrincipal.Click += btnVolverMenuPrincipal_Click;
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            string dni = txtDNI.Text.Trim();

            // Validación de DNI (si tenés esta clase utilitaria)
            if (!Validaciones.EsDniValido(dni, out string error))
            {
                MessageBox.Show(error, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDNI.Focus();
                return;
            }

            var ok = modelo.BuscarPorDni(dni);
            if (!ok) return;

            lvGuias.Items.Clear();

            if (modelo.Encomiendas.Count == 0)
            {
                MessageBox.Show("No se encontraron encomiendas admitidas en CD destino para el DNI ingresado.",
                    "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var g in modelo.Encomiendas)
            {
                var item = new ListViewItem(g.NroGuia);
                item.SubItems.Add(g.Destinatario);
                item.SubItems.Add(g.DNI);
                item.SubItems.Add(g.Estado);
                lvGuias.Items.Add(item);
            }
        }

        private void BtnEntregar_Click(object sender, EventArgs e)
        {
            // Entrega TODAS las listadas (no requiere selección)
            var ok = modelo.EntregarTodas();
            if (ok)
            {
                // Limpiar la vista según el caso de uso (paso 15)
                lvGuias.Items.Clear();
                txtDNI.Clear();
                txtDNI.Focus();
            }
        }

        private void btnVolverMenuPrincipal_Click(object sender, EventArgs e)
        {
            FormUtils.VolverAlMenu(this);
        }
    }
}