using System;

namespace RbbSolucoes.Website.Backend.Api.Domain.Entities.Shared;

public abstract class BaseEntity
{
    public BaseEntity()
    {
        if (Id == Guid.Empty) Id = Guid.NewGuid();
    }

    public Guid Id { get; init; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }

    public void Update()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
