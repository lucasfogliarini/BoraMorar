using BoraMorar.Imoveis;

namespace BoraMorar.Tests;

public class ImovelTests
{
    [Fact]
    public void CadastrarImovel_WhenValidParameters_ShouldInitializeSuccessfully()
    {
        // Arrange
        var endereco = new Endereco(
            tipo: TipoEndereco.Logradouro,
            logradouro: "Av. Dr. Sezefredo Azambuja Vieira",
            bairro: "Mal. Rondon",
            cidade: "Canoas",
            cep: "92020-020"
        );

        var nome = "Grand Park Moinhos";
        var incorporadora = Incorporadora.Melnick;
        var paginaUrl = "https://www.melnick.com.br/enterprise/grand-park-moinhos/";
        var bookUrl = "https://wordpress-melnick.s3.sa-east-1.amazonaws.com/wp-content/uploads/2022/06/31100946/Book-Grand-Park-Moinhos.pdf";

        // Act
        var imovel = new Imovel(
            nome: nome,
            endereco: endereco,
            incorporadora: incorporadora,
            paginalUrl: paginaUrl,
            bookUrl: bookUrl
        );

        // Assert
        Assert.Equal(nome, imovel.Nome);
        Assert.Equal(endereco, imovel.Endereco);
        Assert.Equal(incorporadora, imovel.Incorporadora);
        Assert.Equal(paginaUrl, imovel.PaginaUrl);
        Assert.Equal(bookUrl, imovel.BookUrl);
    }
}
