using System;
using CAI_Proyecto.Almacenes.Almacen;
using CAI_Proyecto.Almacenes.ClaseAuxiliar;
using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.Linq;

namespace CAI_Proyecto.Forms.Operacion.ImponerCallCenter.Model
{
    internal partial class ImponerCallCenterModel
    {

        public List<dynamic> Clientes
        {
            get
            {
                return ClienteAlmacen.Clientes
                    .Select(c => new
                    {
                        Cuit_RazonSocial = new Cliente
                        {
                            Cuit = c.Cuit,
                            RazonSocial = c.RazonSocial
                        }.Cuit_RazonSocial,
                        Entidad = c
                    })
                    .OrderBy(x => x.Cuit_RazonSocial)
                    .ToList<dynamic>();
            }
        }

        public List<ProvinciaEntidad> Provincias
        {
            get
            {
                return ProvinciaAlmacen.Provincias
                    .OrderBy(p => p.Nombre)
                    .ToList();
            }
        }

        public List<string> Dimensiones
        {
            get
            {
                return Enum.GetValues(typeof(TipoBultoEnum))
                    .Cast<TipoBultoEnum>()
                    .Select(d => d.ToString())
                    .ToList();
            }
        }

        public IEnumerable<AgenciaEntidad> AgenciasDeRetiroPorCliente(ClienteEntidad cliente)
        {
            if (cliente == null || cliente.AgenciasAsociadas == null || !cliente.AgenciasAsociadas.Any())
                return Enumerable.Empty<AgenciaEntidad>();

            var agencias = AgenciaAlmacen.Agencias
                .Where(a => cliente.AgenciasAsociadas.Contains(a.IdAgencia))
                .OrderBy(a => a.Nombre)
                .ToList();

            return agencias;
        }

        public IEnumerable<AgenciaEntidad> AgenciasDeEnvioPorProvincia(ProvinciaEntidad provincia)
        {
            if (provincia == null || string.IsNullOrWhiteSpace(provincia.IdCD))
                return Enumerable.Empty<AgenciaEntidad>();

            var agencias = AgenciaAlmacen.Agencias
                .Where(a => a.CDAsignado == provincia.IdCD)
                .OrderBy(a => a.Nombre)
                .ToList();

            return agencias;
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

        public void LimpiarEncomiendas()
        {
            Encomiendas.Clear();
        }

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

        /*
         * Método de negocio: crea una o varias GuiaEntidad desde un Pedido validado y las persiste.
         * Se genera una Guia por cada unidad (Cantidad) en cada Encomienda.
         * Devuelve lista de errores (vacía si se guardó correctamente).
         */
        public List<string> ImponerPedido(Pedido p)
        {
            var errores = ValidarPedido(p);
            if (errores.Any())
                return errores;

            // Crear guías por cada unidad de encomienda
            var guiasGeneradas = new List<GuiaEntidad>();

            // Valores comunes base
            var baseCuit = p.Cliente?.Cuit;
            var agenciaOrigenId = p.AgenciaRetiro != null ? p.AgenciaRetiro.Id : 0;
            var agenciaDestinoId = p.AgenciaEnvio != null ? p.AgenciaEnvio.Id : 0;
            var idProvinciaDestino = p.ProvinciaEnvio?.Codigo ?? p.ProvinciaEnvio?.Nombre;
            var idCDDestino = p.ProvinciaEnvio != null ? (p.ProvinciaEnvio.Codigo ?? p.ProvinciaEnvio.Nombre) : null;
            var idCDOrigen = p.AgenciaRetiro != null ? (p.AgenciaRetiro.Id.ToString()) : null; // si no hay CD, queda null
            var tipoRetiroEnum = p.TipoRetiro == "Agencia" ? TipoRetiroEnum.DesdeAgencia :
                                 p.TipoRetiro == "Domicilio" ? TipoRetiroEnum.DesdeDomicilio :
                                 TipoRetiroEnum.SinRetiro;
            var tipoEnvioEnum = p.TipoEnvio == "Agencia" ? TipoEnvioEnum.VaAAgencia :
                                p.TipoEnvio == "Centro de distribución" ? TipoEnvioEnum.VaACD :
                                TipoEnvioEnum.VaADomicilio;
            var dni = int.TryParse(p.DniDestinatario, out var dniVal) ? dniVal : 0;
            var localidad = p.LocalidadDestinatario;
            var domicilio = p.DomicilioDestinatario;

            foreach (var encomienda in p.Encomiendas)
            {
                var cantidad = encomienda?.Cantidad ?? 0;
                var tamaño = encomienda?.Dimension?.Tamaño;

                for (int i = 0; i < cantidad; i++)
                {
                    var guia = new GuiaEntidad
                    {
                        NumeroGuia = GenerarNumeroGuia(),
                        FechaIngreso = DateTime.Now,
                        CuitCliente = baseCuit,
                        AgenciaOrigen = agenciaOrigenId,
                        AgenciaDestino = agenciaDestinoId,
                        IdCDOrigen = idCDOrigen,
                        IdCDDestino = idCDDestino,
                        TipoRetiro = tipoRetiroEnum,
                        TipoEnvio = tipoEnvioEnum,
                        DniDestinatario = dni,
                        IdProvinciaDestino = idProvinciaDestino,
                        LocalidadDestino = localidad,
                        DomicilioDestino = domicilio,
                        Dimension = TryParseTipoBulto(tamaño),
                        Recorrido = new List<Recorrido>(),
                        Precio = 0m,
                        ComisionesAgenciaOrigen = 0m,
                        ComisionesAgenciaDestino = 0m,
                        ComisionesFleteroOrigen = 0m,
                        ComisionesFleteroDestino = 0m,
                        NumeroFactura = 0
                    };

                    // Estado inicial según tipo de retiro
                    var estadoInicial = guia.TipoRetiro switch
                    {
                        TipoRetiroEnum.DesdeDomicilio => TipoEstadoGuiaEnum.EnBusquedaRetiroDomicilio,
                        TipoRetiroEnum.DesdeAgencia => TipoEstadoGuiaEnum.EnBusquedaRetiroAgencia,
                        _ => TipoEstadoGuiaEnum.AdmitidoCDOrigen
                    };
                    guia.Estado = estadoInicial;
                    guia.EstadoActual = new EstadoGuia
                    {
                        Estado = estadoInicial,
                        Fecha = DateTime.Now,
                        IdCentroDistribucion = guia.IdCDOrigen
                    };
                    guia.Historial = new List<EstadoGuia> { guia.EstadoActual };

                    guiasGeneradas.Add(guia);

                    // Persistir inmediatamente (cada Agregar hace Grabar())
                    GuiaAlmacen.Agregar(guia);
                }
            }

            return new List<string>();
        }

        // ----- Helpers privados -----
        private string GenerarNumeroGuia()
        {
            return $"G-{DateTime.Now:yyyyMMddHHmmssfff}";
        }

        private TipoBultoEnum TryParseTipoBulto(string tamaño)
        {
            if (string.IsNullOrWhiteSpace(tamaño)) return TipoBultoEnum.S;
            if (Enum.TryParse<TipoBultoEnum>(tamaño, true, out var result)) return result;
            var c = tamaño.Trim().ToUpperInvariant()[0];
            return c switch
            {
                'M' => TipoBultoEnum.M,
                'L' => TipoBultoEnum.L,
                'X' => TipoBultoEnum.XL,
                _ => TipoBultoEnum.S
            };
        }
    }
}