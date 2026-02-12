using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using MinhaApi.Queues; // Importante para reconhecer a nova pasta
using StackExchange.Redis; // Adicionado para suporte ao Worker

var builder = WebApplication.CreateBuilder(args);

// --- 1. REGISTRO DE SERVIÇOS (Configuração) ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// REGISTRO DA QUEUE: Essencial para o Controller conseguir usar o RedisQueueService
builder.Services.AddScoped<RedisQueueService>();
builder.Services.AddHostedService<LoteQueueWorker>();

// Registro do Redis (Apenas se não for ambiente de Teste)
if (builder.Environment.EnvironmentName != "Testing")
{
    var redisConnection = builder.Configuration.GetConnectionString("RedisConnection");

    // Registro para o IDistributedCache (usado pelo RedisQueueService)
    builder.Services.AddStackExchangeRedisCache(options => {
        options.Configuration = redisConnection;
    });

    // Registro do Multiplexer (Essencial para o Worker conseguir listar chaves)
    builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnection!));
}

// Registro do Postgres (Apenas se não for ambiente de Teste)
if (builder.Environment.EnvironmentName != "Testing")
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
}

// --- 2. CONSTRUÇÃO DO APP ---
var app = builder.Build();

// --- 3. PIPELINE DE REQUISIÇÕES (Middlewares) ---

// Habilita Swagger em Desenvolvimento ou Testes para validar a cobertura
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Testing"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();

// Necessário para o WebApplicationFactory dos testes
public partial class Program { }