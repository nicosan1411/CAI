using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormRecepcionarMicros : Form
    {
        private readonly IGuiaMaster _master = new CsvGuiaMaster();
        private readonly MicroAssignmentService _microAssign = new MicroAssignmentService();

        public FormRecepcionarMicros()
        {
            InitializeComponent();
            InitCombos();
            InitListViews();
            WireHandlers();

            if (cbPatente.Items.Count > 0)
            {
                cbPatente.SelectedIndex = 0;
                CargarPorPatente();
            }
        }

        private void InitCombos()
        {
            cbPatente.DropDownStyle = ComboBoxStyle.DropDownList;
            cbPatente.Items.Clear();
            cbPatente.Items.AddRange(ComboData.Patentes.ToArray());
            if (cbPatente.Items.Count > 0) cbPatente.SelectedIndex = -1;
        }

        private void InitListViews()
        {
            // lvRecepciones ya tiene columnas definidas por Designer (N° Guía | Entregado en CD Destino)
            FormUtils.ConfigureListView(lvRecepciones, true);
        }

        private void WireHandlers()
        {
            cbPatente.SelectedIndexChanged += (_, __) => CargarPorPatente();
            btnGuardar.Click += (_, __) => GuardarRecepciones();
            btnVolverMenuPrincipal.Click += (_, __) => FormUtils.VolverAlMenu(this);
        }

        private void CargarPorPatente()
        {
            var pat = cbPatente.Text?.Trim();
            if (string.IsNullOrWhiteSpace(pat))
            {
                lvRecepciones.Items.Clear();
                return;
            }

            // IDs asignados a esta patente
            var asignadas = new HashSet<string>(_microAssign.ListarAsignadas(pat), StringComparer.OrdinalIgnoreCase);

            // Desde el master, traemos por estado y filtramos por "asignadas a esta patente"
            var enOrigen = _master.ListarPorEstado(GuiaEstados.AdmitidaOrigen).Where(asignadas.Contains);
            var enTransito = _master.ListarPorEstado(GuiaEstados.EnTransito).Where(asignadas.Contains);

            // Unión de ambas (ListView único)
            var paraRecepcionar = enOrigen.Concat(enTransito).Distinct(StringComparer.OrdinalIgnoreCase).ToList();

            lvRecepciones.BeginUpdate();
            lvRecepciones.Items.Clear();
            foreach (var g in paraRecepcionar)
            {
                var it = new ListViewItem(g) { Checked = false }; // Columna 0: N° Guía
                it.SubItems.Add(""); // Columna "Entregado en CD Destino" (visual)
                lvRecepciones.Items.Add(it);
            }
            lvRecepciones.EndUpdate();
        }

        private void GuardarRecepciones()
        {
            var pat = cbPatente.Text?.Trim();
            if (string.IsNullOrWhiteSpace(pat))
            {
                MessageBox.Show("Seleccioná una patente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var seleccionadas = lvRecepciones.Items
                .Cast<ListViewItem>()
                .Where(it => it.Checked)
                .Select(it => it.Text.Trim())
                .Where(s => s != "")
                .ToList();

            if (seleccionadas.Count == 0)
            {
                if (MessageBox.Show("No seleccionaste ninguna guía. ¿Querés guardar igual?",
                    "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            }

            if (MessageBox.Show("¿Confirmás recepcionar las guías seleccionadas en CD destino?",
                "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            // Cambiar estado en master: (Admitida en CD origen | En tránsito) -> Admitida en CD destino
            if (seleccionadas.Count > 0)
                _master.CambiarEstado(seleccionadas, GuiaEstados.AdmitidaDestino);

            // Al quedar en CD destino ya no deben seguir asignadas a la patente
            if (seleccionadas.Count > 0)
                _microAssign.RemoverAsignadas(pat, seleccionadas);

            MessageBox.Show("Recepciones guardadas.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarPorPatente(); // refrescar
        }
    }
}
