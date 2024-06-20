namespace DataAccess
{
    public interface IRepositorio<T> where T : class
    {
        Task Crear(T entidad);
        Task Actualizar(int id, T entidad);
        Task Borrar(int id);
        Task<T> GetPorId(int id);
        Task<IEnumerable<T>> GetAll();
    }
}