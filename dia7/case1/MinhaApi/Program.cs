<<<<<<< HEAD
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
=======
using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;

// Correção para o erro de TIMESTAMPTZ do PostgreSQL
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte aos Controllers e OpenAPI
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Configura a conexão com o PostgreSQL usando a string do appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

>>>>>>> 5fce523 (Salvando progresso da API na main)
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

<<<<<<< HEAD
app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
=======
// Mapeia as rotas dos seus Controllers (Essencial para não dar erro de conexão)
app.MapControllers();

app.Run();
>>>>>>> 5fce523 (Salvando progresso da API na main)
