using Microsoft.EntityFrameworkCore;
using ValeMonitoramento.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configuração unificada de Controllers e JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Converte Enums em Strings nas respostas e aceita Strings nas entradas
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        
        // Formatação amigável para leitura
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do Banco de Dados PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Bloco para aplicar migrações automaticamente ao iniciar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configuração do Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();