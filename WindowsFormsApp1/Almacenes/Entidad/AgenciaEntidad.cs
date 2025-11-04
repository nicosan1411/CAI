using CAI_Proyecto.Almacenes.ClaseAuxiliar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAI_Proyecto.Almacenes.Entidad
{
    public class AgenciaEntidad
    {
        public int IdAgencia { get; set; }
        public string Nombre { get; set; }
        public string CDAsignado { get; set; }
        public List<AgenciaComisionEntidad> Comisiones { get; set; }
    }
}
