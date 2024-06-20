namespace DataAccess
{
    public interface IRepositorio<T>
    {
        void Crear(T entidad);
        void Borrar(int id);
        void Actualizar(T entidad);
        T GetPorId(int id);
        List<T> GetAll();
    }
}