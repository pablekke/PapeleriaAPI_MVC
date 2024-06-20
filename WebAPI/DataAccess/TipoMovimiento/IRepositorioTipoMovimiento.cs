using Domain.Modelos;

namespace DataAccess
{
    public interface IRepositorioTipoMovimiento : IRepositorio<TipoMovimiento>
    {
        bool SePuedeBorrar(int id);
        bool ExisteNombre(string nombre);
    }
}