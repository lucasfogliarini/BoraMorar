namespace BoraMorar.Moradias.Crawlers.Tests
{
    public class CyrelaCrawlerTests
    {
        [Fact]
        public async Task CrawlPropertiesAsync()
        {
            var crawler = new CyrelaCrawler();

            var properties = await crawler.CrawlPropertiesAsync(DateTime.Now);
        }
    }
}