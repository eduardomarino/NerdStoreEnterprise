using NSE.Identidade.API.Models;

namespace NSE.Identidade.API.Services
{
    public interface IJwtService
    {
        Task<UsuarioRespostaLogin> GerarJwt(string email);
    }
}
