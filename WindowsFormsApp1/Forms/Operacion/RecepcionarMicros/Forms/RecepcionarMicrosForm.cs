using CAI_Proyecto.Forms.Inicio;
using CAI_Proyecto.Forms.Operacion.RecepcionarMicros.Model;
using System.Linq;
using System.Windows.Forms;

namespace CAI_Proyecto.Forms.Operacion.RecepcionarMicros.Forms
{
    public partial class RecepcionarMicrosForm : Form
    {
        private readonly RecepcionarMicrosModel _modelo;

        public RecepcionarMicrosForm()
        {
            InitializeComponent();

            _modelo = new RecepcionarMicrosModel();

            InitCombos();
            InitListViews();
            WireHandlers();
        }

        private void InitCombos()
        {
            cbPatente.DropDownStyle = ComboBoxStyle.DropDownList;
            cbPatente.Items.Clear();

            foreach (var m in _modelo.Micros)
                cbPatente.Items.Add(m);

            cbPatente.SelectedIndex = -1;
        }

        private void InitListViews()
        {
            lvRecepciones.FullRowSelect = true;
            lvRecepciones.CheckBoxes = true;
            lvRecepciones.GridLines = true;
            lvRecepciones.View = View.Details;
            lvRecepciones.MultiSelect = false;

            columnEntregadoEnCD.DisplayIndex = 0;
            columnNroGuia.DisplayIndex = 1;
        }

        private void WireHandlers()
        {
            cbPatente.SelectedIndexChanged += (_, __) => CargarGuiasPorMicro();
            btnGuardar.Click += (_, __) => GuardarAccion();
            btnVolverMenuPrincipal.Click += (_, __) => InicioForm.VolverAlMenu(this);
        }

        private void CargarGuiasPorMicro()
        {
            var micro = cbPatente.SelectedItem as Micro;

            lvRecepciones.BeginUpdate();
            lvRecepciones.Items.Clear();

            // Nuevo: usar PrepararMicro en lugar de BuscarPorMicro
            var hayRecepcion = _modelo.PrepararMicro(micro);

            if (hayRecepcion)
            {
                // Mostrar guías en tránsito (estado 4) para recepcionar
                foreach (var guia in _modelo.Guias)
                {
                    var item = new ListViewItem(string.Empty)
                    {
                        Checked = guia.Seleccionada
                    };
                    item.SubItems.Add(guia.NroGuia);
                    lvRecepciones.Items.Add(item);
                }
            }
            else
            {
                // No hay guías de recepción; si hay para carga avisar
                if (_modelo.HayGuiasParaCarga)
                {
                    MessageBox.Show(
                        "No hay guías para recepcionar de este micro.\n" +
                        "Hay guías admitidas (Estado 2) disponibles para cargar a este micro.",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }

            lvRecepciones.EndUpdate();
        }

        private void GuardarAccion()
        {
            var micro = cbPatente.SelectedItem as Micro;

            // Si hay guías en el ListView estamos en modo recepción
            if (_modelo.Guias.Any())
            {
                foreach (ListViewItem item in lvRecepciones.Items)
                {
                    var nroGuia = item.SubItems.Count > 1 ? item.SubItems[1].Text.Trim() : string.Empty;
                    var guia = _modelo.Guias.FirstOrDefault(g => g.NroGuia == nroGuia);
                    if (guia != null)
                        guia.Seleccionada = item.Checked;
                }

                if (_modelo.GuardarRecepcion())
                    CargarGuiasPorMicro();
            }
            else
            {
                // No hay guías para recepción; si hay para carga ejecutamos la carga (2 -> 4)
                if (_modelo.HayGuiasParaCarga)
                {
                    if (_modelo.CargarGuiasPendientes(micro))
                        CargarGuiasPorMicro();
                }
                else
                {
                    MessageBox.Show("No hay acciones disponibles (ni recepción ni carga).",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
