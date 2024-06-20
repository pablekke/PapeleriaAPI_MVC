using Domain.DTOs;

namespace Services
{
    public interface IServicioTipoMovimiento : IServicio<TipoMovimientoDTO>
    {
        bool ExisteNombre(string nombre);
    }
}