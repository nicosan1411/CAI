using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Encodings.Web; // ← agregar

namespace CAI_Proyecto.Almacenes.Almacen
{
    static class FacturaAlmacen
    {
        private static List<FacturaEntidad> facturas = new List<FacturaEntidad>();

        public static IReadOnlyCollection<FacturaEntidad> Facturas => facturas.AsReadOnly();

        static FacturaAlmacen()
        {
            var ruta = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Datos\Facturas.json");
            if (File.Exists(ruta))
            {
                var FacturaJson = File.ReadAllText(ruta);
                facturas = JsonSerializer.Deserialize<List<FacturaEntidad>>(FacturaJson) ?? new List<FacturaEntidad>();
            }
        }

        public static void Grabar()
        {
            var ruta = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Datos\Facturas.json");
            var opciones = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping // ← evita \u00F3, etc.
            };
            var FacturaJson = JsonSerializer.Serialize(facturas, opciones);
            File.WriteAllText(ruta, FacturaJson);
        }

        // Nuevo: agrega una factura a la lista en memoria y persiste inmediatamente
        public static void Agregar(FacturaEntidad factura)
        {
            if (factura == null) return;
            facturas.Add(factura);
            Grabar();
        }

        // Nuevo: devuelve el siguiente número de factura (max + 1)
        public static int ObtenerSiguienteNumeroFactura()
        {
            if (!facturas.Any()) return 1;
            return facturas.Max(f => f.NumeroFactura) + 1;
        }
    }
}
