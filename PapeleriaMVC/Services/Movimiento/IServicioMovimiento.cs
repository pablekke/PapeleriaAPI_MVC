using Domain;
using Domain.DTOs;

namespace Services
{
    public interface IServicioMovimiento : IServicio<Movimiento>
    {
        Task<IEnumerable<Movimiento>> GetMovimientos();
        Task<IEnumerable<Movimiento>> GetMovimientosPorArticuloYTipo(int articuloId, int tipoMovimientoId);
        Task<IEnumerable<Movimiento>> GetMovimientosPorFecha(DateTime fechaInicio, DateTime fechaFin);
        Task<IEnumerable<ResumenStock>> GetResumenCantidadesMovidas();
        Task ModificarTope(int tope);
        Task<int> GetTope();
    }
}