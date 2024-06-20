using Domain.DTOs;
using Domain.Modelos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class RepositorioStockArticulo : Repositorio<StockArticulo>, IRepositorioStockArticulo
    {
        private readonly DbSet<StockArticulo> Stock;

        public RepositorioStockArticulo(DbContext contexto) : base(contexto)
        {
            Stock = contexto.Set<StockArticulo>();
        }

        public int[] MovimientosEnUso()
        {
            return Stock.Select(ms => ms.TipoMovimientoId).Distinct().ToArray();
        }
        public List<StockArticulo> GetStocksDetallados()
        {
            return Stock
                .Include(p => p.Articulo)
                .Include(p => p.TipoMovimiento)
                .OrderByDescending(p => p.Fecha)
                .ToList();
        }

        public StockArticulo GetStockDetallado(int id)
        {
            var mov = GetPorId(id);

            if (mov != null)
            {
                mov = Stock
                    .Include(p => p.Articulo)
                    .Include(p => p.TipoMovimiento)
                    .FirstOrDefault(p => p.MovimientoId == p.TipoMovimientoId);
            }

            return mov;
        }

        public IEnumerable<StockArticulo> GetMovimientosPorArticuloYTipo(int articuloId, int tipoMovimientoId)
        {
            return Stock
                .Include(m => m.Articulo)
                .Include(m => m.TipoMovimiento)
                .Where(m => m.ArticuloId == articuloId && m.TipoMovimientoId == tipoMovimientoId)
                .OrderByDescending(m => m.Fecha)
                .ThenBy(m => m.CantidadMovida)
                .ToList();
        }

        public IEnumerable<StockArticulo> GetMovimientosPorRangoFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            fechaInicio = fechaInicio.Date;
            fechaFin = fechaFin.Date.AddDays(1).AddTicks(-1);

            return Stock
                .Include(m => m.Articulo)
                .Include(m => m.TipoMovimiento)
                .Where(m => m.Fecha >= fechaInicio && m.Fecha <= fechaFin)
                .OrderByDescending(m => m.Fecha)
                .ThenBy(m => m.CantidadMovida)
                .ToList();
        }
        public IEnumerable<ResumenStock> GetResumenCantidadesMovidas()
        {
            return Stock
                .GroupBy(m => new { Año = m.Fecha.Year, TipoId = m.TipoMovimientoId, TipoNombre = m.TipoMovimiento.Nombre })
                .Select(g => new ResumenStock
                {
                    Año = g.Key.Año,
                    TipoMovimientoId = g.Key.TipoId,
                    TipoMovimientoNombre = g.Key.TipoNombre,
                    Cantidad = g.Sum(m => m.CantidadMovida)
                })
                .OrderBy(g => g.Año)
                .ThenBy(g => g.TipoMovimientoNombre)
                .ToList();
        }
    }
}