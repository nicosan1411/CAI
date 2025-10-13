namespace WindowsFormsApp1
{
    /// <summary>
    /// Estados de vida de una guía. Mantener nombres consistentes con los CU.
    /// </summary>
    internal static class GuiaEstados
    {
        public const string Impuesta = "Impuesta";
        public const string EnProcesoRetiro = "En proceso de retiro";
        public const string AdmitidaOrigen = "Admitida en CD origen";
        public const string EnTransito = "En tránsito";
        public const string AdmitidaDestino = "Admitida en CD destino";
        public const string EnProcesoEntrega = "En proceso de entrega";
        public const string EntregadaCliente = "Entregada a cliente";

        /// <summary>Facilita validaciones: lista completa de estados conocidos.</summary>
        public static readonly string[] Todos =
        {
            Impuesta, EnProcesoRetiro, AdmitidaOrigen, EnTransito, AdmitidaDestino, EnProcesoEntrega, EntregadaCliente
        };
    }
}
