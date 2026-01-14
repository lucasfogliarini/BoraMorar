using BoraMorar.Indices;

namespace BoraMorar.Tests
{
    public class IndiceAplicadoTests
    {
        [Fact]
        public void RegistrarIndiceAplicado_WhenValidParameters_ShouldInitializeSuccessfully()
        {
            // Arrange
            var indice = Indice.IGMI_R;
            var portoAlegre = new Endereco(TipoEndereco.Cidade, "90000-001", "Porto Alegre");
            var valor = 0.45m;
            var dataReferencia = new DateOnly(2025, 3, 1);

            // Act
            var indiceAplicado = new IndiceAplicado(indice, portoAlegre, valor, dataReferencia);

            // Assert
            Assert.Equal(indice, indiceAplicado.Indice);
            Assert.Equal(portoAlegre, indiceAplicado.Endereco);
            Assert.Equal(valor, indiceAplicado.Valor);
            Assert.Equal(dataReferencia, indiceAplicado.DataReferencia);
        }
    }
}
