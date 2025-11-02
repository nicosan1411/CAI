using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAI_Proyecto.Almacenes.Entidad
{
    public class HojaDeRutaMicroEntidad
    {
        public int IdHDRMicro { get; set; }
        public string IdCDOrigen { get; set; }
        public string IdCDDestino { get; set; }
        public List<string> Guias { get; set; }
    }
}
