using CAI_Proyecto.Forms.Inicio;
using CAI_Proyecto.Forms.Operacion.AdmitirEnCD.Model;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CAI_Proyecto.Forms.Operacion.AdmitirEnCD.Forms
{
    public partial class AdmitirEnCDForm : Form
    {
        internal AdmitirEnCDModel modelo = new AdmitirEnCDModel();

        public AdmitirEnCDForm()
        {
            InitializeComponent();
            InicializarFormulario();
            ConectarEventos();
        }

        private void InicializarFormulario()
        {
            // Cliente
            cbEmpresaCliente.DataSource = modelo.Clientes;
            cbEmpresaCliente.DisplayMember = "Cuit_RazonSocial";
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

            // Agencia de envío (se cargarán al elegir provincia)
            cmbAgenciaEnvio.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAgenciaEnvio.Enabled = false;
            cmbAgenciaEnvio.DataSource = null;

            // Radio por defecto
            rbEnvioDomicilio.Checked = true;

            // Destinatario
            ActualizarCamposDestinatario();

            // ListBox de encomiendas
            lstEncomiendas.DisplayMember = nameof(EncomiendaItem.Dimension_Cantidad);

            // DNI destinatario
            txtDniDestinatario.MaxLength = 8;
            txtDniDestinatario.KeyPress += SoloDigitos_KeyPress;
        }

        private void SoloDigitos_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo dígitos y teclas de control (como retroceso)
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

            // Envio
            rbEnvioDomicilio.CheckedChanged += (_, __) => { ActualizarHabilitacionAgencias(); ActualizarCamposDestinatario(); };
            rbEnvioCentroDistribucion.CheckedChanged += (_, __) => { ActualizarHabilitacionAgencias(); ActualizarCamposDestinatario(); };
            rbEnvioAgencia.CheckedChanged += (_, __) => { ActualizarHabilitacionAgencias(); ActualizarCamposDestinatario(); };

            // Botones
            btnGuardarEncomienda.Click += BtnGuardarEncomienda_Click;
            btnQuitarEncomienda.Click += BtnQuitarEncomienda_Click;
            btnAdmitirEncomienda.Click += BtnAdmitirEncomienda_Click;
        }

        // ============ Habilitar / deshabilitar según radios ============
        private void ActualizarHabilitacionAgencias()
        {
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
                Encomiendas = modelo.Encomiendas.ToList()
            };

            return pedido;
        }

        private void RefrescarListaEncomiendas()
        {
            lstEncomiendas.BeginUpdate();
            try
            {
                lstEncomiendas.DataSource = null;
                lstEncomiendas.DataSource = modelo.Encomiendas;
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

            modelo.AgregarEncomienda(enc);
            RefrescarListaEncomiendas();
        }

        private void BtnQuitarEncomienda_Click(object sender, EventArgs e)
        {
            if (lstEncomiendas.SelectedItem is not EncomiendaItem encomiendaSeleccionada)
            {
                MessageBox.Show("Seleccioná una encomienda para quitar.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            modelo.QuitarEncomienda(encomiendaSeleccionada);
            RefrescarListaEncomiendas();
        }

        private void BtnAdmitirEncomienda_Click(object sender, EventArgs e)
        {
            // Validaciones de formulario mínimas
            if (cbEmpresaCliente.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccioná un cliente.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (modelo.Encomiendas.Count == 0)
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

            MessageBox.Show("Pedido admitido correctamente (demo).", "OK",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            modelo.Encomiendas.Clear();
            lstEncomiendas.DataSource = null;

            cbEmpresaCliente.SelectedIndex = -1;
            cbProvinciaEnvio.SelectedIndex = -1;

            txtDniDestinatario.Clear();
            txtLocalidadDestinatario.Clear();
            txtDomicilioDestinatario.Clear();

            cbDimension.SelectedIndex = -1;
            numericCantidadEncomienda.Value = 1;

            ActualizarHabilitacionAgencias();
            ActualizarCamposDestinatario();
        }

        // Botón de volver al menú principal
        private void btnVolverMenuPrincipal_Click(object sender, EventArgs e)
        {
            InicioForm.VolverAlMenu(this);
        }
    }
}
