using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using CAI_Proyecto.Almacenes.Almacen;
using CAI_Proyecto.Almacenes.Entidad;
using CAI_Proyecto.Almacenes.ClaseAuxiliar;

namespace CAI_Proyecto.Forms.Consultas.ResultadosCostosVentas.Model
{
    internal class ResultadosCostosVentasModel
    {
        public IReadOnlyList<ResultadoCostoVenta> Resultados { get; private set; } = new List<ResultadoCostoVenta>();

        private readonly CultureInfo _ars = CultureInfo.GetCultureInfo("es-AR");

        public string FormatearMoneda(decimal valor)
        {
            return valor.ToString("C", _ars);
        }

        public bool Buscar(Filtros filtros)
        {
            // Validación de rango
            if (filtros.Desde.Date > filtros.Hasta.Date)
            {
                MessageBox.Show("El rango de fechas no es válido. Verifique los datos e intente nuevamente.",
                    "Rango inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Resultados = new List<ResultadoCostoVenta>();
                return false;
            }

            // Estado de facturación
            var estadoFacturado = TipoEstadoGuiaEnum.Facturado;

            // Guias facturadas dentro del rango (se exige al menos un registro de historial con estado Facturado en el rango)
            var guiasFacturadas = GuiaAlmacen.Guias
                .Where(g => g.Historial != null &&
                            g.Historial.Any(h =>
                                h.Estado == estadoFacturado &&
                                h.Fecha.Date >= filtros.Desde.Date &&
                                h.Fecha.Date <= filtros.Hasta.Date))
                .ToList();

            if (!guiasFacturadas.Any())
            {
                MessageBox.Show("No se registran guías facturadas en el período seleccionado.",
                    "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Resultados = new List<ResultadoCostoVenta>();
                return false;
            }

            // Join con clientes para obtener razón social y agrupar
            var clientes = ClienteAlmacen.Clientes.ToDictionary(c => c.Cuit, c => c.RazonSocial);

            var resultados = guiasFacturadas
                .GroupBy(g =>
                {
                    // Si no se encuentra el cliente se usa el CUIT como identificador
                    return clientes.TryGetValue(g.CuitCliente, out var rs) ? rs : g.CuitCliente;
                })
                .Select(grp => new ResultadoCostoVenta(
                    empresa: grp.Key,
                    envios: grp.Count(),
                    costoTotal: grp.Sum(g =>
                        (g.ComisionesAgenciaOrigen) +
                        (g.ComisionesAgenciaDestino) +
                        (g.ComisionesFleteroOrigen) +
                        (g.ComisionesFleteroDestino)),
                    ventasTotales: grp.Sum(g => g.Precio)
                ))
                .OrderBy(r => r.Empresa)
                .ToList();

            if (!resultados.Any())
            {
                MessageBox.Show("No se generaron resultados para los criterios indicados.",
                    "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Resultados = new List<ResultadoCostoVenta>();
                return false;
            }

            Resultados = resultados;
            return true;
        }
    }
}