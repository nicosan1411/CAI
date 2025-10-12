using System.Collections.Generic;

namespace WindowsFormsApp1
{
    internal static class ComboData
    {
        public static IReadOnlyList<string> Empresas => new[] { "Empresa 1", "Empresa 2", "Empresa 3" };
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

        public static IReadOnlyList<string> Patentes => new[] { "AA123BB", "AC456DD", "AE789FF" };
    }
}
