using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.IO;

namespace CAI_Proyecto.Almacenes.Almacen
{
    static class ExtrasAlmacen
    {
        private static List<ExtrasEntidad> extras = new List<ExtrasEntidad>();

        public static IReadOnlyCollection<ExtrasEntidad> Extras => extras.AsReadOnly();

        static ExtrasAlmacen()
        {
            if (File.Exists("extras.json"))
            {
                var extrasJson = File.ReadAllText("extras.json");
                extras = System.Text.Json.JsonSerializer.Deserialize<List<ExtrasEntidad>>(extrasJson);
            }
        }

        public static void Grabar()
        {
            var extrasJson = System.Text.Json.JsonSerializer.Serialize(extras);
            File.WriteAllText("extras.json", extrasJson);
        }
    }
}
