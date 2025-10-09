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
    [JsonPropertyName("user")]
    public string? User { get; init; }

    [JsonPropertyName("password")]
    public string? Password { get; init; }

    [JsonPropertyName("collections")]
    public MongoDbCollections? Collections { get; init; }

    public string GetFormattedConnectionString()
    {
        Console.WriteLine("ConnectionString: " + ConnectionString);
        Console.WriteLine("User: " + User);
        Console.WriteLine("DatabaseName: " + DatabaseName);
        Console.WriteLine("Password: " + (string.IsNullOrWhiteSpace(Password) ? "Not Set" : "Set"));
        
        if (string.IsNullOrWhiteSpace(ConnectionString) ||
        string.IsNullOrWhiteSpace(User) ||
        string.IsNullOrWhiteSpace(Password) ||
        string.IsNullOrWhiteSpace(DatabaseName))
        {
            throw new InvalidOperationException("MongoDbSettings properties are not properly set");
        }

        return string.Format(ConnectionString, User, Password, DatabaseName);
    }
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
