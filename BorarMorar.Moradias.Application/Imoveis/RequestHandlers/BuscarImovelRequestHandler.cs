using BoraMorar.Imoveis;
using BoraMorar.Imoveis.Repository;
using CSharpFunctionalExtensions;
using MediatR;

namespace BoraMorar.Moradias.RequestHandlers;

public class BuscarImovelRequestHandler(IImovelRepository repository) : IRequestHandler<BuscarImovelRequest, Result<BuscarImovelResponse>>
{
    public async Task<Result<BuscarImovelResponse>> Handle(BuscarImovelRequest request, CancellationToken cancellationToken)
    {
        Result<Imovel> result = await repository.FindAsync(request.Id);
        return result
            .EnsureNotNull("Imóvel não encontrado.")
            .MapTry(c => new BuscarImovelResponse(c.Id));
    }
}

public record BuscarImovelRequest(long Id) : IRequest<Result<BuscarImovelResponse>>;

public record BuscarImovelResponse(long Id);
