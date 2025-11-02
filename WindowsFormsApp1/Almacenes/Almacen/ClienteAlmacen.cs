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
            if (File.Exists("clientes.json"))
            {
                var clienteJson = File.ReadAllText("clientes.json");
                clientes = System.Text.Json.JsonSerializer.Deserialize<List<ClienteEntidad>>(clienteJson);
            }
        }

        public static void Grabar()
        {
            var clienteJson = System.Text.Json.JsonSerializer.Serialize(clientes);
            File.WriteAllText("clientes.json", clienteJson);
        }
    }
}
