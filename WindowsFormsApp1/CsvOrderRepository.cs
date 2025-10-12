using System;
using System.IO;
using System.Text;

namespace WindowsFormsApp1
{
    internal interface IOrderRepository
    {
        string AppendLines(PedidoHeader header, Encomienda[] encomiendas, string estado = "Impuesta");
        string FilePath { get; }
    }

    internal sealed class CsvOrderRepository : IOrderRepository
    {
        private readonly string _dir;
        private readonly string _file;

        public string FilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _dir, _file);

        public CsvOrderRepository(string dataDir = "data", string fileName = "imposiciones_callcenter.txt")
        {
            _dir = dataDir;
            _file = fileName;
        }

        public string AppendLines(PedidoHeader header, Encomienda[] encomiendas, string estado = "Impuesta")
        {
            EnsureFileWithHeader();

            // Nro de guía / Id secuencial
            var idPedido = GetNextSequentialId().ToString();
            var now = DateTime.Now;

            var sb = new StringBuilder();
            foreach (var e in encomiendas)
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
                    Estado = estado // NUEVO
                };
                sb.AppendLine(ToCsv(rec));
            }

            File.AppendAllText(FilePath, sb.ToString(), Encoding.UTF8);
            return idPedido;
        }


        private int GetNextSequentialId()
        {
            var counterFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _dir, "pedido_counter.txt");
            Directory.CreateDirectory(Path.GetDirectoryName(counterFile));

            int last = 0;
            if (File.Exists(counterFile))
            {
                var txt = File.ReadAllText(counterFile);
                int.TryParse(txt, out last);
            }

            int next = last + 1;
            File.WriteAllText(counterFile, next.ToString());
            return next;
        }


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

        private static string CsvEscape(string s)
        {
            if (s == null) return "\"\"";
            var escaped = s.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }

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
