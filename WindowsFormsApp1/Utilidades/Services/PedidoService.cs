using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Lógica de imposición/admisión: valida header, maneja ítems y persiste al repositorio CSV.
    /// </summary>
    internal sealed class PedidoService
    {
        private readonly IOrderRepository _repo;
        private readonly List<Encomienda> _encomiendas = new List<Encomienda>();

        public PedidoService(IOrderRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public IReadOnlyList<Encomienda> Encomiendas { get { return _encomiendas; } }

        public (bool ok, string message) ValidarHeader(PedidoHeader h)
        {
            var errors = new List<string>();

            string eEmp;
            if (!Validaciones.Requerido(h.EmpresaCliente, "una Empresa/Cliente", out eEmp)) errors.Add(eEmp);

            string eRet;
            if (!Validaciones.Requerido(h.RetiroTipo, "el tipo de retiro", out eRet)) errors.Add(eRet);
            if (h.RetiroTipo == "Retiro en agencia")
            {
                string eAR;
                if (!Validaciones.Requerido(h.AgenciaRetiro, "la agencia de retiro", out eAR)) errors.Add(eAR);
            }

            string eEnv;
            if (!Validaciones.Requerido(h.EnvioTipo, "el tipo de envío", out eEnv)) errors.Add(eEnv);

            string eProv;
            if (!Validaciones.Requerido(h.ProvinciaEnvio, "la provincia de envío", out eProv)) errors.Add(eProv);

            if (h.EnvioTipo == "Envío a agencia")
            {
                string eAE;
                if (!Validaciones.Requerido(h.AgenciaEnvio, "la agencia de envío", out eAE)) errors.Add(eAE);
            }

            string eDni;
            if (!Validaciones.EsDniValido(h.DniDestinatario, out eDni)) errors.Add(eDni);

            bool requiereDir = h.EnvioTipo == "Envío a domicilio";
            if (requiereDir)
            {
                string eLoc;
                if (!Validaciones.Requerido(h.LocalidadDestinatario, "la localidad del destinatario", out eLoc)) errors.Add(eLoc);
                string eDom;
                if (!Validaciones.Requerido(h.DomicilioDestinatario, "el domicilio del destinatario", out eDom)) errors.Add(eDom);
            }

            return errors.Count > 0
                ? (false, string.Join(Environment.NewLine, errors))
                : (true, "OK");
        }

        public (bool ok, string message) AgregarEncomienda(Encomienda e)
        {
            string errDim;
            if (!Validaciones.Requerido(e == null ? null : e.Dimension, "la dimensión de la encomienda", out errDim))
                return (false, errDim);

            if (e.Cantidad < 1)
                return (false, "La cantidad debe ser al menos 1.");

            _encomiendas.Add(e);
            return (true, "OK");
        }

        public (bool ok, string message) QuitarEncomiendaEn(int index)
        {
            if (index < 0 || index >= _encomiendas.Count) return (false, "Índice fuera de rango.");
            _encomiendas.RemoveAt(index);
            return (true, "OK");
        }

        // -------------------------
        // GUARDADO (dos variantes)
        // -------------------------

        /// <summary>
        /// Imposición: guarda en estado "Impuesta" y auto-asigna a fleteros (TXT), sin cambiar estado.
        /// </summary>
        public (bool ok, string message, string guiaId) GuardarPedido(PedidoHeader header)
        {
            // Unidades antes de guardar (la otra sobrecarga limpia la lista)
            int totalUnidades = 0;
            foreach (var e in _encomiendas) totalUnidades += Math.Max(1, e.Cantidad);

            var resultado = GuardarPedido(header, GuiaEstados.Impuesta);
            if (!resultado.ok) return resultado;

            try
            {
                // Asignación a fleteros (solo TXT) con las últimas N guías
                var ids = TomarUltimosIds(_repo.FilePath, totalUnidades);
                if (ids.Count > 0)
                {
                    var fleteros = ComboData.Fleteros;
                    var assign = new AssignmentService();
                    assign.AsignarRetirosPorIds(ids, fleteros);
                }
            }
            catch { /* no cortar el OK */ }

            return resultado;
        }

        /// <summary>
        /// Admisión CD (u otro flujo): guarda con estado inicial indicado.
        /// Si el estado inicial es "Admitida en CD origen", auto-asigna a patentes (TXT) sin cambiar estado.
        /// </summary>
        public (bool ok, string message, string guiaId) GuardarPedido(PedidoHeader header, string estadoInicial)
        {
            if (_encomiendas.Count == 0)
                return (false, "Debe agregar al menos una encomienda antes de aceptar el pedido.", null);

            var valid = ValidarHeader(header);
            if (!valid.ok) return (valid.ok, valid.message, null);

            // contamos unidades antes de escribir
            int totalUnidades = 0;
            foreach (var e in _encomiendas) totalUnidades += Math.Max(1, e.Cantidad);

            string estado = string.IsNullOrWhiteSpace(estadoInicial) ? GuiaEstados.Impuesta : estadoInicial;
            string guiaId = _repo.AppendLines(header, _encomiendas.ToArray(), estado);

            // limpiar items
            _encomiendas.Clear();

            // auto-asignación a patentes si corresponde
            try
            {
                if (string.Equals(estado, GuiaEstados.AdmitidaOrigen, StringComparison.OrdinalIgnoreCase))
                {
                    var ids = TomarUltimosIds(_repo.FilePath, totalUnidades);
                    if (ids.Count > 0)
                    {
                        var patentes = ComboData.Patentes;
                        var microAssign = new MicroAssignmentService();
                        microAssign.AsignarDespachosPorIds(ids, patentes);
                    }
                }
            }
            catch { /* no cortar el OK */ }

            return (true, "Pedido aceptado y guardado con todas las guías.", guiaId);
        }

        private static List<string> TomarUltimosIds(string path, int cantidad)
        {
            var ids = new List<string>();
            if (!File.Exists(path)) return ids;

            var lines = File.ReadAllLines(path).Skip(1)
                .Where(l => !string.IsNullOrWhiteSpace(l)).ToList();

            if (lines.Count == 0) return ids;

            var n = Math.Max(1, cantidad);
            var tail = lines.Skip(Math.Max(0, lines.Count - n)).ToList();

            foreach (var l in tail)
            {
                var cols = l.Split(';');
                var id = ((cols.Length > 0 ? cols[0] : "") ?? "").Replace("\"", "").Trim();
                if (id != "") ids.Add(id);
            }
            return ids;
        }

        public string ObtenerRutaArchivo()
        {
            return _repo.FilePath;
        }
    }
}
