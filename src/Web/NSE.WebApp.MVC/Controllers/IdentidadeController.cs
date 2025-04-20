using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NSE.WebApp.MVC.Controllers
{
    public class IdentidadeController : BaseController
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public IdentidadeController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [HttpGet]
        [Route("registro")]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> Registro(UsuarioRegistro usuarioLogin)
        {
            if (!ModelState.IsValid) return View(usuarioLogin);

            var response = await _autenticacaoService.Registro(usuarioLogin);

            if (ResponsePossuiErros(response.ErrorResponseResult)) return View(usuarioLogin);

            await RealizarLogin(response);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid) return View(usuarioLogin);

            var response = await _autenticacaoService.Login(usuarioLogin);

            if (ResponsePossuiErros(response.ErrorResponseResult)) return View(usuarioLogin);

            await RealizarLogin(response);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("sair")]
        public async Task<IActionResult> Logout()
        {
            await RealizarLogout();
            return RedirectToAction("Index", "Home");
        }

        private async Task RealizarLogin(UsuarioRespostaLogin resposta)
        {
            var token = ObterTokenFormatado(resposta.AccessToken);
            var claims = new List<Claim>();
            claims.Add(new Claim("jwt", resposta.AccessToken));
            claims.AddRange(token.Claims);

            // Representação das claims do usuário
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Armazenar valores da sessão do usuário
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(60), // Tempo de expiração do cookie
                IsPersistent = true // Persiste para múltiplas requisições durante o tempo de expiração do cookie
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        private async Task RealizarLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private static JwtSecurityToken ObterTokenFormatado(string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
        }
    }
}
