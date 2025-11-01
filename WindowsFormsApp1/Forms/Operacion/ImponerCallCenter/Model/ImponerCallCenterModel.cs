using System.Collections.Generic;
using System.Linq;

namespace CAI_Proyecto.Forms.Operacion.ImponerCallCenter.Model
{
    public class ImponerCallCenterModel
    {
        public Cliente[] Clientes => new Cliente[]
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

        public AgenciaRetiro[] AgenciasRetiro => new[]
        {
            new AgenciaRetiro{ Id = 101, Nombre = "Retiro - Sucursal A" },
            new AgenciaRetiro{ Id = 102, Nombre = "Retiro - Sucursal B" },
            new AgenciaRetiro{ Id = 103, Nombre = "Retiro - Sucursal C" },
        };

        public AgenciaEnvio[] AgenciasEnvio => new AgenciaEnvio[] 
        {
            new AgenciaEnvio{ Id = 1, Nombre = "Agencia 1", ProvinciaCodigo = "BA" },
            new AgenciaEnvio{ Id = 2, Nombre = "Agencia 2", ProvinciaCodigo = "CO" },
            new AgenciaEnvio{ Id = 3, Nombre = "Agencia 3", ProvinciaCodigo = "SF" },
            new AgenciaEnvio{ Id = 4, Nombre = "Agencia 4", ProvinciaCodigo = "ME" },
            new AgenciaEnvio{ Id = 5, Nombre = "Agencia 5", ProvinciaCodigo = "TU" },
            new AgenciaEnvio{ Id = 6, Nombre = "Agencia 6", ProvinciaCodigo = "SA" },
            new AgenciaEnvio{ Id = 7, Nombre = "Agencia 7", ProvinciaCodigo = "CH" },
            new AgenciaEnvio{ Id = 8, Nombre = "Agencia 8", ProvinciaCodigo = "ER" },
        };

        public Provincia[] Provincias => new Provincia[]
        {
            new Provincia{ Codigo = "BA", Nombre = "Buenos Aires" },
            new Provincia{ Codigo = "CO", Nombre = "Córdoba" },
            new Provincia{ Codigo = "SF", Nombre = "Santa Fe" },
            new Provincia{ Codigo = "ME", Nombre = "Mendoza" },
            new Provincia{ Codigo = "TU", Nombre = "Tucumán" },
            new Provincia{ Codigo = "SA", Nombre = "Salta" },
            new Provincia{ Codigo = "CH", Nombre = "Chaco" },
            new Provincia{ Codigo = "ER", Nombre = "Entre Ríos" }
        };

        public Dimension[] Dimensiones => new Dimension[]
        {
            new Dimension{ Tamaño = "XS" },
            new Dimension{ Tamaño = "S" },
            new Dimension{ Tamaño = "M" },
            new Dimension{ Tamaño = "L" },
            new Dimension{ Tamaño = "XL" }
        };

        public IEnumerable<AgenciaEnvio> AgenciasEnvioPorProvincia(string provinciaCodigo)
            => string.IsNullOrWhiteSpace(provinciaCodigo)
                ? Enumerable.Empty<AgenciaEnvio>()
                : AgenciasEnvio.Where(a => a.ProvinciaCodigo == provinciaCodigo);

        public IEnumerable<AgenciaRetiro> TodasLasAgenciasDeRetiro() => AgenciasRetiro;

        /*
         * Reglas de negocio del form para aceptar un pedido.
         * Devuelve lista de errores. Si la lista está vacía, el pedido es válido.
         */
        public List<string> ValidarPedido(Pedido p)
        {
            var errores = new List<string>();

            if (p.Cliente == null)
                errores.Add("Debe seleccionar un cliente.");

            // Tipo de retiro
            if (string.IsNullOrWhiteSpace(p.TipoRetiro))
                errores.Add("Debe seleccionar el tipo de retiro.");
            else if (p.TipoRetiro == "Agencia" && p.AgenciaRetiro == null)
                errores.Add("Debe seleccionar la agencia de retiro.");

            // Tipo de envío
            switch (p.TipoEnvio)
            {
                case "Domicilio":
                    if (p.ProvinciaEnvio == null) errores.Add("Debe seleccionar la provincia de envío.");
                    if (string.IsNullOrWhiteSpace(p.DniDestinatario)) errores.Add("Debe ingresar el DNI del destinatario.");
                    if (string.IsNullOrWhiteSpace(p.LocalidadDestinatario)) errores.Add("Debe ingresar la localidad del destinatario.");
                    if (string.IsNullOrWhiteSpace(p.DomicilioDestinatario)) errores.Add("Debe ingresar el domicilio del destinatario.");
                    break;

                case "Centro de distribución":
                    if (p.ProvinciaEnvio == null) errores.Add("Debe seleccionar la provincia del centro de distribución.");
                    if (string.IsNullOrWhiteSpace(p.DniDestinatario)) errores.Add("Debe ingresar el DNI del destinatario.");
                    break;

                case "Agencia":
                    if (p.ProvinciaEnvio == null) errores.Add("Debe seleccionar la provincia de la agencia de envío.");
                    if (p.AgenciaEnvio == null) errores.Add("Debe seleccionar la agencia de envío.");
                    if (string.IsNullOrWhiteSpace(p.DniDestinatario)) errores.Add("Debe ingresar el DNI del destinatario.");
                    break;

                default:
                    errores.Add("Debe seleccionar un tipo de envío.");
                    break;
            }

            if (p.Encomiendas == null || p.Encomiendas.Count == 0)
                errores.Add("Debe agregar al menos una encomienda.");

            return errores;
        }
    }
}
