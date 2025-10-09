using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace RbbSolucoes.Website.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DebugController : ControllerBase
{
    [HttpGet("config-status")]
    public IActionResult GetConfigStatus()
    {
        var mongoConnection = Environment.GetEnvironmentVariable("mongoDbSettings__connectionString");
        var databaseName = Environment.GetEnvironmentVariable("mongoDbSettings__databaseName");
        var basicAuthUser = Environment.GetEnvironmentVariable("basicAuth__username");
        var basicAuthPassword = Environment.GetEnvironmentVariable("basicAuth__password");

        return Ok(new
        {
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
            HasMongoConnection = !string.IsNullOrEmpty(mongoConnection),
            MongoConnectionLength = mongoConnection?.Length ?? 0,
            MongoConnectionPrefix = !string.IsNullOrEmpty(mongoConnection) 
                ? string.Concat(mongoConnection.AsSpan(0, Math.Min(15, mongoConnection.Length)), "...")
                : "",
            HasDatabaseName = !string.IsNullOrEmpty(databaseName),
            DatabaseName = databaseName, // Este pode ser mostrado pois não é sensível
            HasBasicAuthUser = !string.IsNullOrEmpty(basicAuthUser),
            BasicAuthUser = basicAuthUser, // Username geralmente não é sensível
            HasBasicAuthPassword = !string.IsNullOrEmpty(basicAuthPassword),
            AllEnvironmentVariables = Environment.GetEnvironmentVariables()
                .Cast<System.Collections.DictionaryEntry>()
                .Where(x => x.Key?.ToString()?.Contains("mongoDb") == true || 
                           x.Key?.ToString()?.Contains("basicAuth") == true || 
                           x.Key?.ToString()?.Contains("ASPNET") == true)
                .ToDictionary(x => x.Key?.ToString() ?? "", x => x.Value?.ToString()?.Length ?? 0)
        });
    }
}
