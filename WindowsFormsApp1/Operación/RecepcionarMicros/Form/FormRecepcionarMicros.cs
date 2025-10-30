using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.Inicio;

namespace WindowsFormsApp1.Operación.RecepcionarMicros.Model
{
    public partial class FormRecepcionarMicros : Form
    {
        private readonly FormRecepcionarMicrosModelo _modelo;

        public FormRecepcionarMicros()
        {
            InitializeComponent();

            _modelo = new FormRecepcionarMicrosModelo();

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
        }

        private void WireHandlers()
        {
            cbPatente.SelectedIndexChanged += (_, __) => CargarGuiasPorMicro();
            btnGuardar.Click += (_, __) => GuardarRecepcion();
            btnVolverMenuPrincipal.Click += (_, __) => FormInicio.VolverAlMenu(this);
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
                var item = new ListViewItem(guia.NroGuia)
                {
                    Checked = guia.Seleccionada
                };
                item.SubItems.Add(""); // Columna “Entregado en CD Destino”
                lvRecepciones.Items.Add(item);
            }

            lvRecepciones.EndUpdate();
        }

        private void GuardarRecepcion()
        {
            // Actualizar selección en el modelo según los checks del ListView
            foreach (ListViewItem item in lvRecepciones.Items)
            {
                var nroGuia = item.Text.Trim();
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
