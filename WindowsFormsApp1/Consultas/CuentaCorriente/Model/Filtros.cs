using System;

namespace WindowsFormsApp1.Consultas.CuentaCorriente.Model
{
    internal class Filtros
    {
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public string CuitCliente { get; set; }
    }
}
