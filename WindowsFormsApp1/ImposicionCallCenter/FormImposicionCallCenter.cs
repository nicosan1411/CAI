using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormImposicionCallCenter : Form
    {
        // === Configuración de almacenamiento local ===
        // Guardamos los datos en un archivo CSV simple (separador ';') dentro de una carpeta "data"
        // ubicada al lado del .exe. Esto evita dependencias externas y sirve para todas las pantallas.
        private const string DATA_DIR = "data";
        private const string FILE_NAME = "imposiciones_callcenter.txt";
        private string FilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DATA_DIR, FILE_NAME);

        // Lista en memoria con las encomiendas del pedido actual (patrón maestro–detalle).
        // El usuario puede agregar varias encomiendas antes de "Aceptar pedido".
        private readonly List<Encomienda> encomiendas = new List<Encomienda>();

        public FormImposicionCallCenter()
        {
            InitializeComponent();

            // En lugar de cablear eventos en el diseñador, centralizamos acá el comportamiento dinámico
            // (excepto botones a pedido del equipo). Esto facilita el mantenimiento.
            WireHandlers();

            // Cargamos listas cerradas en los ComboBox (empresas, dimensiones, provincias, agencias).
            InitCombos();

            // Ajustamos la UI inicial según selección de retiro/envío.
            ToggleAgenciaControls();
            ToggleDireccionDestinatario();
        }

        // =============================================================================
        // ===============               ARCHIVO / CSV                  ===============
        // =============================================================================

        // Crea el archivo si no existe y escribe el header (una sola vez).
        private void EnsureFileWithHeader()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));

            if (!File.Exists(FilePath) || new FileInfo(FilePath).Length == 0)
            {
                var header = string.Join(";",
                    "Id", "FechaHora", "EmpresaCliente",
                    "RetiroTipo", "AgenciaRetiro",
                    "EnvioTipo", "ProvinciaEnvio", "AgenciaEnvio",
                    "DniDestinatario", "LocalidadDestinatario", "DomicilioDestinatario",
                    "Dimension", "Cantidad"
                );
                File.AppendAllText(FilePath, header + Environment.NewLine, Encoding.UTF8);
            }
        }

        // Escapa valores para CSV: envuelve en comillas y duplica comillas internas.
        private static string CsvEscape(string s)
        {
            if (s == null) return "\"\"";
            var escaped = s.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }

        // Agrega (append) una línea al CSV con todos los campos del registro.
        private void AppendRecord(ImposicionRecord r)
        {
            EnsureFileWithHeader();

            var line = string.Join(";",
                CsvEscape(r.Id),
                CsvEscape(r.FechaHora.ToString("yyyy-MM-dd HH:mm:ss")),
                CsvEscape(r.EmpresaCliente),
                CsvEscape(r.RetiroTipo),
                CsvEscape(r.AgenciaRetiro ?? ""),
                CsvEscape(r.EnvioTipo),
                CsvEscape(r.ProvinciaEnvio),
                CsvEscape(r.AgenciaEnvio ?? ""),
                CsvEscape(r.DniDestinatario),
                CsvEscape(r.LocalidadDestinatario),
                CsvEscape(r.DomicilioDestinatario),
                CsvEscape(r.Dimension),
                CsvEscape(r.Cantidad.ToString())
            );

            File.AppendAllText(FilePath, line + Environment.NewLine, Encoding.UTF8);
        }

        // =============================================================================
        // ===============            INICIALIZACIÓN / UI               ===============
        // =============================================================================

        // Eventos que dependen de la UI dinámica (mostrar/ocultar o habilitar/inhabilitar).
        private void WireHandlers()
        {
            // Radios de Retiro: habilitan el combo de agencia de retiro si corresponde.
            rbRetiroDomicilio.CheckedChanged += (_, __) => ToggleAgenciaControls();
            rbRetiroAgencia.CheckedChanged += (_, __) => ToggleAgenciaControls();

            // Radios de Envío: además de agencias, controlan si Localidad/Domicilio son requeridos.
            rbEnvioDomicilio.CheckedChanged += (_, __) => { ToggleAgenciaControls(); ToggleDireccionDestinatario(); };
            rbEnvioCentroDistribucion.CheckedChanged += (_, __) => { ToggleAgenciaControls(); ToggleDireccionDestinatario(); };
            rbEnvioAgencia.CheckedChanged += (_, __) => { ToggleAgenciaControls(); ToggleDireccionDestinatario(); };
        }

        // Carga de listas cerradas en ComboBox y estilos para evitar valores arbitrarios.
        private void InitCombos()
        {
            // Empresa / Cliente
            cbEmpresaCliente.Items.Clear();
            cbEmpresaCliente.Items.AddRange(new object[] { "Empresa 1", "Empresa 2", "Empresa 3" });

            // Dimensión de encomienda (lista cerrada)
            cbDimension.Items.Clear();
            cbDimension.Items.AddRange(new object[] { "XS", "S", "M", "L", "XL" });

            // Provincias (lista cerrada y extensa)
            cbProvinciaEnvio.DataSource = new[]
            {
                "Buenos Aires","CABA","Catamarca","Chaco","Chubut","Córdoba","Corrientes",
                "Entre Ríos","Formosa","Jujuy","La Pampa","La Rioja","Mendoza","Misiones",
                "Neuquén","Río Negro","Salta","San Juan","San Luis","Santa Cruz","Santa Fe",
                "Santiago del Estero","Tierra del Fuego","Tucumán"
            }.ToList();

            // Agencias (ejemplo)
            cbAgenciaRetiro.Items.Clear();
            cbAgenciaRetiro.Items.AddRange(new object[] { "Agencia 1", "Agencia 2", "Agencia 3" });

            cmbAgenciaEnvio.Items.Clear();
            cmbAgenciaEnvio.Items.AddRange(new object[] { "Agencia A", "Agencia B", "Agencia C" });

            // Defaults razonables para minimizar clics.
            if (cbEmpresaCliente.Items.Count > 0) cbEmpresaCliente.SelectedIndex = 0;
            if (cbDimension.Items.Count > 0) cbDimension.SelectedIndex = 0;
            if (cbProvinciaEnvio.Items.Count > 0) cbProvinciaEnvio.SelectedIndex = 0;

            rbRetiroDomicilio.Checked = true;
            rbEnvioDomicilio.Checked = true;

            // Autocompletado útil para listas largas (provincias).
            SetAutoComplete(cbProvinciaEnvio);

            // Evitar que el usuario tipeé valores fuera del catálogo (listas cerradas).
            cbEmpresaCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDimension.DropDownStyle = ComboBoxStyle.DropDownList;
            cbProvinciaEnvio.DropDownStyle = ComboBoxStyle.DropDownList;
            cbAgenciaRetiro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAgenciaEnvio.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        // Habilita el autocompletado sobre los ítems cargados del combo.
        private void SetAutoComplete(ComboBox combo)
        {
            combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            combo.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        // Habilita/deshabilita combos de agencia según tipo de retiro/envío.
        private void ToggleAgenciaControls()
        {
            cbAgenciaRetiro.Enabled = rbRetiroAgencia.Checked;
            cmbAgenciaEnvio.Enabled = rbEnvioAgencia.Checked;
        }

        // Si el envío NO es a domicilio, los campos de dirección dejan de ser requeridos
        // (se griséan y se limpian para evitar "basura" accidental).
        private void ToggleDireccionDestinatario()
        {
            bool requiereDireccion = rbEnvioDomicilio.Checked;

            txtLocalidadDestinatario.Enabled = requiereDireccion;
            txtDomicilioDestinatario.Enabled = requiereDireccion;

            if (!requiereDireccion)
            {
                txtLocalidadDestinatario.Clear();
                txtDomicilioDestinatario.Clear();
            }

            // Pista visual: agregamos asterisco (*) cuando son obligatorios.
            lblLocalidad.Text = requiereDireccion ? "Localidad *" : "Localidad";
            lblDomicilio.Text = requiereDireccion ? "Domicilio *" : "Domicilio";
        }

        // Refresca el ListBox con las encomiendas acumuladas (detalle del pedido).
        private void RefreshEncomiendasList()
        {
            lstEncomiendas.BeginUpdate();
            lstEncomiendas.Items.Clear();

            // Formato simple: "Tamaño x Cantidad" (ej: "M x 3").
            foreach (var e in encomiendas)
                lstEncomiendas.Items.Add($"{e.Dimension} x {e.Cantidad}");

            lstEncomiendas.EndUpdate();
        }

        // =============================================================================
        // ===============           VALIDACIONES / MODELO              ===============
        // =============================================================================

        // Valida SOLO los datos generales del pedido (maestro).
        // La encomienda actual se valida aparte cuando se pulsa "Guardar encomienda".
        private (bool ok, string message) ValidatePedidoCamposGenerales()
        {
            var errors = new List<string>();

            // Empresa
            if (cbEmpresaCliente.SelectedItem == null || string.IsNullOrWhiteSpace(cbEmpresaCliente.Text))
                errors.Add("Seleccioná una Empresa/Cliente.");

            // Retiro
            if (!rbRetiroDomicilio.Checked && !rbRetiroAgencia.Checked)
                errors.Add("Seleccioná el tipo de retiro.");
            if (rbRetiroAgencia.Checked && string.IsNullOrWhiteSpace(cbAgenciaRetiro.Text))
                errors.Add("Seleccioná la agencia de retiro.");

            // Envío
            if (!rbEnvioDomicilio.Checked && !rbEnvioCentroDistribucion.Checked && !rbEnvioAgencia.Checked)
                errors.Add("Seleccioná el tipo de envío.");
            if (cbProvinciaEnvio.SelectedItem == null || string.IsNullOrWhiteSpace(cbProvinciaEnvio.Text))
                errors.Add("Seleccioná la provincia de envío.");
            if (rbEnvioAgencia.Checked && string.IsNullOrWhiteSpace(cmbAgenciaEnvio.Text))
                errors.Add("Seleccioná la agencia de envío.");

            // Destinatario
            if (string.IsNullOrWhiteSpace(txtDniDestinatario.Text))
                errors.Add("Ingresá el DNI del destinatario.");

            // Localidad/Domicilio solo si el envío es a domicilio.
            bool requiereDireccion = rbEnvioDomicilio.Checked;
            if (requiereDireccion)
            {
                if (string.IsNullOrWhiteSpace(txtLocalidadDestinatario.Text))
                    errors.Add("Ingresá la localidad del destinatario (Envío a domicilio).");
                if (string.IsNullOrWhiteSpace(txtDomicilioDestinatario.Text))
                    errors.Add("Ingresá el domicilio del destinatario (Envío a domicilio).");
            }

            return errors.Any()
                ? (false, string.Join(Environment.NewLine, errors))
                : (true, "OK");
        }

        // Valida la encomienda ACTUAL (detalle) antes de agregarla a la lista.
        private (bool ok, string message) ValidateEncomiendaActual()
        {
            if (cbDimension.SelectedItem == null || string.IsNullOrWhiteSpace(cbDimension.Text))
                return (false, "Seleccioná la dimensión de la encomienda.");
            if (numericCantidadEncomienda.Value < 1)
                return (false, "La cantidad debe ser al menos 1.");
            return (true, "OK");
        }

        // Construye un "snapshot" del pedido desde los controles actuales.
        // NOTA: Dimension/Cantidad se sobreescriben por cada encomienda de la lista al guardar.
        private ImposicionRecord BuildRecord()
        {
            var retiroTipo = rbRetiroDomicilio.Checked ? "Retiro en domicilio" :
                             rbRetiroAgencia.Checked ? "Retiro en agencia" : "";

            var envioTipo = rbEnvioDomicilio.Checked ? "Envío a domicilio" :
                            rbEnvioCentroDistribucion.Checked ? "Envío a centro de distribución" :
                            rbEnvioAgencia.Checked ? "Envío a agencia" : "";

            return new ImposicionRecord
            {
                Id = Guid.NewGuid().ToString("N"), // Se reemplaza por un Id común al aceptar el pedido
                FechaHora = DateTime.Now,
                EmpresaCliente = cbEmpresaCliente.Text?.Trim(),
                RetiroTipo = retiroTipo,
                AgenciaRetiro = rbRetiroAgencia.Checked ? cbAgenciaRetiro.Text?.Trim() : "",
                EnvioTipo = envioTipo,
                ProvinciaEnvio = cbProvinciaEnvio.Text?.Trim(),
                AgenciaEnvio = rbEnvioAgencia.Checked ? cmbAgenciaEnvio.Text?.Trim() : "",
                DniDestinatario = txtDniDestinatario.Text?.Trim(),
                LocalidadDestinatario = txtLocalidadDestinatario.Text?.Trim(),
                DomicilioDestinatario = txtDomicilioDestinatario.Text?.Trim(),
                Dimension = cbDimension.Text?.Trim(),                     // sobreescribe cada encomienda
                Cantidad = (int)numericCantidadEncomienda.Value           // sobreescribe cada encomienda
            };
        }

        // =============================================================================
        // ===============                 EVENTOS                     ===============
        // =============================================================================

        // Guarda UNA encomienda en la lista (no persiste en archivo todavía).
        private void btnGuardarEncomienda_Click(object sender, EventArgs e)
        {
            // 1) Validar maestro (pedido)
            var (okPedido, msgPedido) = ValidatePedidoCamposGenerales();
            if (!okPedido)
            {
                MessageBox.Show(msgPedido, "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2) Validar detalle (encomienda actual)
            var (okEnc, msgEnc) = ValidateEncomiendaActual();
            if (!okEnc)
            {
                MessageBox.Show(msgEnc, "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3) Agregar a la lista en memoria
            var encomienda = new Encomienda
            {
                Dimension = cbDimension.Text.Trim(),
                Cantidad = (int)numericCantidadEncomienda.Value
            };
            encomiendas.Add(encomienda);

            MessageBox.Show(
                $"Encomienda agregada. Total acumuladas: {encomiendas.Count}",
                "OK",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            // 4) Refrescar lista y dejar listo para otra carga
            RefreshEncomiendasList();
            if (cbDimension.Items.Count > 0) cbDimension.SelectedIndex = 0;
            numericCantidadEncomienda.Value = 1;
        }

        // Quita la encomienda seleccionada del ListBox y refresca la vista.
        private void btnQuitarEncomienda_Click(object sender, EventArgs e)
        {
            if (lstEncomiendas.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccioná una encomienda de la lista para quitar.",
                                "Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var idx = lstEncomiendas.SelectedIndex;
            if (idx >= 0 && idx < encomiendas.Count)
            {
                encomiendas.RemoveAt(idx);
                RefreshEncomiendasList();
            }
        }

        // Acepta el pedido: persiste todas las encomiendas acumuladas con el mismo Id de pedido.
        private void btnAceptarPedido_Click(object sender, EventArgs e)
        {
            // Ya se validó maestro + detalle al momento de guardar cada encomienda.
            if (encomiendas.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos una encomienda antes de aceptar el pedido.",
                    "Sin encomiendas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Confirmás el pedido con {encomiendas.Count} encomienda(s)?",
                "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                // Un único Id para todas las líneas del mismo pedido → facilita agrupado en reportes.
                var idPedido = Guid.NewGuid().ToString("N");

                foreach (var enc in encomiendas)
                {
                    var record = BuildRecord();
                    record.Id = idPedido;           // mismo Id para todas las encomiendas del pedido
                    record.Dimension = enc.Dimension;
                    record.Cantidad = enc.Cantidad;
                    AppendRecord(record);
                }

                MessageBox.Show("Pedido aceptado y guardado con todas las encomiendas.",
                    "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiamos estado y UI para el siguiente pedido.
                encomiendas.Clear();
                RefreshEncomiendasList();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error guardando archivo:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Restablece el formulario a sus valores por defecto (sin tocar el archivo).
        private void LimpiarFormulario()
        {
            // Empresa
            if (cbEmpresaCliente.Items.Count > 0) cbEmpresaCliente.SelectedIndex = 0;

            // Retiro
            rbRetiroDomicilio.Checked = true;
            cbAgenciaRetiro.SelectedIndex = cbAgenciaRetiro.Items.Count > 0 ? 0 : -1;

            // Envío
            rbEnvioDomicilio.Checked = true;
            if (cbProvinciaEnvio.Items.Count > 0) cbProvinciaEnvio.SelectedIndex = 0;
            cmbAgenciaEnvio.SelectedIndex = cmbAgenciaEnvio.Items.Count > 0 ? 0 : -1;

            // Destinatario
            txtDniDestinatario.Clear();
            txtLocalidadDestinatario.Clear();
            txtDomicilioDestinatario.Clear();

            // Encomienda (valores iniciales de carga)
            if (cbDimension.Items.Count > 0) cbDimension.SelectedIndex = 0;
            numericCantidadEncomienda.Value = 1;

            // Reaplicar lógicas de habilitación/visibilidad.
            ToggleAgenciaControls();
            ToggleDireccionDestinatario();
        }

        // Navegación a menú principal (provisto por utilitaria del proyecto).
        private void btnVolverMenuPrincipal_Click(object sender, EventArgs e)
        {
            FormUtils.VolverAlMenu(this);
        }
    }

    // =============================================================================
    // ===============                 MODELOS                      ===============
    // =============================================================================

    // Representa una "línea" del CSV (pedido + una encomienda).
    // NOTA: un mismo Id puede repetirse en varias filas (una por encomienda) → maestro–detalle.
    internal class ImposicionRecord
    {
        public string Id { get; set; }
        public DateTime FechaHora { get; set; }
        public string EmpresaCliente { get; set; }
        public string RetiroTipo { get; set; }
        public string AgenciaRetiro { get; set; }
        public string EnvioTipo { get; set; }
        public string ProvinciaEnvio { get; set; }
        public string AgenciaEnvio { get; set; }
        public string DniDestinatario { get; set; }
        public string LocalidadDestinatario { get; set; }
        public string DomicilioDestinatario { get; set; }
        public string Dimension { get; set; }
        public int Cantidad { get; set; }
    }

    // Encomienda "temporal" que vive en memoria hasta aceptar el pedido.
    internal class Encomienda
    {
        public string Dimension { get; set; }
        public int Cantidad { get; set; }
    }
}
