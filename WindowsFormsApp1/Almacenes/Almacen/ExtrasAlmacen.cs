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
            if (File.Exists(@"Datos\Extras.json"))
            {
                var ExtrasJson = File.ReadAllText(@"Datos\Extras.json");
                extras = System.Text.Json.JsonSerializer.Deserialize<List<ExtrasEntidad>>(ExtrasJson);
            }
        }

        public static void Grabar()
        {
            var ExtrasJson = System.Text.Json.JsonSerializer.Serialize(extras);
            File.WriteAllText(@"Datos\Extras.json", ExtrasJson);
        }
    }
}
