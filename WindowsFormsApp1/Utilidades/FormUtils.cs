using System.Windows.Forms;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Utilidades comunes para formularios WinForms (confirmaciones, combos, listviews).
    /// </summary>
    public static class FormUtils
    {
        /// <summary>Pregunta si se desea volver al menú y cierra el form actual si la respuesta es Sí.</summary>
        public static void VolverAlMenu(Form formularioActual)
        {
            var result = MessageBox.Show(
                "¿Deseás volver al menú principal?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (result == DialogResult.Yes)
                formularioActual.Close();
        }

        /// <summary>Activa autocompletado en un ComboBox con los ítems cargados.</summary>
        public static void EnableAutocomplete(ComboBox combo)
        {
            combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            combo.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        /// <summary>Fuerza el ComboBox a DropDownList (no editable).</summary>
        public static void MakeDropDownList(ComboBox combo)
        {
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// Configura un ListView “básico”: selección de fila completa, single select y (opcional) checkboxes.
        /// </summary>
        public static void ConfigureListView(ListView lv, bool withCheckboxes)
        {
            lv.View = View.Details;
            lv.CheckBoxes = withCheckboxes;
            lv.FullRowSelect = true;
            lv.MultiSelect = false;
            lv.Items.Clear();
        }

        /// <summary>
        /// Rellena un ListView de **1 columna** con un conjunto de strings.
        /// </summary>
        public static void FillListView(ListView lv, System.Collections.Generic.IEnumerable<string> values)
        {
            lv.BeginUpdate();
            lv.Items.Clear();
            foreach (var v in values)
            {
                // Antes: new[] { "", v } (2 columnas) → inconsistente con “1 columna”.
                lv.Items.Add(new ListViewItem(v));
            }
            lv.EndUpdate();
        }

        /// <summary>Muestra un Yes/No estándar y devuelve true si es Sí.</summary>
        public static bool Confirm(string message, string title = "Confirmar") =>
            MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        /// <summary>Pop-up de información simple.</summary>
        public static void Info(string message, string title = "Información") =>
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);

        /// <summary>Handler para permitir solo dígitos en TextBox (asignar a KeyPress).</summary>
        public static void OnlyDigits_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
    }
}
