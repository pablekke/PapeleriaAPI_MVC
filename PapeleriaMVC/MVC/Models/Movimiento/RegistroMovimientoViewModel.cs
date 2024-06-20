using Domain;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class RegistroMovimientoViewModel
    {
        public int MovimientoId { get; set; }
        [Required(ErrorMessage = "El artículo es obligatorio.")]
        public int ArticuloId { get; set; }

        [Required(ErrorMessage = "El tipo de movimiento es obligatorio.")]
        public int TipoMovimientoId { get; set; }
        public string EmailEncargado { get; set; }

        [Required(ErrorMessage = "La cantidad movida es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
        public int CantidadMovida { get; set; }

        public IEnumerable<Articulo> Articulos { get; set; }
        public IEnumerable<TipoMovimiento> Tipos { get; set; }

        public RegistroMovimientoViewModel() { }

        public RegistroMovimientoViewModel(Movimiento a)
        {
            ArticuloId = a.ArticuloId;
            TipoMovimientoId = a.TipoMovimientoId;
            CantidadMovida = a.CantidadMovida;
        }

        // Método para crear un  desde el ViewModel
        public Movimiento ToDTO()
        {
            return new Movimiento()
            {
                ArticuloId = ArticuloId,
                TipoMovimientoId = TipoMovimientoId,
                EmailEncargado = EmailEncargado,
                CantidadMovida = CantidadMovida,
            };
        }
    }
}
