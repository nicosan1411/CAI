using CAI_Proyecto.Almacenes.Almacen;
using CAI_Proyecto.Almacenes.ClaseAuxiliar;
using CAI_Proyecto.Almacenes.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CAI_Proyecto.Forms.Operacion.ImponerCallCenter.Model
{
    internal partial class ImponerCallCenterModel
    {
        public Cliente[] Clientes
        {
            get
            {
                return ClienteAlmacen.Clientes
                                     .Select(c => new Cliente
                                     {
                                         Cuit = c.Cuit,
                                         RazonSocial = c.RazonSocial
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
                                            Nombre = p.Nombre
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
                                     ProvinciaCodigo = provinciaCodigo
                                 });
        }

        public List<AgenciaRetiro> AgenciasRetiroCliente(Cliente cliente)
        {
            if (cliente == null)
            {
                return new List<AgenciaRetiro>();
            }

            var clienteEntidad = ClienteAlmacen.Clientes.Single(c => c.Cuit == cliente.Cuit);

            return clienteEntidad.AgenciasAsociadas
                                 .Select(idAgencia => AgenciaAlmacen.Agencias.Single(agenciaEntidad => idAgencia == agenciaEntidad.IdAgencia))
                                 .Select(agenciaEntidad => new AgenciaRetiro
                                 {
                                     Id = agenciaEntidad.IdAgencia,
                                     Nombre = agenciaEntidad.Nombre
                                 })
                                 .ToList();
        }

        public List<EncomiendaItem> Encomiendas { get; } = new List<EncomiendaItem>();

        public void AgregarEncomienda(EncomiendaItem encomienda) => Encomiendas.Add(encomienda);

        public void QuitarEncomienda(EncomiendaItem encomienda) => Encomiendas.Remove(encomienda);

        public List<string> ValidarPedido(Pedido p)
        {
            var errores = new List<string>();

            if (p.Cliente == null)
                errores.Add("Debe seleccionar un cliente.");

            if (string.IsNullOrWhiteSpace(p.TipoRetiro))
                errores.Add("Debe seleccionar el tipo de retiro.");
            else if (p.TipoRetiro == "Agencia" && p.AgenciaRetiro == null)
                errores.Add("Debe seleccionar la agencia de retiro.");

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

            var provEntidad = ProvinciaAlmacen.Provincias
                .FirstOrDefault(p => pedido.ProvinciaEnvio != null &&
                                     p.IdProvincia == pedido.ProvinciaEnvio.Codigo);

            var tipoRetiroEnum = pedido.TipoRetiro switch
            {
                "Agencia" => TipoRetiroEnum.DesdeAgencia,
                "Domicilio" => TipoRetiroEnum.DesdeDomicilio,
                _ => TipoRetiroEnum.SinRetiro
            };

            var estadoInicial = pedido.TipoRetiro switch
            {
                "Agencia" => TipoEstadoGuiaEnum.EnBusquedaRetiroAgencia,
                "Domicilio" => TipoEstadoGuiaEnum.EnBusquedaRetiroDomicilio,
                _ => TipoEstadoGuiaEnum.EnBusquedaRetiroDomicilio
            };

            var tipoEnvioEnum = pedido.TipoEnvio switch
            {
                "Domicilio" => TipoEnvioEnum.VaADomicilio,
                "Centro de distribución" => TipoEnvioEnum.VaACD,
                "Agencia" => TipoEnvioEnum.VaAAgencia,
                _ => TipoEnvioEnum.VaADomicilio
            };

            var dniDestinatario = int.TryParse(pedido.DniDestinatario, out var dniParsed) ? dniParsed : 0;

            // Determinar CD de origen según agencias asociadas del cliente.
            string cdOrigen = null;
            var clienteEntidad = ClienteAlmacen.Clientes.FirstOrDefault(c => c.Cuit == pedido.Cliente?.Cuit);
            if (clienteEntidad?.AgenciasAsociadas != null && clienteEntidad.AgenciasAsociadas.Count > 0)
            {
                var cdsCliente = clienteEntidad.AgenciasAsociadas
                    .Select(id => AgenciaAlmacen.Agencias.FirstOrDefault(a => a.IdAgencia == id))
                    .Where(a => a != null && !string.IsNullOrWhiteSpace(a.CDAsignado))
                    .Select(a => a.CDAsignado)
                    .Distinct()
                    .ToList();

                if (cdsCliente.Count == 1)
                    cdOrigen = cdsCliente[0];
                else if (cdsCliente.Count > 1)
                    cdOrigen = cdsCliente[0]; // Si hubiera más de uno, se toma el primero (ajustar si se requiere otra regla).
            }

            // Fallbacks si no se obtuvo por agencias del cliente.
            if (string.IsNullOrWhiteSpace(cdOrigen) && pedido.AgenciaRetiro != null)
            {
                var agRetiro = AgenciaAlmacen.Agencias.FirstOrDefault(a => a.IdAgencia == pedido.AgenciaRetiro.Id);
                cdOrigen = agRetiro?.CDAsignado;
            }
            if (string.IsNullOrWhiteSpace(cdOrigen))
            {
                cdOrigen = CentroDeDistribucionAlmacen.CentroDeDistribucionActual?.IdCD;
            }
            if (string.IsNullOrWhiteSpace(cdOrigen) && pedido.AgenciaEnvio != null)
            {
                var agEnvio = AgenciaAlmacen.Agencias.FirstOrDefault(a => a.IdAgencia == pedido.AgenciaEnvio.Id);
                cdOrigen = agEnvio?.CDAsignado;
            }
            if (string.IsNullOrWhiteSpace(cdOrigen))
            {
                cdOrigen = provEntidad?.IdCD;
            }
            if (string.IsNullOrWhiteSpace(cdOrigen))
            {
                cdOrigen = "SIN-CD";
            }

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
                        NumeroGuia = GenerarNumeroGuiaPorCD(cdOrigen),
                        FechaIngreso = now,
                        CuitCliente = pedido.Cliente?.Cuit,
                        TipoRetiro = tipoRetiroEnum,
                        TipoEnvio = tipoEnvioEnum,
                        AgenciaOrigen = pedido.AgenciaRetiro?.Id ?? 0,
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
                // Reset comisiones
                guia.ComisionesAgenciaOrigen = 0m;
                guia.ComisionesAgenciaDestino = 0m;
                guia.ComisionesFleteroOrigen = 0m;
                guia.ComisionesFleteroDestino = 0m;

                // Origen
                var origen =
                    guia.TipoRetiro == TipoRetiroEnum.DesdeDomicilio ? "Domicilio" :
                    (guia.TipoRetiro == TipoRetiroEnum.DesdeAgencia && guia.AgenciaOrigen != 0) ? "Agencia" :
                    "CD";

                // Destino
                var destino = guia.TipoEnvio switch
                {
                    TipoEnvioEnum.VaACD => "CD",
                    TipoEnvioEnum.VaAAgencia => "Agencia",
                    _ => "Domicilio"
                };

                // Base
                var basePrice = TarifarioAlmacen.Tarifarios
                    .FirstOrDefault(t =>
                        t.Tamaño == guia.Dimension &&
                        string.Equals(t.IdCDOrigen ?? string.Empty, guia.IdCDOrigen ?? string.Empty, StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(t.IdCDDestino ?? string.Empty, guia.IdCDDestino ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                    ?.Precio ?? 0m;

                decimal Extra(TipoExtraEnum tipo) =>
                    ExtrasAlmacen.Extras.FirstOrDefault(x => x.TipoExtra == tipo)?.Precio ?? 0m;

                var extraRetiroDomicilio = Extra(TipoExtraEnum.RetiroDomicilio);
                var extraEntregaDomicilio = Extra(TipoExtraEnum.EntregaDomicilio);
                var extraEntregaAgencia = Extra(TipoExtraEnum.EntregaAgencia);

                var comisionAgencia = AgenciaComisionAlmacen.AgenciaComisiones
                    .FirstOrDefault(c => c.TipoBulto == guia.Dimension)?.Comision ?? 0m;

                var comisionFletero = FleteroComisionAlmacen.FleteroComisiones
                    .FirstOrDefault(c => c.TipoBulto == guia.Dimension)?.Comision ?? 0m;

                var precio = basePrice;

                // Casuísticas
                if (origen == "CD" && destino == "CD")
                {
                    // 1. CD → CD
                }
                else if (origen == "CD" && destino == "Agencia")
                {
                    // 2. CD → Agencia
                    precio += extraEntregaAgencia;
                    guia.ComisionesAgenciaDestino = comisionAgencia;
                }
                else if (origen == "CD" && destino == "Domicilio")
                {
                    // 3. CD → Domicilio
                    precio += extraEntregaDomicilio;
                    guia.ComisionesFleteroDestino = comisionFletero;
                }
                else if (origen == "Agencia" && destino == "CD")
                {
                    // 4. Agencia → CD
                    guia.ComisionesAgenciaOrigen = comisionAgencia;
                }
                else if (origen == "Agencia" && destino == "Agencia")
                {
                    // 5. Agencia → Agencia
                    precio += extraEntregaAgencia;
                    guia.ComisionesAgenciaOrigen = comisionAgencia;
                    guia.ComisionesAgenciaDestino = comisionAgencia;
                }
                else if (origen == "Agencia" && destino == "Domicilio")
                {
                    // 6. Agencia → Domicilio
                    precio += extraEntregaDomicilio;
                    guia.ComisionesAgenciaOrigen = comisionAgencia;
                    guia.ComisionesFleteroDestino = comisionFletero;
                }
                else if (origen == "Domicilio" && destino == "CD")
                {
                    // 7. Domicilio → CD
                    precio += extraRetiroDomicilio;
                    guia.ComisionesFleteroOrigen = comisionFletero;
                }
                else if (origen == "Domicilio" && destino == "Agencia")
                {
                    // 8. Domicilio → Agencia
                    precio += (extraRetiroDomicilio + extraEntregaAgencia);
                    guia.ComisionesFleteroOrigen = comisionFletero;
                    guia.ComisionesAgenciaDestino = comisionAgencia;
                }
                else if (origen == "Domicilio" && destino == "Domicilio")
                {
                    // 9. Domicilio → Domicilio
                    precio += (extraRetiroDomicilio + extraEntregaDomicilio);
                    guia.ComisionesFleteroOrigen = comisionFletero;
                    guia.ComisionesFleteroDestino = comisionFletero;
                }

                guia.Precio = precio;

                // Persistir
                GuiaAlmacen.Agregar(guia);
            }
            // Luego de agregar todas las guías
            var hojasGeneradas = GenerarHojasDeRutaFleteRetiro();
            var fleterosAsignados = AsignarFleterosAHojasDeRutaRetiro();
        }

        /// <summary>
        /// Genera (o agrega a) hojas de ruta de retiro (Tipo = Retiro) a partir de las guías en estado de búsqueda de retiro.
        /// Reglas:
        ///  - Agrupa por domicilio (solo el string domicilio, sin distinguir cliente) para TipoRetiro = DesdeDomicilio.
        ///  - Agrupa por IdAgencia para TipoRetiro = DesdeAgencia.
        ///  - Si ya existe una hoja activa (Cumplida = false) para ese destino (domicilio o agencia), NO crea una nueva:
        ///       agrega las nuevas guías a la hoja existente (solo las que no estaban).
        ///  - Si no existe, crea la hoja sin fletero (IdFletero = null) para que luego se asigne.
        ///  - Devuelve la lista de hojas nuevas y/o modificadas.
        /// </summary>
        public List<HojaDeRutaFleteEntidad> GenerarHojasDeRutaFleteRetiro()
        {
            var estadoRetiroDomicilio = TipoEstadoGuiaEnum.EnBusquedaRetiroDomicilio;
            var estadoRetiroAgencia = TipoEstadoGuiaEnum.EnBusquedaRetiroAgencia;

            // Hojas activas (no cumplidas) de retiro
            var hojasActivas = HojaDeRutaFleteAlmacen.HojasDeRutaFlete
                .Where(h => h.Tipo == TipoHojaDeRutaEnum.Retiro && !h.Cumplida)
                .ToList();

            // Guías candidatas (en búsqueda de retiro) que aún no están en ninguna hoja activa
            var guiasCandidatas = GuiaAlmacen.Guias
                .Where(g =>
                       (g.TipoRetiro == TipoRetiroEnum.DesdeDomicilio &&
                        ((g.EstadoActual?.Estado) ?? g.Estado) == estadoRetiroDomicilio)
                    || (g.TipoRetiro == TipoRetiroEnum.DesdeAgencia &&
                        ((g.EstadoActual?.Estado) ?? g.Estado) == estadoRetiroAgencia))
                .Where(g => !hojasActivas.Any(h => h.Guias.Contains(g.NumeroGuia)))
                .ToList();

            if (!guiasCandidatas.Any())
                return new List<HojaDeRutaFleteEntidad>();

            // Agrupar por domicilio (solo string) para retiros a domicilio
            var gruposDomicilio = guiasCandidatas
                .Where(g => g.TipoRetiro == TipoRetiroEnum.DesdeDomicilio)
                .GroupBy(g => (g.DomicilioDestino ?? string.Empty).Trim(), StringComparer.OrdinalIgnoreCase);

            // Agrupar por agencia para retiros en agencia
            var gruposAgencia = guiasCandidatas
                .Where(g => g.TipoRetiro == TipoRetiroEnum.DesdeAgencia && g.AgenciaOrigen != 0)
                .GroupBy(g => g.AgenciaOrigen);

            var resultado = new List<HojaDeRutaFleteEntidad>();
            var almacenInterno = HojaDeRutaFleteAlmacen.HojasDeRutaFlete.ToList(); // referencias a objetos

            // Procesar domicilios
            foreach (var grp in gruposDomicilio)
            {
                var domicilio = grp.Key;
                var guiasGrupo = grp.Select(g => g.NumeroGuia).Distinct().ToList();

                // Buscar hoja activa existente para ese domicilio (IdAgencia == 0)
                var hojaExistente = hojasActivas
                    .FirstOrDefault(h => h.IdAgencia == 0 &&
                        string.Equals(h.DomicilioDestino ?? string.Empty, domicilio, StringComparison.OrdinalIgnoreCase));

                if (hojaExistente != null)
                {
                    // Agregar solo guías nuevas
                    var nuevas = guiasGrupo.Where(n => !hojaExistente.Guias.Contains(n)).ToList();
                    if (nuevas.Any())
                    {
                        hojaExistente.Guias.AddRange(nuevas);
                        resultado.Add(hojaExistente);
                    }
                }
                else
                {
                    var hojaNueva = new HojaDeRutaFleteEntidad
                    {
                        Tipo = TipoHojaDeRutaEnum.Retiro,
                        IdFletero = null,
                        CuitCliente = null, // Puede haber múltiples clientes para un mismo domicilio
                        IdAgencia = 0,
                        DomicilioDestino = domicilio,
                        Cumplida = false,
                        Guias = guiasGrupo
                    };
                    HojaDeRutaFleteAlmacen.Agregar(hojaNueva);
                    resultado.Add(hojaNueva);
                }
            }

            // Procesar agencias
            foreach (var grp in gruposAgencia)
            {
                var idAgencia = grp.Key;
                var guiasGrupo = grp.Select(g => g.NumeroGuia).Distinct().ToList();

                var hojaExistente = hojasActivas
                    .FirstOrDefault(h => h.IdAgencia == idAgencia);

                if (hojaExistente != null)
                {
                    var nuevas = guiasGrupo.Where(n => !hojaExistente.Guias.Contains(n)).ToList();
                    if (nuevas.Any())
                    {
                        hojaExistente.Guias.AddRange(nuevas);
                        resultado.Add(hojaExistente);
                    }
                }
                else
                {
                    var hojaNueva = new HojaDeRutaFleteEntidad
                    {
                        Tipo = TipoHojaDeRutaEnum.Retiro,
                        IdFletero = null,
                        CuitCliente = null,
                        IdAgencia = idAgencia,
                        DomicilioDestino = null,
                        Cumplida = false,
                        Guias = guiasGrupo
                    };
                    HojaDeRutaFleteAlmacen.Agregar(hojaNueva);
                    resultado.Add(hojaNueva);
                }
            }

            // Si solo se modificaron hojas existentes (no se agregaron nuevas), hay que grabar igualmente
            // porque se agregaron guías.
            HojaDeRutaFleteAlmacen.Grabar();

            return resultado;
        }

        /// <summary>
        /// Asigna fleteros a hojas de ruta de retiro no cumplidas y sin fletero,
        /// respetando la restricción de centro de distribución / agencia, eligiendo
        /// el fletero con menor cantidad de guías en hojas activas.
        /// Si el fletero ya posee una hoja activa con el mismo destino (domicilio o agencia),
        /// se agregan las guías a esa hoja en lugar de crear otra.
        /// </summary>
        /// <returns>Hojas modificadas (asignadas/actualizadas).</returns>
        public List<HojaDeRutaFleteEntidad> AsignarFleterosAHojasDeRutaRetiro()
        {
            var hojas = HojaDeRutaFleteAlmacen.HojasDeRutaFlete.ToList();

            // Solo hojas de Retiro, activas (no cumplidas), sin fletero asignado todavía
            var pendientes = hojas
                .Where(h => h.Tipo == TipoHojaDeRutaEnum.Retiro
                            && !h.Cumplida
                            && string.IsNullOrWhiteSpace(h.IdFletero))
                .ToList();

            if (!pendientes.Any()) return new List<HojaDeRutaFleteEntidad>();

            // Índices rápidos
            var agenciasPorId = AgenciaAlmacen.Agencias.ToDictionary(a => a.IdAgencia, a => a);
            var guiasPorNumero = GuiaAlmacen.Guias.ToDictionary(g => g.NumeroGuia, g => g);
            var fleteros = FleteroAlmacen.Fleteros.ToList();

            var modificadas = new List<HojaDeRutaFleteEntidad>();

            foreach (var hoja in pendientes)
            {
                // Determinar ámbito (agencia/CD) para filtrar fleteros elegibles
                string cdObjetivo = null;
                int? agenciaObjetivo = null;

                if (hoja.IdAgencia != 0)
                {
                    agenciaObjetivo = hoja.IdAgencia;
                    if (!agenciasPorId.TryGetValue(hoja.IdAgencia, out var agenciaEnt)) continue;
                    cdObjetivo = agenciaEnt.CDAsignado;
                }
                else
                {
                    // Tomar el CD de origen desde alguna guía de la hoja
                    var guiaRef = hoja.Guias?.Select(n => guiasPorNumero.TryGetValue(n, out var g) ? g : null)
                                         ?.FirstOrDefault(g => g != null);
                    cdObjetivo = guiaRef?.IdCDOrigen;
                    if (string.IsNullOrWhiteSpace(cdObjetivo)) continue;
                }

                // Fleteros elegibles:
                // - Si hay agenciaObjetivo: deben pertenecer a esa agencia.
                // - Si no hay agenciaObjetivo (retiro a domicilio): deben pertenecer al CD objetivo.
                var fleterosElegibles = fleteros
                    .Where(f => agenciaObjetivo.HasValue
                        ? f.IdAgenciaAsignada == agenciaObjetivo.Value
                        : string.Equals(f.IdCDAsignado, cdObjetivo, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (!fleterosElegibles.Any())
                    continue;

                // Carga actual de cada fletero (cantidad de guías en hojas activas)
                int CargaFletero(string idFletero) =>
                    hojas.Where(h => !h.Cumplida && string.Equals(h.IdFletero, idFletero, StringComparison.OrdinalIgnoreCase))
                         .Sum(h => h.Guias?.Count ?? 0);

                // Preferir fleteros que ya tengan hoja activa para el mismo destino (domicilio o agencia)
                bool CoincideDestino(HojaDeRutaFleteEntidad h1, HojaDeRutaFleteEntidad h2) =>
                    h1.IdAgencia != 0
                        ? h1.IdAgencia == h2.IdAgencia
                        : string.Equals(h1.DomicilioDestino ?? string.Empty, h2.DomicilioDestino ?? string.Empty, StringComparison.OrdinalIgnoreCase);

                var candidatosConMismaHoja = fleterosElegibles
                    .Select(f => new
                    {
                        Fletero = f,
                        HojaExistente = hojas.FirstOrDefault(h =>
                            !h.Cumplida
                            && h.Tipo == TipoHojaDeRutaEnum.Retiro
                            && string.Equals(h.IdFletero, f.IdFletero, StringComparison.OrdinalIgnoreCase)
                            && CoincideDestino(h, hoja))
                    })
                    .Where(x => x.HojaExistente != null)
                    .OrderBy(x => CargaFletero(x.Fletero.IdFletero))
                    .ToList();

                var asignacion = candidatosConMismaHoja.FirstOrDefault();

                // Si ningún fletero tiene hoja activa para ese destino, tomar el de menor carga general
                if (asignacion == null)
                {
                    var fleteroElegido = fleterosElegibles
                        .Select(f => new { Fletero = f, Carga = CargaFletero(f.IdFletero) })
                        .OrderBy(x => x.Carga)
                        .First().Fletero;

                    // Asignar el fletero a la hoja pendiente en lugar de crear una nueva
                    hoja.IdFletero = fleteroElegido.IdFletero;
                    modificadas.Add(hoja);
                    continue;
                }

                // Agregar guías de la hoja pendiente a la hoja existente del fletero para ese destino
                var hojaDestino = asignacion.HojaExistente;
                var guiasNuevas = (hoja.Guias ?? new List<string>()).Where(n => !(hojaDestino.Guias ?? new List<string>()).Contains(n)).ToList();

                if (guiasNuevas.Any())
                {
                    if (hojaDestino.Guias == null) hojaDestino.Guias = new List<string>();
                    hojaDestino.Guias.AddRange(guiasNuevas);
                    modificadas.Add(hojaDestino);
                }

                // Marcar la hoja pendiente como cumplida para evitar duplicidad
                hoja.Cumplida = true;
                modificadas.Add(hoja);
            }

            if (modificadas.Any())
                HojaDeRutaFleteAlmacen.Grabar();

            return modificadas.Distinct().ToList();
        }

        // Reemplazo del método anterior GenerarNumeroGuia()
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

