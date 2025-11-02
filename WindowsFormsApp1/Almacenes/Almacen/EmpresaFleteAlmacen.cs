using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.IO;

namespace CAI_Proyecto.Almacenes.Almacen
{
    static class EmpresaFleteAlmacen
    {
        private static List<EmpresaFleteEntidad> empresafletes = new List<EmpresaFleteEntidad>();

        public static IReadOnlyCollection<EmpresaFleteEntidad> EmpresaFletes => empresafletes.AsReadOnly();

        static EmpresaFleteAlmacen()
        {
            if (File.Exists(@"Datos\EmpresaFletes.json"))
            {
                var EmpresaFleteJson = File.ReadAllText(@"Datos\EmpresaFletes.json");
                empresafletes = System.Text.Json.JsonSerializer.Deserialize<List<EmpresaFleteEntidad>>(EmpresaFleteJson);
            }
        }

        public static void Grabar()
        {
            var EmpresaFleteJson = System.Text.Json.JsonSerializer.Serialize(empresafletes);
            File.WriteAllText(@"Datos\EmpresaFletes.json", EmpresaFleteJson);
        }
    }
}
