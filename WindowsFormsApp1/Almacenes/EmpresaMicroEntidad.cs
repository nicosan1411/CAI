using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAI_Proyecto.Almacenes
{
    public class EmpresaMicroEntidad
    {
        public string CuitEmpresaMicro { get; set; }
        public string NombreEmpresa { get; set; }
        public List<MicroTarifa> Tarifas { get; set; }
    }
}
