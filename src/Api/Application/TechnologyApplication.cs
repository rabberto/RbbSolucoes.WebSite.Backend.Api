using System;
using System.Runtime.CompilerServices;
using RbbSolucoes.Website.Backend.Api.Application.Shared;
using RbbSolucoes.Website.Backend.Api.Domain.DTOs.Technology;
using RbbSolucoes.Website.Backend.Api.Domain.Interface.Applications;
using RbbSolucoes.Website.Backend.Api.Domain.Interface.Repositories;
using RbbSolucoes.Website.Backend.Api.Domain.Mappers;

namespace RbbSolucoes.Website.Backend.Api.Application;

public class TechnologyApplication(ITechnologyRepository technologyRepository) : BaseApplication<TechnologyApplication>, ITechnologyApplication
{
    public async Task<ResponseTechnologyDto> CreateAsync(CreateUpdateTechnologyDto createUpdateTechnologyDto)
    {
        return await SafeExecuteAsync(async () =>
        {
            return await technologyRepository.CreateAsync(createUpdateTechnologyDto.MapDtoToEntity())
                .ContinueWith(t => t.Result.MapEntityToResponseDto());
        });
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await SafeExecuteAsync(async () =>
        {
            return await technologyRepository.DeleteAsync(id);
        });
    }

    public async Task<IEnumerable<ResponseTechnologyDto>> GetAllAsync()
    {
        return await SafeExecuteAsync(async () =>
        {
            return await technologyRepository.GetAllAsync()
                .ContinueWith(t => t.Result.Select(e => e.MapEntityToResponseDto()));
        });
    }

    public async Task<ResponseTechnologyDto?> GetByIdAsync(Guid id)
    {
        return await SafeExecuteNullableAsync(async () =>
        {
            return await technologyRepository.GetByIdAsync(id)
                .ContinueWith(t => t.Result?.MapEntityToResponseDto());
        });
    }

    public async Task<ResponseTechnologyDto?> UpdateAsync(Guid id, CreateUpdateTechnologyDto entity)
    {
        return await SafeExecuteNullableAsync(async () =>
        {
            var techEntity = entity.MapDtoToEntity(id);
        
            return await technologyRepository.UpdateAsync(techEntity)
                .ContinueWith(t => t.Result?.MapEntityToResponseDto());
        });
    }
}
