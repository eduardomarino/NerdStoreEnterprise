using NSE.Identidade.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Carregar configurações iniciais
ApiConfig.AddConfigurationSources(builder);

// Configurações de serviços
ApiConfig.AddDbContext(builder);
IdentityConfig.AddIdentity(builder);
IdentityConfig.AddJwtConfiguration(builder);
ApiConfig.AddControllers(builder);
SwaggerConfig.AddSwagger(builder);

builder.Services.RegisterServices();

var app = builder.Build();

// Configuração do pipeline HTTP
ApiConfig.UseApiConfiguration(app);
SwaggerConfig.UseSwagger(app);

app.Run();
