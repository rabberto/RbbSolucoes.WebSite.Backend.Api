using System;
using System.Diagnostics.CodeAnalysis;
using RbbSolucoes.Website.Backend.Api.CrossCutting.AppSettings;

namespace RbbSolucoes.Website.Backend.Api.Infrastructure.Data;

[ExcludeFromCodeCoverage]
public class MongodbMigrations(MongoDbSession mongoDbSession)
{
    public async Task ExecuteMigrationsAsync()
    {
        CreatedCollectionsAsync();
        await RemoveIndexesAsync();
        await CreateIndexesAsync();

        Console.WriteLine("Executing MongoDB migrations...");
    }

    private void CreatedCollectionsAsync()
    {
        var collectionNames = new List<string>
        {
            AppSettings.Settings.MongoDbSettings?.Collections?.About ?? throw new InvalidOperationException("Collection name for About is not configured."),
            AppSettings.Settings.MongoDbSettings?.Collections?.Services ?? throw new InvalidOperationException("Collection name for Services is not configured."),
            AppSettings.Settings.MongoDbSettings?.Collections?.Technologies ?? throw new InvalidOperationException("Collection name for Technologies is not configured."),
            AppSettings.Settings.MongoDbSettings?.Collections?.Contact ?? throw new InvalidOperationException("Collection name for Contact is not configured."),
            AppSettings.Settings.MongoDbSettings?.Collections?.ContactMessages ?? throw new InvalidOperationException("Collection name for ContactMessages is not configured.")
        };

        foreach (var collectionName in collectionNames)
        {
            mongoDbSession.CreateCollection(collectionName);
        }
    }

    private static async Task RemoveIndexesAsync()
    {
        await Task.Delay(1);
    }

    private static async Task CreateIndexesAsync()
    {
        await Task.Delay(1);
    }

    public async Task ExecutarMigracao()
    {
        await ExecuteMigrationsAsync();
    }
}
