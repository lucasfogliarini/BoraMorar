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

    public async Task<Imovel?> FindAsync(int id)
    {
        return await dbContext.FindAsync<Imovel>(id);
    }
}
