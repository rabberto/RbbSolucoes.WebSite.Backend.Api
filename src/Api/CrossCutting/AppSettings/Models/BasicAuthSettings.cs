using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace RbbSolucoes.Website.Backend.Api.CrossCutting.AppSettings.Models;

[ExcludeFromCodeCoverage]
public class BasicAuthSettings
{
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}