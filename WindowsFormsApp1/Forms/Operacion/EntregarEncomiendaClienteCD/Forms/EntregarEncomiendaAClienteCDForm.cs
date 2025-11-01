using CAI_Proyecto.Forms.Inicio;
using CAI_Proyecto.Forms.Operacion.EntregarEncomiendaClienteCD.Model;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CAI_Proyecto.Forms.Operacion.EntregarEncomiendaClienteCD.Forms
{
    public partial class EntregarEncomiendaAClienteCDForm : Form
    {
        // Modelo del formulario
        internal EntregarEncomiendaClienteCDModel modelo = new EntregarEncomiendaClienteCDModel();

        public EntregarEncomiendaAClienteCDForm()
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
            
        }

        public static bool EsDniValido(string dni, out string error)
        {
            error = null;

            if (string.IsNullOrWhiteSpace(dni))
            {
                error = "Ingresá el DNI del destinatario.";
                return false;
            }

            // Normalizo a dígitos y valido largo exacto
            var limpio = new string(dni.Where(char.IsDigit).ToArray());
            if (limpio.Length != 8)
            {
                error = "El DNI debe tener exactamente 8 dígitos.";
                return false;
            }

            return true;
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            string dni = txtDNI.Text.Trim();

            // Validación de DNI
            if (!EsDniValido(dni, out string error))
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
            // Entrega TODAS las listadas 
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
            InicioForm.VolverAlMenu(this);
        }
    }
}