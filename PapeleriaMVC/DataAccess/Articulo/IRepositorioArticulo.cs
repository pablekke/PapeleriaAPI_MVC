using Domain;

namespace DataAccess
{
    public interface IRepositorioArticulo : IRepositorio<Articulo>
    {
        Task<IEnumerable<Articulo>?> GetFiltrados(string nombre, double monto);
    }
}