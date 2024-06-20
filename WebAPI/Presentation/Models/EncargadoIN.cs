using Domain.DTOs;

namespace Presentation
{
    public class EncargadoIN
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }

        public EncargadoDTO ToDTO()
        {
            return new EncargadoDTO
            {
                EncargadoId = 0,
                Nombre = Nombre,
                Apellido = Apellido,
                Email = Email,
                Contraseña = Contraseña
            };
        }
    }
}