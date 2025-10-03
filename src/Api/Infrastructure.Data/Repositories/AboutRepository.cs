using System;
using MongoDB.Driver;
using RbbSolucoes.Website.Backend.Api.CrossCutting.AppSettings;
using RbbSolucoes.Website.Backend.Api.Domain.Entities;
using RbbSolucoes.Website.Backend.Api.Domain.Interface.Repositories;
using RbbSolucoes.Website.Backend.Api.Infrastructure.Data.Repositories.Shared;

namespace RbbSolucoes.Website.Backend.Api.Infrastructure.Data.Repositories;

public class AboutRepository(MongoDbContext dbContext) 
    : BaseRepository<AboutEntity>(dbContext), IAboutRepository
{
    private readonly string CollectionName = AppSettings.Settings?.MongoDbSettings?.Collections?.About
        ?? throw new InvalidOperationException("Collection name for About is not configured.");

    protected override IMongoCollection<AboutEntity> Collection => CollectionName is not null
        ? _dbContext.GetCollection<AboutEntity>(CollectionName)
        : throw new InvalidOperationException("Collection name for About is not configured.");
}
