using BoraMorar.Imoveis;

namespace BoraMorar.Tests;

public class ImovelTests
{
    [Fact]
    public void CadastrarImovel_WhenValidParameters_ShouldInitializeSuccessfully()
    {
        // Arrange
        var endereco = new Endereco(
            Logradouro: "Av. Dr. Sezefredo Azambuja Vieira",
            Bairro: "Mal. Rondon",
            Cidade: "Canoas",
            Cep: "92020-020"
        );

        decimal? preco = null;
        var nome = "Grand Park Moinhos";
        var areaUtil = 73;
        var quartos = 2;
        var banheiros = 2;
        var vagasGaragem = 1;
        var url = "https://www.melnick.com.br/enterprise/grand-park-moinhos/";
        var bookUrl = "";
        var caracteristicas = new List<string>
        {
            "Piscina",
            "Salão de Festas",
            "Espaço Gourmet",
            "Pet Place"
        };

        // Act
        var imovel = new Imovel(
            nome: nome,
            endereco: endereco,
            preco: preco,
            areaUtil: areaUtil,
            quartos: quartos,
            banheiros: banheiros,
            vagasGaragem: vagasGaragem,
            url: url,
            bookUrl,
            caracteristicas: caracteristicas
        );

        // Assert
        Assert.Equal(nome, imovel.Nome);
        Assert.Equal(endereco, imovel.Endereco);
        Assert.Null(imovel.Preco);
        Assert.Equal(areaUtil, imovel.AreaUtil);
        Assert.Equal(quartos, imovel.Quartos);
        Assert.Equal(banheiros, imovel.Banheiros);
        Assert.Equal(vagasGaragem, imovel.VagasGaragem);
        Assert.Equal(url, imovel.Url);
        Assert.Equal(bookUrl, imovel.BookUrl);
        Assert.Equal(caracteristicas, imovel.Caracteristicas);
    }
}
