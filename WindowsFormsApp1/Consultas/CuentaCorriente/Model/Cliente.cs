namespace WindowsFormsApp1.Consultas.CuentaCorriente.Model
{
    public class Cliente
    {
        public string Cuit { get; set; }
        public string RazonSocial { get; set; }

        public string Cuit_RazonSocial
        {
            get { return $"({Cuit}) {RazonSocial}"; }
        }
    }
}
