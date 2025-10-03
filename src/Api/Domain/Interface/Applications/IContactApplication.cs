using System;
using RbbSolucoes.Website.Backend.Api.Domain.DTOs.Contact;

namespace RbbSolucoes.Website.Backend.Api.Domain.Interface.Applications;

public interface IContactApplication
{
    Task<ResponseContactDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<ResponseContactDto>> GetAllAsync();
    Task<ResponseContactDto> CreateAsync(CreateUpdateContactDto entity);
    Task<ResponseContactDto?> UpdateAsync(Guid id, CreateUpdateContactDto entity);
    Task<bool> DeleteAsync(Guid id);
}
