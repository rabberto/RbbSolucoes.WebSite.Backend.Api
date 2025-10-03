using System;
using System.Diagnostics.CodeAnalysis;
using MongoDB.Driver;
using RbbSolucoes.Website.Backend.Api.CrossCutting.AppSettings;

namespace RbbSolucoes.Website.Backend.Api.Infrastructure.Data;

 [ExcludeFromCodeCoverage]
    public class MongoDbContext(IMongoClient mongoClient)
    {
        private readonly IMongoDatabase _database = mongoClient.GetDatabase(AppSettings.Settings?.MongoDbSettings?.DatabaseName
            ?? throw new InvalidOperationException("MongoDbSettings is not initialized."));

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }

        public IEnumerable<string> ListCollections()
        {
            return _database.ListCollectionNames().ToList();
        }
    }
