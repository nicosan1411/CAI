using CAI_Proyecto.Almacenes.ClaseAuxiliar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAI_Proyecto.Almacenes.Entidad
{
    public class GuiaEntidad
    {
        public string NumeroGuia { get; set; }
        public TipoEstadoGuiaEnum Estado { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string CuitCliente { get; set; }
        public int AgenciaOrigen { get; set; }
        public int AgenciaDestino { get; set; }
        public string IdCDOrigen { get; set; }
        public string IdCDDestino { get; set; }
        public TipoRetiroEnum TipoRetiro { get; set; }
        public TipoEnvioEnum TipoEnvio { get; set; }
        public int DniDestinatario { get; set; }
        public string IdProvinciaDestino { get; set; }
        public string LocalidadDestino { get; set; }
        public string DomicilioDestino { get; set; }
        public TipoBultoEnum Dimension { get; set; }
        public List<Recorrido> Recorrido { get; set; }
        public EstadoGuia EstadoActual { get; set; }
        public List<EstadoGuia> Historial { get; set; }
        public decimal Precio { get; set; }
        public decimal ComisionesAgenciaOrigen { get; set; }
        public decimal ComisionesAgenciaDestino { get; set; }
        public decimal ComisionesFleteroOrigen { get; set; }
        public decimal ComisionesFleteroDestino { get; set; }
        public int NumeroFactura { get; set; }
    }
}
