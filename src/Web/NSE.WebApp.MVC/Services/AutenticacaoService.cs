using NSE.WebApp.MVC.Models;
using System.Text.Json;

namespace NSE.WebApp.MVC.Services
{
    public class AutenticacaoService : BaseService, IAutenticacaoService
    {
        private readonly HttpClient _httpClient;
        public AutenticacaoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin)
        {
            var loginContent = new StringContent(JsonSerializer.Serialize(usuarioLogin),
                System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44363/api/identidade/acessos", loginContent);

            // Diferentemente do Newtonsoft.Json, o System.Text.Json é case-sensitive por padrão
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin
                {
                    ErrorResponseResult = JsonSerializer.Deserialize<ErrorResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };
            }

            return JsonSerializer.Deserialize<UsuarioRespostaLogin>(await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro)
        {
            var registroContent = new StringContent(JsonSerializer.Serialize(usuarioRegistro),
                System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44363/api/identidade/registros", registroContent);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin
                {
                    ErrorResponseResult = JsonSerializer.Deserialize<ErrorResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };
            }

            return JsonSerializer.Deserialize<UsuarioRespostaLogin>(await response.Content.ReadAsStringAsync(), options);
        }
    }
}
