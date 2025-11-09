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
            if (File.Exists(@"Datos\EmpresaMicros.json"))
            {
                var EmpresaMicroJson = File.ReadAllText(@"Datos\EmpresaMicros.json");
                var options = new System.Text.Json.JsonSerializerOptions
                {
                    Converters =
                    {
                        //Escribe/Lee las enumeraciones como strings.
                        new System.Text.Json.Serialization.JsonStringEnumConverter()
                    }
                };
                empresamicros = System.Text.Json.JsonSerializer.Deserialize<List<EmpresaMicroEntidad>>(EmpresaMicroJson, options);
            }
        }

        public static void Grabar()
        {
            var options = new System.Text.Json.JsonSerializerOptions
            {
                Converters =
                    {
                        new System.Text.Json.Serialization.JsonStringEnumConverter()
                    }
            };
            var EmpresaMicroJson = System.Text.Json.JsonSerializer.Serialize(empresamicros, options);
            File.WriteAllText(@"Datos\EmpresaMicros.json", EmpresaMicroJson);
        }
    }
}
