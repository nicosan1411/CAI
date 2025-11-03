using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.IO;

namespace CAI_Proyecto.Almacenes.Almacen
{
    static class ClienteAlmacen
    {
        private static List<ClienteEntidad> clientes = new List<ClienteEntidad>();

        public static IReadOnlyCollection<ClienteEntidad> Clientes => clientes.AsReadOnly();

        static ClienteAlmacen()
        {
            if (File.Exists(@"Datos\Clientes.json"))
            {
                var ClienteJson = File.ReadAllText(@"Datos\Clientes.json");
                clientes = System.Text.Json.JsonSerializer.Deserialize<List<ClienteEntidad>>(ClienteJson);
            }
        }

        public static void Grabar()
        {
            var ClienteJson = System.Text.Json.JsonSerializer.Serialize(clientes);
            File.WriteAllText(@"Datos\Clientes.json", ClienteJson);
        }
    }
}
