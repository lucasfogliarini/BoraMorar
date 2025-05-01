using BoraMorar.Imoveis.Repository;
using BoraMorar.Moradias.Crawlers;
using CSharpFunctionalExtensions;
using MediatR;

namespace BoraMorar.Moradias.RequestHandlers;

public class CrawlCyrelaGoldszteinImoveisRequestHandler(ICyrelaCrawler cyrelaCrawler, IImovelRepository imovelRepository) : IRequestHandler<CrawlCyrelaGoldszteinImoveisRequest, Result>
{
    public async Task<Result> Handle(CrawlCyrelaGoldszteinImoveisRequest request, CancellationToken cancellationToken)
    {
        var properties = await cyrelaCrawler.CrawlPropertiesAsync(DateTime.Now.AddDays(-7), DateTime.Now);
        Result<IEnumerable<CrawledProperty>> result = properties.ToList();
        return result
            .EnsureNotNull("Imóveis não encontrados.")
            .Tap(properties =>
            {
                var imoveis = properties.ToImoveis();
                imovelRepository.AddRange(imoveis);
                imovelRepository.CommitScope.Commit();
            });
    }
}

public record CrawlCyrelaGoldszteinImoveisRequest(long Id) : IRequest<Result>;
