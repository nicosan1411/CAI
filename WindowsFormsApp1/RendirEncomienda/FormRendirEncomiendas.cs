using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormRendirEncomiendas : Form
    {
        private readonly RendicionService _service;
        private readonly AssignmentService _assignService = new AssignmentService();

        private List<string> _retirosPendientes = new List<string>();   // lvRetirosDomicilioAdmitir (editable con check)
        private List<string> _entregasPendientes = new List<string>();  // lvEntregasDomicilioRealizadas (editable con check)

        public FormRendirEncomiendas()
        {
            InitializeComponent();

            _service = new RendicionService(new CsvRendicionRepository());


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

            lvRetirosDomicilioAdmitir.MultiSelect = false;
            lvEntregasDomicilioRealizadas.MultiSelect = false;

            // Limpiar lo que dejó el Designer (items de ejemplo)
            lvRetirosDomicilioAdmitir.Items.Clear();
            lvEntregasDomicilioRealizadas.Items.Clear();
        }

        private void CargarAsignacionesParaFletero()
        {
            var fletero = cbFletero.Text?.Trim();
            if (string.IsNullOrWhiteSpace(fletero))
            {
                LimpiarListas();
                return;
            }

            var master = new CsvGuiaMaster();

            // Informativas (siempre desde master por estado "base")
            var aRetirar = master.ListarPorEstado(GuiaEstados.Impuesta).ToList();            // LV: A retirar en domicilio
            var aEntregar = master.ListarPorEstado(GuiaEstados.AdmitidaDestino).ToList();    // LV: Entregas a domicilio a realizar

            // Editables (trabajo del fletero). CLAVE: si no hay archivos -> NO mostrar nada (no hacer fallback global)
            List<string> retirosEditar = new List<string>();
            List<string> entregasEditar = new List<string>();

            if (_service.HayArchivosAsignacion(fletero))
            {
                var data = _service.CargarAsignaciones(fletero);
                retirosEditar = data.retirosPendientes ?? new List<string>();
                entregasEditar = data.entregasPendientes ?? new List<string>();
            }
            // else: SIN FALLBACK. Si no tiene asignaciones, no muestra nada en las listas editables

            _retirosPendientes = retirosEditar;
            _entregasPendientes = entregasEditar;

            // Pintar LVs editables
            lvRetirosDomicilioAdmitir.BeginUpdate();
            lvRetirosDomicilioAdmitir.Items.Clear();
            foreach (var g in retirosEditar) lvRetirosDomicilioAdmitir.Items.Add(new ListViewItem(new[] { "", g }) { Checked = false });
            lvRetirosDomicilioAdmitir.EndUpdate();

            lvEntregasDomicilioRealizadas.BeginUpdate();
            lvEntregasDomicilioRealizadas.Items.Clear();
            foreach (var g in entregasEditar) lvEntregasDomicilioRealizadas.Items.Add(new ListViewItem(new[] { "", g }) { Checked = false });
            lvEntregasDomicilioRealizadas.EndUpdate();
        }


        private void LimpiarListas()
        {
            lvRetirosDomicilioAdmitir.Items.Clear();
            lvEntregasDomicilioRealizadas.Items.Clear();
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
