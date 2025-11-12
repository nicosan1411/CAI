using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CAI_Proyecto.Almacenes.Almacen;
using CAI_Proyecto.Almacenes.Entidad;

namespace CAI_Proyecto.Forms.Operacion.RendirEncomienda.Model
{
    public class RendirEncomiendasModel
    {
        public IReadOnlyList<GuiaRendir> Retiros { get; private set; } = new List<GuiaRendir>();
        public IReadOnlyList<GuiaRendir> Entregas { get; private set; } = new List<GuiaRendir>();

        public List<Fletero> Fleteros = new List<Fletero>();

        public List<GuiaRendir> Guias = new List<GuiaRendir>
        {
            new GuiaRendir("1001", "En Proceso De Retiro", "F001"),
            new GuiaRendir("1002", "En Proceso De Entrega", "F001"),
            new GuiaRendir("2001", "En Proceso De Retiro", "F002"),
            new GuiaRendir("2002", "En Proceso De Entrega", "F002"),
            new GuiaRendir("3001", "En Proceso De Retiro", "F003"),
            new GuiaRendir("3002", "En Proceso De Entrega", "F003"),
        };

        public RendirEncomiendasModel()
        {
            CargarFleterosSegunContexto();
        }

        /// <summary>
        /// Devuelve la lista de fleteros vigente. Recalcula cada vez para reflejar cambios
        /// en la agencia seleccionada en InicioForm.
        /// </summary>
        public List<Fletero> ObtenerFleteros()
        {
            CargarFleterosSegunContexto();
            return Fleteros;
        }

        /// <summary>
        /// Carga los fleteros filtrando por la agencia actual (AgenciaAlmacen.AgenciaActual).
        /// Si no hay agencia seleccionada, queda la lista vacía.
        /// </summary>
        private void CargarFleterosSegunContexto()
        {
            var agencia = AgenciaAlmacen.AgenciaActual;

            if (agencia == null)
            {
                Fleteros = new List<Fletero>();
                return;
            }

            // Fleteros del almacén que pertenecen a la agencia actual.
            // Se proyectan a la clase Fletero utilizada en este módulo.
            Fleteros = FleteroAlmacen.Fleteros
                .Where(f => f.IdAgenciaAsignada == agencia.IdAgencia)
                .OrderBy(f => f.Nombre)
                .Select(f => new Fletero(
                    nombre: f.Nombre,
                    patente: f.CuitEmpresaFlete ?? string.Empty,
                    nroFlete: f.IdFletero))
                .ToList();
        }

        public bool BuscarPorFletero(Fletero fletero)
        {
            if (fletero == null)
            {
                MessageBox.Show("Debe seleccionar un fletero.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var guiasFletero = Guias
                .Where(g => g.NroFleteAsignado == fletero.NroFlete)
                .ToList();

            Retiros = guiasFletero
                .Where(g => g.Estado == "En Proceso De Retiro")
                .ToList();

            Entregas = guiasFletero
                .Where(g => g.Estado == "En Proceso De Entrega")
                .ToList();

            if (Retiros.Count == 0 && Entregas.Count == 0)
            {
                MessageBox.Show("No hay encomiendas asociadas al fletero ingresado.",
                    "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        public bool GuardarCambios()
        {
            if (!Retiros.Any(r => r.Seleccionada) && !Entregas.Any(e => e.Seleccionada))
            {
                var confirm = MessageBox.Show(
                    "No se seleccionó ninguna guía de retiro ni entrega realizada.\n" +
                    "De continuar, el fletero no cumplió con ninguna obligación.\n¿Desea guardar igualmente?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes)
                    return false;
            }

            var confirmSave = MessageBox.Show(
                "¿Está seguro de que desea guardar los cambios realizados?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmSave != DialogResult.Yes)
                return false;

            foreach (var g in Retiros.Where(r => r.Seleccionada))
                g.Estado = "Admitida en CD Origen";

            foreach (var g in Entregas.Where(e => e.Seleccionada))
                g.Estado = "Entregada a cliente";

            MessageBox.Show("Datos guardados correctamente.", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            Retiros = new List<GuiaRendir>();
            Entregas = new List<GuiaRendir>();
            return true;
        }
    }
}
