using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Consultas.CuentaCorriente
{
    public class MovimientoCuenta
    {
        public string Fecha { get; set; }
        public string Comprobante { get; set; }
        public string Concepto { get; set; }
        public decimal Monto { get; set; }
        public decimal Pago { get; set; }
        public decimal Saldo { get; set; }

        public MovimientoCuenta(string fecha, string comprobante, string concepto, decimal monto, decimal pago, decimal saldo)
        {
            Fecha = fecha;
            Comprobante = comprobante;
            Concepto = concepto;
            Monto = monto;
            Pago = pago;
            Saldo = saldo;
        }
    }
}
