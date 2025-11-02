using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAI_Proyecto.Almacenes.Entidad
{
    public class FacturaEntidad
    {
        public string CuitCliente { get; set; }
        public List<string> NumeroGuia { get; set; }
        public string Concepto { get; set; }
        public decimal Monto { get; set; }
    }
}
