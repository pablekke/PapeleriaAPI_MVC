using Domain;

namespace Services
{
    public interface IServicioLogin
    {
        Task<string?> Login(Credenciales c);
    }
}