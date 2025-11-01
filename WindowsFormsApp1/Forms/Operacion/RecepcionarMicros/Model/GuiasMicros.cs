namespace CAI_Proyecto.Forms.Operacion.RecepcionarMicros.Model
{
    public class GuiasMicros
    {
        public string NroGuia { get; set; }
        public string Estado { get; set; }    // "EnTransito" 
        public bool Seleccionada { get; set; }
        public string PatenteAsignada { get; set; }

        public GuiasMicros(string nroGuia, string estado, string patenteAsignada)
        {
            NroGuia = nroGuia;
            Estado = estado;
            Seleccionada = false;
            PatenteAsignada = patenteAsignada;
        }
    }
}
