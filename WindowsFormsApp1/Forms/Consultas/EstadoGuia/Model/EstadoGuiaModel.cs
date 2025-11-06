using CAI_Proyecto.Almacenes.Almacen;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CAI_Proyecto.Forms.Consultas.EstadoGuia.Model
{
    public class EstadoGuiaModel
    {
        // Datos de prueba internos
        /*private readonly List<Guia> _demo = new List<Guia>
        {
            new Guia("1001", "M", "Centro de Distribución Rosario",  "Envío a domicilio", "32455678", "18-07-2020", "Admitida en CD destino"),
            new Guia("1002", "L", "Centro de Distribución CABA",     "Envío a domicilio", "33122345", "25-06-2021", "Entregada a cliente"),
            new Guia("1003", "S", "Centro de Distribución Córdoba",  "Envío a agencia",   "29877654", "05-09-2024", "En tránsito"),
            new Guia("1004", "M", "Centro de Distribución Tucumán",  "Envío a domicilio", "31233987", "08-10-2025", "Impuesta")
        }; */

        /*
         * Devuelve una lista con los números de todas las guías disponibles en el almacén.
         * Se usa para llenar el ComboBox de selección de guía en el formulario.
         */
        public IReadOnlyList<string> GuiasDisponibles =>
            GuiaAlmacen.Guias
                .Select(g => g.NumeroGuia)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

        /*
         * Devuelve todas las guías en formato de clase intermedia "Guia"
         * (para mostrar en la tabla del formulario).
         */
        public Guia[] Guias
        {
            get
            {
                return GuiaAlmacen.Guias
                    .Select(g => new Guia(
                        g.NumeroGuia,
                        g.Dimension.ToString(),
                        g.IdCDDestino,
                        g.TipoEnvio.ToString(),
                        g.DniDestinatario.ToString(),
                        g.FechaIngreso.ToString("dd-MM-yyyy"),
                        g.EstadoActual != null ? g.EstadoActual.Estado.ToString() : g.Estado.ToString()
                    ))
                    .ToArray();
            }
        }

        /*
         * Busca una guía específica por su número y devuelve su información lista para mostrar.
         */
        public Guia ConsultarPorNumero(string numeroGuia)
        {
            if (string.IsNullOrWhiteSpace(numeroGuia))
            {
                MessageBox.Show("Debe seleccionar una guía antes de continuar.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            var entidad = GuiaAlmacen.Guias.FirstOrDefault(g => g.NumeroGuia == numeroGuia);

            if (entidad == null)
            {
                MessageBox.Show($"No se encontró la guía {numeroGuia}.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            return new Guia(
                entidad.NumeroGuia,
                entidad.Dimension.ToString(),
                entidad.IdCDDestino,
                entidad.TipoEnvio.ToString(),
                entidad.DniDestinatario.ToString(),
                entidad.FechaIngreso.ToString("dd-MM-yyyy"),
                entidad.EstadoActual != null ? entidad.EstadoActual.Estado.ToString() : entidad.Estado.ToString()
            );
        }
    }
}

