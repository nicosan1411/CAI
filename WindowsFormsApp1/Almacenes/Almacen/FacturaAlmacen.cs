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
            if (File.Exists(@"Datos\Facturas.json"))
            {
                var FacturaJson = File.ReadAllText(@"Datos\Facturas.json");
                facturas = System.Text.Json.JsonSerializer.Deserialize<List<FacturaEntidad>>(FacturaJson);
            }
        }

        public static void Grabar()
        {
            var FacturaJson = System.Text.Json.JsonSerializer.Serialize(facturas);
            File.WriteAllText(@"Datos\Facturas.json", FacturaJson);
        }
    }
}
