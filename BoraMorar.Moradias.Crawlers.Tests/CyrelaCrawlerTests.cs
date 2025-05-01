using BoraMorar.Imoveis;

namespace BoraMorar.Moradias.Crawlers.Tests
{
    public class CyrelaCrawlerTests
    {
        [Fact]
        public async Task CrawlPropertiesAsync()
        {
            var crawler = new CyrelaCrawler();

            var properties = await crawler.CrawlPropertiesAsync(DateTime.Now);

            Assert.NotNull(properties);

            foreach (var property in properties)
            {
                Assert.NotNull(property);
                Assert.NotEmpty(property.Nome);
                Assert.NotNull(property.PaginaUrl);
                //Assert.NotNull(property.BookUrl);
                Assert.Equal(Incorporadora.CyrelaGoldsztein, property.Incorporadora);
                Assert.Equal(DateTime.Today, property.CrawledAt.Date);
                Assert.NotNull(property.Endereco);
                //Assert.NotEmpty(property.Endereco.Cep);
                Assert.Equal(TipoEndereco.Logradouro, property.Endereco.Tipo);
            }
        }
    }
}