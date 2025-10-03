using System;
using RbbSolucoes.Website.Backend.Api.Domain.DTOs.About;
using RbbSolucoes.Website.Backend.Api.Domain.Entities;

namespace RbbSolucoes.Website.Backend.Api.Domain.Mappers;

public static class AboutMapper
{
    public static ResponseAboutDto MapEntityToResponseDto(this AboutEntity aboutEntity)
    {
        return new ResponseAboutDto
        {
            Id = aboutEntity.Id,
            Title = aboutEntity.Title,
            Description = aboutEntity.Content,
            ImageUrl = aboutEntity.ImageUrl ?? string.Empty,
            Mission = aboutEntity.Mission,
            TitleMission = aboutEntity.TitleMission,
            CreatedAt = aboutEntity.CreatedAt,
            UpdatedAt = aboutEntity.UpdatedAt
        };
    }

    public static AboutEntity MapDtoToEntity(this CreateUpdateAboutDto createUpdateAboutDto, Guid? id = null)
    {
        return new AboutEntity
        {
            Id = id ?? Guid.Empty,
            Title = createUpdateAboutDto.Title,
            Content = createUpdateAboutDto.Description,
            ImageUrl = createUpdateAboutDto.ImageUrl,
            Mission = createUpdateAboutDto.Mission,
            TitleMission = createUpdateAboutDto.TitleMission
        };
    }
}
