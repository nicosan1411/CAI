using CAI_Proyecto.Almacenes.Almacen;
using CAI_Proyecto.Almacenes.ClaseAuxiliar;
using CAI_Proyecto.Almacenes.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CAI_Proyecto.Forms.Consultas.CuentaCorriente.Model
{
    public class CuentaCorrienteModel
    {
        public IReadOnlyList<MovimientoCuenta> MovimientosCuenta { get; set; }

        public Cliente[] Empresas
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

        internal bool CargarEstadoCuenta(Filtros filtros)
        {
            if (filtros.FechaDesde > filtros.FechaHasta)
            {
                MessageBox.Show("La fecha desde debe ser mayor a la hasta.");
                return false;
            }

            // Normalizar fechas (solo fecha, sin hora) y CUIT
            var desde = filtros.FechaDesde.Date;
            var hasta = filtros.FechaHasta.Date;
            var cuitFiltro = NormalizarCuit(filtros.CuitCliente);

            // Preparar colecciones seguras
            var guias = GuiaAlmacen.Guias ?? Array.Empty<GuiaEntidad>();
            var facturas = FacturaAlmacen.Facturas ?? Array.Empty<FacturaEntidad>();

            // 1) Filtrar guías por cliente y por historial (solo estados 12 o 13) en rango de fechas
            var guiasConEstados =
                from g in guias
                where g != null
                      && !string.IsNullOrWhiteSpace(g.CuitCliente)
                      && string.Equals(NormalizarCuit(g.CuitCliente), cuitFiltro, StringComparison.OrdinalIgnoreCase)
                      && g.Historial != null
                from h in g.Historial
                where (h.Estado == TipoEstadoGuiaEnum.Facturado || h.Estado == TipoEstadoGuiaEnum.Pagado)
                      && h.Fecha.Date >= desde && h.Fecha.Date <= hasta
                select new { Guia = g, Hist = h };

            // 2) Unir con facturas por NumeroFactura para obtener Concepto
            var movimientosBase =
                (from gh in guiasConEstados
                 join f in facturas on gh.Guia.NumeroFactura.GetValueOrDefault(-1) equals f.NumeroFactura into gj
                 from f in gj.DefaultIfEmpty()
                 select new
                 {
                     Fecha = gh.Hist.Fecha.Date,
                     Comprobante = gh.Guia.NumeroFactura.HasValue ? gh.Guia.NumeroFactura.Value.ToString() : string.Empty,
                     Concepto = f != null ? f.Concepto : string.Empty,
                     EsFacturado = gh.Hist.Estado == TipoEstadoGuiaEnum.Facturado,
                     Precio = gh.Guia.Precio
                 })
                .Select(x => new
                {
                    x.Fecha,
                    x.Comprobante,
                    x.Concepto,
                    Monto = x.EsFacturado ? -Math.Abs(x.Precio) : 0m, // Factura: monto negativo
                    Pago = x.EsFacturado ? 0m : Math.Abs(x.Precio)    // Pago: ingreso
                })
                .OrderBy(x => x.Fecha)
                .ThenBy(x => x.Comprobante)
                .ToList();

            if (movimientosBase.Count == 0)
            {
                MovimientosCuenta = new List<MovimientoCuenta>();
                MessageBox.Show("No se registran movimientos");
                return true;
            }

            // 3) Calcular saldo progresivo: Saldo = SaldoAnterior + Pago - |Monto|
            decimal saldo = 0m;
            var movimientos = new List<MovimientoCuenta>(movimientosBase.Count);
            foreach (var m in movimientosBase)
            {
                saldo = saldo + m.Pago - Math.Abs(m.Monto);
                movimientos.Add(new MovimientoCuenta(
                    m.Fecha.ToString("yyyy-MM-dd"),
                    m.Comprobante,
                    m.Concepto,
                    m.Monto,
                    m.Pago,
                    saldo));
            }

            MovimientosCuenta = movimientos;
            return true;
        }

        private static string NormalizarCuit(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor)) return string.Empty;
            valor = valor.Trim();

            // Si viene como "({CUIT}) Razón Social" extraer lo que está entre paréntesis
            var ini = valor.IndexOf('(');
            var fin = valor.IndexOf(')');
            if (ini >= 0 && fin > ini)
            {
                return valor.Substring(ini + 1, fin - ini - 1).Trim();
            }

            return valor;
        }
    }
}
