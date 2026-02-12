using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;
using MinhaApi; // Referência para o namespace da sua API

namespace MinhaApi.tests
{
    /// <summary>
    /// Teste de integração para validar a inicialização da API (Program.cs).
    /// Utilizamos a CustomWebApplicationFactory para evitar dependência do PostgreSQL real.
    /// </summary>
    public class ProgramTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public ProgramTest(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task App_DeveSubirEndpointPrincipalComSucesso()
        {
            // Arrange - Cria o cliente HTTP simulado
            var client = _factory.CreateClient();

            // Act - Tenta acessar o endpoint principal da API
            var response = await client.GetAsync("/api/LotesMinerio");

            // Assert - Verifica se a aplicação respondeu 200 OK
            // O uso do banco em memória garante que não teremos erro de conexão (500)
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Swagger_DeveEstarDisponivelEmDesenvolvimento()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act - Verifica se a rota do Swagger está respondendo
            var response = await client.GetAsync("/swagger/index.html");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}