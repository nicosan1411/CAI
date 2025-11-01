using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace CAI_Proyecto.Almacenes
{
    public class Parada
    {
        public string CDOrigen { get; set; }
        public string CDDestino { get; set; }
        public DateTime Llegada { get; set; }
        public DateTime Salida { get; set; }
    }
}
