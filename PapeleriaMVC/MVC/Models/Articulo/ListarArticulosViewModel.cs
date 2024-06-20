using Domain;

namespace MVC.Models
{
    public class ListarArticulosViewModel
    {
        public int ArticuloId { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Codigo { get; set; }
        public double Precio { get; set; }

        public IEnumerable<Articulo>? Articulos { get; set; }
    }
}