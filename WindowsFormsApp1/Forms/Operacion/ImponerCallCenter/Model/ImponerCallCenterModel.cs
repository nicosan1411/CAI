using CAI_Proyecto.Almacenes.Almacen;
using CAI_Proyecto.Almacenes.ClaseAuxiliar;
using CAI_Proyecto.Almacenes.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CAI_Proyecto.Forms.Operacion.ImponerCallCenter.Model
{
    internal partial class ImponerCallCenterModel
    {
        public Cliente[] Clientes
        {
            get
            {
                return ClienteAlmacen.Clientes
                                     .Select(c => new Cliente
                                     {
                                         Cuit = c.Cuit,
                                         RazonSocial = c.RazonSocial
                                     }).ToArray();
            }
        }

        public Provincia[] Provincias
        {
            get
            {
                return ProvinciaAlmacen.Provincias
                                        .Select(p => new Provincia
                                        {
                                            Codigo = p.IdProvincia,
                                            Nombre = p.Nombre
                                        }).ToArray();
            }
        }

        public Dimension[] Dimensiones => new Dimension[]
        {
            new Dimension{ Tamaño = "S" },
            new Dimension{ Tamaño = "M" },
            new Dimension{ Tamaño = "L" },
            new Dimension{ Tamaño = "XL" }
        };

        public IEnumerable<AgenciaEnvio> AgenciasEnvioPorProvincia(string provinciaCodigo)
        {
            var provincia = ProvinciaAlmacen.Provincias.Single(p => p.IdProvincia == provinciaCodigo);
            var cd = CentroDeDistribucionAlmacen.CentrosDeDistribucion.Single(cd => cd.IdCD == provincia.IdCD);

            return AgenciaAlmacen.Agencias
                                 .Where(a => a.CDAsignado == cd.IdCD)
                                 .Select(a => new AgenciaEnvio
                                 {
                                     Id = a.IdAgencia,
                                     Nombre = a.Nombre,
                                     ProvinciaCodigo = provinciaCodigo
                                 });
        }

        public List<AgenciaRetiro> AgenciasRetiroCliente(Cliente cliente)
        {
            if (cliente == null)
            {
                return new List<AgenciaRetiro>();
            }

            var clienteEntidad = ClienteAlmacen.Clientes.Single(c => c.Cuit == cliente.Cuit);

            return clienteEntidad.AgenciasAsociadas
                                 .Select(idAgencia => AgenciaAlmacen.Agencias.Single(agenciaEntidad => idAgencia == agenciaEntidad.IdAgencia))
                                 .Select(agenciaEntidad => new AgenciaRetiro
                                 {
                                     Id = agenciaEntidad.IdAgencia,
                                     Nombre = agenciaEntidad.Nombre
                                 })
                                 .ToList();
        }

        public List<EncomiendaItem> Encomiendas { get; } = new List<EncomiendaItem>();

        public void AgregarEncomienda(EncomiendaItem encomienda)
        {
            Encomiendas.Add(encomienda);
        }

        public void QuitarEncomienda(EncomiendaItem encomienda)
        {
            Encomiendas.Remove(encomienda);
        }

        public void LimpiarEncomiendas()
        {
            Encomiendas.Clear();
        }

        /*
         * Reglas de negocio del form para aceptar un pedido.
         * Devuelve lista de errores. Si la lista está vacía, el pedido es válido.
         */
        public List<string> ValidarPedido(Pedido p)
        {
            var errores = new List<string>();

            if (p.Cliente == null)
                errores.Add("Debe seleccionar un cliente.");

            // Tipo de retiro
            if (string.IsNullOrWhiteSpace(p.TipoRetiro))
                errores.Add("Debe seleccionar el tipo de retiro.");
            else if (p.TipoRetiro == "Agencia" && p.AgenciaRetiro == null)
                errores.Add("Debe seleccionar la agencia de retiro.");

            // Tipo de envío
            switch (p.TipoEnvio)
            {
                case "Domicilio":
                    if (p.ProvinciaEnvio == null) errores.Add("Debe seleccionar la provincia de envío.");
                    if (string.IsNullOrWhiteSpace(p.DniDestinatario)) errores.Add("Debe ingresar el DNI del destinatario.");
                    if (string.IsNullOrWhiteSpace(p.LocalidadDestinatario)) errores.Add("Debe ingresar la localidad del destinatario.");
                    if (string.IsNullOrWhiteSpace(p.DomicilioDestinatario)) errores.Add("Debe ingresar el domicilio del destinatario.");
                    break;

                case "Centro de distribución":
                    if (p.ProvinciaEnvio == null) errores.Add("Debe seleccionar la provincia del centro de distribución.");
                    if (string.IsNullOrWhiteSpace(p.DniDestinatario)) errores.Add("Debe ingresar el DNI del destinatario.");
                    break;

                case "Agencia":
                    if (p.ProvinciaEnvio == null) errores.Add("Debe seleccionar la provincia de la agencia de envío.");
                    if (p.AgenciaEnvio == null) errores.Add("Debe seleccionar la agencia de envío.");
                    if (string.IsNullOrWhiteSpace(p.DniDestinatario)) errores.Add("Debe ingresar el DNI del destinatario.");
                    break;

                default:
                    errores.Add("Debe seleccionar un tipo de envío.");
                    break;
            }

            if (p.Encomiendas == null || p.Encomiendas.Count == 0)
                errores.Add("Debe agregar al menos una encomienda.");

            return errores;
        }

        public void Aceptar(Pedido pedido)
        {
            //TODO: crear una entidad GuiasEntidad y mandarla al almacen.
            var guia = new GuiaEntidad();
            //llenarla con los datos de pedido.
            //(ver que otra operacion o cambio en el modelo)
        }
    }
}
