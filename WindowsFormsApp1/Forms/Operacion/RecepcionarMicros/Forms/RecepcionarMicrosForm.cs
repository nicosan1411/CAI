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

            // Cargar micros desde el modelo
            foreach (var m in _modelo.ObtenerMicros())
                cbPatente.Items.Add(m);

            if (cbPatente.Items.Count > 0)
                cbPatente.SelectedIndex = -1; // Ninguno seleccionado inicialmente
        }

        private void InitListViews()
        {
            // Configurar apariencia general del ListView
            lvRecepciones.FullRowSelect = true;
            lvRecepciones.CheckBoxes = true;
            lvRecepciones.GridLines = true;
            lvRecepciones.View = View.Details;
            lvRecepciones.MultiSelect = false;

            // Ensure checkbox appears in "Entregado en CD Destino" column
            // by making it the first displayed column.
            columnEntregadoEnCD.DisplayIndex = 0;
            columnNroGuia.DisplayIndex = 1;
        }

        private void WireHandlers()
        {
            cbPatente.SelectedIndexChanged += (_, __) => CargarGuiasPorMicro();
            btnGuardar.Click += (_, __) => GuardarRecepcion();
            btnVolverMenuPrincipal.Click += (_, __) => InicioForm.VolverAlMenu(this);
        }

        private void CargarGuiasPorMicro()
        {
            var micro = cbPatente.SelectedItem as Micro;

            lvRecepciones.BeginUpdate();
            lvRecepciones.Items.Clear();

            if (!_modelo.BuscarPorMicro(micro))
            {
                lvRecepciones.EndUpdate();
                return;
            }

            foreach (var guia in _modelo.Guias)
            {
                // First displayed column (Entregado) stays empty, only shows the checkbox.
                var item = new ListViewItem(string.Empty)
                {
                    Checked = guia.Seleccionada
                };

                // Second column (N° de guía) shows the guide number.
                item.SubItems.Add(guia.NroGuia);

                lvRecepciones.Items.Add(item);
            }

            lvRecepciones.EndUpdate();
        }

        private void GuardarRecepcion()
        {
            // Actualizar selección en el modelo según los checks del ListView
            foreach (ListViewItem item in lvRecepciones.Items)
            {
                // The guide number is now in SubItems[1]
                var nroGuia = item.SubItems.Count > 1 ? item.SubItems[1].Text.Trim() : string.Empty;
                var guia = _modelo.Guias.FirstOrDefault(g => g.NroGuia == nroGuia);
                if (guia != null)
                    guia.Seleccionada = item.Checked;
            }

            // Guardar y validar resultados
            if (_modelo.GuardarCambios())
            {
                // Recargar la lista luego de guardar (para reflejar cambios)
                CargarGuiasPorMicro();
            }
        }
    }
}
