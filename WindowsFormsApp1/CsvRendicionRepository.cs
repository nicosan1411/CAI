using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    internal interface IRendicionRepository
    {
        string FilePath { get; }
        void Append(IEnumerable<RendicionLinea> lineas);
    }

    internal sealed class CsvRendicionRepository : IRendicionRepository
    {
        private readonly string _dir;
        private readonly string _file;

        public string FilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _dir, _file);

        public CsvRendicionRepository(string dataDir = "data", string fileName = "rendiciones_primera_milla.txt")
        {
            _dir = dataDir;
            _file = fileName;
        }

        public void Append(IEnumerable<RendicionLinea> lineas)
        {
            EnsureFileWithHeader();

            var sb = new StringBuilder();
            foreach (var l in lineas ?? Enumerable.Empty<RendicionLinea>())
            {
                sb.AppendLine(string.Join(";",
                    Csv(l.FechaHora.ToString("yyyy-MM-dd HH:mm:ss")),
                    Csv(l.Fletero),
                    Csv(l.Guia),
                    Csv(l.Tipo),        // "Retiro" | "Entrega"
                    Csv(l.Resultado)    // "Admitida en CD Origen" | "Entregada a cliente"
                ));
            }

            File.AppendAllText(FilePath, sb.ToString(), Encoding.UTF8);
        }

        private void EnsureFileWithHeader()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            if (!File.Exists(FilePath) || new FileInfo(FilePath).Length == 0)
            {
                var header = string.Join(";", "FechaHora", "Fletero", "Guia", "Tipo", "Resultado");
                File.AppendAllText(FilePath, header + Environment.NewLine, Encoding.UTF8);
            }
        }

        private static string Csv(string s)
        {
            if (s == null) return "\"\"";
            var escaped = s.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
    }

    internal sealed class RendicionLinea
    {
        public DateTime FechaHora { get; set; }
        public string Fletero { get; set; }
        public string Guia { get; set; }
        public string Tipo { get; set; }      // "Retiro" | "Entrega"
        public string Resultado { get; set; } // "Admitida en CD Origen" | "Entregada a cliente"
    }
}
