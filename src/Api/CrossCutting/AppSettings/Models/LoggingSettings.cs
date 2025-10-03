using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace RbbSolucoes.Website.Backend.Api.CrossCutting.AppSettings.Models;

[ExcludeFromCodeCoverage]
public sealed class LoggingSettings
{
    [JsonPropertyName("elasticLogUrl")]
    public string? ElasticLogUrl { get; init; }

    [JsonPropertyName("logLevel")]
    public LogLevelSettings? LogLevel { get; init; }
}

[ExcludeFromCodeCoverage]
public sealed class LogLevelSettings
{
    [JsonPropertyName("default")]
    public string? Default { get; init; }

    [JsonPropertyName("Microsoft.AspNetCore")]
    public string? MicrosoftAspNetCore { get; init; }
}