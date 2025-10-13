using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WindowsFormsApp1
{
    internal sealed class RendicionService
    {
        private readonly IRendicionRepository _repo;

        public RendicionService(IRendicionRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public string ObtenerRutaArchivo() => _repo.FilePath;

        public IReadOnlyList<string> ObtenerFleteros() => ComboData.Fleteros;

        public (List<string> retirosPendientes, List<string> entregasPendientes) CargarAsignaciones(string fletero)
        {
            var (retiros, entregas) = LeerAsignacionesDesdeArchivos(fletero);
            if (retiros.Count > 0 || entregas.Count > 0)
                return (retiros, entregas);

            var ids = LeerGuiasDeArchivosBase()
                .Distinct()
                .OrderByDescending(x => x)
                .Take(10)
                .ToList();

            var mitad = ids.Count / 2;
            var retirosAuto = ids.Take(mitad).ToList();
            var entregasAuto = ids.Skip(mitad).ToList();

            return (retirosAuto, entregasAuto);
        }

        private (List<string> retiros, List<string> entregas) LeerAsignacionesDesdeArchivos(string fletero)
        {
            var safe = SanitizeFileName(fletero);
            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
            var fRet = Path.Combine(dir, $"asignaciones_retiro_{safe}.txt");
            var fEnt = Path.Combine(dir, $"asignaciones_entrega_{safe}.txt");

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

        private string DataDir => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");

        private static string SanitizeFileName(string s)
        {
            var invalid = Path.GetInvalidFileNameChars();
            return new string((s ?? "").Select(ch => invalid.Contains(ch) ? '_' : ch).ToArray());
        }

        public bool HayArchivosAsignacion(string fletero)
        {
            var safe = SanitizeFileName(fletero ?? "");
            var fRet = Path.Combine(DataDir, $"asignaciones_retiro_{safe}.txt");
            var fEnt = Path.Combine(DataDir, $"asignaciones_entrega_{safe}.txt");

            bool vacio(string p) => !File.Exists(p) || new FileInfo(p).Length == 0;

            return !(vacio(fRet) && vacio(fEnt));
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

        private void ActualizarAsignacionesTrasRendicion(string fletero, IEnumerable<string> guiasRetiros, IEnumerable<string> guiasEntregas)
        {
            var safe = SanitizeFileName(fletero);
            var fRet = Path.Combine(DataDir, $"asignaciones_retiro_{safe}.txt");
            var fEnt = Path.Combine(DataDir, $"asignaciones_entrega_{safe}.txt");

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

        private IEnumerable<string> LeerGuiasDeArchivosBase()
        {
            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
            var archivos = new[]
            {
                Path.Combine(dir, "imposiciones_callcenter.txt"),
                Path.Combine(dir, "admision_cd.txt")
            }.Where(File.Exists);

            foreach (var f in archivos)
            {
                foreach (var (linea, idx) in File.ReadLines(f).Select((l, i) => (l, i)))
                {
                    if (idx == 0) continue;
                    if (string.IsNullOrWhiteSpace(linea)) continue;
                    var campos = linea.Split(';');
                    if (campos.Length == 0) continue;
                    var id = campos[0].Trim().Trim('"');
                    if (!string.IsNullOrWhiteSpace(id))
                        yield return id;
                }
            }
        }

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
