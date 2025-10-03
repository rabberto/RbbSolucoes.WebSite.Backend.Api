using System;
using MongoDB.Driver;
using RbbSolucoes.Website.Backend.Api.CrossCutting.AppSettings;
using RbbSolucoes.Website.Backend.Api.Domain.Entities;
using RbbSolucoes.Website.Backend.Api.Domain.Interface.Repositories;
using RbbSolucoes.Website.Backend.Api.Infrastructure.Data.Repositories.Shared;

namespace RbbSolucoes.Website.Backend.Api.Infrastructure.Data.Repositories;

public class ServiceRepository(MongoDbContext dbContext) 
    : BaseRepository<ServiceEntity>(dbContext), IServiceRepository
{
    private readonly string CollectionName = AppSettings.Settings?.MongoDbSettings?.Collections?.Services
        ?? throw new InvalidOperationException("Collection name for Services is not configured.");

    protected override IMongoCollection<ServiceEntity> Collection => CollectionName is not null
        ? _dbContext.GetCollection<ServiceEntity>(CollectionName)
        : throw new InvalidOperationException("Collection name for Services is not configured.");
}
