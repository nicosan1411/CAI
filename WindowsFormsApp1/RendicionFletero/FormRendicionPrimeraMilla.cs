using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormRendicionPrimeraMilla : Form
    {
        private readonly IGuiaMaster _master = new CsvGuiaMaster(); // usa data/guias_master.txt
        private readonly RendicionService _service;
        private readonly AssignmentService _assignService = new AssignmentService();

        private List<string> _retirosPendientes = new List<string>();   // lvRetirosDomicilioAdmitir (editable con check)
        private List<string> _entregasPendientes = new List<string>();  // lvEntregasDomicilioRealizadas (editable con check)

        public FormRendicionPrimeraMilla()
        {
            InitializeComponent();

            _service = new RendicionService(new CsvRendicionRepository("data", "guias_master.txt"));


            WireHandlers();
            InitCombos();
            InitListViews();
            CargarAsignacionesParaFletero();
        }

        private void WireHandlers()
        {
            cbFletero.SelectedIndexChanged += (_, __) => CargarAsignacionesParaFletero();
            try { btnGuardar.Click -= btnVolverMenuPrincipal_Click; } catch { }
            btnGuardar.Click += btnGuardar_Click;

            if (btnGenerarAsignaciones != null)
                btnGenerarAsignaciones.Click += (_, __) => GenerarAsignacionesYRefrescar();
        }

        private void InitCombos()
        {
            cbFletero.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFletero.Items.Clear();
            cbFletero.Items.AddRange(_service.ObtenerFleteros().ToArray());
            if (cbFletero.Items.Count > 0) cbFletero.SelectedIndex = -1;
        }

        private void InitListViews()
        {
            // Asegurar checks en las que son editables
            lvRetirosDomicilioAdmitir.CheckBoxes = true;
            lvEntregasDomicilioRealizadas.CheckBoxes = true;

            lvRetirosDomicilioAdmitir.FullRowSelect = true;
            lvEntregasDomicilioRealizadas.FullRowSelect = true;
            lvRetirarDomicilio.FullRowSelect = true;
            lvEntregasDomicilioARealizar.FullRowSelect = true;

            lvRetirosDomicilioAdmitir.MultiSelect = false;
            lvEntregasDomicilioRealizadas.MultiSelect = false;
            lvRetirarDomicilio.MultiSelect = false;
            lvEntregasDomicilioARealizar.MultiSelect = false;

            // Limpiar lo que dejó el Designer (items de ejemplo)
            lvRetirosDomicilioAdmitir.Items.Clear();
            lvEntregasDomicilioRealizadas.Items.Clear();
            lvRetirarDomicilio.Items.Clear();
            lvEntregasDomicilioARealizar.Items.Clear();
        }

        private string FleteroActual => cbFletero.Text?.Trim();

        private void CargarAsignacionesParaFletero()
        {
            var aRetirar = _master.ListarPorEstado(GuiaEstados.Impuesta);
            var retirosAdmitir = _master.ListarPorEstado(GuiaEstados.EnProcesoRetiro);
            var aEntregar = _master.ListarPorEstado(GuiaEstados.AdmitidaDestino);
            var entregasRealizadas = _master.ListarPorEstado(GuiaEstados.EnProcesoEntrega);

            // LV1 (editable): Retiros a domicilio a admitir
            lvRetirosDomicilioAdmitir.BeginUpdate();
            lvRetirosDomicilioAdmitir.Items.Clear();
            foreach (var g in retirosAdmitir) lvRetirosDomicilioAdmitir.Items.Add(new ListViewItem(new[] { "", g }));
            lvRetirosDomicilioAdmitir.EndUpdate();

            // LV2 (editable): Entregas a domicilio realizadas
            lvEntregasDomicilioRealizadas.BeginUpdate();
            lvEntregasDomicilioRealizadas.Items.Clear();
            foreach (var g in entregasRealizadas) lvEntregasDomicilioRealizadas.Items.Add(new ListViewItem(new[] { "", g }));
            lvEntregasDomicilioRealizadas.EndUpdate();

            // LV3 (info): A retirar en domicilio
            lvRetirarDomicilio.BeginUpdate();
            lvRetirarDomicilio.Items.Clear();
            foreach (var g in aRetirar) lvRetirarDomicilio.Items.Add(new ListViewItem(new[] { g }));
            lvRetirarDomicilio.EndUpdate();

            // LV4 (info): Entregas a domicilio a realizar
            lvEntregasDomicilioARealizar.BeginUpdate();
            lvEntregasDomicilioARealizar.Items.Clear();
            foreach (var g in aEntregar) lvEntregasDomicilioARealizar.Items.Add(new ListViewItem(new[] { g }));
            lvEntregasDomicilioARealizar.EndUpdate();
        }


        private void LimpiarListas()
        {
            lvRetirosDomicilioAdmitir.Items.Clear();
            lvEntregasDomicilioRealizadas.Items.Clear();
            lvRetirarDomicilio.Items.Clear();
            lvEntregasDomicilioARealizar.Items.Clear();
            _retirosPendientes.Clear();
            _entregasPendientes.Clear();
        }

        private void GenerarAsignacionesYRefrescar()
        {
            try
            {
                var fleteros = ComboData.Fleteros;
                if (fleteros == null || fleteros.Count == 0)
                {
                    MessageBox.Show("No hay fleteros configurados.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var (r, e) = _assignService.GenerarAsignacionesAleatorias(fleteros);
                MessageBox.Show($"Asignación generada.\nRetiros: {r}\nEntregas: {e}", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarAsignacionesParaFletero(); // refresca listas del fletero actual
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generando asignaciones: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static int FindGuiaColumnIndex(ListView lv)
        {
            for (int i = 0; i < lv.Columns.Count; i++)
            {
                var txt = (lv.Columns[i].Text ?? "").ToLowerInvariant();
                if (txt.Contains("guía") || txt.Contains("guia"))
                    return i;
            }
            return Math.Min(1, Math.Max(0, lv.Columns.Count - 1));
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbFletero.Text))
            {
                MessageBox.Show("Seleccioná un fletero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var guiaIdxRetiros = FindGuiaColumnIndex(lvRetirosDomicilioAdmitir);
            var guiaIdxEntregas = FindGuiaColumnIndex(lvEntregasDomicilioRealizadas);

            var retirosMarcados = lvRetirosDomicilioAdmitir.Items
                .Cast<ListViewItem>()
                .Where(i => i.Checked)
                .Select(i => i.SubItems[guiaIdxRetiros].Text.Trim())
                .ToList();

            var entregasMarcadas = lvEntregasDomicilioRealizadas.Items
                .Cast<ListViewItem>()
                .Where(i => i.Checked)
                .Select(i => i.SubItems[guiaIdxEntregas].Text.Trim())
                .ToList();

            // Transiciones definidas para este form:
            if (retirosMarcados.Count > 0)
            {
                // Retiros a domicilio a admitir: En proceso de retiro -> Admitida en CD origen
                _master.CambiarEstado(retirosMarcados, GuiaEstados.AdmitidaOrigen);
            }

            if (entregasMarcadas.Count > 0)
            {
                // Entregas a domicilio realizadas: En proceso de entrega -> Entregada a cliente
                _master.CambiarEstado(entregasMarcadas, GuiaEstados.EntregadaCliente);
            }

            // ... ya mostraste el MessageBox "Datos guardados", ahora refrescá:
            CargarAsignacionesParaFletero();

            if (retirosMarcados.Count == 0 && entregasMarcadas.Count == 0)
            {
                var r = MessageBox.Show(
                    "No se seleccionó ninguna guía de retiro ni entrega realizada.\n" +
                    "De continuar, el fletero no cumplió con ninguna obligación.\n\n" +
                    "¿Desea guardar igualmente?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
                if (r == DialogResult.No) return;
            }
            else if (retirosMarcados.Count > 0 && entregasMarcadas.Count == 0)
            {
                var r = MessageBox.Show(
                    "No se seleccionó ninguna entrega a domicilio realizada.\n\n" +
                    "¿Desea guardar igualmente?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
                if (r == DialogResult.No) return;
            }
            else if (retirosMarcados.Count == 0 && entregasMarcadas.Count > 0)
            {
                var r = MessageBox.Show(
                    "No se seleccionó ningún retiro a domicilio a admitir.\n\n" +
                    "¿Desea guardar igualmente?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
                if (r == DialogResult.No) return;
            }

            var master = new CsvGuiaMaster();

            // Retiros a domicilio a admitir: En proceso de retiro -> Admitida en CD origen
            if (retirosMarcados.Count > 0)
                master.CambiarEstado(retirosMarcados, GuiaEstados.AdmitidaOrigen);

            // Entregas a domicilio realizadas: En proceso de entrega -> Entregada a cliente
            if (entregasMarcadas.Count > 0)
                master.CambiarEstado(entregasMarcadas, GuiaEstados.EntregadaCliente);

            CargarAsignacionesParaFletero();

            var confirm = MessageBox.Show(
                "¿Está seguro de que desea guardar los cambios realizados?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (confirm == DialogResult.No) return;

            try
            {
                var fletero = cbFletero.Text.Trim();
                _service.Guardar(fletero, retirosMarcados, entregasMarcadas);

                MessageBox.Show(
                    "Datos guardados.\n\n" +
                    $"Archivo: {_service.ObtenerRutaArchivo()}",
                    "OK",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Si existen archivos de asignación → recargo desde disco (ya no aparecerán las guías rendidas)
                if (_service.HayArchivosAsignacion(fletero))
                {
                    CargarAsignacionesParaFletero();
                }
                else
                {
                    // Modo muestra: al menos limpiá los checks
                    foreach (ListViewItem it in lvRetirosDomicilioAdmitir.Items) it.Checked = false;
                    foreach (ListViewItem it in lvEntregasDomicilioRealizadas.Items) it.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolverMenuPrincipal_Click(object sender, EventArgs e)
        {
            FormUtils.VolverAlMenu(this);
        }
    }
}
