using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Contrato del repositorio de guías (recepciones/ despachos).
    /// </summary>
    internal interface IGuiaRepository
    {
        IEnumerable<string> ListarRecepcionesPorPatente(string patente);
        IEnumerable<string> ListarDespachosPorPatente(string patente);
        void MarcarRecepcionadasEnCD(IEnumerable<string> nroGuias);
    }

    /// <summary>
    /// Servicio de consultas/actualizaciones de guías por patente (micro).
    /// </summary>
    internal sealed class GuiaService
    {
        private readonly IGuiaRepository _repo;

        public GuiaService(IGuiaRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        /// <summary>
        /// Devuelve recepciones y despachos pendientes para la patente indicada.
        /// Si la patente está vacía, devuelve listas vacías.
        /// </summary>
        public (IReadOnlyList<string> recepciones, IReadOnlyList<string> despachos) ObtenerPorPatente(string patente)
        {
            if (!Validaciones.Requerido(patente, "la patente del micro", out var _))
                return (Array.Empty<string>(), Array.Empty<string>());

            var rec = _repo.ListarRecepcionesPorPatente(patente)?.ToList() ?? new List<string>();
            var des = _repo.ListarDespachosPorPatente(patente)?.ToList() ?? new List<string>();
            return (rec, des);
        }

        /// <summary>
        /// Marca guías seleccionadas como recepcionadas en CD.
        /// NOTA: en InMemoryGuiaRepository es NO-OP; en CSV debería actualizar "Estado".
        /// </summary>
        public (bool ok, string mensaje) GuardarRecepciones(IEnumerable<string> guiasRecepcionadas)
        {
            var list = (guiasRecepcionadas ?? Enumerable.Empty<string>()).Where(g => !string.IsNullOrWhiteSpace(g)).ToList();
            // Validaciones extra (duplicados, estado compatible, etc.) podrían ir aquí
            _repo.MarcarRecepcionadasEnCD(list);
            return (true, "Datos Guardados");
        }
    }
}
