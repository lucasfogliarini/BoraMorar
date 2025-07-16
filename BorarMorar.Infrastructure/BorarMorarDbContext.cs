﻿using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BoraMorar.Infrastructure;

public class BoraMorarDbContext(DbContextOptions options, IMediator mediator) : DbContext(options), ICommitScope
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var thisAssembly = Assembly.GetExecutingAssembly();
        modelBuilder.ApplyConfigurationsFromAssembly(thisAssembly);
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        var domainEntities = ChangeTracker.Entries<AggregateRoot>()
            .Where(x => x.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent, cancellationToken);

        domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

        return result;
    }

    public int Commit(CancellationToken cancellationToken = default) => CommitAsync(cancellationToken).Result;
}
