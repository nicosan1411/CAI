// ===================================================
// CÓDIGO FUNCIONAL PERO CON MEJORAS A REALIZAR
// ---------------------------------------------------
// EN EL LISTVIEW DE ENTREGAS NO SE OBTIENEN DATOS
// DE REGISTROS CON ESTADO "ADMITIDO EN CD DE DESTINO"
// ===================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormRendirEncomiendas : Form
    {
        // Servicio de rendición (sin log de archivo por defecto)
        private readonly RendicionService _service;

        public FormRendirEncomiendas()
        {
            InitializeComponent();

            _service = new RendicionService(null) { HabilitarLog = false };          // sin log, simple


            InitCombos();
            InitListViews();
            WireHandlers();

            // Selecciona el primer fletero si hay, y carga sus asignaciones
            if (cbFletero.Items.Count > 0)
            {
                cbFletero.SelectedIndex = 0;
                CargarAsignacionesParaFletero();
            }
        }

        private void InitCombos()
        {
            cbFletero.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFletero.Items.Clear();
            // Fuente centralizada de fleteros
            foreach (var f in _service.ObtenerFleteros())
                cbFletero.Items.Add(f);

            if (cbFletero.Items.Count > 0)
                cbFletero.SelectedIndex = -1;
        }

        private void InitListViews()
        {
            // Configura columnas/checks/selección (la define tu helper)
            FormUtils.ConfigureListView(lvRetirosDomicilioAdmitir, true);
        }

        private void WireHandlers()
        {
            cbFletero.SelectedIndexChanged += (_, __) => CargarAsignacionesParaFletero();
            btnGuardar.Click += (_, __) => GuardarYRefrescar();
        }

        /// <summary>
        /// Carga SOLO lo asignado a ese fletero desde los TXT (nada de fallback al master).
        /// </summary>
        private void CargarAsignacionesParaFletero()
        {
            var fletero = (cbFletero.Text ?? "").Trim();

            lvRetirosDomicilioAdmitir.BeginUpdate();
            lvRetirosDomicilioAdmitir.Items.Clear();
            lvRetirosDomicilioAdmitir.EndUpdate();

            if (string.IsNullOrWhiteSpace(fletero))
                return;

            var data = _service.CargarAsignaciones(fletero);
            var retiros = data.retirosPendientes ?? new List<string>();

            lvRetirosDomicilioAdmitir.BeginUpdate();
            foreach (var guia in retiros)
            {
                // Si tu LV tiene dos columnas ["", "Guía"], usamos 2 subitems; si es 1 columna, solo Text.
                if (lvRetirosDomicilioAdmitir.Columns.Count > 1)
                    lvRetirosDomicilioAdmitir.Items.Add(new ListViewItem(new[] { "", guia }) { Checked = false });
                else
                    lvRetirosDomicilioAdmitir.Items.Add(new ListViewItem(guia) { Checked = false });
            }
            lvRetirosDomicilioAdmitir.EndUpdate();
        }

        /// <summary>
        /// Devuelve el N° de guía desde un ListViewItem, tolerando 1 o 2 columnas.
        /// </summary>
        private static string GetGuiaFromItem(ListViewItem it)
        {
            if (it == null) return "";
            return (it.SubItems.Count > 1 ? it.SubItems[1].Text : it.Text ?? "").Trim();
        }

        /// <summary>
        /// Toma tildadas, guarda (cambia estados + limpia TXT) y recarga la lista para que desaparezcan.
        /// </summary>
        private void GuardarYRefrescar()
        {
            var fletero = (cbFletero.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(fletero))
            {
                MessageBox.Show("Seleccioná un fletero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var guiasRetiros = lvRetirosDomicilioAdmitir.Items
                .Cast<ListViewItem>()
                .Where(it => it.Checked)
                .Select(GetGuiaFromItem)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();

            // Si este form también procesara entregas, acá armarías la lista:
            var guiasEntregas = new List<string>(); // no se usa en esta pantalla

            if (guiasRetiros.Count == 0 && guiasEntregas.Count == 0)
            {
                if (MessageBox.Show("No seleccionaste ninguna guía. ¿Querés guardar igual?",
                    "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            }

            if (MessageBox.Show("¿Confirmás guardar la rendición seleccionada?",
                "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            try
            {
                // Cambia estado en master (Impuesta -> Admitida en CD origen) y limpia TXT del fletero
                _service.Guardar(fletero, guiasRetiros, guiasEntregas);

                // 🔁 Recarga desde disco las asignaciones del fletero para que desaparezcan las ya rendidas
                CargarAsignacionesParaFletero();

                MessageBox.Show("Rendición guardada.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolverMenuPrincipal_Click(object sender, EventArgs e)
        {
            FormUtils.VolverAlMenu(this);
        }
    }

}
