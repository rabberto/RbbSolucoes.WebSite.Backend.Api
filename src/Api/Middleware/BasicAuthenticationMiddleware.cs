using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using RbbSolucoes.Website.Backend.Api.CrossCutting.AppSettings;

namespace RbbSolucoes.Website.Backend.Api.Middleware;

public class BasicAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _serviceProvider;

    public BasicAuthenticationMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        var hasBasicAuthAttribute = endpoint?.Metadata?.GetMetadata<RequireBasicAuthAttribute>() != null;

        if (!hasBasicAuthAttribute)
        {
            await _next(context);
            return;
        }

        string authHeader = context.Request.Headers.Authorization.FirstOrDefault() ?? string.Empty;

        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Basic "))
        {
            await HandleUnauthorized(context);
            return;
        }

        try
        {
            var token = authHeader["Basic ".Length..].Trim();
            var credentialBytes = Convert.FromBase64String(token);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);

            if (credentials.Length != 2)
            {
                await HandleUnauthorized(context);
                return;
            }

            var username = credentials[0];
            var password = credentials[1];

            using var scope = _serviceProvider.CreateScope();
            var appSettings = scope.ServiceProvider.GetRequiredService<AppSettings>();

            if (!ValidateCredentials(username, password, appSettings))
            {
                await HandleUnauthorized(context);
                return;
            }

            // Set user identity
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, username)
            };

            var identity = new ClaimsIdentity(claims, "Basic");
            var principal = new ClaimsPrincipal(identity);
            context.User = principal;

            await _next(context);
        }
        catch
        {
            await HandleUnauthorized(context);
        }
    }

    private static bool ValidateCredentials(string username, string password, AppSettings appSettings)
    {
        return username == appSettings.BasicAuth.Username && 
               password == appSettings.BasicAuth.Password;
    }

    private static async Task HandleUnauthorized(HttpContext context)
    {
        context.Response.StatusCode = 401;
        context.Response.Headers.WWWAuthenticate = "Basic realm=\"API\"";
        await context.Response.WriteAsync("Unauthorized");
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RequireBasicAuthAttribute : Attribute
{
}