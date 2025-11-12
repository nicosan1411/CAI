using CAI_Proyecto.Forms.Inicio;
using CAI_Proyecto.Forms.Operacion.RendirEncomienda.Model;
using System.Windows.Forms;

namespace CAI_Proyecto.Forms.Operacion.RendirEncomienda.Forms
{
    public partial class RendirEncomiendasForm : Form
    {
        // Modelo de negocio (reemplaza al RendicionService)
        private readonly RendirEncomiendasModel _modelo;

        public RendirEncomiendasForm()
        {
            InitializeComponent();

            _modelo = new RendirEncomiendasModel();

            InitCombos();
            InitListViews();
            WireHandlers();
        }

        // =========================
        // Inicialización de combos
        // =========================
        private void InitCombos()
        {
            cbFletero.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFletero.Items.Clear();

            foreach (var f in _modelo.ObtenerFleteros())
                cbFletero.Items.Add(f);

            cbFletero.SelectedIndex = -1;
        }

        // =========================
        // Inicialización de ListViews
        // =========================
        private void InitListViews()
        {
            // Retiros
            InicioForm.ConfigureListView(lvRetirosDomicilioAdmitir, true);
            lvRetirosDomicilioAdmitir.Columns.Clear();
            lvRetirosDomicilioAdmitir.Columns.Add("N° Guía", 120);
            lvRetirosDomicilioAdmitir.Columns.Add("Estado", 180);

            // Entregas
            InicioForm.ConfigureListView(lvEntregasDomicilioRealizadas, true);
            lvEntregasDomicilioRealizadas.Columns.Clear();
            lvEntregasDomicilioRealizadas.Columns.Add("N° Guía", 120);
            lvEntregasDomicilioRealizadas.Columns.Add("Estado", 180);
        }

        // =========================
        // Asociar eventos
        // =========================
        private void WireHandlers()
        {
            cbFletero.SelectedIndexChanged += (_, __) => CargarAsignacionesParaFletero();
            btnGuardar.Click += (_, __) => GuardarYRefrescar();
            btnVolverMenuPrincipal.Click += (_, __) => VolverMenuPrincipal();
        }

        // =========================
        // Cargar guías del fletero
        // =========================
        private void CargarAsignacionesParaFletero()
        {
            var fletero = cbFletero.SelectedItem as Fletero;
            if (fletero == null)
                return;

            var ok = _modelo.BuscarPorFletero(fletero);
            lvRetirosDomicilioAdmitir.Items.Clear();
            lvEntregasDomicilioRealizadas.Items.Clear();

            if (!ok) return;

            // --- RETIROS ---
            foreach (var g in _modelo.Retiros)
            {
                var item = new ListViewItem(g.NroGuia);
                item.SubItems.Add(g.Estado);
                item.Checked = false;
                lvRetirosDomicilioAdmitir.Items.Add(item);
            }

            // --- ENTREGAS ---
            foreach (var g in _modelo.Entregas)
            {
                var item = new ListViewItem(g.NroGuia);
                item.SubItems.Add(g.Estado);
                item.Checked = false;
                lvEntregasDomicilioRealizadas.Items.Add(item);
            }
        }

        // =========================
        // Guardar rendición
        // =========================
        private void GuardarYRefrescar()
        {
            var fletero = cbFletero.SelectedItem as Fletero;
            if (fletero == null)
            {
                MessageBox.Show("Debe seleccionar un fletero.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Actualizar selección en el modelo según los checkboxes
            for (int i = 0; i < _modelo.Retiros.Count; i++)
                _modelo.Retiros[i].Seleccionada = lvRetirosDomicilioAdmitir.Items[i].Checked;

            for (int i = 0; i < _modelo.Entregas.Count; i++)
                _modelo.Entregas[i].Seleccionada = lvEntregasDomicilioRealizadas.Items[i].Checked;

            // Ejecutar la lógica de guardado
            var ok = _modelo.GuardarCambios();
            if (!ok) return;

            // Refrescar vista
            lvRetirosDomicilioAdmitir.Items.Clear();
            lvEntregasDomicilioRealizadas.Items.Clear();
            cbFletero.SelectedIndex = -1;
            cbFletero.Focus();
        }

        // =========================
        // Volver al menú principal
        // =========================
        private void VolverMenuPrincipal()
        {
            InicioForm.VolverAlMenu(this);
        }

    }

}
