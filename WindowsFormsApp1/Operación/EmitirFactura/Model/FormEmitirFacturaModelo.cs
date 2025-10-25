using System.Collections.Generic;

namespace WindowsFormsApp1.Operación.EmitirFactura.Model
{
    public class FormEmitirFacturaModelo
    {

        public IReadOnlyList<ListadoFacturacion> ListadoFacturacion { get; set; }

        public ClienteFactura[] Empresas => new ClienteFactura[]
        {
            new ClienteFactura { Cuit = "30-50109269-6", RazonSocial = "Unilever de Argentina S.A." },
            new ClienteFactura { Cuit = "30-50361405-3", RazonSocial = "Arcor S.A.I.C." },
            new ClienteFactura { Cuit = "30-70752101-7", RazonSocial = "Molinos Río de la Plata S.A." },
            new ClienteFactura { Cuit = "30-50033372-9", RazonSocial = "Coca-Cola FEMSA S.A." },
            new ClienteFactura { Cuit = "30-56712390-1", RazonSocial = "Procter & Gamble S.R.L." },
            new ClienteFactura { Cuit = "30-58412999-2", RazonSocial = "Ledesma S.A.A.I." },
            new ClienteFactura { Cuit = "30-70012345-8", RazonSocial = "Nestlé Argentina S.A." },
            new ClienteFactura { Cuit = "30-66544332-7", RazonSocial = "Danone S.A." }
        };



        public FormEmitirFacturaModelo()
        {
            ListadoFacturacion = new List<ListadoFacturacion>
            {
                new ListadoFacturacion("30-50109269-6  Unilever de Argentina S.A.", "2001", "Flete y entrega a domicilio", 8200.00m),
                new ListadoFacturacion("30-50361405-3  Arcor S.A.I.C.", "2002", "Servicio de transporte regional", 6500.00m),
                new ListadoFacturacion("30-70752101-7  Molinos Río de la Plata S.A.", "2003", "Entrega en CD destino", 9700.00m),
                new ListadoFacturacion("30-50033372-9  Coca-Cola FEMSA S.A.", "2004", "Entrega a cliente final", 11300.00m),
                new ListadoFacturacion("30-56712390-1  Procter & Gamble S.R.L.", "2005", "Transporte nacional", 9800.00m)
            };
        }
    }
}
