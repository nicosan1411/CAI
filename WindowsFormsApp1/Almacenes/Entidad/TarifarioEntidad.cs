using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAI_Proyecto.Almacenes.ClaseAuxiliar;


namespace CAI_Proyecto.Almacenes.Entidad
{
    public class TarifarioEntidad
    {
        public TipoBulto Tamaño { get; set; }
        public string IdCDOrigen { get; set; }
        public string IdCDDestino { get; set; }
        public decimal Precio { get; set; }
    }
}
