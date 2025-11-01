using CAI_Proyecto.Forms.Consultas.ResultadosCostosVentas.Model;
using CAI_Proyecto.Forms.Inicio;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace CAI_Proyecto.Forms.Consultas.ResultadosCostosVentas.Forms
{
    public partial class ResultadosCostosVentasForm : Form
    {
        private readonly CultureInfo _ars = CultureInfo.GetCultureInfo("es-AR");
        internal ResultadosCostosVentasModel modelo = new ResultadosCostosVentasModel();

        public ResultadosCostosVentasForm()
        {
            InitializeComponent();
            InicializarFormulario();
        }

        private void InicializarFormulario()
        {
            // ListView (uso el nombre existente en tu Designer)
            lvCuentasCorrientes.FullRowSelect = true;
            lvCuentasCorrientes.GridLines = true;
            lvCuentasCorrientes.View = View.Details;
            lvCuentasCorrientes.MultiSelect = false;

            if (lvCuentasCorrientes.Columns.Count == 0)
            {
                lvCuentasCorrientes.Columns.Add("Empresa", 160);
                lvCuentasCorrientes.Columns.Add("Envíos", 70, HorizontalAlignment.Right);
                lvCuentasCorrientes.Columns.Add("Costo Total", 110, HorizontalAlignment.Right);
                lvCuentasCorrientes.Columns.Add("Ventas Totales", 120, HorizontalAlignment.Right);
                lvCuentasCorrientes.Columns.Add("Resultado", 110, HorizontalAlignment.Right);
                lvCuentasCorrientes.Columns.Add("Margen", 80, HorizontalAlignment.Right);
            }

            // DateTimePicker
            dtpDesde.Value = DateTime.Today.AddDays(-7);
            dtpHasta.Value = DateTime.Today;

            // Eventos
            btnBuscar.Click += BtnBuscar_Click;
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            var filtros = new Filtros
            {
                Desde = dtpDesde.Value.Date,
                Hasta = dtpHasta.Value.Date
            };

            var ok = modelo.Buscar(filtros);
            lvCuentasCorrientes.Items.Clear();

            if (!ok) return;

            foreach (var r in modelo.Resultados)
            {
                var item = new ListViewItem(r.Empresa);
                item.SubItems.Add(r.Envios.ToString("N0", _ars));
                item.SubItems.Add(r.CostoTotal.ToString("C", _ars));
                item.SubItems.Add(r.VentasTotales.ToString("C", _ars));
                item.SubItems.Add(r.Resultado.ToString("C", _ars));
                item.SubItems.Add(r.Margen.ToString("P2", _ars)); // % con 2 decimales
                lvCuentasCorrientes.Items.Add(item);
            }
        }

        private void btnVolverMenuPrincipal_Click_1(object sender, EventArgs e)
        {
            InicioForm.VolverAlMenu(this);
        }
    }
}