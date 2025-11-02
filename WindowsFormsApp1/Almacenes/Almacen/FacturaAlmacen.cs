using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.IO;

namespace CAI_Proyecto.Almacenes.Almacen
{
    static class FacturaAlmacen
    {
        private static List<FacturaEntidad> facturas = new List<FacturaEntidad>();

        public static IReadOnlyCollection<FacturaEntidad> Facturas => facturas.AsReadOnly();

        static FacturaAlmacen()
        {
            if (File.Exists("facturas.json"))
            {
                var FacturaJson = File.ReadAllText("facturas.json");
                facturas = System.Text.Json.JsonSerializer.Deserialize<List<FacturaEntidad>>(FacturaJson);
            }
        }

        public static void Grabar()
        {
            var FacturaJson = System.Text.Json.JsonSerializer.Serialize(facturas);
            File.WriteAllText("facturas.json", FacturaJson);
        }
    }
}
