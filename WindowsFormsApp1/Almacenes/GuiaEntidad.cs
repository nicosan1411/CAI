﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAI_Proyecto.Almacenes
{
    public class GuiaEntidad
    {
        public string NumeroGuia { get; set; }
        public TipoEstadoGuia Estado { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string CuitCliente { get; set; }
        public int AgenciaOrigen { get; set; }
        public string IdCDOrigen { get; set; }
        public string TipoEnvio { get; set; }
        public int DniDestinatario { get; set; }
        public string IdCDDestino { get; set; }
        public string ProvinciaDestino { get; set; }
        public string LocalidadDestino { get; set; }
        public string DomicilioDestino { get; set; }
        public TipoBulto Dimension { get; set; }
        public List<Recorrido> Recorridos { get; set; }
        public EstadoGuia EstadoActual { get; set; }
        public List<EstadoGuia> Historial { get; set; }
        public decimal Precio { get; set; }
        public decimal ComisionesAgenciaOrigen { get; set; }
        public decimal ComisionesAgenciaDestino { get; set; }
        public decimal ComisionesFleteroOrigen { get; set; }
        public decimal ComisionesFleteroDestino { get; set; }
    }
}
