using Domain.DTOs;

namespace Presentation
{
    public class Articulo
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public double Precio { get; set; }

        public ArticuloDTO ToDTO() {
            return new ArticuloDTO()
            {
                ArticuloId = 0,
                Nombre = Nombre,
                Descripcion = Descripcion,
                Codigo = Codigo,
                Precio = Precio
            };
        }
    }
}
