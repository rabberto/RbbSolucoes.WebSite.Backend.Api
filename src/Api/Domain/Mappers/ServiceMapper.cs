using System;
using RbbSolucoes.Website.Backend.Api.Domain.DTOs.Services;
using RbbSolucoes.Website.Backend.Api.Domain.Entities;

namespace RbbSolucoes.Website.Backend.Api.Domain.Mappers;

public static class ServiceMapper
{
    public static ResponseServiceDto MapEntityToResponseDto(this ServiceEntity entity)
    {
        return new ResponseServiceDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Icon = entity.Icon,
            Order = entity.Order,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
        };
    }

    public static ServiceEntity MapDtoToEntity(this CreateUpdateServiceDto createUpdateServiceDto, Guid? id = null)
    {
        return new ServiceEntity
        {
            Id = id ?? Guid.Empty,
            Name = createUpdateServiceDto.Name,
            Description = createUpdateServiceDto.Description,
            Icon = createUpdateServiceDto.Icon,
            Order = createUpdateServiceDto.Order
        };
    }
}
