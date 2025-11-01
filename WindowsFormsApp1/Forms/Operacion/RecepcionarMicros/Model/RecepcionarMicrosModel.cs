using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CAI_Proyecto.Forms.Operacion.RecepcionarMicros.Model
{
    public class RecepcionarMicrosModel
    {
        public IReadOnlyList<GuiasMicros> Guias { get; private set; } = new List<GuiasMicros>();
        // --- Datos de ejemplo ---
        private readonly List<Micro> _microsDemo = new List<Micro>
        {
            new Micro("AA123BB", "ViaSur S.A."),
            new Micro("AB456CD", "TransAndes S.R.L."),
            new Micro("AC789EF", "Expreso Norte"),
            new Micro("VX809MF", "Cruz del Sur S.A."),
        };

        private readonly List<GuiasMicros> _guiasDemo = new List<GuiasMicros>
        {
            // Guías del micro AA123BB
            new GuiasMicros("1001", "EnTransito", "AA123BB"),
            new GuiasMicros("1002", "EnTransito", "AA123BB"),

            // Guías del micro AB456CD
            new GuiasMicros("2001", "EnTransito", "AB456CD"),
            new GuiasMicros("2002", "EnTransito", "AB456CD"),

            // Guías del micro AC789EF
            new GuiasMicros("3001", "EnTransito", "AC789EF"),
            new GuiasMicros("3002", "EnTransito", "AC789EF"),
        };
        // --- Métodos públicos ---
        public List<Micro> ObtenerMicros() => _microsDemo;

        public bool BuscarPorMicro(Micro micro)
        {
            if (micro == null)
            {
                MessageBox.Show("Debe seleccionar una patente de micro.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 🔍 Filtrar guías solo de ese micro
            Guias = _guiasDemo
                .Where(g => g.PatenteAsignada == micro.Patente && g.Estado == "EnTransito")
                .ToList();

            if (Guias.Count == 0)
            {
                MessageBox.Show("No hay encomiendas en tránsito asociadas a esta patente.",
                    "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        public bool GuardarCambios()
        {
            // Validar que haya selección
            if (!Guias.Any(g => g.Seleccionada))
            {
                var confirm = MessageBox.Show(
                    "No seleccionó ninguna guía para recepcionar.\n¿Desea guardar igualmente?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes)
                    return false;
            }

            // Confirmación final
            var confirmSave = MessageBox.Show(
                "¿Confirma la recepción de las encomiendas seleccionadas?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmSave != DialogResult.Yes)
                return false;

            // Cambiar el estado de las guías seleccionadas
            foreach (var g in Guias.Where(g => g.Seleccionada))
                g.Estado = "Admitida en CD Destino";

            MessageBox.Show("Las guías fueron recepcionadas correctamente.", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Limpiar listas (simulando refresco)
            Guias = new List<GuiasMicros>();
            return true;
        }

    }
}
