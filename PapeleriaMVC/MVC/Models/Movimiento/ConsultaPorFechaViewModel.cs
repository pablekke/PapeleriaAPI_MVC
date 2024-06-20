using Domain;

namespace MVC.Models
{
    public class ConsultaPorFechaViewModel
    {
        public IEnumerable<Movimiento> Movimientos { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
