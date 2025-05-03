using BoraMorar.Imoveis;
using BoraMorar.Indices;

namespace BoraMorar.Tests
{
    public class ValorizacaoImovelTests
    {
        [Theory]
        [InlineData(502250.0, 0.45, 500000.0)]
        [InlineData(1010000.0, 1.00, 1000000.0)]
        [InlineData(746250.0, -0.50, 750000.0)]
        public void CalcularValorEstimadoAtual(decimal valorEstimadoAtualEsperado, decimal variacaoPercentual, decimal valorEstimadoAtual)
        {
            // Arrange
            var indiceAplicado = new IndiceAplicado(
                indice: Indice.IGMI_R,
                endereco: new Endereco(TipoEndereco.Cidade, "90000-001", "Porto Alegre"),
                valor: variacaoPercentual,
                dataReferencia: new DateOnly(2025, 3, 1)
            );

            var imovel = CriarImoveis().First(e=>e.Endereco.Cidade == indiceAplicado.Endereco.Cidade);
            imovel.ValorEstimadoAtual = valorEstimadoAtual;

            // Act
            var valorizacaoImovel = ValorizacaoImovel.Criar(imovel, indiceAplicado);

            // Assert
            Assert.True(valorizacaoImovel.IsSuccess);
            Assert.Equal(valorEstimadoAtualEsperado, imovel.ValorEstimadoAtual);
            Assert.Equal(valorEstimadoAtualEsperado, valorizacaoImovel.Value.ValorEstimado);
        }

        [Fact]
        public void CalcularValorEstimadoAtual_DeveRetornarFailure_QuandoValorAtualEstimadoForNulo()
        {
            // Arrange
            var endereco = new Endereco(TipoEndereco.Cidade, "90000-001", "Porto Alegre");
            var imovel = CriarImoveis().First();

            var indice = new IndiceAplicado(
                indice: Indice.IGMI_R,
                endereco: endereco,
                valor: 1.0m,
                dataReferencia: new DateOnly(2025, 3, 1)
            );

            // Act
            var result = ValorizacaoImovel.Criar(imovel, indice);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(ValorizacaoImovel.ValorEstimadoAtualNull, result.Error);
        }

        [Fact]
        public void CalcularValorEstimadoAtual_DeveRetornarFailure_QuandoEnderecoForDiferente()
        {
            // Arrange
            var saoPaulo = new Endereco(TipoEndereco.Cidade, "90000-001", "São Paulo");

            var imovel = CriarImoveis().First(e=>e.Endereco.Cidade == "Porto Alegre");
            imovel.ValorEstimadoAtual = 500000.0m;

            var indice = new IndiceAplicado(
                indice: Indice.IGMI_R,
                endereco: saoPaulo,
                valor: 1.0m,
                dataReferencia: new DateOnly(2025, 3, 1)
            );

            // Act
            var result = ValorizacaoImovel.Criar(imovel, indice);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(ValorizacaoImovel.EnderecoNotEqual, result.Error);
        }

        public static List<Imovel> CriarImoveis()
        {
            return
            [
                new (nome: "Seen Boa Vista", endereco: new Endereco(TipoEndereco.Logradouro, "90480-200", "Porto Alegre", "Boa Vista"), incorporadora: Incorporadora.Melnick, paginalUrl: "", bookUrl: ""),
                new (nome: "Supreme Altos do Central Parque", endereco: new Endereco(TipoEndereco.Logradouro, "91410-110", "Porto Alegre", "Jardim do Salso"), incorporadora: Incorporadora.Melnick, paginalUrl: "", bookUrl: ""),
                new (nome: "Grand Park Moinhos", endereco: new Endereco(TipoEndereco.Logradouro, "90420-190", "Canoas", "Mal Rondon"), incorporadora: Incorporadora.Melnick, paginalUrl: "", bookUrl: ""),
                new (nome: "High Garden Rio Branco", endereco: new Endereco(TipoEndereco.Logradouro, "90420-000", "Porto Alegre", "Rio Branco"), incorporadora: Incorporadora.Melnick, paginalUrl: "", bookUrl: ""),
                new (nome: "High Garden Iguatemi", endereco: new Endereco(TipoEndereco.Logradouro, "90480-000", "Porto Alegre", "Boa Vista"), incorporadora: Incorporadora.Melnick, paginalUrl: "", bookUrl: ""),
                new (nome: "Zayt", endereco: new Endereco(TipoEndereco.Logradouro, "00000-000", "Porto Alegre", "Auxiliadora"), incorporadora: Incorporadora.Melnick, paginalUrl: "", bookUrl: ""),
                new (nome: "GO Bom Fim", endereco: new Endereco(TipoEndereco.Logradouro, "90035-120", "Porto Alegre", "Bom Fim"), incorporadora: Incorporadora.Melnick, paginalUrl: "", bookUrl: ""),
                new (nome: "Cidade Nilo", endereco: new Endereco(TipoEndereco.Logradouro, "90460-050", "Porto Alegre", "Petrópolis"), incorporadora: Incorporadora.Melnick, paginalUrl: "", bookUrl: ""),
                new (nome: "Prime Wallig", endereco: new Endereco(TipoEndereco.Logradouro, "91350-200", "Porto Alegre", "Passo d'Areia"), incorporadora: Incorporadora.CyrelaGoldsztein, paginalUrl: "", bookUrl: ""),
                new (nome: "Prime Altos do Germânia", endereco: new Endereco(TipoEndereco.Logradouro, "91350-050", "Porto Alegre", "Cristo Redentor"), incorporadora: Incorporadora.CyrelaGoldsztein, paginalUrl: "", bookUrl: ""),
                new (nome: "Atmosfera Air", endereco: new Endereco(TipoEndereco.Logradouro, "90110-001", "Porto Alegre", "Praia de Belas"), incorporadora: Incorporadora.CyrelaGoldsztein, paginalUrl: "", bookUrl: ""),
                new (nome: "Prime Wish", endereco: new Endereco(TipoEndereco.Logradouro, "90240-512", "Porto Alegre", "São João"), incorporadora: Incorporadora.CyrelaGoldsztein, paginalUrl: "", bookUrl: ""),
                new (nome: "Float Residences", endereco: new Endereco(TipoEndereco.Logradouro, "90690-140", "Porto Alegre", "Petrópolis"), incorporadora: Incorporadora.CyrelaGoldsztein, paginalUrl: "", bookUrl: ""),
                new (nome: "The Arch", endereco: new Endereco(TipoEndereco.Logradouro, "90450-070", "Porto Alegre", "Mont'Serrat"), incorporadora: Incorporadora.CyrelaGoldsztein, paginalUrl: "", bookUrl: ""),
                new (nome: "Tree Haus Porsche Consulting", endereco: new Endereco(TipoEndereco.Logradouro, "91340-010", "Porto Alegre", "Jardim Europa"), incorporadora: Incorporadora.CyrelaGoldsztein, paginalUrl: "", bookUrl: ""),
                new (nome: "Skyglass Parque Moinhos", endereco: new Endereco(TipoEndereco.Logradouro, "90420-000", "Porto Alegre", "Rio Branco"), incorporadora: Incorporadora.CyrelaGoldsztein, paginalUrl: "", bookUrl: ""),
                new (nome: "Ereditá Moinhos", endereco: new Endereco(TipoEndereco.Logradouro, "90570-140", "Porto Alegre", "Moinhos de Vento"), incorporadora: Incorporadora.CyrelaGoldsztein, paginalUrl: "", bookUrl: ""),
                new (nome: "Vista Menino Deus", endereco: new Endereco(TipoEndereco.Logradouro, "90150-004", "Porto Alegre", "Menino Deus"), incorporadora: Incorporadora.CyrelaGoldsztein, paginalUrl: "", bookUrl: ""),
                new (nome: "24/SE7E - Bela Vista", endereco: new Endereco(TipoEndereco.Logradouro, "90450-001", "Porto Alegre", "Bela Vista"), incorporadora: Incorporadora.CyrelaGoldsztein, paginalUrl: "", bookUrl: ""),
                new (nome: "Boa Vista Country Club", endereco: new Endereco(TipoEndereco.Logradouro, "90480-000", "Porto Alegre", "Boa Vista"), incorporadora: Incorporadora.CyrelaGoldsztein, paginalUrl: "", bookUrl: ""),
                new (nome: "Nova Olaria Residences", endereco: new Endereco(TipoEndereco.Logradouro, "90050-000", "Porto Alegre", "Cidade Baixa"), incorporadora: Incorporadora.CyrelaGoldsztein, paginalUrl: "", bookUrl: ""),
                new (nome: "Garden Haus Porsche Consulting", endereco: new Endereco(TipoEndereco.Logradouro, "91340-010", "Porto Alegre", "Jardim Europa"), incorporadora: Incorporadora.CyrelaGoldsztein, paginalUrl: "", bookUrl: ""),
                new (nome: "Vida Viva Clube Centro", endereco: new Endereco(TipoEndereco.Logradouro, "92310-150", "Canoas", "Centro"), incorporadora: Incorporadora.Melnick, paginalUrl: "", bookUrl: ""),
                new (nome: "Teená", endereco: new Endereco(TipoEndereco.Logradouro, "91330-180", "Porto Alegre", "Três Figueiras"), incorporadora: Incorporadora.Melnick, paginalUrl: "", bookUrl: ""),
                new (nome: "Go Carlos Gomes", endereco: new Endereco(TipoEndereco.Logradouro, "90450-001", "Porto Alegre", "Boa Vista"), incorporadora: Incorporadora.Melnick, paginalUrl: "", bookUrl: ""),
                new (nome: "Casa Moinhos", endereco: new Endereco(TipoEndereco.Logradouro, "90510-040", "Porto Alegre", "Moinhos de Vento"), incorporadora: Incorporadora.Melnick, paginalUrl: "", bookUrl: ""),
            ];
        }
    }
}
