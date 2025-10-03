using System;
using RbbSolucoes.Website.Backend.Api.Domain.ValueObject;

namespace RbbSolucoes.Website.Backend.Api.Domain.DTOs.Contact;

public class CreateUpdateContactDto
{
    public required string Address { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public required string Mobile { get; init; }
    public required SocialMedia SocialMedia { get; init; }
}
