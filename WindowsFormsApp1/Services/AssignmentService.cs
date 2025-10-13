using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Genera archivos de asignación por fletero (retiros/entregas) a partir del CSV maestro.
    /// Crea/actualiza:
    ///   data/asignaciones/{fletero}_retiros.txt
    ///   data/asignaciones/{fletero}_entregas.txt
    /// </summary>
    public class AssignmentService
    {
        private readonly string _dataDir;
        private readonly string _masterFile;

        /// <param name="dataDir">Directorio base de datos (default: "data")</param>
        /// <param name="masterFile">Nombre del archivo maestro (default: "guias_master.txt")</param>
        public AssignmentService(string dataDir = "data", string masterFile = "guias_master.txt")
        {
            _dataDir = dataDir;
            _masterFile = masterFile;
        }

        /// <summary>
        /// Lee el maestro, separa RETIROS/ENTREGAS y las reparte aleatoriamente a los fleteros indicados.
        /// Devuelve la cantidad total asignada de cada tipo.
        /// </summary>
        public (int retiros, int entregas) GenerarAsignacionesAleatorias(IEnumerable<string> fleteros)
        {
            // Convertimos a List para poder indexar y contar (C# 7.3-friendly)
            var listaFleteros = (fleteros == null) ? new List<string>() : fleteros.ToList();
            if (listaFleteros.Count == 0)
                return (0, 0);

            var masterPath = Path.Combine(_dataDir, _masterFile);
            if (!File.Exists(masterPath))
                throw new FileNotFoundException("No se encontró el archivo maestro: " + masterPath);

            var asignDir = Path.Combine(_dataDir, "asignaciones");
            Directory.CreateDirectory(asignDir);

            // Limpio archivos previos de cada fletero (overwrite)
            foreach (var f in listaFleteros)
            {
                var baseName = SanitizarNombre(f);
                File.WriteAllText(Path.Combine(asignDir, baseName + "_retiros.txt"), string.Empty);
                File.WriteAllText(Path.Combine(asignDir, baseName + "_entregas.txt"), string.Empty);
            }

            // Parseo maestro
            var separado = LeerYSepararDesdeMaster(masterPath);
            var retiros = separado.retiros;
            var entregas = separado.entregas;

            // Reparto aleatorio
            var rnd = new Random();
            int countR = 0, countE = 0;

            foreach (var guia in retiros)
            {
                var fletero = listaFleteros[rnd.Next(listaFleteros.Count)];
                var file = Path.Combine(asignDir, SanitizarNombre(fletero) + "_retiros.txt");
                File.AppendAllLines(file, new[] { guia });
                countR++;
            }

            foreach (var guia in entregas)
            {
                var fletero = listaFleteros[rnd.Next(listaFleteros.Count)];
                var file = Path.Combine(asignDir, SanitizarNombre(fletero) + "_entregas.txt");
                File.AppendAllLines(file, new[] { guia });
                countE++;
            }

            return (countR, countE);
        }

        // =======================
        // Parsing del maestro
        // =======================
        private static (List<string> retiros, List<string> entregas) LeerYSepararDesdeMaster(string masterPath)
        {
            string[] lineas;
            try
            {
                lineas = File.ReadAllLines(masterPath, Encoding.UTF8);
            }
            catch
            {
                lineas = File.ReadAllLines(masterPath);
            }

            lineas = lineas.Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
            if (lineas.Length == 0) return (new List<string>(), new List<string>());

            // Detecto delimitador por heurística
            char delim = DetectarDelimitador(lineas[0]);

            // ¿Tiene header?
            bool tieneHeader = PoseeHeader(lineas[0], delim);

            int startIdx = 0;
            var headerIndex = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            if (tieneHeader)
            {
                var headers = SplitCsvLine(lineas[0], delim).Select(h => (h ?? "").Trim()).ToArray();
                for (int i = 0; i < headers.Length; i++)
                {
                    if (!headerIndex.ContainsKey(headers[i]))
                        headerIndex[headers[i]] = i;
                }
                startIdx = 1;
            }

            // Identifico columnas candidatas
            int idxTipo = GetColIndex(headerIndex, new[]
            {
                "tipo","operacion","tipo_operacion","clase","accion","categoria"
            });

            int idxGuia = GetColIndex(headerIndex, new[]
            {
                "guia","nro_guia","numero_guia","num_guia","id_guia","id","pedido_id","nroguia"
            });

            var retiros = new List<string>();
            var entregas = new List<string>();

            for (int i = startIdx; i < lineas.Length; i++)
            {
                var cols = SplitCsvLine(lineas[i], delim);
                if (cols.Length == 0) continue;

                string tipo = (idxTipo >= 0 && idxTipo < cols.Length) ? (cols[idxTipo] ?? "").Trim() : InferirTipoDesdeLinea(lineas[i]);
                string guia = (idxGuia >= 0 && idxGuia < cols.Length) ? (cols[idxGuia] ?? "").Trim() : ExtraerGuiaFallback(cols);

                if (string.IsNullOrWhiteSpace(guia))
                    guia = lineas[i].Trim(); // último recurso: toda la línea

                var t = (tipo ?? "").ToUpperInvariant();

                if (EsRetiro(t))
                    retiros.Add(guia);
                else if (EsEntrega(t))
                    entregas.Add(guia);
                else
                {
                    // Si no puedo determinar tipo, lo ignoro o lo podrías mandar a un bucket "otros"
                }
            }

            retiros = retiros.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToList();
            entregas = entregas.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToList();

            return (retiros, entregas);
        }

        private static bool EsRetiro(string t)
        {
            return t.Contains("RETIRO") || t == "RET" || t.Contains("PICKUP");
        }

        private static bool EsEntrega(string t)
        {
            return t.Contains("ENTREGA") || t == "ENT" || t.Contains("DELIVERY");
        }

        private static string ExtraerGuiaFallback(string[] cols)
        {
            foreach (var c in cols.Take(4))
            {
                var v = (c ?? "").Trim();
                if (string.IsNullOrEmpty(v)) continue;
                if (v.Any(char.IsDigit) && v.Length >= 5) return v;
            }
            return cols.FirstOrDefault(c => !string.IsNullOrWhiteSpace(c))?.Trim() ?? "";
        }

        private static string InferirTipoDesdeLinea(string linea)
        {
            var l = (linea ?? "").ToUpperInvariant();
            if (l.Contains("RETIRO")) return "RETIRO";
            if (l.Contains("ENTREGA")) return "ENTREGA";
            return "";
        }

        private static int GetColIndex(Dictionary<string, int> headerIndex, IEnumerable<string> candidates)
        {
            if (headerIndex == null || headerIndex.Count == 0) return -1;

            foreach (var c in candidates)
            {
                int idx;
                if (headerIndex.TryGetValue(c, out idx)) return idx;
            }
            // búsqueda por "contiene"
            foreach (var kv in headerIndex)
            {
                var k = kv.Key.Replace(" ", "").Replace("-", "").Replace("_", "").ToLowerInvariant();
                foreach (var c in candidates)
                {
                    var cc = c.Replace(" ", "").Replace("-", "").Replace("_", "").ToLowerInvariant();
                    if (k.Contains(cc)) return kv.Value;
                }
            }
            return -1;
        }

        private static char DetectarDelimitador(string firstLine)
        {
            int comas = firstLine.Count(c => c == ',');
            int puntosYComas = firstLine.Count(c => c == ';');
            int tabs = firstLine.Count(c => c == '\t');

            if (puntosYComas >= comas && puntosYComas >= tabs) return ';';
            if (comas >= puntosYComas && comas >= tabs) return ',';
            if (tabs > 0) return '\t';
            return ';';
        }

        private static bool PoseeHeader(string line, char delim)
        {
            var cols = SplitCsvLine(line, delim);
            if (cols.Length == 0) return false;

            int texty = cols.Count(c => !string.IsNullOrWhiteSpace(c) && !c.Any(char.IsDigit));
            return texty >= Math.Max(1, cols.Length / 2);
        }

        // Split simple; si necesitás CSV entrecomillado complejo, usar parser CSV dedicado
        private static string[] SplitCsvLine(string line, char delim)
        {
            return (line ?? "").Split(delim);
        }

        private static string SanitizarNombre(string nombre)
        {
            var invalid = Path.GetInvalidFileNameChars();
            return new string((nombre ?? "").Select(ch => invalid.Contains(ch) ? '_' : ch).ToArray());
        }
    }
}
