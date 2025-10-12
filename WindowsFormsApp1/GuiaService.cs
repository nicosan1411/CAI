// GuiaService.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApp1
{
    internal interface IGuiaRepository
    {
        IEnumerable<string> ListarRecepcionesPorPatente(string patente);
        IEnumerable<string> ListarDespachosPorPatente(string patente);
        void MarcarRecepcionadasEnCD(IEnumerable<string> nroGuias);
    }

    internal sealed class GuiaService
    {
        private readonly IGuiaRepository _repo;

        public GuiaService(IGuiaRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public (IReadOnlyList<string> recepciones, IReadOnlyList<string> despachos) ObtenerPorPatente(string patente)
        {
            if (!Validaciones.Requerido(patente, "la patente del micro", out var e))
                return (Array.Empty<string>(), Array.Empty<string>());

            var rec = _repo.ListarRecepcionesPorPatente(patente)?.ToList() ?? new List<string>();
            var des = _repo.ListarDespachosPorPatente(patente)?.ToList() ?? new List<string>();
            return (rec, des);
        }

        public (bool ok, string mensaje) GuardarRecepciones(IEnumerable<string> guiasRecepcionadas)
        {
            var list = (guiasRecepcionadas ?? Enumerable.Empty<string>()).Where(g => !string.IsNullOrWhiteSpace(g)).ToList();
            // Podés agregar validaciones de negocio acá (duplicados, estados, etc.)
            _repo.MarcarRecepcionadasEnCD(list);
            return (true, "Datos Guardados");
        }
    }
}
