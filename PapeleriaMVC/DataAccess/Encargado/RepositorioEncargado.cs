using Domain;
using Microsoft.AspNetCore.Http;

namespace DataAccess
{
    public class RepositorioEncargado : Repositorio<Encargado>, IRepositorioEncargado
    {
        public RepositorioEncargado(string apiUrl, IHttpContextAccessor httpContextAccessor)
            : base(apiUrl, httpContextAccessor)
        {
        }
    }
}