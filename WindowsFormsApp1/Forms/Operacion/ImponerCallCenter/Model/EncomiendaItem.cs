namespace CAI_Proyecto.Forms.Operacion.ImponerCallCenter.Model
{
    public class EncomiendaItem
    {
        public Dimension Dimension { get; set; }
        public int Cantidad { get; set; }

        public string Dimension_Cantidad => $"{Dimension.Tamaño} x {Cantidad}";
    }
}
