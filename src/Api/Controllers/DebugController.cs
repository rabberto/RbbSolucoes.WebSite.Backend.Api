using System;
using Microsoft.AspNetCore.Mvc;

namespace RbbSolucoes.Website.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DebugController : ControllerBase
{
    [HttpGet("config-status")]
    public IActionResult GetConfigStatus()
    {
        return Ok(new
        {
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
            HasMongoConnection = Environment.GetEnvironmentVariable("mongoDbSettings__connectionString"),
            HasDatabaseName = Environment.GetEnvironmentVariable("mongoDbSettings__databaseName"),
            HasBasicAuthUser = Environment.GetEnvironmentVariable("basicAuth__username"),
            ConnectionStringLength = Environment.GetEnvironmentVariable("mongoDbSettings__connectionString")
        });
    }
}
