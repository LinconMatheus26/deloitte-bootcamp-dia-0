using System.ComponentModel.DataAnnotations;
using ValeMonitoramento.Dtos;
using ValeMonitoramento.Models;
using Xunit;

namespace ValeMonitoramento.Tests;

public class EquipamentoCreateDtoTests
{
    [Fact]
    public void Dto_DeveTerErro_QuandoHorimetroForNegativo()
    {
        // Arrange
        var dto = new EquipamentoCreateDto 
        { 
            Codigo = "TESTE", 
            Horimetro = -10.5m, // Valor inválido
            Modelo = "Modelo X" 
        };

        // Act
        var context = new ValidationContext(dto);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(dto, context, results, true);

        // Assert
        Assert.False(isValid);
       Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("não pode ser negativo"));
    }

    [Fact]
    public void Dto_DeveSerValido_QuandoDadosEstaoCorretos()
    {
        // Arrange
        var dto = new EquipamentoCreateDto 
        { 
            Codigo = "VALE01", 
            Modelo = "CAT 793", 
            Horimetro = 100,
            Tipo = TipoEquipamento.Caminhao,
            StatusOperacional = StatusOperacional.Operacional,
            DataAquisicao = new DateOnly(2026, 02, 20),
            LocalizacaoAtual = "Mina"
        };

        // Act
        var context = new ValidationContext(dto);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(dto, context, results, true);

        // Assert
        Assert.True(isValid);
    }
}