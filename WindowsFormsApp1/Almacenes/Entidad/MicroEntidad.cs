using CAI_Proyecto.Almacenes.ClaseAuxiliar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAI_Proyecto.Almacenes.Entidad
{
    public class MicroEntidad
    {
        public int IdMicro { get; set; }
        public string CuitEmpresaMicro { get; set; }
        public string Patente { get; set; }
        public List<Parada> Recorrido { get; set; }
        public TipoTarifaMicroEnum TipoTarifa { get; set; }
        public List<int> HojasDeRuta { get; set; }
    }
}
