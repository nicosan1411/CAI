using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Genera archivos de asignación por fletero (retiros/entregas) a partir del maestro.
    /// Crea/actualiza, por cada fletero:
    ///   data/asignaciones/{slugFletero}_retiros.txt
    ///   data/asignaciones/{slugFletero}_entregas.txt
    ///
    /// Lógica:
    /// - Retiros candidatos = guías en estado "Impuesta".
    /// - Entregas candidatas = guías en estado "Admitida en CD destino".
    /// - Distribución aleatoria entre fleteros recibidos.
    /// - Al asignar, se actualiza el estado en el master:
    ///     * Retiros -> "En proceso de retiro"
    ///     * Entregas -> "En proceso de entrega"
    /// </summary>
    public sealed class AssignmentService
    {
        private readonly string _dataDir;

        public AssignmentService(string dataDir = "data")
        {
            _dataDir = dataDir;
        }

        /// <summary>Directorio absoluto para asignaciones.</summary>
        private string AsignDir =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _dataDir, "asignaciones");

        private static string FileRetiros(string dir, string slug) =>
            Path.Combine(dir, slug + "_retiros.txt");

        private static string FileEntregas(string dir, string slug) =>
            Path.Combine(dir, slug + "_entregas.txt");

        private static string Slug(string nombre)
        {
            var invalid = Path.GetInvalidFileNameChars();
            var s = (nombre ?? "").Trim();
            return new string(s.Select(ch => invalid.Contains(ch) ? '_' : ch).ToArray());
        }

        /// <summary>
        /// Lee el maestro por estados, reparte guías y mueve estados al asignar.
        /// Devuelve la cantidad total asignada de cada tipo.
        /// </summary>
        public (int retiros, int entregas) GenerarAsignacionesAleatorias(IEnumerable<string> fleteros)
        {
            // Normalizo fleteros a lista utilizable
            var listaFleteros = (fleteros ?? Enumerable.Empty<string>())
                .Select(f => (f ?? "").Trim())
                .Where(f => f.Length > 0)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (listaFleteros.Count == 0)
                return (0, 0);

            // Aseguro carpeta
            Directory.CreateDirectory(AsignDir);

            // Limpio archivos previos por fletero (dejo en blanco para que sea "estado fresco")
            foreach (var f in listaFleteros)
            {
                var slug = Slug(f);
                File.WriteAllText(FileRetiros(AsignDir, slug), string.Empty);
                File.WriteAllText(FileEntregas(AsignDir, slug), string.Empty);
            }

            var master = new CsvGuiaMaster();

            // Candidatos por estado (alineado a CU y equipo)
            var candidatosRetiros = master.ListarPorEstado(GuiaEstados.Impuesta)?.ToList() ?? new List<string>();
            var candidatosEntregas = master.ListarPorEstado(GuiaEstados.AdmitidaDestino)?.ToList() ?? new List<string>();

            if (candidatosRetiros.Count == 0 && candidatosEntregas.Count == 0)
                return (0, 0);

            var rnd = new Random();

            // Para luego mover estado en batch
            var asignadasRetiros = new List<string>();
            var asignadasEntregas = new List<string>();

            // Reparto retiros
            foreach (var guia in candidatosRetiros)
            {
                var fletero = listaFleteros[rnd.Next(listaFleteros.Count)];
                var slug = Slug(fletero);
                File.AppendAllLines(FileRetiros(AsignDir, slug), new[] { guia });
                asignadasRetiros.Add(guia);
            }

            // Reparto entregas
            foreach (var guia in candidatosEntregas)
            {
                var fletero = listaFleteros[rnd.Next(listaFleteros.Count)];
                var slug = Slug(fletero);
                File.AppendAllLines(FileEntregas(AsignDir, slug), new[] { guia });
                asignadasEntregas.Add(guia);
            }

            // 🔄 Mover estados en el master para reflejar que ya están "en trabajo"
            if (asignadasRetiros.Count > 0)
                master.CambiarEstado(asignadasRetiros, GuiaEstados.EnProcesoRetiro);

            if (asignadasEntregas.Count > 0)
                master.CambiarEstado(asignadasEntregas, GuiaEstados.EnProcesoEntrega);

            return (asignadasRetiros.Count, asignadasEntregas.Count);
        }
    }
}
