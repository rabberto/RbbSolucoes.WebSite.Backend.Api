using System;
using Microsoft.AspNetCore.Mvc;
using RbbSolucoes.Website.Backend.Api.Domain.DTOs.ContactMessage;
using RbbSolucoes.Website.Backend.Api.Domain.Enums;
using RbbSolucoes.Website.Backend.Api.Domain.Interface.Applications;
using RbbSolucoes.Website.Backend.Api.Middleware;

namespace RbbSolucoes.Website.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactMessageController(IContactMessageApplication contactMessageApplication) : ControllerBase
{
    [HttpGet("{id}")]
    [RequireBasicAuth]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await contactMessageApplication.GetByIdAsync(id);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet]
    [RequireBasicAuth]
    public async Task<IActionResult> GetAll()
    {
        var result = await contactMessageApplication.GetAllAsync();
        return Ok(result);
    }

    [HttpPost]
    [RequireBasicAuth]
    public async Task<IActionResult> Create([FromBody] CreateContactMessageDto createContactMessageDto)
    {
        if (ModelState.IsValid is false)
            return BadRequest(ModelState);

        var result = await contactMessageApplication.CreateAsync(createContactMessageDto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}/status")]
    [RequireBasicAuth]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] StatusContactMessage status)
    {
        var updated = await contactMessageApplication.UpdateStatusAsync(id, status);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [RequireBasicAuth]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await contactMessageApplication.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

}
