using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.IO;

namespace CAI_Proyecto.Almacenes.Almacen
{
    static class ProvinciaAlmacen
    {
        private static List<ProvinciaEntidad> provincias = new List<ProvinciaEntidad>();

        public static IReadOnlyCollection<ProvinciaEntidad> Provincias => provincias.AsReadOnly();

        static ProvinciaAlmacen()
        {
            if (File.Exists("provincias.json"))
            {
                var ProvinciaJson = File.ReadAllText("provincias.json");
                provincias = System.Text.Json.JsonSerializer.Deserialize<List<ProvinciaEntidad>>(ProvinciaJson);
            }
        }

        public static void Grabar()
        {
            var ProvinciaJson = System.Text.Json.JsonSerializer.Serialize(provincias);
            File.WriteAllText("provincias.json", ProvinciaJson);
        }
    }
}
