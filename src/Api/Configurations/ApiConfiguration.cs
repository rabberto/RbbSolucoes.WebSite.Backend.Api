using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using RbbSolucoes.Website.Backend.Api.CrossCutting.Models;

namespace RbbSolucoes.Website.Backend.Api.Configurations;

[ExcludeFromCodeCoverage]
public static class ApiConfiguration
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddControllers();

        services.AddMvc()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext => CustomErrorResponse(actionContext);
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "RbbSolucoes.Website.Backend.Api",
                Version = "v1",
                Description = "API for RbbSolucoes.Website.Backend"
            });

            // Add Basic Authentication to Swagger
            c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "basic",
                In = ParameterLocation.Header,
                Description = "Insira as credenciais de autenticação básica."
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "basic"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        services.AddHealthChecks()
            .AddMongoDb(sp => sp.GetRequiredService<IMongoClient>());

        services.AddEndpointsApiExplorer();

        services.AddMemoryCache();

        services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("fixed", opt =>
            {
                opt.PermitLimit = 1;
                opt.Window = TimeSpan.FromSeconds(1);
                opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                opt.QueueLimit = 0;
            })
            .OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = 429;
                await context.HttpContext.Response.WriteAsync("Limite de requisições atingido.", token);
            };
        });

        return services;
    }

    public static WebApplication UseApiConfiguration(this WebApplication app)
    {

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "RbbSolucoes.Website.Backend.Api v1");
            c.DocumentTitle = "RbbSolucoes API";
            c.EnableDeepLinking();
            c.DisplayOperationId();
            c.DisplayRequestDuration();
        });

        app.UseRateLimiter();

        app.UseHealthChecks("/health");
        return app;
    }

    private static BadRequestObjectResult CustomErrorResponse(ActionContext actionContext)
    {
        var erros = actionContext.ModelState
            .Where(modelError => modelError.Value?.Errors.Count > 0)
            .SelectMany(modelError => modelError.Value!.Errors)
            .Select(modelError => new ErrorModel(new ErrorModel.ErrorDetails
            {
                StatusCode = 400,
                Message = modelError.ErrorMessage
            }));

        return new BadRequestObjectResult(erros);
    }
}
