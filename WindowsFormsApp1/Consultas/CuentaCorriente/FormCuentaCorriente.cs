// ==================================================================
// FALTA CÓDIGO POR REALIZAR
// EL CÓDIGO AQUÍ ES SOLO DEMOSTRATIVO PARA MOSTRAR LA IDEA GENERAL
// ==================================================================

using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.Consultas.CuentaCorriente;

namespace WindowsFormsApp1
{
    public partial class FormCuentaCorriente : Form
    {
        internal FormCuentaCorrienteModelo modelo = new FormCuentaCorrienteModelo();

        public FormCuentaCorriente()
        {
            InitializeComponent();
            InicializarFormulario();
        }

        private void InicializarFormulario()
        {
            // Cargar clientes en el ComboBox
            cbCliente.Items.Clear();

            //foreach (var empresa in ComboData.Empresas)
            foreach (var empresa in modelo.Empresas)
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
            if (cbCliente.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un cliente.", "Cliente no seleccionado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Filtros filtros = new Filtros();
            filtros.CuitCliente = cbCliente.SelectedItem?.ToString();
            filtros.FechaDesde = dtpDesde.Value.Date;
            filtros.FechaHasta = dtpHasta.Value.Date;
            var ok = modelo.CargarEstadoCuenta(filtros);
            if (!ok)
            {
                return;
            }

            lvCuentasCorrientes.Items.Clear();

            foreach (var mov in modelo.MovimientosCuenta)
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

