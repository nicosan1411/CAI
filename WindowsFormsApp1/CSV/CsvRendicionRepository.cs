using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Repositorio CSV para rendiciones de primera milla.
    /// Escribe /data/rendiciones_primera_milla.txt con layout propio.
    /// </summary>
    internal interface IRendicionRepository
    {
        /// <summary>Ruta absoluta del archivo de rendiciones.</summary>
        string FilePath { get; }

        /// <summary>Agrega líneas de rendición (append). No borra/reescribe lo previo.</summary>
        void Append(IEnumerable<RendicionLinea> lineas);
    }

    /// <summary>
    /// Implementación CSV de rendición. Cada línea representa un resultado por guía.
    /// </summary>
    internal sealed class CsvRendicionRepository : IRendicionRepository
    {
        private readonly string _dir;
        private readonly string _file;

        public string FilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _dir, _file);

        /// <param 'name'="dataDir">Carpeta (por defecto, "data").</param>
        /// <param 'name'="fileName">Nombre de archivo (por defecto, "rendiciones_primera_milla.txt").</param>
        public CsvRendicionRepository(string dataDir = "data", string fileName = "rendiciones_primera_milla.txt")
        {
            _dir = dataDir;
            _file = fileName;
        }

        /// <inheritdoc />
        public void Append(IEnumerable<RendicionLinea> lineas)
        {
            EnsureFileWithHeader();

            var sb = new StringBuilder();
            foreach (var l in lineas ?? Enumerable.Empty<RendicionLinea>())
            {
                // Tipo: "Retiro" | "Entrega"
                // Resultado: "Admitida en CD Origen" | "Entregada a cliente"
                sb.AppendLine(string.Join(";",
                    Csv(l.FechaHora.ToString("yyyy-MM-dd HH:mm:ss")),
                    Csv(l.Fletero),
                    Csv(l.Guia),
                    Csv(l.Tipo),
                    Csv(l.Resultado)
                ));
            }

            File.AppendAllText(FilePath, sb.ToString(), Encoding.UTF8);
        }

        /// <summary>Crea archivo con cabecera si no existe.</summary>
        private void EnsureFileWithHeader()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            if (!File.Exists(FilePath) || new FileInfo(FilePath).Length == 0)
            {
                var header = string.Join(";", "FechaHora", "Fletero", "Guia", "Tipo", "Resultado");
                File.AppendAllText(FilePath, header + Environment.NewLine, Encoding.UTF8);
            }
        }

        /// <summary>Escapa comillas y envuelve en comillas dobles.</summary>
        private static string Csv(string s)
        {
            if (s == null) return "\"\"";
            var escaped = s.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
    }

    /// <summary>DTO simple para una línea de rendición.</summary>
    internal sealed class RendicionLinea
    {
        public DateTime FechaHora { get; set; }
        public string Fletero { get; set; }
        public string Guia { get; set; }
        public string Tipo { get; set; }      // "Retiro" | "Entrega"
        public string Resultado { get; set; } // "Admitida en CD Origen" | "Entregada a cliente"
    }
}
