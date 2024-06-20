using DataAccess;
using Domain;

namespace Services
{
    public class ServicioLogin : IServicioLogin
    {
        private readonly IRepositorioLogin _repositorio;
        public ServicioLogin(IRepositorioLogin repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<string?> Login(Credenciales c)
        {
            return await _repositorio.Login(c);
        }
    }
}