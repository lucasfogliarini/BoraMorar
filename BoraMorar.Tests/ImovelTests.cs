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

        decimal? preco = null;
        var nome = "Grand Park Moinhos";
        var incorporadora = Incorporadora.Melnick;
        var areaPrivativa = 73;
        var quartos = 2;
        var banheiros = 2;
        var vagasGaragem = 1;
        var paginaUrl = "https://www.melnick.com.br/enterprise/grand-park-moinhos/";
        var bookUrl = "https://wordpress-melnick.s3.sa-east-1.amazonaws.com/wp-content/uploads/2022/06/31100946/Book-Grand-Park-Moinhos.pdf";
        var caracteristicas = new List<string>
        {
            "Piscina",
            "Sal�o de Festas",
            "Espa�o Gourmet",
            "Pet Place"
        };

        // Act
        var imovel = new Imovel(
            nome: nome,
            endereco: endereco,
            incorporadora: incorporadora,
            preco: preco,
            areaPrivativa: areaPrivativa,
            quartos: quartos,
            banheiros: banheiros,
            vagasGaragem: vagasGaragem,
            paginalUrl: paginaUrl,
            bookUrl,
            caracteristicas: caracteristicas
        );

        // Assert
        Assert.Equal(nome, imovel.Nome);
        Assert.Equal(endereco, imovel.Endereco);
        Assert.Equal(incorporadora, imovel.Incorporadora);
        Assert.Null(imovel.Preco);
        Assert.Equal(areaPrivativa, imovel.AreaPrivativa);
        Assert.Equal(quartos, imovel.Quartos);
        Assert.Equal(banheiros, imovel.Banheiros);
        Assert.Equal(vagasGaragem, imovel.VagasGaragem);
        Assert.Equal(paginaUrl, imovel.PaginaUrl);
        Assert.Equal(bookUrl, imovel.BookUrl);
        Assert.Equal(caracteristicas, imovel.Caracteristicas);
    }
}
