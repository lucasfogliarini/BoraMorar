using BoraMorar.Propostas;
using BoraMorar.Propostas.Repository;

namespace BoraMorar.Infrastructure.Repositories;

internal class PropostaRepository(BoraMorarDbContext boraMorarDbContext) : IPropostaRepository
{
    public ICommitScope CommitScope => boraMorarDbContext;

    public void Add(Proposta proposta)
    {
        boraMorarDbContext.Add(proposta);
    }

    public void AddRange(IEnumerable<Proposta> propostas)
    {
        boraMorarDbContext.Add(propostas);
    }

    public async Task<Proposta?> FindAsync(int id)
    {
        return await boraMorarDbContext.FindAsync<Proposta>(id);
    }
}
