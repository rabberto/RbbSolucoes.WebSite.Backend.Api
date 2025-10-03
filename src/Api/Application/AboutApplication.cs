using RbbSolucoes.Website.Backend.Api.Application.Shared;
using RbbSolucoes.Website.Backend.Api.Domain.DTOs.About;
using RbbSolucoes.Website.Backend.Api.Domain.Interface.Applications;
using RbbSolucoes.Website.Backend.Api.Domain.Interface.Repositories;
using RbbSolucoes.Website.Backend.Api.Domain.Mappers;

namespace RbbSolucoes.Website.Backend.Api.Application;

public class AboutApplication(IAboutRepository aboutRepository) : BaseApplication<AboutApplication>, IAboutApplication
{
    public async Task<ResponseAboutDto> CreateAsync(CreateUpdateAboutDto createUpdateAboutDto)
    {
        return await SafeExecuteAsync(async () =>
        {
            var entity = createUpdateAboutDto.MapDtoToEntity();

            var createdEntity = await aboutRepository.CreateAsync(entity);

            return createdEntity.MapEntityToResponseDto();
        });
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await SafeExecuteAsync(async () =>
        {
            return await aboutRepository.DeleteAsync(id);
        });
    }

    public async Task<IEnumerable<ResponseAboutDto>> GetAllAsync()
    {
        return await SafeExecuteAsync(async () =>
        {
            return await aboutRepository.GetAllAsync()
                .ContinueWith(t => t.Result.Select(e => e.MapEntityToResponseDto()));
        });
    }

    public async Task<ResponseAboutDto?> GetByIdAsync(Guid id)
    {
        return await SafeExecuteNullableAsync(async () =>
        {
            var entity = await aboutRepository.GetByIdAsync(id);

            return entity?.MapEntityToResponseDto();
        });
    }

    public async Task<ResponseAboutDto?> UpdateAsync(Guid id, CreateUpdateAboutDto createUpdateAboutDto)
    {
        return await SafeExecuteNullableAsync(async () =>
        {
            var entity = createUpdateAboutDto.MapDtoToEntity(id);

            return await aboutRepository.UpdateAsync(entity)
                .ContinueWith(t => t.Result?.MapEntityToResponseDto());
        });
    }
}
