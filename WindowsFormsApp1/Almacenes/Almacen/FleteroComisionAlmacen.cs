using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.IO;

namespace CAI_Proyecto.Almacenes.Almacen
{
    static class FleteroComisionAlmacen
    {
        private static List<FleteroComisionEntidad> fleterocomisiones = new List<FleteroComisionEntidad>();

        public static IReadOnlyCollection<FleteroComisionEntidad> FleteroComisiones => fleterocomisiones.AsReadOnly();

        static FleteroComisionAlmacen()
        {
            if (File.Exists("fleterocomisiones.json"))
            {
                var FleteroComisionJson = File.ReadAllText("fleterocomisiones.json");
                fleterocomisiones = System.Text.Json.JsonSerializer.Deserialize<List<FleteroComisionEntidad>>(FleteroComisionJson);
            }
        }

        public static void Grabar()
        {
            var FleteroComisionJson = System.Text.Json.JsonSerializer.Serialize(fleterocomisiones);
            File.WriteAllText("fleterocomisiones.json", FleteroComisionJson);
        }
    }
}
