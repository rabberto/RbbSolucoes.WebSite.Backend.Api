using System;
using System.Diagnostics.CodeAnalysis;
using MongoDB.Driver;
using RbbSolucoes.Website.Backend.Api.CrossCutting.AppSettings;

namespace RbbSolucoes.Website.Backend.Api.Infrastructure.Data;

[ExcludeFromCodeCoverage]
public class MongoDbSession(IMongoClient mongoClient) : IDisposable
{
    private readonly IMongoDatabase _database = mongoClient.GetDatabase(AppSettings.Settings.MongoDbSettings?.DatabaseName
        ?? throw new InvalidOperationException("MongoDbSettings is not initialized."));

    private readonly IClientSessionHandle _session = mongoClient.StartSession();
    private bool _disposed;

    public IMongoCollection<TEntity> GetCollection<TEntity>(string collectionName) => _database.GetCollection<TEntity>(collectionName);
    public IClientSessionHandle Session => _session;

    public IEnumerable<string> ListCollectionNames() => _database.ListCollectionNames().ToList();

    public void Dispose(bool disposing = false)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _session?.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _session.Dispose();
    }

    public void CreateCollection(string collectionName)
    {
        if (!_database.ListCollectionNames().ToList().Contains(collectionName))
        {
            _database.CreateCollection(collectionName);
        }
    }
}
