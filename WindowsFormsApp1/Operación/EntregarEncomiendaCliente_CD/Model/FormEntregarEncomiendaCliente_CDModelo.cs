using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1.Operacion.EntregarEncomiendaCliente_CD.Model
{
    internal class FormEntregarEncomiendaCliente_CDModelo
    {
        // Resultado de la última búsqueda
        public IReadOnlyList<Encomienda> Encomiendas { get; private set; } = new List<Encomienda>();

        // Datos de prueba internos (evitamos conflictos con ComboData)
        private readonly List<Encomienda> _demo = new List<Encomienda>
        {
            new Encomienda("1001", "Carlos Pérez",   "32455678", "Admitida en CD destino"),
            new Encomienda("1002", "María Gómez",    "29877654", "Admitida en CD destino"),
            new Encomienda("1003", "Laura Fernández","31233987", "En tránsito"),
            new Encomienda("1004", "Juan Torres",    "33122345", "Admitida en CD destino"),
            new Encomienda("1005", "Jimena Muñoz",    "33122345", "Entregada")
        };

        // Buscar por DNI solo "Admitida en CD destino"
        public bool BuscarPorDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
            {
                MessageBox.Show("Debe ingresar un DNI para realizar una búsqueda.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            Encomiendas = _demo
                .Where(e => e.DNI == dni && e.Estado == "Admitida en CD destino")
                .ToList();

            return true;
        }

        // Entregar TODAS las listadas
        public bool EntregarTodas()
        {
            if (Encomiendas == null || Encomiendas.Count == 0)
            {
                MessageBox.Show("No hay encomiendas para entregar.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var confirm = MessageBox.Show("¿Estás seguro que querés realizar esta acción?",
                "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return false;

            var aEntregar = new HashSet<string>(Encomiendas.Select(e => e.NroGuia));
            foreach (var enc in _demo.Where(x => aEntregar.Contains(x.NroGuia)))
                enc.Estado = "Entregada";

            MessageBox.Show("¡Su pedido fue entregado!", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            Encomiendas = new List<Encomienda>(); // limpiar después de entregar
            return true;
        }
    }
}