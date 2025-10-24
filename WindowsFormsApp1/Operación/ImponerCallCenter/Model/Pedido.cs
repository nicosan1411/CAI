using System.Collections.Generic;

namespace WindowsFormsApp1.Operación.ImponerCallCenter.Model
{
    public class Pedido
    {
        // Cliente
        public Cliente Cliente { get; set; }

        // Retiro
        public string TipoRetiro { get; set; }           // "Domicilio" | "Agencia"
        public AgenciaRetiro AgenciaRetiro { get; set; }       // requerido si TipoRetiro = "Agencia"

        // Envío
        public string TipoEnvio { get; set; }            // "Domicilio" | "Agencia" | "Centro de distribución"
        public Provincia ProvinciaEnvio { get; set; }    // según la opción de envío
        public AgenciaEnvio AgenciaEnvio { get; set; }        // requerido si TipoEnvio = "Agencia"

        // Destinatario (cuando aplique)
        public string DniDestinatario { get; set; }
        public string LocalidadDestinatario { get; set; }
        public string DomicilioDestinatario { get; set; }

        // Encomiendas
        public List<EncomiendaItem> Encomiendas { get; set; } = new List<EncomiendaItem>();
    }
}

