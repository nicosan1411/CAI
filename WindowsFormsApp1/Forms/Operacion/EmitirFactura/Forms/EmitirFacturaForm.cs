using CAI_Proyecto.Forms.Inicio;
using CAI_Proyecto.Forms.Operacion.EmitirFactura.Model;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace CAI_Proyecto.Forms.Operacion.EmitirFactura.Forms
{
    public partial class EmitirFacturaForm : Form
    {
        internal EmitirFacturaModel modelo = new EmitirFacturaModel();
        public EmitirFacturaForm()
        {
            InitializeComponent();
            InicializarFormulario();
        }

        private void InicializarFormulario()
        {
            // Cargar clientes en el combo
            cbCliente.Items.Clear();

            cbCliente.DataSource = modelo.Empresas;
            cbCliente.DisplayMember = "Cuit_RazonSocial";
            cbCliente.ValueMember = "Cuit";


            if (cbCliente.Items.Count > 0)
                cbCliente.SelectedIndex = -1;

            // Eventos
            btnSeleccionar.Click += BtnSeleccionar_Click;
            btnEmitirFactura.Click += BtnEmitirFactura_Click;
        }

        private void BtnSeleccionar_Click(object sender, EventArgs e)
        {
            if (cbCliente.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un cliente.", "Cliente no seleccionado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Limpiar el ListView antes de cargar nuevos datos
            lvFacturasClientes.Items.Clear();

            // Obtener el cliente seleccionado
            var clienteSeleccionado = (ClienteFactura)cbCliente.SelectedItem;

            // 🔹 Filtrar por CUIT, no por texto
            var facturas = modelo.ListadoFacturacion
                .Where(f => f.Cliente.Contains(clienteSeleccionado.Cuit))
                .ToList();

            if (facturas.Count == 0)
            {
                MessageBox.Show("No hay encomiendas pendientes de facturar para este cliente.",
                    "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Cargar los resultados en el ListView
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
                // ✅ Identificar las guías facturadas
                var guiasFacturadas = lvFacturasClientes.Items
                    .Cast<ListViewItem>()
                    .Select(i => i.Text) // el NroGuia está en la columna principal
                    .ToList();

                // ✅ Eliminar esas guías del modelo (de la "base de datos" temporal)
                var listaEditable = modelo.ListadoFacturacion.ToList();
                listaEditable.RemoveAll(f => guiasFacturadas.Contains(f.NroGuia));
                modelo.ListadoFacturacion = listaEditable;

                MessageBox.Show("Factura emitida correctamente para el cliente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ✅ Limpiar la vista
                lvFacturasClientes.Items.Clear();
            }
        }

        private void btnVolverMenuPrincipal_Click(object sender, EventArgs e)
        {
            InicioForm.VolverAlMenu(this);
        }
    }
}