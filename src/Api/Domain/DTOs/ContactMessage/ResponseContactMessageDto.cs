using System;
using RbbSolucoes.Website.Backend.Api.Domain.DTOs.Shared;
using RbbSolucoes.Website.Backend.Api.Domain.Enums;

namespace RbbSolucoes.Website.Backend.Api.Domain.DTOs.ContactMessage;

public class ResponseContactMessageDto : BaseResponseDto
{
    public required string Company { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Message { get; set; }
    public StatusContactMessage Status { get; set; }
}
