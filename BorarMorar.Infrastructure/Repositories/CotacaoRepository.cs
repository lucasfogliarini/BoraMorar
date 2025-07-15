using BoraMorar.Cotacoes.Repository;

namespace BoraMorar.Infrastructure.Repositories;

public class CotacaoRepository(BoraMorarDbContext boraCotacoesDbContext) : ICotacaoRepository
{
    public ICommitScope CommitScope => boraCotacoesDbContext;

    public void Add(Cotacao cotacao)
    {
        boraCotacoesDbContext.Add(cotacao);
    }

    public void AddRange(IEnumerable<Cotacao> cotacoes)
    {
        boraCotacoesDbContext.AddRange(cotacoes);
    }

    public async Task<Cotacao?> FindAsync(int id)
    {
        return await boraCotacoesDbContext.FindAsync<Cotacao>(id);
    }
}
