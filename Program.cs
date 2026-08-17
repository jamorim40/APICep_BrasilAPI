using APICep.Clients;
using APICep.Middlewares;
using APICep.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<ICepService, CepService>();
builder.Services.AddHttpClient<IBrasilApiClient, BrasilApiClient>(client =>
{
    var baseUrl = builder.Configuration["BrasilApi:BaseUrl"];
    client.BaseAddress = new Uri(baseUrl!);
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
