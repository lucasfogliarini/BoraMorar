using BoraMorar.Cotacoes;

namespace BoraMorar.Tests
{
    public class CotacaoTests
    {
        [Fact]
        public void Instantiate_WhenValidParameters_ShouldInitializeSuccessfully()
        {
            // Arrange
            var expectedStatus = CotacaoStatus.CotacaoSolicitada;
            var expectedClientId = 1;
            var expectedTipoDoBem = TipoDoBem.Imovel;
            var expectedPreco = 500000;

            // Act
            var cotacao = new Cotacao(expectedClientId, expectedTipoDoBem, expectedPreco);

            // Assert
            Assert.Equal(expectedStatus, cotacao.Status);
            Assert.Equal(expectedClientId, cotacao.ClienteId);
            Assert.Equal(expectedTipoDoBem, cotacao.TipoDoBem);
            Assert.Equal(expectedPreco, cotacao.Preco);
            Assert.StartsWith("COT-", cotacao.Numero);
        }

        [Fact]
        public void SolicitarRenda_WhenCalled_ShouldUpdateStatusToRendaSolicitada()
        {
            // Arrange
            var expectedCorretorId = 123;
            var expectedStatus = CotacaoStatus.RendaSolicitada;
            var cotacao = new Cotacao(1, TipoDoBem.Imovel, 500000);

            // Act
            cotacao.SolicitarRenda(expectedCorretorId);

            // Assert
            Assert.Equal(expectedStatus, cotacao.Status);
            Assert.Equal(expectedCorretorId, cotacao.CorretorId);
            Assert.True((DateTime.UtcNow - cotacao.DataRendaSolicitada).TotalSeconds < 1);
        }

        [Fact]
        public void InformarCompromissoFinanceiro_WhenCalled_ShouldStoreValuesCorrectly()
        {
            // Arrange
            var expectedRenda = 10000;
            var expectedPrazo = 240;
            var expectedStatus = CotacaoStatus.CompromissoFinanceiroInformado;
            var cotacao = new Cotacao(1, TipoDoBem.Imovel, 500000);

            // Act
            cotacao.InformarCompromissoFinanceiro(expectedRenda, expectedPrazo);

            // Assert
            Assert.Equal(expectedRenda, cotacao.RendaBrutaMensal);
            Assert.Equal(expectedPrazo, cotacao.PrazoPretendido);
            Assert.Equal(expectedStatus, cotacao.Status);
        }

        [Fact]
        public void InformarCompromissoFinanceiro_WhenValidData_ShouldSucceed()
        {
            // Arrange
            var expectedTaxa = 0.01m;
            var expectedPrazoMax = 360;
            var cotacao = new Cotacao(1, TipoDoBem.Imovel, 500000);
            cotacao.InformarCompromissoFinanceiro(10000, 240);

            // Act
            var result = cotacao.CalcularPrestacoes(expectedTaxa, expectedPrazoMax);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedTaxa, cotacao.TaxaJuros);
            Assert.Equal(expectedPrazoMax, cotacao.PrazoMaximo);
            Assert.True(cotacao.PrestacaoPrazoPretendido > 0);
            Assert.True(cotacao.PrestacaoPrazoMaximo > 0);
            Assert.Equal(CotacaoStatus.PrestacoesCalculadas, cotacao.Status);
        }

        [Fact]
        public void InformarCompromissoFinanceiro_WhenInvalidData_ShouldReturnFailure()
        {
            // Arrange
            var cotacao = new Cotacao(1, TipoDoBem.Imovel, 500000);
            cotacao.InformarCompromissoFinanceiro(10000, 240);

            // Act
            var result1 = cotacao.CalcularPrestacoes(0, 360);
            var result2 = cotacao.CalcularPrestacoes(0.01m, 0);

            // Assert
            Assert.True(result1.IsFailure);
            Assert.True(result2.IsFailure);
        }

        [Fact]
        public void AprovarCotacao_WhenCalled_ShouldUpdateStatusToApproved()
        {
            // Arrange
            var expectedStatus = CotacaoStatus.CotacaoAprovada;
            var cotacao = new Cotacao(1, TipoDoBem.Imovel, 500000);

            // Act
            cotacao.AprovarCotacao();

            // Assert
            Assert.Equal(expectedStatus, cotacao.Status);
            Assert.True((DateTime.UtcNow - cotacao.DataCotacaoAprovada).TotalSeconds < 1);
        }
    }
}
