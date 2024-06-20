using Domain.DTOs;

namespace MVC.Models
{
    public class ResumenViewModel
    {
        public int Año { get; set; }
        public int TipoMovimientoId { get; set; }
        public string TipoMovimientoNombre { get; set; }
        public int Cantidad { get; set; }

        public IEnumerable<ResumenStock> ResumenStocks { get; set; }
    }
}