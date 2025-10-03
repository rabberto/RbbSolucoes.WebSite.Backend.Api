using System;
using RbbSolucoes.Website.Backend.Api.Domain.Entities.Shared;
using RbbSolucoes.Website.Backend.Api.Domain.Enums;

namespace RbbSolucoes.Website.Backend.Api.Domain.Entities;

public class ContactMessageEntity : BaseEntity
{
    public required string Company { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public required string Message { get; init; }
    public StatusContactMessage Status { get; private set; }    

    public void UpdateStatus(StatusContactMessage status)
    {
        Status = status;
    }
}
