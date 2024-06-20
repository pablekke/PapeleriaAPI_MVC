using DataAccess;
using Domain;

namespace Services
{
    public class ServicioArticulo : Servicio<Articulo>, IServicioArticulo
    {
        private readonly IRepositorioArticulo _repositorio;

        public ServicioArticulo(IRepositorioArticulo repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<Articulo>?> GetArticulosFiltrados(string nombre, double monto)
        {
            return await _repositorio.GetFiltrados(nombre, monto);
        }
    }
}