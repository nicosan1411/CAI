using CAI_Proyecto.Forms.Inicio;
using CAI_Proyecto.Forms.Operacion.RendirEncomienda.Model;
using System.Windows.Forms;

namespace CAI_Proyecto.Forms.Operacion.RendirEncomienda.Forms
{
    public partial class RendirEncomiendasForm : Form
    {
        private readonly RendirEncomiendasModel _modelo;

        public RendirEncomiendasForm()
        {
            InitializeComponent();

            _modelo = new RendirEncomiendasModel();

            InitCombos();
            InitListViews();
            WireHandlers();
        }

        private void InitCombos()
        {
            cbFletero.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFletero.Items.Clear();

            foreach (var f in _modelo.ObtenerFleteros())
                cbFletero.Items.Add(f);

            cbFletero.SelectedIndex = -1;
        }

        private void InitListViews()
        {
            InicioForm.ConfigureListView(lvRetirosDomicilioAdmitir, true);
            InicioForm.ConfigureListView(lvEntregasDomicilioRealizadas, true);

            if (lvRetirosDomicilioAdmitir.Columns.Count >= 2)
            {
                lvRetirosDomicilioAdmitir.Columns[0].Width = 133;
                lvRetirosDomicilioAdmitir.Columns[1].Width = 168;
            }
            if (lvEntregasDomicilioRealizadas.Columns.Count >= 2)
            {
                lvEntregasDomicilioRealizadas.Columns[0].Width = 180;
                lvEntregasDomicilioRealizadas.Columns[1].Width = 120;
            }
        }

        private void WireHandlers()
        {
            cbFletero.SelectedIndexChanged += (_, __) => CargarAsignacionesParaFletero();
            btnGuardar.Click += (_, __) => GuardarYRefrescar();
            btnVolverMenuPrincipal.Click += (_, __) => VolverMenuPrincipal();
        }

        private void CargarAsignacionesParaFletero()
        {
            var fletero = cbFletero.SelectedItem as Fletero;
            if (fletero == null)
                return;

            var ok = _modelo.BuscarPorFletero(fletero);
            lvRetirosDomicilioAdmitir.Items.Clear();
            lvEntregasDomicilioRealizadas.Items.Clear();

            if (!ok) return;

            foreach (var g in _modelo.Retiros)
            {
                var item = new ListViewItem(string.Empty);
                item.SubItems.Add(g.NroGuia);
                item.Checked = false;
                lvRetirosDomicilioAdmitir.Items.Add(item);
            }

            foreach (var g in _modelo.Entregas)
            {
                var item = new ListViewItem(string.Empty);
                item.SubItems.Add(g.NroGuia);
                item.Checked = false;
                lvEntregasDomicilioRealizadas.Items.Add(item);
            }
        }

        private void GuardarYRefrescar()
        {
            var fletero = cbFletero.SelectedItem as Fletero;
            if (fletero == null)
            {
                MessageBox.Show("Debe seleccionar un fletero.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mapear checks a selección
            for (int i = 0; i < _modelo.Retiros.Count; i++)
                _modelo.Retiros[i].Seleccionada = lvRetirosDomicilioAdmitir.Items[i].Checked;

            for (int i = 0; i < _modelo.Entregas.Count; i++)
                _modelo.Entregas[i].Seleccionada = lvEntregasDomicilioRealizadas.Items[i].Checked;

            var ok = _modelo.GuardarCambios();
            if (!ok) return;

            // REFRESCAR manteniendo fletero seleccionado
            var nroFlete = fletero.NroFlete;
            _modelo.BuscarPorFletero(fletero); // recarga colecciones filtrando por nuevos estados

            // Repoblar ListViews con las guías restantes (las que cambiaron de estado ya no cumplen el filtro)
            lvRetirosDomicilioAdmitir.Items.Clear();
            foreach (var g in _modelo.Retiros)
            {
                var item = new ListViewItem(string.Empty);
                item.SubItems.Add(g.NroGuia);
                item.Checked = false;
                lvRetirosDomicilioAdmitir.Items.Add(item);
            }

            lvEntregasDomicilioRealizadas.Items.Clear();
            foreach (var g in _modelo.Entregas)
            {
                var item = new ListViewItem(string.Empty);
                item.SubItems.Add(g.NroGuia);
                item.Checked = false;
                lvEntregasDomicilioRealizadas.Items.Add(item);
            }

            // Mantener selección y foco
            cbFletero.SelectedItem = fletero;
            cbFletero.Focus();
        }

        private void VolverMenuPrincipal()
        {
            InicioForm.VolverAlMenu(this);
        }
    }
}
