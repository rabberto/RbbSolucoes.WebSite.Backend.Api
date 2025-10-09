using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Authentication;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using RbbSolucoes.Website.Backend.Api.CrossCutting.AppSettings;
using RbbSolucoes.Website.Backend.Api.Infrastructure.Data;

namespace RbbSolucoes.Website.Backend.Api.Configurations;

[ExcludeFromCodeCoverage]
public static class DatabaseConfiguration
{
    private readonly static string _connectionString = AppSettings.Settings?.MongoDbSettings?.GetFormattedConnectionString()
        ?? throw new InvalidOperationException("MongoDbSettings.ConnectionString is not initialized.");

    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services)
    {
        services.AddScoped<MongoDbSession>();

        services.AddSingleton<IMongoClient>(sp =>
        {
            MongoClientSettings mongoSettings = MongoClientSettings.FromConnectionString(_connectionString);

            mongoSettings.ConnectTimeout = TimeSpan.FromSeconds(30);
            mongoSettings.ServerSelectionTimeout = TimeSpan.FromSeconds(30);
            mongoSettings.SocketTimeout = TimeSpan.FromSeconds(30);

            mongoSettings.LoggingSettings = new LoggingSettings(sp.GetRequiredService<ILoggerFactory>());

            mongoSettings.SslSettings = new SslSettings
            {
                EnabledSslProtocols = SslProtocols.Tls12
            };

            mongoSettings.WaitQueueTimeout = TimeSpan.FromSeconds(30);

            return new MongoClient(mongoSettings);
        });

        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        services.AddScoped<MongoDbContext>();

        return services;
    }

    public static WebApplication UseMigration(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var mongoDbSession = scope.ServiceProvider.GetRequiredService<MongoDbSession>();
        var migrationMongodb = new MongodbMigrations(mongoDbSession);

        migrationMongodb.ExecutarMigracao().Wait();

        return app;
    }
}
