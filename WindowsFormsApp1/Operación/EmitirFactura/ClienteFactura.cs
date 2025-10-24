using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Operación.EmitirFactura
{
    public class ClienteFactura
    {
        
                public string Cuit { get; set; }
                public string RazonSocial { get; set; }

       public string Cuit_RazonSocial
        {
            get { return $"({Cuit}) {RazonSocial}"; }
        }
    }
        }

    


    
