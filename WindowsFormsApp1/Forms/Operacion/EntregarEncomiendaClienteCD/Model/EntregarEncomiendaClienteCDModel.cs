using CAI_Proyecto.Almacenes.Almacen;
using CAI_Proyecto.Almacenes.ClaseAuxiliar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CAI_Proyecto.Forms.Operacion.EntregarEncomiendaClienteCD.Model
{
    internal partial class EntregarEncomiendaClienteCDModel
    {
        public Encomienda[] Encomiendas
        {
            get
            {
                if (Guias != null && Guias.Count > 0)
                    return Guias.ToArray();

                return GuiaAlmacen.Guias
                  .Select(g => new Encomienda(
                      g.NumeroGuia,
                      g.DniDestinatario.ToString(),
                      g.Estado.ToString()
                  ))
                  .ToArray();
            }
        }

        // Resultado de la última búsqueda (mapeadas a Encomienda)
        public IReadOnlyList<Encomienda> Guias { get; private set; } = new List<Encomienda>();

        // Buscar por DNI considerando CD actual y TipoEnvio = VaACD (valor enum 1)
        public bool BuscarPorDni(int dni)
        {
            var cdActual = CentroDeDistribucionAlmacen.CentroDeDistribucionActual?.IdCD;
            if (string.IsNullOrWhiteSpace(cdActual))
            {
                MessageBox.Show("Debe seleccionar el Centro de Distribución actual en el menú principal.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            Guias = GuiaAlmacen.Guias
               .Where(g =>
                      g.DniDestinatario == dni &&
                      string.Equals(g.IdCDDestino, cdActual, StringComparison.OrdinalIgnoreCase) &&
                      g.TipoEnvio == TipoEnvioEnum.VaACD)  // TipoEnvio = 1
               .Select(g => new Encomienda(
                   g.NumeroGuia,
                   g.DniDestinatario.ToString(),
                   g.Estado.ToString()))
               .ToList();

            if (Guias == null || Guias.Count == 0)
            {
                MessageBox.Show("No se encontraron guías para ese DNI en el CD actual con TipoEnvio = Centro de distribución.",
                    "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        public bool BuscarPorDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
            {
                MessageBox.Show("Debe ingresar un DNI para realizar la búsqueda.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(dni, out var dniInt))
            {
                MessageBox.Show("DNI inválido. Ingrese solo números.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return BuscarPorDni(dniInt);
        }

        // Entregar TODAS las guías listadas (Estado -> 8 = Entregado)
        public bool EntregarTodas()
        {
            if (Guias == null || Guias.Count == 0)
            {
                MessageBox.Show("No hay guías listadas para entregar.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var confirm = MessageBox.Show("¿Confirmás la entrega de todas las guías listadas?",
                "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return false;

            var numeros = new HashSet<string>(Guias.Select(g => g.NumeroGuia));

            // Actualizar objetos de la lista (UI)
            foreach (var enc in Guias.Where(x => numeros.Contains(x.NumeroGuia)))
                enc.Estado = TipoEstadoGuiaEnum.Entregado.ToString(); // 8

            // Actualizar entidades persistidas
            var entidades = GuiaAlmacen.Guias.Where(g => numeros.Contains(g.NumeroGuia)).ToList();
            foreach (var entidad in entidades)
            {
                entidad.Estado = TipoEstadoGuiaEnum.Entregado;

                if (entidad.Historial == null)
                    entidad.Historial = new List<EstadoGuia>();

                var nuevoEstado = new EstadoGuia
                {
                    Estado = TipoEstadoGuiaEnum.Entregado,
                    Fecha = DateTime.Now,
                    IdCentroDistribucion = entidad.IdCDDestino // entrega en CD destino
                };

                entidad.Historial.Add(nuevoEstado);
                entidad.EstadoActual = nuevoEstado;
            }

            GuiaAlmacen.Grabar();

            MessageBox.Show("Las guías fueron marcadas como entregadas.",
                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Guias = new List<Encomienda>();
            return true;
        }
    }
}