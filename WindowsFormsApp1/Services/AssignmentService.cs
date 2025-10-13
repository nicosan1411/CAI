using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Genera y persiste asignaciones de guías a fleteros leyendo desde el archivo maestro: data/guias_master.txt
    /// 
    /// Escribe: asignaciones_retiro_{fletero}.txt y asignaciones_entrega_{fletero}.txt
    /// </summary>
    internal sealed class AssignmentService
    {
        // ---------- Configuración / Constantes ----------
        private readonly string _dataDir;
        private const string MASTER_FILE = "guias_master.txt";
        private const char SEP = ';';

        // Índices de columnas en guias_master.txt (mismo layout que CsvOrderRepository/CsvGuiaMaster)
        private const int COL_ID = 0;
        private const int COL_RETIRO_TIPO = 3;
        private const int COL_ENVIO_TIPO = 5;
        private const int COL_ESTADO = 13;

        // Búsquedas "a domicilio" (se normaliza acentos y mayúsculas)
        private const string KEY_DOMICILIO = "domicilio";

        // (Opcional) Filtros por estado recomendados para acotar universo
        private static readonly string[] ESTADOS_VALIDOS_RETIRO = { "Impuesta", "En proceso de retiro" };
        private static readonly string[] ESTADOS_VALIDOS_ENTREGA = { "Admitida en CD destino", "En proceso de entrega" };

        // Random estático para evitar reseed
        private static readonly Random _rnd = new Random();

        public AssignmentService(string dataDir = "data")
        {
            _dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dataDir);
            Directory.CreateDirectory(_dataDir);
        }

        /// <summary>
        /// Genera asignaciones round-robin a partir del archivo maestro.
        /// Devuelve el total de retiros y entregas asignadas.
        /// </summary>
        /// <param 'name'="fleteros">Listado visible (ej. ComboData.Fleteros)</param>
        /// <param 'name'="usarFiltrosEstado">
        /// true=filtra por estados sugeridos (ver arriba), false=no filtra por estado.
        /// </param>
        public (int retiros, int entregas) GenerarAsignacionesDesdeMaster(
            IReadOnlyList<string> fleteros, bool usarFiltrosEstado = true)
        {
            if (fleteros == null || fleteros.Count == 0)
                throw new InvalidOperationException("No hay fleteros para asignar.");

            var masterPath = Path.Combine(_dataDir, MASTER_FILE);
            if (!File.Exists(masterPath))
                throw new FileNotFoundException($"No se encontró {_dataDir}\\{MASTER_FILE}");

            var (idsRetiros, idsEntregas) = LeerIdsDesdeMaster(masterPath, usarFiltrosEstado);

            Shuffle(idsRetiros);
            Shuffle(idsEntregas);

            var mapRetiros = DistribuirRoundRobin(idsRetiros, fleteros);
            var mapEntregas = DistribuirRoundRobin(idsEntregas, fleteros);

            int totalR = 0, totalE = 0;
            foreach (var f in fleteros)
            {
                var safe = SanitizeFileName(f);
                EscribirLineas(Path.Combine(_dataDir, $"asignaciones_retiro_{safe}.txt"),
                               mapRetiros.TryGetValue(f, out var lr) ? lr : Enumerable.Empty<string>());
                EscribirLineas(Path.Combine(_dataDir, $"asignaciones_entrega_{safe}.txt"),
                               mapEntregas.TryGetValue(f, out var le) ? le : Enumerable.Empty<string>());

                totalR += mapRetiros.TryGetValue(f, out var rlist) ? rlist.Count : 0;
                totalE += mapEntregas.TryGetValue(f, out var elist) ? elist.Count : 0;
            }

            return (totalR, totalE);
        }

        // ================== Lectura desde MASTER ==================
        private static (List<string> retiros, List<string> entregas) LeerIdsDesdeMaster(string masterPath, bool usarFiltrosEstado)
        {
            var retiros = new List<string>();
            var entregas = new List<string>();

            var lines = File.ReadAllLines(masterPath, Encoding.UTF8);
            for (int i = 0; i < lines.Length; i++)
            {
                if (i == 0) continue; // header
                var line = lines[i];
                if (string.IsNullOrWhiteSpace(line)) continue;

                var row = ParseCsvLine(line, SEP);
                if (row.Length <= Math.Max(Math.Max(COL_ID, COL_RETIRO_TIPO), Math.Max(COL_ENVIO_TIPO, COL_ESTADO)))
                    continue;

                var id = TrimQuotes(row[COL_ID]).Trim();
                var retiroTipo = Normalize(TrimQuotes(row[COL_RETIRO_TIPO]));
                var envioTipo = Normalize(TrimQuotes(row[COL_ENVIO_TIPO]));
                var estado = TrimQuotes(row[COL_ESTADO]).Trim();

                if (string.IsNullOrWhiteSpace(id)) continue;

                // -------- Retiros: RetiroTipo contiene "domicilio" --------
                if (!string.IsNullOrEmpty(retiroTipo) && retiroTipo.Contains(Normalize(KEY_DOMICILIO)))
                {
                    if (!usarFiltrosEstado || ESTADOS_VALIDOS_RETIRO.Contains(estado, StringComparer.OrdinalIgnoreCase))
                        retiros.Add(id);
                }

                // -------- Entregas: EnvioTipo contiene "domicilio" --------
                if (!string.IsNullOrEmpty(envioTipo) && envioTipo.Contains(Normalize(KEY_DOMICILIO)))
                {
                    if (!usarFiltrosEstado || ESTADOS_VALIDOS_ENTREGA.Contains(estado, StringComparer.OrdinalIgnoreCase))
                        entregas.Add(id);
                }
            }

            // Distintos por seguridad (si en algún momento duplicamos líneas en CSV)
            return (retiros.Distinct().ToList(), entregas.Distinct().ToList());
        }

        // ================== Helpers ==================
        private static string SanitizeFileName(string s)
        {
            var invalid = Path.GetInvalidFileNameChars();
            return new string((s ?? "").Select(ch => invalid.Contains(ch) ? '_' : ch).ToArray());
        }

        private static void Shuffle<T>(IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = _rnd.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }

        private static Dictionary<string, List<string>> DistribuirRoundRobin(List<string> ids, IReadOnlyList<string> fleteros)
        {
            var map = fleteros.ToDictionary(f => f, _ => new List<string>());
            if (ids == null || ids.Count == 0) return map;

            int idx = 0;
            foreach (var id in ids)
            {
                var f = fleteros[idx % fleteros.Count];
                map[f].Add(id);
                idx++;
            }
            return map;
        }

        private static void EscribirLineas(string path, IEnumerable<string> lineas)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllLines(path, (lineas ?? Enumerable.Empty<string>()));
        }

        /// <summary>Parser CSV simple con soporte de comillas dobles escapadas.</summary>
        private static string[] ParseCsvLine(string line, char sep)
        {
            if (line == null) return Array.Empty<string>();
            var res = new List<string>();
            bool inQuotes = false;
            var cur = new StringBuilder();

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '\"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '\"') { cur.Append('\"'); i++; }
                    else { inQuotes = !inQuotes; }
                }
                else if (c == sep && !inQuotes)
                {
                    res.Add(cur.ToString()); cur.Clear();
                }
                else cur.Append(c);
            }
            res.Add(cur.ToString());
            return res.ToArray();
        }

        private static string TrimQuotes(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            if (s.Length >= 2 && s[0] == '\"' && s[s.Length - 1] == '\"')
                return s.Substring(1, s.Length - 2).Replace("\"\"", "\"");
            return s;
        }

        private static string Normalize(string input)
        {
            if (input == null) return "";
            var formD = input.ToLowerInvariant().Normalize(NormalizationForm.FormD);
            var chars = formD.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }
    }
}
