using ValeMonitoramento.Models;
using Xunit;

namespace ValeMonitoramento.Tests.Models;

public class EquipamentoTests
{
    [Fact]
    public void Equipamento_DeveAtribuirPropriedadesCorretamente()
    {
        // Arrange (Preparação)
        var dataAquisicao = new DateOnly(2026, 2, 20);
        
        // Act (Ação)
        var equipamento = new Equipamento
        {
            Id = 1,
            Codigo = "VALE-001",
            Tipo = TipoEquipamento.Caminhao,
            Modelo = "Caterpillar 793F",
            Horimetro = 1500.50m,
            StatusOperacional = StatusOperacional.Operacional,
            DataAquisicao = dataAquisicao,
            LocalizacaoAtual = "Mina Norte"
        };

        // Assert (Verificação)
        Assert.Equal(1, equipamento.Id);
        Assert.Equal("VALE-001", equipamento.Codigo);
        Assert.Equal(TipoEquipamento.Caminhao, equipamento.Tipo);
        Assert.Equal(1500.50m, equipamento.Horimetro);
        Assert.Equal(dataAquisicao, equipamento.DataAquisicao);
    }
}