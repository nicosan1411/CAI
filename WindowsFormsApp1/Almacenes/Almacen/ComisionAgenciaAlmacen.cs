using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.IO;

namespace CAI_Proyecto.Almacenes.Almacen
{
    static class ComisionAgenciaAlmacen
    {
        private static List<ComisionAgenciaEntidad> comisionesagencia = new List<ComisionAgenciaEntidad>();

        public static IReadOnlyCollection<ComisionAgenciaEntidad> ComisionesAgencia => comisionesagencia.AsReadOnly();

        static ComisionAgenciaAlmacen()
        {
            if (File.Exists(@"Datos\ComisionesAgencia.json"))
            {
                var ComisionAgenciaJson = File.ReadAllText(@"Datos\ComisionesAgencia.json");
                comisionesagencia = System.Text.Json.JsonSerializer.Deserialize<List<ComisionAgenciaEntidad>>(ComisionAgenciaJson);
            }
        }

        public static void Grabar()
        {
            var ComisionAgenciaJson = System.Text.Json.JsonSerializer.Serialize(comisionesagencia);
            File.WriteAllText(@"Datos\ComisionesAgencia.json", ComisionAgenciaJson);
        }
    }
}
