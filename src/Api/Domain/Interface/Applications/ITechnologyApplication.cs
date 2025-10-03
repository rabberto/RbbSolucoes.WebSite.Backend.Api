using RbbSolucoes.Website.Backend.Api.Domain.DTOs.Technology;

namespace RbbSolucoes.Website.Backend.Api.Domain.Interface.Applications;

public interface ITechnologyApplication
{
    Task<ResponseTechnologyDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<ResponseTechnologyDto>> GetAllAsync();
    Task<ResponseTechnologyDto> CreateAsync(CreateUpdateTechnologyDto createUpdateTechnologyDto);
    Task<ResponseTechnologyDto?> UpdateAsync(Guid id, CreateUpdateTechnologyDto createUpdateTechnologyDto);
    Task<bool> DeleteAsync(Guid id);
}
