using BoraMorar.Imoveis;
using BoraMorar.Imoveis.Repository;
using CSharpFunctionalExtensions;

namespace BoraMorar.Application.Imoveis.Buscar;

internal sealed class BuscarImovelCommandHandler(IImovelRepository repository) : ICommandHandler<BuscarImovelCommand, BuscarImovelResponse>
{
    public async Task<Result<BuscarImovelResponse>> Handle(BuscarImovelCommand command, CancellationToken cancellationToken)
    {
        Result<Imovel> result = await repository.FindAsync(command.Id);
        return result
            .EnsureNotNull("Imóvel não encontrado.")
            .MapTry(c => new BuscarImovelResponse(c.Id));
    }
}
