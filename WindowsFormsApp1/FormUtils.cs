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
    }
}
