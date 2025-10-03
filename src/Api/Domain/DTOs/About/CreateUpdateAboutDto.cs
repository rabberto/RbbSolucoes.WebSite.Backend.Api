using System;
using RbbSolucoes.Website.Backend.Api.Domain.DTOs.Shared;

namespace RbbSolucoes.Website.Backend.Api.Domain.DTOs.About;

public class CreateUpdateAboutDto
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required string ImageUrl { get; init; }
    public required string Mission { get; init; }
    public required string TitleMission { get; init; }
}
