using System;
using RbbSolucoes.Website.Backend.Api.Domain.Entities.Shared;

namespace RbbSolucoes.Website.Backend.Api.Domain.Entities;

public class ServiceEntity : BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Icon { get; set; }
    public required int Order { get; set; }
}
