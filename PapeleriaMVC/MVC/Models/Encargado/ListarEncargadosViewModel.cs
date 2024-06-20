using Domain;

namespace MVC.Models
{
    public class ListarEncargadosViewModel
    {
        public int EncargadoId { get; set; }
        public string Nombre { get; set;}
        public string Apellido { get; set;}
        public string Email { get; set;}

        public IEnumerable<Encargado> Encargados { get; set; }
    }
}