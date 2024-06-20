using DataAccess;
using Domain;

namespace Services
{
    public class ServicioTipoMovimiento : Servicio<TipoMovimiento>, IServicioTipoMovimiento
    {
        public ServicioTipoMovimiento(IRepositorioTipoMovimiento repositorio) : base(repositorio)
        {
        }
    }
}