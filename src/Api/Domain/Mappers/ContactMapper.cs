using System;
using RbbSolucoes.Website.Backend.Api.Domain.DTOs.Contact;
using RbbSolucoes.Website.Backend.Api.Domain.Entities;

namespace RbbSolucoes.Website.Backend.Api.Domain.Mappers;

public static class ContactMapper
{
    public static ResponseContactDto MapEntityToResponseDto(this ContactEntity contactEntity)
    {
        return new ResponseContactDto
        {
            Id = contactEntity.Id,
            Address = contactEntity.Address,
            Email = contactEntity.Email,
            Phone = contactEntity.Phone,
            Mobile = contactEntity.Mobile,
            SocialMedia = contactEntity.SocialMedia,
            CreatedAt = contactEntity.CreatedAt,
            UpdatedAt = contactEntity.UpdatedAt
        };
    }

    public static ContactEntity MapDtoToEntity(this CreateUpdateContactDto createUpdateContactDto, Guid? id = null)
    {
        return new ContactEntity
        {
            Id = id ?? Guid.NewGuid(),
            Address = createUpdateContactDto.Address,
            Email = createUpdateContactDto.Email,
            Phone = createUpdateContactDto.Phone,
            Mobile = createUpdateContactDto.Mobile,
            SocialMedia = createUpdateContactDto.SocialMedia
        };
    }
}
