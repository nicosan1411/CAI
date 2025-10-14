using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Repositorio de "guías master" basado en CSV (13 columnas, sin 'Cantidad').
    /// Lee y actualiza estados en data/guias_master.txt.
    /// </summary>
    internal interface IGuiaMaster
    {
        IEnumerable<string> ListarPorEstado(string estado);
        void CambiarEstado(IEnumerable<string> guias, string nuevoEstado);
    }

    internal sealed class CsvGuiaMaster : IGuiaMaster
    {
        private readonly string _path;

        /// <summary>Si es null, usa /data/guias_master.txt</summary>
        public CsvGuiaMaster(string path = null)
        {
            _path = path ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "guias_master.txt");
            EnsureHeader();
        }

        public IEnumerable<string> ListarPorEstado(string estado)
        {
            if (!File.Exists(_path) || string.IsNullOrWhiteSpace(estado))
                yield break;

            foreach (var raw in File.ReadLines(_path).Skip(1))
            {
                if (string.IsNullOrWhiteSpace(raw)) continue;

                var cols = raw.Split(';');
                if (cols.Length < 13) continue; // 13 columnas exactas

                var id = (cols[0] ?? "").Replace("\"", "").Trim();
                var estadoActual = (cols[12] ?? "").Replace("\"", "").Trim(); // idx 12 = Estado

                if (id == "") continue;

                if (string.Equals(estadoActual, estado, StringComparison.OrdinalIgnoreCase))
                    yield return id;
            }
        }

        public void CambiarEstado(IEnumerable<string> guias, string nuevoEstado)
        {
            var set = new HashSet<string>((guias ?? Enumerable.Empty<string>())
                .Select(g => (g ?? "").Replace("\"", "").Trim())
                .Where(g => g != ""));

            if (set.Count == 0 || !File.Exists(_path))
                return;

            var lines = File.ReadAllLines(_path).ToList();
            for (int i = 1; i < lines.Count; i++) // i=1 para saltear header
            {
                var cols = lines[i].Split(';').ToArray();
                if (cols.Length < 13) continue;

                var id = (cols[0] ?? "").Replace("\"", "").Trim();
                if (!set.Contains(id)) continue;

                // col[12] = Estado (guardamos entrecomillado)
                cols[12] = Quote(nuevoEstado);
                lines[i] = string.Join(";", cols);
            }

            File.WriteAllLines(_path, lines);
        }

        private void EnsureHeader()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_path));
            if (!File.Exists(_path) || new FileInfo(_path).Length == 0)
            {
                // Header sin "Cantidad"
                var header = string.Join(";",
                    "Id", "FechaHora", "EmpresaCliente",
                    "RetiroTipo", "AgenciaRetiro",
                    "EnvioTipo", "ProvinciaEnvio", "AgenciaEnvio",
                    "DniDestinatario", "LocalidadDestinatario", "DomicilioDestinatario",
                    "Dimension", "Estado"
                );
                File.WriteAllText(_path, header + Environment.NewLine);
            }
        }

        private static string Quote(string s) => $"\"{(s ?? "").Replace("\"", "\"\"")}\"";
    }
}

