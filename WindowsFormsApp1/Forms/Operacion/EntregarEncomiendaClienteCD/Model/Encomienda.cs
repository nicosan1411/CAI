namespace CAI_Proyecto.Forms.Operacion.EntregarEncomiendaClienteCD.Model
{
    public class Encomienda
    {
        public string NumeroGuia { get; set; }
        public string DniDestinatario { get; set; }
        public string Estado { get; set; }  // Necesario para “Admitida en CD destino” / “Entregada”

        public Encomienda(string nroGuia, string dni, string estado)
        {
            NumeroGuia = nroGuia;
            DniDestinatario = dni;
            Estado = estado;
        }
    }
}
