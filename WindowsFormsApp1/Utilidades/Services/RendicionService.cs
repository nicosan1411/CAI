using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WindowsFormsApp1
{
    internal sealed class RendicionService
    {
        private readonly IRendicionRepository _repo;

        public bool HabilitarLog { get; set; } = false;

        public RendicionService(IRendicionRepository repo)
        {
            _repo = repo; // puede ser null
        }

        public IReadOnlyList<string> ObtenerFleteros() { return ComboData.Fleteros; }

        public string ObtenerRutaArchivo()
        {
            var csv = _repo as CsvRendicionRepository;
            if (csv != null) return csv.FilePath;
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "rendiciones_primera_milla.txt");
        }

        private string AsignDir { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "asignaciones"); } }

        private static string SanitizeFileName(string s)
        {
            if (string.IsNullOrEmpty(s)) return "_";
            foreach (char c in Path.GetInvalidFileNameChars()) s = s.Replace(c, '_');
            return s.Trim();
        }

        private static string FileRetiros(string dir, string fleteroSafe)
        {
            return Path.Combine(dir, fleteroSafe + "_retiros.txt");
        }

        private static List<string> LeerLineasSiExiste(string path)
        {
            if (!File.Exists(path)) return new List<string>();
            return File.ReadAllLines(path).Select(l => (l ?? "").Trim())
                .Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
        }

        private static void EscribirLineas(string path, List<string> lineas)
        {
            File.WriteAllLines(path, lineas ?? new List<string>());
        }

        public bool HayArchivosAsignacion(string fletero)
        {
            string safe = SanitizeFileName(fletero ?? "");
            Directory.CreateDirectory(AsignDir);
            string fRet = FileRetiros(AsignDir, safe);
            bool vacio = !File.Exists(fRet) || new FileInfo(fRet).Length == 0;
            return !vacio;
        }

        public (List<string> retirosPendientes, List<string> entregasPendientes) CargarAsignaciones(string fletero)
        {
            string safe = SanitizeFileName(fletero ?? "");
            Directory.CreateDirectory(AsignDir);
            string fRet = FileRetiros(AsignDir, safe);

            var retiros = LeerLineasSiExiste(fRet);
            return (retiros, new List<string>());
        }

        public void Guardar(string fletero, IEnumerable<string> guiasRetirosAdmitidos, IEnumerable<string> guiasEntregasRealizadas)
        {
            var ahora = DateTime.Now;

            // (1) Log opcional
            if (HabilitarLog && _repo != null)
            {
                var lines = new List<RendicionLinea>();
                foreach (var g in (guiasRetirosAdmitidos ?? Enumerable.Empty<string>()))
                {
                    lines.Add(new RendicionLinea
                    {
                        FechaHora = ahora,
                        Fletero = fletero,
                        Guia = (g ?? "").Trim(),
                        Tipo = "Retiro",
                        Resultado = "Admitida en CD Origen"
                    });
                }
                foreach (var g in (guiasEntregasRealizadas ?? Enumerable.Empty<string>()))
                {
                    lines.Add(new RendicionLinea
                    {
                        FechaHora = ahora,
                        Fletero = fletero,
                        Guia = (g ?? "").Trim(),
                        Tipo = "Entrega",
                        Resultado = "Entregada a cliente"
                    });
                }
                if (lines.Count > 0) _repo.Append(lines);
            }

            // (2) Cambiar estados en master
            var master = new CsvGuiaMaster();

            var retiros = (guiasRetirosAdmitidos ?? Enumerable.Empty<string>())
                .Select(x => (x ?? "").Trim()).Where(x => x != "").ToList();
            if (retiros.Count > 0)
                master.CambiarEstado(retiros, GuiaEstados.AdmitidaOrigen); // Impuesta -> Admitida en CD origen

            var entregas = (guiasEntregasRealizadas ?? Enumerable.Empty<string>())
                .Select(x => (x ?? "").Trim()).Where(x => x != "").ToList();
            if (entregas.Count > 0)
                master.CambiarEstado(entregas, GuiaEstados.EntregadaCliente);

            // (3) Auto-asignar a patentes los retiros que pasaron a "Admitida en CD origen"
            if (retiros.Count > 0)
            {
                try
                {
                    var microAssign = new MicroAssignmentService();
                    microAssign.AsignarDespachosPorIds(retiros, ComboData.Patentes);
                }
                catch { /* no romper el flujo */ }
            }

            // (4) Limpiar del TXT del fletero las guías rendidas (retiros)
            string safe = SanitizeFileName(fletero ?? "");
            Directory.CreateDirectory(AsignDir);
            string fRet = FileRetiros(AsignDir, safe);

            if (retiros.Count > 0 && File.Exists(fRet))
            {
                var actuales = LeerLineasSiExiste(fRet);
                var set = new HashSet<string>(retiros, StringComparer.OrdinalIgnoreCase);
                var post = actuales.Where(g => !set.Contains((g ?? "").Trim())).ToList();
                EscribirLineas(fRet, post);
            }
        }
    }
}
