using System;
using RbbSolucoes.Website.Backend.Api.Application.Shared;
using RbbSolucoes.Website.Backend.Api.Domain.DTOs.ContactMessage;
using RbbSolucoes.Website.Backend.Api.Domain.Enums;
using RbbSolucoes.Website.Backend.Api.Domain.Interface.Applications;
using RbbSolucoes.Website.Backend.Api.Domain.Interface.Repositories;
using RbbSolucoes.Website.Backend.Api.Domain.Mappers;

namespace RbbSolucoes.Website.Backend.Api.Application;

public class ContactMessageApplication(IContactMessageRepository contactMessageRepository) : BaseApplication<ContactMessageApplication>, IContactMessageApplication
{
    public async Task<ResponseContactMessageDto> CreateAsync(CreateContactMessageDto createContactMessageDto)
    {
        return await SafeExecuteAsync(async () =>
        {
            var entity = createContactMessageDto.MapDtoToEntity();

            var createdEntity = await contactMessageRepository.CreateAsync(entity);

            return createdEntity.MapEntityToDto(createdEntity.Id);
        });
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await SafeExecuteAsync(async () =>
        {
            return await contactMessageRepository.DeleteAsync(id);
        });
    }

    public async Task<IEnumerable<ResponseContactMessageDto>> GetAllAsync()
    {
        return await SafeExecuteAsync(async () =>
        {
            return await contactMessageRepository.GetAllAsync()
                .ContinueWith(t => t.Result.Select(e => e.MapEntityToDto(e.Id)));
        });
    }

    public async Task<ResponseContactMessageDto?> GetByIdAsync(Guid id)
    {
        return await SafeExecuteNullableAsync(async () =>
        {
            var entity = await contactMessageRepository.GetByIdAsync(id);

            return entity?.MapEntityToDto(entity.Id);
        });
    }

    public async Task<bool> UpdateStatusAsync(Guid id, StatusContactMessage status)
    {
        return await SafeExecuteAsync(async () =>
        {
            var entity = await contactMessageRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            entity.UpdateStatus(status);
            var updatedEntity = await contactMessageRepository.UpdateAsync(entity);
        
            return updatedEntity != null;
        });
    }
}
