using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Operación.RendirEncomienda.Model
{
    public class FormRendirEncomiendasModelo
    {
        public IReadOnlyList<GuiaRendir> Retiros { get; private set; } = new List<GuiaRendir>();
        public IReadOnlyList<GuiaRendir> Entregas { get; private set; } = new List<GuiaRendir>();

        // --- Datos de prueba ---
        private readonly List<Fletero> _fleterosDemo = new List<Fletero>
        {
            new Fletero("José Pérez", "AA123BB", "F001"),
            new Fletero("Lucía Gómez", "AB456CD", "F002"),
            new Fletero("Carlos Ruiz", "AC789EF", "F003"),
        };

        private readonly List<GuiaRendir> _guiasDemo = new List<GuiaRendir>
        {
            new GuiaRendir("1001", "EnProcesoDeRetiro"),
            new GuiaRendir("1002", "EnProcesoDeRetiro"),
            new GuiaRendir("2001", "EnProcesoDeEntrega"),
            new GuiaRendir("2002", "EnProcesoDeEntrega"),
        };

        // --- Métodos públicos ---

        public List<Fletero> ObtenerFleteros() => _fleterosDemo;

        public bool BuscarPorFletero(Fletero fletero)
        {
            if (fletero == null)
            {
                MessageBox.Show("Debe seleccionar un fletero.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Separar guías por estado
            Retiros = _guiasDemo
                .Where(g => g.Estado == "EnProcesoDeRetiro")
                .ToList();

            Entregas = _guiasDemo
                .Where(g => g.Estado == "EnProcesoDeEntrega")
                .ToList();

            if (Retiros.Count == 0 && Entregas.Count == 0)
            {
                MessageBox.Show("No hay encomiendas asociadas al flete ingresado.",
                    "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        public bool GuardarCambios()
        {
            // Validación: no se seleccionó ninguna guía
            if (!Retiros.Any(r => r.Seleccionada) && !Entregas.Any(e => e.Seleccionada))
            {
                var confirm = MessageBox.Show(
                    "No se seleccionó ninguna guía de retiro ni entrega realizada.\n" +
                    "De continuar, el fletero no cumplió con ninguna obligación.\n¿Desea guardar igualmente?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes)
                    return false;
            }

            // Confirmación final
            var confirmSave = MessageBox.Show(
                "¿Está seguro de que desea guardar los cambios realizados?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmSave != DialogResult.Yes)
                return false;

            // Actualizar estados según el caso
            foreach (var g in Retiros.Where(r => r.Seleccionada))
                g.Estado = "Admitida en CD Origen";

            foreach (var g in Entregas.Where(e => e.Seleccionada))
                g.Estado = "Entregada a cliente";

            MessageBox.Show("Datos guardados correctamente.", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Limpiar datos internos (volver al estado inicial)
            Retiros = new List<GuiaRendir>();
            Entregas = new List<GuiaRendir>();
            return true;
        }

    }
}
