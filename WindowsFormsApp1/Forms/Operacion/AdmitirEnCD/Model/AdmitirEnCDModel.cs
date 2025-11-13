using System;
using System.Collections.Generic;
using System.Linq;
using CAI_Proyecto.Almacenes.Almacen;
using CAI_Proyecto.Almacenes.Entidad;
using CAI_Proyecto.Almacenes.ClaseAuxiliar;

namespace CAI_Proyecto.Forms.Operacion.AdmitirEnCD.Model
{
    public class AdmitirEnCDModel
    {
        public Cliente[] Clientes =>
            ClienteAlmacen.Clientes
                .Select(c => new Cliente { Cuit = c.Cuit, RazonSocial = c.RazonSocial })
                .ToArray();

        public Provincia[] Provincias =>
            ProvinciaAlmacen.Provincias
                .Select(p => new Provincia { Codigo = p.IdProvincia, Nombre = p.Nombre })
                .ToArray();

        public Dimension[] Dimensiones => new[]
        {
            new Dimension{ Tamaño = "S" },
            new Dimension{ Tamaño = "M" },
            new Dimension{ Tamaño = "L" },
            new Dimension{ Tamaño = "XL" }
        };

        public IEnumerable<AgenciaEnvio> AgenciasEnvioPorProvincia(string provinciaCodigo)
        {
            var provincia = ProvinciaAlmacen.Provincias.Single(p => p.IdProvincia == provinciaCodigo);
            var cd = CentroDeDistribucionAlmacen.CentrosDeDistribucion.Single(x => x.IdCD == provincia.IdCD);

            return AgenciaAlmacen.Agencias
                .Where(a => a.CDAsignado == cd.IdCD)
                .Select(a => new AgenciaEnvio
                {
                    Id = a.IdAgencia,
                    Nombre = a.Nombre,
                    ProvinciaCodigo = provinciaCodigo
                });
        }

        public List<EncomiendaItem> Encomiendas { get; } = new List<EncomiendaItem>();
        public void AgregarEncomienda(EncomiendaItem e) => Encomiendas.Add(e);
        public void QuitarEncomienda(EncomiendaItem e) => Encomiendas.Remove(e);

        public List<string> ValidarPedido(Pedido p)
        {
            var errores = new List<string>();
            if (p.Cliente == null) errores.Add("Debe seleccionar un cliente.");
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
            if (p.Encomiendas == null || p.Encomiendas.Count == 0) errores.Add("Debe agregar al menos una encomienda.");
            return errores;
        }

        public void Aceptar(Pedido pedido)
        {
            if (pedido == null) return;

            var provEntidad = ProvinciaAlmacen.Provincias
                .FirstOrDefault(px => pedido.ProvinciaEnvio != null && px.IdProvincia == pedido.ProvinciaEnvio.Codigo);

            string cdOrigen = null;
            var clienteEntidad = ClienteAlmacen.Clientes.FirstOrDefault(c => c.Cuit == pedido.Cliente?.Cuit);
            if (clienteEntidad?.AgenciasAsociadas != null && clienteEntidad.AgenciasAsociadas.Count > 0)
            {
                cdOrigen = clienteEntidad.AgenciasAsociadas
                    .Select(id => AgenciaAlmacen.Agencias.FirstOrDefault(a => a.IdAgencia == id)?.CDAsignado)
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Distinct()
                    .FirstOrDefault();
            }
            if (string.IsNullOrWhiteSpace(cdOrigen))
                cdOrigen = CentroDeDistribucionAlmacen.CentroDeDistribucionActual?.IdCD;
            if (string.IsNullOrWhiteSpace(cdOrigen) && pedido.AgenciaEnvio != null)
                cdOrigen = AgenciaAlmacen.Agencias.FirstOrDefault(a => a.IdAgencia == pedido.AgenciaEnvio.Id)?.CDAsignado;
            if (string.IsNullOrWhiteSpace(cdOrigen))
                cdOrigen = provEntidad?.IdCD;
            if (string.IsNullOrWhiteSpace(cdOrigen))
                cdOrigen = "SIN-CD";

            var estadoInicial = TipoEstadoGuiaEnum.AdmitidoCDOrigen;
            var tipoRetiroEnum = TipoRetiroEnum.SinRetiro;
            var tipoEnvioEnum = pedido.TipoEnvio switch
            {
                "Domicilio" => TipoEnvioEnum.VaADomicilio,
                "Centro de distribución" => TipoEnvioEnum.VaACD,
                "Agencia" => TipoEnvioEnum.VaAAgencia,
                _ => TipoEnvioEnum.VaADomicilio
            };
            var dniDestinatario = int.TryParse(pedido.DniDestinatario, out var dniParsed) ? dniParsed : 0;

            var guiasCreadas = (pedido.Encomiendas ?? Enumerable.Empty<EncomiendaItem>())
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
                        NumeroGuia = GenerarNumeroGuiaPorCD(cdOrigen),
                        FechaIngreso = now,
                        CuitCliente = pedido.Cliente?.Cuit,
                        TipoRetiro = tipoRetiroEnum,
                        TipoEnvio = tipoEnvioEnum,
                        AgenciaOrigen = 0,
                        AgenciaDestino = pedido.AgenciaEnvio?.Id ?? 0,
                        LocalidadDestino = pedido.LocalidadDestinatario,
                        DomicilioDestino = pedido.DomicilioDestinatario,
                        IdProvinciaDestino = provEntidad?.IdProvincia,
                        IdCDDestino = provEntidad?.IdCD,
                        IdCDOrigen = cdOrigen,
                        DniDestinatario = dniDestinatario,
                        Dimension = dimEnum,
                        Estado = estadoInicial,
                        Recorrido = new List<Recorrido>(),
                        EstadoActual = new EstadoGuia
                        {
                            Estado = estadoInicial,
                            Fecha = now,
                            IdCentroDistribucion = cdOrigen
                        },
                        Historial = new List<EstadoGuia>
                        {
                            new EstadoGuia
                            {
                                Estado = estadoInicial,
                                Fecha = now,
                                IdCentroDistribucion = cdOrigen
                            }
                        },
                        Precio = 0m
                    };
                }))
                .ToList();

            foreach (var g in guiasCreadas) GuiaAlmacen.Agregar(g);

            var gruposPorDestino = guiasCreadas
                .Where(g => !string.IsNullOrWhiteSpace(g.IdCDDestino))
                .GroupBy(g => new { g.IdCDOrigen, g.IdCDDestino });

            foreach (var grp in gruposPorDestino)
            {
                var idCDDestino = grp.Key.IdCDDestino;
                var idCDOrigenHoja = grp.Key.IdCDOrigen;
                var numeros = grp.Select(g => g.NumeroGuia).Distinct().ToList();

                var hojaId = CrearHojaMicroSiNoExiste(idCDOrigenHoja, idCDDestino, numeros);

                var nowUpdate = DateTime.Now;
                foreach (var n in numeros)
                {
                    var entidad = GuiaAlmacen.Guias.FirstOrDefault(x => x.NumeroGuia == n);
                    if (entidad == null) continue;
                    entidad.Estado = TipoEstadoGuiaEnum.AdmitidoCDOrigen;
                    entidad.EstadoActual ??= new EstadoGuia();
                    entidad.EstadoActual.Estado = TipoEstadoGuiaEnum.AdmitidoCDOrigen;
                    entidad.EstadoActual.Fecha = nowUpdate;
                    entidad.EstadoActual.IdCentroDistribucion = entidad.IdCDOrigen;
                    entidad.Historial ??= new List<EstadoGuia>();
                    entidad.Historial.Add(new EstadoGuia
                    {
                        Estado = TipoEstadoGuiaEnum.AdmitidoCDOrigen,
                        Fecha = nowUpdate,
                        IdCentroDistribucion = entidad.IdCDOrigen
                    });
                }
                GuiaAlmacen.Grabar();

                AsignarMicroParaRuta(idCDOrigenHoja, idCDDestino, hojaId);
            }
        }

        private void AsignarMicroParaRuta(string idCDOrigen, string idCDDestino, int idHDRMicro)
        {
            if (idHDRMicro <= 0 || string.IsNullOrWhiteSpace(idCDOrigen) || string.IsNullOrWhiteSpace(idCDDestino))
                return;

            var micros = MicroAlmacen.Micros;
            if (!micros.Any()) return;

            HojaDeRutaMicroEntidad HojaPorId(int id) =>
                HojaDeRutaMicroAlmacen.HojasDeRutaMicro.FirstOrDefault(h => h.IdHDRMicro == id);

            IEnumerable<HojaDeRutaMicroEntidad> HojasDe(MicroEntidad m) =>
                (m.HojasDeRuta ?? new List<int>()).Select(HojaPorId).Where(h => h != null);

            var candidatos = micros
                .Where(m => (m.Recorrido ?? new List<Parada>())
                    .Any(p => p.IdCDOrigen == idCDOrigen && p.IdCDDestino == idCDDestino))
                .ToList();

            if (!candidatos.Any()) return;

            var microConHojaPair = candidatos.FirstOrDefault(m =>
                HojasDe(m).Any(h => h.IdCDOrigen == idCDOrigen && h.IdCDDestino == idCDDestino));

            MicroEntidad elegido = microConHojaPair ?? candidatos
                .OrderBy(m => (m.HojasDeRuta ?? new List<int>()).Count)
                .First();

            elegido.HojasDeRuta ??= new List<int>();
            if (!elegido.HojasDeRuta.Contains(idHDRMicro))
            {
                elegido.HojasDeRuta.Add(idHDRMicro);
                MicroAlmacen.Grabar();
            }
        }

        private int CrearHojaMicroSiNoExiste(string idCDOrigen, string idCDDestino, List<string> numerosGuias)
        {
            if (string.IsNullOrWhiteSpace(idCDOrigen) || string.IsNullOrWhiteSpace(idCDDestino) ||
                numerosGuias == null || numerosGuias.Count == 0)
                return 0;

            var hoja = HojaDeRutaMicroAlmacen.HojasDeRutaMicro
                .FirstOrDefault(h => h.IdCDOrigen == idCDOrigen && h.IdCDDestino == idCDDestino);

            if (hoja == null)
            {
                hoja = new HojaDeRutaMicroEntidad
                {
                    IdCDOrigen = idCDOrigen,
                    IdCDDestino = idCDDestino,
                    Guias = new List<string>(numerosGuias)
                };
                HojaDeRutaMicroAlmacen.Agregar(hoja);
            }
            else
            {
                hoja.Guias ??= new List<string>();
                var nuevos = numerosGuias.Where(g => !hoja.Guias.Contains(g)).ToList();
                if (nuevos.Any())
                {
                    hoja.Guias.AddRange(nuevos);
                    HojaDeRutaMicroAlmacen.Grabar();
                }
            }

            return hoja.IdHDRMicro;
        }

        // Reemplazo del método anterior basado en timestamp
        private string GenerarNumeroGuiaPorCD(string idCDOrigen)
        {
            var prefix = string.IsNullOrWhiteSpace(idCDOrigen) ? "SIN-CD" : idCDOrigen.Trim();
            var prefixDash = prefix + "-";

            var max = GuiaAlmacen.Guias
                .Where(g => !string.IsNullOrWhiteSpace(g.IdCDOrigen) &&
                            string.Equals(g.IdCDOrigen, prefix, StringComparison.OrdinalIgnoreCase))
                .Select(g =>
                {
                    var nro = g.NumeroGuia;
                    if (string.IsNullOrWhiteSpace(nro) || !nro.StartsWith(prefixDash, StringComparison.OrdinalIgnoreCase))
                        return 0;
                    var tail = nro.Substring(prefixDash.Length);
                    return int.TryParse(tail, out var n) ? n : 0;
                })
                .DefaultIfEmpty(0)
                .Max();

            return $"{prefixDash}{(max + 1):000000}";
        }
    }
}
