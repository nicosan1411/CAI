using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public static class FormUtils
    {
        public static void VolverAlMenu(Form formularioActual)
        {
            var result = MessageBox.Show(
                "¿Estás seguro de que querés volver al menú principal?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                formularioActual.Close();
            }
        }

        // Activar autocompletado en cualquier ComboBox
        public static void EnableAutocomplete(ComboBox combo)
        {
            combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            combo.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        // Forzar DropDownList (solo selección)
        public static void MakeDropDownList(ComboBox combo)
        {
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        // Configurar un ListView típico (con o sin checkboxes)
        public static void ConfigureListView(ListView lv, bool withCheckboxes)
        {
            lv.CheckBoxes = withCheckboxes;
            lv.FullRowSelect = true;
            lv.MultiSelect = false;
            lv.Items.Clear();
        }

        // Rellenar un ListView de 1 columna (ej. guías)
        public static void FillListView(ListView lv, System.Collections.Generic.IEnumerable<string> values)
        {
            lv.BeginUpdate();
            lv.Items.Clear();
            foreach (var v in values)
            {
                var item = new ListViewItem(new[] { "", v });
                lv.Items.Add(item);
            }
            lv.EndUpdate();
        }

        // Confirmación estándar
        public static bool Confirm(string message, string title = "Confirmar")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        // Helper de info
        public static void Info(string message, string title = "Información")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Handler genérico para permitir solo dígitos
        public static void OnlyDigits_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
    }
}
