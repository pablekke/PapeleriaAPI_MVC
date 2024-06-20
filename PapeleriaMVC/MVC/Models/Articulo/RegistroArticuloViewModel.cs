using System.ComponentModel.DataAnnotations;
using Domain;

namespace MVC.Models
{
    public class RegistroArticuloViewModel
    {
        public int ArticuloId { get; set; }

        [Required(ErrorMessage = "El nombre del artículo es obligatorio.")]
        [MinLength(10, ErrorMessage = "El nombre del artículo debe tener al menos 10 caracteres.")]
        [MaxLength(200, ErrorMessage = "El nombre del artículo no debe exceder los 200 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción del artículo es obligatoria.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El código del artículo es obligatorio.")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "El código del artículo debe tener exactamente 13 dígitos.")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El precio del artículo es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        public double Precio { get; set; }

        public RegistroArticuloViewModel() { }

        public RegistroArticuloViewModel(Articulo a)
        {
            Nombre = a.Nombre;
            Descripcion = a.Descripcion;
            Codigo = a.Codigo;
            Precio = a.Precio;
        }

        // Método para crear un  desde el ViewModel
        public Articulo ToDTO()
        {
            return new Articulo()
            {
                Nombre = Nombre,
                Descripcion = Descripcion,
                Codigo = Codigo,
                Precio = Precio
            };
        }
    }
}