using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.IO;

namespace CAI_Proyecto.Almacenes.Almacen
{
    static class HojaDeRutaMicroAlmacen
    {
        private static List<HojaDeRutaMicroEntidad> hojasderutamicro = new List<HojaDeRutaMicroEntidad>();

        public static IReadOnlyCollection<HojaDeRutaMicroEntidad> HojasDeRutaMicro => hojasderutamicro.AsReadOnly();

        static HojaDeRutaMicroAlmacen()
        {
            if (File.Exists("hojasderutamicro.json"))
            {
                var HojaDeRutaMicroJson = File.ReadAllText("hojasderutamicro.json");
                hojasderutamicro = System.Text.Json.JsonSerializer.Deserialize<List<HojaDeRutaMicroEntidad>>(HojaDeRutaMicroJson);
            }
        }

        public static void Grabar()
        {
            var HojaDeRutaMicroJson = System.Text.Json.JsonSerializer.Serialize(hojasderutamicro);
            File.WriteAllText("hojasderutamicro.json", HojaDeRutaMicroJson);
        }
    }
}
