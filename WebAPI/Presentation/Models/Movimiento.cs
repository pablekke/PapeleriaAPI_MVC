using Domain.DTOs;

namespace Presentation
{
    public class Movimiento
    {
        public int ArticuloId { get; set; }
        public int TipoMovimientoId { get; set; }
        public string EmailEncargado { get; set; }
        public int CantidadMovida { get; set; }

        public StockArticuloDTO ToDTO()
        {
            return new StockArticuloDTO
            {
                MovimientoId = 0,
                ArticuloId = ArticuloId,
                TipoMovimientoId = TipoMovimientoId,
                EmailEncargado = EmailEncargado,
                CantidadMovida = CantidadMovida,
                Fecha = DateTime.Now
            };
        }
    }
}