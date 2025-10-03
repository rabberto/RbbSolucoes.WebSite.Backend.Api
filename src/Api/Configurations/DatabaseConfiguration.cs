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
    private readonly static string _connectionString = AppSettings.Settings?.MongoDbSettings?.ConnectionString
        ?? throw new InvalidOperationException("MongoDbSettings.ConnectionString is not initialized.");

    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services)
    {
        services.AddScoped<MongoDbSession>();

        services.AddSingleton<IMongoClient>(sp =>
        {
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(_connectionString);

            settings.LoggingSettings = new LoggingSettings(sp.GetRequiredService<ILoggerFactory>());

            settings.SslSettings = new SslSettings
            {
                EnabledSslProtocols = SslProtocols.Tls12
            };

            settings.WaitQueueTimeout = TimeSpan.FromSeconds(30);

            return new MongoClient(settings);
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
