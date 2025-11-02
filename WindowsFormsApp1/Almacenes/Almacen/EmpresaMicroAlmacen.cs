using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.IO;

namespace CAI_Proyecto.Almacenes.Almacen
{
    static class EmpresaMicroAlmacen
    {
        private static List<EmpresaMicroEntidad> empresamicros = new List<EmpresaMicroEntidad>();

        public static IReadOnlyCollection<EmpresaMicroEntidad> EmpresaMicros => empresamicros.AsReadOnly();

        static EmpresaMicroAlmacen()
        {
            if (File.Exists("empresamicros.json"))
            {
                var EmpresaMicroJson = File.ReadAllText("empresamicros.json");
                empresamicros = System.Text.Json.JsonSerializer.Deserialize<List<EmpresaMicroEntidad>>(EmpresaMicroJson);
            }
        }

        public static void Grabar()
        {
            var EmpresaMicroJson = System.Text.Json.JsonSerializer.Serialize(empresamicros);
            File.WriteAllText("empresamicros.json", EmpresaMicroJson);
        }
    }
}
