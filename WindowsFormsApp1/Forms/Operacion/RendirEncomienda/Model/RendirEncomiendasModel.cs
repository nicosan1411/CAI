using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CAI_Proyecto.Almacenes.Almacen;
using CAI_Proyecto.Almacenes.ClaseAuxiliar;
using CAI_Proyecto.Almacenes.Entidad;

namespace CAI_Proyecto.Forms.Operacion.RendirEncomienda.Model
{
    public class RendirEncomiendasModel
    {
        public IReadOnlyList<GuiaRendir> Retiros { get; private set; } = new List<GuiaRendir>();
        public IReadOnlyList<GuiaRendir> Entregas { get; private set; } = new List<GuiaRendir>();

        public List<Fletero> Fleteros = new List<Fletero>();
        public List<GuiaRendir> Guias = new List<GuiaRendir>();

        private string _fleteroActualId; // para usar en GuardarCambios

        public RendirEncomiendasModel()
        {
            CargarFleterosSegunContexto();
        }

        public List<Fletero> ObtenerFleteros()
        {
            CargarFleterosSegunContexto();
            return Fleteros;
        }

        private void CargarFleterosSegunContexto()
        {
            var agencia = AgenciaAlmacen.AgenciaActual;

            if (agencia == null)
            {
                Fleteros = new List<Fletero>();
                return;
            }

            Fleteros = FleteroAlmacen.Fleteros
                .Where(f => f.IdAgenciaAsignada == agencia.IdAgencia)
                .OrderBy(f => f.Nombre)
                .Select(f => new Fletero(
                    nombre: f.Nombre,
                    patente: f.CuitEmpresaFlete ?? string.Empty,
                    nroFlete: f.IdFletero))
                .ToList();
        }

        /// <summary>
        /// Carga guías para el fletero desde hojas de ruta de flete activas (no cumplidas).
        /// Reglas:
        ///  - Retiros: Hoja.Tipo == Retiro (0) AND Estado guía ∈ {0,1}.
        ///  - Entregas: Hoja.Tipo == Entrega (1) AND Estado guía == 3 AND TipoEnvio ∈ {0,2}.
        /// </summary>
        private void CargarGuiasPorFletero(string idFletero)
        {
            _fleteroActualId = idFletero;

            Guias = new List<GuiaRendir>();
            Retiros = new List<GuiaRendir>();
            Entregas = new List<GuiaRendir>();

            if (string.IsNullOrWhiteSpace(idFletero))
                return;

            var hojasActivas = HojaDeRutaFleteAlmacen.HojasDeRutaFlete
                .Where(h => !h.Cumplida && string.Equals(h.IdFletero, idFletero, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!hojasActivas.Any()) return;

            var guiaDict = GuiaAlmacen.Guias.ToDictionary(g => g.NumeroGuia, g => g);

            var estadosRetiroValidos = new HashSet<int> { 0, 1 };
            const int estadoEntregaValido = 3;
            var tipoEnvioEntregaValidos = new HashSet<int> { 0, 2 }; // VaADomicilio (0), VaAAgencia (2)

            var retirosTmp = new Dictionary<string, GuiaRendir>();
            var entregasTmp = new Dictionary<string, GuiaRendir>();

            foreach (var hoja in hojasActivas)
            {
                if (hoja.Guias == null) continue;

                foreach (var nro in hoja.Guias.Distinct())
                {
                    if (!guiaDict.TryGetValue(nro, out var guíaEntidad))
                        continue;

                    var estadoEnum = guíaEntidad.EstadoActual?.Estado ?? guíaEntidad.Estado;
                    var estadoInt = (int)estadoEnum;
                    var tipoEnvioInt = (int)guíaEntidad.TipoEnvio;

                    if (hoja.Tipo == TipoHojaDeRutaEnum.Retiro)
                    {
                        if (!estadosRetiroValidos.Contains(estadoInt))
                            continue;

                        if (!retirosTmp.ContainsKey(nro))
                            retirosTmp[nro] = new GuiaRendir(nro, estadoInt.ToString(), idFletero);
                    }
                    else if (hoja.Tipo == TipoHojaDeRutaEnum.Entrega)
                    {
                        if (estadoInt != estadoEntregaValido)
                            continue;
                        if (!tipoEnvioEntregaValidos.Contains(tipoEnvioInt))
                            continue;

                        if (!entregasTmp.ContainsKey(nro))
                            entregasTmp[nro] = new GuiaRendir(nro, estadoInt.ToString(), idFletero);
                    }
                }
            }

            Retiros = retirosTmp.Values.ToList();
            Entregas = entregasTmp.Values.ToList();
            Guias = Retiros.Concat(Entregas).ToList();
        }

        public bool BuscarPorFletero(Fletero fletero)
        {
            if (fletero == null)
            {
                MessageBox.Show("Debe seleccionar un fletero.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            CargarGuiasPorFletero(fletero.NroFlete);

            if (Retiros.Count == 0 && Entregas.Count == 0)
            {
                MessageBox.Show("No hay encomiendas asociadas al fletero ingresado con los criterios requeridos.",
                    "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Guardado:
        /// - Guías seleccionadas en retiros => Estado = 2 (AdmitidoCDOrigen) y las hojas de ruta de tipo Retiro
        ///   que contengan alguna de esas guías cambian su Tipo a Entrega (0 -> 1).
        /// - Guías seleccionadas en entregas => Estado = 9 si TipoEnvio = 0 (VaADomicilio),
        ///   Estado = 10 si TipoEnvio = 2 (VaAAgencia).
        /// </summary>
        public bool GuardarCambios()
        {
            if (!Retiros.Any(r => r.Seleccionada) && !Entregas.Any(e => e.Seleccionada))
            {
                var confirm = MessageBox.Show(
                    "No se seleccionó ninguna guía de retiro ni entrega realizada.\n" +
                    "De continuar, el fletero no cumplió con ninguna obligación.\n¿Desea guardar igualmente?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes)
                    return false;
            }

            var confirmSave = MessageBox.Show(
                "¿Está seguro de que desea guardar los cambios realizados?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmSave != DialogResult.Yes)
                return false;

            var ahora = DateTime.Now;

            var numerosRetirosSeleccionados = new HashSet<string>(
                Retiros.Where(r => r.Seleccionada).Select(r => r.NroGuia));

            // Retiros seleccionados -> estado 2
            foreach (var guiaRendir in Retiros.Where(r => r.Seleccionada))
            {
                var entidad = GuiaAlmacen.Guias.FirstOrDefault(x => x.NumeroGuia == guiaRendir.NroGuia);
                if (entidad == null) continue;

                entidad.Estado = TipoEstadoGuiaEnum.AdmitidoCDOrigen;
                entidad.EstadoActual = new EstadoGuia
                {
                    Estado = entidad.Estado,
                    Fecha = ahora,
                    IdCentroDistribucion = entidad.IdCDOrigen
                };
                entidad.Historial ??= new List<EstadoGuia>();
                entidad.Historial.Add(entidad.EstadoActual);
            }

            // Hojas de retiro que contienen alguna guía seleccionada -> cambiar Tipo a Entrega
            if (numerosRetirosSeleccionados.Any() && !string.IsNullOrWhiteSpace(_fleteroActualId))
            {
                var hojasRetiroFletero = HojaDeRutaFleteAlmacen.HojasDeRutaFlete
                    .Where(h => !h.Cumplida
                                && h.Tipo == TipoHojaDeRutaEnum.Retiro
                                && string.Equals(h.IdFletero, _fleteroActualId, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                bool huboCambio = false;
                foreach (var hoja in hojasRetiroFletero)
                {
                    if (hoja.Guias != null && hoja.Guias.Any(g => numerosRetirosSeleccionados.Contains(g)))
                    {
                        hoja.Tipo = TipoHojaDeRutaEnum.Entrega;
                        huboCambio = true;
                    }
                }
                if (huboCambio)
                    HojaDeRutaFleteAlmacen.Grabar();
            }

            // Generar/actualizar Hoja de Ruta Micro UNA por (IdCDOrigen, IdCDDestino) con las guías que pasaron a estado 2
            if (numerosRetirosSeleccionados.Any())
            {
                var guiasRetiros = GuiaAlmacen.Guias
                    .Where(g => numerosRetirosSeleccionados.Contains(g.NumeroGuia))
                    .Where(g => !string.IsNullOrWhiteSpace(g.IdCDDestino) && !string.IsNullOrWhiteSpace(g.IdCDOrigen))
                    .ToList();

                var gruposPorTramo = guiasRetiros
                    .GroupBy(g => new { Origen = g.IdCDOrigen, Destino = g.IdCDDestino });

                foreach (var grp in gruposPorTramo)
                {
                    var idCDOrigen = grp.Key.Origen;
                    var idCDDestino = grp.Key.Destino;
                    var numeros = grp.Select(g => g.NumeroGuia).Distinct().ToList();

                    // Crear / actualizar hoja por ORIGEN+DESTINO
                    var hojaId = CrearHojaMicroSiNoExiste(idCDOrigen, idCDDestino, numeros);

                    // Asignar micro estrictamente por tramo ORIGEN->DESTINO
                    AsignarMicroParaRuta(idCDOrigen, idCDDestino, hojaId);
                }
            }

            // Entregas seleccionadas -> estado 9 (domicilio) o 10 (agencia)
            foreach (var guiaRendir in Entregas.Where(e => e.Seleccionada))
            {
                var entidad = GuiaAlmacen.Guias.FirstOrDefault(x => x.NumeroGuia == guiaRendir.NroGuia);
                if (entidad == null) continue;

                var tipoEnvioInt = (int)entidad.TipoEnvio;
                TipoEstadoGuiaEnum nuevoEstado =
                    tipoEnvioInt == 0 ? TipoEstadoGuiaEnum.EntregadoClienteDomicilio :
                    tipoEnvioInt == 2 ? TipoEstadoGuiaEnum.EntregadoAgencia :
                    entidad.Estado;

                if (nuevoEstado != entidad.Estado)
                {
                    entidad.Estado = nuevoEstado;
                    entidad.EstadoActual = new EstadoGuia
                    {
                        Estado = entidad.Estado,
                        Fecha = ahora,
                        IdCentroDistribucion = entidad.IdCDDestino
                    };
                    entidad.Historial ??= new List<EstadoGuia>();
                    entidad.Historial.Add(entidad.EstadoActual);
                }
            }

            GuiaAlmacen.Grabar();

            MessageBox.Show("Datos guardados correctamente.", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }

        // Crea/actualiza hoja por (origen,destino) y agrega guías
        private static int CrearHojaMicroSiNoExiste(string idCDOrigen, string idCDDestino, List<string> numerosGuias)
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

        // Asignación estricta: solo micros cuyo Recorrido contiene el tramo exacto ORIGEN->DESTINO
        private static void AsignarMicroParaRuta(string idCDOrigen, string idCDDestino, int idHDRMicro)
        {
            if (idHDRMicro <= 0 || string.IsNullOrWhiteSpace(idCDOrigen) || string.IsNullOrWhiteSpace(idCDDestino))
                return;

            var micros = MicroAlmacen.Micros;
            if (!micros.Any()) return;

            var candidatos = micros
                .Where(m => (m.Recorrido ?? new List<Parada>())
                    .Any(p => p.IdCDOrigen == idCDOrigen && p.IdCDDestino == idCDDestino))
                .ToList();

            if (!candidatos.Any()) return;

            // Preferir micro con menos hojas asignadas (balanceo) entre candidatos válidos
            var elegido = candidatos
                .OrderBy(m => (m.HojasDeRuta ?? new List<int>()).Count)
                .First();

            elegido.HojasDeRuta ??= new List<int>();
            if (!elegido.HojasDeRuta.Contains(idHDRMicro))
            {
                elegido.HojasDeRuta.Add(idHDRMicro);
                MicroAlmacen.Grabar();
            }
        }
    }
}
