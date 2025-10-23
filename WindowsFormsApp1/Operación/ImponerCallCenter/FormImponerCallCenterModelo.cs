using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Consultas.CuentaCorriente;

namespace WindowsFormsApp1.Operación.ImponerCallCenter
{
    internal class FormImponerCallCenterModelo
    {
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

        public Agencia[] Agencia => new Agencia[] 
        {
            new Agencia{ Nombre = "Agencia Central" },
            new Agencia{ Nombre = "Agencia Norte" },
            new Agencia{ Nombre = "Agencia Sur" },
            new Agencia{ Nombre = "Agencia Este" },
            new Agencia{ Nombre = "Agencia Oeste" }
        };

        public Provincia[] Provincia => new Provincia[]
        {
            new Provincia{ Nombre = "Buenos Aires" },
            new Provincia{ Nombre = "Córdoba" },
            new Provincia{ Nombre = "Santa Fe" },
            new Provincia{ Nombre = "Mendoza" },
            new Provincia{ Nombre = "Tucumán" },
            new Provincia{ Nombre = "Salta" },
            new Provincia{ Nombre = "Chaco" },
            new Provincia{ Nombre = "Entre Ríos" }
        };
    }
}
