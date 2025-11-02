using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.IO;

namespace CAI_Proyecto.Almacenes.Almacen
{
    static class TarifarioAlmacen
    {
        private static List<TarifarioEntidad> tarifarios = new List<TarifarioEntidad>();

        public static IReadOnlyCollection<TarifarioEntidad> Tarifarios => tarifarios.AsReadOnly();

        static TarifarioAlmacen()
        {
            if (File.Exists(@"Datos\Tarifarios.json"))
            {
                var TarifarioJson = File.ReadAllText(@"Datos\Tarifarios.json");
                tarifarios = System.Text.Json.JsonSerializer.Deserialize<List<TarifarioEntidad>>(TarifarioJson);
            }
        }

        public static void Grabar()
        {
            var TarifarioJson = System.Text.Json.JsonSerializer.Serialize(tarifarios);
            File.WriteAllText(@"Datos\Tarifarios.json", TarifarioJson);
        }
    }
}
