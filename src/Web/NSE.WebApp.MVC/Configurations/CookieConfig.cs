using Microsoft.AspNetCore.Authentication.Cookies;

namespace NSE.WebApp.MVC.Configurations
{
    public static class CookieConfig
    {
        public static void AddCookieConfiguration(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/login";
                    options.AccessDeniedPath = "/acesso-negado";
                });
        }
    }
}
