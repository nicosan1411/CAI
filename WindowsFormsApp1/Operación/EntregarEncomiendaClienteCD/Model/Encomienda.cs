namespace WindowsFormsApp1.Operacion.EntregarEncomiendaClienteCD.Model
{
    public class Encomienda
    {
        public string NroGuia { get; set; }
        public string Destinatario { get; set; }
        public string DNI { get; set; }
        public string Estado { get; set; }  // Necesario para “Admitida en CD destino” / “Entregada”

        public Encomienda(string nroGuia, string destinatario, string dni, string estado)
        {
            NroGuia = nroGuia;
            Destinatario = destinatario;
            DNI = dni;
            Estado = estado;
        }
    }
}
