using System;
using System.Diagnostics.CodeAnalysis;
using RbbSolucoes.Website.Backend.Api.CrossCutting.AppSettings;

namespace RbbSolucoes.Website.Backend.Api.Configurations;

[ExcludeFromCodeCoverage]
public static class AppSettingsConfiguration
{
    public static void AddAppSettingsConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        var appSettings = builder.Configuration.Get<AppSettings>() ?? throw new Exception("AppSettings not found");

        AppSettings.Initialize(appSettings);
        
        // Registrar também como serviço para DI
        builder.Services.AddSingleton(appSettings);
    }
}
