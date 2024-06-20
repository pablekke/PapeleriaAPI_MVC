using Domain;

namespace MVC.Models
{
    public class ConsultaPorArticuloYTipoViewModel
    {
        public string ArticuloNombre { get; set; }
        public string TipoMovimientoNombre { get; set; }
        public int ArticuloId { get; set; }
        public int TipoMovimientoId { get; set; }

        public IEnumerable<Articulo> Articulos { get; set; }
        public IEnumerable<Movimiento> Movimientos { get; set; }
        public IEnumerable<TipoMovimiento> TiposMovimiento { get; set; }
        // Propiedades para paginación
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
