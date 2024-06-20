namespace Services
{
    public interface IServicio<Modelo>
    {
        Task Crear(Modelo modelo);
        Task Borrar(int id);
        Task Actualizar(int id, Modelo modelo);
        Task<Modelo> GetPorId(int id);
        Task<IEnumerable<Modelo>> GetAll();
    }
}