using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAI_Proyecto.Almacenes.Entidad
{
    public class ClienteEntidad
    {
        public string Cuit { get; set; }
        public string RazonSocial { get; set; }
        public List<int> AgenciasAsociadas { get; set; }
        public string Domicilio { get; set; }
    }
}
