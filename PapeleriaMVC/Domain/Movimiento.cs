namespace Domain
{
    public class Movimiento
    {
        public int MovimientoId { get; set; }
        public int ArticuloId { get; set; }
        public int TipoMovimientoId { get; set; }
        public TipoMovimiento TipoMovimiento { get; set; }
        public Articulo Articulo { get; set; }
        public string EmailEncargado { get; set; }
        public DateTime Fecha { get; set; }
        public int CantidadMovida { get; set; }
    }
}