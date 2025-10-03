using System;
using RbbSolucoes.Website.Backend.Api.Domain.DTOs.Technology;
using RbbSolucoes.Website.Backend.Api.Domain.Entities;

namespace RbbSolucoes.Website.Backend.Api.Domain.Mappers;

public static class TechnologyMapper
{
    public static ResponseTechnologyDto MapEntityToResponseDto(this TechnologyEntity entity)
    {
        return new ResponseTechnologyDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Category = entity.Category,
            Icon = entity.Icon,
            Order = entity.Order,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
        };
    }

    public static TechnologyEntity MapDtoToEntity(this CreateUpdateTechnologyDto createUpdateTechnologyDto, Guid? id = null)
    {
        return new TechnologyEntity
        {
            Id = id ?? Guid.Empty,
            Name = createUpdateTechnologyDto.Name,
            Description = createUpdateTechnologyDto.Description,
            Category = createUpdateTechnologyDto.Category,
            Icon = createUpdateTechnologyDto.Icon,
            Order = createUpdateTechnologyDto.Order
        };
    }
}
