namespace CAI_Proyecto.Forms.Consultas.ResultadosCostosVentas.Model
{
    public class ResultadoCostoVenta
    {
        public string Empresa { get; set; }
        public int Envios { get; set; }
        public decimal CostoTotal { get; set; }
        public decimal VentasTotales { get; set; }

      
        public decimal Resultado => VentasTotales - CostoTotal;
        public decimal Margen => VentasTotales == 0 ? 0 : Resultado / VentasTotales;

        public ResultadoCostoVenta(string empresa, int envios, decimal costoTotal, decimal ventasTotales)
        {
            Empresa = empresa;
            Envios = envios;
            CostoTotal = costoTotal;
            VentasTotales = ventasTotales;
        }
    }
}