using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Lógica de rendición de primera milla (retira/entrega).
    /// - Lee asignaciones por fletero (si existen) desde archivos dedicados.
    /// - Si no existen, toma IDs recientes desde data/guias_master.txt (única fuente).
    /// - Persiste rendiciones en CSV mediante IRendicionRepository.
    /// - Actualiza archivos de asignación removiendo las guías rendidas.
    /// </summary>
    internal sealed class RendicionService
    {
        private readonly IRendicionRepository _repo;

        // Prefijos para archivos de asignación por fletero
        private const string ASIG_RETIRO_PREFIX = "asignaciones_retiro_";
        private const string ASIG_ENTREGA_PREFIX = "asignaciones_entrega_";

        // Master unificado generado por imposición y actualizado por otros procesos
        private const string MASTER_FILE = "guias_master.txt";

        public RendicionService(IRendicionRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        /// <summary>Ruta del CSV de rendiciones (para mostrar/abrir desde la UI).</summary>
        public string ObtenerRutaArchivo() => _repo.FilePath;

        /// <summary>Listado de fleteros (catálogo estático por ahora).</summary>
        public IReadOnlyList<string> ObtenerFleteros() => ComboData.Fleteros;

        /// <summary>
        /// Carga asignaciones del fletero desde archivos. Si no existen, hace un fallback
        /// leyendo IDs recientes desde el archivo maestro (guias_master.txt).
        /// </summary>
        public (List<string> retirosPendientes, List<string> entregasPendientes) CargarAsignaciones(string fletero)
        {
            var (retiros, entregas) = LeerAsignacionesDesdeArchivos(fletero);
            if (retiros.Count > 0 || entregas.Count > 0)
                return (retiros, entregas);

            // Fallback: tomar últimos IDs del master (sin filtrar por tipo ni estado)
            var ids = LeerGuiasDesdeMaster()
                .Distinct()
                .OrderByDescending(x => x) // asume formato numérico o lexicográfico creciente
                .Take(10)
                .ToList();

            var mitad = ids.Count / 2;
            var retirosAuto = ids.Take(mitad).ToList();
            var entregasAuto = ids.Skip(mitad).ToList();

            return (retirosAuto, entregasAuto);
        }

        /// <summary>Directorio base de datos de la app.</summary>
        private string DataDir => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");

        /// <summary>
        /// Lee asignaciones persistidas para el fletero (si existen).
        /// Formato simple: un N° de guía por línea.
        /// </summary>
        private (List<string> retiros, List<string> entregas) LeerAsignacionesDesdeArchivos(string fletero)
        {
            var safe = SanitizeFileName(fletero);
            var fRet = Path.Combine(DataDir, $"{ASIG_RETIRO_PREFIX}{safe}.txt");
            var fEnt = Path.Combine(DataDir, $"{ASIG_ENTREGA_PREFIX}{safe}.txt");

            List<string> leer(string path)
            {
                if (!File.Exists(path)) return new List<string>();
                return File.ReadAllLines(path)
                           .Select(l => (l ?? "").Trim())
                           .Where(l => !string.IsNullOrWhiteSpace(l))
                           .ToList();
            }

            return (leer(fRet), leer(fEnt));
        }

        /// <summary>
        /// Devuelve true si existen archivos (no vacíos) de asignación para el fletero.
        /// Útil para decidir si mostrar "Cargar asignaciones" o "Generar".
        /// </summary>
        public bool HayArchivosAsignacion(string fletero)
        {
            var safe = SanitizeFileName(fletero ?? "");
            var fRet = Path.Combine(DataDir, $"{ASIG_RETIRO_PREFIX}{safe}.txt");
            var fEnt = Path.Combine(DataDir, $"{ASIG_ENTREGA_PREFIX}{safe}.txt");

            bool vacio(string p) => !File.Exists(p) || new FileInfo(p).Length == 0;
            return !(vacio(fRet) && vacio(fEnt));
        }

        private static string SanitizeFileName(string s)
        {
            var invalid = Path.GetInvalidFileNameChars();
            return new string((s ?? "").Select(ch => invalid.Contains(ch) ? '_' : ch).ToArray());
        }

        private static List<string> LeerLineasSiExiste(string path)
        {
            if (!File.Exists(path)) return new List<string>();
            return File.ReadAllLines(path)
                       .Select(x => (x ?? "").Trim())
                       .Where(x => !string.IsNullOrWhiteSpace(x))
                       .Distinct()
                       .ToList();
        }

        private static void EscribirLineas(string path, IEnumerable<string> lineas)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllLines(path, (lineas ?? Enumerable.Empty<string>()));
        }

        /// <summary>
        /// Luego de rendir, se quitan de los archivos de asignación las guías reportadas,
        /// manteniendo sincronizada la “lista de pendientes” del fletero.
        /// </summary>
        private void ActualizarAsignacionesTrasRendicion(string fletero, IEnumerable<string> guiasRetiros, IEnumerable<string> guiasEntregas)
        {
            var safe = SanitizeFileName(fletero);
            var fRet = Path.Combine(DataDir, $"{ASIG_RETIRO_PREFIX}{safe}.txt");
            var fEnt = Path.Combine(DataDir, $"{ASIG_ENTREGA_PREFIX}{safe}.txt");

            var retiros = LeerLineasSiExiste(fRet);
            var entregas = LeerLineasSiExiste(fEnt);

            var setR = new HashSet<string>((guiasRetiros ?? Enumerable.Empty<string>()).Select(x => x.Trim()));
            var setE = new HashSet<string>((guiasEntregas ?? Enumerable.Empty<string>()).Select(x => x.Trim()));

            if (retiros.Count > 0 && setR.Count > 0)
            {
                retiros = retiros.Where(g => !setR.Contains(g.Trim())).ToList();
                EscribirLineas(fRet, retiros);
            }
            if (entregas.Count > 0 && setE.Count > 0)
            {
                entregas = entregas.Where(g => !setE.Contains(g.Trim())).ToList();
                EscribirLineas(fEnt, entregas);
            }
        }

        /// <summary>
        /// Lee IDs de guías desde el archivo maestro unificado (data/guias_master.txt).
        /// Solo devuelve la primera columna (Id). No filtra por tipo ni estado.
        /// </summary>
        private IEnumerable<string> LeerGuiasDesdeMaster()
        {
            var master = Path.Combine(DataDir, MASTER_FILE);
            if (!File.Exists(master)) yield break;

            foreach (var (linea, idx) in File.ReadLines(master).Select((l, i) => (l, i)))
            {
                if (idx == 0) continue; // header
                if (string.IsNullOrWhiteSpace(linea)) continue;

                // Layout compartido con CsvOrderRepository/CsvGuiaMaster:
                // 0: Id ; 1: FechaHora ; ... ; 13: Estado
                var campos = linea.Split(';');
                if (campos.Length == 0) continue;

                var id = campos[0].Trim().Trim('"');
                if (!string.IsNullOrWhiteSpace(id))
                    yield return id;
            }
        }

        /// <summary>
        /// Persiste rendición (retiros admitidos y entregas realizadas) y limpia pendientes del fletero.
        /// </summary>
        public void Guardar(string fletero, IEnumerable<string> guiasRetirosAdmitidos, IEnumerable<string> guiasEntregasRealizadas)
        {
            var ahora = DateTime.Now;
            var registros = new List<RendicionLinea>();

            foreach (var g in guiasRetirosAdmitidos ?? Enumerable.Empty<string>())
            {
                registros.Add(new RendicionLinea
                {
                    FechaHora = ahora,
                    Fletero = fletero,
                    Guia = g,
                    Tipo = "Retiro",
                    Resultado = "Admitida en CD Origen"
                });
            }

            foreach (var g in guiasEntregasRealizadas ?? Enumerable.Empty<string>())
            {
                registros.Add(new RendicionLinea
                {
                    FechaHora = ahora,
                    Fletero = fletero,
                    Guia = g,
                    Tipo = "Entrega",
                    Resultado = "Entregada a cliente"
                });
            }

            _repo.Append(registros);
            ActualizarAsignacionesTrasRendicion(fletero, guiasRetirosAdmitidos, guiasEntregasRealizadas);
        }
    }
}
