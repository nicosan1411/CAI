using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.IO;

namespace CAI_Proyecto.Almacenes.Almacen
{
    static class AgenciaComisionAlmacen
    {
        private static List<AgenciaComisionEntidad> agenciacomisiones = new List<AgenciaComisionEntidad>();

        public static IReadOnlyCollection<AgenciaComisionEntidad> AgenciaComisiones => agenciacomisiones.AsReadOnly();

        static AgenciaComisionAlmacen()
        {
            if (File.Exists(@"Datos\AgenciaComisiones.json"))
            {
                var AgenciaComisionJson = File.ReadAllText(@"Datos\AgenciaComisiones.json");
                agenciacomisiones = System.Text.Json.JsonSerializer.Deserialize<List<AgenciaComisionEntidad>>(AgenciaComisionJson);
            }
        }

        public static void Grabar()
        {
            var AgenciaComisionJson = System.Text.Json.JsonSerializer.Serialize(agenciacomisiones);
            File.WriteAllText(@"Datos\AgenciaComisiones.json", AgenciaComisionJson);
        }
    }
}
