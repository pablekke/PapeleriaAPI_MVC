using Domain.DTOs;

namespace Services
{
    public interface IServicioEncargado : IServicio<EncargadoDTO>
    {
        EncargadoDTO? ExisteEncargado(string email, string contraseña);
        bool ExisteEmail(string email);
    }
}