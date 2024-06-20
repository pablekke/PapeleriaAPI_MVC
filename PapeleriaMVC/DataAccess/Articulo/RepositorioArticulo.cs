using Domain;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace DataAccess
{
    public class RepositorioArticulo : Repositorio<Articulo>, IRepositorioArticulo
    {
        public RepositorioArticulo(string apiUrl, IHttpContextAccessor httpContextAccessor)
            : base(apiUrl, httpContextAccessor)
        {
        }

        public async Task<IEnumerable<Articulo>?> GetFiltrados(string nombre, double monto)
        {
            AgregarAutorizacion();
            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiUrl}/filtrados?nombre={nombre}&monto={monto}");
            await HandleResponseErrors(response);
            return await response.Content.ReadFromJsonAsync<IEnumerable<Articulo>>();
        }
    }
}