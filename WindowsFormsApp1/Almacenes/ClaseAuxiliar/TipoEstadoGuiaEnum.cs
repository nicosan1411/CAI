using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAI_Proyecto.Almacenes.ClaseAuxiliar
{
    public enum TipoEstadoGuiaEnum
    {
        EnBusquedaRetiroDomicilio,
        EnBusquedaRetiroAgencia,
        AdmitidoCDOrigen,
        AdmitidoCDDestino,
        EnTransitoPorMicro,
        Recepcionado,
        EnTransitoADomicilio,
        EnTransitoAAgencia,
        Entregado,
        EntregadoClienteDomicilio,
        EntregadoAgencia,
        Devuelto,
        Facturado,
        Pagado
    }
}
