using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValeMonitoramento.Controllers;
using ValeMonitoramento.Data;
using ValeMonitoramento.Models;
using ValeMonitoramento.Dtos;
using Xunit;

namespace ValeMonitoramento.Tests;

public class EquipamentosControllerTests
{
    // Método auxiliar para configurar o banco de dados em memória
    private AppDbContext GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        return new AppDbContext(options);
    }

    [Fact]
    public async Task Create_DeveRetornarConflict_QuandoCodigoJaExiste()
    {
        // Arrange
        var db = GetDatabaseContext();
        db.Equipamentos.Add(new Equipamento { Codigo = "VALE01", Modelo = "Teste", Tipo = TipoEquipamento.Caminhao });
        await db.SaveChangesAsync();

        var controller = new EquipamentosController(db);
        var dtoDuplicado = new EquipamentoCreateDto { Codigo = "VALE01", Modelo = "Novo" };

        // Act
        var result = await controller.Create(dtoDuplicado);

        // Assert
        Assert.IsType<ConflictObjectResult>(result);
    }

    [Fact]
    public async Task GetById_DeveRetornarNotFound_QuandoIdNaoExiste()
    {
        // Arrange
        var db = GetDatabaseContext();
        var controller = new EquipamentosController(db);

        // Act
        var result = await controller.GetById(999);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}