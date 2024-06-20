using Domain;

namespace MVC.Models
{
    public class EditarEncargadoViewModel
    {
        public int EncargadoId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public EditarEncargadoViewModel()
        {
            
        }
        public EditarEncargadoViewModel(Encargado e)
        {
            EncargadoId = e.EncargadoId;
            Nombre = e.Nombre;
            Apellido = e.Apellido;
            Email = e.Email;
        }
        public Encargado ToDto()
        {
            return new Encargado
            {
                Nombre = Nombre,
                Apellido = Apellido,
                Email = Email,
            };
        }
    }
}