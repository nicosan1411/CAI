using CAI_Proyecto.Almacenes.Almacen;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CAI_Proyecto.Forms.Operacion.RecepcionarMicros.Model
{
    public class RecepcionarMicrosModel
    {
        public IReadOnlyList<GuiasMicros> Guias { get; private set; } = new List<GuiasMicros>();

        public List<Micro> Micros
        {
            get
            {
                return MicroAlmacen.Micros
                                   .Select(m => new Micro(m.Patente, m.CuitEmpresaMicro))
                                   .ToList();
            }
        }

        public List<GuiasMicros> _guiasDemo
        {
            get
            {
                return HojaDeRutaMicroAlmacen.HojasDeRutaMicro
                    .SelectMany(hoja => hoja.Guias.Select(nroGuia => new { hoja, nroGuia }))
                    .Select(x => new GuiasMicros(
                        x.nroGuia,
                        "EnTransitoPorMicro",
                        MicroAlmacen.Micros.FirstOrDefault(m => m.HojasDeRuta.Contains(x.hoja.IdHDRMicro))?.Patente
                    ))
                    .ToList();
            }
        }

        // Faltaría arreglar la selección de guías con el check para que guardarlas

        // --- Métodos públicos ---
        public List<Micro> ObtenerMicros() => Micros;

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
                    "No elegiste ninguna guía para recepcionar.\n¿Querés guardar igualmente?",
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
