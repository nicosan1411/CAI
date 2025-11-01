using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAI_Proyecto.Almacenes
{
    public class HojaDeRutaMicroEntidad
    {
        public int IdHDRMicro { get; set; }
        public string CDOrigen { get; set; }
        public string CDDestino { get; set; }
        public List<string> Guias { get; set; }
    }
}
