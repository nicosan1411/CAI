using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CAI_Proyecto.Forms.Consultas.EstadoGuia.Model
{
    public class EstadoGuiaModel
    {
        // Datos de prueba internos
        private readonly List<Guia> _demo = new List<Guia>
        {
            new Guia("1001", "M", "Centro de Distribución Rosario",  "Envío a domicilio", "32455678", "18-07-2020", "Admitida en CD destino"),
            new Guia("1002", "L", "Centro de Distribución CABA",     "Envío a domicilio", "33122345", "25-06-2021", "Entregada a cliente"),
            new Guia("1003", "S", "Centro de Distribución Córdoba",  "Envío a agencia",   "29877654", "05-09-2024", "En tránsito"),
            new Guia("1004", "M", "Centro de Distribución Tucumán",  "Envío a domicilio", "31233987", "08-10-2025", "Impuesta")
        };

        public IReadOnlyList<string> GuiasDisponibles => _demo
            .Select(g => g.NumeroGuia)
            .Distinct()
            .OrderBy(x => x)
            .ToList();

        public Guia ConsultarPorNumero(string numeroGuia)
        {
            if (string.IsNullOrWhiteSpace(numeroGuia))
            {
                MessageBox.Show("Debe seleccionar una guía antes de continuar.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            return _demo.FirstOrDefault(g => g.NumeroGuia == numeroGuia);
        }
    }
}
