using CAI_Proyecto.Almacenes.Almacen;
using CAI_Proyecto.Almacenes.ClaseAuxiliar;
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
                return GuiaAlmacen.Guias
                  .Select(g => new Encomienda(
                      g.NumeroGuia,
                      g.DniDestinatario.ToString(),   
                      g.Estado.ToString()             
                  ))
                  .ToArray();
            }
        }

        // Resultado de la última búsqueda (listas mapeadas a Encomienda)
        public IReadOnlyList<Encomienda> Guias { get; private set; } = new List<Encomienda>();

        // Buscar por DNI solo "Admitida en CD destino"
        public bool BuscarPorDni(int dni)
        {
            // Buscar guías cuyo DniDestinatario coincide y que estén admitidas en CD destino
            Guias = GuiaAlmacen.Guias
                .Where(g => g.DniDestinatario == dni && g.Estado == TipoEstadoGuiaEnum.AdmitidoCDDestino)
                .Select(g => new Encomienda(g.NumeroGuia, g.DniDestinatario.ToString(), g.Estado.ToString()))
                .ToList();

            if (Guias == null || Guias.Count == 0)
            {
                MessageBox.Show("No se encontraron encomiendas admitidas en CD destino para ese DNI.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return true;
        }

        public bool BuscarPorDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
            {
                MessageBox.Show("Debe ingresar un DNI para realizar una búsqueda.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(dni, out var dniInt))
            {
                MessageBox.Show("DNI inválido. Ingrese solo números.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return BuscarPorDni(dniInt);
        }

        // Entregar TODAS las listadas
        public bool EntregarTodas()
        {
            if (Guias == null || Guias.Count == 0)
            {
                MessageBox.Show("No hay encomiendas para entregar.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var confirm = MessageBox.Show("¿Estás seguro que querés realizar esta acción?",
                "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return false;

            var aEntregar = new HashSet<string>(Guias.Select(e => e.NumeroGuia));
            foreach (var enc in Guias.Where(x => aEntregar.Contains(x.NumeroGuia)))
                enc.Estado = "Entregado";

            MessageBox.Show("¡Su pedido fue entregado!", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            Guias = new List<Encomienda>(); // limpiar después de entregar
            return true;
        }
    }
}