using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace DataAccess
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _apiUrl = "";
        protected readonly IHttpContextAccessor _httpContextAccessor;
        public Repositorio(string apiUrl, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
             _apiUrl = apiUrl;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected void AgregarAutorizacion()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetToken());
        }

        protected StringContent CrearContenido(string entidadJson)
        {
            return new StringContent(entidadJson, System.Text.Encoding.UTF8, "application/json");
        }

        public async Task Crear(T entidad)
        {
            string entidadJson = JsonSerializer.Serialize(entidad);
            AgregarAutorizacion();
            var content = CrearContenido(entidadJson);
            HttpResponseMessage response = await _httpClient.PostAsync(_apiUrl, content);
            await HandleResponseErrors(response);
        }

        public async Task Actualizar(int id, T entidad)
        {
            string entidadJson = JsonSerializer.Serialize(entidad);
            AgregarAutorizacion();
            var content = CrearContenido(entidadJson);
            HttpResponseMessage response = await _httpClient.PutAsync($"{_apiUrl}/{id}", content);
            await HandleResponseErrors(response);
        }

        public async Task Borrar(int id)
        {
            AgregarAutorizacion();
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{_apiUrl}/{id}");
            await HandleResponseErrors(response);
        }



        public async Task<T?> GetPorId(int id)
        {
            AgregarAutorizacion();
            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiUrl}/{id}");
            await HandleResponseErrors(response);
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<IEnumerable<T>?> GetAll()
        {
            AgregarAutorizacion();
            HttpResponseMessage response = await _httpClient.GetAsync(_apiUrl);
            await HandleResponseErrors(response);
            return await response.Content.ReadFromJsonAsync<IEnumerable<T>>();
        }

        protected string GetToken()
        {
            // Obtener la sesión del contexto actual
            ISession session = _httpContextAccessor.HttpContext.Session;
            return session.GetString("Token");
        }
        protected async Task HandleResponseErrors(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new ArgumentException(errorMessage);
            }
        }
    }
}