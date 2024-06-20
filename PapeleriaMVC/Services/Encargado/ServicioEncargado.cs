using DataAccess;
using Domain;

namespace Services
{
    public class ServicioEncargado : Servicio<Encargado> , IServicioEncargado
    {
        private readonly IRepositorioEncargado _repositorio;
        public ServicioEncargado(IRepositorioEncargado repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
        }
    }
}