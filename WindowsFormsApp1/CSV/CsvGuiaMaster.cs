using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Repositorio de "guías master" basado en CSV.
    /// Lee y actualiza estados en data/guias_master.txt.
    /// </summary>
    internal interface IGuiaMaster
    {
        /// <summary>
        /// Lista IDs de guías cuyo estado actual coincide (case-insensitive).
        /// </summary>
        IEnumerable<string> ListarPorEstado(string estado);

        /// <summary>
        /// Cambia el estado de las guías indicadas. Escribe el archivo completo.
        /// </summary>
        void CambiarEstado(IEnumerable<string> guias, string nuevoEstado);
    }

    /// <summary>
    /// Implementación CSV de IGuiaMaster. Comparte estructura con CsvOrderRepository.
    /// </summary>
    internal sealed class CsvGuiaMaster : IGuiaMaster
    {
        private readonly string _path;

        /// Si es null, usa /data/guias_master.txt</param>
        public CsvGuiaMaster(string path = null)
        {
            _path = path ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "guias_master.txt");
            EnsureHeader(); // crea carpeta/archivo con encabezado si no existe
        }

        /// <inheritdoc />
        public IEnumerable<string> ListarPorEstado(string estado)
        {
            if (!File.Exists(_path) || string.IsNullOrWhiteSpace(estado))
                yield break;

            // Salteamos línea 0 (header). Formato esperado: 14 columnas (col[13] = Estado)
            foreach (var line in File.ReadLines(_path).Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var cols = line.Split(';').Select(s => s.Trim().Trim('"')).ToArray();
                if (cols.Length < 14) continue;

                // cols[13] es Estado, cols[0] es Id
                if (string.Equals(cols[13] ?? "", estado, StringComparison.OrdinalIgnoreCase))
                    yield return cols[0];
            }
        }

        /// <inheritdoc />
        public void CambiarEstado(IEnumerable<string> guias, string nuevoEstado)
        {
            // Normalizamos ids a un set (trim y sin vacíos) para match rápido
            var set = new HashSet<string>((guias ?? Enumerable.Empty<string>())
                .Select(g => (g ?? "").Trim())
                .Where(g => g != ""));

            if (set.Count == 0 || !File.Exists(_path))
                return;

            var lines = File.ReadAllLines(_path).ToList(); // leemos todo (pequeño, simple)
            for (int i = 1; i < lines.Count; i++) // i=1 para saltear header
            {
                var cols = lines[i].Split(';').ToArray();
                if (cols.Length < 14) continue;

                var id = cols[0].Trim().Trim('"');
                if (!set.Contains(id)) continue;

                // col[13] = Estado
                cols[13] = Quote(nuevoEstado);
                lines[i] = string.Join(";", cols);
            }

            // Escritura atómica simple: si querés, se puede hacer con .tmp + Replace
            File.WriteAllLines(_path, lines);
        }

        /// <summary>Crea directorio y archivo con header si no existen.</summary>
        private void EnsureHeader()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_path));
            if (!File.Exists(_path) || new FileInfo(_path).Length == 0)
            {
                var header = string.Join(";",
                    "Id", "FechaHora", "EmpresaCliente",
                    "RetiroTipo", "AgenciaRetiro",
                    "EnvioTipo", "ProvinciaEnvio", "AgenciaEnvio",
                    "DniDestinatario", "LocalidadDestinatario", "DomicilioDestinatario",
                    "Dimension", "Cantidad", "Estado"
                );
                File.WriteAllText(_path, header + Environment.NewLine);
            }
        }

        /// <summary>Escapa comillas y envuelve en comillas dobles para CSV.</summary>
        private static string Quote(string s) => $"\"{(s ?? "").Replace("\"", "\"\"")}\"";
    }
}
