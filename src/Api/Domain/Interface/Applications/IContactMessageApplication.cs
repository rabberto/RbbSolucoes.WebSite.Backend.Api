using System;
using RbbSolucoes.Website.Backend.Api.Domain.DTOs.ContactMessage;
using RbbSolucoes.Website.Backend.Api.Domain.Enums;

namespace RbbSolucoes.Website.Backend.Api.Domain.Interface.Applications;

public interface IContactMessageApplication
{
    Task<ResponseContactMessageDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<ResponseContactMessageDto>> GetAllAsync();
    Task<ResponseContactMessageDto> CreateAsync(CreateContactMessageDto entity);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> UpdateStatusAsync(Guid id, StatusContactMessage status);
}
