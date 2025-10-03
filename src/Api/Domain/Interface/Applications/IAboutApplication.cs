using System;
using RbbSolucoes.Website.Backend.Api.Domain.DTOs.About;

namespace RbbSolucoes.Website.Backend.Api.Domain.Interface.Applications;

public interface IAboutApplication
{
    Task<ResponseAboutDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<ResponseAboutDto>> GetAllAsync();
    Task<ResponseAboutDto> CreateAsync(CreateUpdateAboutDto entity);
    Task<ResponseAboutDto?> UpdateAsync(Guid id, CreateUpdateAboutDto entity);
    Task<bool> DeleteAsync(Guid id);
}
