using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace MinhaApi.Queues; // Mantendo o namespace da sua pasta

public class LoteQueueWorker : BackgroundService
{
    private readonly ILogger<LoteQueueWorker> _logger;
    private readonly IConnectionMultiplexer _redis;

    public LoteQueueWorker(ILogger<LoteQueueWorker> logger, IConnectionMultiplexer redis)
    {
        _logger = logger;
        _redis = redis;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var db = _redis.GetDatabase();
        var server = _redis.GetServer(_redis.GetEndPoints().First());

        _logger.LogInformation(">>> [WORKER] Monitor iniciado. Modo de Visualização Ativo (Não deleta chaves).");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // Busca todas as chaves que começam com lotes conforme seu padrão
                var keys = server.Keys(pattern: "lotes*").ToList();

                foreach (var key in keys)
                {
                    // Lendo como Hash (padrão do .NET IDistributedCache)
                    var data = await db.HashGetAsync(key, "data");

                    if (!data.IsNull)
                    {
                        _logger.LogInformation($"[WORKER] Lote encontrado: {key}");
                        _logger.LogInformation($"[DADOS NO REDIS]: {data}");
                    }
                    else 
                    {
                        // Fallback para string simples se necessário
                        var value = await db.StringGetAsync(key);
                        if (!value.IsNull)
                            _logger.LogInformation($"[WORKER] Lote (String) encontrado: {key} | Valor: {value}");
                    }

                    // --- LINHA COMENTADA PARA VOCÊ VER NO REDIS INSIGHT ---
                    // await db.KeyDeleteAsync(key); 
                    
                    _logger.LogWarning($"[AVISO] A chave {key} FOI MANTIDA no Redis para você olhar no Redis Insight.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"[WORKER ERRO] Falha ao processar: {ex.Message}");
            }

            // Aguarda 5 segundos para a próxima verificação
            await Task.Delay(5000, stoppingToken);
        }
    }
}