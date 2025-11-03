using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.IO;

namespace CAI_Proyecto.Almacenes.Almacen
{
    static class ComisionFleteroAlmacen
    {
        private static List<ComisionFleteroEntidad> comisionesfletero = new List<ComisionFleteroEntidad>();

        public static IReadOnlyCollection<ComisionFleteroEntidad> ComisionesFletero => comisionesfletero.AsReadOnly();

        static ComisionFleteroAlmacen()
        {
            if (File.Exists(@"Datos\ComisionesFletero.json"))
            {
                var ComisionFleteroJson = File.ReadAllText(@"Datos\ComisionesFletero.json");
                comisionesfletero = System.Text.Json.JsonSerializer.Deserialize<List<ComisionFleteroEntidad>>(ComisionFleteroJson);
            }
        }

        public static void Grabar()
        {
            var ComisionFleteroJson = System.Text.Json.JsonSerializer.Serialize(comisionesfletero);
            File.WriteAllText(@"Datos\ComisionesFletero.json", ComisionFleteroJson);
        }
    }
}
