using Domain;

namespace DataAccess
{
    public interface IRepositorioLogin
    {
        Task<string?> Login(Credenciales credenciales);
    }
}