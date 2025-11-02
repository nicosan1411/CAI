using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.IO;

namespace CAI_Proyecto.Almacenes.Almacen
{
    static class GuiaAlmacen
    {
        private static List<GuiaEntidad> guias = new List<GuiaEntidad>();

        public static IReadOnlyCollection<GuiaEntidad> Guias => guias.AsReadOnly();

        static GuiaAlmacen()
        {
            if (File.Exists(@"Datos\Guias.json"))
            {
                var GuiaJson = File.ReadAllText(@"Datos\Guias.json");
                guias = System.Text.Json.JsonSerializer.Deserialize<List<GuiaEntidad>>(GuiaJson);
            }
        }

        public static void Grabar()
        {
            var GuiaJson = System.Text.Json.JsonSerializer.Serialize(guias);
            File.WriteAllText(@"Datos\Guias.json", GuiaJson);
        }
    }
}
