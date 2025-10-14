using System.Collections.Generic;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Catálogos estáticos y datos de ejemplo para los formularios del sistema.
    /// Estos datos están hardcodeados con propósito demostrativo.
    /// </summary>
    internal static class ComboData
    {
        // ============================
        //   CATÁLOGOS PRINCIPALES
        // ============================

        public static IReadOnlyList<string> Empresas => new[]
        {
            "30-50109269-6  Unilever de Argentina S.A.",
            "30-50361405-3  Arcor S.A.I.C.",
            "30-70752101-7  Molinos Río de la Plata S.A.",
            "30-50033372-9  Coca-Cola FEMSA S.A.",
            "30-56712390-1  Procter & Gamble S.R.L.",
            "30-58412999-2  Ledesma S.A.A.I.",
            "30-70012345-8  Nestlé Argentina S.A.",
            "30-66544332-7  Danone S.A."
        };

        public static IReadOnlyList<string> Dimensiones => new[] { "XS", "S", "M", "L", "XL" };

        public static IReadOnlyList<string> Provincias => new[]
        {
            "Buenos Aires","CABA","Catamarca","Chaco","Chubut","Córdoba","Corrientes",
            "Entre Ríos","Formosa","Jujuy","La Pampa","La Rioja","Mendoza","Misiones",
            "Neuquén","Río Negro","Salta","San Juan","San Luis","Santa Cruz","Santa Fe",
            "Santiago del Estero","Tierra del Fuego","Tucumán"
        };

        public static IReadOnlyList<string> AgenciasRetiro => new[] { "Agencia 1", "Agencia 2", "Agencia 3" };

        public static IReadOnlyList<string> AgenciasEnvio => new[] { "Agencia A", "Agencia B", "Agencia C" };

        public static IReadOnlyList<string> Patentes => new[]
        {
            "AA123BB","AC456DD","AE789FF","AF321GG","AH654HH"
        };

        public static IReadOnlyList<string> Fleteros => new[]
        {
            "Juan Pérez - ABC123 - Flete 01",
            "María Gómez - JKL456 - Flete 02",
            "Carlos Ruiz - MNO789 - Flete 03",
            "Laura Fernández - PQR321 - Flete 04",
            "Diego Torres - STU654 - Flete 05"
        };
    }
}
