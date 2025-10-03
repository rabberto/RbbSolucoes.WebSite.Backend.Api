using System;
using Microsoft.AspNetCore.Mvc;
using RbbSolucoes.Website.Backend.Api.Domain.DTOs.Contact;
using RbbSolucoes.Website.Backend.Api.Domain.Interface.Applications;
using RbbSolucoes.Website.Backend.Api.Middleware;

namespace RbbSolucoes.Website.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController(IContactApplication contactApplication) : ControllerBase
{
    [HttpGet("{id}")]
    [RequireBasicAuth]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await contactApplication.GetByIdAsync(id);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpGet]
    [RequireBasicAuth]
    public async Task<IActionResult> GetAll()
    {
        var result = await contactApplication.GetAllAsync();
        return Ok(result);
    }

    [HttpPost]
    [RequireBasicAuth]
    public async Task<IActionResult> Create([FromBody] CreateUpdateContactDto createUpdateContactDto)
    {
        if (ModelState.IsValid is false)
            return BadRequest(ModelState);

        var result = await contactApplication.CreateAsync(createUpdateContactDto);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [RequireBasicAuth]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateUpdateContactDto createUpdateContactDto)
    {
        if (ModelState.IsValid is false)
            return BadRequest(ModelState);

        var result = await contactApplication.UpdateAsync(id, createUpdateContactDto);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpDelete("{id}")]
    [RequireBasicAuth]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await contactApplication.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
