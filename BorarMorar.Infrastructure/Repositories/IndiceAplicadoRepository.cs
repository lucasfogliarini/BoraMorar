using BoraMorar.Indices;
using BoraMorar.Indices.Repository;

namespace BoraMorar.Infrastructure.Repositories;

public class IndiceAplicadoRepository(BoraMorarDbContext dbContext) : IIndiceAplicadoRepository
{
    public ICommitScope CommitScope => dbContext;

    public void Add(IndiceAplicado indice)
    {
        dbContext.Add(indice);
    }

    public async Task<IndiceAplicado?> FindAsync(int id)
    {
        return await dbContext.FindAsync<IndiceAplicado>(id);
    }
}
