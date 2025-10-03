using System;

namespace RbbSolucoes.Website.Backend.Api.Domain.DTOs.ContactMessage;

public class CreateContactMessageDto
{
    public required string Company { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Message { get; set; }
}
