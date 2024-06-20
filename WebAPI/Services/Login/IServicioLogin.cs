using Domain.DTOs;

namespace Services
{
    public interface IServicioLogin
    {
        string Login(Credenciales c);
    }
}