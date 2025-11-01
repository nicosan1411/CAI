namespace CAI_Proyecto.Forms.Operacion.ImponerCallCenter.Model
{
    public class Cliente
    {
        public string Cuit { get; set; }
        public string RazonSocial { get; set; }
        public string Cuit_RazonSocial => $"({Cuit}) {RazonSocial}";
    }
}
