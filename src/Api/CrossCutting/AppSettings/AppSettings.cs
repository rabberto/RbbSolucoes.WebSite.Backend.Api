using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using RbbSolucoes.Website.Backend.Api.CrossCutting.AppSettings.Models;

namespace RbbSolucoes.Website.Backend.Api.CrossCutting.AppSettings;

[ExcludeFromCodeCoverage]
public sealed class AppSettings
{
    private static AppSettings? _instance;

    public static void Initialize(AppSettings settings)
    {
        if (_instance != null)
        {
            throw new InvalidOperationException("AppSettings is already initialized.");
        }

        _instance = settings;
    }

    public static AppSettings Settings => _instance
        ?? throw new InvalidOperationException("AppSettings is not initialized.");


    [JsonPropertyName("enviroment")]
    public string? Enviroment { get; set; }

    [JsonPropertyName("logging")]
    public LoggingSettings? Logging { get; set; }

    [JsonPropertyName("mongoDbSettings")]
    public MongoDbSettings? MongoDbSettings { get; set; }

    [JsonPropertyName("serviceClients")]
    public IEnumerable<ServiceClientSettings>? ServiceClients { get; set; }

    [JsonPropertyName("basicAuth")]
    public BasicAuthSettings BasicAuth { get; set; } = new();
}
