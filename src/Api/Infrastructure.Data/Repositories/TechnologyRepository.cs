using System;
using MongoDB.Driver;
using RbbSolucoes.Website.Backend.Api.CrossCutting.AppSettings;
using RbbSolucoes.Website.Backend.Api.Domain.Entities;
using RbbSolucoes.Website.Backend.Api.Domain.Interface.Repositories;
using RbbSolucoes.Website.Backend.Api.Infrastructure.Data.Repositories.Shared;

namespace RbbSolucoes.Website.Backend.Api.Infrastructure.Data.Repositories;

public class TechnologyRepository(MongoDbContext dbContext) 
    : BaseRepository<TechnologyEntity>(dbContext), ITechnologyRepository
{
    private readonly string CollectionName = AppSettings.Settings?.MongoDbSettings?.Collections?.Technologies
        ?? throw new InvalidOperationException("Collection name for Technologies is not configured.");

    protected override IMongoCollection<TechnologyEntity> Collection => CollectionName is not null
        ? _dbContext.GetCollection<TechnologyEntity>(CollectionName)
        : throw new InvalidOperationException("Collection name for Technologies is not configured.");
}
