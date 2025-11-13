using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAI_Proyecto.Almacenes.ClaseAuxiliar
{
    public enum TipoEstadoGuiaEnum
    {
        EnBusquedaRetiroDomicilio,      // 0
        EnBusquedaRetiroAgencia,        // 1
        AdmitidoCDOrigen,               // 2
        AdmitidoCDDestino,              // 3
        EnTransitoPorMicro,             // 4
        Recepcionado,                   // 5
        EnTransitoADomicilio,           // 6
        EnTransitoAAgencia,             // 7
        Entregado,                      // 8
        EntregadoClienteDomicilio,      // 9
        EntregadoAgencia,               // 10
        Devuelto,                       // 11
        Facturado,                      // 12
        Pagado                          // 13
    }
}
