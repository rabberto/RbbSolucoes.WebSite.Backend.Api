using System;
using RbbSolucoes.Website.Backend.Api.Domain.Entities.Shared;

namespace RbbSolucoes.Website.Backend.Api.Domain.Entities;

public class AboutEntity : BaseEntity
{
    public required string Title { get; init; }
    public required string Content { get; init; }
    public string? ImageUrl { get; init; }
    public required string TitleMission { get; init; }
    public required string Mission { get; init; }
}
