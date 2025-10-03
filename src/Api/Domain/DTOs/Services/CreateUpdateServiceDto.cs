using System;
using RbbSolucoes.Website.Backend.Api.Domain.DTOs.Shared;

namespace RbbSolucoes.Website.Backend.Api.Domain.DTOs.Services;

public class CreateUpdateServiceDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Icon { get; set; }
    public required int Order { get; set; }
}
