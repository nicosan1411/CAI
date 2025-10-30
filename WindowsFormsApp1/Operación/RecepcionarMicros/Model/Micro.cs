namespace WindowsFormsApp1.Operación.RecepcionarMicros.Model
{
    public class Micro
    {
        public string Patente { get; set; }
        public string EmpresaTransporte { get; set; }

        public Micro(string patente, string empresaTransporte)
        {
            Patente = patente;
            EmpresaTransporte = empresaTransporte;
        }

        //Define cómo se muestra el fletero en el ComboBox.
        public override string ToString() => $"{Patente}";
    }
}
