namespace BoraMorar.Moradias.Crawlers
{
    public interface ICyrelaCrawler
    {
        Task<IEnumerable<CrawledProperty>> CrawlPropertiesAsync(DateTime startDate, DateTime? endDate = null);
    }
}
