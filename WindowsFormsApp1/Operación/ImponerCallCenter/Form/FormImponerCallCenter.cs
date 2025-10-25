using System;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.Inicio;

namespace WindowsFormsApp1.Operación.ImponerCallCenter.Model
{
    public partial class FormImponerCallCenter : Form
    {
        // Modelo del formulario (negocio + datos para combos)
        internal FormImponerCallCenterModelo modelo = new FormImponerCallCenterModelo();

        // Lista temporal de encomiendas que se ve en el ListBox
        private readonly System.Collections.Generic.List<EncomiendaItem> _encomiendasTemp
            = new System.Collections.Generic.List<EncomiendaItem>();

        public FormImponerCallCenter()
        {
            InitializeComponent();
            InicializarFormulario();
            ConectarEventos();
        }

        // ============ Inicialización de la UI ============
        private void InicializarFormulario()
        {
            // Cliente
            cbEmpresaCliente.DataSource = modelo.Clientes;
            cbEmpresaCliente.DisplayMember = "Cuit_RazonSocial"; // (Cuit) RazonSocial
            cbEmpresaCliente.SelectedIndex = -1;
            cbEmpresaCliente.DropDownStyle = ComboBoxStyle.DropDownList;

            // Provincia de envío
            cbProvinciaEnvio.DataSource = modelo.Provincias;
            cbProvinciaEnvio.DisplayMember = "Nombre";
            cbProvinciaEnvio.SelectedIndex = -1;
            cbProvinciaEnvio.DropDownStyle = ComboBoxStyle.DropDownList;

            // Dimensión (clase con propiedad Tamaño)
            cbDimension.DataSource = modelo.Dimensiones;
            cbDimension.DisplayMember = "Tamaño";
            cbDimension.SelectedIndex = -1;
            cbDimension.DropDownStyle = ComboBoxStyle.DropDownList;

            // Agencia de retiro
            cbAgenciaRetiro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbAgenciaRetiro.Enabled = false;
            cbAgenciaRetiro.DataSource = modelo.TodasLasAgenciasDeRetiro().ToList();
            cbAgenciaRetiro.DisplayMember = "Nombre";
            cbAgenciaRetiro.SelectedIndex = -1;

            // Agencia de envío (se cargarán al elegir provincia)
            cmbAgenciaEnvio.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAgenciaEnvio.Enabled = false;
            cmbAgenciaEnvio.DataSource = null;

            // Radios por defecto
            rbRetiroDomicilio.Checked = true;
            rbEnvioDomicilio.Checked = true;

            // Destinatario
            ActualizarCamposDestinatario();

            // ListBox de encomiendas
            lstEncomiendas.DisplayMember = nameof(EncomiendaItem.Dimension_Cantidad);

            // === DNI DESTINATARIO ===
            txtDniDestinatario.MaxLength = 8;
            txtDniDestinatario.KeyPress += SoloDigitos_KeyPress;
        }

        private void SoloDigitos_KeyPress(object sender, KeyPressEventArgs e)
        {
            // permite sólo números y teclas de control (backspace, etc.)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        // ============ Conexión de eventos ============
        private void ConectarEventos()
        {
            // Provincia -> recarga agencias (envío y retiro)
            cbProvinciaEnvio.SelectedIndexChanged += (_, __) =>
            {
                var prov = cbProvinciaEnvio.SelectedItem as Provincia;
                var listaEnvio = prov == null
                    ? Enumerable.Empty<AgenciaEnvio>()
                    : modelo.AgenciasEnvioPorProvincia(prov.Codigo).ToList();

                cmbAgenciaEnvio.DataSource = listaEnvio;
                cmbAgenciaEnvio.DisplayMember = nameof(AgenciaEnvio.Nombre);
                cmbAgenciaEnvio.SelectedIndex = -1;
            };

            // Retiro
            rbRetiroDomicilio.CheckedChanged += (_, __) => ActualizarHabilitacionAgencias();
            rbRetiroAgencia.CheckedChanged += (_, __) => ActualizarHabilitacionAgencias();

            // Envío
            rbEnvioDomicilio.CheckedChanged += (_, __) => { ActualizarHabilitacionAgencias(); ActualizarCamposDestinatario(); };
            rbEnvioCentroDistribucion.CheckedChanged += (_, __) => { ActualizarHabilitacionAgencias(); ActualizarCamposDestinatario(); };
            rbEnvioAgencia.CheckedChanged += (_, __) => { ActualizarHabilitacionAgencias(); ActualizarCamposDestinatario(); };

            // Botones
            btnGuardarEncomienda.Click += BtnGuardarEncomienda_Click;
            btnQuitarEncomienda.Click += BtnQuitarEncomienda_Click;
            btnAceptarPedido.Click += BtnAceptarPedido_Click;
        }

        // ============ Habilitar / deshabilitar según radios ============
        private void ActualizarHabilitacionAgencias()
        {
            cbAgenciaRetiro.Enabled = rbRetiroAgencia.Checked;
            cmbAgenciaEnvio.Enabled = rbEnvioAgencia.Checked;
        }

        private void ActualizarCamposDestinatario()
        {
            bool requiereDireccion = rbEnvioDomicilio.Checked;

            txtLocalidadDestinatario.Enabled = requiereDireccion;
            txtDomicilioDestinatario.Enabled = requiereDireccion;

            if (!requiereDireccion)
            {
                txtLocalidadDestinatario.Clear();
                txtDomicilioDestinatario.Clear();
            }

            lblLocalidad.Text = requiereDireccion ? "Localidad *" : "Localidad";
            lblDomicilio.Text = requiereDireccion ? "Domicilio *" : "Domicilio";
        }

        // ============ Helpers UI ============
        private EncomiendaItem LeerEncomiendaDeLaUI()
        {
            var dim = cbDimension.SelectedItem as Dimension; // clase con Tamaño
            if (dim == null) return null;

            return new EncomiendaItem
            {
                Dimension = dim,
                Cantidad = (int)numericCantidadEncomienda.Value
            };
        }

        private Pedido ConstruirPedidoDesdeUI()
        {
            var pedido = new Pedido
            {
                // Cliente
                Cliente = cbEmpresaCliente.SelectedItem as Cliente,

                // Retiro
                TipoRetiro = rbRetiroAgencia.Checked ? "Agencia"
                                : rbRetiroDomicilio.Checked ? "Domicilio" : null,
                AgenciaRetiro = rbRetiroAgencia.Checked ? cbAgenciaRetiro.SelectedItem as AgenciaRetiro : null,

                // Envío
                TipoEnvio = rbEnvioAgencia.Checked ? "Agencia"
                                : rbEnvioCentroDistribucion.Checked ? "Centro de distribución"
                                : rbEnvioDomicilio.Checked ? "Domicilio" : null,
                ProvinciaEnvio = cbProvinciaEnvio.SelectedItem as Provincia,
                AgenciaEnvio = rbEnvioAgencia.Checked ? cmbAgenciaEnvio.SelectedItem as AgenciaEnvio : null,

                // Destinatario
                DniDestinatario = txtDniDestinatario.Text?.Trim(),
                LocalidadDestinatario = txtLocalidadDestinatario.Text?.Trim(),
                DomicilioDestinatario = txtDomicilioDestinatario.Text?.Trim(),

                // Encomiendas
                Encomiendas = _encomiendasTemp.ToList()
            };

            return pedido;
        }

        private void RefrescarListaEncomiendas()
        {
            lstEncomiendas.BeginUpdate();
            try
            {
                lstEncomiendas.DataSource = null;
                lstEncomiendas.DataSource = _encomiendasTemp;
                lstEncomiendas.DisplayMember = nameof(EncomiendaItem.Dimension_Cantidad);
            }
            finally
            {
                lstEncomiendas.EndUpdate();
            }
        }

        // ============ Botones ============
        private void BtnGuardarEncomienda_Click(object sender, EventArgs e)
        {
            // Validaciones de formulario
            if (cbDimension.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccioná una dimensión.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (numericCantidadEncomienda.Value <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a 0.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var enc = LeerEncomiendaDeLaUI();
            if (enc == null)
            {
                MessageBox.Show("Completá la encomienda.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _encomiendasTemp.Add(enc);
            RefrescarListaEncomiendas();
        }

        private void BtnQuitarEncomienda_Click(object sender, EventArgs e)
        {
            var idx = lstEncomiendas.SelectedIndex;
            if (idx < 0)
            {
                MessageBox.Show("Seleccioná una encomienda para quitar.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _encomiendasTemp.RemoveAt(idx);
            RefrescarListaEncomiendas();
        }

        private void BtnAceptarPedido_Click(object sender, EventArgs e)
        {
            // Validaciones de formulario mínimas
            if (cbEmpresaCliente.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccioná un cliente.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_encomiendasTemp.Count == 0)
            {
                MessageBox.Show("Agregá al menos una encomienda.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var pedido = ConstruirPedidoDesdeUI();

            // Reglas de negocio del modelo
            var errores = modelo.ValidarPedido(pedido); // método del modelo
            if (errores.Any())
            {
                var msg = string.Join(Environment.NewLine, errores);
                MessageBox.Show(msg, "Errores de negocio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Pedido impuesto correctamente (demo).", "OK",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            _encomiendasTemp.Clear();
            lstEncomiendas.DataSource = null;

            cbEmpresaCliente.SelectedIndex = -1;
            cbProvinciaEnvio.SelectedIndex = -1;

            cbAgenciaRetiro.SelectedIndex = -1;
            cmbAgenciaEnvio.DataSource = null;

            rbRetiroDomicilio.Checked = true;
            rbEnvioDomicilio.Checked = true;

            txtDniDestinatario.Clear();
            txtLocalidadDestinatario.Clear();
            txtDomicilioDestinatario.Clear();

            cbDimension.SelectedIndex = -1;
            numericCantidadEncomienda.Value = 1;

            ActualizarHabilitacionAgencias();
            ActualizarCamposDestinatario();
        }

        private void btnVolverMenuPrincipal_Click(object sender, EventArgs e)
        {
            FormInicio.VolverAlMenu(this);
        }
    }
}
