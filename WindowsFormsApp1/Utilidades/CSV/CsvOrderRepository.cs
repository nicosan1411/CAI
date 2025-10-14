using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Contrato del repositorio que persiste pedidos/guías en el maestro CSV.
    /// </summary>
    internal interface IOrderRepository
    {
        /// <summary>Ruta absoluta del archivo CSV destino (guias_master.txt).</summary>
        string FilePath { get; }

        /// <summary>
        /// Persiste el pedido expandiendo **una línea por unidad** (sin columna Cantidad).
        /// Retorna el **primer Id** generado (compatibilidad con UI).
        /// </summary>
        string AppendLines(PedidoHeader header, Encomienda[] items, string estadoInicial);
    }

    /// <summary>
    /// Implementación CSV: escribe en data/guias_master.txt con **13** columnas (sin Cantidad).
    /// Genera Ids crecientes desde 1 usando un archivo contador (data/guia_counter.txt).
    /// </summary>
    internal sealed class CsvOrderRepository : IOrderRepository
    {
        private readonly string _dir;
        private readonly string _file;
        private string MasterPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _dir, _file);
        private string CounterPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _dir, "guia_counter.txt");

        public string FilePath => MasterPath;

        public CsvOrderRepository(string dataDir = "data", string fileName = "guias_master.txt")
        {
            _dir = string.IsNullOrWhiteSpace(dataDir) ? "data" : dataDir;
            _file = string.IsNullOrWhiteSpace(fileName) ? "guias_master.txt" : fileName;
            EnsureMasterWithHeader();
        }

        /// <summary>
        /// Guarda el pedido **una guía por unidad**.
        /// - Si una encomienda trae Cantidad=3, se crean 3 filas independientes (cada una es una guía).
        /// - SIN columna "Cantidad" en el archivo.
        /// </summary>
        public string AppendLines(PedidoHeader header, Encomienda[] items, string estadoInicial)
        {
            if (header == null) throw new ArgumentNullException(nameof(header));
            var listado = new List<string>();
            var now = DateTime.Now;

            string q(string s) => $"\"{(s ?? "").Replace("\"", "\"\"")}\"";
            string estado = estadoInicial ?? GuiaEstados.Impuesta;

            string firstId = null;
            foreach (var e in (items ?? Array.Empty<Encomienda>()))
            {
                var dimension = e?.Dimension ?? "";
                var unidades = Math.Max(1, e?.Cantidad ?? 0);

                for (int u = 0; u < unidades; u++)
                {
                    var id = NextId().ToString();
                    if (firstId == null) firstId = id;

                    // 13 columnas: (Id; FechaHora; EmpresaCliente; RetiroTipo; AgenciaRetiro; EnvioTipo; ProvinciaEnvio; 
                    // AgenciaEnvio; DniDestinatario; LocalidadDestinatario; DomicilioDestinatario; Dimension; Estado)
                    var line = string.Join(";",
                        q(id),
                        q(now.ToString("yyyy-MM-dd HH:mm:ss")),
                        q(header.EmpresaCliente),
                        q(header.RetiroTipo),
                        q(header.AgenciaRetiro),
                        q(header.EnvioTipo),
                        q(header.ProvinciaEnvio),
                        q(header.AgenciaEnvio),
                        q(Validaciones.SoloDigitos(header.DniDestinatario)),
                        q(header.LocalidadDestinatario),
                        q(header.DomicilioDestinatario),
                        q(dimension),
                        q(estado)
                    );

                    listado.Add(line);
                }
            }

            if (listado.Count == 0)
                throw new InvalidOperationException("No hay unidades a persistir.");

            File.AppendAllText(MasterPath, string.Join(Environment.NewLine, listado) + Environment.NewLine, Encoding.UTF8);
            return firstId ?? "0";
        }

        // =======================
        // Helpers de persistencia
        // =======================

        private void EnsureMasterWithHeader()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(MasterPath));
            if (!File.Exists(MasterPath) || new FileInfo(MasterPath).Length == 0)
            {
                // Header sin "Cantidad"
                var header = string.Join(";",
                    "Id", "FechaHora", "EmpresaCliente",
                    "RetiroTipo", "AgenciaRetiro",
                    "EnvioTipo", "ProvinciaEnvio", "AgenciaEnvio",
                    "DniDestinatario", "LocalidadDestinatario", "DomicilioDestinatario",
                    "Dimension", "Estado"
                );
                File.WriteAllText(MasterPath, header + Environment.NewLine, Encoding.UTF8);
            }

            // Si no existe el contador, se inicializa en 0 (el próximo será 1)
            if (!File.Exists(CounterPath))
                File.WriteAllText(CounterPath, "0", Encoding.UTF8);
        }

        /// <summary>Obtiene el próximo Id correlativo (persistente) desde data/guia_counter.txt.</summary>
        private long NextId()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(CounterPath));

            long current = 0;
            try
            {
                var raw = File.ReadAllText(CounterPath).Trim();
                long.TryParse(raw, out current);
            }
            catch
            {
                current = 0;
            }

            var next = current + 1;
            File.WriteAllText(CounterPath, next.ToString(), Encoding.UTF8);
            return next;
        }
    }
}
