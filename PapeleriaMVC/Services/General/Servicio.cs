using DataAccess;

namespace Services
{
    public class Servicio<Modelo> : IServicio<Modelo> where Modelo : class
    {
        protected IRepositorio<Modelo> _repositorio;
        public Servicio(IRepositorio<Modelo> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task Crear(Modelo modelo)
        {
            await _repositorio.Crear(modelo);
        }

        public async Task Borrar(int id)
        {
            await _repositorio.Borrar(id);
        }

        public async Task Actualizar(int id, Modelo modelo)
        {
            await _repositorio.Actualizar(id, modelo);
        }

        public async Task<Modelo> GetPorId(int id)
        {
            return await _repositorio.GetPorId(id);
        }

        public async Task<IEnumerable<Modelo>> GetAll()
        {
            return await _repositorio.GetAll();
        }
    }
}