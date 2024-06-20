using Domain.DTOs;

namespace Services
{
    public interface IServicioStockArticulo : IServicio<StockArticuloDTO>
    {
        List<StockArticuloDTO> GetStocksDetallados();
        StockArticuloDTO GetStockDetallado(int id);
        IEnumerable<StockArticuloDTO> GetMovimientosPorArticuloYTipo(int articuloId, int tipoMovimientoId);
        IEnumerable<StockArticuloDTO> GetMovimientosPorRangoFechas(DateTime fechaInicio, DateTime fechaFin);
        IEnumerable<ResumenStock> GetResumenCantidadesMovidas();
    }
}