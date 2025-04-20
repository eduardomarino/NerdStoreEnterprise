using NSE.Catalogo.API.Configurations;
using NSE.WebAPI.Core.Identidade;

var builder = WebApplication.CreateBuilder(args);

// Carregar configurações iniciais
builder.AddConfigurationSources();

// Configurações de serviços
builder.AddDbContext();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.AddControllers();
builder.AddSwagger();

builder.Services.RegisterServices();

var app = builder.Build();

// Configuração do pipeline HTTP
app.UseApiConfiguration(app.Environment);
app.UseSwagger(app.Environment);

app.Run();
