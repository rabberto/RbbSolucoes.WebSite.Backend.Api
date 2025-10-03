using RbbSolucoes.Website.Backend.Api.Domain.DTOs.Services;

namespace RbbSolucoes.Website.Backend.Api.Domain.Interface.Applications;

public interface IServiceApplication
{
    Task<ResponseServiceDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<ResponseServiceDto>> GetAllAsync();
    Task<ResponseServiceDto> CreateAsync(CreateUpdateServiceDto entity);
    Task<ResponseServiceDto?> UpdateAsync(Guid id, CreateUpdateServiceDto entity);
    Task<bool> DeleteAsync(Guid id);
}
