using Domain.DTOs;

namespace Services
{
    public interface IServicioArticulo : IServicio<ArticuloDTO>
    {
        IEnumerable<ArticuloDTO> GetArticulosFiltrados(string nombre, double monto);
    }
}