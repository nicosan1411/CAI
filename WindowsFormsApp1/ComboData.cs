using System.Collections.Generic;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Catálogos estáticos para combos. 
    /// NOTA: hoy están hardcodeados; a futuro pueden salir de archivos CSV/JSON.
    /// </summary>
    internal static class ComboData
    {
        /// <summary>Lista de empresas visibles en el combo (CUIT + Razón Social).</summary>
        public static IReadOnlyList<string> Empresas => new[]
        {
            "30-50109269-6  Unilever de Argentina S.A.",
            "30-50361405-3  Arcor S.A.I.C.",
            "30-54724233-1  Mastellone Hnos. S.A."
        };

        /// <summary>Catálogo de dimensiones (para detalle de encomiendas).</summary>
        public static IReadOnlyList<string> Dimensiones => new[] { "XS", "S", "M", "L", "XL" };

        /// <summary>Listado de provincias argentinas.</summary>
        public static IReadOnlyList<string> Provincias => new[]
        {
            "Buenos Aires","CABA","Catamarca","Chaco","Chubut","Córdoba","Corrientes",
            "Entre Ríos","Formosa","Jujuy","La Pampa","La Rioja","Mendoza","Misiones",
            "Neuquén","Río Negro","Salta","San Juan","San Luis","Santa Cruz","Santa Fe",
            "Santiago del Estero","Tierra del Fuego","Tucumán"
        };

        /// <summary>Agencias para retiro (cuando corresponde).</summary>
        public static IReadOnlyList<string> AgenciasRetiro => new[] { "Agencia 1", "Agencia 2", "Agencia 3" };

        /// <summary>Agencias para envío (cuando corresponde).</summary>
        public static IReadOnlyList<string> AgenciasEnvio => new[] { "Agencia A", "Agencia B", "Agencia C" };

        /// <summary>Patentes de micros (demo).</summary>
        public static IReadOnlyList<string> Patentes => new[] { "AA123BB", "AC456DD", "AE789FF" };

        /// <summary>Listado de fleteros (demo). Formato: Nombre - Patente - Código Flete.</summary>
        public static IReadOnlyList<string> Fleteros => new[]
        {
            "Juan Pérez - ABC123 - Flete 01",
            "María Gómez - JKL456 - Flete 02",
            "Carlos Ruiz - MNO789 - Flete 03"
        };
    }
}
