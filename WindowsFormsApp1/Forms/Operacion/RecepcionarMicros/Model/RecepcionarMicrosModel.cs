using CAI_Proyecto.Almacenes.Almacen;
using CAI_Proyecto.Almacenes.ClaseAuxiliar;
using CAI_Proyecto.Almacenes.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CAI_Proyecto.Forms.Operacion.RecepcionarMicros.Model
{
    public class RecepcionarMicrosModel
    {
        public IReadOnlyList<GuiasMicros> Guias { get; private set; } = new List<GuiasMicros>();
        public List<string> GuiasDisponiblesParaCarga { get; private set; } = new List<string>();
        public bool HayGuiasParaCarga { get; private set; }

        public List<Micro> Micros =>
            MicroAlmacen.Micros
                .Select(m => new Micro(m.Patente, m.CuitEmpresaMicro))
                .ToList();

        private string CdActual => CentroDeDistribucionAlmacen.CentroDeDistribucionActual?.IdCD;

        public bool PrepararMicro(Micro micro)
        {
            Guias = new List<GuiasMicros>();
            GuiasDisponiblesParaCarga = new List<string>();
            HayGuiasParaCarga = false;

            if (!ValidarCdActual()) return false;
            if (micro == null)
            {
                MessageBox.Show("Debe seleccionar un micro.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var microEntidad = MicroAlmacen.Micros.FirstOrDefault(m => m.Patente == micro.Patente);
            if (microEntidad == null)
            {
                MessageBox.Show("El micro seleccionado no existe.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var hojasAsignadas = (microEntidad.HojasDeRuta ?? new List<int>())
                .Select(id => HojaDeRutaMicroAlmacen.HojasDeRutaMicro.FirstOrDefault(h => h.IdHDRMicro == id))
                .Where(h => h != null && h.IdCDDestino == CdActual)
                .ToList();

            var guiasEnHojas = new HashSet<string>(hojasAsignadas.SelectMany(h => h.Guias ?? new List<string>()));

            var recepcion = GuiaAlmacen.Guias
                .Where(g =>
                    g.Estado == TipoEstadoGuiaEnum.EnTransitoPorMicro &&
                    g.IdCDDestino == CdActual &&
                    guiasEnHojas.Contains(g.NumeroGuia))
                .Select(g => new GuiasMicros(g.NumeroGuia, g.Estado.ToString(), micro.Patente))
                .ToList();

            Guias = recepcion;

            if (Guias.Count > 0)
                return true;

            var paradas = microEntidad.Recorrido ?? new List<Parada>();
            var destinosMicro = new HashSet<string>(paradas.Select(p => p.IdCDDestino));

            var paraCargaDirecto = GuiaAlmacen.Guias
                .Where(g =>
                    g.Estado == TipoEstadoGuiaEnum.AdmitidoCDOrigen &&
                    g.IdCDOrigen == CdActual &&
                    g.IdCDDestino != CdActual &&
                    destinosMicro.Contains(g.IdCDDestino))
                .Select(g => g.NumeroGuia);

            var hojasPosibles = HojaDeRutaMicroAlmacen.HojasDeRutaMicro
                .Where(h => h.IdCDOrigen == CdActual && destinosMicro.Contains(h.IdCDDestino))
                .ToList();

            bool HojaAsignadaAAlguno(int idHoja) =>
                MicroAlmacen.Micros.Any(mi => (mi.HojasDeRuta ?? new List<int>()).Contains(idHoja));

            var hojasSinMicro = hojasPosibles.Where(h => !HojaAsignadaAAlguno(h.IdHDRMicro)).ToList();

            var guiasDesdeHojasSinMicro = hojasSinMicro
                .SelectMany(h => h.Guias ?? new List<string>())
                .Select(nro => GuiaAlmacen.Guias.FirstOrDefault(g => g.NumeroGuia == nro))
                .Where(g => g != null
                            && g.Estado == TipoEstadoGuiaEnum.AdmitidoCDOrigen
                            && g.IdCDOrigen == CdActual
                            && g.IdCDDestino != CdActual
                            && destinosMicro.Contains(g.IdCDDestino))
                .Select(g => g.NumeroGuia);

            GuiasDisponiblesParaCarga = paraCargaDirecto
                .Concat(guiasDesdeHojasSinMicro)
                .Distinct()
                .ToList();

            HayGuiasParaCarga = GuiasDisponiblesParaCarga.Count > 0;
            return false;
        }

        public bool CargarGuiasPendientes(Micro micro)
        {
            if (!HayGuiasParaCarga || GuiasDisponiblesParaCarga.Count == 0)
            {
                MessageBox.Show("No hay guías disponibles para cargar en este micro.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (!ValidarCdActual()) return false;
            if (micro == null)
            {
                MessageBox.Show("Debe seleccionar un micro.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var confirm = MessageBox.Show(
                $"Se van a cargar {GuiasDisponiblesParaCarga.Count} guía(s) al micro.\n¿Desea continuar?",
                "Confirmar carga", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return false;

            var microEntidad = MicroAlmacen.Micros.FirstOrDefault(m => m.Patente == micro.Patente);
            if (microEntidad == null)
            {
                MessageBox.Show("El micro seleccionado no existe.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            microEntidad.HojasDeRuta ??= new List<int>();
            var ahora = DateTime.Now;

            foreach (var numero in GuiasDisponiblesParaCarga)
            {
                var entidad = GuiaAlmacen.Guias.FirstOrDefault(g => g.NumeroGuia == numero);
                if (entidad == null) continue;
                if (entidad.Estado != TipoEstadoGuiaEnum.AdmitidoCDOrigen ||
                    entidad.IdCDOrigen != CdActual ||
                    entidad.IdCDDestino == CdActual) continue;

                var hoja = HojaDeRutaMicroAlmacen.HojasDeRutaMicro
                    .FirstOrDefault(h => h.IdCDOrigen == CdActual && h.IdCDDestino == entidad.IdCDDestino);

                if (hoja == null)
                {
                    hoja = new HojaDeRutaMicroEntidad
                    {
                        IdCDOrigen = CdActual,
                        IdCDDestino = entidad.IdCDDestino,
                        Guias = new List<string> { entidad.NumeroGuia }
                    };
                    HojaDeRutaMicroAlmacen.Agregar(hoja);
                }
                else
                {
                    hoja.Guias ??= new List<string>();
                    if (!hoja.Guias.Contains(entidad.NumeroGuia))
                    {
                        hoja.Guias.Add(entidad.NumeroGuia);
                        HojaDeRutaMicroAlmacen.Grabar();
                    }
                }

                if (!microEntidad.HojasDeRuta.Contains(hoja.IdHDRMicro))
                    microEntidad.HojasDeRuta.Add(hoja.IdHDRMicro);

                entidad.Estado = TipoEstadoGuiaEnum.EnTransitoPorMicro;
                entidad.EstadoActual = new EstadoGuia
                {
                    Estado = TipoEstadoGuiaEnum.EnTransitoPorMicro,
                    Fecha = ahora,
                    IdCentroDistribucion = CdActual
                };
                entidad.Historial ??= new List<EstadoGuia>();
                entidad.Historial.Add(entidad.EstadoActual);
            }

            MicroAlmacen.Grabar();
            GuiaAlmacen.Grabar();

            MessageBox.Show("Guías cargadas correctamente al micro.", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            GuiasDisponiblesParaCarga = new List<string>();
            HayGuiasParaCarga = false;
            return true;
        }

        public bool GuardarRecepcion()
        {
            if (!ValidarCdActual()) return false;

            var seleccionadas = Guias.Where(g => g.Seleccionada).ToList();
            if (seleccionadas.Count == 0)
            {
                MessageBox.Show("No seleccionó ninguna guía para recepcionar.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            var confirm = MessageBox.Show(
                $"Se van a recepcionar {seleccionadas.Count} guía(s).\n¿Desea continuar?",
                "Confirmar recepción", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return false;

            var ahora = DateTime.Now;
            var numerosRecepcionadas = new List<string>();

            foreach (var guiaUI in seleccionadas)
            {
                var entidad = GuiaAlmacen.Guias.FirstOrDefault(g => g.NumeroGuia == guiaUI.NroGuia);
                if (entidad == null) continue;
                if (entidad.Estado != TipoEstadoGuiaEnum.EnTransitoPorMicro ||
                    entidad.IdCDDestino != CdActual) continue;

                entidad.Estado = TipoEstadoGuiaEnum.AdmitidoCDDestino;
                entidad.EstadoActual = new EstadoGuia
                {
                    Estado = TipoEstadoGuiaEnum.AdmitidoCDDestino,
                    Fecha = ahora,
                    IdCentroDistribucion = CdActual
                };
                entidad.Historial ??= new List<EstadoGuia>();
                entidad.Historial.Add(entidad.EstadoActual);

                guiaUI.Estado = TipoEstadoGuiaEnum.AdmitidoCDDestino.ToString();
                numerosRecepcionadas.Add(entidad.NumeroGuia);
            }

            // Generar hojas de ruta de flete (Entrega) para guías que van a domicilio (0) o agencia (2)
            GenerarHojasDeRutaFleteEntrega(numerosRecepcionadas);

            GuiaAlmacen.Grabar();

            MessageBox.Show("Guías recepcionadas correctamente.", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            Guias = new List<GuiasMicros>();
            return true;
        }

        // Genera/actualiza hojas de entrega (Tipo = Entrega) agrupando por destino.
        // Asigna fletero si hay alguno disponible según AgenciaDestino o CD destino (domicilio).
        private void GenerarHojasDeRutaFleteEntrega(IEnumerable<string> numerosGuias)
        {
            var numeros = (numerosGuias ?? Enumerable.Empty<string>())
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .Distinct()
                .ToList();

            if (!numeros.Any()) return;

            var candidatas = GuiaAlmacen.Guias
                .Where(g => numeros.Contains(g.NumeroGuia))
                .Where(g => g.Estado == TipoEstadoGuiaEnum.AdmitidoCDDestino)
                .Where(g => g.TipoEnvio == TipoEnvioEnum.VaADomicilio || g.TipoEnvio == TipoEnvioEnum.VaAAgencia)
                .ToList();

            if (!candidatas.Any()) return;

            var hojasActivasEntrega = HojaDeRutaFleteAlmacen.HojasDeRutaFlete
                .Where(h => h.Tipo == TipoHojaDeRutaEnum.Entrega && !h.Cumplida)
                .ToList();

            var agenciasPorId = AgenciaAlmacen.Agencias.ToDictionary(a => a.IdAgencia, a => a);
            var fleterosTodos = FleteroAlmacen.Fleteros.ToList();

            int CargaFletero(string idFletero) =>
                HojaDeRutaFleteAlmacen.HojasDeRutaFlete
                    .Where(h => !h.Cumplida && h.Tipo == TipoHojaDeRutaEnum.Entrega && string.Equals(h.IdFletero, idFletero, StringComparison.OrdinalIgnoreCase))
                    .Sum(h => h.Guias?.Count ?? 0);

            var grupos = candidatas.GroupBy(g =>
            {
                if (g.TipoEnvio == TipoEnvioEnum.VaAAgencia)
                    return $"AGENCIA:{g.AgenciaDestino}";
                var dom = (g.DomicilioDestino ?? string.Empty).Trim();
                return $"DOMIC:{dom}";
            });

            foreach (var grp in grupos)
            {
                var guiasGrupo = grp.ToList();
                HojaDeRutaFleteEntidad hojaDestinoExistente = null;
                string cdAmbito = null;
                int idAgencia = 0;
                string domicilio = null;

                if (grp.Key.StartsWith("AGENCIA:"))
                {
                    idAgencia = int.TryParse(grp.Key.Substring("AGENCIA:".Length), out var parsed) ? parsed : 0;
                    hojaDestinoExistente = hojasActivasEntrega.FirstOrDefault(h => h.IdAgencia == idAgencia);
                    cdAmbito = agenciasPorId.TryGetValue(idAgencia, out var ag) ? ag.CDAsignado : null;
                }
                else
                {
                    domicilio = grp.Key.Substring("DOMIC:".Length);
                    hojaDestinoExistente = hojasActivasEntrega.FirstOrDefault(h =>
                        h.IdAgencia == 0 &&
                        string.Equals(h.DomicilioDestino ?? string.Empty, domicilio, StringComparison.OrdinalIgnoreCase));
                    cdAmbito = guiasGrupo.FirstOrDefault()?.IdCDDestino;
                }

                var fleterosElegibles = fleterosTodos
                    .Where(f => idAgencia != 0
                        ? f.IdAgenciaAsignada == idAgencia
                        : string.Equals(f.IdCDAsignado, cdAmbito, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                // Si ya existe hoja con fletero: agregar guías
                if (hojaDestinoExistente != null && !string.IsNullOrWhiteSpace(hojaDestinoExistente.IdFletero))
                {
                    var nuevos = guiasGrupo.Select(g => g.NumeroGuia)
                        .Where(n => !(hojaDestinoExistente.Guias ?? new List<string>()).Contains(n))
                        .ToList();
                    if (nuevos.Any())
                    {
                        hojaDestinoExistente.Guias ??= new List<string>();
                        hojaDestinoExistente.Guias.AddRange(nuevos);
                        HojaDeRutaFleteAlmacen.Grabar();
                    }
                    continue;
                }

                // Elegir fletero (menor carga) si hay elegibles
                var fleteroElegido = fleterosElegibles
                    .Select(f => new { Fletero = f, Carga = CargaFletero(f.IdFletero) })
                    .OrderBy(x => x.Carga)
                    .FirstOrDefault()?.Fletero;

                if (hojaDestinoExistente == null)
                {
                    var hojaNueva = new HojaDeRutaFleteEntidad
                    {
                        Tipo = TipoHojaDeRutaEnum.Entrega,
                        IdFletero = fleteroElegido?.IdFletero, // null si no hay
                        CuitCliente = null,
                        IdAgencia = idAgencia,
                        DomicilioDestino = idAgencia == 0 ? domicilio : null,
                        Cumplida = false,
                        Guias = guiasGrupo.Select(g => g.NumeroGuia).Distinct().ToList()
                    };
                    HojaDeRutaFleteAlmacen.Agregar(hojaNueva);
                }
                else
                {
                    // Hoja existente sin fletero
                    if (fleteroElegido != null)
                        hojaDestinoExistente.IdFletero = fleteroElegido.IdFletero;

                    var nuevos = guiasGrupo.Select(g => g.NumeroGuia)
                        .Where(n => !(hojaDestinoExistente.Guias ?? new List<string>()).Contains(n))
                        .ToList();
                    if (nuevos.Any())
                    {
                        hojaDestinoExistente.Guias ??= new List<string>();
                        hojaDestinoExistente.Guias.AddRange(nuevos);
                    }
                    HojaDeRutaFleteAlmacen.Grabar();
                }
            }
        }

        private bool ValidarCdActual()
        {
            if (string.IsNullOrWhiteSpace(CdActual))
            {
                MessageBox.Show("No hay un Centro de Distribución actual seleccionado.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}
