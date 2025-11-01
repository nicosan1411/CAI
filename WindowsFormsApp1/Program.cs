using System;
using System.Windows.Forms;
using CAI_Proyecto.Forms.Inicio;

namespace CAI_Proyecto
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new InicioForm());
        }
    }
}