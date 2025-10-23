// ==================================================================
// FALTA CÓDIGO POR REALIZAR
// EL CÓDIGO AQUÍ ES SOLO DEMOSTRATIVO PARA MOSTRAR LA IDEA GENERAL
// ==================================================================

using System;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.Consultas.EstadoGuia;

namespace WindowsFormsApp1
{
    public partial class FormEstadoGuia : Form
    {
        internal FormEstadoGuiaModelo modelo = new FormEstadoGuiaModelo();

        public FormEstadoGuia()
        {
            InitializeComponent();
            InicializarFormulario();
        }

        private void InicializarFormulario()
        {
            // Combo de guías
            cbGuia.DropDownStyle = ComboBoxStyle.DropDownList;
            cbGuia.Items.Clear();
            foreach (var nro in modelo.GuiasDisponibles)
                cbGuia.Items.Add(nro);

            // No seleccionamos nada por defecto
            cbGuia.SelectedIndex = -1;

            // ListView (por si no está en el Designer)
            lvGuias.FullRowSelect = true;
            lvGuias.GridLines = true;
            lvGuias.View = View.Details;

            if (lvGuias.Columns.Count == 0)
            {
                lvGuias.Columns.Add("N° Guía", 100);
                lvGuias.Columns.Add("Dimensión", 90);
                lvGuias.Columns.Add("CD Destino", 160);
                lvGuias.Columns.Add("Tipo de Envío", 140);
                lvGuias.Columns.Add("DNI Destinatario", 130);
                lvGuias.Columns.Add("Fecha de Ingreso", 130);
                lvGuias.Columns.Add("Estado", 140);
            }

            btnSeleccionar.Click += BtnSeleccionar_Click;
        }

        private void BtnSeleccionar_Click(object sender, EventArgs e)
        {
            if (cbGuia.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar una guía antes de continuar.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nroGuia = cbGuia.SelectedItem.ToString();
            var guia = modelo.ConsultarPorNumero(nroGuia);

            if (guia == null)
            {
                MessageBox.Show("No se encontró información para la guía seleccionada.",
                    "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            lvGuias.Items.Clear();

            var item = new ListViewItem(guia.NumeroGuia);
            item.SubItems.Add(guia.Dimension);
            item.SubItems.Add(guia.CDDestino);
            item.SubItems.Add(guia.TipoEnvio);
            item.SubItems.Add(guia.DniDestinatario);
            item.SubItems.Add(guia.FechaIngreso);
            item.SubItems.Add(guia.Estado);

            lvGuias.Items.Add(item);
        }

        private void btnVolverMenuPrincipal_Click(object sender, EventArgs e)
        {
            FormUtils.VolverAlMenu(this);
        }
    }
}