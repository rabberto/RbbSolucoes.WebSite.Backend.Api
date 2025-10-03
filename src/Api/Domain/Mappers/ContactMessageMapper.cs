using System;
using RbbSolucoes.Website.Backend.Api.Domain.DTOs.ContactMessage;
using RbbSolucoes.Website.Backend.Api.Domain.Entities;
using RbbSolucoes.Website.Backend.Api.Domain.Enums;

namespace RbbSolucoes.Website.Backend.Api.Domain.Mappers;

public static class ContactMessageMapper
{
    public static ContactMessageEntity MapDtoToEntity(this CreateContactMessageDto dto)
    {
        return new ContactMessageEntity
        {
            Company = dto.Company,
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            Message = dto.Message
        };
    }

    public static ResponseContactMessageDto MapEntityToDto(this ContactMessageEntity entity, Guid? id = null)
    {
        return new ResponseContactMessageDto
        {
            Id = id ?? Guid.Empty,
            Company = entity.Company,
            Name = entity.Name,
            Email = entity.Email,
            Phone = entity.Phone,
            Message = entity.Message,
            Status = entity.Status
        };
    }
}
