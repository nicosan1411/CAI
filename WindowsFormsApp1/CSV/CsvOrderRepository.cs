using System;
using System.IO;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Repositorio que "apendea" líneas de imposición a /data/guias_master.txt.
    /// Responsable de generar el Id secuencial (N° de guía).
    /// </summary>
    internal interface IOrderRepository
    {
        /// <summary>
        /// Agrega una o más líneas (1 por encomienda) al archivo.
        /// Devuelve el Id de pedido generado (secuencia).
        /// </summary>
        string AppendLines(PedidoHeader header, Encomienda[] encomiendas, string estado = "Impuesta");

        /// <summary>Ruta absoluta del archivo guias_master.txt</summary>
        string FilePath { get; }
    }

    /// <summary>
    /// Implementación CSV del repositorio de imposiciones.
    /// Escribe el mismo layout que lee/actualiza CsvGuiaMaster.
    /// </summary>
    internal sealed class CsvOrderRepository : IOrderRepository
    {
        private readonly string _dir;
        private readonly string _file;

        public string FilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _dir, _file);

        /// <param 'name'="dataDir">Carpeta (por defecto, "data").</param>
        /// <param 'name'="fileName">Nombre de archivo (por defecto, "guias_master.txt").</param>
        public CsvOrderRepository(string dataDir = "data", string fileName = "guias_master.txt")
        {
            _dir = dataDir;
            _file = fileName;
        }

        /// <inheritdoc />
        public string AppendLines(PedidoHeader header, Encomienda[] encomiendas, string estado = "Impuesta")
        {
            EnsureFileWithHeader();

            // Id secuencial (lee el máximo actual en la 1ra columna y suma 1)
            var idPedido = GetNextSequentialId().ToString();
            var now = DateTime.Now;

            var sb = new StringBuilder();
            foreach (var e in encomiendas ?? Array.Empty<Encomienda>())
            {
                var rec = new ImposicionRecord
                {
                    Id = idPedido,
                    FechaHora = now,
                    EmpresaCliente = header.EmpresaCliente,
                    RetiroTipo = header.RetiroTipo,
                    AgenciaRetiro = header.AgenciaRetiro,
                    EnvioTipo = header.EnvioTipo,
                    ProvinciaEnvio = header.ProvinciaEnvio,
                    AgenciaEnvio = header.AgenciaEnvio,
                    DniDestinatario = header.DniDestinatario,
                    LocalidadDestinatario = header.LocalidadDestinatario,
                    DomicilioDestinatario = header.DomicilioDestinatario,
                    Dimension = e.Dimension,
                    Cantidad = e.Cantidad,
                    Estado = estado
                };
                sb.AppendLine(ToCsv(rec));
            }

            File.AppendAllText(FilePath, sb.ToString(), Encoding.UTF8);
            return idPedido;
        }

        /// <summary>
        /// Lee el archivo y devuelve max(Id) + 1; si no existe, arranca en 1.
        /// </summary>
        private int GetNextSequentialId()
        {
            if (!File.Exists(FilePath))
                return 1;

            int max = 0;

            // Saltamos header. Tomamos la primera columna; viene entre comillas.
            foreach (var line in File.ReadLines(FilePath).Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var firstField = line.Split(';')[0].Trim().Trim('"');
                if (int.TryParse(firstField, out var id) && id > max)
                    max = id;
            }

            return max + 1;
        }

        /// <summary>Crea archivo/carpeta con encabezado si no existe.</summary>
        private void EnsureFileWithHeader()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            if (!File.Exists(FilePath) || new FileInfo(FilePath).Length == 0)
            {
                var header = string.Join(";",
                    "Id", "FechaHora", "EmpresaCliente",
                    "RetiroTipo", "AgenciaRetiro",
                    "EnvioTipo", "ProvinciaEnvio", "AgenciaEnvio",
                    "DniDestinatario", "LocalidadDestinatario", "DomicilioDestinatario",
                    "Dimension", "Cantidad", "Estado"
                );
                File.AppendAllText(FilePath, header + Environment.NewLine, Encoding.UTF8);
            }
        }

        /// <summary>Escapa un campo CSV (comillas → dobles comillas) y lo envuelve en comillas.</summary>
        private static string CsvEscape(string s)
        {
            if (s == null) return "\"\"";
            var escaped = s.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }

        /// <summary>Serializa el registro al layout CSV compartido.</summary>
        private static string ToCsv(ImposicionRecord r)
        {
            return string.Join(";",
                CsvEscape(r.Id),
                CsvEscape(r.FechaHora.ToString("yyyy-MM-dd HH:mm:ss")),
                CsvEscape(r.EmpresaCliente),
                CsvEscape(r.RetiroTipo),
                CsvEscape(r.AgenciaRetiro ?? ""),
                CsvEscape(r.EnvioTipo),
                CsvEscape(r.ProvinciaEnvio),
                CsvEscape(r.AgenciaEnvio ?? ""),
                CsvEscape(r.DniDestinatario),
                CsvEscape(r.LocalidadDestinatario),
                CsvEscape(r.DomicilioDestinatario),
                CsvEscape(r.Dimension),
                CsvEscape(r.Cantidad.ToString()),
                CsvEscape(r.Estado ?? "Impuesta")
            );
        }
    }
}
