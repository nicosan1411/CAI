using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.IO;

namespace CAI_Proyecto.Almacenes.Almacen
{
    static class MicroAlmacen
    {
        private static List<MicroEntidad> micros = new List<MicroEntidad>();

        public static IReadOnlyCollection<MicroEntidad> Micros => micros.AsReadOnly();

        static MicroAlmacen()
        {
            if (File.Exists(@"Datos\Micros.json"))
            {
                var MicroJson = File.ReadAllText(@"Datos\Micros.json");
                micros = System.Text.Json.JsonSerializer.Deserialize<List<MicroEntidad>>(MicroJson);
            }
        }

        public static void Grabar()
        {
            var MicroJson = System.Text.Json.JsonSerializer.Serialize(micros);
            File.WriteAllText(@"Datos\Micros.json", MicroJson);
        }
    }
}
