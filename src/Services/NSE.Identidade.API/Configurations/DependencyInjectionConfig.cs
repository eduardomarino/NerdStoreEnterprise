using NSE.Identidade.API.Services;

namespace NSE.Identidade.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
        }
    }
}
