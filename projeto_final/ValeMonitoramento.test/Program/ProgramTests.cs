using System.Text.Json;
using System.Text.Json.Serialization;
using ValeMonitoramento.Models;
using Xunit;

namespace ValeMonitoramento.Tests.Configuration;

public class ProgramTests
{
    [Fact]
    public void JsonSerializer_DeveConverterEnumParaString_ConformeConfiguradoNoProgram()
    {
        // Arrange
        // Simulamos a configuração que você fez no Program.cs
        var options = new JsonSerializerOptions();
        options.Converters.Add(new JsonStringEnumConverter());
        
        var equipamento = new Equipamento { Tipo = TipoEquipamento.Escavadeira };

        // Act
        var json = JsonSerializer.Serialize(equipamento, options);

        // Assert
        // Verificamos se o nome do Enum aparece no texto, provando a conversão
        Assert.Contains("Escavadeira", json);
    }
}