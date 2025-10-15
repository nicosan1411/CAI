// ==================================================================
// FALTA CÓDIGO POR REALIZAR
// EL CÓDIGO AQUÍ ES SOLO DEMOSTRATIVO PARA MOSTRAR LA IDEA GENERAL
// ==================================================================

using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormEstadoGuia : Form
    {
        public FormEstadoGuia()
        {
            InitializeComponent();
            InicializarFormulario();
        }

        private void InicializarFormulario()
        {
            cbGuia.DropDownStyle = ComboBoxStyle.DropDownList;
            cbGuia.Items.Clear();

            // Cargar las guías en el combo
            foreach (var g in ComboData.GuiasDemo)
                cbGuia.Items.Add(g.Numero);

            if (cbGuia.Items.Count > 0)
                cbGuia.SelectedIndex = 0;

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
            var guia = ComboData.GuiasDemo.FirstOrDefault(g => g.Numero == nroGuia);

            if (guia == null)
            {
                MessageBox.Show("La consulta no logró ser ejecutada correctamente.\nPor favor intente de nuevo.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Mostrar los datos en el ListView
            var item = new ListViewItem(guia.Numero);
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
