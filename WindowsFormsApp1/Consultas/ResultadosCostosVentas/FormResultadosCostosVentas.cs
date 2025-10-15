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
    public partial class FormResultadosCostosVentas : Form
    {
        public FormResultadosCostosVentas()
        {
            InitializeComponent();
            InicializarFormulario();
        }

        private void InicializarFormulario()
        {
            btnBuscar.Click += BtnBuscar_Click;
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date;

            if (desde > hasta)
            {
                MessageBox.Show("El rango de fechas no es válido. Verifique los datos e intente nuevamente.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var resultados = ComboData.ResultadosCostosVentas
                .Where(r =>
                {
                    DateTime fDesde = DateTime.ParseExact(r.FechaDesde, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    DateTime fHasta = DateTime.ParseExact(r.FechaHasta, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    return fHasta >= desde && fDesde <= hasta;
                })
                .ToList();

            lvCuentasCorrientes.Items.Clear();

            if (resultados.Count == 0)
            {
                MessageBox.Show("No se registran movimientos en el período seleccionado.",
                    "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var r in resultados)
            {
                decimal resultado = r.VentasTotales - r.CostoTotal;
                decimal margen = r.CostoTotal == 0 ? 0 : (resultado / r.CostoTotal) * 100;

                var item = new ListViewItem(r.Empresa);
                item.SubItems.Add(r.Envios.ToString());
                item.SubItems.Add(r.CostoTotal.ToString("C", CultureInfo.GetCultureInfo("es-AR")));
                item.SubItems.Add(r.VentasTotales.ToString("C", CultureInfo.GetCultureInfo("es-AR")));
                item.SubItems.Add(resultado.ToString("C", CultureInfo.GetCultureInfo("es-AR")));
                item.SubItems.Add($"{margen:F2}%");
                lvCuentasCorrientes.Items.Add(item);
            }
        }

        private void btnVolverMenuPrincipal_Click_1(object sender, EventArgs e)
        {
            FormUtils.VolverAlMenu(this);
        }
    }
}
