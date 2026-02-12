using MinhaApi.Dtos;
using Xunit;

namespace MinhaApi.tests.Dtos
{
    public class LoteMinerioDtoTest
    {
        [Fact]
        public void CreateLoteMinerioDto_DeveAtribuirValoresCorretamente()
        {
            // Arrange & Act
            var dto = new CreateLoteMinerioDto
            {
                CodigoLote = "LOTE-001",
                MinaOrigem = "Mina Principal",
                TeorFe = 62.1m,
                Toneladas = 500.5m,
                Status = 0
            };

            // Assert
            Assert.Equal("LOTE-001", dto.CodigoLote);
            Assert.Equal("Mina Principal", dto.MinaOrigem);
            Assert.Equal(62.1m, dto.TeorFe);
            Assert.Equal(500.5m, dto.Toneladas);
            Assert.Equal(0, dto.Status);
        }
    }
}