namespace Services
{
    public interface IServicio<DTO>
    {
        void Crear(DTO dto);
        void Actualizar(int id, DTO dto);
        void Borrar(int id);
        DTO GetPorId(int id);
        List<DTO> GetAll();
    }
}