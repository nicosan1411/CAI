using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;

namespace CAI_Proyecto.Almacenes.Almacen
{
    static class HojaDeRutaFleteAlmacen
    {
        private static List<HojaDeRutaFleteEntidad> hojasderutaflete = new List<HojaDeRutaFleteEntidad>();

        public static IReadOnlyCollection<HojaDeRutaFleteEntidad> HojasDeRutaFlete => hojasderutaflete.AsReadOnly();

        static HojaDeRutaFleteAlmacen()
        {
            if (File.Exists(@"Datos\HojasDeRutaFlete.json"))
            {
                var HojaDeRutaFleteJson = File.ReadAllText(@"Datos\HojasDeRutaFlete.json");
                hojasderutaflete = System.Text.Json.JsonSerializer.Deserialize<List<HojaDeRutaFleteEntidad>>(HojaDeRutaFleteJson);
            }
        }

        public static void Grabar()
        {
            var HojaDeRutaFleteJson = System.Text.Json.JsonSerializer.Serialize(hojasderutaflete);
            File.WriteAllText(@"Datos\HojasDeRutaFlete.json", HojaDeRutaFleteJson);
        }

        public static void Agregar(HojaDeRutaFleteEntidad hoja)
        {
            if (hoja == null) throw new ArgumentNullException(nameof(hoja));
            hoja.IdHDRFlete = hojasderutaflete.Any() ? hojasderutaflete.Max(h => h.IdHDRFlete) + 1 : 1;
            hojasderutaflete.Add(hoja);
            Grabar();
        }
    }
}
