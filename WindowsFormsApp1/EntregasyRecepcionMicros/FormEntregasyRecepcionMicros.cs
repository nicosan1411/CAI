using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormEntregasyRecepcionMicros : Form
    {
        private readonly GuiaService _service;

        public FormEntregasyRecepcionMicros()
        {
            InitializeComponent();

            // Inyección simple (como en PedidoService)
            _service = new GuiaService(new InMemoryGuiaRepository());

            WireHandlers();
            InitCombos();
            PrepareListViews();
            CargarListasParaPatenteSeleccionada();
        }

        private void WireHandlers()
        {
            cbPatenteMicro.SelectedIndexChanged += (_, __) => CargarListasParaPatenteSeleccionada();
        }

        private void InitCombos()
        {
            cbPatenteMicro.Items.Clear();
            cbPatenteMicro.Items.AddRange(ComboData.Patentes.ToArray());
            FormUtils.MakeDropDownList(cbPatenteMicro);
            if (cbPatenteMicro.Items.Count > 0) cbPatenteMicro.SelectedIndex = 0;
        }

        private void PrepareListViews()
        {
            FormUtils.ConfigureListView(listView2, withCheckboxes: true);  // Recepciones
            FormUtils.ConfigureListView(listView1, withCheckboxes: false); // Despachos
        }

        private void CargarListasParaPatenteSeleccionada()
        {
            var patente = cbPatenteMicro.Text?.Trim();
            var data = _service.ObtenerPorPatente(patente);

            // Caso CU 2.1: sin guías asociadas
            if ((data.recepciones?.Count ?? 0) == 0 && (data.despachos?.Count ?? 0) == 0)
            {
                FormUtils.Info("No hay encomiendas asociadas al micro ingresado.", "Sin datos");
                listView2.Items.Clear();
                listView1.Items.Clear();
                return;
            }

            FormUtils.FillListView(listView2, data.recepciones);
            FormUtils.FillListView(listView1, data.despachos);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var patente = cbPatenteMicro.Text?.Trim();
            if (string.IsNullOrWhiteSpace(patente))
            {
                MessageBox.Show("Seleccioná una patente de micro.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var guiasRecep = listView2.Items
                .Cast<ListViewItem>()
                .Where(i => i.Checked)
                .Select(i => i.SubItems.Count > 1 ? i.SubItems[1].Text : null)
                .Where(g => !string.IsNullOrWhiteSpace(g))
                .ToList();

            // Excepción 1: ninguna guía tildada → confirmación especial
            if (guiasRecep.Count == 0)
            {
                if (!FormUtils.Confirm("No se seleccionó ninguna recepción.\n\nDe continuar, el micro no cumplió con ninguna entrega estipulada.\n\n¿Desea guardar igualmente?"))
                    return;
            }
            else
            {
                if (!FormUtils.Confirm("¿Esta seguro de que desea guardar los cambios realizados?"))
                    return;
            }

            var (ok, msg) = _service.GuardarRecepciones(guiasRecep);
            if (!ok)
            {
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FormUtils.Info(msg, "OK");
            ResetUI();
        }

        private void ResetUI()
        {
            if (cbPatenteMicro.Items.Count > 0) cbPatenteMicro.SelectedIndex = 0;
            listView2.Items.Clear();
            listView1.Items.Clear();
        }

        private void btnVolverMenuPrincipal_Click(object sender, EventArgs e)
        {
            FormUtils.VolverAlMenu(this);
        }
    }
}
