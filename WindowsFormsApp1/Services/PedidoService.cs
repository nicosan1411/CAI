using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Lógica de imposición: valida header, maneja ítems y persiste al repositorio CSV.
    /// </summary>
    internal sealed class PedidoService
    {
        private readonly IOrderRepository _repo;
        private readonly List<Encomienda> _encomiendas = new List<Encomienda>();

        public PedidoService(IOrderRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public IReadOnlyList<Encomienda> Encomiendas => _encomiendas;

        /// <summary>Valida el encabezado del pedido según reglas del CU.</summary>
        public (bool ok, string message) ValidarHeader(PedidoHeader h)
        {
            var errors = new List<string>();

            // Empresa
            if (!Validaciones.Requerido(h.EmpresaCliente, "una Empresa/Cliente", out var eEmp)) errors.Add(eEmp);

            // Retiro
            if (!Validaciones.Requerido(h.RetiroTipo, "el tipo de retiro", out var eRet)) errors.Add(eRet);
            if (h.RetiroTipo == "Retiro en agencia" && !Validaciones.Requerido(h.AgenciaRetiro, "la agencia de retiro", out var eAR)) errors.Add(eAR);

            // Envío
            if (!Validaciones.Requerido(h.EnvioTipo, "el tipo de envío", out var eEnv)) errors.Add(eEnv);
            if (!Validaciones.Requerido(h.ProvinciaEnvio, "la provincia de envío", out var eProv)) errors.Add(eProv);
            if (h.EnvioTipo == "Envío a agencia" && !Validaciones.Requerido(h.AgenciaEnvio, "la agencia de envío", out var eAE)) errors.Add(eAE);

            // DNI
            if (!Validaciones.EsDniValido(h.DniDestinatario, out var eDni)) errors.Add(eDni);

            // Dirección si corresponde
            var requiereDir = h.EnvioTipo == "Envío a domicilio";
            if (requiereDir)
            {
                if (!Validaciones.Requerido(h.LocalidadDestinatario, "la localidad del destinatario", out var eLoc)) errors.Add(eLoc);
                if (!Validaciones.Requerido(h.DomicilioDestinatario, "el domicilio del destinatario", out var eDom)) errors.Add(eDom);
            }

            return errors.Any()
                ? (false, string.Join(Environment.NewLine, errors))
                : (true, "OK");
        }

        /// <summary>Agrega una encomienda (valida dimensión y cantidad &gt;= 1).</summary>
        public (bool ok, string message) AgregarEncomienda(Encomienda e)
        {
            if (!Validaciones.Requerido(e?.Dimension, "la dimensión de la encomienda", out var errDim))
                return (false, errDim);
            if (e.Cantidad < 1)
                return (false, "La cantidad debe ser al menos 1.");

            _encomiendas.Add(e);
            return (true, "OK");
        }

        /// <summary>Quita la encomienda en el índice indicado.</summary>
        public (bool ok, string message) QuitarEncomiendaEn(int index)
        {
            if (index < 0 || index >= _encomiendas.Count) return (false, "Índice fuera de rango.");
            _encomiendas.RemoveAt(index);
            return (true, "OK");
        }

        /// <summary>
        /// Valida y persiste el pedido en el repositorio. 
        /// Genera el Id secuencial (lo devuelve) y limpia la lista de encomiendas.
        /// </summary>
        public (bool ok, string message, string guiaId) GuardarPedido(PedidoHeader header)
        {
            if (_encomiendas.Count == 0)
                return (false, "Debe agregar al menos una encomienda antes de aceptar el pedido.", null);

            var (ok, msg) = ValidarHeader(header);
            if (!ok) return (ok, msg, null);

            var guiaId = _repo.AppendLines(header, _encomiendas.ToArray(), "Impuesta");
            _encomiendas.Clear();
            return (true, "Pedido aceptado y guardado con todas las encomiendas.", guiaId);
        }

        /// <summary>Devuelve la ruta física del archivo (útil para mostrar/abrir).</summary>
        public string ObtenerRutaArchivo() => _repo.FilePath;
    }
}
