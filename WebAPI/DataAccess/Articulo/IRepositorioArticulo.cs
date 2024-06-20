using Domain.Modelos;

namespace DataAccess
{
    public interface IRepositorioArticulo : IRepositorio<Articulo>
    {
        IEnumerable<Articulo> GetArticulosFiltrados(string? nombre, double monto);
        bool ExisteCodigo(string? codigo);
        bool ExisteNombre(string? nombre);
    }
}