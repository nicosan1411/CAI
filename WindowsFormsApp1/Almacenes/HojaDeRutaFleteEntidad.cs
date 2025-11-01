using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAI_Proyecto.Almacenes
{
    public class HojaDeRutaFleteEntidad
    {
        public int IdHDRFlete { get; set; }
        public TipoHojaDeRuta Tipo { get; set; }
        public string IdFletero { get; set; }
        public string CuitCliente { get; set; }
        public int IdAgencia { get; set; }
        public string DomicilioDestino { get; set; }
        List<string> Guias { get; set; }
    }
}
