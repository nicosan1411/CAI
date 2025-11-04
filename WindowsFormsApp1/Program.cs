using System;
using System.Windows.Forms;
using CAI_Proyecto.Almacenes.Almacen;
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

            // Guardo los datos de los almacenes antes de salir
            AgenciaAlmacen.Grabar();
            CentroDeDistribucionAlmacen.Grabar();
            ClienteAlmacen.Grabar();
            EmpresaFleteAlmacen.Grabar();
            EmpresaMicroAlmacen.Grabar();
            ExtrasAlmacen.Grabar();
            FacturaAlmacen.Grabar();
            FleteroAlmacen.Grabar();
            FleteroComisionAlmacen.Grabar();
            GuiaAlmacen.Grabar();
            HojaDeRutaFleteAlmacen.Grabar();
            HojaDeRutaMicroAlmacen.Grabar();
            MicroAlmacen.Grabar();
            ProvinciaAlmacen.Grabar();
            TarifarioAlmacen.Grabar();
        }
    }
}