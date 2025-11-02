using System.Collections.Generic;

namespace CAI_Proyecto.Forms.Operacion.EmitirFactura.Model
{
    public class EmitirFacturaModel
    {

        public IReadOnlyList<ListadoFacturacion> ListadoFacturacion { get; set; }

        public Cliente[] Empresas => new Cliente[]
        {
            new Cliente { Cuit = "30-50109269-6", RazonSocial = "Unilever de Argentina S.A." },
            new Cliente { Cuit = "30-50361405-3", RazonSocial = "Arcor S.A.I.C." },
            new Cliente { Cuit = "30-70752101-7", RazonSocial = "Molinos Río de la Plata S.A." },
            new Cliente { Cuit = "30-50033372-9", RazonSocial = "Coca-Cola FEMSA S.A." },
            new Cliente { Cuit = "30-56712390-1", RazonSocial = "Procter & Gamble S.R.L." },
            new Cliente { Cuit = "30-58412999-2", RazonSocial = "Ledesma S.A.A.I." },
            new Cliente { Cuit = "30-70012345-8", RazonSocial = "Nestlé Argentina S.A." },
            new Cliente { Cuit = "30-66544332-7", RazonSocial = "Danone S.A." }
        };



        public EmitirFacturaModel()
        {
            ListadoFacturacion = new List<ListadoFacturacion>
            {
                new ListadoFacturacion("30-50109269-6  Unilever de Argentina S.A.", "2001", "Desde CD a domicilio", 8200.00m),
                new ListadoFacturacion("30-50361405-3  Arcor S.A.I.C.", "2002", "Desde CD a CD", 6500.00m),
                new ListadoFacturacion("30-70752101-7  Molinos Río de la Plata S.A.", "2003", "Desde agencia a domicilio", 9700.00m),
                new ListadoFacturacion("30-50033372-9  Coca-Cola FEMSA S.A.", "2004", "Desde agencia a domicilio", 11300.00m),
                new ListadoFacturacion("30-56712390-1  Procter & Gamble S.R.L.", "2005", "Desde CD a agencia", 9800.00m)
            };
        }
    }
}
