using CAI_Proyecto.Almacenes.Almacen;
using System.Collections.Generic;
using System.Linq;

namespace CAI_Proyecto.Forms.Operacion.EmitirFactura.Model
{
    public class EmitirFacturaModel
    {

        public IReadOnlyList<ListadoFacturacion> ListadoFacturacion { get; set; }

        public Cliente[] Clientes
        {
            get
            {
                return ClienteAlmacen.Clientes
                                     .Select(c => new Cliente
                                     {
                                         Cuit = c.Cuit,
                                         RazonSocial = c.RazonSocial,
                                     }).ToArray();
            }
        }

        public EmitirFacturaModel()
        {
            ListadoFacturacion = FacturaAlmacen.Facturas
                .Join(ClienteAlmacen.Clientes,
                      f => f.CuitCliente,
                      c => c.Cuit,
                      (f, c) => new ListadoFacturacion(
                          $"{c.Cuit}  {c.RazonSocial}",
                          string.Join(", ", f.NumeroGuia),
                          f.Concepto,
                          f.Monto
                      ))
                .ToList();
        }
    }
}