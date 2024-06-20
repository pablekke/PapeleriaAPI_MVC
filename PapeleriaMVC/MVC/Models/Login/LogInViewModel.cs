using Domain;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debés proporcionar una dirección de email válida.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }

        public Credenciales ToDTO() {
            return new Credenciales() {
                Email = Email,
                Contraseña = Contraseña
            };
        }
        
    }
}
