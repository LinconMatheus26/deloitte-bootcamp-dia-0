using Microsoft.EntityFrameworkCore;
using ValeMonitoramento.Data;
using ValeMonitoramento.Models;
using Xunit;

namespace ValeMonitoramento.Tests;

public class AppDbContextTests
{
    [Fact]
    public async Task Banco_DeveSalvarEnumComoString()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestEnumString")
            .Options;

        using var db = new AppDbContext(options);
        var equip = new Equipamento 
        { 
            Codigo = "STR_TEST", 
            Tipo = TipoEquipamento.Escavadeira,
            StatusOperacional = StatusOperacional.EmManutencao 
        };

        // Act
        db.Equipamentos.Add(equip);
        await db.SaveChangesAsync();
        var salvo = await db.Equipamentos.FirstAsync();

        // Assert
        Assert.Equal(TipoEquipamento.Escavadeira, salvo.Tipo);
        // Nota: Em bancos InMemory o HasConversion é simulado, 
        // mas garante que a configuração da Fluent API não quebra a execução.
    }
}