using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Asignación de guías a micros (por patente).
    /// - Guarda listas en: /data/asignaciones_micros/{patente}_despachos.txt
    /// - No cambia estados en el master.
    /// - Mantiene asignadas las guías desde "Admitida en CD origen" hasta "Admitida en CD destino".
    /// </summary>
    internal sealed class MicroAssignmentService
    {
        private string AsignDir
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "asignaciones_micros"); }
        }

        private static string SanitizeFileName(string s)
        {
            if (string.IsNullOrEmpty(s)) return "_";
            foreach (var c in Path.GetInvalidFileNameChars()) s = s.Replace(c, '_');
            return s.Trim();
        }

        private static string FileDespachos(string dir, string patenteSafe)
        {
            return Path.Combine(dir, patenteSafe + "_despachos.txt");
        }

        /// <summary>
        /// Asigna guías a patentes (round-robin) y escribe/actualiza los TXT.
        /// </summary>
        public int AsignarDespachosPorIds(IList<string> guias, IEnumerable<string> patentesEnumerable)
        {
            if (guias == null || guias.Count == 0) return 0;
            if (patentesEnumerable == null) return 0;

            var patentes = patentesEnumerable.Where(p => !string.IsNullOrWhiteSpace(p)).ToList();
            if (patentes.Count == 0) return 0;

            Directory.CreateDirectory(AsignDir);

            int idx = 0;
            var porPatente = new Dictionary<string, List<string>>();

            foreach (var g in guias)
            {
                var guia = (g ?? "").Trim();
                if (guia == "") continue;

                var p = patentes[idx % patentes.Count];
                idx++;

                if (!porPatente.ContainsKey(p)) porPatente[p] = new List<string>();
                porPatente[p].Add(guia);
            }

            foreach (var kv in porPatente)
            {
                var pSafe = SanitizeFileName(kv.Key);
                var path = FileDespachos(AsignDir, pSafe);

                var existentes = File.Exists(path)
                    ? File.ReadAllLines(path).Select(l => (l ?? "").Trim()).Where(l => l != "").ToList()
                    : new List<string>();

                var set = new HashSet<string>(existentes, StringComparer.OrdinalIgnoreCase);
                foreach (var guia in kv.Value)
                    if (!set.Contains(guia)) existentes.Add(guia);

                File.WriteAllLines(path, existentes);
            }

            return guias.Count;
        }

        /// <summary>Lista guías asignadas a una patente (si no hay archivo, lista vacía).</summary>
        public List<string> ListarAsignadas(string patente)
        {
            var safe = SanitizeFileName(patente ?? "");
            Directory.CreateDirectory(AsignDir);
            var path = FileDespachos(AsignDir, safe);
            if (!File.Exists(path)) return new List<string>();
            return File.ReadAllLines(path)
                       .Select(l => (l ?? "").Trim())
                       .Where(l => l != "").ToList();
        }

        /// <summary>Quita de la patente las guías indicadas (p.ej. cuando llegan a CD destino).</summary>
        public void RemoverAsignadas(string patente, IEnumerable<string> guias)
        {
            var safe = SanitizeFileName(patente ?? "");
            Directory.CreateDirectory(AsignDir);
            var path = FileDespachos(AsignDir, safe);
            if (!File.Exists(path)) return;

            var set = new HashSet<string>((guias ?? Enumerable.Empty<string>())
                .Select(x => (x ?? "").Trim()), StringComparer.OrdinalIgnoreCase);

            var actual = File.ReadAllLines(path)
                .Select(l => (l ?? "").Trim())
                .Where(l => l != "" && !set.Contains(l))
                .ToList();

            File.WriteAllLines(path, actual);
        }
    }
}
