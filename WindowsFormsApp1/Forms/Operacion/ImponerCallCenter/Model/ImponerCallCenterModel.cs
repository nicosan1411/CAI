using System;
using CAI_Proyecto.Almacenes.Almacen;
using CAI_Proyecto.Almacenes.ClaseAuxiliar;
using CAI_Proyecto.Almacenes.Entidad;
using System.Collections.Generic;
using System.Linq;

namespace CAI_Proyecto.Forms.Operacion.ImponerCallCenter.Model
{
    internal partial class ImponerCallCenterModel
    {

        public List<dynamic> Clientes
        {
            get
            {
                return ClienteAlmacen.Clientes
                    .Select(c => new
                    {
                        Cuit_RazonSocial = new Cliente
                        {
                            Cuit = c.Cuit,
                            RazonSocial = c.RazonSocial
                        }.Cuit_RazonSocial,
                        Entidad = c
                    })
                    .OrderBy(x => x.Cuit_RazonSocial)
                    .ToList<dynamic>();
            }
        }

        public List<ProvinciaEntidad> Provincias
        {
            get
            {
                return ProvinciaAlmacen.Provincias
                    .OrderBy(p => p.Nombre)
                    .ToList();
            }
        }

        public List<string> Dimensiones
        {
            get
            {
                return Enum.GetValues(typeof(TipoBultoEnum))
                    .Cast<TipoBultoEnum>()
                    .Select(d => d.ToString())
                    .ToList();
            }
        }

        public IEnumerable<AgenciaEntidad> AgenciasDeRetiroPorCliente(ClienteEntidad cliente)
        {
            if (cliente == null || cliente.AgenciasAsociadas == null || !cliente.AgenciasAsociadas.Any())
                return Enumerable.Empty<AgenciaEntidad>();

            var agencias = AgenciaAlmacen.Agencias
                .Where(a => cliente.AgenciasAsociadas.Contains(a.IdAgencia))
                .OrderBy(a => a.Nombre)
                .ToList();

            return agencias;
        }

        public IEnumerable<AgenciaEntidad> AgenciasDeEnvioPorProvincia(ProvinciaEntidad provincia)
        {
            if (provincia == null || string.IsNullOrWhiteSpace(provincia.IdCD))
                return Enumerable.Empty<AgenciaEntidad>();

            var agencias = AgenciaAlmacen.Agencias
                .Where(a => a.CDAsignado == provincia.IdCD)
                .OrderBy(a => a.Nombre)
                .ToList();

            return agencias;
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
    }
}
