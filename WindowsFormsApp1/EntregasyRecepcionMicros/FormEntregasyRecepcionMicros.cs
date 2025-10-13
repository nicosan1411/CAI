using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormEntregasyRecepcionMicros : Form
    {
        private readonly IGuiaMaster _master = new CsvGuiaMaster();
        private readonly IGuiaRepository _repo = new InMemoryGuiaRepository();
        private readonly CsvRendicionRepository _rendicionRepo = new CsvRendicionRepository("data", "recepcion_micro.txt");

        public FormEntregasyRecepcionMicros()
        {
            InitializeComponent();

            InitCombos();
            InitListViews();
            WireHandlers();
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
            FormUtils.ConfigureListView(lvRecepciones, true);
            FormUtils.ConfigureListView(lvDespachos, true);
            lvRecepciones.Columns.Add("Guía", 150);
            lvDespachos.Columns.Add("Guía", 150);
        }

        private void WireHandlers()
        {
            cbPatente.SelectedIndexChanged += (_, __) => CargarPorPatente();
            btnAceptar.Click += (_, __) => GuardarMicros();
            btnVolverMenuPrincipal.Click += (_, __) => FormUtils.VolverAlMenu(this);
        }

        private void CargarPorPatente()
        {
            var pat = cbPatente.Text?.Trim();
            if (string.IsNullOrWhiteSpace(pat))
            {
                lvDespachos.Items.Clear();
                lvRecepciones.Items.Clear();
                return;
            }

            var despachos = _master.ListarPorEstado(GuiaEstados.AdmitidaOrigen);
            var recepciones = _master.ListarPorEstado(GuiaEstados.EnTransito);

            // (Si más adelante querés filtrar por patente, intersectás acá con tu asignación por micro)
            lvDespachos.BeginUpdate();
            lvDespachos.Items.Clear();
            foreach (var g in despachos) lvDespachos.Items.Add(new ListViewItem(new[] { "", g }) { Checked = false });
            lvDespachos.EndUpdate();

            lvRecepciones.BeginUpdate();
            lvRecepciones.Items.Clear();
            foreach (var g in recepciones) lvRecepciones.Items.Add(new ListViewItem(new[] { "", g }) { Checked = false });
            lvRecepciones.EndUpdate();
        }


        private int FindGuiaCol(ListView lv)
        {
            // Si tu LV tiene ["", "Guía"], devolvé 1. Si es de 1 col ("Guía"), devolvé 0.
            return (lv.Columns.Count >= 2) ? 1 : 0;
        }

        private void GuardarMicros()
        {
            var pat = cbPatente.Text?.Trim();
            if (string.IsNullOrWhiteSpace(pat))
            {
                MessageBox.Show("Seleccioná una patente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var iDesp = FindGuiaCol(lvDespachos);
            var iRecep = FindGuiaCol(lvRecepciones);

            var despachosMarcados = lvDespachos.Items.Cast<ListViewItem>()
                .Where(it => it.Checked)
                .Select(it => it.SubItems[iDesp].Text.Trim())
                .Where(s => s != "")
                .ToList();

            var recepcionesMarcadas = lvRecepciones.Items.Cast<ListViewItem>()
                .Where(it => it.Checked)
                .Select(it => it.SubItems[iRecep].Text.Trim())
                .Where(s => s != "")
                .ToList();

            if (despachosMarcados.Count == 0 && recepcionesMarcadas.Count == 0)
            {
                if (MessageBox.Show("No seleccionaste ninguna guía. ¿Querés guardar igual?",
                    "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            }

            if (MessageBox.Show("¿Confirmás registrar las guías seleccionadas?",
                "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            var master = new CsvGuiaMaster();

            // Despachos: Admitida en CD origen -> En tránsito
            if (despachosMarcados.Count > 0)
                master.CambiarEstado(despachosMarcados, GuiaEstados.EnTransito);

            // Recepciones: En tránsito -> Admitida en CD destino
            if (recepcionesMarcadas.Count > 0)
                master.CambiarEstado(recepcionesMarcadas, GuiaEstados.AdmitidaDestino);

            MessageBox.Show("Datos guardados.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarPorPatente(); // refrescar listas
        }
    }
}
