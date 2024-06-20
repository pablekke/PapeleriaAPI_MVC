using Domain;
using Microsoft.AspNetCore.Http;

namespace DataAccess
{
    public class RepositorioTipoMovimiento : Repositorio<TipoMovimiento>, IRepositorioTipoMovimiento
    {
        public RepositorioTipoMovimiento(string apiUrl, IHttpContextAccessor httpContextAccessor) 
            : base(apiUrl, httpContextAccessor)
        {
        }
    }
}