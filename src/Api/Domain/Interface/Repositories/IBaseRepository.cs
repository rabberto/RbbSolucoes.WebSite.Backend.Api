using System;
using RbbSolucoes.Website.Backend.Api.Domain.Entities.Shared;

namespace RbbSolucoes.Website.Backend.Api.Domain.Interface.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> CreateAsync(T entity);
    Task<T?> UpdateAsync(T entity);
    Task<bool> DeleteAsync(Guid id);
}
