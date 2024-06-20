using Domain;

namespace MVC.Models
{
    public class ListarMovimientosViewModel
    {
        public IEnumerable<Movimiento> Movimientos { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize = 10;
    }
}