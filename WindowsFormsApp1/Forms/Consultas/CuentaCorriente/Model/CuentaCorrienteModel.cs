using CAI_Proyecto.Almacenes.Almacen;
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

        //devuelve false si hay algun error.
        internal bool CargarEstadoCuenta(Filtros filtros)
        {
            if (filtros.FechaDesde > filtros.FechaHasta)
            {
                MessageBox.Show("La fecha desde debe ser mayor a la hasta.");
                return false;
            }

            /*No sé cómo voy a hacer esto, así que pongo datos de prueba*/
            //Seria mejor tener un set de datos de prueba para cada uno de los clientes de prueba.
            MovimientosCuenta = new List<MovimientoCuenta>
            {
                new MovimientoCuenta("2025-10-01", "F0001-00001234","Servicio de envío",8450.00m,0.00m,-8450.00m),
                new MovimientoCuenta("2025-10-03", "F0001-00001235","Transporte puerta a puerta",10200.50m,0.00m,-10200.50m),
                new MovimientoCuenta("2025-10-05", "REC-000145","Pago parcial",0.00m,5000.00m,-5200.50m),
                new MovimientoCuenta("2025-10-07", "F0001-00001237","Entrega a cliente final",9150.75m,0.00m,-9150.75m),
                new MovimientoCuenta("2025-10-09", "REC-000146","Pago total",0.00m,11800.00m,0.00m)
            };

            return true;
        }
    }
}
