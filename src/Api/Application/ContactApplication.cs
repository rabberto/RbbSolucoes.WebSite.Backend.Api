using System;
using RbbSolucoes.Website.Backend.Api.Application.Shared;
using RbbSolucoes.Website.Backend.Api.Domain.DTOs.Contact;
using RbbSolucoes.Website.Backend.Api.Domain.Interface.Applications;
using RbbSolucoes.Website.Backend.Api.Domain.Interface.Repositories;
using RbbSolucoes.Website.Backend.Api.Domain.Mappers;

namespace RbbSolucoes.Website.Backend.Api.Application;

public class ContactApplication(IContactRepository contactRepository) : BaseApplication<ContactApplication>, IContactApplication
{
    public async Task<ResponseContactDto> CreateAsync(CreateUpdateContactDto createUpdateContactDto)
    {
        return await SafeExecuteAsync(async () =>
        {
            var entity = createUpdateContactDto.MapDtoToEntity();

            var createdEntity = await contactRepository.CreateAsync(entity);

            return createdEntity.MapEntityToResponseDto();
        });
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await SafeExecuteAsync(async () =>
        {
            return await contactRepository.DeleteAsync(id);
        });
    }

    public async Task<IEnumerable<ResponseContactDto>> GetAllAsync()
    {
        return await SafeExecuteAsync(async () =>
        {
            return await contactRepository.GetAllAsync()
                .ContinueWith(t => t.Result.Select(e => e.MapEntityToResponseDto()));
        });
    }

    public async Task<ResponseContactDto?> GetByIdAsync(Guid id)
    {
        return await SafeExecuteNullableAsync(async () =>
        {
            var entity = await contactRepository.GetByIdAsync(id);

            return entity?.MapEntityToResponseDto();
        });
    }

    public async Task<ResponseContactDto?> UpdateAsync(Guid id, CreateUpdateContactDto createUpdateContactDto)
    {
        return await SafeExecuteNullableAsync(async () =>
        {
            var entity = createUpdateContactDto.MapDtoToEntity(id);

            return await contactRepository.UpdateAsync(entity)
                .ContinueWith(t => t.Result?.MapEntityToResponseDto());
        });
    }
}
