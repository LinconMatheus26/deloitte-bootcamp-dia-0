using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MinhaApi.Data;
using System.Linq;

namespace MinhaApi.tests
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Define o ambiente como Testing para pular a configuração do Postgres real no Program.cs
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                // Remove qualquer registro prévio do DbContext (limpeza de segurança)
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Configura o Banco em Memória para os Testes
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("IntegrationTestDb");
                });

                // --- AJUSTE PARA O REDIS ---
                // Substitui o Redis real (Docker) por um cache em memória RAM local
                // Isso evita erros de conexão durante os testes e aumenta a cobertura
                services.AddDistributedMemoryCache(); 
            });
        }
    }
}