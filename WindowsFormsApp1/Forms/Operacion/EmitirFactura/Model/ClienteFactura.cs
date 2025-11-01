namespace CAI_Proyecto.Forms.Operacion.EmitirFactura.Model
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
