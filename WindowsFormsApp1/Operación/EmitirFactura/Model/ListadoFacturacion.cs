namespace WindowsFormsApp1.Operación.EmitirFactura.Model
{
    public class ListadoFacturacion
    {

        public string Cliente { get; set; }
        public string NroGuia { get; set; }
        public string Concepto { get; set; }
        public decimal Monto { get; set; }


        public ListadoFacturacion(string cliente, string nroGuia, string concepto, decimal monto)
        {
            Cliente = cliente;
            NroGuia = nroGuia;
            Concepto = concepto;
            Monto = monto;
                 }
    }
}

