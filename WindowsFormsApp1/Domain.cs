using System;
using System.Collections.Generic;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Encabezado de un pedido (datos de cliente, retiro y envío).
    /// </summary>
    internal class PedidoHeader
    {
        public string EmpresaCliente { get; set; }
        public string RetiroTipo { get; set; }            // "Retiro en domicilio" | "Retiro en agencia"
        public string AgenciaRetiro { get; set; }         // si aplica
        public string EnvioTipo { get; set; }             // "Envío a domicilio" | "Envío a CD" | "Envío a agencia"
        public string ProvinciaEnvio { get; set; }
        public string AgenciaEnvio { get; set; }          // si aplica
        public string DniDestinatario { get; set; }       // normalizado a dígitos
        public string LocalidadDestinatario { get; set; } // requerido si "Envío a domicilio"
        public string DomicilioDestinatario { get; set; } // requerido si "Envío a domicilio"
    }

    /// <summary>
    /// Ítem de encomienda: dimensión y cantidad.
    /// </summary>
    internal class Encomienda
    {
        public string Dimension { get; set; }
        public int Cantidad { get; set; }
    }

    /// <summary>
    /// Registro “aplanado” para persistencia CSV/Texto de una línea de imposición.
    /// </summary>
    internal class ImposicionRecord
    {
        public string Id { get; set; }            // mismo Id para todas las líneas del pedido
        public DateTime FechaHora { get; set; }
        public string EmpresaCliente { get; set; }
        public string RetiroTipo { get; set; }
        public string AgenciaRetiro { get; set; }
        public string EnvioTipo { get; set; }
        public string ProvinciaEnvio { get; set; }
        public string AgenciaEnvio { get; set; }
        public string DniDestinatario { get; set; }
        public string LocalidadDestinatario { get; set; }
        public string DomicilioDestinatario { get; set; }
        public string Dimension { get; set; }
        public int Cantidad { get; set; }
        public string Estado { get; set; }
    }
}
