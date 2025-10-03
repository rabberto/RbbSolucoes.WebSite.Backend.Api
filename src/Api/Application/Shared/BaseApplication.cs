using System;
using Serilog;

namespace RbbSolucoes.Website.Backend.Api.Application.Shared;

public abstract class BaseApplication<TApplication>
{
    protected async Task<T> SafeExecuteAsync<T>(Func<Task<T>> func)
    {
        try
        {
            return await func();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred in {Application}: {ErrorMessage}", typeof(TApplication).Name, ex.Message);
            throw;
        }
    }

    protected async Task<T?> SafeExecuteNullableAsync<T>(Func<Task<T?>> func) where T : class
    {
        try
        {
            return await func();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred in {Application}: {ErrorMessage}", typeof(TApplication).Name, ex.Message);
            throw;
        }
    }
}
