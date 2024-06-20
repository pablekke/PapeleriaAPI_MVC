namespace Domain.DTOs
{
    public class StockArticuloDTO
    {
        public int MovimientoId { get; set; }
        public int ArticuloId { get; set; }
        public int TipoMovimientoId { get; set; }
        public ArticuloDTO Articulo { get; set; }
        public TipoMovimientoDTO TipoMovimiento { get; set; }
        public string EmailEncargado { get; set; }
        public int CantidadMovida { get; set; }
        public DateTime Fecha { get; set; }
    }
}