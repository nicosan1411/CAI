namespace CAI_Proyecto.Forms.Consultas.EstadoGuia.Model
{
    public class Guia
    {
        public string NumeroGuia { get; set; }       
        public string Dimension { get; set; }        
        public string IdCDDestino { get; set; }        
        public string TipoEnvio { get; set; }        
        public string DniDestinatario { get; set; }  
        public string FechaIngreso { get; set; }    
        public string Estado { get; set; }          

        public Guia(string numeroGuia, string dimension, string IdcdDestino, string tipoEnvio,
                    string dniDestinatario, string fechaIngreso, string estado)
        {
            NumeroGuia = numeroGuia;
            Dimension = dimension;
            IdCDDestino = IdcdDestino;
            TipoEnvio = tipoEnvio;
            DniDestinatario = dniDestinatario;
            FechaIngreso = fechaIngreso;
            Estado = estado;
        }
    }
}
