using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.IO;

namespace CAI_Proyecto.Almacenes.Almacen
{
    static class CentroDeDistribucionAlmacen
    {
        private static List<CentroDeDistribucionEntidad> centrosdedistribucion = new List<CentroDeDistribucionEntidad>();

        public static IReadOnlyCollection<CentroDeDistribucionEntidad> CentrosDeDistribucion => centrosdedistribucion.AsReadOnly();

        static CentroDeDistribucionAlmacen()
        {
            if (File.Exists("CentroDeDistribucion.json"))
            {
                var clienteJson = File.ReadAllText("CentroDeDistribucion.json");
                centrosdedistribucion = System.Text.Json.JsonSerializer.Deserialize<List<CentroDeDistribucionEntidad>>(clienteJson);
            }
        }

        public static void Grabar()
        {
            var clienteJson = System.Text.Json.JsonSerializer.Serialize(centrosdedistribucion);
            File.WriteAllText("CentroDeDistribucion.json", clienteJson);
        }
    }
}
