using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace RbbSolucoes.Website.Backend.Api.CrossCutting.AppSettings.Models;

[ExcludeFromCodeCoverage]
public sealed class MongoDbSettings
{
    [JsonPropertyName("connectionString")]
    public string? ConnectionString { get; init; }

    [JsonPropertyName("databaseName")]
    public string? DatabaseName { get; init; }

    [JsonPropertyName("collections")]
    public MongoDbCollections? Collections { get; init; }
}

[ExcludeFromCodeCoverage]
public sealed class MongoDbCollections
{
    [JsonPropertyName("about")]
    public string? About { get; init; }

    [JsonPropertyName("services")]
    public string? Services { get; init; }

    [JsonPropertyName("technologies")]
    public string? Technologies { get; init; }

    [JsonPropertyName("contact")]
    public string? Contact { get; init; }

    [JsonPropertyName("contactMessages")]
    public string? ContactMessages { get; init; }
}
