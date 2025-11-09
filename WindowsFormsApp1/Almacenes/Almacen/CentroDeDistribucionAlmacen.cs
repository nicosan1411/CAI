using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.IO;

namespace CAI_Proyecto.Almacenes.Almacen
{
    static class CentroDeDistribucionAlmacen
    {
        public static CentroDeDistribucionEntidad CentroDeDistribucionActual { get; set; }


        private static List<CentroDeDistribucionEntidad> centrosdedistribucion = new List<CentroDeDistribucionEntidad>();

        public static IReadOnlyCollection<CentroDeDistribucionEntidad> CentrosDeDistribucion => centrosdedistribucion.AsReadOnly();

        static CentroDeDistribucionAlmacen()
        {
            if (File.Exists(@"Datos\CentrosDeDistribucion.json"))
            {
                var CentroDeDistribucionJson = File.ReadAllText(@"Datos\CentrosDeDistribucion.json");
                centrosdedistribucion = System.Text.Json.JsonSerializer.Deserialize<List<CentroDeDistribucionEntidad>>(CentroDeDistribucionJson);
            }
        }

        public static void Grabar()
        {
            var CentroDeDistribucionJson = System.Text.Json.JsonSerializer.Serialize(centrosdedistribucion);
            File.WriteAllText(@"Datos\CentrosDeDistribucion.json", CentroDeDistribucionJson);
        }
    }
}
