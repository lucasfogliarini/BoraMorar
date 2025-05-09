using BoraMorar.Propostas;

namespace BoraMorar.Tests
{
    public class PropostaTests
    {
        [Fact]
        public void GerarProposta_WhenValidParameters_ShouldInitializeSuccessfully()
        {
            // Arrange
            var expectedStatus = PropostaStatus.Gerada;
            var expectedCotacaoId = 1;

            // Act
            var proposta = new Proposta(expectedCotacaoId);

            // Assert
            Assert.Equal(expectedStatus, proposta.Status);
            Assert.Equal(expectedCotacaoId, proposta.CotacaoId);
            Assert.StartsWith("PROP-", proposta.Numero);
            Assert.True((DateTime.Now - proposta.CreatedAt).TotalSeconds < 1);
        }
    }
}
