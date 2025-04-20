using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Controllers
{
    public class BaseController : Controller
    {
        protected bool ResponsePossuiErros(ErrorResponseResult errorResponseResult)
        {
            if (errorResponseResult == null || errorResponseResult.Errors == null || !errorResponseResult.Errors.Mensagens.Any())
                return false;
            foreach (var mensagem in errorResponseResult.Errors.Mensagens)
            {
                ModelState.AddModelError(string.Empty, mensagem);
            }
            return true;
        }
    }
}
