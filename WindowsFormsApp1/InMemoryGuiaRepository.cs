using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApp1
{
    internal sealed class InMemoryGuiaRepository : IGuiaRepository
    {
        private readonly Dictionary<string, List<string>> _recepcionesPorMicro;
        private readonly Dictionary<string, List<string>> _despachosPorMicro;

        public InMemoryGuiaRepository()
        {
            _recepcionesPorMicro = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                ["AA123BB"] = new List<string> { "10001", "10002", "10003" },
                ["AC456DD"] = new List<string> { "20010", "20011" },
                ["AE789FF"] = new List<string> { "30020" }
            };

            _despachosPorMicro = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                ["AA123BB"] = new List<string> { "D-50001", "D-50002" },
                ["AC456DD"] = new List<string> { "D-60010" },
                ["AE789FF"] = new List<string> { "D-70020", "D-70021", "D-70022" }
            };
        }

        public IEnumerable<string> ListarRecepcionesPorPatente(string patente)
            => _recepcionesPorMicro.TryGetValue(patente ?? "", out var list) ? list : Enumerable.Empty<string>();

        public IEnumerable<string> ListarDespachosPorPatente(string patente)
            => _despachosPorMicro.TryGetValue(patente ?? "", out var list) ? list : Enumerable.Empty<string>();

        public void MarcarRecepcionadasEnCD(IEnumerable<string> nroGuias)
        {
            // DO NOTHING
        }
    }
}
