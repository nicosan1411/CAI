using CAI_Proyecto.Forms.Inicio;
using CAI_Proyecto.Forms.Operacion.EntregarEncomiendaClienteCD.Model;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CAI_Proyecto.Forms.Operacion.EntregarEncomiendaClienteCD.Forms
{
    public partial class EntregarEncomiendaAClienteCDForm : Form
    {
        internal EntregarEncomiendaClienteCDModel modelo = new EntregarEncomiendaClienteCDModel();

        public EntregarEncomiendaAClienteCDForm()
        {
            InitializeComponent();
            InicializarFormulario();
        }

        private void InicializarFormulario()
        {
            lvGuias.FullRowSelect = true;
            lvGuias.GridLines = true;
            lvGuias.View = View.Details;
            lvGuias.MultiSelect = false;

            if (lvGuias.Columns.Count == 0)
            {
                // Se asume que columnGuiaNumero y columnDNI existen en el diseñador;
                // si no, se agregan aquí para garantizar funcionamiento.
                lvGuias.Columns.Add("N° Guía", 140);      // columnGuiaNumero
                lvGuias.Columns.Add("DNI", 120);          // columnDNI
            }

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

            if (!EsDniValido(dni, out string error))
            {
                MessageBox.Show(error, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDNI.Focus();
                return;
            }

            if (!modelo.BuscarPorDni(dni))
                return;

            lvGuias.Items.Clear();

            foreach (var g in modelo.Guias)  // solo resultados filtrados
            {
                var item = new ListViewItem(g.NumeroGuia);
                item.SubItems.Add(g.DniDestinatario);
                lvGuias.Items.Add(item);
            }
        }

        private void BtnEntregar_Click(object sender, EventArgs e)
        {
            if (modelo.EntregarTodas())
            {
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