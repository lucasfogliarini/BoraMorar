using BoraMorar.Imoveis;
using BoraMorar.Imoveis.Repository;

namespace BoraMorar.Infrastructure.Repositories;

public class ImovelRepository(BoraMorarDbContext dbContext) : IImovelRepository
{
    public ICommitScope CommitScope => dbContext;

    public void Add(Imovel imovel)
    {
        dbContext.Add(imovel);
    }

    public void AddRange(IEnumerable<Imovel> imoveis)
    {
        dbContext.AddRange(imoveis);
    }

    public async Task<Imovel?> FindAsync(long id)
    {
        return await dbContext.FindAsync<Imovel>(id);
    }
}
