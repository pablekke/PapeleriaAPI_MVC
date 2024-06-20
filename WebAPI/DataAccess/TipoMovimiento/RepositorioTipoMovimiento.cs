using Domain.Modelos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class RepositorioTipoMovimiento : Repositorio<TipoMovimiento>, IRepositorioTipoMovimiento
    {
        private readonly IRepositorioStockArticulo _repositorioStock;
        private readonly DbSet<TipoMovimiento> TiposMovimiento;

        public RepositorioTipoMovimiento(DbContext contexto, IRepositorioStockArticulo repositorioStock) : base(contexto)
        {
            TiposMovimiento = _contexto.Set<TipoMovimiento>();
            _repositorioStock = repositorioStock;
        }

        public bool ExisteNombre(string nombre)
        {
            return TiposMovimiento.Any(t => t.Nombre == nombre);
        }

        public bool SePuedeBorrar(int id)
        {
            int[]? movimientosEnUso = _repositorioStock.MovimientosEnUso();
            if (movimientosEnUso == null || movimientosEnUso.Length == 0)
            {
                return true;
            }
            return !movimientosEnUso.Contains(id);
        }
    }
}