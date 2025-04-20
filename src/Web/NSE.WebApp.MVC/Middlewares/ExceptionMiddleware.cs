using NSE.WebApp.MVC.Extensions;
using Polly.CircuitBreaker;
using Refit;
using System.Net;

namespace NSE.WebApp.MVC.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            //catch (CustomHttpRequestException ex)
            //{
            //    HandleRequestExceptionAsync(httpContext, ex);
            //}
            catch (CustomHttpRequestException ex)
            {
                HandleRequestExceptionAsync(httpContext, ex.StatusCode);
            }
            catch (ValidationApiException ex) // Exceção do Refit para erros de validação, como 403, 400
            {
                HandleRequestExceptionAsync(httpContext, ex.StatusCode);
            }
            catch (ApiException ex) // Exceção do Refit para outros erros de API, como 401, 500
            {
                HandleRequestExceptionAsync(httpContext, ex.StatusCode);
            }
            catch (BrokenCircuitException)
            {
                HandleCircuitBreakerExceptionAsync(httpContext);
            }
        }

        //private static void HandleRequestExceptionAsync(HttpContext context, CustomHttpRequestException httpRequestException)
        //{
        //    if (httpRequestException.StatusCode == HttpStatusCode.Unauthorized)
        //    {
        //        context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}"); // Guarda a URL original para redirecionar após o login
        //        return;
        //    }

        //    context.Response.StatusCode = (int)httpRequestException.StatusCode;
        //}

        private static void HandleRequestExceptionAsync(HttpContext context, HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}"); // Guarda a URL original para redirecionar após o login
                return;
            }

            context.Response.StatusCode = (int)statusCode;
        }

        private static void HandleCircuitBreakerExceptionAsync(HttpContext context)
        {
            context.Response.Redirect("/sistema-indisponivel");
        }
    }
}
