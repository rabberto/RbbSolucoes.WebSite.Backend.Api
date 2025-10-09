using System;
using RbbSolucoes.Website.Backend.Api.Application;
using RbbSolucoes.Website.Backend.Api.Domain.Interface.Applications;
using RbbSolucoes.Website.Backend.Api.Domain.Interface.Repositories;
using RbbSolucoes.Website.Backend.Api.Infrastructure.Data.Repositories;
using RbbSolucoes.Website.Backend.Api.Infrastructure.Data.Repositories.Shared;

namespace RbbSolucoes.Website.Backend.Api.Configurations;

public static class DependencyResolver
{
    public static void AddDependencyResolver(this IServiceCollection services)
    {
        AddHttpClientConfiguration(services);
        AddApplicationHandlers(services);
        AddServices(services);
        AddRepositories(services);
        AddValidators(services);
    }
    
    private static void AddHttpClientConfiguration(this IServiceCollection services)
    {
    }

    private static void AddApplicationHandlers(this IServiceCollection services)
    {
        services.AddScoped<IAboutApplication, AboutApplication>();
        services.AddScoped<IServiceApplication, ServiceApplication>();
        services.AddScoped<ITechnologyApplication, TechnologyApplication>();
        services.AddScoped<IContactApplication, ContactApplication>();
        services.AddScoped<IContactMessageApplication, ContactMessageApplication>();
    }

    private static void AddServices(this IServiceCollection services)
    {
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IAboutRepository, AboutRepository>();
        services.AddTransient<IServiceRepository, ServiceRepository>();
        services.AddTransient<ITechnologyRepository, TechnologyRepository>();
        services.AddTransient<IContactRepository, ContactRepository>();
        services.AddTransient<IContactMessageRepository, ContactMessageRepository>();
    }

    private static void AddValidators(this IServiceCollection services)
    {
    }
}
