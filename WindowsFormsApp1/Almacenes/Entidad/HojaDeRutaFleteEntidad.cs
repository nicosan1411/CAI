using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAI_Proyecto.Almacenes.Clase;

namespace CAI_Proyecto.Almacenes.Entidad
{
    public class HojaDeRutaFleteEntidad
    {
        public int IdHDRFlete { get; set; }
        public TipoHojaDeRuta Tipo { get; set; }
        public string IdFletero { get; set; }
        public string CuitCliente { get; set; }
        public int IdAgencia { get; set; }
        public string DomicilioDestino { get; set; }
        public bool Cumplida { get; set; }
        public List<string> Guias { get; set; }
    }
}
