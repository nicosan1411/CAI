using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Operación.RendirEncomienda.Model
{
    public class Fletero
    {
        public string Nombre { get; set; }
        public string Patente { get; set; }
        public string NroFlete { get; set; }

        public Fletero(string nombre, string patente, string nroFlete)
        {
            Nombre = nombre;
            Patente = patente;
            NroFlete = nroFlete;
        }

        //Define cómo se muestra el fletero en el ComboBox.
        public override string ToString() => $"{Nombre} - {Patente} - {NroFlete}";
    }
}
