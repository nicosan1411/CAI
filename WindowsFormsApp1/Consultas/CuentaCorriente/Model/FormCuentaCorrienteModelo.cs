using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Consultas.CuentaCorriente
{
    public class FormCuentaCorrienteModelo
    {
        public IReadOnlyList<MovimientoCuenta> MovimientosCuenta { get; set; }

        public Cliente[] Empresas => new Cliente[]
        {
            new Cliente{ Cuit = "30-50109269-6", RazonSocial = "Unilever de Argentina S.A." },
            new Cliente{ Cuit = "30-50361405-3", RazonSocial = "Arcor S.A.I.C."},
            new Cliente{ Cuit = "30-70752101-7", RazonSocial = "Molinos Río de la Plata S.A."},
            new Cliente{ Cuit = "30-50033372-9", RazonSocial = "Coca-Cola FEMSA S.A."},
            new Cliente{ Cuit = "30-56712390-1", RazonSocial = "Procter & Gamble S.R.L."},
            new Cliente{ Cuit = "30-58412999-2", RazonSocial = "Ledesma S.A.A.I."},
            new Cliente{ Cuit = "30-70012345-8", RazonSocial = "Nestlé Argentina S.A."},
            new Cliente{ Cuit = "30-66544332-7", RazonSocial = "Danone S.A."}
        };

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
