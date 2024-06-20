using Domain.DTOs;

namespace Presentation
{
    public class EncargadoUpdate
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }

        public EncargadoDTO ToDTO()
        {
            return new EncargadoDTO
            {
                Nombre = Nombre,
                Apellido = Apellido,
                Email = Email,
            };
        }
    }
}