// ==================================================================
// FALTA CÓDIGO POR REALIZAR
// EL CÓDIGO AQUÍ ES SOLO DEMOSTRATIVO PARA MOSTRAR LA IDEA GENERAL
// ==================================================================

using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormEmitirFactura : Form
    {
        public FormEmitirFactura()
        {
            InitializeComponent();
            InicializarFormulario();
        }

        private void InicializarFormulario()
        {
            // Cargar clientes en el combo
            cbCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCliente.Items.Clear();
            foreach (var empresa in ComboData.Empresas)
                cbCliente.Items.Add(empresa);

            if (cbCliente.Items.Count > 0)
                cbCliente.SelectedIndex = 0;

            // Eventos
            btnSeleccionar.Click += BtnSeleccionar_Click;
            btnEmitirFactura.Click += BtnEmitirFactura_Click;
        }

        private void BtnSeleccionar_Click(object sender, EventArgs e)
        {
            string cliente = cbCliente.Text;
            if (string.IsNullOrWhiteSpace(cliente))
            {
                MessageBox.Show("Seleccioná un cliente para continuar.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Traer las facturas del cliente seleccionado
            var facturas = ComboData.FacturasDemo
                .Where(f => f.Cliente == cliente)
                .ToList();

            if (facturas.Count == 0)
            {
                MessageBox.Show("No hay encomiendas pendientes de facturar para este cliente.",
                    "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Cargar en el ListView (acumula)
            foreach (var f in facturas)
            {
                var item = new ListViewItem(f.NroGuia);
                item.SubItems.Add(f.Concepto);
                item.SubItems.Add(f.Monto.ToString("C", CultureInfo.GetCultureInfo("es-AR")));
                lvFacturasClientes.Items.Add(item);
            }
        }

        private void BtnEmitirFactura_Click(object sender, EventArgs e)
        {
            if (lvFacturasClientes.Items.Count == 0)
            {
                MessageBox.Show("No hay guías seleccionadas para facturar.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "¿Está seguro que desea realizar esta acción?",
                "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                MessageBox.Show("Factura emitida correctamente para el cliente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                lvFacturasClientes.Items.Clear(); // Limpia después de emitir
            }
        }

        private void btnVolverMenuPrincipal_Click(object sender, EventArgs e)
        {
            FormUtils.VolverAlMenu(this);
        }
    }
}
