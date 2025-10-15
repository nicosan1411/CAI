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
    public partial class FormCuentaCorriente : Form
    {
        public FormCuentaCorriente()
        {
            InitializeComponent();
            InicializarFormulario();
        }

        private void InicializarFormulario()
        {
            // Cargar clientes en el ComboBox
            cbCliente.Items.Clear();
            foreach (var empresa in ComboData.Empresas)
                cbCliente.Items.Add(empresa);

            if (cbCliente.Items.Count > 0)
                cbCliente.SelectedIndex = -1;

            // Configurar ListView
            lvCuentasCorrientes.FullRowSelect = true;
            lvCuentasCorrientes.GridLines = true;
            lvCuentasCorrientes.View = View.Details;

            // Eventos
            btnBuscar.Click += BtnBuscar_Click;
            btnExportar.Click += BtnExportar_Click;
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            string clienteSeleccionado = cbCliente.Text;
            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date;

            // Filtrar movimientos según cliente y rango de fechas
            var movimientos = ComboData.MovimientosCuenta
                .Where(m => m.Cliente == ObtenerNombreCliente(clienteSeleccionado))
                .Where(m =>
                {
                    DateTime fecha = DateTime.ParseExact(m.Fecha, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    return fecha >= desde && fecha <= hasta;
                })
                .ToList();

            lvCuentasCorrientes.Items.Clear();

            if (movimientos.Count == 0)
            {
                MessageBox.Show("No se encontraron movimientos para el cliente en el rango de fechas seleccionado.",
                    "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var mov in movimientos)
            {
                var item = new ListViewItem(mov.Fecha);
                item.SubItems.Add(mov.Comprobante);
                item.SubItems.Add(mov.Concepto);
                item.SubItems.Add(mov.Monto.ToString("C", CultureInfo.GetCultureInfo("es-AR")));
                item.SubItems.Add(mov.Pago.ToString("C", CultureInfo.GetCultureInfo("es-AR")));
                item.SubItems.Add(mov.Saldo.ToString("C", CultureInfo.GetCultureInfo("es-AR")));
                lvCuentasCorrientes.Items.Add(item);
            }
        }

        private string ObtenerNombreCliente(string comboText)
        {
            // Ejemplo: "30-50109269-6  Unilever de Argentina S.A." → "Unilever de Argentina S.A."
            int idx = comboText.IndexOf("  ");
            if (idx >= 0 && idx + 2 < comboText.Length)
                return comboText.Substring(idx + 2).Trim();
            return comboText;
        }

        private void BtnExportar_Click(object sender, EventArgs e)
        {
            // No hace nada aún, solo muestra mensaje
            MessageBox.Show("Archivo exportado correctamente (demo).", "Exportar",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnVolverMenuPrincipal_Click(object sender, EventArgs e)
        {
            FormUtils.VolverAlMenu(this);
        }
    }
}

