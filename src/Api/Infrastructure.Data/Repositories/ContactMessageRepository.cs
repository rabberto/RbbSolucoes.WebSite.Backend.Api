using System;
using MongoDB.Driver;
using RbbSolucoes.Website.Backend.Api.CrossCutting.AppSettings;
using RbbSolucoes.Website.Backend.Api.Domain.Entities;
using RbbSolucoes.Website.Backend.Api.Domain.Enums;
using RbbSolucoes.Website.Backend.Api.Domain.Interface.Repositories;
using RbbSolucoes.Website.Backend.Api.Infrastructure.Data.Repositories.Shared;

namespace RbbSolucoes.Website.Backend.Api.Infrastructure.Data.Repositories;

public class ContactMessageRepository(MongoDbContext dbContext) 
    : BaseRepository<ContactMessageEntity>(dbContext), IContactMessageRepository
{
    private readonly string CollectionName = AppSettings.Settings?.MongoDbSettings?.Collections?.ContactMessages
        ?? throw new InvalidOperationException("Collection name for ContactMessages is not configured.");

    protected override IMongoCollection<ContactMessageEntity> Collection => CollectionName is not null
        ? _dbContext.GetCollection<ContactMessageEntity>(CollectionName)
        : throw new InvalidOperationException("Collection name for ContactMessages is not configured.");
}
