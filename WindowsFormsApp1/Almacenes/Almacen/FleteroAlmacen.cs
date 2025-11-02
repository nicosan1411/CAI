using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.IO;

namespace CAI_Proyecto.Almacenes.Almacen
{
    static class FleteroAlmacen
    {
        private static List<FleteroEntidad> fleteros = new List<FleteroEntidad>();

        public static IReadOnlyCollection<FleteroEntidad> Fleteros => fleteros.AsReadOnly();

        static FleteroAlmacen()
        {
            if (File.Exists("fleteros.json"))
            {
                var FleteroJson = File.ReadAllText("fleteros.json");
                fleteros = System.Text.Json.JsonSerializer.Deserialize<List<FleteroEntidad>>(FleteroJson);
            }
        }

        public static void Grabar()
        {
            var FleteroJson = System.Text.Json.JsonSerializer.Serialize(fleteros);
            File.WriteAllText("fleteros.json", FleteroJson);
        }
    }
}
