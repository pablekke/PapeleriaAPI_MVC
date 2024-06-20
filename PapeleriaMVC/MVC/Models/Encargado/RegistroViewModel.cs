using Domain;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class RegistroViewModel
    {
        public int EncargadoId { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe proporcionar una dirección de email válida.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[.,;!])[A-Za-z\d.,;!]{6,}$")]
        public string Contraseña { get; set; }

        [Required(ErrorMessage = "La confirmación de contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [Compare("Contraseña", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarContraseña { get; set; }

        public RegistroViewModel() { }

        public RegistroViewModel(Encargado encargado)
        {
            Nombre = encargado.Nombre;
            Apellido = encargado.Apellido;
            Email = encargado.Email;
        }

        public Encargado ToDTO()
        {
            return new Encargado()
            {
                EncargadoId = EncargadoId,
                Nombre = Nombre,
                Apellido = Apellido,
                Email = Email,
                Contraseña = Contraseña
            };
        }
    }
}