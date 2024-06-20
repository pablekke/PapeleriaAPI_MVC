using Domain.DTOs;
using Domain.Modelos;

namespace DataAccess
{
    public interface IRepositorioStockArticulo : IRepositorio<StockArticulo>
    {
        int[]? MovimientosEnUso();
        List<StockArticulo> GetStocksDetallados();
        StockArticulo GetStockDetallado(int id);
        IEnumerable<StockArticulo> GetMovimientosPorArticuloYTipo(int articuloId, int tipoMovimientoId);
        IEnumerable<StockArticulo> GetMovimientosPorRangoFechas(DateTime fechaInicio, DateTime fechaFin);
        IEnumerable<ResumenStock> GetResumenCantidadesMovidas();
    }
}