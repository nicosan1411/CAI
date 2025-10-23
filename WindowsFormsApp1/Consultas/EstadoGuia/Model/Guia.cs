using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Consultas.EstadoGuia
{
    public class Guia
    {
        public string NumeroGuia { get; set; }       
        public string Dimension { get; set; }        
        public string CDDestino { get; set; }        
        public string TipoEnvio { get; set; }        
        public string DniDestinatario { get; set; }  
        public string FechaIngreso { get; set; }    
        public string Estado { get; set; }          

        public Guia(string numeroGuia, string dimension, string cdDestino, string tipoEnvio,
                    string dniDestinatario, string fechaIngreso, string estado)
        {
            NumeroGuia = numeroGuia;
            Dimension = dimension;
            CDDestino = cdDestino;
            TipoEnvio = tipoEnvio;
            DniDestinatario = dniDestinatario;
            FechaIngreso = fechaIngreso;
            Estado = estado;
        }
    }
}
