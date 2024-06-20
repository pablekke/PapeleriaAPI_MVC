using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Presentation.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace DataAccess
{
    public class RepositorioMovimiento : Repositorio<Movimiento>, IRepositorioMovimiento
    {
        public RepositorioMovimiento(string apiUrl, IHttpContextAccessor httpContextAccessor)
            : base(apiUrl, httpContextAccessor)
        {
        }

        public async Task<IEnumerable<Movimiento>> GetMovimientos()
        {
            AgregarAutorizacion();
            HttpResponseMessage response = await _httpClient.GetAsync(_apiUrl);
            await HandleResponseErrors(response);

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<Movimiento>>();
            return apiResponse.Movimientos;
        }
        public async Task<IEnumerable<Movimiento>> GetMovimientosPorArticuloYTipo(int articuloId, int tipoMovimientoId)
        {
            AgregarAutorizacion();
            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiUrl}/articulo/{articuloId}/tipo/{tipoMovimientoId}");
            await HandleResponseErrors(response);

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<Movimiento>>();
            return apiResponse.Movimientos;
        }

        public async Task<IEnumerable<Movimiento>> GetMovimientosPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            AgregarAutorizacion();
            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiUrl}/fechas?fechaInicio={fechaInicio.Year}-{fechaInicio.Month}-{fechaInicio.Day}&fechaFin={fechaFin.Year}-{fechaFin.Month}-{fechaFin.Day}");
            await HandleResponseErrors(response);

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<Movimiento>>();
            return apiResponse.Movimientos;
        }

        public async Task<IEnumerable<ResumenStock>> GetResumenCantidadesMovidas()
        {
            AgregarAutorizacion();
            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiUrl}/resumen");
            await HandleResponseErrors(response);
            return await response.Content.ReadFromJsonAsync<IEnumerable<ResumenStock>>();
        }

        public async Task<int> GetTope()
        {
            AgregarAutorizacion();
            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiUrl}/tope");
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task ModificarTope(int tope)
        {
            var entidad = new TopeOut() { Tope = tope };
            string entidadJson = JsonSerializer.Serialize(entidad);
            AgregarAutorizacion();
            var content = CrearContenido(entidadJson);
            HttpResponseMessage response = await _httpClient.PutAsync($"{_apiUrl}/tope", content);
            await HandleResponseErrors(response);
        }
    }
    public class ApiResponse<Movimiento>
    {
        public int Total { get; set; }
        public IEnumerable<Movimiento> Movimientos { get; set; }
    }
}