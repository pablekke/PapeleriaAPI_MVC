using Domain;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DataAccess
{
    public class RepositorioLogin : IRepositorioLogin
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "";
        public RepositorioLogin(string apiUrl)
        {
            _apiUrl = apiUrl;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<string?> Login(Credenciales credenciales)
        {
            string credencialesJSON = JsonSerializer.Serialize(credenciales);
            StringContent contenido = new StringContent(credencialesJSON, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(_apiUrl, contenido);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, 
                WriteIndented = true
            };
            var loginResponse = JsonSerializer.Deserialize<LoginResponse>(responseBody, options);
            return loginResponse.Token;
        }
    }

    internal class LoginResponse
    {
        public string Token { get; set; }
    }
}