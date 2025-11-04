using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;


namespace CAI_Proyecto.Forms.Consultas.ResultadosCostosVentas.Model
{
    internal class ResultadosCostosVentasModel
    {
        public IReadOnlyList<ResultadoCostoVenta> Resultados { get; private set; } = new List<ResultadoCostoVenta>();

        // Movimientos diarios por empresa)
        private readonly List<(DateTime Fecha, string Empresa, int Envios, decimal Costo, decimal Venta)> _movs =
            new List<(DateTime, string, int, decimal, decimal)>
        {
            (new DateTime(2025,10,01), "Unilever",  12,  820000m, 1180000m),
            (new DateTime(2025,10,05), "Unilever",   4,  210000m,  295000m),
            (new DateTime(2025,10,03), "Arcor",     10,  760000m,  990000m),
            (new DateTime(2025,10,08), "Arcor",      3,  120000m,  170000m),
            (new DateTime(2025,10,02), "Danone",     5,  260000m,  360000m),
            (new DateTime(2025,10,09), "Danone",     2,   90000m,  130000m),
        };

        private readonly CultureInfo _ars = CultureInfo.GetCultureInfo("es-AR");

        public string FormatearMoneda(decimal valor)
        {
            return valor.ToString("C", _ars);
        }

        public bool Buscar(Filtros filtros)
        {
            if (filtros.Desde.Date > filtros.Hasta.Date)
            {
                MessageBox.Show("El rango de fechas no es válido. Verifique los datos e intente nuevamente.",
                    "Rango inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Resultados = new List<ResultadoCostoVenta>();
                return false;
            }

            var q = _movs
                .Where(m => m.Fecha.Date >= filtros.Desde.Date && m.Fecha.Date <= filtros.Hasta.Date)
                .GroupBy(m => m.Empresa)
                .Select(g => new ResultadoCostoVenta(
                    empresa: g.Key,
                    envios: g.Sum(x => x.Envios),
                    costoTotal: g.Sum(x => x.Costo),
                    ventasTotales: g.Sum(x => x.Venta)))
                .OrderBy(r => r.Empresa)
                .ToList();

            if (!q.Any())
            {
                MessageBox.Show("No se registran movimientos en el período seleccionado.",
                    "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Resultados = new List<ResultadoCostoVenta>();
                return false;
            }

            Resultados = q;
            return true;
        }
    }
}