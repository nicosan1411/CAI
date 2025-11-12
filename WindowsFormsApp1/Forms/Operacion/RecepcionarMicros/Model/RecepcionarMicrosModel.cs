using CAI_Proyecto.Almacenes.Almacen;
using CAI_Proyecto.Almacenes.ClaseAuxiliar;
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

        public List<GuiasMicros> GuiasMicros
        {
            get
            {
                var estadoObjetivo = TipoEstadoGuiaEnum.AdmitidoCDDestino;

                // Une Hojas de Ruta con sus guías y trae el estado real desde GuiaAlmacen
                var query =
                    from hoja in HojaDeRutaMicroAlmacen.HojasDeRutaMicro
                    from nroGuia in hoja.Guias
                    let entidad = GuiaAlmacen.Guias.FirstOrDefault(g => g.NumeroGuia == nroGuia)
                    where entidad != null
                    let estadoReal = (entidad.EstadoActual != null ? entidad.EstadoActual.Estado : entidad.Estado)
                    where estadoReal == estadoObjetivo
                    let patente = MicroAlmacen.Micros
                        .FirstOrDefault(m => m.HojasDeRuta != null && m.HojasDeRuta.Contains(hoja.IdHDRMicro))?.Patente
                    select new GuiasMicros(
                        nroGuia,
                        estadoReal.ToString(),
                        patente
                    );

                return query.ToList();
            }
        }

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

            var estadoObjetivoStr = TipoEstadoGuiaEnum.AdmitidoCDDestino.ToString();

            // Filtrar guías solo de ese micro y en el estado objetivo
            Guias = GuiasMicros
                .Where(g => g.PatenteAsignada == micro.Patente && g.Estado == estadoObjetivoStr)
                .ToList();

            if (Guias.Count == 0)
            {
                MessageBox.Show("No hay encomiendas en admisión asociadas a esta patente.",
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

            // Nota: aquí solo se actualiza el estado mostrado en la UI.
            // Si necesitás persistir el cambio en GuiaAlmacen, hay que buscar cada guía por NumeroGuia y grabar.
            foreach (var g in Guias.Where(g => g.Seleccionada))
                g.Estado = TipoEstadoGuiaEnum.Recepcionado.ToString();

            MessageBox.Show("Las guías fueron recepcionadas correctamente.", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Limpiar listas (simulando refresco)
            Guias = new List<GuiasMicros>();
            return true;
        }
    }
}
