using CAI_Proyecto.Almacenes.Almacen;
using CAI_Proyecto.Almacenes.ClaseAuxiliar;
using CAI_Proyecto.Almacenes.Entidad;
using System;
using System.Collections.Generic;
using System.IO;
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
            var estadosPermitidos = new[]
            {
                TipoEstadoGuiaEnum.Entregado,
                TipoEstadoGuiaEnum.EntregadoClienteDomicilio,
                TipoEstadoGuiaEnum.EntregadoAgencia
            };

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

                    return new ListadoFacturacion(
                        clienteTexto,
                        g.NumeroGuia,
                        concepto,
                        g.Precio
                    );
                })
                .ToList();
        }

        // Usa FacturaAlmacen para persistir; evita escrituras directas que se pierden al finalizar.
        public FacturaEntidad FacturarGuias(string cuitCliente, IEnumerable<string> numerosGuias)
        {
            if (string.IsNullOrWhiteSpace(cuitCliente)) throw new ArgumentException("CUIT inválido", nameof(cuitCliente));
            if (numerosGuias == null) throw new ArgumentNullException(nameof(numerosGuias));

            var numerosDistinct = numerosGuias
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .Select(n => n.Trim())
                .Distinct()
                .ToList();

            if (!numerosDistinct.Any()) throw new InvalidOperationException("No hay números de guía válidos para facturar.");

            var guias = GuiaAlmacen.Guias.Where(g => numerosDistinct.Contains(g.NumeroGuia)).ToList();
            if (!guias.Any()) throw new InvalidOperationException("No se encontraron guías en el almacén para los números proporcionados.");

            // Usar FacturaAlmacen para el número siguiente
            int nuevoNumeroFactura = FacturaAlmacen.ObtenerSiguienteNumeroFactura();

            decimal montoTotal = guias.Sum(g => g.Precio);
            int cantidadGuias = guias.Select(g => g.NumeroGuia).Distinct().Count();

            var clienteEntidad = ClienteAlmacen.Clientes.FirstOrDefault(c => c.Cuit == cuitCliente);
            var clienteTexto = clienteEntidad != null ? $"{clienteEntidad.Cuit} {clienteEntidad.RazonSocial}" : cuitCliente;
            string concepto = $"Se realizó la entrega de {cantidadGuias} Guías para la empresa {clienteTexto}";

            var concatenado = string.Concat(guias.Select(g => g.NumeroGuia));

            var nuevaFactura = new FacturaEntidad
            {
                CuitCliente = cuitCliente,
                NumeroGuia = new List<string> { concatenado },
                Concepto = concepto,
                Monto = montoTotal,
                NumeroFactura = nuevoNumeroFactura
            };

            // Persistir usando la API del almacén (actualiza la lista en memoria)
            FacturaAlmacen.Agregar(nuevaFactura);

            // Marcar guías en memoria y persistir con GuiaAlmacen.Grabar()
            foreach (var g in guias)
            {
                g.Estado = TipoEstadoGuiaEnum.Facturado;
                g.NumeroFactura = nuevoNumeroFactura;

                var nuevoEstado = new EstadoGuia
                {
                    Estado = TipoEstadoGuiaEnum.Facturado,
                    Fecha = DateTime.Now,
                    IdCentroDistribucion = g.EstadoActual?.IdCentroDistribucion
                };

                g.EstadoActual = nuevoEstado;
                if (g.Historial == null) g.Historial = new List<EstadoGuia>();
                g.Historial.Add(nuevoEstado);
            }

            GuiaAlmacen.Grabar();

            // Recalcular ListadoFacturacion para mantener coherencia en la UI
            var estadosPermitidos = new[]
            {
                TipoEstadoGuiaEnum.Entregado,
                TipoEstadoGuiaEnum.EntregadoClienteDomicilio,
                TipoEstadoGuiaEnum.EntregadoAgencia
            };

            ListadoFacturacion = GuiaAlmacen.Guias
                .Where(g => g != null
                            && estadosPermitidos.Contains(g.EstadoActual != null ? g.EstadoActual.Estado : g.Estado))
                .Select(g =>
                {
                    var clienteEnt = ClienteAlmacen.Clientes.FirstOrDefault(c => c.Cuit == g.CuitCliente);
                    var clienteTxt = clienteEnt != null
                        ? $"{clienteEnt.Cuit}  {clienteEnt.RazonSocial}"
                        : g.CuitCliente ?? string.Empty;

                    var conceptoTxt = $"Desde {g.TipoRetiro} hasta {g.TipoEnvio}";
                    return new ListadoFacturacion(clienteTxt, g.NumeroGuia, conceptoTxt, g.Precio);
                })
                .ToList();

            return nuevaFactura;
        }
    }
}