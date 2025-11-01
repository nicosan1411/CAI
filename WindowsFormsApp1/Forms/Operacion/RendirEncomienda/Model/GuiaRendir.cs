namespace CAI_Proyecto.Forms.Operacion.RendirEncomienda.Model
{
    public class GuiaRendir
    {
        public string NroGuia { get; set; }
        public string Estado { get; set; }    // "EnProcesoDeRetiro" o "EnProcesoDeEntrega"
        public bool Seleccionada { get; set; }
        public string NroFleteAsignado { get; set; }

        public GuiaRendir(string nroGuia, string estado, string nroFleteAsignado)
        {
            NroGuia = nroGuia;
            Estado = estado;
            Seleccionada = false;
            NroFleteAsignado = nroFleteAsignado;
        }

    }
}
