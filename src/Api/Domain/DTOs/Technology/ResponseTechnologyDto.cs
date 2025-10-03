using System;
using RbbSolucoes.Website.Backend.Api.Domain.DTOs.Shared;

namespace RbbSolucoes.Website.Backend.Api.Domain.DTOs.Technology;

public class ResponseTechnologyDto : BaseResponseDto
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Category { get; init; }
    public required string Icon { get; init; }
    public required int Order { get; init; }
}
