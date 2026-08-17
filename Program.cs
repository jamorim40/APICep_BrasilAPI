using APICep.Clients;
using APICep.Configurations;
using APICep.Middlewares;
using APICep.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddMemoryCache();

// Registrar configurações da BrasilAPI
builder.Services.Configure<BrasilApiSettings>(
    builder.Configuration.GetSection("BrasilApi"));

builder.Services.AddScoped<ICepService, CepService>();
builder.Services.AddHttpClient<IBrasilApiClient, BrasilApiClient>(
    (serviceProvider, client) =>
    {
        var brasilApiSettings = serviceProvider.GetRequiredService<IOptions<BrasilApiSettings>>();
        client.BaseAddress = new Uri(brasilApiSettings.Value.BaseUrl);
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseMiddleware<ExcecaoMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
