using CAI_Proyecto.Almacenes.Almacen;
using CAI_Proyecto.Almacenes.Entidad;
using CAI_Proyecto.Almacenes.ClaseAuxiliar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CAI_Proyecto.Forms.Operacion.AdmitirEnCD.Model
{
    public class AdmitirEnCDModel
    {
        public Cliente[] Clientes
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

        public Provincia[] Provincias
        {
            get
            {
                return ProvinciaAlmacen.Provincias
                    .Select(p => new Provincia
                    {
                        Codigo = p.IdProvincia,
                        Nombre = p.Nombre,
                    }).ToArray();
            }
        }

        public Dimension[] Dimensiones => new Dimension[]
        {
            new Dimension{ Tamaño = "S" },
            new Dimension{ Tamaño = "M" },
            new Dimension{ Tamaño = "L" },
            new Dimension{ Tamaño = "XL" }
        };

        public IEnumerable<AgenciaEnvio> AgenciasEnvioPorProvincia(string provinciaCodigo)
        {
            var provincia = ProvinciaAlmacen.Provincias.Single(p => p.IdProvincia == provinciaCodigo);
            var cd = CentroDeDistribucionAlmacen.CentrosDeDistribucion.Single(cd => cd.IdCD == provincia.IdCD);

            return AgenciaAlmacen.Agencias
                                 .Where(a => a.CDAsignado == cd.IdCD)
                                 .Select(a => new AgenciaEnvio
                                 {
                                     Id = a.IdAgencia,
                                     Nombre = a.Nombre,
                                     ProvinciaCodigo = provinciaCodigo,
                                 });
        }

        public List<EncomiendaItem> Encomiendas { get; } = new List<EncomiendaItem>();

        public void AgregarEncomienda(EncomiendaItem encomienda)
        {
            Encomiendas.Add(encomienda);
        }

        public void QuitarEncomienda(EncomiendaItem encomienda)
        {
            Encomiendas.Remove(encomienda);
        }

        public List<string> ValidarPedido(Pedido p)
        {
            var errores = new List<string>();

            if (p.Cliente == null)
                errores.Add("Debe seleccionar un cliente.");

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

        public void Aceptar(Pedido pedido)
        {
            if (pedido == null) return;

            // Provincia destino (puede ser null si no aplica)
            var provEntidad = ProvinciaAlmacen.Provincias
                .FirstOrDefault(p => pedido.ProvinciaEnvio != null &&
                                     p.IdProvincia == pedido.ProvinciaEnvio.Codigo);

            // En el caso de "Admitir en CD" no existe retiro: se fija en SinRetiro.
            var tipoRetiroEnum = TipoRetiroEnum.SinRetiro;

            // Estado inicial al admitir en el CD origen.
            var estadoInicial = TipoEstadoGuiaEnum.AdmitidoCDOrigen;

            // Mapeo del tipo de envío (igual que en ImponerCallCenterModel).
            var tipoEnvioEnum = pedido.TipoEnvio switch
            {
                "Domicilio" => TipoEnvioEnum.VaADomicilio,
                "Centro de distribución" => TipoEnvioEnum.VaACD,
                "Agencia" => TipoEnvioEnum.VaAAgencia,
                _ => TipoEnvioEnum.VaADomicilio
            };

            var dniDestinatario = int.TryParse(pedido.DniDestinatario, out var dniParsed) ? dniParsed : 0;

            var guias =
                (pedido.Encomiendas ?? Enumerable.Empty<EncomiendaItem>())
                .Where(enc => enc != null && enc.Cantidad > 0)
                .SelectMany(enc => Enumerable.Range(0, enc.Cantidad).Select(_ =>
                {
                    var now = DateTime.Now;

                    var dimEnum = (enc.Dimension?.Tamaño ?? "S") switch
                    {
                        "S" => TipoBultoEnum.S,
                        "M" => TipoBultoEnum.M,
                        "L" => TipoBultoEnum.L,
                        "XL" => TipoBultoEnum.XL,
                        _ => TipoBultoEnum.S
                    };

                    return new GuiaEntidad
                    {
                        NumeroGuia = GenerarNumeroGuia(),
                        FechaIngreso = now,
                        CuitCliente = pedido.Cliente?.Cuit,
                        TipoRetiro = tipoRetiroEnum,
                        TipoEnvio = tipoEnvioEnum,
                        AgenciaOrigen = 0, // No hay agencia de retiro en este flujo.
                        AgenciaDestino = pedido.AgenciaEnvio?.Id ?? 0,
                        LocalidadDestino = pedido.LocalidadDestinatario,
                        DomicilioDestino = pedido.DomicilioDestinatario,
                        IdProvinciaDestino = provEntidad?.IdProvincia,
                        IdCDDestino = provEntidad?.IdCD,
                        IdCDOrigen = provEntidad?.IdCD,
                        DniDestinatario = dniDestinatario,
                        Dimension = dimEnum,
                        Estado = estadoInicial,
                        Recorrido = new List<Recorrido>(),
                        EstadoActual = new EstadoGuia
                        {
                            Estado = estadoInicial,
                            Fecha = now,
                            IdCentroDistribucion = provEntidad?.IdCD
                        },
                        Historial = new List<EstadoGuia>
                        {
                            new EstadoGuia
                            {
                                Estado = estadoInicial,
                                Fecha = now,
                                IdCentroDistribucion = provEntidad?.IdCD
                            }
                        },
                        Precio = 0m,
                        ComisionesAgenciaOrigen = 0m,
                        ComisionesAgenciaDestino = 0m,
                        ComisionesFleteroOrigen = 0m,
                        ComisionesFleteroDestino = 0m,
                        NumeroFactura = 0
                    };
                }));

            foreach (var guia in guias)
            {
                GuiaAlmacen.Agregar(guia);
            }
        }

        private string GenerarNumeroGuia()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(100, 999).ToString();
        }
    }
}
