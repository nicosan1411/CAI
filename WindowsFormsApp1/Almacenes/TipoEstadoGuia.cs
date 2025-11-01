using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAI_Proyecto.Almacenes
{
    public enum TipoEstadoGuia
    {
        PendienteRetiroDomicilio,
        PendienteRetiroAgencia,
        EnBusquedaRetiroDomicilio,
        EnBusquedaRetiroAgencia,
        Admitido,
        EnTransitoPorMicro,
        Recepcionado,
        EnDestino,
        Entregado,
        EntregadoClienteDomicilio,
        EntregadoAgencia,
        Devuelto,
        Facturado
    }
}
