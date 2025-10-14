using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Servicio de asignaciones a fleteros.
    /// - Escribe/lee worklists de asignación en /data/asignaciones/{fletero}_retiros.txt
    /// - NO cambia estados en guias_master.txt (eso lo hace Rendición al Guardar).
    /// </summary>
    internal sealed class AssignmentService
    {
        private string AsignDir
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "asignaciones"); }
        }

        private static string SanitizeFileName(string s)
        {
            if (string.IsNullOrEmpty(s)) return "_";
            foreach (char c in Path.GetInvalidFileNameChars())
                s = s.Replace(c, '_');
            return s.Trim();
        }

        private static string FileRetiros(string dir, string fleteroSafe)
        {
            return Path.Combine(dir, fleteroSafe + "_retiros.txt");
        }

        /// <summary>
        /// Asigna guías a fleteros en round-robin y escribe/actualiza los TXT por fletero.
        /// No toca estados del master (permanecen como 'Impuesta').
        /// Devuelve la cantidad total asignada.
        /// </summary>
        public int AsignarRetirosPorIds(IList<string> guias, IEnumerable<string> fleterosEnumerable)
        {
            if (guias == null || guias.Count == 0) return 0;
            if (fleterosEnumerable == null) return 0;

            var fleteros = fleterosEnumerable.ToList();
            if (fleteros.Count == 0) return 0;

            Directory.CreateDirectory(AsignDir);

            int idxF = 0;
            var porFletero = new Dictionary<string, List<string>>();

            foreach (string g in guias)
            {
                string guia = (g ?? "").Trim();
                if (guia == "") continue;

                string f = fleteros[idxF % fleteros.Count];
                idxF++;

                if (!porFletero.ContainsKey(f)) porFletero[f] = new List<string>();
                porFletero[f].Add(guia);
            }

            foreach (var kv in porFletero)
            {
                string fSafe = SanitizeFileName(kv.Key);
                string pathR = FileRetiros(AsignDir, fSafe);

                List<string> existentes = File.Exists(pathR)
                    ? File.ReadAllLines(pathR).Select(l => (l ?? "").Trim()).Where(l => l != "").ToList()
                    : new List<string>();

                var set = new HashSet<string>(existentes, StringComparer.OrdinalIgnoreCase);
                foreach (string guia in kv.Value)
                    if (!set.Contains(guia)) existentes.Add(guia);

                File.WriteAllLines(pathR, existentes);
            }

            return guias.Count;
        }

        /// <summary>
        /// Genera asignaciones "aleatorias" (round-robin) desde el estado 'Impuesta' a los TXT de fleteros.
        /// No cambia estados en el master. Devuelve (retirosAsignados, entregasAsignadas=0).
        /// </summary>
        public (int retiros, int entregas) GenerarAsignacionesAleatorias(IEnumerable<string> fleterosEnumerable)
        {
            var fleteros = (fleterosEnumerable ?? Enumerable.Empty<string>()).ToList();
            if (fleteros.Count == 0) return (0, 0);

            // Tomamos todas las "Impuestas" del master
            var master = new CsvGuiaMaster();
            var impuestas = master.ListarPorEstado(GuiaEstados.Impuesta).ToList();
            if (impuestas.Count == 0) return (0, 0);

            // Reutilizamos la lógica de escritura de TXT
            AsignarRetirosPorIds(impuestas, fleteros);
            // Entregas automáticas NO se generan aquí (las manejaremos luego cuando lleguen a "Admitida en CD destino")
            return (impuestas.Count, 0);
        }

        /// <summary>Lee las guías asignadas de retiros para un fletero (si no existe, devuelve lista vacía).</summary>
        public List<string> ListarRetirosAsignados(string fletero)
        {
            string safe = SanitizeFileName(fletero ?? "");
            Directory.CreateDirectory(AsignDir);
            string path = FileRetiros(AsignDir, safe);

            if (!File.Exists(path)) return new List<string>();
            return File.ReadAllLines(path).Select(l => (l ?? "").Trim())
                .Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
        }

        /// <summary>Quita del TXT del fletero las guías indicadas (ej.: luego de rendir).</summary>
        public void RemoverRetirosAsignados(string fletero, IEnumerable<string> guiasProcesadas)
        {
            string safe = SanitizeFileName(fletero ?? "");
            Directory.CreateDirectory(AsignDir);
            string path = FileRetiros(AsignDir, safe);

            if (!File.Exists(path)) return;

            var set = new HashSet<string>((guiasProcesadas ?? Enumerable.Empty<string>())
                .Select(x => (x ?? "").Trim()), StringComparer.OrdinalIgnoreCase);

            var actual = File.ReadAllLines(path)
                .Select(l => (l ?? "").Trim())
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Where(l => !set.Contains(l))
                .ToList();

            File.WriteAllLines(path, actual);
        }
    }
}
