using Domain.Modelos;

namespace DataAccess
{
    public interface IRepositorioEncargado : IRepositorio<Encargado>
    {
        Encargado? ExisteEncargado(string email, string contraseña);
        bool ExisteEmail(string email);
    }
}