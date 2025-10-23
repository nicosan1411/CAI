using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Operación.RendirEncomienda.Model
{
    public class GuiaRendir
    {
        public string NroGuia { get; set; }
        public string Estado { get; set; }    // "EnProcesoDeRetiro" o "EnProcesoDeEntrega"
        public bool Seleccionada { get; set; }

        public GuiaRendir(string nroGuia, string estado)
        {
            NroGuia = nroGuia;
            Estado = estado;
            Seleccionada = false;
        }

    }
}
