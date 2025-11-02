using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.IO;

namespace CAI_Proyecto.Almacenes.Almacen
{
    static class HojaDeRutaFleteAlmacen
    {
        private static List<HojaDeRutaFleteEntidad> hojasderutaflete = new List<HojaDeRutaFleteEntidad>();

        public static IReadOnlyCollection<HojaDeRutaFleteEntidad> HojasDeRutaFlete => hojasderutaflete.AsReadOnly();

        static HojaDeRutaFleteAlmacen()
        {
            if (File.Exists("hojasderutaflete.json"))
            {
                var HojaDeRutaFleteJson = File.ReadAllText("hojasderutaflete.json");
                hojasderutaflete = System.Text.Json.JsonSerializer.Deserialize<List<HojaDeRutaFleteEntidad>>(HojaDeRutaFleteJson);
            }
        }

        public static void Grabar()
        {
            var HojaDeRutaFleteJson = System.Text.Json.JsonSerializer.Serialize(hojasderutaflete);
            File.WriteAllText("hojasderutaflete.json", HojaDeRutaFleteJson);
        }
    }
}
