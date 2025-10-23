namespace WindowsFormsApp1.Operación.ImponerCallCenter.Model
{
    public class EncomiendaItem
    {
        public Dimension Dimension { get; set; }
        public int Cantidad { get; set; }

        public string Dimension_Cantidad => $"{Dimension.Tamaño} x {Cantidad}";
    }
}
