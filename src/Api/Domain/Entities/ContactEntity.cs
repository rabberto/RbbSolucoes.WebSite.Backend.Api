using System;
using RbbSolucoes.Website.Backend.Api.Domain.Entities.Shared;
using RbbSolucoes.Website.Backend.Api.Domain.ValueObject;

namespace RbbSolucoes.Website.Backend.Api.Domain.Entities;

public class ContactEntity : BaseEntity
{
    public required string Address { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public required string Mobile { get; init; }
    public required SocialMedia SocialMedia { get; init; }
}