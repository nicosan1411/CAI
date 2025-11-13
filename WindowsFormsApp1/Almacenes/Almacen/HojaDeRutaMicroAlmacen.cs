using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace CAI_Proyecto.Almacenes.Almacen
{
    static class HojaDeRutaMicroAlmacen
    {
        private static List<HojaDeRutaMicroEntidad> hojasderutamicro = new List<HojaDeRutaMicroEntidad>();

        public static IReadOnlyCollection<HojaDeRutaMicroEntidad> HojasDeRutaMicro => hojasderutamicro.AsReadOnly();

        static HojaDeRutaMicroAlmacen()
        {
            if (File.Exists(@"Datos\HojasDeRutaMicro.json"))
            {
                var HojaDeRutaMicroJson = File.ReadAllText(@"Datos\HojasDeRutaMicro.json");
                hojasderutamicro = System.Text.Json.JsonSerializer.Deserialize<List<HojaDeRutaMicroEntidad>>(HojaDeRutaMicroJson);
            }
        }

        public static void Grabar()
        {
            var HojaDeRutaMicroJson = System.Text.Json.JsonSerializer.Serialize(hojasderutamicro);
            File.WriteAllText(@"Datos\HojasDeRutaMicro.json", HojaDeRutaMicroJson);
        }

        public static void Agregar(HojaDeRutaMicroEntidad hoja)
        {
            if (hoja == null) throw new ArgumentNullException(nameof(hoja));
            hoja.IdHDRMicro = hojasderutamicro.Any() ? hojasderutamicro.Max(h => h.IdHDRMicro) + 1 : 1;
            hojasderutamicro.Add(hoja);
            Grabar();
        }
    }
}
