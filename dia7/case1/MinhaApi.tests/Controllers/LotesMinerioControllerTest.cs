using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using MinhaApi.Controllers;
using MinhaApi.Models;
using MinhaApi.Data;
using MinhaApi.Dtos;
using MinhaApi.Queues;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Distributed;

namespace MinhaApi.tests.Controllers
{
    public class LotesMinerioControllerTest
    {
        // Método auxiliar para configurar o banco e o cache em memória para os testes
        private (AppDbContext context, RedisQueueService queue) GetDependencies()
        {
            var services = new ServiceCollection();

            // 1. Configura Banco de Dados em Memória
            services.AddDbContext<AppDbContext>(opt => 
                opt.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()));

            // 2. Configura Cache em Memória (Simula o Redis do Docker)
            services.AddDistributedMemoryCache(); 
            services.AddScoped<RedisQueueService>();

            var serviceProvider = services.BuildServiceProvider();

            return (
                serviceProvider.GetRequiredService<AppDbContext>(),
                serviceProvider.GetRequiredService<RedisQueueService>()
            );
        }

        [Fact]
        public async Task GetAll_DeveRetornarOk()
        {
            var (context, queue) = GetDependencies();
            context.LotesMinerio.Add(new LoteMinerio { Id = 1, CodigoLote = "L01", MinaOrigem = "Mina Norte" });
            await context.SaveChangesAsync();

            var controller = new LotesMinerioController(context, queue);
            var result = await controller.GetAll();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Create_DeveSalvarNoBancoEEnviarParaFilaRedis()
        {
            // Arrange
            var (context, queue) = GetDependencies();
            var controller = new LotesMinerioController(context, queue);
            var dto = new CreateLoteMinerioDto 
            { 
                CodigoLote = "TESTE-REDIS", 
                TeorFe = 65, 
                Status = 1,
                MinaOrigem = "Mina Sul",
                Toneladas = 100
            };

            // Act
            var result = await controller.Create(dto);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
            var salvo = await context.LotesMinerio.AnyAsync(x => x.CodigoLote == "TESTE-REDIS");
            Assert.True(salvo); // Verifica se gravou no banco
        }

        [Fact]
        public async Task Update_DeveRetornarOk_E_EnviarParaFila()
        {
            // Arrange
            var (context, queue) = GetDependencies();
            var lote = new LoteMinerio { Id = 10, CodigoLote = "L10", MinaOrigem = "Original" };
            context.LotesMinerio.Add(lote);
            await context.SaveChangesAsync();
            
            var controller = new LotesMinerioController(context, queue);
            var dto = new CreateLoteMinerioDto { MinaOrigem = "Atualizada", Status = 1 };

            // Act
            var result = await controller.Update(10, dto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var atualizado = await context.LotesMinerio.FindAsync(10);
            Assert.Equal("Atualizada", atualizado!.MinaOrigem);
        }

        [Fact]
        public async Task Delete_DeveRetornarNoContent_QuandoExcluir()
        {
            // Arrange
            var (context, queue) = GetDependencies();
            context.LotesMinerio.Add(new LoteMinerio { Id = 50, CodigoLote = "L50" });
            await context.SaveChangesAsync();
            var controller = new LotesMinerioController(context, queue);

            // Act
            var result = await controller.Delete(50);

            // Assert
            Assert.IsType<NoContentResult>(result);
            var excluido = await context.LotesMinerio.FindAsync(50);
            Assert.Null(excluido);
        }
    }
}