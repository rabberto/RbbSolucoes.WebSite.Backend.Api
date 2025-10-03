using System;
using RbbSolucoes.Website.Backend.Api.Application.Shared;
using RbbSolucoes.Website.Backend.Api.Domain.DTOs.Services;
using RbbSolucoes.Website.Backend.Api.Domain.Entities;
using RbbSolucoes.Website.Backend.Api.Domain.Interface.Applications;
using RbbSolucoes.Website.Backend.Api.Domain.Interface.Repositories;
using RbbSolucoes.Website.Backend.Api.Domain.Mappers;

namespace RbbSolucoes.Website.Backend.Api.Application;

public class ServiceApplication(IServiceRepository serviceRepository) : BaseApplication<ServiceApplication>, IServiceApplication
{
    private readonly IServiceRepository _serviceRepository = serviceRepository;

    public async Task<ResponseServiceDto> CreateAsync(CreateUpdateServiceDto serviceDto)
    {
        return await SafeExecuteAsync(async () =>
        {
            return await _serviceRepository.CreateAsync(serviceDto.MapDtoToEntity())
                .ContinueWith(t => t.Result.MapEntityToResponseDto());
        });
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await SafeExecuteAsync(async () =>
        {
            return await _serviceRepository.DeleteAsync(id);
        });
    }

    public async Task<IEnumerable<ResponseServiceDto>> GetAllAsync()
    {
        return await SafeExecuteAsync(async () =>
        {
            return await _serviceRepository.GetAllAsync()
                .ContinueWith(t => t.Result.Select(e => e.MapEntityToResponseDto()));
        });
    }

    public async Task<ResponseServiceDto?> GetByIdAsync(Guid id)
    {
        return await SafeExecuteNullableAsync(async () =>
        {
            return await _serviceRepository.GetByIdAsync(id)
                .ContinueWith(t => t.Result?.MapEntityToResponseDto());
        });
    }

    public async Task<ResponseServiceDto?> UpdateAsync(Guid id, CreateUpdateServiceDto createUpdateServiceDto)
    {
        return await SafeExecuteNullableAsync(async () =>
        {
            var entity = createUpdateServiceDto.MapDtoToEntity(id);
        
            return await _serviceRepository.UpdateAsync(entity)
                .ContinueWith(t => t.Result?.MapEntityToResponseDto());
        });
    }
}
