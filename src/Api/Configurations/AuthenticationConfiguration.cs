using System.Diagnostics.CodeAnalysis;
using RbbSolucoes.Website.Backend.Api.Middleware;

namespace RbbSolucoes.Website.Backend.Api.Configurations;

[ExcludeFromCodeCoverage]
public static class AuthenticationConfiguration
{
    public static IServiceCollection AddBasicAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication("Basic")
            .AddScheme<BasicAuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

        return services;
    }

    public static WebApplication UseBasicAuthentication(this WebApplication app)
    {
        app.UseMiddleware<BasicAuthenticationMiddleware>();
        return app;
    }
}

[ExcludeFromCodeCoverage]
public class BasicAuthenticationSchemeOptions : Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions
{
}

[ExcludeFromCodeCoverage]
public class BasicAuthenticationHandler : Microsoft.AspNetCore.Authentication.AuthenticationHandler<BasicAuthenticationSchemeOptions>
{
    public BasicAuthenticationHandler(Microsoft.Extensions.Options.IOptionsMonitor<BasicAuthenticationSchemeOptions> options, 
        Microsoft.Extensions.Logging.ILoggerFactory logger, 
        System.Text.Encodings.Web.UrlEncoder encoder) 
        : base(options, logger, encoder)
    {
    }

    protected override Task<Microsoft.AspNetCore.Authentication.AuthenticateResult> HandleAuthenticateAsync()
    {
        return Task.FromResult(Microsoft.AspNetCore.Authentication.AuthenticateResult.NoResult());
    }
}