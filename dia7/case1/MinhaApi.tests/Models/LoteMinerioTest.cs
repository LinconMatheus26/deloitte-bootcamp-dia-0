using MinhaApi.Models;
using Xunit;

namespace MinhaApi.tests.Models
{
    public class LoteMinerioTest
    {
        [Fact]
        public void LoteMinerio_DeveValidarPropriedades()
        {
            var lote = new LoteMinerio { CodigoLote = "ABC", Toneladas = 100 };
            Assert.Equal("ABC", lote.CodigoLote);
            Assert.Equal(100, lote.Toneladas);
        }
    }
}