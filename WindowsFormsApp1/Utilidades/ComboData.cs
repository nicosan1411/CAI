using System.Collections.Generic;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Catálogos estáticos y datos de ejemplo para los formularios del sistema.
    /// Estos datos están hardcodeados con propósito demostrativo.
    /// </summary>
    internal static class ComboData
    {
        // ============================
        //   CATÁLOGOS PRINCIPALES
        // ============================

        public static IReadOnlyList<string> Empresas => new[]
        {
            "30-50109269-6  Unilever de Argentina S.A.",
            "30-50361405-3  Arcor S.A.I.C.",
            "30-70752101-7  Molinos Río de la Plata S.A.",
            "30-50033372-9  Coca-Cola FEMSA S.A.",
            "30-56712390-1  Procter & Gamble S.R.L.",
            "30-58412999-2  Ledesma S.A.A.I.",
            "30-70012345-8  Nestlé Argentina S.A.",
            "30-66544332-7  Danone S.A."
        };

        public static IReadOnlyList<string> Dimensiones => new[] { "XS", "S", "M", "L", "XL" };

        public static IReadOnlyList<string> Provincias => new[]
        {
            "Buenos Aires","CABA","Catamarca","Chaco","Chubut","Córdoba","Corrientes",
            "Entre Ríos","Formosa","Jujuy","La Pampa","La Rioja","Mendoza","Misiones",
            "Neuquén","Río Negro","Salta","San Juan","San Luis","Santa Cruz","Santa Fe",
            "Santiago del Estero","Tierra del Fuego","Tucumán"
        };

        public static IReadOnlyList<string> AgenciasRetiro => new[] { "Agencia 1", "Agencia 2", "Agencia 3" };

        public static IReadOnlyList<string> AgenciasEnvio => new[] { "Agencia A", "Agencia B", "Agencia C" };

        public static IReadOnlyList<string> Patentes => new[]
        {
            "AA123BB","AC456DD","AE789FF","AF321GG","AH654HH"
        };

        public static IReadOnlyList<string> Fleteros => new[]
        {
            "Juan Pérez - ABC123 - Flete 01",
            "María Gómez - JKL456 - Flete 02",
            "Carlos Ruiz - MNO789 - Flete 03",
            "Laura Fernández - PQR321 - Flete 04",
            "Diego Torres - STU654 - Flete 05"
        };

        // ============================
        //   ENCOMIENDAS A ENTREGAR (CU 5)
        // ============================

        public static IReadOnlyList<EntregaDemo> EntregasDemo => new List<EntregaDemo>
        {
            new EntregaDemo("1001", "Carlos Pérez", "32455678"),
            new EntregaDemo("1002", "María Gómez", "29877654"),
            new EntregaDemo("1003", "Laura Fernández", "31233987"),
            new EntregaDemo("1004", "Juan Torres", "33122345")
        };

        internal class EntregaDemo
        {
            public string NroGuia { get; set; }
            public string Destinatario { get; set; }
            public string DNI { get; set; }

            public EntregaDemo(string nroGuia, string destinatario, string dni)
            {
                NroGuia = nroGuia;
                Destinatario = destinatario;
                DNI = dni;
            }
        }

        // ============================
        //   FACTURAS DEMO (CU 6)
        // ============================

        public static IReadOnlyList<FacturaDemo> FacturasDemo => new List<FacturaDemo>
        {
            new FacturaDemo("30-50109269-6  Unilever de Argentina S.A.", "2001", "Flete y entrega a domicilio", 8200.00m),
            new FacturaDemo("30-50361405-3  Arcor S.A.I.C.", "2002", "Servicio de transporte regional", 6500.00m),
            new FacturaDemo("30-70752101-7  Molinos Río de la Plata S.A.", "2003", "Entrega en CD destino", 9700.00m),
            new FacturaDemo("30-50033372-9  Coca-Cola FEMSA S.A.", "2004", "Entrega a cliente final", 11300.00m),
            new FacturaDemo("30-56712390-1  Procter & Gamble S.R.L.", "2005", "Transporte nacional", 9800.00m)
        };

        internal class FacturaDemo
        {
            public string Cliente { get; set; }
            public string NroGuia { get; set; }
            public string Concepto { get; set; }
            public decimal Monto { get; set; }

            public FacturaDemo(string cliente, string nroGuia, string concepto, decimal monto)
            {
                Cliente = cliente;
                NroGuia = nroGuia;
                Concepto = concepto;
                Monto = monto;
            }
        }

        // ============================
        //   MOVIMIENTOS (CU 7)
        // ============================

        public static IReadOnlyList<MovimientoCuenta> MovimientosCuenta => new List<MovimientoCuenta>
        {
            new MovimientoCuenta("2025-10-01","Unilever de Argentina S.A.","F0001-00001234","Servicio de envío",8450.00m,0.00m,-8450.00m),
            new MovimientoCuenta("2025-10-03","Arcor S.A.I.C.","F0001-00001235","Transporte puerta a puerta",10200.50m,0.00m,-10200.50m),
            new MovimientoCuenta("2025-10-05","Molinos Río de la Plata S.A.","REC-000145","Pago parcial",0.00m,5000.00m,-5200.50m),
            new MovimientoCuenta("2025-10-07","Coca-Cola FEMSA S.A.","F0001-00001237","Entrega a cliente final",9150.75m,0.00m,-9150.75m),
            new MovimientoCuenta("2025-10-09","Procter & Gamble S.R.L.","REC-000146","Pago total",0.00m,11800.00m,0.00m)
        };

        internal class MovimientoCuenta
        {
            public string Fecha { get; set; }
            public string Cliente { get; set; }
            public string Comprobante { get; set; }
            public string Concepto { get; set; }
            public decimal Monto { get; set; }
            public decimal Pago { get; set; }
            public decimal Saldo { get; set; }

            public MovimientoCuenta(string fecha, string cliente, string comprobante, string concepto, decimal monto, decimal pago, decimal saldo)
            {
                Fecha = fecha;
                Cliente = cliente;
                Comprobante = comprobante;
                Concepto = concepto;
                Monto = monto;
                Pago = pago;
                Saldo = saldo;
            }
        }

        // =================================
        //   RESULTADOS COSTOS–VENTAS (CU 8)
        // =================================

        public static IReadOnlyList<ResultadoCostoVenta> ResultadosCostosVentas => new List<ResultadoCostoVenta>
        {
            new ResultadoCostoVenta("Unilever de Argentina S.A.", "2025-10-01", "2025-10-10", 240, 845000m, 1020000m),
            new ResultadoCostoVenta("Arcor S.A.I.C.", "2025-10-02", "2025-10-09", 120, 512000m, 750000m),
            new ResultadoCostoVenta("Molinos Río de la Plata S.A.", "2025-10-03", "2025-10-07", 80, 305000m, 418000m),
            new ResultadoCostoVenta("Coca-Cola FEMSA S.A.", "2025-10-05", "2025-10-08", 150, 410000m, 590000m),
            new ResultadoCostoVenta("Procter & Gamble S.R.L.", "2025-10-06", "2025-10-11", 200, 680000m, 935000m)
        };

        internal class ResultadoCostoVenta
        {
            public string Empresa { get; set; }
            public string FechaDesde { get; set; }
            public string FechaHasta { get; set; }
            public int Envios { get; set; }
            public decimal CostoTotal { get; set; }
            public decimal VentasTotales { get; set; }

            public ResultadoCostoVenta(string empresa, string fechaDesde, string fechaHasta,
                                       int envios, decimal costoTotal, decimal ventasTotales)
            {
                Empresa = empresa;
                FechaDesde = fechaDesde;
                FechaHasta = fechaHasta;
                Envios = envios;
                CostoTotal = costoTotal;
                VentasTotales = ventasTotales;
            }
        }

        // ============================
        //   GUÍAS DEMO (CU 9)
        // ============================

        public static IReadOnlyList<GuiaDemo> GuiasDemo => new List<GuiaDemo>
        {
            new GuiaDemo("1001", "M", "Centro de Distribución Rosario", "Envío a domicilio", "32455678", "2025-10-05", "Admitida en CD destino"),
            new GuiaDemo("1002", "L", "Centro de Distribución CABA", "Envío a domicilio", "33122345", "2025-10-07", "Entregada a cliente"),
            new GuiaDemo("1003", "S", "Centro de Distribución Córdoba", "Envío a agencia", "29877654", "2025-10-08", "En tránsito"),
            new GuiaDemo("1004", "M", "Centro de Distribución Tucumán", "Envío a domicilio", "31233987", "2025-10-09", "Impuesta")
        };

        internal class GuiaDemo
        {
            public string Numero { get; set; }
            public string Dimension { get; set; }
            public string CDDestino { get; set; }
            public string TipoEnvio { get; set; }
            public string DniDestinatario { get; set; }
            public string FechaIngreso { get; set; }
            public string Estado { get; set; }

            public GuiaDemo(string numero, string dimension, string cdDestino, string tipoEnvio,
                            string dniDestinatario, string fechaIngreso, string estado)
            {
                Numero = numero;
                Dimension = dimension;
                CDDestino = cdDestino;
                TipoEnvio = tipoEnvio;
                DniDestinatario = dniDestinatario;
                FechaIngreso = fechaIngreso;
                Estado = estado;
            }
        }
    }
}
