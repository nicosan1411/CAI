using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Consultas.CuentaCorriente.Model
{
    public class Cliente
    {
        public string Cuit { get; set; }
        public string RazonSocial { get; set; }

        public string Cuit_RazonSocial
        {
            get { return $"({Cuit}) {RazonSocial}"; }
        }
    }
}
