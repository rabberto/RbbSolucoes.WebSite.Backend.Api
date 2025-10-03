using System;
using MongoDB.Driver;
using RbbSolucoes.Website.Backend.Api.CrossCutting.AppSettings;
using RbbSolucoes.Website.Backend.Api.Domain.Entities;
using RbbSolucoes.Website.Backend.Api.Domain.Interface.Repositories;

namespace RbbSolucoes.Website.Backend.Api.Infrastructure.Data.Repositories.Shared;

public class ContactRepository(MongoDbContext dbContext) 
    : BaseRepository<ContactEntity>(dbContext), IContactRepository
{
    private readonly string CollectionName = AppSettings.Settings?.MongoDbSettings?.Collections?.Contact
        ?? throw new InvalidOperationException("Collection name for Contact is not configured.");

    protected override IMongoCollection<ContactEntity> Collection => CollectionName is not null
        ? _dbContext.GetCollection<ContactEntity>(CollectionName)
        : throw new InvalidOperationException("Collection name for Contact is not configured.");
}
