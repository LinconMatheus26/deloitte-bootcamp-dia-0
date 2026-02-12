using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MinhaApi.Queues;

public class RedisQueueService
{
    private readonly IDistributedCache _cache;
    public RedisQueueService(IDistributedCache cache) => _cache = cache;

    public async Task EnviarParaFila<T>(string filaNome, T dados)
    {
        var json = JsonSerializer.Serialize(dados);
        
        // Define quanto tempo o dado fica no Redis (Ex: 30 minutos)
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
        };

        // Salva com o tempo de expiração definido
        await _cache.SetStringAsync($"{filaNome}:{DateTime.UtcNow.Ticks}", json, options);
    }
}