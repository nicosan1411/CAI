using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    internal sealed class AssignmentService
    {
        private readonly string _dataDir;

        public AssignmentService(string dataDir = "data")
        {
            _dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dataDir);
            Directory.CreateDirectory(_dataDir);
        }

        public (int retiros, int entregas) GenerarAsignacionesAleatorias(IReadOnlyList<string> fleteros)
        {
            if (fleteros == null || fleteros.Count == 0)
                throw new InvalidOperationException("No hay fleteros para asignar.");

            var retiros = LeerIdsFiltrando(Path.Combine(_dataDir, "imposiciones_callcenter.txt"), 0, 3, "domicilio"); // RetiroTipo col3
            var entregas = LeerIdsFiltrando(Path.Combine(_dataDir, "admision_cd.txt"), 0, 5, "domicilio"); // EnvioTipo col5

            Shuffle(retiros);
            Shuffle(entregas);

            var mapRetiros = DistribuirRoundRobin(retiros, fleteros);
            var mapEntregas = DistribuirRoundRobin(entregas, fleteros);

            int totalR = 0, totalE = 0;
            foreach (var f in fleteros)
            {
                var safe = SanitizeFileName(f);
                EscribirLineas(Path.Combine(_dataDir, $"asignaciones_retiro_{safe}.txt"), mapRetiros.TryGetValue(f, out var lr) ? lr : Enumerable.Empty<string>());
                EscribirLineas(Path.Combine(_dataDir, $"asignaciones_entrega_{safe}.txt"), mapEntregas.TryGetValue(f, out var le) ? le : Enumerable.Empty<string>());
                totalR += mapRetiros.TryGetValue(f, out var rlist) ? rlist.Count : 0;
                totalE += mapEntregas.TryGetValue(f, out var elist) ? elist.Count : 0;
            }
            return (totalR, totalE);
        }

        // ===== utilitarios lectura CSV =====
        private static List<string> LeerIdsFiltrando(string path, int colId, int filtroCol, string mustContain)
        {
            var result = new List<string>();
            if (!File.Exists(path)) return result;

            var lines = File.ReadAllLines(path, Encoding.UTF8);
            for (int i = 0; i < lines.Length; i++)
            {
                if (i == 0) continue; // header
                var row = ParseCsvLine(lines[i], ';');
                if (row.Length <= Math.Max(colId, filtroCol)) continue;

                var id = TrimQuotes(row[colId]).Trim();
                var filtro = Normalize(TrimQuotes(row[filtroCol]));

                if (string.IsNullOrWhiteSpace(id)) continue;
                if (filtro.Contains(Normalize(mustContain)))
                    result.Add(id);
            }
            return result.Distinct().ToList();
        }

        // ===== helpers =====
        private static string Normalize(string input)
        {
            if (input == null) return "";
            var formD = input.ToLowerInvariant().Normalize(NormalizationForm.FormD);
            var chars = formD.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }

        private static string SanitizeFileName(string s)
        {
            var invalid = Path.GetInvalidFileNameChars();
            return new string((s ?? "").Select(ch => invalid.Contains(ch) ? '_' : ch).ToArray());
        }

        private static void Shuffle<T>(IList<T> list)
        {
            var rnd = new Random(unchecked((int)DateTime.Now.Ticks));
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
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

        // parser CSV simple con comillas
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
    }
}
