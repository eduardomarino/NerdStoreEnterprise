using NSE.WebApp.MVC.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Carregar configurações iniciais
builder.Configuration.AddMvcConfigurationSources(builder.Environment);

// Add services to the container.
builder.Services.AddCookieConfiguration();
builder.Services.AddMvcConfiguration();

builder.Services.RegisterServices();

var app = builder.Build();

app.UseMvcConfiguration(app.Environment);

app.Run();
