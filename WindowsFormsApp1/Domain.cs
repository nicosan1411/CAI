using System;
using System.Collections.Generic;
using System.Web;

namespace WindowsFormsApp1
{
    internal class PedidoHeader
    {
        public string EmpresaCliente { get; set; }
        public string RetiroTipo { get; set; }              // "Retiro en domicilio" | "Retiro en agencia"
        public string AgenciaRetiro { get; set; }           // si aplica
        public string EnvioTipo { get; set; }               // "Envío a domicilio" | "Envío a centro de distribución" | "Envío a agencia"
        public string ProvinciaEnvio { get; set; }
        public string AgenciaEnvio { get; set; }            // si aplica
        public string DniDestinatario { get; set; }         // normalizado a dígitos
        public string LocalidadDestinatario { get; set; }   // requerido si Envío a domicilio
        public string DomicilioDestinatario { get; set; }   // requerido si Envío a domicilio
    }

    internal class Encomienda
    {
        public string Dimension { get; set; }
        public int Cantidad { get; set; }
    }

    internal class ImposicionRecord
    {
        public string Id { get; set; }              // mismo Id para todas las líneas del pedido
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
