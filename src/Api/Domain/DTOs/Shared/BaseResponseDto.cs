using System;

namespace RbbSolucoes.Website.Backend.Api.Domain.DTOs.Shared;

public abstract class BaseResponseDto
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }

}
