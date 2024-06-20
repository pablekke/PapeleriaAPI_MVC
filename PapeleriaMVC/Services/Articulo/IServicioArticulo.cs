using Domain;

namespace Services
{
    public interface IServicioArticulo : IServicio<Articulo>
    {
        Task<IEnumerable<Articulo>?> GetArticulosFiltrados(string nombre, double monto);
    }
}