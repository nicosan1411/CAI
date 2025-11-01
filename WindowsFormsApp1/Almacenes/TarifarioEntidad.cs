using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAI_Proyecto.Almacenes
{
    public class TarifarioEntidad
    {
        public string Tamaño { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public decimal Precio { get; set; }
        public List<ExtrasEntidad> Extras { get; set; }
    }
}
