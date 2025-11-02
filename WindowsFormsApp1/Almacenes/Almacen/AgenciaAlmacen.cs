using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.IO;

namespace CAI_Proyecto.Almacenes.Almacen
{
    static class AgenciaAlmacen
    {
        private static List<AgenciaEntidad> agencias = new List<AgenciaEntidad>();

        public static IReadOnlyCollection<AgenciaEntidad> Agencias => agencias.AsReadOnly();

        static AgenciaAlmacen()
        {
            if (File.Exists("agencias.json"))
            {
                var AgenciaJson = File.ReadAllText("agencias.json");
                agencias = System.Text.Json.JsonSerializer.Deserialize<List<AgenciaEntidad>>(AgenciaJson);
            }
        }

        public static void Grabar()
        {
            var AgenciaJson = System.Text.Json.JsonSerializer.Serialize(agencias);
            File.WriteAllText("agencias.json", AgenciaJson);
        }
    }
}
