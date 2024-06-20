namespace Domain.DTOs
{
    public class ResumenStock
    {
        public int Año { get; set; }
        public int TipoMovimientoId { get; set; }
        public string TipoMovimientoNombre { get; set; }
        public int Cantidad { get; set; }
    }
}
