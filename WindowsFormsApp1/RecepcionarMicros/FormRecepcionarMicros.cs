using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormRecepcionarMicros : Form
    {
        private readonly IGuiaMaster _master = new CsvGuiaMaster();
        private readonly IGuiaRepository _repo = new InMemoryGuiaRepository(); // reservado por si luego querés asignar por patente
        private readonly CsvRendicionRepository _rendicionRepo = new CsvRendicionRepository("data", "recepcion_micro.txt");

        public FormRecepcionarMicros()
        {
            InitializeComponent();

            InitCombos();
            InitListViews();
            WireHandlers();

            // Cargar algo al abrir para no ver vacío
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
            // Dejamos que el Designer defina las columnas.
            // Solo ajustamos comportamiento (checkboxes, selección, etc.).
            FormUtils.ConfigureListView(lvRecepciones, true);
        }

        private void WireHandlers()
        {
            cbPatente.SelectedIndexChanged += (_, __) => CargarPorPatente();
            btnGuardar.Click += (_, __) => GuardarMicros();
            btnVolverMenuPrincipal.Click += (_, __) => FormUtils.VolverAlMenu(this);
        }

        private void CargarPorPatente()
        {
            // Estados según CU:
            //  - Despachos: "Admitida en CD origen"
            //  - Recepciones: "En tránsito"
            var master = new CsvGuiaMaster();
            var despachos = master.ListarPorEstado(GuiaEstados.AdmitidaOrigen).ToList();
            var recepciones = master.ListarPorEstado(GuiaEstados.EnTransito).ToList();

            // (Si más adelante querés filtrar por patente, intersectás acá con tu asignación por micro)

            // --- RECEPCIONES (2 columnas: N° Guía | Entregado en CD Destino) ---
            lvRecepciones.BeginUpdate();
            lvRecepciones.Items.Clear();
            foreach (var g in recepciones)
            {
                var item = new ListViewItem(g) { Checked = false }; // Columna 0: N° Guía
                item.SubItems.Add(""); // Columna "Entregado en CD Destino" (vacía/indicativa)
                lvRecepciones.Items.Add(item);
            }
            lvRecepciones.EndUpdate();
        }

        private void GuardarMicros()
        {
            var pat = cbPatente.Text?.Trim();
            if (string.IsNullOrWhiteSpace(pat))
            {
                MessageBox.Show("Seleccioná una patente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var recepcionesMarcadas = lvRecepciones.Items.Cast<ListViewItem>()
                .Where(it => it.Checked)
                .Select(it => it.Text.Trim())   // también es la guía en la col 0
                .Where(s => s != "")
                .ToList();

            if (MessageBox.Show("¿Confirmás registrar las guías seleccionadas?",
                "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            var master = new CsvGuiaMaster();

            // Recepciones: En tránsito -> Admitida en CD destino
            if (recepcionesMarcadas.Count > 0)
                master.CambiarEstado(recepcionesMarcadas, GuiaEstados.AdmitidaDestino);

            MessageBox.Show("Datos guardados.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarPorPatente(); // refrescar listas
        }
    }
}
