using CAI_Proyecto.Almacenes.Almacen;
using CAI_Proyecto.Almacenes.ClaseAuxiliar;
using System.Collections.Generic;
using System.Linq;

namespace CAI_Proyecto.Forms.Operacion.EmitirFactura.Model
{
    public class EmitirFacturaModel
    {

        public IReadOnlyList<ListadoFacturacion> ListadoFacturacion { get; set; }

        public Cliente[] Clientes
        {
            get
            {
                return ClienteAlmacen.Clientes
                                     .Select(c => new Cliente
                                     {
                                         Cuit = c.Cuit,
                                         RazonSocial = c.RazonSocial,
                                     }).ToArray();
            }
        }

        public EmitirFacturaModel()
        {
            // Estados permitidos: Entregado (8), EntregadoClienteDomicilio (9), EntregadoAgencia (10)
            var estadosPermitidos = new[]
            {
                TipoEstadoGuiaEnum.Entregado,
                TipoEstadoGuiaEnum.EntregadoClienteDomicilio,
                TipoEstadoGuiaEnum.EntregadoAgencia
            };

            // Llenar el listado a partir de las guías que cumplan el filtro
            ListadoFacturacion = GuiaAlmacen.Guias
                .Where(g => g != null
                            && estadosPermitidos.Contains(g.EstadoActual != null ? g.EstadoActual.Estado : g.Estado))
                .Select(g =>
                {
                    var clienteEntidad = ClienteAlmacen.Clientes.FirstOrDefault(c => c.Cuit == g.CuitCliente);
                    var clienteTexto = clienteEntidad != null
                        ? $"{clienteEntidad.Cuit}  {clienteEntidad.RazonSocial}"
                        : g.CuitCliente ?? string.Empty;

                    var concepto = $"Desde {g.TipoRetiro} hasta {g.TipoEnvio}";

                    // Monto traído desde Precio de la guía
                    return new ListadoFacturacion(
                        clienteTexto,
                        g.NumeroGuia,
                        concepto,
                        g.Precio
                    );
                })
                .ToList();
        }
    }
}