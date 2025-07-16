﻿using BoraMorar.Indices;
using BoraMorar.Indices.Repository;

namespace BoraMorar.Infrastructure.Repositories;

internal class IndiceAplicadoRepository(BoraMorarDbContext dbContext) : IIndiceAplicadoRepository
{
    public ICommitScope CommitScope => dbContext;

    public void Add(IndiceAplicado indice)
    {
        dbContext.Add(indice);
    }

    public void AddRange(IEnumerable<IndiceAplicado> indices)
    {
        dbContext.AddRange(indices);
    }

    public async Task<IndiceAplicado?> FindAsync(int id)
    {
        return await dbContext.FindAsync<IndiceAplicado>(id);
    }
}
