using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    // Form principal para registrar pedidos desde Call Center
    public partial class FormImponerCallCenter : Form
    {
        // Servicio que maneja la lógica de negocio (validaciones, almacenamiento, etc.)
        private readonly PedidoService _service;

        public FormImponerCallCenter()
        {
            InitializeComponent();

            // Inyección simple: el servicio usa un repositorio CSV como almacenamiento
            _service = new PedidoService(new CsvOrderRepository());

            // Conectar eventos, inicializar combos y estado inicial de controles
            WireHandlers();
            InitCombos();
            ToggleAgenciaControls();
            ToggleDireccionDestinatario();

            // Configurar el campo DNI: máximo 8 dígitos y solo números permitidos
            txtDniDestinatario.MaxLength = 8;
            txtDniDestinatario.KeyPress += OnlyDigits_KeyPress;
        }

        // ======================================================
        //   CONFIGURACIÓN DE EVENTOS DE LA UI
        // ======================================================
        private void WireHandlers()
        {
            // Cada cambio en los radio buttons ajusta la habilitación de combos
            rbRetiroDomicilio.CheckedChanged += (_, __) => ToggleAgenciaControls();
            rbRetiroAgencia.CheckedChanged += (_, __) => ToggleAgenciaControls();

            // En los tipos de envío, además de agencias, cambia si se pide dirección o no
            rbEnvioDomicilio.CheckedChanged += (_, __) => { ToggleAgenciaControls(); ToggleDireccionDestinatario(); };
            rbEnvioCentroDistribucion.CheckedChanged += (_, __) => { ToggleAgenciaControls(); ToggleDireccionDestinatario(); };
            rbEnvioAgencia.CheckedChanged += (_, __) => { ToggleAgenciaControls(); ToggleDireccionDestinatario(); };
        }

        // ======================================================
        //   CARGA DE COMBOS CON DATOS PREDEFINIDOS
        // ======================================================
        private void InitCombos()
        {
            // Se obtienen las listas estáticas desde ComboData (reutilizable)
            cbEmpresaCliente.Items.Clear();
            cbEmpresaCliente.Items.AddRange(ComboData.Empresas.ToArray());

            cbDimension.Items.Clear();
            cbDimension.Items.AddRange(ComboData.Dimensiones.ToArray());

            cbProvinciaEnvio.DataSource = ComboData.Provincias.ToList();

            cbAgenciaRetiro.Items.Clear();
            cbAgenciaRetiro.Items.AddRange(ComboData.AgenciasRetiro.ToArray());

            cmbAgenciaEnvio.Items.Clear();
            cmbAgenciaEnvio.Items.AddRange(ComboData.AgenciasEnvio.ToArray());

            // Se seleccionan valores por defecto
            if (cbEmpresaCliente.Items.Count > 0) cbEmpresaCliente.SelectedIndex = -1;
            if (cbDimension.Items.Count > 0) cbDimension.SelectedIndex = -1;
            if (cbProvinciaEnvio.Items.Count > 0) cbProvinciaEnvio.SelectedIndex = -1;

            // Valores iniciales de los radio buttons
            rbRetiroDomicilio.Checked = true;
            rbEnvioDomicilio.Checked = true;

            // Activar autocompletado en el combo de provincias
            SetAutoComplete(cbProvinciaEnvio);

            // Definir combos como "cerrados" (solo selección, sin escritura libre)
            cbEmpresaCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDimension.DropDownStyle = ComboBoxStyle.DropDownList;
            cbProvinciaEnvio.DropDownStyle = ComboBoxStyle.DropDownList;
            cbAgenciaRetiro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAgenciaEnvio.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        // Habilita el autocompletado en un ComboBox
        private void SetAutoComplete(ComboBox combo)
        {
            combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            combo.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        // ======================================================
        //   COMPORTAMIENTO DE LA INTERFAZ SEGÚN OPCIONES
        // ======================================================
        private void ToggleAgenciaControls()
        {
            // Habilita o deshabilita combos de agencias según el tipo seleccionado
            cbAgenciaRetiro.Enabled = rbRetiroAgencia.Checked;
            cmbAgenciaEnvio.Enabled = rbEnvioAgencia.Checked;
        }

        private void ToggleDireccionDestinatario()
        {
            // Si se elige "Envío a domicilio", se deben completar los campos de dirección
            bool requiereDireccion = rbEnvioDomicilio.Checked;
            txtLocalidadDestinatario.Enabled = requiereDireccion;
            txtDomicilioDestinatario.Enabled = requiereDireccion;

            // Si no se requiere dirección, se limpian los campos
            if (!requiereDireccion)
            {
                txtLocalidadDestinatario.Clear();
                txtDomicilioDestinatario.Clear();
            }

            // Ajusta el texto de las etiquetas (agrega * si son obligatorias)
            lblLocalidad.Text = requiereDireccion ? "Localidad *" : "Localidad";
            lblDomicilio.Text = requiereDireccion ? "Domicilio *" : "Domicilio";
        }

        // Bloquea cualquier caracter que no sea numérico en el campo DNI
        private void OnlyDigits_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        // Refresca visualmente la lista de encomiendas agregadas al pedido
        private void RefreshEncomiendasList()
        {
            lstEncomiendas.BeginUpdate();
            lstEncomiendas.Items.Clear();
            foreach (var e in _service.Encomiendas)
                lstEncomiendas.Items.Add($"{e.Dimension} x {e.Cantidad}");
            lstEncomiendas.EndUpdate();
        }

        // ======================================================
        //   MAPEOS: LEE LOS DATOS DE LA UI Y LOS PASA AL DOMINIO
        // ======================================================
        private PedidoHeader ReadHeaderFromUI()
        {
            // Obtiene los textos según los radio buttons seleccionados
            var retiro = rbRetiroDomicilio.Checked ? "Retiro en domicilio" :
                         rbRetiroAgencia.Checked ? "Retiro en agencia" : "";

            var envio = rbEnvioDomicilio.Checked ? "Envío a domicilio" :
                        rbEnvioCentroDistribucion.Checked ? "Envío a centro de distribución" :
                        rbEnvioAgencia.Checked ? "Envío a agencia" : "";

            // Crea el objeto PedidoHeader con los valores de la interfaz
            return new PedidoHeader
            {
                EmpresaCliente = cbEmpresaCliente.Text?.Trim(),
                RetiroTipo = retiro,
                AgenciaRetiro = rbRetiroAgencia.Checked ? cbAgenciaRetiro.Text?.Trim() : "",
                EnvioTipo = envio,
                ProvinciaEnvio = cbProvinciaEnvio.Text?.Trim(),
                AgenciaEnvio = rbEnvioAgencia.Checked ? cmbAgenciaEnvio.Text?.Trim() : "",
                DniDestinatario = Validaciones.SoloDigitos(txtDniDestinatario.Text), // DNI sin puntos ni letras
                LocalidadDestinatario = txtLocalidadDestinatario.Text?.Trim(),
                DomicilioDestinatario = txtDomicilioDestinatario.Text?.Trim()
            };
        }

        // Crea una instancia de Encomienda con los datos del bloque correspondiente
        private Encomienda ReadEncomiendaFromUI()
        {
            return new Encomienda
            {
                Dimension = cbDimension.Text?.Trim(),
                Cantidad = (int)numericCantidadEncomienda.Value
            };
        }

        // Limpia los campos del bloque de encomienda
        private void ResetEncomiendaUI()
        {
            if (cbDimension.Items.Count > 0) cbDimension.SelectedIndex = 0;
            numericCantidadEncomienda.Value = 1;
        }

        // Limpia todo el formulario para un nuevo pedido
        private void ResetFormUI()
        {
            if (cbEmpresaCliente.Items.Count > 0) cbEmpresaCliente.SelectedIndex = 0;

            rbRetiroDomicilio.Checked = true;
            cbAgenciaRetiro.SelectedIndex = cbAgenciaRetiro.Items.Count > 0 ? 0 : -1;

            rbEnvioDomicilio.Checked = true;
            if (cbProvinciaEnvio.Items.Count > 0) cbProvinciaEnvio.SelectedIndex = 0;
            cmbAgenciaEnvio.SelectedIndex = cmbAgenciaEnvio.Items.Count > 0 ? 0 : -1;

            txtDniDestinatario.Clear();
            txtLocalidadDestinatario.Clear();
            txtDomicilioDestinatario.Clear();

            ResetEncomiendaUI();
            ToggleAgenciaControls();
            ToggleDireccionDestinatario();
        }

        // ======================================================
        //   EVENTOS DE BOTONES
        // ======================================================

        // Guarda la encomienda actual en la lista temporal del pedido
        private void btnGuardarEncomienda_Click(object sender, EventArgs e)
        {
            var header = ReadHeaderFromUI();
            var (okHead, msgHead) = _service.ValidarHeader(header);
            if (!okHead)
            {
                MessageBox.Show(msgHead, "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var enc = ReadEncomiendaFromUI();
            var (okEnc, msgEnc) = _service.AgregarEncomienda(enc);
            if (!okEnc)
            {
                MessageBox.Show(msgEnc, "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            RefreshEncomiendasList();
            MessageBox.Show($"Encomienda agregada. Total: {_service.Encomiendas.Count}", "OK",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            ResetEncomiendaUI();
        }

        // Quita la encomienda seleccionada de la lista
        private void btnQuitarEncomienda_Click(object sender, EventArgs e)
        {
            if (lstEncomiendas.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccioná una encomienda de la lista para quitar.",
                                "Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var (ok, msg) = _service.QuitarEncomiendaEn(lstEncomiendas.SelectedIndex);
            if (!ok)
            {
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            RefreshEncomiendasList();
        }

        // Guarda el pedido completo en archivo CSV
        private void btnAceptarPedido_Click(object sender, EventArgs e)
        {
            var header = ReadHeaderFromUI();
            var result = _service.GuardarPedido(header);
            if (!result.ok)
            {
                MessageBox.Show(result.message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(
                result.message + Environment.NewLine +
                $"N° de guía: {result.guiaId}" + Environment.NewLine +
                $"Archivo: {_service.ObtenerRutaArchivo()}",
                "OK", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information
            );

            RefreshEncomiendasList(); // Se limpia la lista al guardar
            ResetFormUI();            // Se deja el form listo para un nuevo pedido
        }

        // Botón de volver al menú principal
        private void btnVolverMenuPrincipal_Click(object sender, EventArgs e)
        {
            FormUtils.VolverAlMenu(this);
        }

       
    }
}
